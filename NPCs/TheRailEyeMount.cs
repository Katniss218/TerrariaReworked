using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrariaReworked.NPCs
{
	public class TheRailEyeMount : ModNPC
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
			npc.boss = false;
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
		
		// ai0 = -1 for upper eye, 1 for lower eye.

		// ai1 = index of the mouth npc.

		public override void FindFrame( int frameHeight )
		{
			npc.frame.X = 0;
			npc.frame.Y = 0;
			
			npc.frame.Width = 130;
			npc.frame.Height = 106;
		}
		

		public override void AI()
		{
			if( npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead || !Main.player[npc.target].active )
			{
				npc.TargetClosest( true );
			}
			bool playerDead = Main.player[npc.target].dead;

			Vector2 dir = (Main.player[npc.target].position + new Vector2( 0, npc.ai[0] * 200 )) - npc.position;
			dir.Normalize();
			npc.velocity = dir * 5;

			Vector2 dir2 = Main.player[npc.target].position - npc.position;
			dir2.Normalize();

			npc.rotation = dir2.ToRotation();
			//npc.spriteDirection = npc.velocity.X > 0 ? 1 : 0;

			// if the life source (mouth) is no longer alive or valid - kill this npc.
			if( !Main.npc[(int)npc.ai[1]].active || Main.npc[(int)npc.ai[1]].type != mod.NPCType( "TheRailMouth" ) )
			{
				npc.life = 0;
				npc.HitEffect( 0, 10.0 );
				npc.active = false;
			}
		}

		public override void NPCLoot()
		{
			Gore.NewGore( npc.position, npc.velocity, 9, 1f );
			Gore.NewGore( npc.position, npc.velocity, 10, 1f );
		}

		public override bool PreDraw( SpriteBatch spriteBatch, Color drawColor )
		{
			Vector2 vector11 = new Vector2( 65f, 53f );
			ModMain.DrawShadowSprites( npc, 5, 2, vector11, true );

			return false;
		}

		public override bool? DrawHealthBar( byte hbPosition, ref float scale, ref Vector2 position )
		{
			return false;
		}
	}
}