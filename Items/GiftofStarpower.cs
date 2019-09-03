using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrariaReworked.Items
{
	public class GiftofStarpower : ModItem
	{
		public override void SetStaticDefaults()
		{
			this.DisplayName.SetDefault( "Gift of Starpower" );
			this.Tooltip.SetDefault( "5% decreased mana usage\nIncreases maximum mana by 40\nAutomatically use mana potions when needed\n12% increased magic damage" );
		}

		public override void SetDefaults()
		{
			this.item.width = 28;
			this.item.height = 28;
			this.item.value = 200000;
			this.item.rare = 4;
			this.item.accessory = true;
		}

		public override void UpdateAccessory( Player player, bool hideVisual )
		{
			player.statManaMax2 += 40;
			player.manaCost -= 0.05f;
			player.manaFlower = true;
			player.magicDamage += 0.12f;
		}
		
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe( this.mod );
			recipe.AddIngredient( ItemID.ManaFlower, 1 );
			recipe.AddIngredient( ItemID.BandofStarpower, 1 );
			recipe.AddIngredient( ItemID.SorcererEmblem, 1 );
			recipe.AddTile( TileID.TinkerersWorkbench );
			recipe.SetResult( this );
			recipe.AddRecipe();
		}
	}
}