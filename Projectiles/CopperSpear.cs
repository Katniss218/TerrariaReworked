using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace TerrariaReworked.Projectiles
{
	public class CopperSpear : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault( "Copper Spear" );
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
			/*Vector2 vector24 = Main.player[this.owner].RotatedRelativePoint( Main.player[this.owner].MountedCenter, true );
			this.direction = Main.player[this.owner].direction;
			Main.player[this.owner].heldProj = this.whoAmI;
			Main.player[this.owner].itemTime = Main.player[this.owner].itemAnimation;
			this.position.X = vector24.X - (float)(this.width / 2);
			this.position.Y = vector24.Y - (float)(this.height / 2);
			if( !Main.player[this.owner].frozen )
			{
				if( this.type == 46 )
				{
					if( this.ai[0] == 0f )
					{
						this.ai[0] = 3f;
						this.netUpdate = true;
					}
					if( Main.player[this.owner].itemAnimation < Main.player[this.owner].itemAnimationMax / 3 )
					{
						ref float ptr = ref this.ai[0];
						ptr -= 1.6f;
					}
					else
					{
						ref float ptr = ref this.ai[0];
						ptr += 1.4f;
					}
				}
				else if( this.type == 105 )
				{
					if( this.ai[0] == 0f )
					{
						this.ai[0] = 3f;
						this.netUpdate = true;
					}
					if( Main.player[this.owner].itemAnimation < Main.player[this.owner].itemAnimationMax / 3 )
					{
						ref float ptr = ref this.ai[0];
						ptr -= 2.4f;
					}
					else
					{
						ref float ptr = ref this.ai[0];
						ptr += 2.1f;
					}
				}
				else if( this.type == 367 )
				{
					this.spriteDirection = -this.direction;
					if( this.ai[0] == 0f )
					{
						this.ai[0] = 3f;
						this.netUpdate = true;
					}
					if( Main.player[this.owner].itemAnimation < Main.player[this.owner].itemAnimationMax / 3 )
					{
						ref float ptr = ref this.ai[0];
						ptr -= 1.6f;
					}
					else
					{
						ref float ptr = ref this.ai[0];
						ptr += 1.5f;
					}
				}
				else if( this.type == 368 )
				{
					this.spriteDirection = -this.direction;
					if( this.ai[0] == 0f )
					{
						this.ai[0] = 3f;
						this.netUpdate = true;
					}
					if( Main.player[this.owner].itemAnimation < Main.player[this.owner].itemAnimationMax / 3 )
					{
						ref float ptr = ref this.ai[0];
						ptr -= 1.5f;
					}
					else
					{
						ref float ptr = ref this.ai[0];
						ptr += 1.4f;
					}
				}
				else if( this.type == 222 )
				{
					if( this.ai[0] == 0f )
					{
						this.ai[0] = 3f;
						this.netUpdate = true;
					}
					if( Main.player[this.owner].itemAnimation < Main.player[this.owner].itemAnimationMax / 3 )
					{
						ref float ptr = ref this.ai[0];
						ptr -= 2.4f;
						if( this.localAI[0] == 0f && Main.myPlayer == this.owner )
						{
							this.localAI[0] = 1f;
							Projectile.NewProjectile( base.Center.X + this.velocity.X * this.ai[0], base.Center.Y + this.velocity.Y * this.ai[0], this.velocity.X, this.velocity.Y, 228, this.damage, this.knockBack, this.owner, 0f, 0f );
						}
					}
					else
					{
						ref float ptr = ref this.ai[0];
						ptr += 2.1f;
					}
				}
				else if( this.type == 342 )
				{
					if( this.ai[0] == 0f )
					{
						this.ai[0] = 3f;
						this.netUpdate = true;
					}
					if( Main.player[this.owner].itemAnimation < Main.player[this.owner].itemAnimationMax / 3 )
					{
						ref float ptr = ref this.ai[0];
						ptr -= 2.4f;
						if( this.localAI[0] == 0f && Main.myPlayer == this.owner )
						{
							this.localAI[0] = 1f;
							if( Collision.CanHit( Main.player[this.owner].position, Main.player[this.owner].width, Main.player[this.owner].height, new Vector2( base.Center.X + this.velocity.X * this.ai[0], base.Center.Y + this.velocity.Y * this.ai[0] ), this.width, this.height ) )
							{
								Projectile.NewProjectile( base.Center.X + this.velocity.X * this.ai[0], base.Center.Y + this.velocity.Y * this.ai[0], this.velocity.X * 2.4f, this.velocity.Y * 2.4f, 343, (int)((double)this.damage * 0.8), this.knockBack * 0.85f, this.owner, 0f, 0f );
							}
						}
					}
					else
					{
						ref float ptr = ref this.ai[0];
						ptr += 2.1f;
					}
				}
				else if( this.type == 47 )
				{
					if( this.ai[0] == 0f )
					{
						this.ai[0] = 4f;
						this.netUpdate = true;
					}
					if( Main.player[this.owner].itemAnimation < Main.player[this.owner].itemAnimationMax / 3 )
					{
						ref float ptr = ref this.ai[0];
						ptr -= 1.2f;
					}
					else
					{
						ref float ptr = ref this.ai[0];
						ptr += 0.9f;
					}
				}
				else if( this.type == 153 )
				{
					this.spriteDirection = -this.direction;
					if( this.ai[0] == 0f )
					{
						this.ai[0] = 4f;
						this.netUpdate = true;
					}
					if( Main.player[this.owner].itemAnimation < Main.player[this.owner].itemAnimationMax / 3 )
					{
						ref float ptr = ref this.ai[0];
						ptr -= 1.5f;
					}
					else
					{
						ref float ptr = ref this.ai[0];
						ptr += 1.3f;
					}
				}
				else if( this.type == 49 )
				{
					if( this.ai[0] == 0f )
					{
						this.ai[0] = 4f;
						this.netUpdate = true;
					}
					if( Main.player[this.owner].itemAnimation < Main.player[this.owner].itemAnimationMax / 3 )
					{
						ref float ptr = ref this.ai[0];
						ptr -= 1.1f;
					}
					else
					{
						ref float ptr = ref this.ai[0];
						ptr += 0.85f;
					}
				}
				else if( this.type == 64 || this.type == 215 )
				{
					this.spriteDirection = -this.direction;
					if( this.ai[0] == 0f )
					{
						this.ai[0] = 3f;
						this.netUpdate = true;
					}
					if( Main.player[this.owner].itemAnimation < Main.player[this.owner].itemAnimationMax / 3 )
					{
						ref float ptr = ref this.ai[0];
						ptr -= 1.9f;
					}
					else
					{
						ref float ptr = ref this.ai[0];
						ptr += 1.7f;
					}
				}
				else if( this.type == 66 || this.type == 97 || this.type == 212 || this.type == 218 )
				{
					this.spriteDirection = -this.direction;
					if( this.ai[0] == 0f )
					{
						this.ai[0] = 3f;
						this.netUpdate = true;
					}
					if( Main.player[this.owner].itemAnimation < Main.player[this.owner].itemAnimationMax / 3 )
					{
						ref float ptr = ref this.ai[0];
						ptr -= 2.1f;
					}
					else
					{
						ref float ptr = ref this.ai[0];
						ptr += 1.9f;
					}
				}
				else if( this.type == 130 )
				{
					this.spriteDirection = -this.direction;
					if( this.ai[0] == 0f )
					{
						this.ai[0] = 3f;
						this.netUpdate = true;
					}
					if( Main.player[this.owner].itemAnimation < Main.player[this.owner].itemAnimationMax / 3 )
					{
						ref float ptr = ref this.ai[0];
						ptr -= 1.3f;
					}
					else
					{
						ref float ptr = ref this.ai[0];
						ptr += 1f;
					}
				}
			}
			this.position += this.velocity * this.ai[0];
			if( this.type == 130 )
			{
				if( this.ai[1] == 0f || this.ai[1] == 4f || this.ai[1] == 8f || this.ai[1] == 12f || this.ai[1] == 16f || this.ai[1] == 20f || this.ai[1] == 24f )
				{
					Projectile.NewProjectile( this.position.X + (float)(this.width / 2), this.position.Y + (float)(this.height / 2), this.velocity.X, this.velocity.Y, 131, this.damage / 3, 0f, this.owner, 0f, 0f );
				}
				ref float ptr = ref this.ai[1];
				ptr += 1f;
			}
			if( Main.player[this.owner].itemAnimation == 0 )
			{
				this.Kill();
			}
			this.rotation = (float)Math.Atan2( (double)this.velocity.Y, (double)this.velocity.X ) + 2.355f;
			if( this.spriteDirection == -1 )
			{
				this.rotation -= 1.57f;
			}
			if( this.type == 46 )
			{
				if( Main.rand.Next( 5 ) == 0 )
				{
					Dust.NewDust( this.position, this.width, this.height, 14, 0f, 0f, 150, default( Color ), 1.4f );
				}
				int num265 = Dust.NewDust( this.position, this.width, this.height, 27, this.velocity.X * 0.2f + (float)(this.direction * 3), this.velocity.Y * 0.2f, 100, default( Color ), 1.2f );
				Main.dust[num265].noGravity = true;
				ref float ptr = ref Main.dust[num265].velocity.X;
				ptr /= 2f;
				ptr = ref Main.dust[num265].velocity.Y;
				ptr /= 2f;
				num265 = Dust.NewDust( this.position - this.velocity * 2f, this.width, this.height, 27, 0f, 0f, 150, default( Color ), 1.4f );
				ptr = ref Main.dust[num265].velocity.X;
				ptr /= 5f;
				ptr = ref Main.dust[num265].velocity.Y;
				ptr /= 5f;
				return;
			}
			if( this.type == 105 )
			{
				if( Main.rand.Next( 3 ) == 0 )
				{
					int num266 = Dust.NewDust( new Vector2( this.position.X, this.position.Y ), this.width, this.height, 57, this.velocity.X * 0.2f, this.velocity.Y * 0.2f, 200, default( Color ), 1.2f );
					Dust dust = Main.dust[num266];
					dust.velocity += this.velocity * 0.3f;
					dust = Main.dust[num266];
					dust.velocity *= 0.2f;
				}
				if( Main.rand.Next( 4 ) == 0 )
				{
					int num267 = Dust.NewDust( new Vector2( this.position.X, this.position.Y ), this.width, this.height, 43, 0f, 0f, 254, default( Color ), 0.3f );
					Dust dust = Main.dust[num267];
					dust.velocity += this.velocity * 0.5f;
					dust = Main.dust[num267];
					dust.velocity *= 0.5f;
					return;
				}
			}
			else if( this.type == 153 )
			{
				int num268 = Dust.NewDust( this.position - this.velocity * 3f, this.width, this.height, 115, this.velocity.X * 0.4f, this.velocity.Y * 0.4f, 140, default( Color ), 1f );
				Main.dust[num268].noGravity = true;
				Main.dust[num268].fadeIn = 1.25f;
				Dust dust = Main.dust[num268];
				dust.velocity *= 0.25f;
				return;
			}
		}*/
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
					velocity -= 1.1f;
				}
				else // Otherwise, increase the movement factor
				{
					velocity += 0.9f;
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