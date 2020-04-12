using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;
// If you are using c# 6, you can use: "using static Terraria.Localization.GameCulture;" which would mean you could just write "DisplayName.AddTranslation(German, "");"
//using Terraria.Localization;

namespace TerrariaReworked.Items
{
	// this gets executed not only for coal, byt for all items
	public class VanillaItemChanger : GlobalItem
	{
		//override void 
		public override void SetDefaults( Item item )
		{
			for( int i = 0; i < ModMain.disabledItems.Length; i++ )
			{
				if( ModMain.disabledItems[i] == item.type )
				{
					// don't set the item to null. crashes the game.
					item.SetNameOverride( "This item is disabled by TerrariaReworked!" );
					item.damage = 0;
					item.defense = 0;
					item.useStyle = 4;
					item.createTile = -1;
					item.createWall = -1;
					item.consumable = true;
					item.hammer = 0;
					item.pick = 0;
					item.axe = 0;
					item.useTime = 1;
					item.useAnimation = 1;
					item.shootSpeed = 0;
					return;
				}
			}
			//
			//   Copper
			//
			if( item.type == ItemID.CopperShortsword || item.type == ItemID.TinShortsword )
			{
				item.damage = 5;
				item.knockBack = 4;
				item.useTime = 13;
				item.useAnimation = 13;
				item.value = 350;
				return;
			}
			if( item.type == ItemID.CopperBroadsword || item.type == ItemID.TinBroadsword )
			{
				item.damage = 8;
				item.knockBack = 5;
				item.useTime = 22;
				item.useAnimation = 22;
				item.value = 450;
				return;
			}
			if( item.type == ItemID.CopperBow || item.type == ItemID.TinBow )
			{
				item.damage = 6;
				item.knockBack = 0;
				item.useAnimation = 28;
				item.useTime = 28;
				item.value = 350;
				item.shootSpeed = 6.6f;
				return;
			}
			if( item.type == ItemID.CopperPickaxe || item.type == ItemID.TinPickaxe )
			{
				item.damage = 4;
				item.pick = 35;
				item.knockBack = 2;
				item.tileBoost = -1;
				item.useAnimation = 22;
				item.useTime = 15;
				item.value = 500;
				return;
			}
			if( item.type == ItemID.CopperAxe || item.type == ItemID.TinAxe )
			{
				item.damage = 3;
				item.axe = 7;
				item.knockBack = 4.5f;
				item.tileBoost = -1;
				item.useAnimation = 29;
				item.useTime = 21;
				item.value = 400;
				return;
			}
			if( item.type == ItemID.CopperHammer || item.type == ItemID.TinHammer )
			{
				item.damage = 4;
				item.hammer = 35;
				item.knockBack = 5.5f;
				item.tileBoost = -1;
				item.useAnimation = 32;
				item.useTime = 23;
				item.value = 400;
				return;
			}
			if( item.type == ItemID.CopperHelmet || item.type == ItemID.TinHelmet )
			{
				item.defense = 1; item.value = 1250;
				return;
			}
			if( item.type == ItemID.CopperChainmail || item.type == ItemID.TinChainmail )
			{
				item.defense = 2; item.value = 1000;
				return;
			}
			if( item.type == ItemID.CopperGreaves || item.type == ItemID.TinGreaves )
			{
				item.defense = 1; item.value = 750;
				return;
			}


			//
			//   Iron
			//
			if( item.type == ItemID.IronShortsword || item.type == ItemID.LeadShortsword )
			{
				item.damage = 7;
				item.knockBack = 4;
				item.useTime = 12;
				item.useAnimation = 12;
				item.value = 1400;
				return;
			}
			if( item.type == ItemID.IronBroadsword || item.type == ItemID.LeadBroadsword )
			{
				item.damage = 10;
				item.knockBack = 5;
				item.useTime = 21;
				item.useAnimation = 21;
				item.value = 1800;
				return;
			}
			if( item.type == ItemID.IronBow || item.type == ItemID.LeadBow )
			{
				item.damage = 8;
				item.knockBack = 0;
				item.useAnimation = 27;
				item.useTime = 27;
				item.value = 1400;
				item.shootSpeed = 6.6f;
				return;
			}
			if( item.type == ItemID.IronPickaxe || item.type == ItemID.LeadPickaxe )
			{
				item.damage = 5;
				item.pick = 40;
				item.knockBack = 2;
				item.tileBoost = 0;
				item.useAnimation = 19;
				item.useTime = 13;
				item.value = 2000;
				return;
			}
			if( item.type == ItemID.IronAxe || item.type == ItemID.LeadAxe )
			{
				item.damage = 5;
				item.axe = 8;
				item.knockBack = 4.5f;
				item.tileBoost = 0;
				item.useAnimation = 26;
				item.useTime = 19;
				item.value = 1600;
				return;
			}
			if( item.type == ItemID.IronHammer || item.type == ItemID.LeadHammer )
			{
				item.damage = 7;
				item.hammer = 40;
				item.knockBack = 5.5f;
				item.tileBoost = 0;
				item.useAnimation = 29;
				item.useTime = 20;
				item.value = 1600;
				return;
			}
			if( item.type == ItemID.IronHelmet || item.type == ItemID.LeadHelmet )
			{
				item.defense = 2; item.value = 5000;
				return;
			}
			if( item.type == ItemID.IronChainmail || item.type == ItemID.LeadChainmail )
			{
				item.defense = 3; item.value = 4000;
				return;
			}
			if( item.type == ItemID.IronGreaves || item.type == ItemID.LeadGreaves )
			{
				item.defense = 2; item.value = 3000;
				return;
			}


			//
			//   Silver
			//
			if( item.type == ItemID.SilverShortsword || item.type == ItemID.TungstenShortsword )
			{
				item.damage = 9;
				item.knockBack = 4;
				item.useTime = 11;
				item.useAnimation = 11;
				item.value = 3500;
				return;
			}
			if( item.type == ItemID.SilverBroadsword || item.type == ItemID.TungstenBroadsword )
			{
				item.damage = 12;
				item.knockBack = 5;
				item.useTime = 20;
				item.useAnimation = 20;
				item.value = 4500;
				return;
			}
			if( item.type == ItemID.SilverBow || item.type == ItemID.TungstenBow )
			{
				item.damage = 10;
				item.knockBack = 0;
				item.useAnimation = 26;
				item.useTime = 26;
				item.value = 3500;
				item.shootSpeed = 6.6f;
				return;
			}
			if( item.type == ItemID.SilverPickaxe || item.type == ItemID.TungstenPickaxe )
			{
				item.damage = 6;
				item.pick = 45;
				item.knockBack = 2;
				item.tileBoost = 0;
				item.useAnimation = 18;
				item.useTime = 11;
				item.value = 5000;
				return;
			}
			if( item.type == ItemID.SilverAxe || item.type == ItemID.TungstenAxe )
			{
				item.damage = 5;
				item.axe = 9;
				item.knockBack = 4.5f;
				item.tileBoost = 0;
				item.useAnimation = 25;
				item.useTime = 18;
				item.value = 4000;
				return;
			}
			if( item.type == ItemID.SilverHammer || item.type == ItemID.TungstenHammer )
			{
				item.damage = 9;
				item.hammer = 45;
				item.knockBack = 5.5f;
				item.tileBoost = 0;
				item.useAnimation = 28;
				item.useTime = 19;
				item.value = 4000;
				return;
			}
			if( item.type == ItemID.SilverHelmet || item.type == ItemID.TungstenHelmet )
			{
				item.defense = 3; item.value = 12500;
				return;
			}
			if( item.type == ItemID.SilverChainmail || item.type == ItemID.TungstenChainmail )
			{
				item.defense = 4; item.value = 10000;
				return;
			}
			if( item.type == ItemID.SilverGreaves || item.type == ItemID.TungstenGreaves )
			{
				item.defense = 3; item.value = 7500;
				return;
			}


			//
			//   Gold
			//
			if( item.type == ItemID.GoldShortsword || item.type == ItemID.PlatinumShortsword )
			{
				item.damage = 11;
				item.knockBack = 4;
				item.useTime = 10;
				item.useAnimation = 10;
				item.value = 7000;
				return;
			}
			if( item.type == ItemID.GoldBroadsword || item.type == ItemID.PlatinumBroadsword )
			{
				item.damage = 14;
				item.knockBack = 5;
				item.useTime = 19;
				item.useAnimation = 19;
				item.value = 9000;
				return;
			}
			if( item.type == ItemID.GoldBow || item.type == ItemID.PlatinumBow )
			{
				item.damage = 12;
				item.knockBack = 0;
				item.useAnimation = 25;
				item.useTime = 25;
				item.value = 7000;
				item.shootSpeed = 6.6f;
				return;
			}
			if( item.type == ItemID.GoldPickaxe || item.type == ItemID.PlatinumPickaxe )
			{
				item.damage = 6;
				item.pick = 55;
				item.knockBack = 2;
				item.tileBoost = 0;
				item.useAnimation = 19;
				item.useTime = 17;
				item.value = 10000;
				return;
			}
			if( item.type == ItemID.GoldAxe || item.type == ItemID.PlatinumAxe )
			{
				item.damage = 7;
				item.axe = 11;
				item.knockBack = 4.5f;
				item.tileBoost = 0;
				item.useAnimation = 25;
				item.useTime = 18;
				item.value = 8000;
				return;
			}
			if( item.type == ItemID.GoldHammer || item.type == ItemID.PlatinumHammer )
			{
				item.damage = 9;
				item.hammer = 55;
				item.knockBack = 5.5f;
				item.tileBoost = 0;
				item.useAnimation = 27;
				item.useTime = 23;
				item.value = 8000;
				return;
			}
			if( item.type == ItemID.GoldHelmet || item.type == ItemID.PlatinumHelmet )
			{
				item.defense = 4; item.value = 25000;
				return;
			}
			if( item.type == ItemID.GoldChainmail || item.type == ItemID.PlatinumChainmail )
			{
				item.defense = 5; item.value = 20000;
				return;
			}
			if( item.type == ItemID.GoldGreaves || item.type == ItemID.PlatinumGreaves )
			{
				item.defense = 4; item.value = 15000;
				return;
			}


			//
			//   Coal
			//
			if( item.type == ItemID.Coal )
			{
				item.SetNameOverride( "Coal" );
				item.width = 12;
				item.height = 12;
				item.maxStack = 999;
				item.useTurn = true;
				item.autoReuse = true;
				item.useAnimation = 15;
				item.useTime = 10;
				item.useStyle = 1;
				item.consumable = true;
				item.createTile = this.mod.TileType( "Coal" );
				return;
			}

			if( item.type == ItemID.ChlorophyteOre )
			{
				item.SetNameOverride( "Caesium Ore" );
			}

			if( item.type == ItemID.ChlorophyteBar )
			{
				item.SetNameOverride( "Caesium Bar" );
			}

			if( item.type == ItemID.CobaltShield )
			{
				item.SetNameOverride( "Aqua Shield" );
			}
		}
		
