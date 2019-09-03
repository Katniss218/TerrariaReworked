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
			Item refItem = new Item();
			refItem.SetDefaults( ItemID.SoulofSight );
			item.width = refItem.width;
			item.height = refItem.height;
			item.maxStack = 999;
			item.value = 75000;
			item.rare = 5;
		}

		public override void PostUpdate()
		{
			Lighting.AddLight( item.Center, Color.WhiteSmoke.ToVector3() * 0.55f * Main.essScale );
		}
	}
}
