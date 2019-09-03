using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace TerrariaReworked.Tiles
{
	public class ImperviousBrick : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileMergeDirt[mod.TileType( "ImperviousBrick" )] = true;
			this.minPick = 250;
			Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = true;
			Main.tileBlockLight[Type] = true;
			Main.tileLighted[Type] = true;
			dustType = mod.DustType("Sparkle");
			drop = mod.ItemType("ImperviousBrick");
			AddMapEntry(new Color(15, 15, 15));
			//SetModTree(new ExampleTree());
		}

		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = fail ? 1 : 3;
		}
		
	}
}