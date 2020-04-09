using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace TerrariaReworked.Tiles
{
	public class Opal : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileMergeDirt[mod.TileType( "Opal" )] = true;
			this.minPick = 100;

			// tileShine: Copper Ore - 1100, Iron - 1150, Silver - 1050, Gold - 1000, Demonite - 1150, Hellstone - 0
			// tileShine: Tin Ore - 1125, Lead - 1075, Tungsten - 1025, Platinum - 975, Crimtane - 1150
			Main.tileShine[this.Type] = 500; // vanilla gems (178) have that set to 500
			Main.tileShine2[this.Type] = true; // vanilla gems (178) have that set to true
			Main.tileSpelunker[this.Type] = true; // vanilla gems (178) have that set to true
			Main.tileObsidianKill[this.Type] = true; // vanilla gems (178) have that set to true
			Main.tileFrameImportant[this.Type] = true; // vanilla gems (178) have that set to true.
			this.soundType = 0; // metallic 'click' sound on break.

			//Main.tileSolid[this.Type] = false;
			//Main.tileMergeDirt[this.Type] = false;
			//Main.tileBlockLight[this.Type] = false;
			//Main.tileLighted[this.Type] = false;

			dustType = mod.DustType( "Sparkle" );
			drop = mod.ItemType( "Opal" );
			ModTranslation name = CreateMapEntryName();
			name.SetDefault( "Opal" );
			AddMapEntry( new Color( 222, 78, 133 ) );
		}

		public override void NumDust( int i, int j, bool fail, ref int num )
		{
			num = fail ? 1 : 3;
		}

		public override bool CanPlace( int i, int j )
		{
			return WorldGen.SolidTile( i - 1, j ) || WorldGen.SolidTile( i + 1, j ) || WorldGen.SolidTile( i, j - 1 ) || WorldGen.SolidTile( i, j + 1 );
		}

		public override bool TileFrame( int i, int j, ref bool resetFrame, ref bool noBreak )
		{
			Tile tile = Main.tile[i, j];

			Tile tile2 = Main.tile[i, j - 1];
			Tile tile3 = Main.tile[i, j + 1];
			Tile tile4 = Main.tile[i - 1, j];
			Tile tile5 = Main.tile[i + 1, j];
			int type3 = -1;
			int type2 = -1;
			int type4 = -1;
			int type5 = -1;

			if( tile2 != null && tile2.active() && !tile2.bottomSlope() )
			{
				type2 = tile2.type;
			}
			if( tile3 != null && tile3.active() && !tile3.halfBrick() && !tile3.topSlope() )
			{
				type3 = tile3.type;
			}
			if( tile4 != null && tile4.active() )
			{
				type4 = tile4.type;
			}
			if( tile5 != null && tile5.active() )
			{
				type5 = tile5.type;
			}

			// order matters. We want to keep the behavior analogous to the vanilla gems.
			short randFrame = (short)(WorldGen.genRand.Next( 3 ) * 18);
			if( type3 >= 0 && Main.tileSolid[type3] && !Main.tileSolidTop[type3] )
			{
				if( tile.frameY < 0 || tile.frameY > 36 )
				{
					tile.frameY = /* 0 + */ randFrame;
				}
			}
			else if( type4 >= 0 && Main.tileSolid[type4] && !Main.tileSolidTop[type4] )
			{
				if( tile.frameY < 108 || tile.frameY > 54 )
				{
					tile.frameY = (short)(108 + randFrame);
				}
			}
			else if( type5 >= 0 && Main.tileSolid[type5] && !Main.tileSolidTop[type5] )
			{
				if( tile.frameY < 162 || tile.frameY > 198 )
				{
					tile.frameY = (short)(162 + randFrame);
				}
			}
			else if( type2 >= 0 && Main.tileSolid[type2] && !Main.tileSolidTop[type2] )
			{
				if( tile.frameY < 54 || tile.frameY > 90 )
				{
					tile.frameY = (short)(54 + randFrame);
				}
			}
			else // if can't attach, but it tried (bypassed the place check), kill the tile.
			{
				WorldGen.KillTile( i, j, false, false, false );
			}
			return false; // i guess we will let it.
		}

		// placing vanilla gem tiles.
		/*else if( type == 178 ) // gem id.
			{
				if( WorldGen.SolidTile( i - 1, j ) || WorldGen.SolidTile( i + 1, j ) || WorldGen.SolidTile( i, j - 1 ) || WorldGen.SolidTile( i, j + 1 ) ) // can place?
				{
					newTile.active( true );
					newTile.type = (ushort)type;
					newTile.frameX = (short)(style * 18); // switches between different tile ids.
					newTile.frameY = (short)(WorldGen.genRand.Next( 3 ) * 18);
					WorldGen.SquareTileFrame( i, j, true ); // recalculates the frames of 8 tiles around, and optionally the center.
				}
			}*/

	}
}