using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace TerrariaReworked.Tiles
{
	public class CursedForge : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileLighted[Type] = true;
			Main.tileFrameImportant[Type] = true;
			Main.tileNoAttach[Type] = true;
			Main.tileWaterDeath[Type] = true;
			Main.tileLavaDeath[Type] = true;
			//Main.tileFrameImportant[Type] = true;
			//Main.tileNoAttach[Type] = true;
			//Main.tileLavaDeath[Type] = true;
			TileObjectData.newTile.CopyFrom( TileObjectData.Style3x2 );
			//TileObjectData.newTile.CoordinateHeights = new int[] { 18 }; // something to do with frames maybe? (tile's height is 18, when it 'should' be 16 && it 'sinks' into the grass below)
			TileObjectData.addTile( Type );
			//Main.tileShine[Type] = 1150;
			//Main.tileShine2[Type] = true;

			ModTranslation name = CreateMapEntryName();
			name.SetDefault( "Cursed Forge" );

			this.AddMapEntry( new Color( 200, 200, 200 ), name );
			//dustType = mod.DustType("Sparkle");
			this.disableSmartCursor = true;
			this.adjTiles = new int[] { TileID.Furnaces, TileID.AdamantiteForge };
		}

		public override void NumDust( int i, int j, bool fail, ref int num )
		{
			num = fail ? 1 : 3;
		}

		public override void ModifyLight( int i, int j, ref float r, ref float g, ref float b )
		{
			int frameX = Main.tile[i, j].frameX;
			if( frameX >= 54 )
			{
				// ichor torch's values => 1.25, 1.25, 0.8
				r = 1.25f;
				g = 1.25f;
				b = 0.8f;
			}
			else
			{
				// cursed torch's values => 0.85, 1.0, 0.7
				r = 0.85f;
				g = 1f;
				b = 0.7f;
			}
		}

		public override void DrawEffects( int i, int j, SpriteBatch spriteBatch, ref Color drawColor, ref int nextSpecialDrawIndex )
		{
			/*if( !Main.gamePaused && Main.instance.IsActive && (!Lighting.UpdateEveryFrame || Main.rand.NextBool( 4 )) )
			{
				if( Main.rand.Next( 40 ) == 0 )
				{
					int dust = Dust.NewDust( new Vector2( (float)(j * 16 - 4), (float)(i * 16 - 6) ), 8, 6, 6, 0f, 0f, 100, default( Color ), 1f );
					if( Main.rand.Next( 3 ) != 0 )
					{
						Main.dust[dust].noGravity = true;
					}
				}
			}*/
		}


		public override void KillMultiTile( int i, int j, int frameX, int frameY )
		{
			int item = 0;
			switch( frameX / 54 )
			{
				case 0:
					item = mod.ItemType( "CursedForge" );
					break;
				case 1:
					item = mod.ItemType( "IchorForge" );
					break;
			}
			Item.NewItem( i * 16, j * 16, 48, 32, item );
		}
	}
}