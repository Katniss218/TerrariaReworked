using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrariaReworked.Items
{
	public class ChaosTome : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault( "Casts a purple flame that can pass through solid objects." );
			//Item.staff[item.type] = true; //this makes the useStyle animate as a staff instead of as a gun
		}

		public override void SetDefaults()
		{
			item.damage = 21;
			item.magic = true;
			item.mana = 10;
			item.width = 28;
			item.height = 30;
			item.useTime = 25;
			item.useAnimation = 25;
			item.crit = 4;
			item.useStyle = 5;
			item.noMelee = true; //so the item's animation doesn't do damage
			item.knockBack = 3.5f;
			item.value = 18400;
			item.rare = 2;
			item.UseSound = SoundID.Item20;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("ChaosBall2");
			item.shootSpeed = 6.5f;
		}
	}
}