using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
// If you are using c# 6, you can use: "using static Terraria.Localization.GameCulture;" which would mean you could just write "DisplayName.AddTranslation(German, "");"
using Terraria.Localization;

namespace TerrariaReworked.Items
{
	public class Cinderplate : ModItem
	{
		public override void SetStaticDefaults()
		{
			this.Tooltip.SetDefault( "Melts your hands" );
		}

		public override void SetDefaults()
		{
			item.width = 12;
			item.height = 12;
			item.maxStack = 999;
			item.useTurn = true;
			item.autoReuse = true;
			item.useAnimation = 15;
			item.useTime = 10;
			item.useStyle = 1;
			item.rare = 8;
			item.consumable = true;
			item.createTile = mod.TileType("Cinderplate");
		}
	}
}
