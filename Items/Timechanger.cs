using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrariaReworked.Items
{
	public class Timechanger : ModItem
	{
		public override void SetStaticDefaults()
		{
			this.DisplayName.SetDefault( "Timechanger" );
			this.Tooltip.SetDefault( "Switches between the night and day!" );
		}

		public override void SetDefaults()
		{
			this.item.width = 24;
			this.item.height = 24;
			this.item.useStyle = 4;
			this.item.useTime = 30;
			this.item.useAnimation = 30;
			this.item.UseSound = SoundID.Item1;
			this.item.value = 135000;
			this.item.rare = 8;
		}
		
		public override bool UseItem( Player player )
		{
			Main.dayTime = !Main.dayTime;
			Main.time = 10000;
			if( Main.netMode == 0 )
			{
				if( Main.dayTime )
				{
					Main.NewText( "It is now Day.", 50, 255, 130 );
				}
				else Main.NewText( "It is now Night.", 50, 255, 130 );
			}
			else if( Main.netMode == 2 )
			{
				if( Main.dayTime )
				{
					//NetMessage.SendData( 25, -1, -1, "It is now Day.", 255, 50f, 255f, 130f, 0 );
				}
				//else NetMessage.SendData( 25, -1, -1, "It is now Night.", 255, 50f, 255f, 130f, 0 );
			}
			return true;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe( this.mod );
			recipe.AddIngredient( ItemID.SoulofLight, 15 );
			recipe.AddIngredient( ItemID.SoulofNight, 15 );
			recipe.AddRecipeGroup( "TerrariaReworked:CopperWatch", 1 );
			recipe.AddRecipeGroup( "TerrariaReworked:SilverWatch", 1 );
			recipe.AddRecipeGroup( "TerrariaReworked:GoldWatch", 1 );
			recipe.AddTile( TileID.MythrilAnvil );
			recipe.SetResult( this );
			recipe.AddRecipe();
		}
	}
}