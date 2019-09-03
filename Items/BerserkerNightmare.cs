using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrariaReworked.Items
{
	public class BerserkerNightmare : ModItem
	{
		/*public override void SetStaticDefaults()
		{
			this.Tooltip.SetDefault("This is a modded sword.");  //The (English) text shown below your weapon's name
		}*/

		public override void SetDefaults()
		{
			this.item.damage = 165;           //The damage of your weapon
			this.item.melee = true;          //Is your weapon a melee weapon?
			this.item.width = 34;            //Weapon's texture's width
			this.item.height = 32;           //Weapon's texture's height
			this.item.useTime = 44;          //The time span of using the weapon. Remember in terraria, 60 frames is a second.
			this.item.useAnimation = 44;         //The time span of the using animation of the weapon, suggest set it the same as useTime.
			this.item.useStyle = 5;          //The use style of weapon, 1 for swinging, 2 for drinking, 3 act like shortsword, 4 for use like life crystal, 5 for use staffs or guns
			this.item.knockBack = 9f;         //The force of knockback of the weapon. Maximum is 20
			this.item.value = 250;           //The value of the weapon
			this.item.rare = 0;              //The rarity of the weapon, from -1 to 13
			this.item.UseSound = SoundID.Item1;      //The sound when the weapon is using
			this.item.scale = 1.2f;
			this.item.autoReuse = true;
			this.item.shoot = mod.ProjectileType( "BerserkerNightmare" );
			this.item.shootSpeed = 18;
			this.item.noUseGraphic = true;
			this.item.noMelee = true;
			this.item.channel = true;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe( this.mod );
			recipe.AddIngredient( null, "BerserkerBar", 15 );
			recipe.AddTile( mod.TileType( "BerserkerAnvil" ) );
			recipe.SetResult( this );
			recipe.AddRecipe();
		}

		public override bool Shoot( Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack )
		{
			float vX = speedX; // +(float)Main.rand.Next(-spread,spread+1) * spreadMult;
			float vY = speedY; // +(float)Main.rand.Next(-spread,spread+1) * spreadMult;

			Projectile.NewProjectile( position, new Vector2 ( vX, vY).RotatedBy( MathHelper.ToRadians( Main.rand.Next( 15, 30 ) ) ), type, damage, knockBack, player.whoAmI );
			Projectile.NewProjectile( position, new Vector2( vX, vY ).RotatedBy( MathHelper.ToRadians( Main.rand.Next( -10, 10 ) ) ), type, damage, knockBack, player.whoAmI );
			Projectile.NewProjectile( position, new Vector2( vX, vY ).RotatedBy( MathHelper.ToRadians( Main.rand.Next( -30, -15 ) ) ), type, damage, knockBack, player.whoAmI );

			return false;
		}

		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, mod.DustType("BerserkerDust"), Main.rand.NextFloat( -0.1f, 0.1f ), Main.rand.NextFloat( -0.1f, 0.1f ) );
			
			Vector2 target = Main.screenPosition + new Vector2( (float)Main.mouseX, (float)Main.mouseY );

			Vector2 randomInside = new Vector2( hitbox.X + Main.rand.Next( 0, hitbox.Width ), hitbox.Y + Main.rand.Next( 0, hitbox.Height ) );
			Vector2 vel = target - player.position;
			const int spread = 150;
			vel.X += (0.05f * Main.rand.Next( -spread, spread ));
			vel.Y += (0.05f * Main.rand.Next( -spread, spread ));

			Projectile.NewProjectile( randomInside, vel, mod.ProjectileType( "BerserkerDust" ), (int)(this.item.damage * 0.70f), 3, player.whoAmI );
			
		}

		public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
		{
			// Add Onfire buff to the NPC for 1 second
			// 60 frames = 1 second
			target.AddBuff(BuffID.OnFire, 60);
		}
		
		// Star Wrath/Starfury style weapon. Spawn projectiles from sky that aim towards mouse.
		// See Source code for Star Wrath projectile to see how it passes through tiles.
		/*	The following changes to SetDefaults 
		 	item.shoot = 503;
			item.shootSpeed = 8f;
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			Vector2 target = Main.screenPosition + new Vector2((float)Main.mouseX, (float)Main.mouseY);
			float ceilingLimit = target.Y;
			if (ceilingLimit > player.Center.Y - 200f)
			{
				ceilingLimit = player.Center.Y - 200f;
			}
			for (int i = 0; i < 3; i++)
			{
				position = player.Center + new Vector2((-(float)Main.rand.Next(0, 401) * player.direction), -600f);
				position.Y -= (100 * i);
				Vector2 heading = target - position;
				if (heading.Y < 0f)
				{
					heading.Y *= -1f;
				}
				if (heading.Y < 20f)
				{
					heading.Y = 20f;
				}
				heading.Normalize();
				heading *= new Vector2(speedX, speedY).Length();
				speedX = heading.X;
				speedY = heading.Y + Main.rand.Next(-40, 41) * 0.02f;
				Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, damage * 2, knockBack, player.whoAmI, 0f, ceilingLimit);
			}
			return false;
		}*/
	}
}