using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrariaReworked.NPCs
{
	public class BloodshotEye : ModNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault( "Bloodshot Eye" );
			Main.npcFrameCount[npc.type] = 2;//Main.npcFrameCount[NPCID.DemonEye];
		}

		public override void SetDefaults()
		{
			npc.width = 36;
			npc.height = 22;
			npc.damage = 25;
			npc.defense = 7;
			npc.lifeMax = 75;
			npc.HitSound = SoundID.NPCHit1;
			npc.DeathSound = SoundID.NPCDeath6;
			npc.value = 450f;
			npc.knockBackResist = 0.4f;
			npc.aiStyle = 2;
			aiType = NPCID.DemonEye;
			animationType = NPCID.DemonEye;
		}

		public override void NPCLoot()
		{
			if( Main.rand.Next( 34 ) == 0 ) Item.NewItem( (int)npc.position.X, (int)npc.position.Y, 38, 20, ItemID.BlackLens, 1, false );
			if( Main.rand.Next( 6 ) == 0 ) Item.NewItem( (int)npc.position.X, (int)npc.position.Y, 38, 20, mod.ItemType( "BloodshotLens" ), 1, false );
			
			Gore.NewGore( npc.position, npc.velocity, mod.GetGoreSlot( "Gores/BloodshotEyeGore1" ), 1f );
			Gore.NewGore( npc.position, npc.velocity, mod.GetGoreSlot( "Gores/BloodshotEyeGore2" ), 1f );
			Gore.NewGore( npc.position, npc.velocity, mod.GetGoreSlot( "Gores/BloodshotEyeGore3" ), 1f );
		}

		public override float SpawnChance( NPCSpawnInfo spawnInfo )
		{
			return Main.bloodMoon ? SpawnCondition.OverworldNightMonster.Chance * 0.25f : SpawnCondition.OverworldNightMonster.Chance * 0.005f;
		}
	}
}