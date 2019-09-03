using System;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.GameContent.Generation;
using Terraria.ID;
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
		public static EvilCombo evilCombo { get; private set; }

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
				throw new System.Exception( "Invalid world save format!" );
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
				throw new System.Exception( "InitiateSuperHardmode called on a superhardmode world" );
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
			MyWorldUtils.RemoveWorldGenTasks( ref tasks, "Marble", "Granite", "Spider Caves", "Jungle Temple" );
			
			tasks.Insert( 1, new PassLegacy( "Post Reset", PostResetFunc ) );
			
			MyWorldUtils.ReplaceWorldGenTask( ref tasks, "Shinies", new PassLegacy( "Shinies", ShiniesFunc ) );

			tasks.Add( new PassLegacy( "Hellcastle", HellcastleFunc ) );
			tasks.Add( new PassLegacy( "Dungeon Chests", DungeonChestsFunc ) );
			tasks.Add( new PassLegacy( "Finishing Up", FinishingUpFunc ) );
		}
		/*
		private void CorruptionFunc( GenerationProgress progress )
		{
			int i2;
			int dungeonSide = Math.Sign( WorldGen.dungeonX );
			if( WorldGen.crimson )
			{
				progress.Message = Lang.gen[72].Value;
				int num = 0;
				while( (double)num < (double)Main.maxTilesX * 0.00045 )
				{
					float value = (float)((double)num / ((double)Main.maxTilesX * 0.00045));
					progress.Set( value );
					bool flag2 = false;
					int num2 = 0;
					int num3 = 0;
					int num4 = 0;
					while( !flag2 )
					{
						int num5 = 0;
						flag2 = true;
						int num6 = Main.maxTilesX / 2;
						int num7 = 200;
						if( dungeonSide < 0 )
						{
							num2 = WorldGen.genRand.Next( 600, Main.maxTilesX - 320 );
						}
						else
						{
							num2 = WorldGen.genRand.Next( 320, Main.maxTilesX - 600 );
						}
						num3 = num2 - WorldGen.genRand.Next( 200 ) - 100;
						num4 = num2 + WorldGen.genRand.Next( 200 ) + 100;
						if( num3 < 285 )
						{
							num3 = 285;
						}
						if( num4 > Main.maxTilesX - 285 )
						{
							num4 = Main.maxTilesX - 285;
						}
						if( dungeonSide < 0 && num3 < 400 )
						{
							num3 = 400;
						}
						else if( dungeonSide > 0 && num3 > Main.maxTilesX - 400 )
						{
							num3 = Main.maxTilesX - 400;
						}
						if( num2 > num6 - num7 && num2 < num6 + num7 )
						{
							flag2 = false;
						}
						if( num3 > num6 - num7 && num3 < num6 + num7 )
						{
							flag2 = false;
						}
						if( num4 > num6 - num7 && num4 < num6 + num7 )
						{
							flag2 = false;
						}
						if( num2 > WorldGen.UndergroundDesertLocation.X && num2 < WorldGen.UndergroundDesertLocation.X + WorldGen.UndergroundDesertLocation.Width )
						{
							flag2 = false;
						}
						if( num3 > WorldGen.UndergroundDesertLocation.X && num3 < WorldGen.UndergroundDesertLocation.X + WorldGen.UndergroundDesertLocation.Width )
						{
							flag2 = false;
						}
						if( num4 > WorldGen.UndergroundDesertLocation.X && num4 < WorldGen.UndergroundDesertLocation.X + WorldGen.UndergroundDesertLocation.Width )
						{
							flag2 = false;
						}
						for( int k = num3; k < num4; k++ )
						{
							for( int l = 0; l < (int)Main.worldSurface; l += 5 )
							{
								if( Main.tile[k, l].active() && Main.tileDungeon[(int)Main.tile[k, l].type] )
								{
									flag2 = false;
									break;
								}
								if( !flag2 )
								{
									break;
								}
							}
						}
						if( num5 < 200 && WorldGen.JungleX > num3 && WorldGen.JungleX < num4 )
						{
							num5++;
							flag2 = false;
						}
					}
					WorldGen.CrimStart( num2, (int)WorldGen.worldSurfaceLow - 10 );
					for( int m = num3; m < num4; m++ )
					{
						int num8 = (int)WorldGen.worldSurfaceLow;
						while( (double)num8 < Main.worldSurface - 1.0 )
						{
							if( Main.tile[m, num8].active() )
							{
								int num9 = num8 + WorldGen.genRand.Next( 10, 14 );
								for( int n = num8; n < num9; n++ )
								{
									if( (Main.tile[m, n].type == 59 || Main.tile[m, n].type == 60) && m >= num3 + WorldGen.genRand.Next( 5 ) && m < num4 - WorldGen.genRand.Next( 5 ) )
									{
										Main.tile[m, n].type = 0;
									}
								}
								break;
							}
							num8++;
						}
					}
					double num10 = Main.worldSurface + 40.0;
					for( int num11 = num3; num11 < num4; num11++ )
					{
						num10 += (double)WorldGen.genRand.Next( -2, 3 );
						if( num10 < Main.worldSurface + 30.0 )
						{
							num10 = Main.worldSurface + 30.0;
						}
						if( num10 > Main.worldSurface + 50.0 )
						{
							num10 = Main.worldSurface + 50.0;
						}
						i2 = num11;
						bool flag3 = false;
						int num12 = (int)WorldGen.worldSurfaceLow;
						while( (double)num12 < num10 )
						{
							if( Main.tile[i2, num12].active() )
							{
								if( Main.tile[i2, num12].type == 53 && i2 >= num3 + WorldGen.genRand.Next( 5 ) && i2 <= num4 - WorldGen.genRand.Next( 5 ) )
								{
									Main.tile[i2, num12].type = 234;
								}
								if( Main.tile[i2, num12].type == 0 && (double)num12 < Main.worldSurface - 1.0 && !flag3 )
								{
									WorldGen.grassSpread = 0;
									WorldGen.SpreadGrass( i2, num12, 0, 199, true, 0 );
								}
								flag3 = true;
								if( Main.tile[i2, num12].wall == 216 )
								{
									Main.tile[i2, num12].wall = 218;
								}
								else if( Main.tile[i2, num12].wall == 187 )
								{
									Main.tile[i2, num12].wall = 221;
								}
								if( Main.tile[i2, num12].type == 1 )
								{
									if( i2 >= num3 + WorldGen.genRand.Next( 5 ) && i2 <= num4 - WorldGen.genRand.Next( 5 ) )
									{
										Main.tile[i2, num12].type = 203;
									}
								}
								else if( Main.tile[i2, num12].type == 2 )
								{
									Main.tile[i2, num12].type = 199;
								}
								else if( Main.tile[i2, num12].type == 161 )
								{
									Main.tile[i2, num12].type = 200;
								}
								else if( Main.tile[i2, num12].type == 396 )
								{
									Main.tile[i2, num12].type = 401;
								}
								else if( Main.tile[i2, num12].type == 397 )
								{
									Main.tile[i2, num12].type = 399;
								}
							}
							num12++;
						}
					}
					int num13 = WorldGen.genRand.Next( 10, 15 );
					for( int num14 = 0; num14 < num13; num14++ )
					{
						int num15 = 0;
						bool flag4 = false;
						int num16 = 0;
						while( !flag4 )
						{
							num15++;
							int num17 = WorldGen.genRand.Next( num3 - num16, num4 + num16 );
							int num18 = WorldGen.genRand.Next( (int)(Main.worldSurface - (double)(num16 / 2)), (int)(Main.worldSurface + 100.0 + (double)num16) );
							if( num15 > 100 )
							{
								num16++;
								num15 = 0;
							}
							if( !Main.tile[num17, num18].active() )
							{
								while( !Main.tile[num17, num18].active() )
								{
									num18++;
								}
								num18--;
							}
							else
							{
								while( Main.tile[num17, num18].active() && (double)num18 > Main.worldSurface )
								{
									num18--;
								}
							}
							if( num16 > 10 || (Main.tile[num17, num18 + 1].active() && Main.tile[num17, num18 + 1].type == 203) )
							{
								WorldGen.Place3x2( num17, num18, 26, 1 );
								if( Main.tile[num17, num18].type == 26 )
								{
									flag4 = true;
								}
							}
							if( num16 > 100 )
							{
								flag4 = true;
							}
						}
					}
					num++;
				}
				return;
			}
			progress.Message = Lang.gen[20].Value;
			int num19 = 0;
			while( (double)num19 < (double)Main.maxTilesX * 0.00045 )
			{
				float value2 = (float)((double)num19 / ((double)Main.maxTilesX * 0.00045));
				progress.Set( value2 );
				bool flag5 = false;
				int num20 = 0;
				int num21 = 0;
				int num22 = 0;
				while( !flag5 )
				{
					int num23 = 0;
					flag5 = true;
					int num24 = Main.maxTilesX / 2;
					int num25 = 200;
					num20 = WorldGen.genRand.Next( 320, Main.maxTilesX - 320 );
					num21 = num20 - WorldGen.genRand.Next( 200 ) - 100;
					num22 = num20 + WorldGen.genRand.Next( 200 ) + 100;
					if( num21 < 285 )
					{
						num21 = 285;
					}
					if( num22 > Main.maxTilesX - 285 )
					{
						num22 = Main.maxTilesX - 285;
					}
					if( num20 > num24 - num25 && num20 < num24 + num25 )
					{
						flag5 = false;
					}
					if( num21 > num24 - num25 && num21 < num24 + num25 )
					{
						flag5 = false;
					}
					if( num22 > num24 - num25 && num22 < num24 + num25 )
					{
						flag5 = false;
					}
					if( num20 > WorldGen.UndergroundDesertLocation.X && num20 < WorldGen.UndergroundDesertLocation.X + WorldGen.UndergroundDesertLocation.Width )
					{
						flag5 = false;
					}
					if( num21 > WorldGen.UndergroundDesertLocation.X && num21 < WorldGen.UndergroundDesertLocation.X + WorldGen.UndergroundDesertLocation.Width )
					{
						flag5 = false;
					}
					if( num22 > WorldGen.UndergroundDesertLocation.X && num22 < WorldGen.UndergroundDesertLocation.X + WorldGen.UndergroundDesertLocation.Width )
					{
						flag5 = false;
					}
					for( int num26 = num21; num26 < num22; num26++ )
					{
						for( int num27 = 0; num27 < (int)Main.worldSurface; num27 += 5 )
						{
							if( Main.tile[num26, num27].active() && Main.tileDungeon[(int)Main.tile[num26, num27].type] )
							{
								flag5 = false;
								break;
							}
							if( !flag5 )
							{
								break;
							}
						}
					}
					if( num23 < 200 && WorldGen.JungleX > num21 && WorldGen.JungleX < num22 )
					{
						num23++;
						flag5 = false;
					}
				}
				int num28 = 0;
				for( int num29 = num21; num29 < num22; num29++ )
				{
					if( num28 > 0 )
					{
						num28--;
					}
					if( num29 == num20 || num28 == 0 )
					{
						int num30 = (int)WorldGen.worldSurfaceLow;
						while( (double)num30 < Main.worldSurface - 1.0 )
						{
							if( Main.tile[num29, num30].active() || Main.tile[num29, num30].wall > 0 )
							{
								if( num29 == num20 )
								{
									num28 = 20;
									WorldGen.ChasmRunner( num29, num30, WorldGen.genRand.Next( 150 ) + 150, true );
									break;
								}
								if( WorldGen.genRand.Next( 35 ) == 0 && num28 == 0 )
								{
									num28 = 30;
									bool makeOrb = true;
									WorldGen.ChasmRunner( num29, num30, WorldGen.genRand.Next( 50 ) + 50, makeOrb );
									break;
								}
								break;
							}
							else
							{
								num30++;
							}
						}
					}
					int num31 = (int)WorldGen.worldSurfaceLow;
					while( (double)num31 < Main.worldSurface - 1.0 )
					{
						if( Main.tile[num29, num31].active() )
						{
							int num32 = num31 + WorldGen.genRand.Next( 10, 14 );
							for( int num33 = num31; num33 < num32; num33++ )
							{
								if( (Main.tile[num29, num33].type == 59 || Main.tile[num29, num33].type == 60) && num29 >= num21 + WorldGen.genRand.Next( 5 ) && num29 < num22 - WorldGen.genRand.Next( 5 ) )
								{
									Main.tile[num29, num33].type = 0;
								}
							}
							break;
						}
						num31++;
					}
				}
				double num34 = Main.worldSurface + 40.0;
				for( int num35 = num21; num35 < num22; num35++ )
				{
					num34 += (double)WorldGen.genRand.Next( -2, 3 );
					if( num34 < Main.worldSurface + 30.0 )
					{
						num34 = Main.worldSurface + 30.0;
					}
					if( num34 > Main.worldSurface + 50.0 )
					{
						num34 = Main.worldSurface + 50.0;
					}
					i2 = num35;
					bool flag6 = false;
					int num36 = (int)WorldGen.worldSurfaceLow;
					while( (double)num36 < num34 )
					{
						if( Main.tile[i2, num36].active() )
						{
							if( Main.tile[i2, num36].type == 53 && i2 >= num21 + WorldGen.genRand.Next( 5 ) && i2 <= num22 - WorldGen.genRand.Next( 5 ) )
							{
								Main.tile[i2, num36].type = 112;
							}
							if( Main.tile[i2, num36].type == 0 && (double)num36 < Main.worldSurface - 1.0 && !flag6 )
							{
								WorldGen.grassSpread = 0;
								WorldGen.SpreadGrass( i2, num36, 0, 23, true, 0 );
							}
							flag6 = true;
							if( Main.tile[i2, num36].type == 1 && i2 >= num21 + WorldGen.genRand.Next( 5 ) && i2 <= num22 - WorldGen.genRand.Next( 5 ) )
							{
								Main.tile[i2, num36].type = 25;
							}
							if( Main.tile[i2, num36].wall == 216 )
							{
								Main.tile[i2, num36].wall = 217;
							}
							else if( Main.tile[i2, num36].wall == 187 )
							{
								Main.tile[i2, num36].wall = 220;
							}
							if( Main.tile[i2, num36].type == 2 )
							{
								Main.tile[i2, num36].type = 23;
							}
							if( Main.tile[i2, num36].type == 161 )
							{
								Main.tile[i2, num36].type = 163;
							}
							else if( Main.tile[i2, num36].type == 396 )
							{
								Main.tile[i2, num36].type = 400;
							}
							else if( Main.tile[i2, num36].type == 397 )
							{
								Main.tile[i2, num36].type = 398;
							}
						}
						num36++;
					}
				}
				for( int num37 = num21; num37 < num22; num37++ )
				{
					for( int num38 = 0; num38 < Main.maxTilesY - 50; num38++ )
					{
						if( Main.tile[num37, num38].active() && Main.tile[num37, num38].type == 31 )
						{
							int num39 = num37 - 13;
							int num40 = num37 + 13;
							int num41 = num38 - 13;
							int num42 = num38 + 13;
							for( int num43 = num39; num43 < num40; num43++ )
							{
								if( num43 > 10 && num43 < Main.maxTilesX - 10 )
								{
									for( int num44 = num41; num44 < num42; num44++ )
									{
										if( Math.Abs( num43 - num37 ) + Math.Abs( num44 - num38 ) < 9 + WorldGen.genRand.Next( 11 ) && WorldGen.genRand.Next( 3 ) != 0 && Main.tile[num43, num44].type != 31 )
										{
											Main.tile[num43, num44].active( true );
											Main.tile[num43, num44].type = 25;
											if( Math.Abs( num43 - num37 ) <= 1 && Math.Abs( num44 - num38 ) <= 1 )
											{
												Main.tile[num43, num44].active( false );
											}
										}
										if( Main.tile[num43, num44].type != 31 && Math.Abs( num43 - num37 ) <= 2 + WorldGen.genRand.Next( 3 ) && Math.Abs( num44 - num38 ) <= 2 + WorldGen.genRand.Next( 3 ) )
										{
											Main.tile[num43, num44].active( false );
										}
									}
								}
							}
						}
					}
				}
				num19++;
			}
		}
		*/
		private void DungeonChestsFunc( GenerationProgress progress )
		{
			progress.Message = "Dungeon Chests";
			Tile tile;
			Item item;
			for( int i = 0; i < Main.chest.Length; i++ )
			{
				if( Main.chest[i] == null )
					continue;
				tile = Main.tile[Main.chest[i].x, Main.chest[i].y];
				if( tile.type == TileID.Containers && tile.frameX == 72 )
				{
					item = new Item();
					int random = WorldGen.genRand.Next( 0, 8 );
					switch( random )
					{
						case 0:
							item.SetDefaults( ItemID.Muramasa );
							break;
						case 1:
							item.SetDefaults( mod.ItemType( "SapphirePickaxe" ) );
							break;
						case 2:
							item.SetDefaults( mod.ItemType( "Blueshift" ) );
							break;
						case 3:
							item.SetDefaults( ItemID.BlueMoon );
							break;
						case 4:
							item.SetDefaults( ItemID.CobaltShield );
							break;
						case 5:
							item.SetDefaults( ItemID.MagicMissile );
							break;
						case 6:
							item.SetDefaults( ItemID.AquaScepter );
							break;
						case 7:
							item.SetDefaults( ItemID.ShadowKey );
							break;
					}
					item.stack = 1;
					Main.chest[i].item[0] = item;
				}
			}
		}


		/*
		private void OverrideResetFunc (GenerationProgress progress )
		{
			progress.Message = "Overriding Reset";
			/*Liquid.ReInit();
			WorldGen.noTileActions = true;
			progress.Message = "";
			WorldGen.SetupStatueList();
			WorldGen.RandomizeWeather();
			Main.cloudAlpha = 0.0f;
			Main.maxRaining = 0.0f;
			WorldFile.tempMaxRain = 0.0f;
			Main.raining = false;
			WorldGen.heartCount = 0;
			Main.checkXMas();
			Main.checkHalloween();
			WorldGen.gen = true;
			WorldGen.ResetGenerator();
			WorldGen.numLarva = 0;
			int num = 86400;
			Main.slimeRainTime = (double)-WorldGen.genRand.Next( num * 2, num * 3 );
			Main.cloudBGActive = (float)-WorldGen.genRand.Next( 8640, 86400 );
			WorldGen.CopperTierOre = (ushort)7;
			WorldGen.IronTierOre = (ushort)6;
			WorldGen.SilverTierOre = (ushort)9;
			WorldGen.GoldTierOre = (ushort)8;
			WorldGen.copperBar = 20;
			WorldGen.ironBar = 22;
			WorldGen.silverBar = 21;
			WorldGen.goldBar = 19;
			if( WorldGen.genRand.Next( 2 ) == 0 )
			{
				copper = 166;
				WorldGen.copperBar = 703;
				WorldGen.CopperTierOre = (ushort)166;
			}
			if( WorldGen.genRand.Next( 2 ) == 0 )
			{
				iron = 167;
				WorldGen.ironBar = 704;
				WorldGen.IronTierOre = (ushort)167;
			}
			if( WorldGen.genRand.Next( 2 ) == 0 )
			{
				silver = 168;
				WorldGen.silverBar = 705;
				WorldGen.SilverTierOre = (ushort)168;
			}
			if( WorldGen.genRand.Next( 2 ) == 0 )
			{
				gold = 169;
				WorldGen.goldBar = 706;
				WorldGen.GoldTierOre = (ushort)169;
			}/
			//WorldGen.crimson = WorldGen.genRand.Next( 2 ) == 0;
			//if( WorldGen.WorldGenParam_Evil == 0 )
			WorldGen.crimson = false;
			/*if( WorldGen.WorldGenParam_Evil == 1 )
				WorldGen.crimson = true;
			switch( jungleHut )
			{
				case 0:
					WorldGen.jungleHut = (ushort)119;
					break;
				case 1:
					jungleHut = (ushort)120;
					break;
				case 2:
					jungleHut = (ushort)158;
					break;
				case 3:
					jungleHut = (ushort)175;
					break;
				case 4:
					jungleHut = (ushort)45;
					break;
			}
			Main.worldID = WorldGen.genRand.Next( int.MaxValue );
			WorldGen.RandomizeTreeStyle();
			WorldGen.RandomizeCaveBackgrounds();
			WorldGen.RandomizeBackgrounds();
			WorldGen.RandomizeMoonState();
			dungeonSide = WorldGen.genRand.Next( 2 ) == 0 ? -1 : 1;/
		}
		*/

		private void ShiniesFunc( GenerationProgress progress )
		{
			// WorldGen.worldSurfaceLow is actually the highest surface tile. In practice you might want to use WorldGen.rockLayer or other WorldGen values.
			progress.Message = Lang.gen[16].Value;

			// COAL

			for( int i = 0; i < (int)((Main.maxTilesX * Main.maxTilesY) * 0.00003); i++ )
				WorldGen.TileRunner( WorldGen.genRand.Next( 0, Main.maxTilesX ), WorldGen.genRand.Next( (int)WorldGen.worldSurfaceLow, Main.maxTilesY / 2 ), WorldGen.genRand.Next( 6, 8 ), WorldGen.genRand.Next( 20, 32 ), mod.TileType( "Coal" ) );

			// COPPER

			for( int index = 0; index < (int)((Main.maxTilesX * Main.maxTilesY) * 0.00006); ++index )
				WorldGen.TileRunner( WorldGen.genRand.Next( 0, Main.maxTilesX ), WorldGen.genRand.Next( (int)WorldGen.worldSurfaceLow, (int)WorldGen.worldSurfaceHigh ), WorldGen.genRand.Next( 3, 6 ), WorldGen.genRand.Next( 2, 6 ), WorldGen.CopperTierOre );

			for( int index = 0; index < (int)((Main.maxTilesX * Main.maxTilesY) * 0.00008); ++index )
				WorldGen.TileRunner( WorldGen.genRand.Next( 0, Main.maxTilesX ), WorldGen.genRand.Next( (int)WorldGen.worldSurfaceHigh, (int)WorldGen.rockLayerHigh ), WorldGen.genRand.Next( 3, 7 ), WorldGen.genRand.Next( 3, 7 ), WorldGen.CopperTierOre );

			for( int index = 0; index < (int)((Main.maxTilesX * Main.maxTilesY) * 0.0002); ++index )
				WorldGen.TileRunner( WorldGen.genRand.Next( 0, Main.maxTilesX ), WorldGen.genRand.Next( (int)WorldGen.rockLayerLow, Main.maxTilesY ), WorldGen.genRand.Next( 4, 9 ), WorldGen.genRand.Next( 4, 8 ), WorldGen.CopperTierOre );

			// IRON

			for( int index = 0; index < (int)((Main.maxTilesX * Main.maxTilesY) * 0.00003); ++index )
				WorldGen.TileRunner( WorldGen.genRand.Next( 0, Main.maxTilesX ), WorldGen.genRand.Next( (int)WorldGen.worldSurfaceLow, (int)WorldGen.worldSurfaceHigh ), WorldGen.genRand.Next( 3, 7 ), WorldGen.genRand.Next( 2, 5 ), WorldGen.IronTierOre );

			for( int index = 0; index < (int)((Main.maxTilesX * Main.maxTilesY) * 0.00008); ++index )
				WorldGen.TileRunner( WorldGen.genRand.Next( 0, Main.maxTilesX ), WorldGen.genRand.Next( (int)WorldGen.worldSurfaceHigh, (int)WorldGen.rockLayerHigh ), WorldGen.genRand.Next( 3, 6 ), WorldGen.genRand.Next( 3, 6 ), WorldGen.IronTierOre );

			for( int index = 0; index < (int)((Main.maxTilesX * Main.maxTilesY) * 0.0002); ++index )
				WorldGen.TileRunner( WorldGen.genRand.Next( 0, Main.maxTilesX ), WorldGen.genRand.Next( (int)WorldGen.rockLayerLow, Main.maxTilesY ), WorldGen.genRand.Next( 4, 9 ), WorldGen.genRand.Next( 4, 8 ), WorldGen.IronTierOre );

			// SILVER

			for( int index = 0; index < (int)((Main.maxTilesX * Main.maxTilesY) * 0.000026); ++index )
				WorldGen.TileRunner( WorldGen.genRand.Next( 0, Main.maxTilesX ), WorldGen.genRand.Next( (int)WorldGen.worldSurfaceHigh, (int)WorldGen.rockLayerHigh ), WorldGen.genRand.Next( 3, 6 ), WorldGen.genRand.Next( 3, 6 ), WorldGen.SilverTierOre );

			for( int index = 0; index < (int)((Main.maxTilesX * Main.maxTilesY) * 0.00015); ++index )
				WorldGen.TileRunner( WorldGen.genRand.Next( 0, Main.maxTilesX ), WorldGen.genRand.Next( (int)WorldGen.rockLayerLow, Main.maxTilesY ), WorldGen.genRand.Next( 4, 9 ), WorldGen.genRand.Next( 4, 8 ), WorldGen.SilverTierOre );

			for( int index = 0; index < (int)((Main.maxTilesX * Main.maxTilesY) * 0.00017); ++index )
				WorldGen.TileRunner( WorldGen.genRand.Next( 0, Main.maxTilesX ), WorldGen.genRand.Next( 0, (int)WorldGen.worldSurfaceLow ), WorldGen.genRand.Next( 4, 9 ), WorldGen.genRand.Next( 4, 8 ), WorldGen.SilverTierOre );

			// GOLD

			for( int index = 0; index < (int)((Main.maxTilesX * Main.maxTilesY) * 0.00012); ++index )
				WorldGen.TileRunner( WorldGen.genRand.Next( 0, Main.maxTilesX ), WorldGen.genRand.Next( (int)WorldGen.rockLayerLow, Main.maxTilesY ), WorldGen.genRand.Next( 4, 8 ), WorldGen.genRand.Next( 4, 8 ), WorldGen.GoldTierOre );

			for( int index = 0; index < (int)((Main.maxTilesX * Main.maxTilesY) * 0.00012); ++index )
				WorldGen.TileRunner( WorldGen.genRand.Next( 0, Main.maxTilesX ), WorldGen.genRand.Next( 0, (int)WorldGen.worldSurfaceLow - 20 ), WorldGen.genRand.Next( 4, 8 ), WorldGen.genRand.Next( 4, 8 ), WorldGen.GoldTierOre );


			if( WorldGen.crimson )
			{
				for( int index = 0; index < (int)((Main.maxTilesX * Main.maxTilesY) * 0.00002); ++index )
					WorldGen.TileRunner( WorldGen.genRand.Next( 0, Main.maxTilesX ), WorldGen.genRand.Next( (int)WorldGen.rockLayerLow, Main.maxTilesY ), WorldGen.genRand.Next( 2, 4 ), WorldGen.genRand.Next( 3, 6 ), 204, false, 0.0f, 0.0f, false, true );
			}
			else
			{
				for( int index = 0; index < (int)((Main.maxTilesX * Main.maxTilesY) * 0.00002); ++index )
					WorldGen.TileRunner( WorldGen.genRand.Next( 0, Main.maxTilesX ), WorldGen.genRand.Next( (int)WorldGen.rockLayerLow, Main.maxTilesY ), WorldGen.genRand.Next( 2, 4 ), WorldGen.genRand.Next( 3, 6 ), 22, false, 0.0f, 0.0f, false, true );
			}
		}

		private void PostResetFunc( GenerationProgress progress )
		{
			progress.Message = "Post Reset";

			MyWorld.worldProgressionState = WorldProgressionState.PreHardmode;
		}

		private void FinishingUpFunc( GenerationProgress progress )
		{
			progress.Message = "Finishing Up...";

			if( WorldGen.crimson )
			{
				evilCombo = EvilCombo.Crimson;
			}
			else
			{
				evilCombo = EvilCombo.Corruption;
			}
		}

		private void HellcastleFunc( GenerationProgress progress )
		{
			progress.Message = "Generating Hellcastle...";

			Action<int, int> generateUpperLootRooms = ( int posX, int posY ) =>
			{
				int w = 10;
				int h = 10;
				int w2 = w / 2;
				int h2 = h / 2;

				for( int i = posX - w2; i < posX + w2; i++ )
				{
					for( int j = posY - h2; j < posY + h2; j++ )
					{
						WorldGen.PlaceTile( i, j, mod.TileType( "ImperviousBrick" ), false, true );
					}
				}
				for( int i = posX - w2 + 4; i < posX + w2 - 4; i++ )
				{
					for( int j = posY - h2 + 4; j < posY + h2 - 4; j++ )
					{
						WorldGen.PlaceWall( i, j, WallID.ObsidianBrickUnsafe );
						WorldGen.KillTile( i, j );
					}
				}
			};

			int x = Main.dungeonX <= (Main.maxTilesX / 2) ? (Main.maxTilesX / 4) * 3 : (Main.maxTilesX / 4) * 1;
			int y = Main.maxTilesY - 155;
			int width = 100;
			int height = 60;
			int width2 = width / 2;
			int height2 = height / 2;
			for( int i = x - width2; i < x + width2; i++ )
			{
				for( int j = y - height2; j < y + height2; j++ )
				{
					WorldGen.PlaceTile( i, j, mod.TileType( "ImperviousBrick" ), false, true );
				}
			}
			for( int i = x - width2 + 4; i < x + width2 - 4; i++ )
			{
				for( int j = y - height2 + 4; j < y + height2 - 4; j++ )
				{
					WorldGen.PlaceWall( i, j, WallID.ObsidianBrickUnsafe );
					WorldGen.KillTile( i, j );
				}
			}

			for( int i = x - width2; i < x - width2 + 4; i++ )
			{
				for( int j = y - 2; j < y + 2; j++ )
				{
					WorldGen.PlaceTile( i, j, mod.TileType( "ResistantWood" ), false, true );
				}
			}
			for( int i = x + width2 - 4; i < x + width2; i++ )
			{
				for( int j = y - 2; j < y + 2; j++ )
				{
					WorldGen.PlaceTile( i, j, mod.TileType( "ResistantWood" ), false, true );
				}
			}
			for( int i = 0; i < 3; i++ )
			{
				generateUpperLootRooms( WorldGen.genRand.Next( x - width2, x + width2 ), y + height2 );
			}
		}
	}
}