﻿using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrariaReworked.Items
{
	public class TrueGungnir : ModItem
	{
		/*public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("An example spear");
		}*/

		public override void SetDefaults()
		{
			this.item.width = 54;
			this.item.height = 54;
			this.item.damage = 52;
			this.item.useStyle = 5;
			this.item.useAnimation = 18;
			this.item.useTime = 18;
			this.item.shootSpeed = 14f;
			this.item.knockBack = 6.4f;
			this.item.scale = 1f;
			this.item.rare = 8;
			this.item.value = 500000;

			this.item.melee = true;
			this.item.noMelee = true; // Important because the spear is actually a projectile instead of an item. This prevents the melee hitbox of this item.
			this.item.noUseGraphic = true; // Important, it's kind of wired if people see two spears at one time. This prevents the melee animation of this item.
			this.item.autoReuse = false; // Most spears don't autoReuse, but it's possible when used in conjunction with CanUseItem()

			this.item.UseSound = SoundID.Item1;
			this.item.shoot = mod.ProjectileType("TrueGungnir");
		}

		public override bool CanUseItem(Player player)
		{
			// Ensures no more than one spear can be thrown out, use this when using autoReuse
			return player.ownedProjectileCounts[item.shoot] < 1; 
		}
	}
}
