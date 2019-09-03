using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrariaReworked.Projectiles
{
	public class LeadFlail : ModProjectile
	{
		/*public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Tin Flail");     //The English name of the projectile
			//ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;    //The length of old position to be recorded
			//ProjectileID.Sets.TrailingMode[projectile.type] = 0;        //The recording mode
		}*/

		public override void SetDefaults()
		{
			projectile.Name = "Lead Flail";
			projectile.width = 22;
			projectile.height = 22;
			projectile.friendly = true;
			projectile.penetrate = -1; // Penetrates NPCs infinitely.
			projectile.melee = true; // Deals melee dmg.
			projectile.timeLeft = 1000;

			projectile.aiStyle = 15; // Set the aiStyle to that of a flail.
		}
		
		public override void AI()
		{
			this.projectile.rotation += 0.8f * (float)projectile.direction;
		}

		// Now this is where the chain magic happens. You don't have to try to figure this whole thing out.
		// Just make sure that you edit the first line (which starts with 'Texture2D texture') correctly.
		public override bool PreDraw( SpriteBatch spriteBatch, Color lightColor )
		{
			// So set the correct path here to load the chain texture. 'YourModName' is of course the name of your mod.
			// Then into the Projectiles folder and take the texture that is called 'CustomFlailBall_Chain'.
			Texture2D texture = ModLoader.GetTexture( "TerrariaReworked/Projectiles/LeadFlail_Chain" );

			Vector2 position = projectile.Center;
			Vector2 mountedCenter = Main.player[projectile.owner].MountedCenter;
			Rectangle? sourceRectangle = new Microsoft.Xna.Framework.Rectangle?();
			Vector2 origin = new Vector2( (float)texture.Width * 0.5f, (float)texture.Height * 0.5f );
			Vector2 offset = mountedCenter - position;
			float rotation = (float)Math.Atan2( (double)offset.Y, (double)offset.X ) - 1.57f;
			bool flag = true;
			if( float.IsNaN( position.X ) && float.IsNaN( position.Y ) )
			{
				flag = false;
			}
			if( float.IsNaN( offset.X ) && float.IsNaN( offset.Y ) )
			{
				flag = false;
			}
			while( flag )
			{
				if( (double)offset.Length() < (double)texture.Height + 1.0 )
				{
					flag = false;
				}
				else
				{
					Vector2 normOffset = offset;
					normOffset.Normalize();
					position += normOffset * (float)texture.Height;
					offset = mountedCenter - position;
					Color color = Lighting.GetColor( (int)position.X / 16, (int)((double)position.Y / 16.0) );
					color = projectile.GetAlpha( color );
					Main.spriteBatch.Draw( texture, position - Main.screenPosition, sourceRectangle, color, rotation, origin, 1f, SpriteEffects.None, 0.0f );
				}
			}
			return true;
		}
	}
}
