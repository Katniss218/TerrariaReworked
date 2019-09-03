using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrariaReworked.Items
{
	public class PlatinumFlail : ModItem
	{
		/*public override void SetStaticDefaults()
		{
			this.Tooltip.SetDefault("This is a modded sword.");  //The (English) text shown below your weapon's name
		}*/

		public override void SetDefaults()
		{
			this.item.damage = 15;           //The damage of your weapon
			this.item.melee = true;          //Is your weapon a melee weapon?
			this.item.width = 38;            //Weapon's texture's width
			this.item.height = 30;           //Weapon's texture's height
			this.item.useTime = 44;          //The time span of using the weapon. Remember in terraria, 60 frames is a second.
			this.item.useAnimation = 44;         //The time span of the using animation of the weapon, suggest set it the same as useTime.
			this.item.useStyle = 5;          //The use style of weapon, 1 for swinging, 2 for drinking, 3 act like shortsword, 4 for use like life crystal, 5 for use staffs or guns
			this.item.knockBack = 7f;         //The force of knockback of the weapon. Maximum is 20
			this.item.value = 9000;           //The value of the weapon
			this.item.rare = 0;              //The rarity of the weapon, from -1 to 13
			this.item.UseSound = SoundID.Item1;      //The sound when the weapon is using
			this.item.scale = 1.2f;
			this.item.autoReuse = true;
			this.item.shoot = mod.ProjectileType( "PlatinumFlail" );
			this.item.shootSpeed = 16;
			this.item.noUseGraphic = true;
			this.item.noMelee = true;
			this.item.channel = true;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe( this.mod );
			recipe.AddIngredient( ItemID.PlatinumBar, 8 );
			recipe.AddIngredient( ItemID.Chain, 5 );
			recipe.AddTile( TileID.Anvils );
			recipe.SetResult( this );
			recipe.AddRecipe();
		}
	}
}