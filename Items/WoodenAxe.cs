using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrariaReworked.Items
{
	public class WoodenAxe : ModItem
	{
		/*public override void SetStaticDefaults()
		{
			Tooltip.SetDefault( "This is a modded pickaxe." );
		}
		*/
		public override void SetDefaults()
		{
			this.item.damage = 3;
			this.item.melee = true;
			this.item.width = 32;
			this.item.height = 28;
			this.item.useTime = 21;
			this.item.useAnimation = 26;
			this.item.axe = 5;
			this.item.tileBoost = -1;
			this.item.useStyle = 1;
			this.item.knockBack = 4.5f;
			this.item.value = 50;
			this.item.rare = 0;
			this.item.UseSound = SoundID.Item1;
			this.item.autoReuse = true;
			this.item.useTurn = true;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe( this.mod );
			recipe.AddIngredient( ItemID.Wood, 12 );
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