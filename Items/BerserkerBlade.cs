using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrariaReworked.Items
{
	public class BerserkerBlade : ModItem
	{
		public override void SetStaticDefaults()
		{
			this.Tooltip.SetDefault("'Go berserk!'");
		}

		public override void SetDefaults()
		{
			this.item.damage = 132;           //The damage of your weapon
			this.item.melee = true;          //Is your weapon a melee weapon?
			this.item.width = 60;            //Weapon's texture's width
			this.item.height = 74;           //Weapon's texture's height
			this.item.useTime = 15;          //The time span of using the weapon. Remember in terraria, 60 frames is a second.
			this.item.useAnimation = 15;         //The time span of the using animation of the weapon, suggest set it the same as useTime.
			this.item.useStyle = 1;          //The use style of weapon, 1 for swinging, 2 for drinking, 3 act like shortsword, 4 for use like life crystal, 5 for use staffs or guns
			this.item.knockBack = 6.5f;         //The force of knockback of the weapon. Maximum is 20
			this.item.value = 250;           //The value of the weapon
			this.item.rare = 0;              //The rarity of the weapon, from -1 to 13
			this.item.UseSound = SoundID.Item1;      //The sound when the weapon is using
			this.item.scale = 1.2f;
			this.item.autoReuse = true;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe( this.mod );
			recipe.AddIngredient( null, "BerserkerBar", 20 );
			recipe.AddTile( mod.TileType( "BerserkerAnvil" ) );
			recipe.SetResult( this );
			recipe.AddRecipe();
		}

		public override bool UseItem( Player player )
		{
			float targetX = Main.screenPosition.X + Main.mouseX;
			if( targetX < player.position.X )
				player.direction = -1;
			else
				player.direction = 1;
			return true;
		}

		public override void MeleeEffects( Player player, Rectangle hitbox )
		{
			int dust = Dust.NewDust( new Vector2( hitbox.X, hitbox.Y ), hitbox.Width, hitbox.Height, mod.DustType( "BerserkerDust" ), Main.rand.NextFloat( -0.1f, 0.1f ), Main.rand.NextFloat( -0.1f, 0.1f ) );

			Vector2 target = Main.screenPosition + new Vector2( (float)Main.mouseX, (float)Main.mouseY );

			Vector2 randomInside = new Vector2( hitbox.X + Main.rand.Next( 0, hitbox.Width ), hitbox.Y + Main.rand.Next( 0, hitbox.Height ) );
			Vector2 vel = target - player.position;
			vel.Normalize();
			vel *= 10; // speed
			const int spread = 75;
			vel.X += (0.05f * Main.rand.Next( -spread, spread ));
			vel.Y += (0.05f * Main.rand.Next( -spread, spread ));

			Projectile.NewProjectile( randomInside, vel, mod.ProjectileType( "BerserkerDust" ), TerrariaReworked.BerserkerDustDamage, 3, player.whoAmI );

		}

		public override void OnHitNPC( Player player, NPC target, int damage, float knockback, bool crit )
		{
			// Add Onfire buff to the NPC for 1 second
			// 60 frames = 1 second
			//target.AddBuff(BuffID.OnFire, 60);
		}
	}
}