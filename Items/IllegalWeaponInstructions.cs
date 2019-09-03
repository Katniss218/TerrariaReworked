using Terraria.ID;
using Terraria.ModLoader;

namespace TerrariaReworked.Items
{
	public class IllegalWeaponInstructions : ModItem
	{
		public override void SetStaticDefaults()
		{
			this.Tooltip.SetDefault( "'Read, if you dare...'" );
		}

		public override void SetDefaults()
		{
			item.width = 12;
			item.height = 20;
			item.maxStack = 99;
			item.value = 2500000;
			item.rare = 7;
		}
	}
}
