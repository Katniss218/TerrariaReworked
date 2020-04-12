using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrariaReworked.Projectiles
{
	public class TrueGungnir2 : ModProjectile
	{
		public override void SetDefaults()
		{
			projectile.width = 16;
			projectile.height = 16;
			projectile.friendly = true;
			projectile.tileCollide = true;
			projectile.aiStyle = 27;
			projectile.light = 0.5f;
			projectile.melee = true;
			projectile.alpha = 255;
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

		/*public override void AI()
		{
			//DoGravity();


			int dust;
			for( int i = 0; i < 12; i++ )
			{
				dust = Dust.NewDust( new Vector2( projectile.position.X + (projectile.velocity.X * (i/5f)), projectile.position.Y + (projectile.velocity.Y * (i / 5f) ) ), projectile.width, projectile.height, 172, projectile.velocity.X, projectile.velocity.Y, 0, default( Color ), 1f );
				Main.dust[dust].noGravity = true;
				Main.dust[dust].velocity = Main.dust[dust].velocity * 0.3f;
			}
			projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians( 45f );
		}*/

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
				float num801 = this.projectile.oldVelocity.X * (30f / (float)i);
				float num802 = this.projectile.oldVelocity.Y * (30f / (float)i);
				int num803 = Dust.NewDust( new Vector2( this.projectile.oldPosition.X - num801, this.projectile.oldPosition.Y - num802 ), 8, 8, 73, this.projectile.oldVelocity.X, this.projectile.oldVelocity.Y, 255, default( Color ), 1.8f );
				Main.dust[num803].noGravity = true;
				Dust dust = Main.dust[num803];
				dust.velocity *= 0.5f;
				num803 = Dust.NewDust( new Vector2( this.projectile.oldPosition.X - num801, this.projectile.oldPosition.Y - num802 ), 8, 8, 73, this.projectile.oldVelocity.X, this.projectile.oldVelocity.Y, 255, default( Color ), 1.4f );
				dust = Main.dust[num803];
				dust.velocity *= 0.05f;
				Main.dust[num803].noGravity = true;
			}
		}
	}
}