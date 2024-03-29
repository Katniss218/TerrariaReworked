﻿using Terraria.ModLoader;
// If you are using c# 6, you can use: "using static Terraria.Localization.GameCulture;" which would mean you could just write "DisplayName.AddTranslation(German, "");"

namespace TerrariaReworked.Items
{
	public class Opal : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault( "'It's so sparkly'" );
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
			item.createTile = mod.TileType( "Opal" );
		}
	}
}