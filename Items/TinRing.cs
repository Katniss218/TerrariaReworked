using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrariaReworked.Items
{
	public class TinRing : ModItem
	{
		public override void SetStaticDefaults()
		{
			this.DisplayName.SetDefault( "Tin Ring" );
			this.Tooltip.SetDefault( "Increases maximum mana by 20\n3% increased magic damage" );
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
			player.statManaMax2 += 20;
			player.magicDamage += 0.03f;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe( this.mod );
			recipe.AddIngredient( ItemID.TinBar, 9 );
			recipe.AddTile( TileID.Anvils );
			recipe.SetResult( this );
			recipe.AddRecipe();
		}
	}
}