using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrariaReworked.Items
{
	public class WoodenPickaxe : ModItem
	{
		/*public override void SetStaticDefaults()
		{
			Tooltip.SetDefault( "This is a modded pickaxe." );
		}
		*/
		public override void SetDefaults()
		{
			this.item.damage = 4;
			this.item.melee = true;
			this.item.width = 32;
			this.item.height = 32;
			this.item.useTime = 15;
			this.item.useAnimation = 20;
			this.item.pick = 25;
			this.item.tileBoost = -1;
			this.item.useStyle = 1;
			this.item.knockBack = 2;
			this.item.value = 50;
			this.item.rare = 0;
			this.item.UseSound = SoundID.Item1;
			this.item.autoReuse = true;
			this.item.useTurn = true;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe( this.mod );
			recipe.AddIngredient( ItemID.Wood, 15 );
			recipe.AddTile( TileID.WorkBenches );
			recipe.SetResult( this );
			recipe.AddRecipe();
		}
		/*
		public override void MeleeEffects( Player player, Rectangle hitbox )
		{
			if( Main.rand.Next( 10 ) == 0 )
			{
				int dust = Dust.NewDust( new Vector2( hitbox.X, hitbox.Y ), hitbox.Width, hitbox.Height, mod.DustType( "Sparkle" ) );
			}
		}*/
	}
}