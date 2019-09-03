using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrariaReworked.Items
{
	public class ClockworkMegashark : ModItem
	{
		private int barrel;

		public override void SetStaticDefaults()
		{
			this.DisplayName.SetDefault( "Clockwork Megashark" );
			this.Tooltip.SetDefault( "Shoots 2 bullets with one shot\n'Let's just call it \"Clash of the Titans\"'" );
		}

		public override void SetDefaults()
		{
			this.item.damage = 25;
			this.item.ranged = true;
			this.item.width = 80;
			this.item.height = 50;
			this.item.useTime = 5;
			this.item.useAnimation = 5;
			this.item.useStyle = 5;
			this.item.noMelee = true; //so the item's animation doesn't do damage
			this.item.knockBack = 1;
			this.item.value = 10000;
			this.item.rare = 6;
			this.item.UseSound = SoundID.Item31;
			this.item.autoReuse = true;
			this.item.shoot = 10; //idk why but all the guns in the vanilla source have this
			this.item.shootSpeed = 10f;
			this.item.useAmmo = AmmoID.Bullet;
			this.item.scale = 0.85f;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe( mod );
			recipe.AddIngredient( ItemID.Megashark, 2 );
			recipe.AddIngredient( null, "IllegalWeaponInstructions", 1 );
			recipe.AddIngredient( ItemID.IllegalGunParts, 2 );
			recipe.AddTile( mod.TileType("AdamantiteAnvil") );
			recipe.SetResult( this );
			recipe.AddRecipe();
		}

		public override bool Shoot( Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack )
		{
			Vector2 normal = new Vector2( -speedY, speedX );
			normal.Normalize();

			const int spread = 6;
			const int barrelCount = 2;
			const int barrelSpace = 20;
			const int shootSound = 36;
			const int shotCount = 2;

			// Barrel Offset
			if( barrelCount > 1 )
			{
				normal *= barrelSpace;
				position -= normal * ((float)(barrelCount - 1) / 2f);
				position += normal * barrel;
				barrel++;
				if( barrel >= barrelCount )
				{
					barrel = 0;
				}
			}
			for( int i = 0; i < shotCount; i++ )
			{
				// Spread
				float velx = speedX + (normal.X * (0.005f * Main.rand.Next( -spread, spread )));
				float vely = speedY + (normal.Y * (0.005f * Main.rand.Next( -spread, spread )));

				// Fire Gun
				Projectile.NewProjectile( position.X, position.Y, velx, vely, type, damage, knockBack, player.whoAmI );
			}
			Main.PlaySound( shootSound, player.position );

			return false;
		}
		
		public override Vector2? HoldoutOffset()
		{
			return new Vector2( -14, 0 );
		}
	}
}