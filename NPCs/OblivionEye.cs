using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace TerrariaReworked.NPCs
{
	public class OblivionEye : ModNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault( "Oblivion Eye" );
			Main.npcFrameCount[npc.type] = 9;
			NPCID.Sets.TrailCacheLength[npc.type] = 10;    //The length of old position to be recorded
			NPCID.Sets.TrailingMode[npc.type] = 1;        //The recording mode
		}
		
		public override void SetDefaults()
		{
			npc.boss = true;
			music = MusicID.Boss3;
			npc.width = 146;// 146;
			npc.height = 146; // 146;// 212;
			npc.damage = 90;
			npc.defense = 98;
			npc.lifeMax = 65000;
			npc.HitSound = SoundID.NPCHit1;
			npc.DeathSound = SoundID.NPCDeath6;
			npc.value = 450f;
			npc.knockBackResist = 0;
			npc.aiStyle = -1;
			this.aiType = -1;
			npc.npcSlots = 10;
			npc.noGravity = true;
			npc.noTileCollide = true;
			npc.netAlways = true;

			this.currentPhase = 0;
		}
		
		const int TASK_CHARGE = 0;
		const int TASK_FLAMES = 1;
		const int TASK_TRANSFORM = 2;
		const int TASK_CHARGE2 = 3;
		const int TASK_CHARGE3 = 4;
		const int TASK_OBLIVION_SHOWER = 5;

		const int transformLife = 45000;
		const int transformDamage = 160;
		const int transformDefense = 44;

		const int transform2Life = 9000;
		const int transform2Damage = 85;
		const int transform2Defense = 0;

		const int numCharges = 3;
		const int chargeDelay = 30;

		const int numCharges2 = 2;
		const int charge2Delay = 40;

		public int currentPhase { get { return (int)this.npc.ai[0]; } set { this.npc.ai[0] = value; } }
		public float currentTask { get { return this.npc.ai[1]; } set { this.npc.ai[1] = value; } }

		private void FindNewTask()
		{
			npc.ai[2] = 0;
			if( currentPhase == 0 )
			{
				if( this.currentTask == 0 )
				{
					this.currentTask = 1;
				}
				else if( this.currentTask == 1 )
				{
					this.currentTask = 0;
				}
			}
			else if( currentPhase == 1 )
			{
				if( this.currentTask == TASK_CHARGE2 )
				{
					this.currentTask = TASK_OBLIVION_SHOWER;
				}
				else if( this.currentTask == TASK_OBLIVION_SHOWER )
				{
					this.currentTask = TASK_CHARGE2;
				}
				else if( this.currentTask == TASK_TRANSFORM )
				{
					this.currentTask = TASK_CHARGE2;
				}
			}
			else if( currentPhase == 2 )
			{
				this.currentTask = TASK_CHARGE3;
			}
		}
		
		public override void FindFrame( int frameHeight )
		{
			int num = Main.npcTexture[npc.type].Height / Main.npcFrameCount[npc.type];

			npc.frame.X = 0;
			npc.frameCounter += 1.0;
			if( npc.frameCounter < 7.0 )
			{
				npc.frame.Y = 0;
			}
			else if( npc.frameCounter < 14.0 )
			{
				npc.frame.Y = num;
			}
			else if( npc.frameCounter < 21.0 )
			{
				npc.frame.Y = num * 2;
			}
			else
			{
				npc.frameCounter = 0;
			}

			if( this.currentPhase == 1f )
			{
				npc.frame.Y = npc.frame.Y + num * 3;
				return;
			}
			if( this.currentPhase >= 2f )
			{
				npc.frame.Y = npc.frame.Y + num * 6;
				return;
			}
		}


		public override void AI()
		{
			if( npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead || !Main.player[npc.target].active )
			{
				npc.TargetClosest( true );
			}
			bool playerDead = Main.player[npc.target].dead;

			float rotationSpeedAmount = 0.02f;
			if( Main.expertMode )
			{
				rotationSpeedAmount *= 1.5f;
			}

			float distX = npc.position.X + (float)(npc.width / 2) - Main.player[npc.target].position.X - (float)(Main.player[npc.target].width / 2);
			float distY = npc.position.Y + (float)(npc.height / 2) - Main.player[npc.target].position.Y - (float)(Main.player[npc.target].height / 2);
			float rotationToPlayer = (float)Math.Atan2( (double)distY, (double)distX ) + 1.57f; // halfPi
			if( rotationToPlayer < 0f )
			{
				rotationToPlayer += 6.283f;
			}
			else if( (double)rotationToPlayer > 6.283 ) // twoPi
			{
				rotationToPlayer -= 6.283f;
			}
			
			// make NPC rotate towards player.

			if( npc.rotation < rotationToPlayer )
			{
				if( (double)(rotationToPlayer - npc.rotation) > 3.1415 )
				{
					npc.rotation -= rotationSpeedAmount;
				}
				else
				{
					npc.rotation += rotationSpeedAmount;
				}
			}
			else if( npc.rotation > rotationToPlayer )
			{
				if( (double)(npc.rotation - rotationToPlayer) > 3.1415 )
				{
					npc.rotation += rotationSpeedAmount;
				}
				else
				{
					npc.rotation -= rotationSpeedAmount;
				}
			}
			if( npc.rotation > rotationToPlayer - rotationSpeedAmount && npc.rotation < rotationToPlayer + rotationSpeedAmount )
			{
				npc.rotation = rotationToPlayer;
			}
			if( npc.rotation < 0f )
			{
				npc.rotation += 6.283f;
			}
			else if( (double)npc.rotation > 6.283 )
			{
				npc.rotation -= 6.283f;
			}
			if( npc.rotation > rotationToPlayer - rotationSpeedAmount && npc.rotation < rotationToPlayer + rotationSpeedAmount )
			{
				npc.rotation = rotationToPlayer;
			}


			// flee mechanic

			if( Main.dayTime || playerDead )
			{
				npc.velocity.Y -= 0.04f;
				//ptr -= 0.04f;
				if( npc.timeLeft > 10 )
				{
					npc.timeLeft = 10;
					return;
				}
			}
			else
			{
				if( this.currentTask == TASK_CHARGE )
				{
					npc.ai[2]++;
					if( npc.ai[2] % chargeDelay == 0 )
					{
						//Vector2 dirToPlayer = Main.player[this.npc.target].getRect().Center.ToVector2() - this.npc.getRect().Center.ToVector2();
						//dirToPlayer.Normalize();
						this.npc.velocity = npc.rotation.ToRotationVector2().RotatedBy( MathHelper.ToRadians( 90 ) ) * 12f;
						//this.npc.rotation = new Vector2( dirToPlayer.Y, -dirToPlayer.X ).ToRotation();
					}
					if( npc.ai[2] >= (numCharges * chargeDelay) )
					{
						FindNewTask();
					}
				}
				else if( this.currentTask == TASK_FLAMES )
				{
					npc.ai[2]++;
					if( npc.ai[2] == 10 )
					{
						Vector2 dirToPlayer = Main.player[this.npc.target].getRect().Center.ToVector2() - this.npc.getRect().Center.ToVector2();
						dirToPlayer.Normalize();

						this.npc.rotation = new Vector2( dirToPlayer.Y, -dirToPlayer.X ).ToRotation();
						Vector2 center = this.npc.getRect().Center.ToVector2();
						int shotCount = 3;
						int deflection = 20;
						if( npc.life >= 55000 )
						{
							shotCount = 3;
							deflection = 20;
						}
						else if( npc.life >= 45000 )
						{
							shotCount = 4;
							deflection = 25;
						}
						else
						{
							shotCount = 5;
							deflection = 30;
						}
						const float minSpeed = 8f;
						const float maxSpeed = 12f;
						for( int i = 0; i < shotCount; i++ )
						{
							Projectile.NewProjectile( center, dirToPlayer.RotatedBy( MathHelper.ToRadians( Main.rand.NextFloat( -deflection, deflection ) ) ) * Main.rand.NextFloat( minSpeed, maxSpeed ), mod.ProjectileType( "OblivionFlame" ), 60, 2 );
						}
					}
					if( npc.ai[2] == 50 )
					{
						FindNewTask();
					}
				}
				else if( currentTask == TASK_TRANSFORM )
				{
					npc.ai[2]++;
					//npc.velocity = Vector2.Zero;

					if( npc.ai[2] <= 100 )
						npc.ai[3] = npc.ai[2] / 200f;
					else
						npc.ai[3] = (200 - npc.ai[2]) / 200f;
					npc.rotation += npc.ai[3];

					if( npc.ai[2] == 100 )
					{
						this.currentPhase = 1;
						npc.damage = transformDamage;
						npc.defense = transformDefense;
						Main.PlaySound( 15, (int)npc.position.X, (int)npc.position.Y, 0, 1f, 0f );
						Gore.NewGore( npc.position, npc.velocity, 8, 1f );
					}
					if( npc.ai[2] >= 200 ) // wair till 100 for anim change, till 200 resume.
					{
						FindNewTask();
					}
				}
				else if( this.currentTask == TASK_CHARGE2 )
				{
					npc.ai[2]++;
					if( npc.ai[2] % charge2Delay == 0 )
					{
						Vector2 dirToPlayer = Main.player[this.npc.target].getRect().Center.ToVector2() - this.npc.getRect().Center.ToVector2();
						dirToPlayer.Normalize();
						this.npc.velocity = dirToPlayer * 20f;
						this.npc.rotation = new Vector2( dirToPlayer.Y, -dirToPlayer.X ).ToRotation();
						Main.PlaySound( 15, (int)npc.position.X, (int)npc.position.Y, 0, 1f, 0f );
					}
					if( npc.ai[2] >= (numCharges2 * charge2Delay) + (charge2Delay-1) )
					{
						FindNewTask();
					}
				}
				else if( this.currentTask == TASK_OBLIVION_SHOWER )
				{
					Vector2 center = this.npc.getRect().Center.ToVector2();
					Vector2 dirToPlayer = Main.player[this.npc.target].getRect().Center.ToVector2() - center;

					int offset = 90;
					Vector2 dir = this.npc.rotation.ToRotationVector2();
					//dir.Normalize();
					dir = new Vector2( dir.Y, -dir.X );

					int boxRadius = 15;
					float boxRadiusMult = 0.3f;
					Vector2 centerOffset1 = center + (dir * -offset);
					Vector2 centerOffset2 = center + (dir * -offset) + (dir * -boxRadius * 1);
					Vector2 centerOffset3 = center + (dir * -offset) + (dir * -boxRadius * 2);
					Vector2 centerOffset4 = center + (dir * -offset) + (dir * -boxRadius * 3);
					for( int i = 0; i < 2; i++ )
					{
						Dust.NewDust( centerOffset1 - new Vector2( boxRadius * 1 + boxRadiusMult, boxRadius * 1 + boxRadiusMult ), (int)(2 * boxRadius * 1 + boxRadiusMult), (int)(2 * boxRadius * boxRadiusMult), ModMain.instance.DustType( "OblivionDust" ), this.npc.velocity.X * 0.5f, this.npc.velocity.Y * 0.5f, 100, default, 2 );
						Dust.NewDust( centerOffset2 - new Vector2( boxRadius * (1 + boxRadiusMult * 2), boxRadius * (1 + boxRadiusMult * 2) ), (int)(2 * boxRadius * (1 + boxRadiusMult * 2)), (int)(2 * boxRadius * (boxRadiusMult * 2)), ModMain.instance.DustType( "OblivionDust" ), this.npc.velocity.X * 0.5f, this.npc.velocity.Y * 0.5f, 100, default, 2 );
						Dust.NewDust( centerOffset3 - new Vector2( boxRadius * (1 + boxRadiusMult * 3), boxRadius * (1 + boxRadiusMult * 3) ), (int)(2 * boxRadius * (1 + boxRadiusMult * 3)), (int)(2 * boxRadius * (boxRadiusMult * 3)), ModMain.instance.DustType( "OblivionDust" ), this.npc.velocity.X * 0.5f, this.npc.velocity.Y * 0.5f, 100, default, 2 );
						Dust.NewDust( centerOffset4 - new Vector2( boxRadius * (1 + boxRadiusMult * 4), boxRadius * (1 + boxRadiusMult * 4) ), (int)(2 * boxRadius * (1 + boxRadiusMult * 4)), (int)(2 * boxRadius * (boxRadiusMult * 4)), ModMain.instance.DustType( "OblivionDust" ), this.npc.velocity.X * 0.5f, this.npc.velocity.Y * 0.5f, 100, default, 2 );
					}

					if( npc.ai[2] < 5 )
					{
						this.npc.velocity *= 0.2f;
						//this.npc.rotation = new Vector2( dirToPlayer.Y, -dirToPlayer.X ).ToRotation();
						//Main.PlaySound( 15, (int)npc.position.X, (int)npc.position.Y, 0, 1f, 0f );
					}
					else
					{
						if( npc.ai[2] % 8 == 0 )
						{
							float dirRotation = new Vector2( dirToPlayer.Y, -dirToPlayer.X ).ToRotation();
							npc.rotation = dirRotation;

							dirToPlayer.Normalize();
							this.npc.velocity *= 0.25f;
							this.npc.velocity += dirToPlayer * 14f;
							//this.npc.rotation = new Vector2( dirToPlayer.Y, -dirToPlayer.X ).ToRotation();
							Main.PlaySound( 15, (int)npc.position.X, (int)npc.position.Y, 0, 1f, 0f );
							Main.PlaySound( 15, (int)npc.position.X, (int)npc.position.Y, 0, 1f, 0f );
						}
					}
					npc.ai[2]++;
					if( npc.ai[2] % 16 == 0 )
					{
						dirToPlayer.Normalize();
						
						int shotCount = 3;
						float deflection = Main.rand.NextFloat( -40, 40 ); // deg
						float deflectionRandMax = 22;
						const float minSpeed = 5f;
						const float maxSpeed = 9f;
						for( int i = 0; i < shotCount; i++ )
						{
							Projectile.NewProjectile( centerOffset3, dirToPlayer.RotatedBy( MathHelper.ToRadians( deflection ) + MathHelper.ToRadians( Main.rand.NextFloat( -deflectionRandMax, deflectionRandMax ) ) ) * Main.rand.NextFloat( minSpeed, maxSpeed ), mod.ProjectileType( "OblivionFlame2" ), 60, 2 );
						}
					}
					if( npc.ai[2] >= 60 )
					{
						FindNewTask();
					}
				}
				else if( this.currentTask == TASK_CHARGE3 )
				{
					npc.ai[2]++;
					if( npc.ai[2] % 40 == 0 )
					{
						Vector2 dirToPlayer = Main.player[this.npc.target].getRect().Center.ToVector2() - this.npc.getRect().Center.ToVector2();
						dirToPlayer.Normalize();
						this.npc.velocity = dirToPlayer * 37f;
						this.npc.rotation = new Vector2( dirToPlayer.Y, -dirToPlayer.X ).ToRotation();
						Main.PlaySound( 36, (int)npc.position.X, (int)npc.position.Y, -1, 1f, 0f );
					}
					if( npc.ai[2] % 40 < 24 )
					{
						Vector2 center = this.npc.getRect().Center.ToVector2();
						int offset = 0;
						Vector2 dir = this.npc.rotation.ToRotationVector2();
						//dir.Normalize();
						dir = new Vector2( dir.Y, -dir.X );

						int boxRadius = 50;
						float boxRadiusMult = 0.3f;
						Vector2 centerOffset1 = center + (dir * -offset);
						Dust.NewDust( centerOffset1 - new Vector2( boxRadius * 1 + boxRadiusMult, boxRadius * 1 + boxRadiusMult ), (int)(2 * boxRadius * 1 + boxRadiusMult), (int)(2 * boxRadius * boxRadiusMult), ModMain.instance.DustType( "OblivionDust" ), this.npc.velocity.X * 0.5f, this.npc.velocity.Y * 0.5f, 100, default, 2 );
					}
					if( npc.ai[2] % 40 == 32 )
					{
						this.npc.velocity *= 0.15f;
					}
					if( npc.ai[2] >= 80 )
					{
						FindNewTask();
						Main.PlaySound( 15, (int)npc.position.X, (int)npc.position.Y, 0, 1f, 0f );
					}
				}
				if( this.currentPhase == 0 )
				{
					if( this.currentTask != TASK_TRANSFORM && this.npc.life < transformLife )
					{
						this.currentTask = TASK_TRANSFORM;
						npc.ai[2] = 0;
					}
				}
				if( this.currentPhase == 1 )
				{
					if( this.npc.life < transform2Life )
					{
						Gore.NewGore( npc.position, npc.velocity, 8, 1f );
						this.currentPhase = 2;
						npc.ai[2] = 0;
						this.FindNewTask();
					}
				}
				this.npc.velocity *= 0.97f;
			}
		}

		public const string REBORN_MESSAGE = "Oblivion has been reborn!";

		public override bool PreNPCLoot()
		{
			//if( MyWorld.worldProgressionState != WorldProgressionState.Superhardmode )
			//	MyWorld.InitiateSuperhardmode( (int)(this.npc.position.X / 16), (int)(this.npc.position.Y / 16) );

			NPC.NewNPC( (int)this.npc.position.X - 100, (int)this.npc.position.Y - 200, mod.NPCType( "OblivionHead1" ) );
			
			//MyWorld.downedOblivion = true;

			if( Main.netMode == 0 )
			{
				Main.NewText( REBORN_MESSAGE, 255, 60, 0 );
			}
			else if( Main.netMode == 2 )
			{
				NetworkText text = NetworkText.FromLiteral( REBORN_MESSAGE );
				NetMessage.SendData( 25, -1, -1, text, 255, 255f, 60f, 0f, 0 ); // chat
			}

			// spawn 'dead' oblivion eye with the timer to reborn itself.
			Gore.NewGore( npc.position, npc.velocity, 9, 1f );
			Gore.NewGore( npc.position, npc.velocity, 10, 1f );

			return false;
		}

		public static void NPCLoot( NPC npc )
		{
			MyWorld.downedOblivion = true;
			if( Main.netMode == 0 )
			{
				Main.NewText( Language.GetTextValue( "Announcement.HasBeenDefeated_Single", "Oblivion" ), 175, 75, byte.MaxValue, false );
			}
			else if( Main.netMode == 2 )
			{
				NetMessage.BroadcastChatMessage( NetworkText.FromKey( "Announcement.HasBeenDefeated_Single", new object[]
				{
					"The Oblivion"
				} ), new Color( 175, 75, 255 ), -1 );
			}

			Item.NewItem( npc.getRect(), ItemID.SuperHealingPotion, Main.rand.Next( 10, 21 ) );
			Item.NewItem( npc.getRect(), ModMain.instance.ItemType( "OblivionOre" ), Main.rand.Next( 150, 201 ) );
			ModMain.DropCoins( npc );
		}

		public override bool PreDraw( SpriteBatch spriteBatch, Color drawColor )
		{
			Vector2 vector11 = new Vector2( 72f, 132f );
			ModMain.DrawShadowSprites( npc, 5, 2, vector11, true );
			
			return false;
		}
	}
}