using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrariaReworked.Items
{
	public class SoulofBlight : ModItem
	{
		public override void SetStaticDefaults()
		{
			this.DisplayName.SetDefault( "Soul of Blight" );
			this.Tooltip.SetDefault( "'The essence of destruction'" );
			Main.RegisterItemAnimation( item.type, new DrawAnimationVertical( 5, 4 ) );
			ItemID.Sets.AnimatesAsSoul[item.type] = true;
			ItemID.Sets.ItemIconPulse[item.type] = true;
			ItemID.Sets.ItemNoGravity[item.type] = true;
		}

		public override void SetDefaults()
		{
			Item r = new Item();
			r.SetDefaults( ItemID.SoulofSight );
			Main.NewText( r.glowMask, Color.Red );
			Main.NewText( r.alpha, Color.Red );
			Main.NewText( r.color, Color.Red );
			item.width = 18;
			item.height = 18;
			item.maxStack = 999;
			item.value = 75000;
			item.rare = 6;
		}

		public override void PostUpdate()
		{
			// ported from vanilla code.

			float strength = (float)Main.rand.Next( 90, 111 ) * 0.01f;
			strength *= Main.essScale;

			int x = (int)((item.position.X + (float)(item.width / 2)) / 16f);
			int y = (int)((item.position.Y + (float)(item.height / 2)) / 16f);

			Lighting.AddLight( x, y, 0.5f * strength, 0.5f * strength, 0.0f * strength );
		}

		public override Color? GetAlpha( Color lightColor )
		{
			// ported from vanilla code.

			float num2 = (float)(255 - item.alpha) / 255f;
			int r = (int)((float)lightColor.R * num2);
			int g = (int)((float)lightColor.G * num2);
			int b = (int)((float)lightColor.B * num2);
			int num3 = (int)lightColor.A - item.alpha;
			if( num3 < 0 )
			{
				num3 = 0;
			}
			if( num3 > 255 )
			{
				num3 = 255;
			}
			return new Color( r, g, b, num3 );
		}
	}
}
