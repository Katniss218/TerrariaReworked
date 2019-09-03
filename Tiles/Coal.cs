using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrariaReworked.Tiles
{
	public class Coal : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileSolid[this.Type] = true;
			Main.tileMergeDirt[this.Type] = true;
			Main.tileBlockLight[this.Type] = true;
			Main.tileLighted[this.Type] = true;

			// tileShine: Copper Ore - 1100, Iron - 1150, Silver - 1050, Gold - 1000, Demonite - 1150, Hellstone - 0
			// tileShine: Tin Ore - 1125, Lead - 1075, Tungsten - 1025, Platinum - 975, Crimtane - 1150
			Main.tileShine[this.Type] = 1450;
			Main.tileShine2[this.Type] = true;
			Main.tileSpelunker[this.Type] = true;
			this.soundType = 21; // metallic 'click' sound on break.
								 //this.minPick = 35;
			ModTranslation name = CreateMapEntryName();
			name.SetDefault( "Coal" );
			this.dustType = 36;
			this.drop = ItemID.Coal;
			this.AddMapEntry( new Color( 25, 25, 25 ) );
		}
		/*
		public override void NumDust( int i, int j, bool fail, ref int num )
		{
			num = fail ? 1 : 3;
		}
		
		public override void ModifyLight( int i, int j, ref float r, ref float g, ref float b )
		{
			r = 0.5f;
			g = 0.5f;
			b = 0.5f;
		}
		
		public override void ChangeWaterfallStyle( ref int style )
		{
			style = mod.GetWaterfallStyleSlot( "ExampleWaterfallStyle" );
		}

		public override int SaplingGrowthType( ref int style )
		{
			style = 0;
			return mod.TileType( "ExampleSapling" );
		}*/
	}
}