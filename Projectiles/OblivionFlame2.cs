using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrariaReworked.Projectiles
{
	public class OblivionFlame2 : ModProjectile
	{
		public override void SetDefaults()
		{
			projectile.aiStyle = -1; // replicates ai 8
			projectile.width = 32;
			projectile.height = 32;
			projectile.hostile = true;
			projectile.magic = true;
			projectile.timeLeft = 300;
			projectile.alpha = 100;
			projectile.light = 0.8f;
			projectile.tileCollide = true;
			projectile.scale = 1.3f;
		}

		public override void AI()
		{
			projectile.ai[1]++;

			if( projectile.localAI[0] == 0f )
			{
				projectile.localAI[0] = 1f;
				Main.PlaySound( SoundID.Item13, projectile.position );
			}
			
			int dust = Dust.NewDust( new Vector2( projectile.position.X + projectile.velocity.X - 5, projectile.position.Y + projectile.velocity.Y - 5 ), projectile.width + 10, projectile.height + 10, ModMain.instance.DustType( "OblivionDust" ), projectile.velocity.X, projectile.velocity.Y, 100, default( Color ), 2.5f * projectile.scale );
			//Main.dust[dust].noGravity = true;
			Main.dust[dust].velocity *= 0.3f;
			for( int i = 0; i < 2; i++ )
			{
				dust = Dust.NewDust( new Vector2( projectile.position.X - 5, projectile.position.Y - 5 ), projectile.width + 10, projectile.height + 10, ModMain.instance.DustType( "OblivionDust" ), projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 100, default( Color ), 2f );
				//Main.dust[dust].noGravity = true;
				Main.dust[dust].velocity *= 0.9f;
			}

			if( projectile.ai[1] >= 8f )
			{
				projectile.velocity *= 0.97f;
				projectile.ai[1] = 0;
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
			projectile.timeLeft = 0;
		}

		public override bool PreDraw( SpriteBatch spriteBatch, Color lightColor )
		{
			lightColor.A = (byte)(lightColor.A * 0.2f);
			return true;
		}

		public override void Kill( int timeLeft )
		{
			// Mimic the projectile 96.
			
			Main.PlaySound( SoundID.Item10, projectile.position );
			for( int i = 0; i < 20; i++ )
			{
				int dust = Dust.NewDust( new Vector2( projectile.position.X, projectile.position.Y ), projectile.width, projectile.height, ModMain.instance.DustType( "OblivionDust" ), -projectile.velocity.X * 0.2f, -projectile.velocity.Y * 0.2f, 100, default( Color ), 2f * projectile.scale );
				Main.dust[dust].noGravity = true;
				Main.dust[dust].velocity *= 2f;

				dust = Dust.NewDust( new Vector2( projectile.position.X, projectile.position.Y ), projectile.width, projectile.height, ModMain.instance.DustType( "OblivionDust" ), -projectile.velocity.X * 0.2f, -projectile.velocity.Y * 0.2f, 100, default( Color ), 1f * projectile.scale );
				Main.dust[dust].velocity *= 2f;
			}
		}
	}
}