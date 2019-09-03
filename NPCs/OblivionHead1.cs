using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrariaReworked.NPCs
{
	public class OblivionHead1 : ModNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault( "Oblivion" );
			Main.npcFrameCount[npc.type] = 1;
			//NPCID.Sets.TrailCacheLength
			NPCID.Sets.TrailCacheLength[npc.type] = 10;    //The length of old position to be recorded
			NPCID.Sets.TrailingMode[npc.type] = 0;        //The recording mode
		}

		public override void SetDefaults()
		{
			npc.boss = true;
			music = MusicID.Boss3;
			npc.width = 160;
			npc.height = 180;// 212;
			npc.damage = 1;
			npc.defense = 1;
			npc.lifeMax = 10000;
			npc.HitSound = SoundID.NPCHit2;
			npc.DeathSound = SoundID.NPCDeath2;
			npc.value = 450f;
			npc.knockBackResist = 0;
			npc.aiStyle = -1;
			npc.npcSlots = 10;
			npc.noGravity = true;
			npc.noTileCollide = true;
			npc.netAlways = true;
		}
		
		public override void FindFrame( int frameHeight )
		{
			npc.frame.X = 0;
			npc.frame.Y = 0;
			/*npc.frameCounter++;
			if( npc.frameCounter < 10 )
			{
				npc.frame.Y = 0;
			}
			else if( npc.frameCounter < 20 )
			{
				npc.frame.Y = frameHeight;
			}
			else if( npc.frameCounter < 30 )
			{
				npc.frame.Y = frameHeight * 2;
			}
			else
			{
				npc.frameCounter = 0;
			}
			if( this.currentPhase == 1 )
			{
				npc.frame.Y += 642;
			}
			if( this.currentPhase == 2 )
			{
				npc.frame.Y += 1284;
			}*/
			npc.frame.Width = 160;
			npc.frame.Height = 180;
		}


		public override void AI()
		{
			if( npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead || !Main.player[npc.target].active )
			{
				npc.TargetClosest( true );
			}
			bool playerDead = Main.player[npc.target].dead;

			Vector2 dir = Main.player[npc.target].position - npc.position;
			dir.Normalize();
			npc.velocity = dir * 3;
			/*float rotationSpeedAmount = 0.02f;
			if( Main.expertMode )
			{
				rotationSpeedAmount *= 1.5f;
			}

			float distX = npc.position.X + (float)(npc.width / 2) - Main.player[npc.target].position.X - (float)(Main.player[npc.target].width / 2);
			float distY = npc.position.Y + (float)npc.height - 59f - Main.player[npc.target].position.Y - (float)(Main.player[npc.target].height / 2);
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
						currentPhase = 1;
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
					if( npc.ai[2] % 50 == 0 )
					{
						Vector2 dirToPlayer = Main.player[this.npc.target].getRect().Center.ToVector2() - this.npc.getRect().Center.ToVector2();
						dirToPlayer.Normalize();
						this.npc.velocity = dirToPlayer * 18f;
						this.npc.rotation = new Vector2( dirToPlayer.Y, -dirToPlayer.X ).ToRotation();
						Main.PlaySound( 15, (int)npc.position.X, (int)npc.position.Y, 0, 1f, 0f );
					}
					if( npc.ai[2] >= 100 )
					{
						FindNewTask();
						//Main.PlaySound( 15, (int)npc.position.X, (int)npc.position.Y, 0, 1f, 0f );
					}
				}
				else if( this.currentTask == TASK_CHARGE3 )
				{
					npc.ai[2]++;
					if( npc.ai[2] % 40 == 0 )
					{
						Vector2 dirToPlayer = Main.player[this.npc.target].getRect().Center.ToVector2() - this.npc.getRect().Center.ToVector2();
						dirToPlayer.Normalize();
						this.npc.velocity = dirToPlayer * 30f;
						this.npc.rotation = new Vector2( dirToPlayer.Y, -dirToPlayer.X ).ToRotation();
						Main.PlaySound( 36, (int)npc.position.X, (int)npc.position.Y, -1, 1f, 0f );
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
				}*/
			this.npc.velocity *= 0.97f;
			//}
		}

		public override void NPCLoot()
		{
			//if( MyWorld.worldProgressionState != WorldProgressionState.Superhardmode )
			//	MyWorld.InitiateSuperhardmode( (int)(this.npc.position.X / 16), (int)(this.npc.position.Y / 16) );
			//MyWorld.downedOblivion = true;

			// spawn 'dead' oblivion eye with the timer to reborn itself.
			Gore.NewGore( npc.position, npc.velocity, 9, 1f );
			Gore.NewGore( npc.position, npc.velocity, 10, 1f );
		}

		public override bool PreDraw( SpriteBatch spriteBatch, Color drawColor )
		{
			TerrariaReworked.DrawShadowSprites( npc, 5, 2 );

			return true;
		}
	}
}