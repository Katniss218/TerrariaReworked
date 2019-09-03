using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrariaReworked.Items
{
	public class Blueshift : ModItem
	{
		/*public override void SetStaticDefaults()
		{
			Tooltip.SetDefault( "This is a modded pickaxe." );
		}
		*/
		public override void SetDefaults()
		{
			this.item.damage = 7;
			this.item.melee = true;
			this.item.width = 32;
			this.item.height = 28;
			this.item.useTime = 27;
			this.item.useAnimation = 33;
			this.item.axe = 17;
			this.item.hammer = 65;
			this.item.scale = 1.2f;
			this.item.useStyle = 1;
			this.item.knockBack = 7;
			this.item.value = 24000;
			this.item.rare = 2;
			this.item.UseSound = SoundID.Item1;
			this.item.autoReuse = true;
			this.item.useTurn = true;
		}

		public override void MeleeEffects( Player player, Rectangle hitbox )
		{
			if( Main.rand.Next( 3 ) == 0 )
			{
				int dust = Dust.NewDust( new Vector2( hitbox.X, hitbox.Y ), hitbox.Width, hitbox.Height, 59, (player.velocity.X * 0.2f) + (player.direction * 3), player.velocity.Y * 0.2f, 0, new Color( 0, 0, 1 ), 1f );
				Main.dust[dust].noGravity = true;
			}
		}
	}
}