using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrariaReworked.Items
{
	public class PalladiumJackhammer : ModItem
	{
		/*public override void SetStaticDefaults()
		{
			Tooltip.SetDefault( "Able to mine Impervious Brick and Caesium Ore" );
		}
		*/
		public override void SetDefaults()
		{
			this.item.damage = 18;
			this.item.melee = true;
			this.item.width = 46;
			this.item.height = 18;
			this.item.useTime = 9;
			this.item.noMelee = true;
			item.channel = true;
			item.noUseGraphic = true;
			item.shoot = mod.ProjectileType( "PalladiumJackhammer" );
			item.shootSpeed = 40;
			this.item.useAnimation = 24;
			this.item.hammer = 70;
			this.item.scale = 1.1f;
			this.item.useStyle = 5;
			this.item.knockBack = 5.2f;
			this.item.value = 54000;
			this.item.rare = 4;
			this.item.UseSound = SoundID.Item23;
			this.item.autoReuse = true;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe( this.mod );
			recipe.AddIngredient( ItemID.PalladiumBar, 12 );
			recipe.AddTile( TileID.Anvils );
			recipe.SetResult( this );
			recipe.AddRecipe();
		}
		/*
		public override void MeleeEffects( Player player, Rectangle hitbox )
		{
			if( Main.rand.Next( 3 ) == 0 )
			{
				//int dust = Dust.NewDust( new Vector2( hitbox.X, hitbox.Y ), hitbox.Width, hitbox.Height, 59, (player.velocity.X * 0.2f) + (player.direction * 3), player.velocity.Y * 0.2f, 0, new Color( 0, 0, 1 ), 1f );
				//Main.dust[dust].noGravity = true;
			}
		}*/
	}
}