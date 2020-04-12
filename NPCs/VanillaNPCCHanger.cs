using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrariaReworked.NPCs
{
	public class VanillaNPCChanger : GlobalNPC
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

		public override bool PreNPCLoot( NPC npc )
		{
			if( npc.type == NPCID.Mothron )
			{
				if( NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3 )
				{
					if( Main.rand.Next( 20 ) == 0 && NPC.downedPlantBoss )
					{
						Item.NewItem( (int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.MothronWings, 1, false, -1, false, false );
					}
					/*if( Main.rand.Next( 4 ) == 0 )
					{
						Item.NewItem( (int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 1570, 1, false, -1, false, false );
					}
					else if( Main.rand.Next( 3 ) == 0 && NPC.downedPlantBoss )
					{
						Item.NewItem( (int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 3292, 1, false, -1, false, false );
					}*/
					if( Main.rand.Next( 4 ) != 0 ) // 1, 2, 3, not 4 (75%)
					{
						int rand = Main.rand.Next( 4 );
						if( rand == 0 )
						{
							Item.NewItem( (int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.BrokenHeroSword, 1, false, -1, false, false );
						}
						else if( rand == 1 )
						{
							Item.NewItem( (int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModMain.instance.ItemType("BrokenHeroSpear"), 1, false, -1, false, false );
						}
						else if( rand == 2 )
						{
							Item.NewItem( (int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModMain.instance.ItemType( "BrokenHeroPick" ), 1, false, -1, false, false );
						}
						else if( rand == 3 )
						{
							Item.NewItem( (int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModMain.instance.ItemType( "BrokenHeroAx" ), 1, false, -1, false, false );
						}
					}
					else if( Main.rand.Next( 2 ) == 0 && NPC.downedPlantBoss ) // otherwise, 50% chance. (combined chance is 75% for bhs, 12.5% for TEoC)
					{
						Item.NewItem( (int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.TheEyeOfCthulhu, 1, false, -1, false, false );
					}
				}
				return false;
			}
			return true;
		}
	}
}
