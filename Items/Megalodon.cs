using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrariaReworked.Items
{
	public class Megalodon : ModItem
	{
		public override void SetStaticDefaults()
		{
			this.DisplayName.SetDefault( "Megalodon" );
			this.Tooltip.SetDefault( "50% chance to not consume ammo\n'Megashark's extinct cousin!'" );
		}

		public override void SetDefaults()
		{
			this.item.damage = 37;
			this.item.ranged = true;
			this.item.width = 80;
			this.item.height = 28;
			this.item.useTime = 5;
			this.item.useAnimation = 5;
			this.item.useStyle = 5;
			this.item.noMelee = true; //so the item's animation doesn't do damage
			this.item.knockBack = 1;
			this.item.value = 10000;
			this.item.rare = 8;
			this.item.UseSound = SoundID.Item11;
			this.item.autoReuse = true;
			this.item.shoot = 10; //idk why but all the guns in the vanilla source have this
			this.item.shootSpeed = 10f;
			this.item.useAmmo = AmmoID.Bullet;
		}
		/*
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe( mod );
			recipe.AddIngredient( null, "ExampleItem", 10 );
			recipe.AddTile( null, "ExampleWorkbench" );
			recipe.SetResult( this );
			recipe.AddRecipe();
		}
		*/
		public override bool ConsumeAmmo(Player player)
		{
			return Main.rand.NextFloat() >= 0.50f;
		}
		public override bool Shoot( Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack )
		{
			//Main.NewText( "SHM: " + MyWorld.superhardmode );
			//Main.NewText( "Obl: " + MyWorld.downedOblivion );
			//Vector2 normal = new Vector2( -speedY, speedX );
			//normal.Normalize();

			const int spread = 6;
			//const int barrelCount = 2;
			//const int barrelSpace = 20;
			//const int shootSound = 36;
			//const int shotCount = 2;

			// Barrel Offset
			/*if( barrelCount > 1 )
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
			{*/
				// Spread
				float velx = speedX + (0.05f * Main.rand.Next( -spread, spread ));
				float vely = speedY + (0.05f * Main.rand.Next( -spread, spread ));

				// Fire Gun
				Projectile.NewProjectile( position.X, position.Y, velx, vely, type, damage, knockBack, player.whoAmI );
			//}
			//Main.PlaySound( shootSound, player.position );

			return false;
		}
		
		public override Vector2? HoldoutOffset()
		{
			return new Vector2( -6, -2 );
		}
	}
}