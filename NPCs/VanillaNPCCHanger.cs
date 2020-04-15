using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
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

		// ported banner dropping code (needed to keep dropping banners while blocking other npc loot).
		public static void BANNER( NPC npc )
		{
			int num2 = Item.NPCtoBanner( npc.BannerID() );
			if( num2 > 0 && !NPCID.Sets.ExcludedFromDeathTally[npc.type] )
			{
				bool flag2;
				if( npc.realLife >= 0 )
				{
					flag2 = Main.npc[npc.realLife].AnyInteractions();
				}
				else
				{
					flag2 = npc.AnyInteractions();
				}
				if( flag2 )
				{
					NPC.killCount[num2]++;
					if( Main.netMode == 2 )
					{
						NetMessage.SendData( 83, -1, -1, null, num2, 0f, 0f, 0f, 0, 0, 0 );
					}
					int num3 = ItemID.Sets.KillsToBanner[Item.BannerToItem( num2 )];
					if( NPC.killCount[num2] % num3 == 0 && num2 > 0 )
					{
						int num4 = Item.BannerToNPC( num2 );
						new NPC().SetDefaults( num4, -1f );
						int num5 = npc.lastInteraction;
						if( !Main.player[num5].active || Main.player[num5].dead )
						{
							num5 = npc.FindClosestPlayer();
						}
						NetworkText networkText = NetworkText.FromKey( "Game.EnemiesDefeatedAnnouncement", new object[]
						{
					NPC.killCount[num2],
					NetworkText.FromKey(Lang.GetNPCName(num4).Key, new object[0])
						} );
						if( num5 >= 0 && num5 < 255 )
						{
							networkText = NetworkText.FromKey( "Game.EnemiesDefeatedByAnnouncement", new object[]
							{
						Main.player[num5].name,
						NPC.killCount[num2],
						NetworkText.FromKey(Lang.GetNPCName(num4).Key, new object[0])
							} );
						}
						if( Main.netMode == 0 )
						{
							Main.NewText( networkText.ToString(), 250, 250, 0, false );
						}
						else if( Main.netMode == 2 )
						{
							NetMessage.BroadcastChatMessage( networkText, new Color( 250, 250, 0 ), -1 );
						}
						int num6 = Item.BannerToItem( num2 );
						Vector2 position = npc.position;
						if( num5 >= 0 && num5 < 255 )
						{
							position = Main.player[num5].position;
						}
						Item.NewItem( (int)position.X, (int)position.Y, npc.width, npc.height, num6, 1, false, 0, false, false );
					}
				}
			}
		}

		public override bool PreNPCLoot( NPC npc )
		{
			if( npc.type == NPCID.GoblinSorcerer )
			{
				if( Main.rand.Next( 15 ) == 0 )
				{
					Item.NewItem( (int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType( "ChaosTome" ), 1, false, 0, false, false );
				}
				if( Main.rand.Next( 2 ) == 0 )
				{
					int amt = Main.rand.Next( 1, 6 );
					Item.NewItem( (int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.SpikyBall, amt, false, 0, false, false );
				}

				BANNER( npc );

				return false;
			}
			if( npc.type == NPCID.Mothron )
			{
				if( NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3 )
				{
					if( Main.rand.Next( 20 ) == 0 && NPC.downedPlantBoss )
					{
						Item.NewItem( (int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.MothronWings, 1, false, -1, false, false );
					}
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

				BANNER( npc );

				return false;
			}
			return true;
		}
	}
}
