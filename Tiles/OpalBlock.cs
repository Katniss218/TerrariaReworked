using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace TerrariaReworked.Tiles
{
	public class OpalBlock : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileMergeDirt[mod.TileType( "OpalBlock" )] = true;
			this.minPick = 100;

			// tileShine: Copper Ore - 1100, Iron - 1150, Silver - 1050, Gold - 1000, Demonite - 1150, Hellstone - 0
			// tileShine: Tin Ore - 1125, Lead - 1075, Tungsten - 1025, Platinum - 975, Crimtane - 1150
			Main.tileShine[this.Type] = 1450;
			Main.tileShine2[this.Type] = true;
			Main.tileSpelunker[this.Type] = true;
			this.soundType = 21; // metallic 'click' sound on break.

			Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = true;
			Main.tileBlockLight[Type] = true;
			Main.tileLighted[Type] = true;
			dustType = mod.DustType( "Sparkle" );
			drop = mod.ItemType( "Opal" );
			ModTranslation name = CreateMapEntryName();
			name.SetDefault( "OpalBlock" );
			AddMapEntry( new Color( 222, 78, 133 ) );
		}

		public override void NumDust( int i, int j, bool fail, ref int num )
		{
			num = fail ? 1 : 3;
		}
	}
}