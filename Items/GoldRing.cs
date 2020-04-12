using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrariaReworked.Items
{
	public class GoldRing : ModItem
	{
		public override void SetStaticDefaults()
		{
			this.DisplayName.SetDefault( "Gold Ring" );
			this.Tooltip.SetDefault( "Increases maximum mana by 80\n9% increased magic damage" );
		}

		public override void SetDefaults()
		{
			this.item.width = 28;
			this.item.height = 28;
			this.item.value = 350;
			this.item.rare = 0;
			this.item.accessory = true;
		}

		public override void UpdateAccessory( Player player, bool hideVisual )
		{
			player.statManaMax2 += 80;
			player.magicDamage += 0.09f;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe( this.mod );
			recipe.AddIngredient( ItemID.GoldBar, 9 );
			recipe.AddTile( TileID.Anvils );
			recipe.SetResult( this );
			recipe.AddRecipe();
		}
	}
}