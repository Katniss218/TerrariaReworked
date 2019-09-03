using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrariaReworked.Items
{
	public class OrichalcumJackhammer : ModItem
	{
		/*public override void SetStaticDefaults()
		{
			Tooltip.SetDefault( "Able to mine Impervious Brick and Caesium Ore" );
		}
		*/
		public override void SetDefaults()
		{
			this.item.damage = 22;
			this.item.melee = true;
			this.item.width = 46;
			this.item.height = 18;
			this.item.useTime = 7;
			this.item.noMelee = true;
			item.channel = true;
			item.noUseGraphic = true;
			item.shoot = mod.ProjectileType( "OrichalcumJackhammer" );
			item.shootSpeed = 40;
			this.item.useAnimation = 24;
			this.item.hammer = 75;
			this.item.scale = 1.1f;
			this.item.useStyle = 5;
			this.item.knockBack = 5.2f;
			this.item.value = 81000;
			this.item.rare = 4;
			this.item.UseSound = SoundID.Item23;
			this.item.autoReuse = true;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe( this.mod );
			recipe.AddIngredient( ItemID.OrichalcumBar, 12 );
			recipe.AddTile( TileID.MythrilAnvil );
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