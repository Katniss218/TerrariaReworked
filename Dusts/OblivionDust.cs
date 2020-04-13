using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace TerrariaReworked.Dusts
{
	public class OblivionDust : ModDust
	{
		public override void OnSpawn(Dust dust)
		{
			dust.frame = new Rectangle( 0, 12 * Main.rand.Next( 0, 3 ), 10, 10 );
			dust.velocity *= 0.4f;
			dust.noGravity = true;
			dust.noLight = true;
		}

		public override bool Update(Dust dust)
		{
			dust.position += dust.velocity;
			dust.rotation += dust.velocity.X * 0.15f;
			dust.scale *= 0.92f;
			//float light = 0.35f * dust.scale;
			//Lighting.AddLight(dust.position, light, light, light);
			if (dust.scale < 0.25f)
			{
				dust.active = false;
			}
			if( !dust.noGravity )
			{
				dust.velocity.Y = dust.velocity.Y + 0.05f;
			}
			if( !dust.noLight )
			{
				float strength = dust.scale;
				if( strength > 1f )
				{
					strength = 1f;
				}
				Lighting.AddLight( dust.position, 0.15f * strength, 0.05f * strength, 0.325f * strength );
			}
			return false;
		}

		// Token: 0x06006559 RID: 25945 RVA: 0x00447449 File Offset: 0x00445649
		public override Color? GetAlpha( Dust dust, Color lightColor )
		{
			return new Color?( new Color( (int)lightColor.R, (int)lightColor.G, (int)lightColor.B, 90 ) );
		}
	}
}