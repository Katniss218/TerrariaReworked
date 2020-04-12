using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrariaReworked.Projectiles
{
	public class RiaProjectile1 : ModProjectile
	{
		public override void SetDefaults()
		{
			projectile.width = 10;
			projectile.height = 10;
			projectile.friendly = true;
			projectile.penetrate = 2;
			projectile.tileCollide = true;
			projectile.timeLeft = 7;
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

		public override void AI()
		{
			//DoGravity();


			int dust;
			for( int i = 0; i < 12; i++ )
			{
				dust = Dust.NewDust( new Vector2( projectile.position.X + (projectile.velocity.X * (i/5f)), projectile.position.Y + (projectile.velocity.Y * (i / 5f) ) ), projectile.width, projectile.height, 172, projectile.velocity.X, projectile.velocity.Y, 0, default( Color ), 1f );
				Main.dust[dust].noGravity = true;
				Main.dust[dust].velocity = Main.dust[dust].velocity * 0.3f;
			}
			projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians( 90f );
		}
	}
}