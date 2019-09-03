using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace TerrariaReworked.Tiles
{
	public class CaesiumForge : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileFrameImportant[Type] = true;
			//Main.tileNoAttach[Type] = true;
			//Main.tileLavaDeath[Type] = true;
			TileObjectData.newTile.CopyFrom( TileObjectData.Style3x2 );
			//TileObjectData.newTile.CoordinateHeights = new int[] { 18 }; // something to do with frames maybe? (tile's height is 18, when it 'should' be 16 && it 'sinks' into the grass below)
			TileObjectData.addTile( Type );
			//Main.tileShine[Type] = 1150;
			//Main.tileShine2[Type] = true;

			ModTranslation name = CreateMapEntryName();
			name.SetDefault( "Caesium Forge" );

			this.AddMapEntry( new Color( 200, 200, 200 ), name );
			//dustType = mod.DustType("Sparkle");
			this.disableSmartCursor = true;
			this.adjTiles = new int[] { TileID.Furnaces, TileID.AdamantiteForge };
		}

		public override void NumDust( int i, int j, bool fail, ref int num )
		{
			num = fail ? 1 : 3;
		}

		public override void KillMultiTile( int i, int j, int frameX, int frameY )
		{
			/*int item = 0;
			switch( frameX / 36 )
			{
				case 0:
					item = mod.ItemType( "AdamantiteAnvil" );
					break;
				case 1:
					item = mod.ItemType( "TitaniumAnvil" );
					break;
			}*/
			Item.NewItem( i * 16, j * 16, 48, 32, mod.ItemType( "CaesiumForge" ) );
		}
	}
}