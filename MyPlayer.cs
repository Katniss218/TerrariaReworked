using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrariaReworked
{
	// ModPlayer classes provide a way to attach data to Players and act on that data. ExamplePlayer has a lot of functionality related to 
	// several effects and items in ExampleMod. See SimpleModPlayer for a very simple example of how ModPlayer classes work.
	public class MyPlayer : ModPlayer
	{
		public override bool PreKill( double damage, int hitDirection, bool pvp, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource )
		{
			if( player.HasBuff( mod.BuffType( "Oblivion" ) ) )
			{
				damageSource = PlayerDeathReason.ByCustomReason( base.player.name + " disappeared into oblivion." );
			}

			return true;
		}

		public override void SetupStartInventory( IList<Item> items, bool mediumcoreDeath )
		{
			items.Clear();

			Item item;
			item = new Item();
			item.SetDefaults( ItemID.WoodenSword );
			items.Add( item );

			item = new Item();
			item.SetDefaults( mod.ItemType( "WoodenPickaxe" ) );
			items.Add( item );

			item = new Item();
			item.SetDefaults( mod.ItemType( "WoodenAxe" ) );
			items.Add( item );

			item = new Item();
			item.SetDefaults( ItemID.Gel );
			item.stack = Main.rand.Next( 4, 7 ); // 4-6 gel
			items.Add( item );

			item = new Item();
			item.SetDefaults( ItemID.LesserHealingPotion );
			item.stack = 2; // 2 lesser healing potions
			items.Add( item );
		}
	}
}
