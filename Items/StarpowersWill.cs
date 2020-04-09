using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrariaReworked.Items
{
	public class StarpowersWill : ModItem
	{
		public override void SetStaticDefaults()
		{
			this.DisplayName.SetDefault( "Starpower's Will" );
			this.Tooltip.SetDefault( "5% decreased mana usage\nIncreases maximum mana by 40\nAutomatically use mana potions when needed\n12% increased magic damage\nProvides life regeneration\nReduces the cooldown of healing potions" );
		}

		public override void SetDefaults()
		{
			this.item.width = 28;
			this.item.height = 28;
			this.item.value = 205000;
			this.item.rare = 4;
			this.item.accessory = true;
		}

		public override void UpdateAccessory( Player player, bool hideVisual )
		{
			player.statManaMax2 += 40;
			player.manaCost -= 0.05f;
			player.manaFlower = true;
			player.magicDamage += 0.12f;
			player.pStone = true;

			// regen ported from Avalon.
			float num30 = player.statLifeMax / 1100.0f * 0.85f + 0.15f;
			player.lifeRegen += (int)Math.Round( (double)num30 );
			player.lifeRegenCount += player.lifeRegen;
			while( player.lifeRegenCount >= 500 )
			{
				player.lifeRegenCount -= 500;
				if( player.statLife < player.statLifeMax )
				{
					player.statLife++;
				}
				if( player.statLife > player.statLifeMax )
				{
					player.statLife = player.statLifeMax;
				}
			}
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe( this.mod );
			recipe.AddIngredient( mod.ItemType( "GiftofStarpower" ), 1 );
			recipe.AddIngredient( ItemID.CharmofMyths, 1 );
			//recipe.AddIngredient( ItemID.SorcererEmblem, 1 );
			recipe.AddTile( TileID.TinkerersWorkbench );
			recipe.SetResult( this );
			recipe.AddRecipe();
		}
	}
}