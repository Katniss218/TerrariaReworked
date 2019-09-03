using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrariaReworked.Projectiles
{
	//ported from my tAPI mod because I don't want to make artwork
	public class InstantaniumDrill : ModProjectile
	{
		public override void SetDefaults()
		{
			projectile.width = 22;
			projectile.height = 22;
			projectile.aiStyle = 20;
			projectile.friendly = true;
			projectile.penetrate = -1;
			projectile.tileCollide = false;
			projectile.hide = true;
			projectile.ownerHitCheck = true; //so you can't hit enemies through walls
			projectile.melee = true;
		}

		public override void AI()
		{
			int dust = Dust.NewDust(projectile.position + projectile.velocity * 0.85f, projectile.width, projectile.height, 6, 0, 0, 100, default(Color), 2.5f);
			Main.dust[dust].noGravity = true;
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			if (Main.rand.Next(10) == 0)
			{
				target.AddBuff(BuffID.OnFire, 180, false);
			}
		}

		public override void OnHitPvp(Player target, int damage, bool crit)
		{
			if (Main.rand.Next(10) == 0)
			{
				target.AddBuff(BuffID.OnFire, 180, false);
			}
		}
	}
}