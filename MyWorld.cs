using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.GameContent.Biomes;
using Terraria.GameContent.Generation;
using Terraria.ID;
using Terraria.IO;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.World.Generation;

public enum EvilCombo : byte
{
	/// <summary>
	/// Corruption - Purity
	/// </summary>
	Corruption = 0,
	/// <summary>
	/// Crimson - Hallow
	/// </summary>
	Crimson = 1,
	/// <summary>
	/// Cessation - Persistence
	/// </summary>
	Cessation = 2
}

public enum WorldProgressionState : byte
{
	PreHardmode = 0,
	Hardmode = 1,
	Superhardmode = 2
}

namespace TerrariaReworked
{
	public class MyWorld : ModWorld
	{
		private const int saveVersion = 1;

		private const string WorldProgressionStateTagName = "WorldProgression";
		private const string EvilComboTagName = "EvilCombo";
		private const string DownedTagName = "Downed";
		private const string DownedTheRailTagName = "TheRail";
		private const string DownedGargouardianTagName = "Gargouardian";
		private const string DownedOblivionTagName = "Oblivion";
		private const string DownedJormungandrTagName = "Jormungandr";

		private static string[] HardmodeMessages = new string[]
		{
			"The ancient spirits of light and dark have been released!", // Corruption
			"The ancient spirits of peace and blood have been released!", // Crimson
			"The ancient spirits of life and death have been released!" // Cessation
		};

		private static string[] SuperhardmodeMessages = new string[]
		{
			"The ancient spirits of light and dark are now waging war with each other!", // Corruption
			"The ancient spirits of peace and blood are now waging war with each other!", // Crimson
			"The ancient spirits of life and death are now waging war with each other!" // Cessation
		};

		public static bool downedTheRail = false;
		public static bool downedGargouardian = false;
		public static bool downedOblivion = false;
		public static bool downedJormungandr = false;

		// internal state for superhardmode.
		static bool superHardMode = false;

		/// <summary>
		/// The current world progression state of the world (PHM, HM, SHM).
		/// </summary>
		public static WorldProgressionState worldProgressionState
		{
			get
			{
				if( superHardMode )
				{
					return WorldProgressionState.Superhardmode;
				}
				if( Main.hardMode )
				{
					return WorldProgressionState.Hardmode;
				}
				return WorldProgressionState.PreHardmode;
			}
			set
			{
				if( value == WorldProgressionState.PreHardmode )
				{
					superHardMode = false;
					Main.hardMode = false;
				}
				else if( value == WorldProgressionState.Hardmode )
				{
					superHardMode = false;
					Main.hardMode = true;
				}
				else if( value == WorldProgressionState.Superhardmode )
				{
					superHardMode = true;
					Main.hardMode = true;
				}
			}
		}

		/// <summary>
		/// The evil-good biome combo of the current world.
		/// </summary>
		public static EvilCombo evilCombo { get; set; }

		public override void Initialize()
		{
			//worldProgressionState = WorldProgressionState.PreHardmode;

			downedTheRail = false;
			downedGargouardian = false;
			downedOblivion = false;
			downedJormungandr = false;
		}

		// Various Ores/Bars sources:
		// - Worldgen (ore)
		// Chests (search every chest and replace it)
		// write my own generation code and completely replace the vanilla one???

		public override TagCompound Save()
		{
			List<string> downed = new List<string>();
			if( downedOblivion )
				downed.Add( DownedOblivionTagName );
			if( downedTheRail )
				downed.Add( DownedTheRailTagName );
			if( downedGargouardian )
				downed.Add( DownedGargouardianTagName );
			if( downedJormungandr )
				downed.Add( DownedJormungandrTagName );

			return new TagCompound
			{
				{ "SaveVersion", saveVersion },
				{ WorldProgressionStateTagName, (byte)worldProgressionState },
				{ EvilComboTagName, (byte)evilCombo },
				{ DownedTagName, downed }
			};
		}

