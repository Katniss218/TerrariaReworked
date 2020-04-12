using Terraria.ID;
using Terraria.ModLoader;

namespace TerrariaReworked.Items
{
	public class BrokenHeroPick : ModItem
	{
		public override void SetDefaults()
		{
			item.width = 32;
			item.height = 32;
			item.maxStack = 99;
			item.value = 100000;
			item.rare = 8;
		}
	}
}
