using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrariaReworked.Items
{
	public class OblivionGreatsword : ModItem
	{
		/*public override void SetStaticDefaults()
		{
			this.Tooltip.SetDefault("This is a modded sword.");  //The (English) text shown below your weapon's name
		}*/

		public override void SetDefaults()
		{
			this.item.damage = 96;           //The damage of your weapon
			this.item.melee = true;          //Is your weapon a melee weapon?
			this.item.width = 56;            //Weapon's texture's width
			this.item.height = 56;           //Weapon's texture's height
			this.item.useTime = 24;          //The time span of using the weapon. Remember in terraria, 60 frames is a second.
			this.item.useAnimation = 24;         //The time span of the using animation of the weapon, suggest set it the same as useTime.
			this.item.useStyle = 1;          //The use style of weapon, 1 for swinging, 2 for drinking, 3 act like shortsword, 4 for use like life crystal, 5 for use staffs or guns
			this.item.knockBack = 7f;         //The force of knockback of the weapon. Maximum is 20
			this.item.value = 500000;           //The value of the weapon
			this.item.rare = 8;              //The rarity of the weapon, from -1 to 13
			this.item.UseSound = SoundID.Item1;      //The sound when the weapon is using
			this.item.scale = 1.25f;
			this.item.autoReuse = true;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe( this.mod );
			recipe.AddIngredient( null, "OblivionBar", 25 );
			recipe.AddTile( mod.TileType("AdamantiteAnvil") );
			recipe.SetResult( this );
			recipe.AddRecipe();
		}
		
		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			if (Main.rand.Next(3) == 0)
			{
				//Emit dusts when swing the sword
				int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 54, 0f, 0f, 100, new Color(), 2f);
				Main.dust[dust].noGravity = true;
				dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 58, 0f, 0f, 100, new Color(), 2f);
				Main.dust[dust].noGravity = true;
			}
		}

		public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
		{
				// Add the buff 
				target.AddBuff(mod.BuffType("Oblivion"), 240);
		}
	}
}