using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrariaReworked.Items
{
	public class OverlordEmblem : ModItem
	{
		public override void SetStaticDefaults()
		{
			this.DisplayName.SetDefault( "Overlord Emblem" );
			this.Tooltip.SetDefault( "20% increased damage" );
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
			player.meleeDamage += 0.25f;
			player.rangedDamage += 0.25f;
			player.magicDamage += 0.25f;
			player.minionDamage += 0.25f;
		}
		
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe( this.mod );
			recipe.AddIngredient( null, "BerserkerEmblem", 1 );
			recipe.AddIngredient( null, "ArcherEmblem", 1 );
			recipe.AddIngredient( null, "MageEmblem", 1 );
			recipe.AddIngredient( null, "MinionEmblem", 1 );
			recipe.AddTile( TileID.TinkerersWorkbench );
			recipe.SetResult( this );
			recipe.AddRecipe();
		}
	}
}