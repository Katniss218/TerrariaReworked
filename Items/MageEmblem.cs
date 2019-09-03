using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrariaReworked.Items
{
	public class MageEmblem : ModItem
	{
		public override void SetStaticDefaults()
		{
			this.DisplayName.SetDefault( "Mage Emblem" );
			this.Tooltip.SetDefault( "25% increased magic damage" );
		}

		public override void SetDefaults()
		{
			this.item.width = 28;
			this.item.height = 28;
			this.item.value = 200000;
			this.item.rare = 5;
			this.item.accessory = true;
		}

		public override void UpdateAccessory( Player player, bool hideVisual )
		{
			player.magicDamage += 0.25f;
		}
		
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe( this.mod );
			recipe.AddIngredient( ItemID.SorcererEmblem, 2 );
			recipe.AddIngredient( ItemID.SoulofFright, 10 );
			recipe.AddIngredient( ItemID.SoulofMight, 10 );
			recipe.AddIngredient( ItemID.SoulofSight, 10 );
			recipe.AddTile( TileID.TinkerersWorkbench );
			recipe.SetResult( this );
			recipe.AddRecipe();
		}
	}
}