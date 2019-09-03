using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrariaReworked.Projectiles
{
	public class OblivionFlame : ModProjectile
	{
		public override void SetDefaults()
		{
			projectile.aiStyle = -1; // replicates ai 8
			projectile.width = 20;
			projectile.height = 20;
			projectile.hostile = true;
			projectile.magic = true;
			projectile.penetrate = -1;
			projectile.timeLeft = 300;
			projectile.alpha = 100;
			projectile.light = 0.8f;
			projectile.tileCollide = true;
			projectile.scale = 1.3f;
		}

		public override void AI()
		{
			// Mimic the projectile 96.

			if( projectile.localAI[0] == 0f )
			{
				projectile.localAI[0] = 1f;
				Main.PlaySound( SoundID.Item20, projectile.position );
			}
			
			int dust = Dust.NewDust( new Vector2( projectile.position.X + projectile.velocity.X, projectile.position.Y + projectile.velocity.Y ), projectile.width, projectile.height, 58, projectile.velocity.X, projectile.velocity.Y, 100, default( Color ), 2.5f * projectile.scale );
			Main.dust[dust].noGravity = true;
			Main.dust[dust].velocity *= 0.3f;
			for( int i = 0; i < 2; i++ )
			{
				dust = Dust.NewDust( new Vector2( projectile.position.X, projectile.position.Y ), projectile.width, projectile.height, 54, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 100, default( Color ), 2f );
				Main.dust[dust].noGravity = true;
				Main.dust[dust].velocity *= 0.3f;
			}

			if( projectile.ai[1] >= 20f )
			{
				projectile.velocity.Y += 0.2f;
			}

			projectile.rotation += 0.3f * (float)projectile.direction;
			
			if( projectile.velocity.Y > 16f )
			{
				projectile.velocity.Y = 16f;
				return;
			}
		}

		public override void ModifyHitPlayer( Player target, ref int damage, ref bool crit )
		{
			int dust;
			for( int i = 0; i < 100; i++ )
			{
				Vector2 rot = Vector2.Zero.RotatedByRandom( 3.1415 );
				//rot *= Main.rand.NextFloat( 200, 300 );
				Rectangle r = projectile.getRect();
				r.Inflate( 10, 10 );
				dust = Dust.NewDust( new Vector2( r.X, r.Y ), r.Width, r.Height, 58, rot.X * 10, rot.Y * 10, 0, default( Color ), 2f * projectile.scale );
				if( Main.rand.Next( 3 ) != 0 )
					Main.dust[dust].noGravity = true;
			}
			Main.PlaySound( SoundID.Dig, projectile.position );
			projectile.timeLeft = 0;
		}

		public override void Kill( int timeLeft )
		{
			// Mimic the projectile 96.
			
			Main.PlaySound( SoundID.Item10, projectile.position );
			for( int i = 0; i < 20; i++ )
			{
				int dust = Dust.NewDust( new Vector2( projectile.position.X, projectile.position.Y ), projectile.width, projectile.height, 58, -projectile.velocity.X * 0.2f, -projectile.velocity.Y * 0.2f, 100, default( Color ), 2f * projectile.scale );
				Main.dust[dust].noGravity = true;
				Main.dust[dust].velocity *= 2f;

				dust = Dust.NewDust( new Vector2( projectile.position.X, projectile.position.Y ), projectile.width, projectile.height, 58, -projectile.velocity.X * 0.2f, -projectile.velocity.Y * 0.2f, 100, default( Color ), 1f * projectile.scale );
				Main.dust[dust].velocity *= 2f;
			}
		}
	}
}