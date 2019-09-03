using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrariaReworked.Items
{
	public class TropicPickaxe : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault( "Able to mine Hellstone" );
		}
		
		public override void SetDefaults()
		{
			this.item.damage = 10;
			this.item.melee = true;
			this.item.width = 32;
			this.item.height = 32;
			this.item.useTime = 14;
			this.item.useAnimation = 19;
			this.item.pick = 65;
			this.item.scale = 1.2f;
			this.item.useStyle = 1;
			this.item.knockBack = 3;
			this.item.value = 18000;
			this.item.rare = 3;
			this.item.UseSound = SoundID.Item1;
			this.item.autoReuse = true;
			this.item.useTurn = true;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe( this.mod );
			recipe.AddRecipeGroup( "TerrariaReworked:SilverPickaxe", 1 );
			recipe.AddIngredient( ItemID.JungleSpores, 12 );
			recipe.AddIngredient( ItemID.Stinger, 6 );
			recipe.AddIngredient( ItemID.Vine, 2 );
			recipe.AddTile( TileID.Anvils );
			recipe.SetResult( this );
			recipe.AddRecipe();
		}
		
		public override void MeleeEffects( Player player, Rectangle hitbox )
		{
			if( Main.rand.Next( 3 ) == 0 )
			{
				int dust = Dust.NewDust( new Vector2( hitbox.X, hitbox.Y ), hitbox.Width, hitbox.Height, 40, 3.0f * player.direction, 0f, 0, default( Color ), 1.25f );
				Main.dust[dust].noGravity = true;
			}
		}
		public override void ModifyHitNPC( Player player, NPC target, ref int damage, ref float knockBack, ref bool crit )
		{
			if( Main.rand.Next( 4 ) == 0 )
			{
				// Add the buff 
				target.AddBuff( BuffID.Poisoned, 420 );
			}
		}
	}
}