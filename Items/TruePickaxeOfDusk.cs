using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrariaReworked.Items
{
	public class TruePickaxeofDusk : ModItem
	{
		public override void SetStaticDefaults()
		{
			this.DisplayName.SetDefault( "Pickaxe of Dusk" );
		}

		public override void SetDefaults()
		{
			this.item.damage = 35;
			this.item.melee = true;
			this.item.width = 32;
			this.item.height = 32;
			this.item.useTime = 14;
			this.item.useAnimation = 22;
			this.item.pick = 100;
			this.item.scale = 1.1f;
			this.item.useStyle = 1;
			this.item.knockBack = 3;
			this.item.value = 54000;
			this.item.rare = 3;
			this.item.UseSound = SoundID.Item1;
			this.item.autoReuse = true;
			this.item.useTurn = true;
		}

		public override void MeleeEffects( Player player, Rectangle hitbox )
		{
			if( Main.rand.Next( 3 ) == 0 )
			{
				int dust = Dust.NewDust( new Vector2( hitbox.X, hitbox.Y ), hitbox.Width, hitbox.Height, 59, (player.velocity.X * 0.2f) + (player.direction * 3), player.velocity.Y * 0.2f, 0, new Color( 0, 0, 1 ), 1f );
				Main.dust[dust].noGravity = true;
			}
		}
		
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe( this.mod );
			recipe.AddIngredient( mod.ItemType( "PickaxeofDusk" ), 1 );
			recipe.AddIngredient( mod.ItemType( "BrokenHeroPick" ), 1 );
			recipe.AddTile( TileID.MythrilAnvil );
			recipe.SetResult( this );
			recipe.AddRecipe();
		}
	}
}