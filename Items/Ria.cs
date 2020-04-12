using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrariaReworked.Items
{
	public class Ria : ModItem
	{
		public override void SetStaticDefaults()
		{
			this.Tooltip.SetDefault("'The ultimate weapon.'");
		}

		public override void SetDefaults()
		{
			this.item.damage = 166;           //The damage of your weapon
			this.item.melee = true;          //Is your weapon a melee weapon?
			this.item.width = 64;            //Weapon's texture's width
			this.item.height = 64;           //Weapon's texture's height
			this.item.useTime = 13;          //The time span of using the weapon. Remember in terraria, 60 frames is a second.
			this.item.useAnimation = 13;         //The time span of the using animation of the weapon, suggest set it the same as useTime.
			this.item.useStyle = 1;          //The use style of weapon, 1 for swinging, 2 for drinking, 3 act like shortsword, 4 for use like life crystal, 5 for use staffs or guns
			this.item.knockBack = 4f;         //The force of knockback of the weapon. Maximum is 20
			this.item.value = 25000000;           //The value of the weapon
			this.item.rare = 0;              //The rarity of the weapon, from -1 to 13
			this.item.crit = 25;
			this.item.UseSound = SoundID.Item1;      //The sound when the weapon is using
			this.item.scale = 1.25f;
			this.item.autoReuse = true;
		}
		
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe( this.mod );
			recipe.AddIngredient( null, "BerserkerBar", 20 );
			recipe.AddTile( mod.TileType( "BerserkerAnvil" ) );
			recipe.SetResult( this );
			recipe.AddRecipe();
		}

		public override bool UseItem( Player player )
		{
			float targetX = Main.screenPosition.X + Main.mouseX;
			if( targetX < player.position.X )
				player.direction = -1;
			else
				player.direction = 1;
			
			Vector2 target = Main.screenPosition + new Vector2( (float)Main.mouseX, (float)Main.mouseY );
			
			Vector2 vel = target - player.position;
			vel.Normalize();
					   //const int spread = 75;
					   //vel.X += (0.05f * Main.rand.Next( -spread, spread ));
					   //vel.Y += (0.05f * Main.rand.Next( -spread, spread ));

			float separation = 25;

			Vector2 tangent = new Vector2( vel.Y, -vel.X );

			vel *= 25; // speed

			Vector2 pos1 = player.position;
			Vector2 pos2 = player.position + (tangent * separation);
			Vector2 pos3 = player.position - (tangent * separation);

			Lighting.AddLight( player.position, 1f, 1f, 0.8f );

			Projectile.NewProjectile( pos1, vel, mod.ProjectileType( "RiaProjectile1" ), ModMain.RiaProjectileDamage[0], 3, player.whoAmI );
			Projectile.NewProjectile( pos2, vel.RotatedBy( MathHelper.ToRadians( -15 ) ), mod.ProjectileType( "RiaProjectile2" ), ModMain.RiaProjectileDamage[1], 3, player.whoAmI );
			Projectile.NewProjectile( pos3, vel.RotatedBy( MathHelper.ToRadians( 15 ) ), mod.ProjectileType( "RiaProjectile3" ), ModMain.RiaProjectileDamage[2], 3, player.whoAmI );

			return true;
		}

		public override void MeleeEffects( Player player, Rectangle hitbox )
		{
			//int dust = Dust.NewDust( new Vector2( hitbox.X, hitbox.Y ), hitbox.Width, hitbox.Height, mod.DustType( "BerserkerDust" ), Main.rand.NextFloat( -0.1f, 0.1f ), Main.rand.NextFloat( -0.1f, 0.1f ) );

			for( int i = 0; i < 3; i++ )
			{
				int dust = Dust.NewDust( new Vector2( hitbox.X, hitbox.Y ), hitbox.Width, hitbox.Height, 171, Main.rand.NextFloat( -0.1f, 0.1f ), Main.rand.NextFloat( -0.1f, 0.1f ), 1, default( Color ), 1f );
				Main.dust[dust].noGravity = true;
				Main.dust[dust].velocity = Main.dust[dust].velocity * 0.3f;
			}
			for( int i = 0; i < 3; i++ )
			{
				int dust = Dust.NewDust( new Vector2( hitbox.X, hitbox.Y ), hitbox.Width, hitbox.Height, 172, Main.rand.NextFloat( -0.1f, 0.1f ), Main.rand.NextFloat( -0.1f, 0.1f ), 1, default( Color ), 1f );
				Main.dust[dust].noGravity = true;
				Main.dust[dust].velocity = Main.dust[dust].velocity * 0.3f;
			}
			for( int i = 0; i < 3; i++ )
			{
				int dust = Dust.NewDust( new Vector2( hitbox.X, hitbox.Y ), hitbox.Width, hitbox.Height, 173, Main.rand.NextFloat( -0.1f, 0.1f ), Main.rand.NextFloat( -0.1f, 0.1f ), 1, default( Color ), 1f );
				Main.dust[dust].noGravity = true;
				Main.dust[dust].velocity = Main.dust[dust].velocity * 0.3f;
			}
		}

		public override void OnHitNPC( Player player, NPC target, int damage, float knockback, bool crit )
		{
			// Add Onfire buff to the NPC for 1 second
			// 60 frames = 1 second
			//target.AddBuff(BuffID.OnFire, 60);
		}
	}
}