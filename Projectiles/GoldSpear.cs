using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace TerrariaReworked.Projectiles
{
	public class GoldSpear : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault( "Gold Spear" );
		}

		public override void SetDefaults()
		{
			this.projectile.width = 18;
			this.projectile.height = 18;
			this.projectile.aiStyle = 19;
			this.projectile.penetrate = -1;
			this.projectile.scale = 1.3f;
			this.projectile.alpha = 0;

			this.projectile.hide = true;
			this.projectile.ownerHitCheck = true;
			this.projectile.melee = true;
			this.projectile.tileCollide = false;
			this.projectile.friendly = true;
		}
		
		public float velocity // Change this value to alter how fast the spear moves
		{
			get { return this.projectile.ai[0]; }
			set { this.projectile.ai[0] = value; }
		}

		// It appears that for this AI, only the ai0 field is used!
		public override void AI()
		{
			// cache
			Player projectileOwner = Main.player[this.projectile.owner];
			Vector2 center = projectileOwner.RotatedRelativePoint( projectileOwner.MountedCenter, true );

			// Here we set some of the projectile's owner properties, such as held item and itemtime, along with projectile direction and position based on the player

			this.projectile.direction = projectileOwner.direction;
			projectileOwner.heldProj = this.projectile.whoAmI;
			projectileOwner.itemTime = projectileOwner.itemAnimation;
			this.projectile.position.X = center.X - (float)(this.projectile.width / 2);
			this.projectile.position.Y = center.Y - (float)(this.projectile.height / 2);

			// frozen debuff blocks spear movement
			if( !projectileOwner.frozen )
			{
				if( velocity == 0f ) // When initially thrown out, the ai0 will be 0f
				{
					velocity = 3f; // Make sure the spear moves forward when initially thrown out
					this.projectile.netUpdate = true; // Make sure to netUpdate this spear
				}
				if( projectileOwner.itemAnimation < projectileOwner.itemAnimationMax / 3 ) // Somewhere along the item animation, make sure the spear moves back
				{
					velocity -= 1.25f;
				}
				else // Otherwise, increase the movement factor
				{
					velocity += 1.05f;
				}
			}
			this.projectile.position += this.projectile.velocity * velocity;

			// When we reach the end of the animation, we can kill the spear projectile
			if( projectileOwner.itemAnimation == 0 )
			{
				this.projectile.Kill();
			}

			// Apply proper rotation, with an offset of 135 degrees due to the sprite's rotation
			this.projectile.rotation = this.projectile.velocity.ToRotation() + MathHelper.ToRadians( 135f );

			if( Main.player[projectile.owner].itemAnimation == 0 )
			{
				projectile.Kill();
			}
			projectile.rotation = (float)Math.Atan2( (double)projectile.velocity.Y, (double)projectile.velocity.X ) + 2.355f;
			if( projectile.spriteDirection == -1 )
			{
				projectile.rotation -= 1.57f;
			}

			/*
			// These dusts are added later, for the 'ExampleMod' effect
			if (Main.rand.Next(3) == 0)
			{
				Dust dust = Dust.NewDustDirect(projectile.position, projectile.height, projectile.width, mod.DustType<Dusts.Sparkle>(),
					projectile.velocity.X * .2f, projectile.velocity.Y * .2f, 200, Scale: 1.2f);
				dust.velocity += projectile.velocity * 0.3f;
				dust.velocity *= 0.2f;
			}
			if (Main.rand.Next(4) == 0)
			{
				Dust dust = Dust.NewDustDirect(projectile.position, projectile.height, projectile.width, mod.DustType<Dusts.Sparkle>(),
					0, 0, 254, Scale: 0.3f);
				dust.velocity += projectile.velocity * 0.5f;
				dust.velocity *= 0.5f;
			}*/
		}
	}
}