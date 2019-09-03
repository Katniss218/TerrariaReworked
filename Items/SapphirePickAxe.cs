using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrariaReworked.Items
{
	public class SapphirePickaxe : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault( "Able to mine Hellstone" );
		}
		
		public override void SetDefaults()
		{
			this.item.damage = 10;
			this.item.melee = true;
			this.item.width = 32;
			this.item.height = 32;
			this.item.useTime = 14;
			this.item.useAnimation = 19;
			this.item.pick = 75;
			this.item.scale = 1.2f;
			this.item.useStyle = 1;
			this.item.knockBack = 3;
			this.item.value = 32000;
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