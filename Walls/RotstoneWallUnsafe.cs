using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace TerrariaReworked.Walls
{
	public class RotstoneWallUnsafe : ModWall
	{
		public override void SetDefaults()
		{
			Main.wallHouse[Type] = true;
			dustType = mod.DustType( "Sparkle" );
			//drop = ItemType();
			AddMapEntry( new Color( 150, 150, 150 ) );
		}

		public override void NumDust( int i, int j, bool fail, ref int num )
		{
			num = fail ? 1 : 3;
		}
	}
}