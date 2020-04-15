using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrariaReworked.NPCs
{
	public class OblivionBody : ModNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault( "Oblivion" );
			Main.npcFrameCount[npc.type] = 1;
			NPCID.Sets.TrailCacheLength[npc.type] = 10;    //The length of old position to be recorded
			NPCID.Sets.TrailingMode[npc.type] = 0;        //The recording mode
		}

		public override void SetDefaults()
		{
			npc.boss = false;
			music = MusicID.Boss3;
			npc.width = 160;
			npc.height = 180;// 212;
			npc.damage = 0;
			npc.defense = 999;
			npc.lifeMax = 100000;
			npc.immortal = true;
			npc.dontTakeDamage = true;
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
			
			npc.frame.Width = 160;
			npc.frame.Height = 350;
		}
		
		public override void AI()
		{
			if( npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead || !Main.player[npc.target].active )
			{
				npc.TargetClosest( true );
			}
			bool playerDead = Main.player[npc.target].dead;

			Vector2 dir = (Main.player[npc.target].position + new Vector2( 0, 100 )) - npc.position;
			dir.Normalize();
			npc.velocity = dir * 5;
		}

		public override void NPCLoot()
		{
			Gore.NewGore( npc.position, npc.velocity, 9, 1f );
			Gore.NewGore( npc.position, npc.velocity, 10, 1f );
		}

		public override bool PreDraw( SpriteBatch spriteBatch, Color drawColor )
		{
			Vector2 vector11 = new Vector2( 80f, 90f );
			ModMain.DrawShadowSprites( npc, 5, 2, vector11, true );

			return false;
		}
	}
}