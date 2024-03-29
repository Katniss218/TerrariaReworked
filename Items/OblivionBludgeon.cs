using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrariaReworked.Items
{
	public class OblivionBludgeon : ModItem
	{
		public override void SetStaticDefaults()
		{
			this.Tooltip.SetDefault( "Has a chance to confuse the enemy" );  //The (English) text shown below your weapon's name
		}

		public override void SetDefaults()
		{
			this.item.damage = 109;           //The damage of your weapon
			this.item.melee = true;          //Is your weapon a melee weapon?
			this.item.width = 38;            //Weapon's texture's width
			this.item.height = 38;           //Weapon's texture's height
			this.item.useTime = 28;          //The time span of using the weapon. Remember in terraria, 60 frames is a second.
			this.item.useAnimation = 28;         //The time span of the using animation of the weapon, suggest set it the same as useTime.
			this.item.useStyle = 1;          //The use style of weapon, 1 for swinging, 2 for drinking, 3 act like shortsword, 4 for use like life crystal, 5 for use staffs or guns
			this.item.knockBack = 10f;         //The force of knockback of the weapon. Maximum is 20
			this.item.value = 250;           //The value of the weapon
			this.item.rare = 0;              //The rarity of the weapon, from -1 to 13
			this.item.UseSound = SoundID.Item1;      //The sound when the weapon is using
			this.item.scale = 1.3f;
			this.item.autoReuse = true;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe( this.mod );
			recipe.AddIngredient( null, "OblivionBar", 15 );
			recipe.AddTile( mod.TileType( "AdamantiteAnvil" ) );
			recipe.SetResult( this );
			recipe.AddRecipe();
		}

		public override void MeleeEffects( Player player, Rectangle hitbox )
		{
			if( Main.rand.Next( 3 ) == 0 )
			{
				for( int i = 0; i < 2; i++ )
				{
					int dust = Dust.NewDust( new Vector2( hitbox.X, hitbox.Y ), hitbox.Width, hitbox.Height, ModMain.instance.DustType( "OblivionDust" ), 0f, 0f, 100, default, 2f );
					Main.dust[dust].noGravity = true;
				}
			}
		}

		public override void OnHitNPC( Player player, NPC target, int damage, float knockback, bool crit )
		{
			// Add Onfire buff to the NPC for 1 second
			// 60 frames = 1 second
			if( Main.rand.Next( 4 ) != 0 )
			{
				target.AddBuff( BuffID.Confused, 120 );
			}
			target.AddBuff( mod.BuffType( "Oblivion" ), 240 );
		}
	}
}