		public override void MeleeEffects( Item item, Player player, Rectangle hitbox )
		{
			// TODO! - fix, it doesn't disable vanilla dust.
			if( item.type == ItemID.Muramasa )
			{
				if( Main.rand.Next( 3 ) == 0 )
				{
					int dust = Dust.NewDust( new Vector2( hitbox.X, hitbox.Y ), hitbox.Width, hitbox.Height, 59, (player.velocity.X * 0.2f) + (player.direction * 3), player.velocity.Y * 0.2f, 0, new Color( 0, 0, 1 ), 1f );
					Main.dust[dust].noGravity = true;
					return;
				}
			}
		}

		public override void ModifyTooltips( Item item, List<TooltipLine> tooltips )
		{
			if( item.type == ItemID.TungstenPickaxe ) // Remove 'can mine meteorite' tooltip (it can't now)
			{
				foreach( TooltipLine l in tooltips )
				{
					if( l.Name == "Tooltip0" )
					{
						tooltips.Remove( l );
					}
				}
				return;
			}

			//
			//   Coal
			//
			if( item.type == ItemID.Coal )
			{
				foreach( TooltipLine l in tooltips )
				{
					if( l.Name == "Tooltip0" )
					{
						l.text = "'Flammable!'";
					}
				}
				return;
			}
		}
	}
}