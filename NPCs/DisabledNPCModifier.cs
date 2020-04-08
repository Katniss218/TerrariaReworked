using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrariaReworked.NPCs
{
	public class DisabledNPCModifier : GlobalNPC
	{
		public override bool PreAI( NPC npc )
		{
			// HOLY! every frame! but it's necessary. killing mob in SetDefaults doesn't do anything.
			for( int i = 0; i < ModMain.disabledNPCs.Length; i++ )
			{
				if( npc.type == ModMain.disabledNPCs[i] )
				{
					npc.life = 0;
					npc.active = false;
					return false;
				}
			}
			return true;
		}

		public override void SetupShop( int type, Chest shop, ref int nextSlot )
		{
			List<Item> items = shop.item.ToList();

			// item[i] == null is ALWAYS false for every item slot.
			// item[i].type is '0' when there's no item.
						
			int shopItemArrLength = shop.item.Length;
			int removedCount = items.RemoveAll( x => ModMain.disabledItems.Contains( x.type ) );
			int realItemCount = nextSlot - removedCount;
			Item item;
			for( int i = 0; i < removedCount; i++ )
			{
				item = new Item();
				item.SetDefaults( 0 );

				items.Add( item );
			}

			shop.item = items.ToArray();
			nextSlot = realItemCount;
		}
	}
}
