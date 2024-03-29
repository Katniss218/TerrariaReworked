using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace TerrariaReworked.Tiles
{
	public class ResistantWood : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileMergeDirt[mod.TileType( "ResistantWood" )] = true;
			this.minPick = 200;
			Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = true;
			Main.tileBlockLight[Type] = true;
			Main.tileLighted[Type] = true;
			dustType = mod.DustType( "Sparkle" );

			//drop = mod.ItemType( "ResistantWood" ); null drop

			ModTranslation name = CreateMapEntryName();
			name.SetDefault( "Resistant Wood" );
			AddMapEntry( new Color( 50, 48, 46 ) );
		}

		public override void NumDust( int i, int j, bool fail, ref int num )
		{
			num = fail ? 1 : 3;
		}

		public override bool CanKillTile( int i, int j, ref bool blockDamaged )
		{
			return MyWorld.worldProgressionState == WorldProgressionState.Superhardmode;
		}
	}
}