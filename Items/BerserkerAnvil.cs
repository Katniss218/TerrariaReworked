using Terraria.ID;
using Terraria.ModLoader;

namespace TerrariaReworked.Items
{
	public class BerserkerAnvil : ModItem
	{
		public override void SetStaticDefaults()
		{
			this.Tooltip.SetDefault( "Used to craft endgame items" );
		}

		public override void SetDefaults()
		{
			this.item.width = 28;
			this.item.height = 14;
			this.item.maxStack = 99;
			this.item.useTurn = true;
			this.item.autoReuse = true;
			this.item.useAnimation = 15;
			this.item.useTime = 10;
			this.item.useStyle = 1;
			this.item.consumable = true;
			this.item.value = 150;
			this.item.createTile = mod.TileType( "BerserkerAnvil" );
			this.item.placeStyle = 0;
		}
		
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe( mod );
			recipe.AddIngredient( mod.ItemType( "BerserkerBar" ), 10 );
			recipe.AddTile( ModMain.instance.TileType( "AdamantiteAnvil" ) );
			recipe.SetResult( this );
			recipe.AddRecipe();
		}
	}
}