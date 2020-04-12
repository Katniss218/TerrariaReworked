using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrariaReworked.Items
{
	public class TerraSpear : ModItem
	{
		/*public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("An example spear");
		}*/

		public override void SetDefaults()
		{
			this.item.width = 54;
			this.item.height = 54;
			this.item.damage = 75;
			this.item.useStyle = 5;
			this.item.useAnimation = 22;
			this.item.useTime = 22;
			this.item.shootSpeed = 13f;
			this.item.knockBack = 6.5f;
			this.item.scale = 1f;
			this.item.rare = 8;
			this.item.value = 1000000;

			this.item.melee = true;
			this.item.noMelee = true; // Important because the spear is actually a projectile instead of an item. This prevents the melee hitbox of this item.
			this.item.noUseGraphic = true; // Important, it's kind of wired if people see two spears at one time. This prevents the melee animation of this item.
			this.item.autoReuse = true; // Most spears don't autoReuse, but it's possible when used in conjunction with CanUseItem()

			this.item.UseSound = SoundID.Item1;
			this.item.shoot = mod.ProjectileType( "TerraSpear" );
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe( this.mod );
			recipe.AddIngredient( mod.ItemType( "TrueDarkLance" ), 1 );
			recipe.AddIngredient( mod.ItemType( "TrueGungnir" ), 1 );
			recipe.AddIngredient( mod.ItemType( "BrokenHeroSpear" ), 1 );
			recipe.AddTile( mod.TileType( "AdamantiteAnvil" ) );
			recipe.SetResult( this );
			recipe.AddRecipe();
		}

		public override bool CanUseItem( Player player )
		{
			// Ensures no more than one spear can be thrown out, use this when using autoReuse
			return player.ownedProjectileCounts[item.shoot] < 1;
		}
		

		/*  beam sword update snippet (sound of charging)
		if (this.itemTime > 0)
		{
			this.itemTime--;
			if( this.itemTime == 0 && this.whoAmI == Main.myPlayer )
			{
				int type3 = item.type;
				if( type3 == 65 || type3 == 676 || type3 == 723 || type3 == 724 || type3 == 989 || type3 == 1226 || type3 == 1227 )
				{
					Main.PlaySound( 25, -1, -1, 1, 1f, 0f );
					for( int num71 = 0; num71 < 5; num71++ )
					{
						int num72 = Dust.NewDust( this.position, this.width, this.height, 45, 0f, 0f, 255, default( Color ), (float)Main.rand.Next( 20, 26 ) * 0.1f );
						Main.dust[num72].noLight = true;
						Main.dust[num72].noGravity = true;
						Main.dust[num72].velocity *= 0.5f;
					}
				}
			}
		}
		*/
	}
}
