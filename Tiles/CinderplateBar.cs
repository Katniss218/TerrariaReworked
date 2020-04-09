using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace TerrariaReworked.Tiles
{
	public class CinderplateBar : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileSolid[Type] = true;
			Main.tileSolidTop[Type] = true;
			Main.tileFrameImportant[Type] = true;
			//Main.tileNoAttach[Type] = false;
			Main.tileTable[Type] = false;

			//Main.tileLavaDeath[Type] = true;
			TileObjectData.newTile.CopyFrom( TileObjectData.Style1x1 );
			TileObjectData.newTile.CoordinateHeights = new int[] { 18 }; // something to do with frames maybe? (tile's height is 18, when it 'should' be 16 && it 'sinks' into the grass below)
			TileObjectData.addTile( Type );
			ModTranslation name = CreateMapEntryName();
			name.SetDefault( "Cinderplate Bar" );
			this.drop = mod.ItemType( "CinderplateBar" );

			this.AddMapEntry( new Color( 224, 194, 101 ), name );
			//dustType = mod.DustType("Sparkle");
			//this.disableSmartCursor = true;
			//this.adjTiles = new int[] { TileID.Anvils, TileID.MythrilAnvil, mod.TileType("AdamantiteAnvil") };
		}

		public override void NumDust( int i, int j, bool fail, ref int num )
		{
			num = fail ? 1 : 3;
		}/*
		public override void KillTile( int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem )
		{
			Item.NewItem( i * 16, j * 16, 16, 16, mod.ItemType( "OblivionBar" ) );
		}*/
	}
}