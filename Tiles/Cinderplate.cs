using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ModLoader;

namespace TerrariaReworked.Tiles
{
	public class Cinderplate : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileMergeDirt[mod.TileType( "Cinderplate" )] = true;
			this.minPick = 250;

			// tileShine: Copper Ore - 1100, Iron - 1150, Silver - 1050, Gold - 1000, Demonite - 1150, Hellstone - 0
			// tileShine: Tin Ore - 1125, Lead - 1075, Tungsten - 1025, Platinum - 975, Crimtane - 1150
			Main.tileShine[this.Type] = 0;
			Main.tileShine2[this.Type] = true;
			Main.tileSpelunker[this.Type] = true;
			this.soundType = 21; // metallic 'click' sound on break.

			Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = true;
			Main.tileBlockLight[Type] = true;
			Main.tileLighted[Type] = true;
			dustType = mod.DustType("Sparkle");
			drop = mod.ItemType( "Cinderplate" );
			ModTranslation name = CreateMapEntryName();
			name.SetDefault( "Cinderplate" );
			AddMapEntry(new Color(222, 78, 133));
		}
		
		public override bool CreateDust( int x, int y, ref int type )
		{
			Dust.NewDust( new Vector2( x, y ) * 16f, 16, 16, 60, 0f, 0f, 1, new Color( 255, 255, 255 ), 1f );
			Dust.NewDust( new Vector2( x, y ) * 16f, 16, 16, 1, 0f, 0f, 1, new Color( 100, 100, 100 ), 1f );
			return false;
		}

		public override void ModifyLight( int i, int j, ref float r, ref float g, ref float b )
		{
			r = 0.075f;
			g = 0.0375f;
			b = 0.0f;
			float num = 3.4f;
			float bonusStrength = 0.5f;
			float strength = num + (float)Math.Sin( (double)MathHelper.ToRadians( (float)(Main.time / 6.0) ) ) * bonusStrength;
			Lighting.AddLight( new Vector2( (float)(i * 16) + 8f, (float)(j * 16) + 8f ), r * strength, g * strength, b * strength );
			/*if( Main.netMode != 2 ) // 57 - hallow dust.
			{
				if( Main.rand.Next( 100 ) == 0 )
				{
					int dust = Dust.NewDust( new Vector2( (float)(i * 16) + 20f, (float)(j * 16) + 12f ), 4, 4, 55, 0, -0.3f, 0, new Color(), 1f );
					Main.dust[dust].noGravity = true;
				}
			}*/
		}

		public override void PostDraw( int x, int y, SpriteBatch spriteBatch )
		{
			if( !Main.tile[x, y].active() )
			{
				return;
			}

			int xPos = Main.tile[x, y].frameX;
			int yPos = Main.tile[x, y].frameY;

			Texture2D glowmask = ModMain.instance.GetTexture( "Tiles/CinderplateGlow" );

			Vector2 offset = Main.drawToScreen ? Vector2.Zero : new Vector2( Main.offScreenRange );
			Vector2 posOnScreen = new Vector2( x * 16 - Main.screenPosition.X, y * 16 - Main.screenPosition.Y ) + offset;
			Color drawColor = this.GetDrawColor( x, y, new Color( 180, 180, 180, 20 ) );

			if( !Main.tile[x, y].halfBrick() && Main.tile[x, y].slope() == 0 )
			{
				Main.spriteBatch.Draw( glowmask, posOnScreen, new Rectangle?( new Rectangle( xPos, yPos, 18, 18 ) ), drawColor, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f );
			}
			else if( Main.tile[x, y].halfBrick() )
			{
				Main.spriteBatch.Draw( glowmask, posOnScreen + new Vector2( 0f, 8f ), new Rectangle?( new Rectangle( xPos, yPos, 18, 8 ) ), drawColor, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f );
			}
		}
		
		private Color GetDrawColor( int x, int y, Color color )
		{
			int colType = Main.tile[x, y].color();
			Color paintCol = WorldGen.paintColor( colType );
			if( colType >= 13 && colType <= 24 )
			{
				color.R = (byte)(paintCol.R / 255f * color.R);
				color.G = (byte)(paintCol.G / 255f * color.G);
				color.B = (byte)(paintCol.B / 255f * color.B);
			}
			return color;
		}
	}
}