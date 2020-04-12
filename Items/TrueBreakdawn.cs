using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrariaReworked.Items
{
	public class TrueBreakdawn : ModItem
	{
		/*public override void SetStaticDefaults()
		{
			Tooltip.SetDefault( "This is a modded pickaxe." );
		}
		*/
		public override void SetDefaults()
		{
			this.item.damage = 50;
			this.item.melee = true;
			this.item.width = 32;
			this.item.height = 28;
			this.item.useTime = 13;
			this.item.useAnimation = 30;
			this.item.axe = 32;
			this.item.hammer = 70;
			this.item.scale = 1.2f;
			this.item.useStyle = 1;
			this.item.knockBack = 7;
			this.item.value = 30000;
			this.item.rare = 3;
			this.item.UseSound = SoundID.Item1;
			this.item.autoReuse = true;
			this.item.useTurn = true;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe( this.mod );
			recipe.AddIngredient( mod.ItemType( "Breakdawn" ), 1 );
			recipe.AddIngredient( mod.ItemType( "BrokenHeroAx" ), 1 );
			recipe.AddTile( TileID.MythrilAnvil );
			recipe.SetResult( this );
			recipe.AddRecipe();
		}

		public override void MeleeEffects( Player player, Rectangle hitbox )
		{
			if( Main.rand.Next( 3 ) == 0 )
			{
				int dust = Dust.NewDust( new Vector2( hitbox.X, hitbox.Y ), hitbox.Width, hitbox.Height, 59, (player.velocity.X * 0.2f) + (player.direction * 3), player.velocity.Y * 0.2f, 0, new Color( 0, 0, 1 ), 1f );
				Main.dust[dust].noGravity = true;
			}
		}
	}
}