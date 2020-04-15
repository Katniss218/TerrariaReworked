using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace TerrariaReworked.NPCs
{
	public class OblivionHead1 : ModNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault( "Oblivion" );
			Main.npcFrameCount[npc.type] = 2;
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
			npc.value = 2500000f;
			npc.knockBackResist = 0;
			npc.aiStyle = -1;
			npc.npcSlots = 10;
			npc.noGravity = true;
			npc.noTileCollide = true;
			npc.netAlways = true;
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
			else
			{
				npc.frameCounter = 0;
			}
			
		}

		bool spawned = false;
		
		public override void AI()
		{
			if( !spawned )
			{
				NPC.NewNPC( (int)this.npc.position.X + 100, (int)this.npc.position.Y - 100, mod.NPCType( "OblivionHead2" ) );
				NPC.NewNPC( (int)this.npc.position.X, (int)this.npc.position.Y + 50, mod.NPCType( "OblivionBody" ) );

				spawned = true;
			}
			if( npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead || !Main.player[npc.target].active )
			{
				npc.TargetClosest( true );
			}
			bool playerDead = Main.player[npc.target].dead;

			Vector2 dir = (Main.player[npc.target].position + new Vector2( 100, -100 )) - npc.position;
			float speedMult = 7f;

			int head2 = NPC.FindFirstNPC( ModMain.instance.NPCType( "OblivionHead2" ) );
			if( head2 >= 0 )
			{
				dir = (Main.player[npc.target].position + new Vector2( 0, -100 )) - npc.position;
			}
			else
			{
				int body = NPC.FindFirstNPC( ModMain.instance.NPCType( "OblivionBody" ) );
				if( body >= 0 )
				{
					dir = (Main.npc[body].position + new Vector2( 0, -225 )) - npc.position;
					float len = dir.Length();
					if( len < 10 )
					{
						speedMult = dir.Length();
					}
					else
					{
						speedMult = 9;
					}
				}
			}

			dir.Normalize();
			npc.velocity = dir * speedMult;
			
		}

		public override bool PreNPCLoot()
		{
			Gore.NewGore( npc.position, npc.velocity, 9, 1f );
			Gore.NewGore( npc.position, npc.velocity, 10, 1f );

			if( NPC.CountNPCS( ModMain.instance.NPCType("OblivionHead2")) == 0 )
			{
				for( int i = 0; i < Main.maxNPCs; i++ )
				{
					if( Main.npc[i] == null )
					{
						continue;
					}

					if( Main.npc[i].type == ModMain.instance.NPCType("OblivionBody" ) )
					{
						Main.npc[i].life = 0;
						OblivionEye.NPCLoot( npc );
					}
				}
			}

			return false;
		}

		public override bool PreDraw( SpriteBatch spriteBatch, Color drawColor )
		{
			Vector2 vector11 = new Vector2( 80f, 90f );
			ModMain.DrawShadowSprites( npc, 5, 2, vector11, true );

			return false;
		}
	}
}