		public override void Load( TagCompound tag )
		{
			int saveVersion = tag.GetInt( "SaveVersion" );
			if( saveVersion == 1 )
			{
				worldProgressionState = (WorldProgressionState)tag.GetByte( WorldProgressionStateTagName );
				evilCombo = (EvilCombo)tag.GetByte( EvilComboTagName );

				IList<string> downed = tag.GetList<string>( DownedTagName );

				downedOblivion = downed.Contains( DownedOblivionTagName );
				downedTheRail = downed.Contains( DownedTheRailTagName );
				downedGargouardian = downed.Contains( DownedGargouardianTagName );
				downedJormungandr = downed.Contains( DownedJormungandrTagName );
			}
			else
			{
				throw new Exception( "Invalid world save format!" );
			}
		}

		/// <summary>
		/// used to sync on the network.
		/// </summary>
		public override void NetSend( BinaryWriter writer )
		{
			BitsByte flags = new BitsByte(); // max 8 values
			flags[1] = downedOblivion;
			flags[2] = downedTheRail;
			flags[3] = downedGargouardian;
			flags[4] = downedJormungandr;
			writer.Write( flags );
			writer.Write( (byte)evilCombo );
			writer.Write( (byte)worldProgressionState );
		}

		// clear all the passes, add custom replacements (local fields be local fields on MyWorld)

		/// <summary>
		/// used to sync on the network.
		/// </summary>
		public override void NetReceive( BinaryReader reader )
		{
			BitsByte flags = reader.ReadByte();
			downedOblivion = flags[1];
			downedTheRail = flags[2];
			downedGargouardian = flags[3];
			downedJormungandr = flags[4];
			worldProgressionState = (WorldProgressionState)reader.ReadByte();
			evilCombo = (EvilCombo)reader.ReadByte();
		}

		public static void InitiateSuperhardmode( int x, int y )
		{
			// sync world
			if( worldProgressionState == WorldProgressionState.Superhardmode )
			{
				throw new Exception( "InitiateSuperHardmode called on a superhardmode world" );
			}

			if( Main.netMode == 0 )
			{
				Main.NewText( SuperhardmodeMessages[(byte)evilCombo], 255, 60, 0 );
			}
			else if( Main.netMode == 2 )
			{
				NetworkText text = NetworkText.FromLiteral( SuperhardmodeMessages[(byte)evilCombo] );
				NetMessage.SendData( 25, -1, -1, text, 255, 255f, 60f, 0f, 0 ); // chat
			}

			worldProgressionState = WorldProgressionState.Superhardmode;

			SuperhardmodeRunner();
		}

		/// <summary>
		/// Runs the passes for SHM.
		/// </summary>
		private static void SuperhardmodeRunner()
		{
			GenerationProgress progress = new GenerationProgress();
			List<GenPass> shmPasses = new List<GenPass>();
			ModifySuperhardmodeTasks( shmPasses );
			foreach( GenPass genPass2 in shmPasses )
			{
				genPass2.Apply( progress );
			}
		}


		/// <summary>
		/// Is called to setup the Hardmode worldgen passes.
		/// </summary>
		public override void ModifyHardmodeTasks( List<GenPass> list )
		{
			/*for( int i = 0; i < list.Count; i++ )
			{
				Main.NewText( list[i].Name );
			}*/
			// Hardmode Good
			// Hardmode Evil
			// Hardmode Walls
			// Hardmode Announcement

			list.Clear();

			// disables the message.
			// disables generation of the hallow.

			// add custom passes...
		}

		/// <summary>
		/// Is called to setup the Superhardmode worldgen passes.
		/// </summary>
		public static void ModifySuperhardmodeTasks( List<GenPass> list )
		{
			list.Clear();

			// add passes.
		}

		public override void ModifyWorldGenTasks( List<GenPass> tasks, ref float totalWeight )
		{
			WorldGenPasses.GetDefaultWorldGenPasses( tasks, ref totalWeight );
		}


	}
}