using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrariaReworked.Items
{
	public class LeadRing : ModItem
	{
		public override void SetStaticDefaults()
		{
			this.DisplayName.SetDefault( "Lead Ring" );
			this.Tooltip.SetDefault( "Increases maximum mana by 40\n5% increased magic damage" );
		}

		public override void SetDefaults()
		{
			this.item.width = 28;
			this.item.height = 28;
			this.item.value = 1400;
			this.item.rare = 0;
			this.item.accessory = true;
		}

		public override void UpdateAccessory( Player player, bool hideVisual )
		{
			player.statManaMax2 += 40;
			player.magicDamage += 0.05f;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe( this.mod );
			recipe.AddIngredient( ItemID.LeadBar, 9 );
			recipe.AddTile( TileID.Anvils );
			recipe.SetResult( this );
			recipe.AddRecipe();
		}
	}
}