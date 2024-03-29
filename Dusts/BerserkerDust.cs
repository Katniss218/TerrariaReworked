using Terraria;
using Terraria.ModLoader;

namespace TerrariaReworked.Dusts
{
	public class BerserkerDust : ModDust
	{
		public override void OnSpawn(Dust dust)
		{
			dust.frame = new Microsoft.Xna.Framework.Rectangle( 0, 12 * Main.rand.Next( 0, 3 ), 10, 10 );
			dust.velocity *= 0.4f;
			dust.noGravity = true;
			dust.noLight = true;
			dust.scale *= 1.5f;
		}

		public override bool Update(Dust dust)
		{
			dust.position += dust.velocity;
			dust.rotation += dust.velocity.X * 0.15f;
			dust.scale *= 0.95f;
			//float light = 0.35f * dust.scale;
			//Lighting.AddLight(dust.position, light, light, light);
			if (dust.scale < 0.25f)
			{
				dust.active = false;
			}
			return false;
		}
	}
}