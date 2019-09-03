using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrariaReworked.Projectiles
{
	public class BerserkerNightmare : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Berserker Nightmare");     //The English name of the projectile
			//ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;    //The length of old position to be recorded
			//ProjectileID.Sets.TrailingMode[projectile.type] = 0;        //The recording mode
		}

		public override void SetDefaults()
		{
			projectile.Name = "Berserker Nightmare";
			projectile.width = 38;
			projectile.height = 38;
			projectile.friendly = true;
			projectile.penetrate = -1; // Penetrates NPCs infinitely.
			projectile.melee = true; // Deals melee dmg.
			projectile.timeLeft = 1000;

			projectile.aiStyle = 15; // Set the aiStyle to that of a flail.
		}
		
		public override void AI()
		{
			//throw new System.Exception( "" + (Main.projectileTexture[this.projectile.type].Width) );
			if( Main.rand.Next( 6 ) == 0 )
			{
				Rectangle hitbox = this.projectile.getRect();
				Vector2 randomInside = new Vector2( hitbox.X + Main.rand.Next( 0, hitbox.Width ), hitbox.Y + Main.rand.Next( 0, hitbox.Height ) );
				Projectile.NewProjectile( randomInside, Vector2.Zero, mod.ProjectileType( "BerserkerDust" ), TerrariaReworked.BerserkerDustDamage, 1, this.projectile.owner, 0, 0 );
			}
			this.projectile.rotation += 0.8f * (float)projectile.direction;
			if( Main.rand.Next( 3 ) == 0 )
				Dust.NewDust( this.projectile.position, this.projectile.width, this.projectile.height, mod.DustType( "BerserkerDust" ), Main.rand.NextFloat( -0.1f, 0.1f ), Main.rand.NextFloat( -0.1f, 0.1f ) );
		}

		// Now this is where the chain magic happens. You don't have to try to figure this whole thing out.
		// Just make sure that you edit the first line (which starts with 'Texture2D texture') correctly.
		public override bool PreDraw( SpriteBatch spriteBatch, Color lightColor )
		{
			// So set the correct path here to load the chain texture. 'YourModName' is of course the name of your mod.
			// Then into the Projectiles folder and take the texture that is called 'CustomFlailBall_Chain'.
			Texture2D texture = ModLoader.GetTexture( "TerrariaReworked/Projectiles/BerserkerNightmare_Chain" );

			Vector2 position = projectile.Center;
			Vector2 mountedCenter = Main.player[projectile.owner].MountedCenter;
			Rectangle? sourceRectangle = new Microsoft.Xna.Framework.Rectangle?();
			Vector2 origin = new Vector2( (float)texture.Width * 0.5f, (float)texture.Height * 0.5f );
			float num1 = (float)texture.Height;
			Vector2 vector2_4 = mountedCenter - position;
			float rotation = (float)Math.Atan2( (double)vector2_4.Y, (double)vector2_4.X ) - 1.57f;
			bool flag = true;
			if( float.IsNaN( position.X ) && float.IsNaN( position.Y ) )
				flag = false;
			if( float.IsNaN( vector2_4.X ) && float.IsNaN( vector2_4.Y ) )
				flag = false;
			while( flag )
			{
				if( (double)vector2_4.Length() < (double)num1 + 1.0 )
				{
					flag = false;
				}
				else
				{
					Vector2 vector2_1 = vector2_4;
					vector2_1.Normalize();
					position += vector2_1 * num1;
					vector2_4 = mountedCenter - position;
					Color color2 = Lighting.GetColor( (int)position.X / 16, (int)((double)position.Y / 16.0) );
					color2 = projectile.GetAlpha( color2 );
					Main.spriteBatch.Draw( texture, position - Main.screenPosition, sourceRectangle, color2, rotation, origin, 1f, SpriteEffects.None, 0.0f );
				}
			}

			return true;
		}
	}
}
