using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrariaReworked.Projectiles
{
	// this gets executed not only for coal, byt for all items
	public class VanillaProjectileChanger : GlobalProjectile
	{
		public override bool PreAI( Projectile projectile )
		{
			if( projectile.aiStyle == 8 )
			{
				if( projectile.type == 258 && projectile.localAI[0] == 0f )
				{
					projectile.localAI[0] = 1f;
					Main.PlaySound( SoundID.Item20, projectile.position );
				}
				if( projectile.type == 96 && projectile.localAI[0] == 0f )
				{
					projectile.localAI[0] = 1f;
					Main.PlaySound( SoundID.Item20, projectile.position );
				}
				if( projectile.type == ProjectileID.WaterBolt )
				{
					for( int i = 0; i < 7; i++ )
					{
						float x = projectile.velocity.X / 3f * i;
						float y = projectile.velocity.Y / 3f * i;
						int padding = 3;
						int dustIndex = Dust.NewDust( new Vector2( projectile.position.X + padding, projectile.position.Y + padding ), projectile.width - padding * 2, projectile.height - padding * 2, 172, 0f, 0f, 100, default( Color ), 2f );
						Main.dust[dustIndex].noGravity = true;
						Dust dust = Main.dust[dustIndex];
						dust.velocity *= 0.1f;
						dust = Main.dust[dustIndex];
						dust.velocity += projectile.velocity * 0.1f;
						ref float ptr = ref Main.dust[dustIndex].position.X;
						ptr -= x;
						ptr = ref Main.dust[dustIndex].position.Y;
						ptr -= y;
					}
					if( Main.rand.Next( 5 ) == 0 )
					{
						int padding = 3;
						int dustIndex = Dust.NewDust( new Vector2( projectile.position.X + padding, projectile.position.Y + padding ), projectile.width - padding * 2, projectile.height - padding * 2, 172, 0f, 0f, 100, default( Color ), 1.2f );
						Dust dust = Main.dust[dustIndex];
						dust.velocity *= 0.25f;
						dust = Main.dust[dustIndex];
						dust.velocity += projectile.velocity * 0.5f;
					}
				}
				else if( projectile.type == 502 )
				{
					float r = Main.DiscoR / 255f;
					float g = Main.DiscoG / 255f;
					float b = Main.DiscoB / 255f;
					r = (0.5f + r) / 2f;
					g = (0.5f + g) / 2f;
					b = (0.5f + b) / 2f;
					Lighting.AddLight( projectile.Center, r, g, b );
				}
				else if( projectile.type == 95 || projectile.type == 96 )
				{
					int dustIndex = Dust.NewDust( new Vector2( projectile.position.X + projectile.velocity.X, projectile.position.Y + projectile.velocity.Y ), projectile.width, projectile.height, 75, projectile.velocity.X, projectile.velocity.Y, 100, default( Color ), 3f * projectile.scale );
					Main.dust[dustIndex].noGravity = true;
				}
				else if( projectile.type == 253 )
				{
					for( int i = 0; i < 2; i++ )
					{
						int dustIndex = Dust.NewDust( new Vector2( projectile.position.X, projectile.position.Y ), projectile.width, projectile.height, 135, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 100, default( Color ), 2f );
						Main.dust[dustIndex].noGravity = true;
						ref float ptr = ref Main.dust[dustIndex].velocity.X;
						ptr *= 0.3f;
						ptr = ref Main.dust[dustIndex].velocity.Y;
						ptr *= 0.3f;
					}
				}
				else
				{
					for( int i = 0; i < 2; i++ )
					{
						int dustIndex = Dust.NewDust( new Vector2( projectile.position.X, projectile.position.Y ), projectile.width, projectile.height, 6, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 100, default( Color ), 2f );
						Main.dust[dustIndex].noGravity = true;
						ref float ptr = ref Main.dust[dustIndex].velocity.X;
						ptr *= 0.3f;
						ptr = ref Main.dust[dustIndex].velocity.Y;
						ptr *= 0.3f;
					}
				}
				if( projectile.type != 27 && projectile.type != 96 && projectile.type != 258 )
				{
					ref float ptr = ref projectile.ai[1];
					ptr += 1f;
				}
				if( projectile.ai[1] >= 20f )
				{
					ref float ptr = ref projectile.velocity.Y;
					ptr += 0.2f;
				}
				if( projectile.type == 502 )
				{
					projectile.rotation = projectile.velocity.ToRotation() + 1.57079637f;
					if( projectile.velocity.X != 0f )
					{
						projectile.spriteDirection = (projectile.direction = Math.Sign( projectile.velocity.X ));
					}
				}
				else
				{
					projectile.rotation += 0.3f * projectile.direction;
				}
				if( projectile.velocity.Y > 16f )
				{
					projectile.velocity.Y = 16f;
				}
				return false;
			}

			return base.PreAI( projectile );
		}
	}
}