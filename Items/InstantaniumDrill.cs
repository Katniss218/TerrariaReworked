using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrariaReworked.Items
{
	public class InstantaniumDrill : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault( "Able to mine Impervious Brick and Caesium Ore" );
		}
		
		public override void SetDefaults()
		{
			this.item.damage = 40;
			this.item.melee = true;
			this.item.width = 60;
			this.item.height = 22;
			this.item.useTime = 8;
			this.item.noMelee = true;
			item.channel = true;
			item.noUseGraphic = true;
			item.shoot = mod.ProjectileType( "InstantaniumDrill" );
			item.shootSpeed = 40;
			this.item.useAnimation = 14;
			this.item.pick = 250;
			this.item.scale = 1.1f;
			this.item.useStyle = 5;
			this.item.knockBack = 3;
			this.item.value = 554000;
			this.item.rare = 3;
			this.item.UseSound = SoundID.Item23;
			this.item.autoReuse = true;
		}
	}
}