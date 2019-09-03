using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrariaReworked.Projectiles
{
	public class TitaniumJackhammer : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			Main.projFrames[projectile.type] = 4;
		}

		public override void SetDefaults()
		{
			projectile.width = 20;
			projectile.height = 20;
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
			// Loop through the 4 animation frames, spending 5 ticks on each.
			if( ++projectile.frameCounter >= 4 )
			{
				projectile.frameCounter = 0;
				if( ++projectile.frame >= 3 )
				{
					projectile.frame = 0;
				}
			}
		}
		
		/*public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
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
		}*/
	}
}