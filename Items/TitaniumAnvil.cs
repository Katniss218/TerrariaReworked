using Terraria.ID;
using Terraria.ModLoader;

namespace TerrariaReworked.Items
{
	public class TitaniumAnvil : ModItem
	{
		public override void SetStaticDefaults()
		{
			this.Tooltip.SetDefault( "Used to craft high-end items" );
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
			this.item.createTile = this.mod.TileType( "AdamantiteAnvil" );
			this.item.placeStyle = 1;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe( this.mod );
			recipe.AddIngredient( ItemID.AdamantiteBar, 10 );
			recipe.AddTile( TileID.MythrilAnvil );
			recipe.SetResult( this );
			recipe.AddRecipe();
		}
	}
}