using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrariaReworked.Items
{
	public class EyeofOblivion : ModItem
	{
		public override void SetStaticDefaults()
		{
			this.DisplayName.SetDefault( "Eye of Oblivion" );
			this.Tooltip.SetDefault( "Summons the Oblivion" );
		}

		public override void SetDefaults()
		{
			this.item.width = 30;
			this.item.height = 24;
			this.item.useStyle = 4;
			this.item.useTime = 44;
			this.item.useAnimation = 44;
			this.item.UseSound = SoundID.Item1;
			this.item.value = 0;
			this.item.rare = 8;
			this.item.maxStack = 20;
			this.item.consumable = true;
		}
		
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe( this.mod );
			recipe.AddIngredient( null, "BloodshotLens", 3 );
			recipe.AddIngredient( ItemID.BlackLens, 1 );
			recipe.AddTile( mod.TileType( "AdamantiteAnvil" ) );
			recipe.SetResult( this );
			recipe.AddRecipe();
		}
		
		public override bool CanUseItem( Player player )
		{
			if( Main.dayTime )
			{
				return false;
			}
			if( NPC.AnyNPCs( mod.NPCType( "OblivionEye" ) ) 
				|| NPC.AnyNPCs( mod.NPCType( "OblivionBody" ) )
				|| NPC.AnyNPCs( mod.NPCType( "OblivionHead1" ) )
				|| NPC.AnyNPCs( mod.NPCType( "OblivionHead2" ) ) )
			{
				return false;
			}
			return true;
		}

		public override bool UseItem( Player player )
		{
			NPC.SpawnOnPlayer( player.whoAmI, mod.NPCType( "OblivionEye" ) );
			Main.PlaySound( 15, -1, -1, 0 );
			return true;
		}
	}
}