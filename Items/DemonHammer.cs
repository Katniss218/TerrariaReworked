using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrariaReworked.Items
{
	public class DemonHammer : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault( "Strong enough to destroy Hallowed Altars" );
		}
		
		public override void SetDefaults()
		{
			this.item.damage = 45;
			this.item.melee = true;
			this.item.width = 60;
			this.item.height = 60;
			this.item.useTime = 12;
			this.item.useAnimation = 32;
			this.item.hammer = 140;
			this.item.tileBoost = 0;
			this.item.useStyle = 1;
			this.item.knockBack = 9f;
			this.item.scale = 1.1f;
			this.item.value = 124000;
			this.item.rare = 8;
			this.item.UseSound = SoundID.Item1;
			this.item.autoReuse = true;
			this.item.useTurn = true;
		}
		
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe( this.mod );
			recipe.AddIngredient( ItemID.Pwnhammer, 1 );
			recipe.AddIngredient( mod.ItemType( "SoulofBlight" ), 5 );
			recipe.AddIngredient( ItemID.EbonstoneBlock, 100 );
			recipe.AddTile( TileID.DemonAltar );
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