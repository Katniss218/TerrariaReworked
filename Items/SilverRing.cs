using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrariaReworked.Items
{
	public class SilverRing : ModItem
	{
		public override void SetStaticDefaults()
		{
			this.DisplayName.SetDefault( "Silver Ring" );
			this.Tooltip.SetDefault( "Increases maximum mana by 60\n7% increased magic damage" );
		}

		public override void SetDefaults()
		{
			this.item.width = 28;
			this.item.height = 28;
			this.item.value = 4500;
			this.item.rare = 0;
			this.item.accessory = true;
		}

		public override void UpdateAccessory( Player player, bool hideVisual )
		{
			player.statManaMax2 += 60;
			player.magicDamage += 0.07f;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe( this.mod );
			recipe.AddIngredient( ItemID.SilverBar, 9 );
			recipe.AddTile( TileID.Anvils );
			recipe.SetResult( this );
			recipe.AddRecipe();
		}
	}
}