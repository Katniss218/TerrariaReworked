using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrariaReworked.NPCs
{
	public class TheRailMouth : ModNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault( "The Rail" );
			Main.npcFrameCount[npc.type] = 2;
			NPCID.Sets.TrailCacheLength[npc.type] = 10;    //The length of old position to be recorded
			NPCID.Sets.TrailingMode[npc.type] = 0;        //The recording mode
		}

		public override void SetDefaults()
		{
			npc.boss = true;
			music = MusicID.Boss3;
			npc.width = 130;
			npc.height = 106;// 212;
			npc.damage = 10;
			npc.defense = 50;
			npc.lifeMax = 3200;
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
			
			npc.frame.Width = 130;
			npc.frame.Height = 106;
		}

		bool spawned = false;
		
		public override void AI()
		{
			if( Main.netMode != 1 )
			{
				if( !spawned )
				{
					npc.realLife = npc.whoAmI;
					npc.netUpdate = true;

					int eye1 = NPC.NewNPC( (int)npc.position.X, (int)npc.position.Y - 200, mod.NPCType( "TheRailEyeMount" ), 0, -1, npc.whoAmI );
					int eye2 = NPC.NewNPC( (int)npc.position.X, (int)npc.position.Y + 200, mod.NPCType( "TheRailEyeMount" ), 0, 1, npc.whoAmI );

					Main.npc[eye1].realLife = npc.whoAmI;
					Main.npc[eye2].realLife = npc.whoAmI;

					spawned = true;

					if( !npc.active && Main.netMode == 2 )
					{
						NetMessage.SendData( MessageID.StrikeNPC, -1, -1, null, npc.whoAmI, -1f, 0f, 0f, 0, 0, 0 );
					}
				}
			}
			if( npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead || !Main.player[npc.target].active )
			{
				npc.TargetClosest( true );
			}
			bool playerDead = Main.player[npc.target].dead;

			Vector2 dir = (Main.player[npc.target].position + new Vector2( 0, 0 )) - npc.position;
			dir.Normalize();
			npc.velocity = dir * 5;

			Vector2 dir2 = Main.player[npc.target].position - npc.position;
			dir2.Normalize();

			npc.rotation = dir2.ToRotation();
			npc.direction = npc.velocity.X > 0 ? 1 : 0;
		}

		public override void NPCLoot()
		{
			if( MyWorld.worldProgressionState != WorldProgressionState.Superhardmode )
			{
				MyWorld.InitiateSuperhardmode( (int)(npc.position.X / 16), (int)(npc.position.Y / 16) );
			}

			Gore.NewGore( npc.position, npc.velocity, 9, 1f );
			Gore.NewGore( npc.position, npc.velocity, 10, 1f );
		}

		public override bool PreDraw( SpriteBatch spriteBatch, Color drawColor )
		{
			Vector2 vector11 = new Vector2( 65f, 53f );

			spriteBatch.Draw( ModContent.GetTexture( "TerrariaReworked/NPCs/WallOfSteel" ), npc.position - Main.screenPosition, null, drawColor, 0f, Vector2.Zero, 1f, npc.direction > 0 ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0 );

			ModMain.DrawShadowSprites( npc, 5, 2, vector11, true );


			return false;
		}
	}
}