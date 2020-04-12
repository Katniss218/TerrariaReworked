using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrariaReworked.Projectiles
{
	public class TerraSpear2 : ModProjectile
	{
		public override void SetDefaults()
		{
			projectile.width = 16;
			projectile.height = 16;
			projectile.friendly = true;
			projectile.penetrate = 3;
			projectile.tileCollide = true;
			projectile.aiStyle = 27;
			projectile.light = 0.5f;
			projectile.melee = true;
		}

		/*private void DoGravity()
		{
			projectile.ai[0] += 1f; // Use a timer to wait 15 ticks before applying gravity.
			if( projectile.ai[0] >= 15f )
			{
				projectile.ai[0] = 15f;
				projectile.velocity.Y = projectile.velocity.Y + 0.1f;
			}
			if( projectile.velocity.Y > 16f )
			{
				projectile.velocity.Y = 16f;
			}
		}*/

		public override bool PreAI()
		{
			//DoGravity();

			if( this.projectile.localAI[1] > 7f /*&& this.type == 132*/ )
			{
				int num315 = Dust.NewDust( new Vector2( this.projectile.position.X - this.projectile.velocity.X * 4f + 2f, this.projectile.position.Y + 2f - this.projectile.velocity.Y * 4f ), 8, 8, 107, this.projectile.oldVelocity.X, this.projectile.oldVelocity.Y, 100, default( Color ), 1.25f );
				Dust dust = Main.dust[num315];
				dust.velocity *= -0.25f;
				num315 = Dust.NewDust( new Vector2( this.projectile.position.X - this.projectile.velocity.X * 4f + 2f, this.projectile.position.Y + 2f - this.projectile.velocity.Y * 4f ), 8, 8, 107, this.projectile.oldVelocity.X, this.projectile.oldVelocity.Y, 100, default( Color ), 1.25f );
				dust = Main.dust[num315];
				dust.velocity *= -0.25f;
				dust = Main.dust[num315];
				dust.position -= this.projectile.velocity * 0.5f;
			}

			if( this.projectile.localAI[1] < 15f )
			{
				ref float ptr = ref this.projectile.localAI[1];
				ptr += 1f;
			}
			else
			{
				if( this.projectile.localAI[0] == 0f )
				{
					this.projectile.scale -= 0.02f;
					this.projectile.alpha += 30;
					if( this.projectile.alpha >= 250 )
					{
						this.projectile.alpha = 255;
						this.projectile.localAI[0] = 1f;
					}
				}
				else if( this.projectile.localAI[0] == 1f )
				{
					this.projectile.scale += 0.02f;
					this.projectile.alpha -= 30;
					if( this.projectile.alpha <= 0 )
					{
						this.projectile.alpha = 0;
						this.projectile.localAI[0] = 0f;
					}
				}
			}
			if( this.projectile.ai[1] == 0f )
			{
				this.projectile.ai[1] = 1f;

				Main.PlaySound( SoundID.Item60, this.projectile.position );

			}

			this.projectile.rotation = (float)Math.Atan2( (double)this.projectile.velocity.Y, (double)this.projectile.velocity.X ) + 0.785f;

			if( this.projectile.velocity.Y > 16f )
			{
				this.projectile.velocity.Y = 16f;
			}

			return false;
		}

		public override Color? GetAlpha( Color lightColor )
		{
			if( this.projectile.localAI[1] >= 15f )
			{
				return new Color( 255, 255, 255, this.projectile.alpha );
			}
			if( this.projectile.localAI[1] < 5f )
			{
				return Color.Transparent;
			}
			int num7 = (int)((this.projectile.localAI[1] - 5f) / 10f * 255f);
			return new Color( num7, num7, num7, num7 );
		}

		public override void Kill( int timeLeft )
		{
			Main.PlaySound( SoundID.Item10, this.projectile.position );
			for( int i = 4; i < 31; i++ )
			{
				float x = this.projectile.oldVelocity.X * (30f / (float)i);
				float y = this.projectile.oldVelocity.Y * (30f / (float)i);
				int dustIndex = Dust.NewDust( new Vector2( this.projectile.oldPosition.X - x, this.projectile.oldPosition.Y - y ), 8, 8, 107, this.projectile.oldVelocity.X, this.projectile.oldVelocity.Y, 100, default( Color ), 1.8f );
				Main.dust[dustIndex].noGravity = true;
				Dust dust = Main.dust[dustIndex];
				dust.velocity *= 0.5f;
				dustIndex = Dust.NewDust( new Vector2( this.projectile.oldPosition.X - x, this.projectile.oldPosition.Y - y ), 8, 8, 107, this.projectile.oldVelocity.X, this.projectile.oldVelocity.Y, 100, default( Color ), 1.4f );
				dust = Main.dust[dustIndex];
				dust.velocity *= 0.05f;
			}
		}
	}
}