using Terraria.ID;
using Terraria.ModLoader;

namespace TerrariaReworked.Items
{
	public class OblivionBar : ModItem
	{
		/*public override void SetStaticDefaults()
		{
			this.Tooltip.SetDefault( "'The red is soft, the blue is hard.'" );
		}*/

		public override void SetDefaults()
		{
			this.item.width = 30;
			this.item.height = 24;
			this.item.maxStack = 99;
			this.item.useTurn = true;
			this.item.autoReuse = true;
			this.item.useAnimation = 15;
			this.item.useTime = 10;
			this.item.useStyle = 1;
			this.item.consumable = true;
			this.item.value = 150;
			this.item.createTile = this.mod.TileType( "OblivionBar" );
			this.item.placeStyle = 0;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe( this.mod );
			recipe.AddIngredient( null, "OblivionOre", 5 );
			recipe.AddTile( mod.TileType("CaesiumForge") );
			recipe.SetResult( this );
			recipe.AddRecipe();
		}
	}
}