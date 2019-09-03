using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrariaReworked.Projectiles
{
	public class ChaosBall2 : ModProjectile
	{
		public override void SetDefaults()
		{
			projectile.width = 16;
			projectile.height = 16;
			projectile.friendly = true;
			projectile.magic = true;
			projectile.penetrate = 3;
			projectile.timeLeft = 300;
			projectile.alpha = 160;
			projectile.light = 1;
			projectile.tileCollide = false;
			projectile.scale = 0.85f;
		}

		public override void AI()
		{
			
			int dust;
			for( int i = 0; i < 5; i++ )
			{
				dust = Dust.NewDust( new Vector2( projectile.position.X + projectile.velocity.X, projectile.position.Y + projectile.velocity.Y ), projectile.width, projectile.height, 27, 0, 0, 100, default( Color ), 1.25f );
				Main.dust[dust].noGravity = true;
				Main.dust[dust].velocity = Main.dust[dust].velocity * 0.3f;
			}
			projectile.rotation += 0.3f * (float)projectile.direction;
		}
	}
}