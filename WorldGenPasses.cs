using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Biomes;
using Terraria.GameContent.Generation;
using Terraria.ID;
using Terraria.IO;
using Terraria.World.Generation;

namespace TerrariaReworked
{
	public static class WorldGenPasses
	{
		public static StructureMap structures = new StructureMap();
		public static double worldSurface = 0.0;
		public static double worldSurfaceMin = 0.0;
		public static double worldSurfaceMax = 0.0;
		public static double rockLayer = 0.0;
		public static double rockLayerMin = 0.0;
		public static double rockLayerMax = 0.0;
		//public static int copper = 7;
		//public static int iron = 6;
		//public static int silver = 9;
		//public static int gold = 8;
		public static int dungeonSide = 0;
		public static ushort jungleShrineTile = (ushort)WorldGen.genRand.Next( 2 );
		public static int howFar = 0;
		public static int[] PyramidX = null;
		public static int[] PyramidY = null;
		public static int pyramidCount = 0;
		public static int[] snowMinX = new int[Main.maxTilesY];
		public static int[] snowMaxX = new int[Main.maxTilesY];
		public static int snowTop = 0;
		public static int snowBottom = 0;
		public static float dub2 = 0f;
		public static int waterLine { get { return WorldGen.waterLine; } set { WorldGen.waterLine = value; } }
		public static int lavaLine { get { return WorldGen.lavaLine; } set { WorldGen.lavaLine = value; } }


		public static int heartCount;
		public static bool mudWall = false;
		public static int hellChest = 0;
		public static int JungleX = 0;
		public static int numIslandHouses = 0;
		public static int houseCount = 0;
		public static int dEnteranceX = 0;
		public static int numDRooms = 0;
		public static int numDDoors = 0;
		public static int numDPlats = 0;
		public static int jungleChestCount = 0; // jungle chests.
		public static int[] jungleChestX = new int[100]; // jungle chests.
		public static int[] jungleChestY = new int[100]; // jungle chests.
		public static int JungleItemCount = 0;

		public static int copperOreId;
		public static int ironOreId;
		public static int silverOreId;
		public static int goldOreId;

		//public static int copperBar;
		//public static int ironBar;
		//public static int silverBar;
		//public static int goldBar;

		//////
		public static int moundCaveCount = 0; // exists in Webs pass, exists in mud caves to grass 2 pass. mountCaves?
		public static int[] moundCaveX = new int[30];
		public static int[] moundCaveY = new int[30];
		public static int grassSpread = 0;
		public static int tileCounterMax = 20;
		public static bool[] isSkyLake = new bool[30]; // is the floating island a lake island?
		public static int[] floatingIslandHouseX = new int[30]; // floating island house x
		public static int[] floatingIslandHouseY = new int[30]; // floating island house y

		//public static int i2;

		public static int skyLakes = 1;


		private static void ResetGenerator()
		{
			mudWall = false;
			hellChest = 0;
			JungleX = 0;
			moundCaveCount = 0;
			numIslandHouses = 0;
			houseCount = 0;
			dEnteranceX = 0;
			numDRooms = 0;
			numDDoors = 0;
			numDPlats = 0;
			jungleChestCount = 0;
			JungleItemCount = 0;
		}

		public static void GetDefaultWorldGenPasses( List<GenPass> tasks, ref float totalWeight )
		{
			tasks.Clear();

			tasks.Add( new PassLegacy( "Reset", ResetFunc ) );
			tasks.Add( new PassLegacy( "Terrain", TerrainFunc ) );
			tasks.Add( new PassLegacy( "Tunnels", TunnelsFunc ) );
			tasks.Add( new PassLegacy( "Sand", SandFunc ) );
			tasks.Add( new PassLegacy( "Mount Caves", MoundCavesFunc ) );
			tasks.Add( new PassLegacy( "Dirt Walls Background", DirtWallsBackgroundFunc ) );
			tasks.Add( new PassLegacy( "Rocks In Dirt", RocksInDirtFunc ) );
			tasks.Add( new PassLegacy( "Dirt In Rocks", DirtInRocksFunc ) );
			tasks.Add( new PassLegacy( "Clay", ClayFunc ) );
			tasks.Add( new PassLegacy( "Small Holes", SmallHolesFunc ) );
			tasks.Add( new PassLegacy( "Dirt Layer Caves", DirtLayerCavesFunc ) );
			tasks.Add( new PassLegacy( "Rock Layer Caves", RockLayerCavesFunc ) );
			tasks.Add( new PassLegacy( "Surface Caves", SurfaceCavesFunc ) );
			tasks.Add( new PassLegacy( "Slush Check", SnowBiomeFunc ) );
			//tasks.Add( new PassLegacy( "Grass", GrassFunc ) );

			tasks.Add( new PassLegacy( "Jungle", JungleFunc ) );
			tasks.Add( new PassLegacy( "Mud Caves To Grass", MudCavesToGrassFunc ) );
			tasks.Add( new PassLegacy( "Mud To Dirt", MudToDirtFunc ) );
			tasks.Add( new PassLegacy( "Full Desert", FullDesertFunc ) );
			tasks.Add( new PassLegacy( "Clouds", CloudsFunc ) );
			tasks.Add( new PassLegacy( "Floating Islands", FloatingIslandsFunc ) );
			tasks.Add( new PassLegacy( "Mushroom Patches", MushroomPatchesFunc ) );
			tasks.Add( new PassLegacy( "Silt", SiltFunc ) );
			tasks.Add( new PassLegacy( "Shinies", ShiniesFunc ) );
			tasks.Add( new PassLegacy( "Webs", WebsFunc ) );
			tasks.Add( new PassLegacy( "Underworld", UnderworldFunc ) );
			tasks.Add( new PassLegacy( "Lakes", LakesFunc ) );
			tasks.Add( new PassLegacy( "Dungeon", DungeonFunc ) );
			tasks.Add( new PassLegacy( "Dungeon Chests", DungeonChestsFunc ) );
			tasks.Add( new PassLegacy( "Corruption", CorruptionFunc ) );
			tasks.Add( new PassLegacy( "Slush", SlushFunc ) );


			tasks.Add( new PassLegacy( "Mud Caves To Grass 2", MudCavesToGrass2Func ) );
			tasks.Add( new PassLegacy( "Beaches", BeachesFunc ) );
			tasks.Add( new PassLegacy( "Gems", GemsFunc ) );
			tasks.Add( new PassLegacy( "Gravitating Sand", GravitatingSandFunc ) );
			tasks.Add( new PassLegacy( "Clean Up Dirt", CleanUpDirtFunc ) );
			tasks.Add( new PassLegacy( "Pyramids", PyramidsFunc ) );
			tasks.Add( new PassLegacy( "Dirt Rock Wall Runner", DirtRockWallRunnerFunc ) );

			tasks.Add( new PassLegacy( "Living Trees", LivingTreesFunc ) );
			tasks.Add( new PassLegacy( "Wood Tree Walls", WoodTreeWallsFunc ) );
			tasks.Add( new PassLegacy( "Altars", AltarsFunc ) );
			tasks.Add( new PassLegacy( "Wet Jungle", WetJungleFunc ) );
			tasks.Add( new PassLegacy( "Remove Water From Sand", RemoveWaterFromSandFunc ) );
			tasks.Add( new PassLegacy( "Hives", HivesFunc ) );
			tasks.Add( new PassLegacy( "Jungle Chests", JungleShrinesFunc ) );
			tasks.Add( new PassLegacy( "Smooth World", SmoothWorldFunc ) );
			tasks.Add( new PassLegacy( "Settle Liquids", SettleLiquidsFunc ) );
			tasks.Add( new PassLegacy( "Waterfalls", WaterfallsFunc ) );
			tasks.Add( new PassLegacy( "Ice", IceFunc ) );
			tasks.Add( new PassLegacy( "Wall Variety", WallVarietyFunc ) );
			tasks.Add( new PassLegacy( "Traps", TrapsFunc ) );
			tasks.Add( new PassLegacy( "Life Crystals", LifeCrystalsFunc ) );
			tasks.Add( new PassLegacy( "Statues", StatuesFunc ) );
			tasks.Add( new PassLegacy( "Buried Chests", BuriedChestsFunc ) );
			tasks.Add( new PassLegacy( "Surface Chests", SurfaceChestsFunc ) );
			tasks.Add( new PassLegacy( "Jungle Chests Placement", JungleShrinesChestsFunc ) );
			tasks.Add( new PassLegacy( "Water Chests", WaterChestsFunc ) );
			tasks.Add( new PassLegacy( "Gem Caves", GemCavesFunc ) );
			//tasks.Add( new PassLegacy( "Moss", MossFunc ) );
			tasks.Add( new PassLegacy( "Ice Walls", IceWallsFunc ) );
			tasks.Add( new PassLegacy( "Jungle Trees", JungleTreesFunc ) );
			tasks.Add( new PassLegacy( "Floating Island Houses", FloatingIslandHousesFunc ) );
			tasks.Add( new PassLegacy( "Quick Cleanup", QuickCleanupFunc ) );
			tasks.Add( new PassLegacy( "Pots", PotsFunc ) );
			tasks.Add( new PassLegacy( "Hellforge", HellforgeFunc ) );
			tasks.Add( new PassLegacy( "Spreading Grass", SpreadingGrassFunc ) );
			tasks.Add( new PassLegacy( "Piles", PilesFunc ) );
			//tasks.Add( new PassLegacy( "Moss 2", Moss2Func ) );

			tasks.Add( new PassLegacy( "Spawn Point", SpawnPointFunc ) );

			tasks.Add( new PassLegacy( "Grass Wall", GrassWallFunc ) );
			tasks.Add( new PassLegacy( "Guide", GuideFunc ) );
			tasks.Add( new PassLegacy( "Sunflowers", SunflowersFunc ) );
			tasks.Add( new PassLegacy( "Planting Trees", PlantingTreesFunc ) );
			tasks.Add( new PassLegacy( "Herbs", HerbsFunc ) );
			tasks.Add( new PassLegacy( "Dye Plants", DyePlantsFunc ) );
			tasks.Add( new PassLegacy( "Herbs And Honey", HerbsAndHoneyFunc ) );
			tasks.Add( new PassLegacy( "Weeds", WeedsFunc ) );
			tasks.Add( new PassLegacy( "Mud Caves To Grass 3", GlowingMushroomsAndJunglePlantsFunc ) );
			tasks.Add( new PassLegacy( "Jungle Plants", JunglePlantsFunc ) );
			tasks.Add( new PassLegacy( "Vines", VinesFunc ) );

			tasks.Add( new PassLegacy( "Flowers", FlowersFunc ) );
			tasks.Add( new PassLegacy( "Mushrooms", MushroomsFunc ) );
			tasks.Add( new PassLegacy( "Stalac", StalacFunc ) );
			tasks.Add( new PassLegacy( "Gems In Ice Biome", GemsInIceBiomeFunc ) );
			tasks.Add( new PassLegacy( "Random Gems", RandomGemsFunc ) );
			tasks.Add( new PassLegacy( "Moss Grass", MossGrassFunc ) );
			tasks.Add( new PassLegacy( "Mud Walls In Jungle", MudWallsInJungleFunc ) );
			tasks.Add( new PassLegacy( "Larva", LarvaFunc ) );
			tasks.Add( new PassLegacy( "Settle Liquids Again", SettleLiquidsAgainFunc ) );
			tasks.Add( new PassLegacy( "Tile Cleanup", TileCleanupFunc ) );
			//tasks.Add( new PassLegacy( "Micro Biomes", MicroBiomesFunc ) );
			tasks.Add( new PassLegacy( "Final Cleanup", FinalCleanupFunc ) );
			tasks.Add( new PassLegacy( "Hellcastle", HellcastleFunc ) );
			//tasks.Add( new PassLegacy( "HMGems", HMGemsFunc ) );
		}

		public static void GetDefaultHardmodePasses( List<GenPass> tasks, ref float totalWeight )
		{
			//tasks.Clear();

			tasks.Add( new PassLegacy( "HMGems", HMGemsFunc ) );
		}

		public static void GetDefaultSuperhardmodePasses( List<GenPass> tasks, ref float totalWeight )
		{
			tasks.Clear();

			tasks.Add( new PassLegacy( "SHMShinies", SHMShiniesFunc ) );
		}
		
		private static void ResetFunc( GenerationProgress progress )
		{
			Liquid.ReInit();
			WorldGen.noTileActions = true;
			progress.Message = "";
			WorldGen.SetupStatueList();
			WorldGen.RandomizeWeather();
			Main.cloudAlpha = 0f;
			Main.maxRaining = 0f;
			WorldFile.tempMaxRain = 0f;
			Main.raining = false;
			heartCount = 0;
			Main.checkXMas();
			Main.checkHalloween();
			WorldGen.gen = true;
			ResetGenerator();
			WorldGen.numLarva = 0;
			int numSecondsInDay = 86400;
			Main.slimeRainTime = (double)(-(double)WorldGen.genRand.Next( numSecondsInDay * 2, numSecondsInDay * 3 ));
			Main.cloudBGActive = (float)(-(float)WorldGen.genRand.Next( 9000, 86400 ));

			copperOreId = TileID.Copper;
			ironOreId = TileID.Iron;
			silverOreId = TileID.Silver;
			goldOreId = TileID.Gold;
			//copperBar = 20;
			//ironBar = 22;
			//silverBar = 21;
			//goldBar = 19;
			/*if( WorldGen.genRand.Next( 2 ) == 0 )
			{
				//copper = 166;
				//copperBar = 703;
				copperOreId = 166;
			}
			if( WorldGen.genRand.Next( 2 ) == 0 )
			{
				//iron = 167;
				//ironBar = 704;
				ironOreId = 167;
			}
			if( WorldGen.genRand.Next( 2 ) == 0 )
			{
				//silver = 168;
				//silverBar = 705;
				silverOreId = 168;
			}
			if( WorldGen.genRand.Next( 2 ) == 0 )
			{
				//gold = 169;
				//goldBar = 706;
				goldOreId = 169;
			}*/

			MyWorld.worldProgressionState = WorldProgressionState.PreHardmode;

			MyWorld.evilCombo = WorldGen.genRand.Next( 2 ) == 0 ? EvilCombo.Crimson : EvilCombo.Corruption;
			if( WorldGen.WorldGenParam_Evil == 0 ) // forced corruption (worldgen GUI)
			{
				MyWorld.evilCombo = EvilCombo.Corruption;
			}
			if( WorldGen.WorldGenParam_Evil == 1 ) // forced crimson
			{
				MyWorld.evilCombo = EvilCombo.Crimson;
			}
			
			if( jungleShrineTile == 0 )
			{
				jungleShrineTile = 45;
			}
			else if( jungleShrineTile == 1 )
			{
				jungleShrineTile = 120;
			}
			Main.worldID = WorldGen.genRand.Next( int.MaxValue );
			WorldGen.RandomizeTreeStyle();
			WorldGen.RandomizeCaveBackgrounds();
			WorldGen.RandomizeBackgrounds();
			WorldGen.RandomizeMoonState();
			dungeonSide = ((WorldGen.genRand.Next( 2 ) == 0) ? -1 : 1);
			if( Main.maxTilesX > 8000 )
			{
				skyLakes++;
			}
			if( Main.maxTilesX > 6000 )
			{
				skyLakes++;
			}
		}

		private static void TerrainFunc( GenerationProgress progress )
		{
			progress.Message = Lang.gen[0].Value;
			int num = 0;
			int num2 = 0;
			worldSurface = (double)Main.maxTilesY * 0.3;
			worldSurface *= (double)WorldGen.genRand.Next( 90, 110 ) * 0.005;
			rockLayer = worldSurface + (double)Main.maxTilesY * 0.2;
			rockLayer *= (double)WorldGen.genRand.Next( 90, 110 ) * 0.01;
			worldSurfaceMin = worldSurface;
			worldSurfaceMax = worldSurface;
			rockLayerMin = rockLayer;
			rockLayerMax = rockLayer;
			for( int x = 0; x < Main.maxTilesX; x++ )
			{
				progress.Set( x / (float)Main.maxTilesX );

				if( worldSurface < worldSurfaceMin )
				{
					worldSurfaceMin = worldSurface;
				}
				if( worldSurface > worldSurfaceMax )
				{
					worldSurfaceMax = worldSurface;
				}
				if( rockLayer < rockLayerMin )
				{
					rockLayerMin = rockLayer;
				}
				if( rockLayer > rockLayerMax )
				{
					rockLayerMax = rockLayer;
				}

				if( num2 <= 0 )
				{
					num = WorldGen.genRand.Next( 0, 5 );
					num2 = WorldGen.genRand.Next( 5, 40 );
					if( num == 0 )
					{
						num2 *= (int)((double)WorldGen.genRand.Next( 5, 30 ) * 0.2);
					}
				}
				num2--;
				if( (double)x > (double)Main.maxTilesX * 0.43 && (double)x < (double)Main.maxTilesX * 0.57 && num >= 3 )
				{
					num = WorldGen.genRand.Next( 3 );
				}
				if( (double)x > (double)Main.maxTilesX * 0.47 && (double)x < (double)Main.maxTilesX * 0.53 )
				{
					num = 0;
				}
				if( num == 0 )
				{
					while( WorldGen.genRand.Next( 0, 7 ) == 0 )
					{
						worldSurface += (double)WorldGen.genRand.Next( -1, 2 );
					}
				}
				else if( num == 1 )
				{
					while( WorldGen.genRand.Next( 0, 4 ) == 0 )
					{
						worldSurface -= 1.0;
					}
					while( WorldGen.genRand.Next( 0, 10 ) == 0 )
					{
						worldSurface += 1.0;
					}
				}
				else if( num == 2 )
				{
					while( WorldGen.genRand.Next( 0, 4 ) == 0 )
					{
						worldSurface += 1.0;
					}
					while( WorldGen.genRand.Next( 0, 10 ) == 0 )
					{
						worldSurface -= 1.0;
					}
				}
				else if( num == 3 )
				{
					while( WorldGen.genRand.Next( 0, 2 ) == 0 )
					{
						worldSurface -= 1.0;
					}
					while( WorldGen.genRand.Next( 0, 6 ) == 0 )
					{
						worldSurface += 1.0;
					}
				}
				else if( num == 4 )
				{
					while( WorldGen.genRand.Next( 0, 2 ) == 0 )
					{
						worldSurface += 1.0;
					}
					while( WorldGen.genRand.Next( 0, 5 ) == 0 )
					{
						worldSurface -= 1.0;
					}
				}
				if( worldSurface < (double)Main.maxTilesY * 0.17 )
				{
					worldSurface = (double)Main.maxTilesY * 0.17;
					num2 = 0;
				}
				else if( worldSurface > (double)Main.maxTilesY * 0.3 )
				{
					worldSurface = (double)Main.maxTilesY * 0.3;
					num2 = 0;
				}
				if( (x < 275 || x > Main.maxTilesX - 275) && worldSurface > (double)Main.maxTilesY * 0.25 )
				{
					worldSurface = (double)Main.maxTilesY * 0.25;
					num2 = 1;
				}
				while( WorldGen.genRand.Next( 0, 3 ) == 0 )
				{
					rockLayer += (double)WorldGen.genRand.Next( -2, 3 );
				}
				if( rockLayer < worldSurface + (double)Main.maxTilesY * 0.05 )
				{
					rockLayer += 1.0;
				}
				if( rockLayer > worldSurface + (double)Main.maxTilesY * 0.35 )
				{
					rockLayer -= 1.0;
				}
				int y = 0;
				while( (double)y < worldSurface )
				{
					Main.tile[x, y].active( false );
					Main.tile[x, y].frameX = -1;
					Main.tile[x, y].frameY = -1;
					y++;
				}
				for( int y2 = (int)worldSurface; y2 < Main.maxTilesY; y2++ )
				{
					if( (double)y2 < rockLayer )
					{
						Main.tile[x, y2].active( true );
						Main.tile[x, y2].type = 0;
						Main.tile[x, y2].frameX = -1;
						Main.tile[x, y2].frameY = -1;
					}
					else
					{
						Main.tile[x, y2].active( true );
						Main.tile[x, y2].type = 1;
						Main.tile[x, y2].frameX = -1;
						Main.tile[x, y2].frameY = -1;
					}
				}
			}
			Main.worldSurface = worldSurfaceMax + 25.0;
			Main.rockLayer = rockLayerMax;
			double offset = (double)((int)((Main.rockLayer - Main.worldSurface) / 6.0) * 6);
			Main.rockLayer = Main.worldSurface + offset;

			waterLine = (int)(Main.rockLayer + (double)Main.maxTilesY) / 2;
			waterLine += WorldGen.genRand.Next( -100, 20 );
			lavaLine = waterLine + WorldGen.genRand.Next( 50, 80 );
		}

		private static void TunnelsFunc( GenerationProgress progress )
		{
			for( int i = 0; i < (int)(Main.maxTilesX * 0.0015); i++ )
			{
				int tunnelLength = 10;
				int[] arrX = new int[tunnelLength]; // position along the snake
				int[] arrY = new int[tunnelLength]; // position along the snake

				int x = WorldGen.genRand.Next( 450, Main.maxTilesX - 450 );
				int y = 0;

				while( (x > (float)Main.maxTilesX * 0.45f) && (x < (float)Main.maxTilesX * 0.55f) )
				{
					x = WorldGen.genRand.Next( 0, Main.maxTilesX );
				}
				for( int j = 0; j < tunnelLength; j++ )
				{
					x %= Main.maxTilesX;
					while( !Main.tile[x, y].active() )
					{
						y++;
					}
					arrX[j] = x;
					arrY[j] = y - WorldGen.genRand.Next( 11, 16 );
					x += WorldGen.genRand.Next( 5, 11 );
				}
				// place dirt above the ground (speedX, speedY).
				for( int j = 0; j < tunnelLength; j++ )
				{
					WorldGenUtils.TileRunner( arrX[j], arrY[j], WorldGen.genRand.Next( 5, 8 ), WorldGen.genRand.Next( 6, 9 ), TileID.Dirt, true, -2f, -0.3f, false, true );
					WorldGenUtils.TileRunner( arrX[j], arrY[j], WorldGen.genRand.Next( 5, 8 ), WorldGen.genRand.Next( 6, 9 ), TileID.Dirt, true, 2f, -0.3f, false, true );
				}
			}
		}

		private static void SandFunc( GenerationProgress progress )
		{
			progress.Message = Lang.gen[1].Value;
			int pyramidMaxAmt = WorldGen.genRand.Next( (int)((double)Main.maxTilesX * 0.0008), (int)((double)Main.maxTilesX * 0.0025) );
			pyramidMaxAmt += 2;
			PyramidX = new int[pyramidMaxAmt];
			PyramidY = new int[pyramidMaxAmt];
			for( int k = 0; k < pyramidMaxAmt; k++ )
			{
				int num2 = WorldGen.genRand.Next( Main.maxTilesX );
				while( (float)num2 > (float)Main.maxTilesX * 0.4f && (float)num2 < (float)Main.maxTilesX * 0.6f )
				{
					num2 = WorldGen.genRand.Next( Main.maxTilesX );
				}
				int num3 = WorldGen.genRand.Next( 35, 90 );
				if( k == 1 )
				{
					float num4 = (float)(Main.maxTilesX / 4200);
					num3 += (int)((float)WorldGen.genRand.Next( 20, 40 ) * num4);
				}
				if( WorldGen.genRand.Next( 3 ) == 0 )
				{
					num3 *= 2;
				}
				if( k == 1 )
				{
					num3 *= 2;
				}
				int xOffset = num2 - num3;
				num3 = WorldGen.genRand.Next( 35, 90 );
				if( WorldGen.genRand.Next( 3 ) == 0 )
				{
					num3 *= 2;
				}
				if( k == 1 )
				{
					num3 *= 2;
				}
				int num6 = num2 + num3;
				if( xOffset < 0 )
				{
					xOffset = 0;
				}
				if( num6 > Main.maxTilesX )
				{
					num6 = Main.maxTilesX;
				}
				if( k == 0 )
				{
					xOffset = 0;
					num6 = WorldGen.genRand.Next( 260, 300 );
					if( dungeonSide == 1 )
					{
						num6 += 40;
					}
				}
				else if( k == 2 )
				{
					xOffset = Main.maxTilesX - WorldGen.genRand.Next( 260, 300 );
					num6 = Main.maxTilesX;
					if( dungeonSide == -1 )
					{
						xOffset -= 40;
					}
				}
				int num7 = WorldGen.genRand.Next( 50, 100 );
				for( int x = xOffset; x < num6; x++ )
				{
					if( WorldGen.genRand.Next( 2 ) == 0 )
					{
						num7 += WorldGen.genRand.Next( -1, 2 );
						if( num7 < 50 )
						{
							num7 = 50;
						}
						if( num7 > 100 )
						{
							num7 = 100;
						}
					}
					int y = 0;
					while( y < Main.worldSurface )
					{
						if( Main.tile[x, y].active() )
						{
							if( x == (xOffset + num6) / 2 && WorldGen.genRand.Next( 6 ) == 0 )
							{
								PyramidX[pyramidCount] = x;
								PyramidY[pyramidCount] = y;
								pyramidCount++;
							}
							int num9 = num7;
							if( x - xOffset < num9 )
							{
								num9 = x - xOffset;
							}
							if( num6 - x < num9 )
							{
								num9 = num6 - x;
							}
							num9 += WorldGen.genRand.Next( 5 );
							for( int y2 = y; y2 < y + num9; y2++ )
							{
								if( x > xOffset + WorldGen.genRand.Next( 5 ) && x < num6 - WorldGen.genRand.Next( 5 ) )
								{
									Main.tile[x, y2].type = TileID.Sand;
								}
							}
							break;
						}
						y++;
					}
				}
			}
			for( int n = 0; n < (int)(Main.maxTilesX * Main.maxTilesY * 0.000008); n++ )
			{
				int randX = WorldGen.genRand.Next( 0, Main.maxTilesX );
				int randY = WorldGen.genRand.Next( (int)Main.worldSurface, (int)Main.rockLayer );
				WorldGenUtils.TileRunner( randX, randY, WorldGen.genRand.Next( 15, 70 ), WorldGen.genRand.Next( 20, 130 ), TileID.Sand );
			}
		}

		private static void MoundCavesFunc( GenerationProgress progress )
		{
			moundCaveCount = 0;
			progress.Message = Lang.gen[2].Value;
			for( int k = 0; k < Main.maxTilesX * 0.0008; k++ )
			{
				int num = 0;
				bool isOnSand = false;
				bool flag3 = false;
				int x = WorldGen.genRand.Next( (int)(Main.maxTilesX * 0.25), (int)(Main.maxTilesX * 0.75) );
				while( !flag3 )
				{
					flag3 = true;
					while( x > Main.maxTilesX / 2 - 100 && x < Main.maxTilesX / 2 + 100 )
					{
						x = WorldGen.genRand.Next( (int)(Main.maxTilesX * 0.25), (int)(Main.maxTilesX * 0.75) );
					}
					for( int l = 0; l < moundCaveCount; l++ )
					{
						if( x > moundCaveX[l] - 50 && x < moundCaveX[l] + 50 )
						{
							num++;
							flag3 = false;
							break;
						}
					}
					if( num >= 200 )
					{
						isOnSand = true;
						break;
					}
				}
				if( !isOnSand )
				{
					int y = 0;
					while( (double)y < Main.worldSurface )
					{
						if( Main.tile[x, y].active() )
						{
							for( int i = x - 50; i < x + 50; i++ )
							{
								for( int j = y - 25; j < y + 25; j++ )
								{
									if( Main.tile[i, j].active() && (Main.tile[i, j].type == TileID.Sand || Main.tile[i, j].type == TileID.SandstoneBrick || Main.tile[i, j].type == TileID.SandStoneSlab) )
									{
										isOnSand = true;
									}
								}
							}
							if( !isOnSand )
							{
								WorldGen.Mountinater( x, y );
								moundCaveX[moundCaveCount] = x;
								moundCaveY[moundCaveCount] = y;
								moundCaveCount++;
								break;
							}
						}
						y++;
					}
				}
			}
		}

		private static void DirtWallsBackgroundFunc( GenerationProgress progress )
		{
			progress.Message = Lang.gen[3].Value;
			for( int x = 1; x < Main.maxTilesX - 1; x++ )
			{
				byte wallId = WallID.DirtUnsafe;
				progress.Set( x / (float)Main.maxTilesX );
				bool flag2 = false;
				howFar += WorldGen.genRand.Next( -1, 2 );
				if( howFar < 0 )
				{
					howFar = 0;
				}
				if( howFar > 10 )
				{
					howFar = 10;
				}
				int y = 0;
				while( (y < Main.worldSurface + 10.0) && (y <= Main.worldSurface + howFar) )
				{
					if( Main.tile[x, y].active() )
					{
						if( Main.tile[x, y].type == TileID.SnowBlock )
						{
							wallId = WallID.SnowWallUnsafe;
						}
						else
						{
							wallId = WallID.DirtUnsafe;
						}
					}
					if( flag2 && Main.tile[x, y].wall != WallID.JungleUnsafe )
					{
						Main.tile[x, y].wall = wallId;
					}
					if( Main.tile[x, y].active() && Main.tile[x - 1, y].active() && Main.tile[x + 1, y].active() && Main.tile[x, y + 1].active() && Main.tile[x - 1, y + 1].active() && Main.tile[x + 1, y + 1].active() )
					{
						flag2 = true;
					}
					y++;
				}
			}
		}

		private static void RocksInDirtFunc( GenerationProgress progress )
		{
			progress.Message = Lang.gen[4].Value;

			// big
			for( int i = 0; i < (int)(Main.maxTilesX * Main.maxTilesY * 0.00015); i++ )
			{
				int x = WorldGen.genRand.Next( 0, Main.maxTilesX );
				int y = WorldGen.genRand.Next( 0, (int)worldSurfaceMin + 1 );
				WorldGenUtils.TileRunner( x, y, WorldGen.genRand.Next( 4, 15 ), WorldGen.genRand.Next( 5, 40 ), 1 );
			}

			// mid
			for( int i = 0; i < (int)(Main.maxTilesX * Main.maxTilesY * 0.0002); i++ )
			{
				int x = WorldGen.genRand.Next( 0, Main.maxTilesX );
				int y = WorldGen.genRand.Next( (int)worldSurfaceMin, (int)worldSurfaceMax + 1 );
				if( !Main.tile[x, y - 10].active() )
				{
					y = WorldGen.genRand.Next( (int)worldSurfaceMin, (int)worldSurfaceMax + 1 );
				}
				WorldGenUtils.TileRunner( x, y, WorldGen.genRand.Next( 4, 10 ), WorldGen.genRand.Next( 5, 30 ), 1 );
			}

			// small
			for( int i = 0; i < (int)(Main.maxTilesX * Main.maxTilesY * 0.0045); i++ )
			{
				int x = WorldGen.genRand.Next( 0, Main.maxTilesX );
				int y = WorldGen.genRand.Next( (int)worldSurfaceMax, (int)rockLayerMax + 1 );
				WorldGenUtils.TileRunner( x, y, WorldGen.genRand.Next( 2, 7 ), WorldGen.genRand.Next( 2, 23 ), 1 );
			}
		}

		private static void DirtInRocksFunc( GenerationProgress progress )
		{
			progress.Message = Lang.gen[5].Value;

			for( int i = 0; i < (int)(Main.maxTilesX * Main.maxTilesY * 0.005); i++ )
			{
				int x = WorldGen.genRand.Next( 0, Main.maxTilesX );
				int y = WorldGen.genRand.Next( (int)rockLayerMin, Main.maxTilesY );
				WorldGenUtils.TileRunner( x, y, WorldGen.genRand.Next( 2, 6 ), WorldGen.genRand.Next( 2, 40 ), 0 );
			}
		}

		private static void ClayFunc( GenerationProgress progress )
		{
			progress.Message = Lang.gen[6].Value;

			// top
			for( int i = 0; i < (int)(Main.maxTilesX * Main.maxTilesY * 0.00002); i++ )
			{
				int x = WorldGen.genRand.Next( 0, Main.maxTilesX );
				int y = WorldGen.genRand.Next( 0, (int)worldSurfaceMin );
				WorldGenUtils.TileRunner( x, y, WorldGen.genRand.Next( 8, 16 ), WorldGen.genRand.Next( 20, 50 ), TileID.ClayBlock );
			}
			// mid
			for( int i = 0; i < (int)(Main.maxTilesX * Main.maxTilesY * 0.00005); i++ )
			{
				int x = WorldGen.genRand.Next( 0, Main.maxTilesX );
				int y = WorldGen.genRand.Next( (int)worldSurfaceMin, (int)worldSurfaceMax + 1 );
				WorldGenUtils.TileRunner( x, y, WorldGen.genRand.Next( 8, 12 ), WorldGen.genRand.Next( 10, 30 ), TileID.ClayBlock );
			}
			// bottom (removed to have less messed up block palette)
			/*for( int i = 0; i < (int)(Main.maxTilesX * Main.maxTilesY * 0.00002); i++ )
			{
				int x = WorldGen.genRand.Next( 0, Main.maxTilesX );
				int y = WorldGen.genRand.Next( (int)worldSurfaceHigh, (int)rockLayerHigh + 1 );
				WorldGenUtils.TileRunner( x, y, WorldGen.genRand.Next( 8, 15 ), WorldGen.genRand.Next( 5, 50 ), TileID.ClayBlock );
			}*/
			// cover all clay visible on the surface (5 blocks deep).
			/*for( int x = 5; x < Main.maxTilesX - 5; x++ )
			{
				int surfaceY = 1;
				while( surfaceY < Main.worldSurface - 1 )
				{
					if( Main.tile[x, surfaceY].active() )
					{
						for( int y = surfaceY; y < surfaceY + 5; y++ )
						{
							if( Main.tile[x, y].type == TileID.ClayBlock )
							{
								Main.tile[x, y].type = TileID.Dirt;
							}
						}
						break;
					}
					surfaceY++;
				}
			}*/
		}

		private static void SmallHolesFunc( GenerationProgress progress )
		{
			//i2 = 0;
			progress.Message = Lang.gen[7].Value;
			for( int k = 0; k < (int)(Main.maxTilesX * Main.maxTilesY * 0.0015); k++ )
			{
				progress.Set( (float)(k / (Main.maxTilesX * Main.maxTilesY * 0.0015)) );

				int specialId = -1; // set active(false)

				// 20% chance for the hole to be filled with lava (at and below the lavaline).
				if( WorldGen.genRand.Next( 5 ) == 0 )
				{
					specialId = -2;
				}

				int x = WorldGen.genRand.Next( 0, Main.maxTilesX );
				int y = WorldGen.genRand.Next( (int)worldSurfaceMax, Main.maxTilesY );
				WorldGenUtils.TileRunner( x, y, WorldGen.genRand.Next( 2, 5 ), WorldGen.genRand.Next( 2, 20 ), specialId );

				x = WorldGen.genRand.Next( 0, Main.maxTilesX );
				y = WorldGen.genRand.Next( (int)worldSurfaceMax, Main.maxTilesY );
				WorldGenUtils.TileRunner( x, y, WorldGen.genRand.Next( 8, 15 ), WorldGen.genRand.Next( 7, 30 ), specialId );
			}
		}

		private static void DirtLayerCavesFunc( GenerationProgress progress )
		{
			progress.Message = Lang.gen[8].Value;
			for( int i = 0; i < (int)(Main.maxTilesX * Main.maxTilesY * 0.00003); i++ )
			{
				progress.Set( (float)(i / (Main.maxTilesX * Main.maxTilesY * 0.00003)) );
				if( rockLayerMax <= Main.maxTilesY )
				{
					int specialId = -1;
					// 1/6 chance for the cave to be filled with lava (at and below the lavaline).
					if( WorldGen.genRand.Next( 6 ) == 0 )
					{
						specialId = -2;
					}

					int x = WorldGen.genRand.Next( 0, Main.maxTilesX );
					int y = WorldGen.genRand.Next( (int)worldSurfaceMin, (int)rockLayerMax + 1 );
					WorldGenUtils.TileRunner( x, y, WorldGen.genRand.Next( 5, 15 ), WorldGen.genRand.Next( 30, 200 ), specialId );
				}
			}
		}

		private static void RockLayerCavesFunc( GenerationProgress progress )
		{
			progress.Message = Lang.gen[9].Value;

			for( int i = 0; i < (int)(Main.maxTilesX * Main.maxTilesY * 0.00013); i++ )
			{
				progress.Set( (float)(i / (Main.maxTilesX * Main.maxTilesY * 0.00013)) );
				if( rockLayerMax <= (double)Main.maxTilesY )
				{
					int type = -1;
					// 10% chance for the cave to be filled with lava (at and below the lavaline).
					if( WorldGen.genRand.Next( 10 ) == 0 )
					{
						type = -2;
					}

					int x = WorldGen.genRand.Next( 0, Main.maxTilesX );
					int y = WorldGen.genRand.Next( (int)rockLayerMax, Main.maxTilesY );
					WorldGenUtils.TileRunner( x, y, WorldGen.genRand.Next( 6, 20 ), WorldGen.genRand.Next( 50, 300 ), type, false, 0f, 0f, false, true );
				}
			}
		}

		private static void SurfaceCavesFunc( GenerationProgress progress )
		{
			progress.Message = Lang.gen[10].Value;

			// small caves.
			for( int i = 0; i < (int)(Main.maxTilesX * 0.002); i++ )
			{
				int x = WorldGen.genRand.Next( 0, Main.maxTilesX );
				while( x > Main.maxTilesX * 0.45f && x < Main.maxTilesX * 0.55f )
				{
					x = WorldGen.genRand.Next( 0, Main.maxTilesX );
				}
				int y = 0;
				while( y < worldSurfaceMax )
				{
					if( Main.tile[x, y].active() )
					{
						WorldGenUtils.TileRunner( x, y, WorldGen.genRand.Next( 3, 6 ), WorldGen.genRand.Next( 5, 50 ), -1, false, WorldGen.genRand.Next( -10, 11 ) * 0.1f, 1f, false, true );
						break;
					}
					y++;
				}
			}

			// medium caves.
			for( int i = 0; i < (int)(Main.maxTilesX * 0.0007); i++ )
			{
				int x = WorldGen.genRand.Next( 0, Main.maxTilesX );
				while( x > Main.maxTilesX * 0.43f && x < Main.maxTilesX * 0.57f )
				{
					x = WorldGen.genRand.Next( 0, Main.maxTilesX );
				}
				int y = 0;
				while( y < worldSurfaceMax )
				{
					if( Main.tile[x, y].active() )
					{
						WorldGenUtils.TileRunner( x, y, WorldGen.genRand.Next( 10, 15 ), WorldGen.genRand.Next( 50, 130 ), -1, false, WorldGen.genRand.Next( -10, 11 ) * 0.1f, 2f, false, true );
						break;
					}
					y++;
				}
			}

			// branching caves(?)
			for( int i = 0; i < (int)(Main.maxTilesX * 0.0003); i++ )
			{
				int x = WorldGen.genRand.Next( 0, Main.maxTilesX );
				while( x > Main.maxTilesX * 0.4f && x < Main.maxTilesX * 0.6f )
				{
					x = WorldGen.genRand.Next( 0, Main.maxTilesX );
				}
				int y = 0;
				while( y < worldSurfaceMax )
				{
					if( Main.tile[x, y].active() )
					{
						WorldGenUtils.TileRunner( x, y, WorldGen.genRand.Next( 12, 25 ), WorldGen.genRand.Next( 150, 500 ), -1, false, WorldGen.genRand.Next( -10, 11 ) * 0.1f, 4f, false, true );
						WorldGenUtils.TileRunner( x, y, WorldGen.genRand.Next( 8, 17 ), WorldGen.genRand.Next( 60, 200 ), -1, false, WorldGen.genRand.Next( -10, 11 ) * 0.1f, 2f, false, true );
						WorldGenUtils.TileRunner( x, y, WorldGen.genRand.Next( 5, 13 ), WorldGen.genRand.Next( 40, 170 ), -1, false, WorldGen.genRand.Next( -10, 11 ) * 0.1f, 2f, false, true );
						break;
					}
					y++;
				}
			}

			// very long caves.
			for( int i = 0; i < (int)(Main.maxTilesX * 0.0004); i++ )
			{
				int x = WorldGen.genRand.Next( 0, Main.maxTilesX );
				while( x > Main.maxTilesX * 0.4f && x < Main.maxTilesX * 0.6f )
				{
					x = WorldGen.genRand.Next( 0, Main.maxTilesX );
				}
				int y = 0;
				while( y < worldSurfaceMax )
				{
					if( Main.tile[x, y].active() )
					{
						WorldGenUtils.TileRunner( x, y, WorldGen.genRand.Next( 7, 12 ), WorldGen.genRand.Next( 150, 250 ), -1 );
						break;
					}
					y++;
				}
			}

			// special thingy(?) (Caverer)
			float amt = (float)Main.maxTilesX / 4200;
			int j = 0;
			while( j < amt * 5 )
			{
				try
				{
					int x = WorldGen.genRand.Next( 100, Main.maxTilesX - 100 );
					int y = WorldGen.genRand.Next( (int)Main.rockLayer, Main.maxTilesY - 400 );
					WorldGen.Caverer( x, y );
				}
				catch
				{
					// do nothing I guess, just don't crash by thrown exception - quite a bad practice. //Kat
				}
				j++;
			}
		}

		private static void SnowBiomeFunc( GenerationProgress progress )
		{
			progress.Message = Lang.gen[56].Value;
			snowTop = (int)Main.worldSurface;
			int num = WorldGen.genRand.Next( Main.maxTilesX );
			if( dungeonSide == 1 )
			{
				while( (float)num < (float)Main.maxTilesX * 0.55f || (float)num > (float)Main.maxTilesX * 0.7f )
				{
					num = WorldGen.genRand.Next( Main.maxTilesX );
				}
			}
			else
			{
				while( (float)num < (float)Main.maxTilesX * 0.3f || (float)num > (float)Main.maxTilesX * 0.45f )
				{
					num = WorldGen.genRand.Next( Main.maxTilesX );
				}
			}
			int num2 = WorldGen.genRand.Next( 50, 90 );
			float num3 = (float)(Main.maxTilesX / 4200);
			num2 += (int)((float)WorldGen.genRand.Next( 20, 40 ) * num3);
			num2 += (int)((float)WorldGen.genRand.Next( 20, 40 ) * num3);
			int startX = num - num2;
			num2 = WorldGen.genRand.Next( 50, 90 );
			num2 += (int)((float)WorldGen.genRand.Next( 20, 40 ) * num3);
			num2 += (int)((float)WorldGen.genRand.Next( 20, 40 ) * num3);
			int endX = num + num2;
			if( startX < 0 )
			{
				startX = 0;
			}
			if( endX > Main.maxTilesX )
			{
				endX = Main.maxTilesX;
			}
			int yMaxOffset = 10;
			for( int y = 0; y <= lavaLine - 140; y++ )
			{
				startX += WorldGen.genRand.Next( -4, 4 );
				endX += WorldGen.genRand.Next( -3, 5 );
				snowMinX[y] = startX;
				snowMaxX[y] = endX;
				for( int x = startX; x < endX; x++ )
				{
					if( y < lavaLine - 140 )
					{
						if( Main.tile[x, y].wall == WallID.DirtUnsafe )
						{
							Main.tile[x, y].wall = WallID.SnowWallUnsafe;
						}

						int type = Main.tile[x, y].type;
						if( type == TileID.Dirt || type == TileID.Grass || type == TileID.CorruptGrass || type == TileID.ClayBlock || type == TileID.Sand )
						{
							Main.tile[x, y].type = TileID.SnowBlock;
						}
						else if( type == TileID.Stone )
						{
							Main.tile[x, y].type = TileID.IceBlock;
						}
					}
					else
					{
						yMaxOffset += WorldGen.genRand.Next( -3, 4 );
						if( WorldGen.genRand.Next( 3 ) == 0 )
						{
							yMaxOffset += WorldGen.genRand.Next( -4, 5 );
							if( WorldGen.genRand.Next( 3 ) == 0 )
							{
								yMaxOffset += WorldGen.genRand.Next( -6, 7 );
							}
						}
						if( yMaxOffset < 0 )
						{
							yMaxOffset = WorldGen.genRand.Next( 3 );
						}
						else if( yMaxOffset > 50 )
						{
							yMaxOffset = 50 - WorldGen.genRand.Next( 3 );
						}
						for( int y2 = y; y2 < y + yMaxOffset; y2++ )
						{
							if( Main.tile[x, y2].wall == WallID.DirtUnsafe )
							{
								Main.tile[x, y2].wall = WallID.SnowWallUnsafe;
							}

							int type = Main.tile[x, y2].type;
							if( type == TileID.Dirt || type == TileID.Grass || type == TileID.CorruptGrass || type == TileID.ClayBlock || type == TileID.Sand )
							{
								Main.tile[x, y2].type = TileID.SnowBlock;
							}
							else if( type == TileID.Stone )
							{
								Main.tile[x, y2].type = TileID.IceBlock;
							}
						}
					}
				}
				if( snowBottom < y )
				{
					snowBottom = y;
				}
			}
		}

		private static void GrassFunc( GenerationProgress progress )
		{
			for( int k = 0; k < (int)((double)(Main.maxTilesX * Main.maxTilesY) * 0.0002); k++ )
			{
				int num = WorldGen.genRand.Next( 1, Main.maxTilesX - 1 );
				int num2 = WorldGen.genRand.Next( (int)worldSurfaceMin, (int)worldSurfaceMax );
				if( num2 >= Main.maxTilesY )
				{
					num2 = Main.maxTilesY - 2;
				}
				if( Main.tile[num - 1, num2].active() && Main.tile[num - 1, num2].type == 0 && Main.tile[num + 1, num2].active() && Main.tile[num + 1, num2].type == 0 && Main.tile[num, num2 - 1].active() && Main.tile[num, num2 - 1].type == 0 && Main.tile[num, num2 + 1].active() && Main.tile[num, num2 + 1].type == 0 )
				{
					Main.tile[num, num2].active( true );
					Main.tile[num, num2].type = 2;
				}
				num = WorldGen.genRand.Next( 1, Main.maxTilesX - 1 );
				num2 = WorldGen.genRand.Next( 0, (int)worldSurfaceMin );
				if( num2 >= Main.maxTilesY )
				{
					num2 = Main.maxTilesY - 2;
				}
				if( Main.tile[num - 1, num2].active() && Main.tile[num - 1, num2].type == 0 && Main.tile[num + 1, num2].active() && Main.tile[num + 1, num2].type == 0 && Main.tile[num, num2 - 1].active() && Main.tile[num, num2 - 1].type == 0 && Main.tile[num, num2 + 1].active() && Main.tile[num, num2 + 1].type == 0 )
				{
					Main.tile[num, num2].active( true );
					Main.tile[num, num2].type = 2;
				}
			}
		}


		private static void JungleFunc( GenerationProgress progress )
		{
			progress.Message = Lang.gen[11].Value;
			float num = (float)(Main.maxTilesX / 4200);
			num *= 1.5f;
			float num2 = (float)WorldGen.genRand.Next( 15, 30 ) * 0.01f;
			int num3;
			if( dungeonSide == -1 )
			{
				num2 = 1f - num2;
				num3 = (int)((float)Main.maxTilesX * num2);
			}
			else
			{
				num3 = (int)((float)Main.maxTilesX * num2);
			}
			int num4 = (int)((double)Main.maxTilesY + Main.rockLayer) / 2;
			num3 += WorldGen.genRand.Next( (int)(-100f * num), (int)(101f * num) );
			num4 += WorldGen.genRand.Next( (int)(-100f * num), (int)(101f * num) );
			int num5 = num3;
			int num6 = num4;
			WorldGenUtils.TileRunner( num3, num4, (double)WorldGen.genRand.Next( (int)(250f * num), (int)(500f * num) ), WorldGen.genRand.Next( 50, 150 ), 59, false, (float)(dungeonSide * 3), 0f, false, true );
			int num7 = 0;
			while( (float)num7 < 6f * num )
			{
				WorldGenUtils.TileRunner( num3 + WorldGen.genRand.Next( -(int)(125f * num), (int)(125f * num) ), num4 + WorldGen.genRand.Next( -(int)(125f * num), (int)(125f * num) ), (double)WorldGen.genRand.Next( 3, 7 ), WorldGen.genRand.Next( 3, 8 ), WorldGen.genRand.Next( 63, 65 ), false, 0f, 0f, false, true );
				num7++;
			}
			mudWall = true;
			progress.Set( 0.15f );
			num3 += WorldGen.genRand.Next( (int)(-250f * num), (int)(251f * num) );
			num4 += WorldGen.genRand.Next( (int)(-150f * num), (int)(151f * num) );
			int num8 = num3;
			int num9 = num4;
			int num10 = num3;
			int num11 = num4;
			WorldGenUtils.TileRunner( num3, num4, (double)WorldGen.genRand.Next( (int)(250f * num), (int)(500f * num) ), WorldGen.genRand.Next( 50, 150 ), 59, false, 0f, 0f, false, true );
			mudWall = false;
			int num12 = 0;
			while( (float)num12 < 6f * num )
			{
				WorldGenUtils.TileRunner( num3 + WorldGen.genRand.Next( -(int)(125f * num), (int)(125f * num) ), num4 + WorldGen.genRand.Next( -(int)(125f * num), (int)(125f * num) ), (double)WorldGen.genRand.Next( 3, 7 ), WorldGen.genRand.Next( 3, 8 ), WorldGen.genRand.Next( 65, 67 ), false, 0f, 0f, false, true );
				num12++;
			}
			mudWall = true;
			progress.Set( 0.3f );
			num3 += WorldGen.genRand.Next( (int)(-400f * num), (int)(401f * num) );
			num4 += WorldGen.genRand.Next( (int)(-150f * num), (int)(151f * num) );
			int num13 = num3;
			int num14 = num4;
			WorldGenUtils.TileRunner( num3, num4, (double)WorldGen.genRand.Next( (int)(250f * num), (int)(500f * num) ), WorldGen.genRand.Next( 50, 150 ), 59, false, (float)(dungeonSide * -3), 0f, false, true );
			mudWall = false;
			int num15 = 0;
			while( (float)num15 < 6f * num )
			{
				WorldGenUtils.TileRunner( num3 + WorldGen.genRand.Next( -(int)(125f * num), (int)(125f * num) ), num4 + WorldGen.genRand.Next( -(int)(125f * num), (int)(125f * num) ), (double)WorldGen.genRand.Next( 3, 7 ), WorldGen.genRand.Next( 3, 8 ), WorldGen.genRand.Next( 67, 69 ), false, 0f, 0f, false, true );
				num15++;
			}
			mudWall = true;
			progress.Set( 0.45f );
			num3 = (num5 + num8 + num13) / 3;
			num4 = (num6 + num9 + num14) / 3;
			WorldGenUtils.TileRunner( num3, num4, (double)WorldGen.genRand.Next( (int)(400f * num), (int)(600f * num) ), 10000, 59, false, 0f, -20f, true, true );
			WorldGen.JungleRunner( num3, num4 );
			progress.Set( 0.6f );
			mudWall = false;
			for( int k = 0; k < Main.maxTilesX / 4; k++ )
			{
				num3 = WorldGen.genRand.Next( 20, Main.maxTilesX - 20 );
				num4 = WorldGen.genRand.Next( (int)worldSurface + 10, Main.maxTilesY - 200 );
				while( Main.tile[num3, num4].wall != 64 && Main.tile[num3, num4].wall != 15 )
				{
					num3 = WorldGen.genRand.Next( 20, Main.maxTilesX - 20 );
					num4 = WorldGen.genRand.Next( (int)worldSurface + 10, Main.maxTilesY - 200 );
				}
				WorldGen.MudWallRunner( num3, num4 );
			}
			num3 = num10;
			num4 = num11;
			int num16 = 0;
			while( (float)num16 <= 20f * num )
			{
				progress.Set( (60f + (float)num16 / num) * 0.01f );
				num3 += WorldGen.genRand.Next( (int)(-5f * num), (int)(6f * num) );
				num4 += WorldGen.genRand.Next( (int)(-5f * num), (int)(6f * num) );
				WorldGenUtils.TileRunner( num3, num4, (double)WorldGen.genRand.Next( 40, 100 ), WorldGen.genRand.Next( 300, 500 ), 59, false, 0f, 0f, false, true );
				num16++;
			}
			int num17 = 0;
			while( (float)num17 <= 10f * num )
			{
				progress.Set( (80f + (float)num17 / num * 2f) * 0.01f );
				num3 = num10 + WorldGen.genRand.Next( (int)(-600f * num), (int)(600f * num) );
				num4 = num11 + WorldGen.genRand.Next( (int)(-200f * num), (int)(200f * num) );
				while( num3 < 1 || num3 >= Main.maxTilesX - 1 || num4 < 1 || num4 >= Main.maxTilesY - 1 || Main.tile[num3, num4].type != 59 )
				{
					num3 = num10 + WorldGen.genRand.Next( (int)(-600f * num), (int)(600f * num) );
					num4 = num11 + WorldGen.genRand.Next( (int)(-200f * num), (int)(200f * num) );
				}
				int num18 = 0;
				while( (float)num18 < 8f * num )
				{
					num3 += WorldGen.genRand.Next( -30, 31 );
					num4 += WorldGen.genRand.Next( -30, 31 );
					int type = -1;
					if( WorldGen.genRand.Next( 7 ) == 0 )
					{
						type = -2;
					}
					WorldGenUtils.TileRunner( num3, num4, (double)WorldGen.genRand.Next( 10, 20 ), WorldGen.genRand.Next( 30, 70 ), type, false, 0f, 0f, false, true );
					num18++;
				}
				num17++;
			}
			int num19 = 0;
			while( (float)num19 <= 300f * num )
			{
				num3 = num10 + WorldGen.genRand.Next( (int)(-600f * num), (int)(600f * num) );
				num4 = num11 + WorldGen.genRand.Next( (int)(-200f * num), (int)(200f * num) );
				while( num3 < 1 || num3 >= Main.maxTilesX - 1 || num4 < 1 || num4 >= Main.maxTilesY - 1 || Main.tile[num3, num4].type != 59 )
				{
					num3 = num10 + WorldGen.genRand.Next( (int)(-600f * num), (int)(600f * num) );
					num4 = num11 + WorldGen.genRand.Next( (int)(-200f * num), (int)(200f * num) );
				}
				WorldGenUtils.TileRunner( num3, num4, (double)WorldGen.genRand.Next( 4, 10 ), WorldGen.genRand.Next( 5, 30 ), 1, false, 0f, 0f, false, true );
				if( WorldGen.genRand.Next( 4 ) == 0 )
				{
					int type2 = WorldGen.genRand.Next( 63, 69 );
					WorldGenUtils.TileRunner( num3 + WorldGen.genRand.Next( -1, 2 ), num4 + WorldGen.genRand.Next( -1, 2 ), (double)WorldGen.genRand.Next( 3, 7 ), WorldGen.genRand.Next( 4, 8 ), type2, false, 0f, 0f, false, true );
				}
				num19++;
			}
		}

		private static void MudCavesToGrassFunc( GenerationProgress progress )
		{
			progress.Message = Lang.gen[77].Value;
			for( int k = 0; k < Main.maxTilesX; k++ )
			{
				for( int l = 0; l < Main.maxTilesY; l++ )
				{
					if( Main.tile[k, l].active() )
					{
						grassSpread = 0;
						WorldGen.SpreadGrass( k, l, 59, 60, true, 0 );
					}
					progress.Set( 0.2f * ((float)(k * Main.maxTilesY + l) / (float)(Main.maxTilesX * Main.maxTilesY)) );
				}
			}
			for( int m = 10; m < Main.maxTilesX - 10; m++ )
			{
				for( int n = 10; n < Main.maxTilesY - 10; n++ )
				{
					if( Main.tile[m, n].active() && WorldGen.tileCounter( m, n ) < tileCounterMax )
					{
						WorldGen.tileCounterKill();
					}
					float num = (float)((m - 10) * (Main.maxTilesY - 20) + (n - 10)) / (float)((Main.maxTilesX - 20) * (Main.maxTilesY - 20));
					progress.Set( 0.2f + num * 0.8f );
				}
			}
		}

		private static void MudToDirtFunc( GenerationProgress progress )
		{
			progress.Message = Lang.gen[14].Value;

			for( int k = 0; k < (int)(Main.maxTilesX * Main.maxTilesY * 0.0004); k++ ) // 0.001
			{
				int x = WorldGen.genRand.Next( 0, Main.maxTilesX );
				int y = WorldGen.genRand.Next( (int)worldSurfaceMin, (int)(Main.maxTilesY * 0.75) );
				if( Main.tile[x, y].type == TileID.Dirt )
				{
					WorldGenUtils.TileRunner( x, y, WorldGen.genRand.Next( 5, 10 ), WorldGen.genRand.Next( 8, 16 ), TileID.Mud );
				}
			}
		}

		// calls external function.
		private static void FullDesertFunc( GenerationProgress progress )
		{
			progress.Message = Lang.gen[78].Value;
			int num = dungeonSide;
			int num2 = Main.maxTilesX / 2;
			int num3 = WorldGen.genRand.Next( num2 ) / 8;
			num3 += num2 / 8;
			int x = num2 + num3 * -num;
			int num4 = 0;
			while( !Biomes<DesertBiome>.Place( new Point( x, (int)worldSurface ), structures ) )
			{
				num3 = WorldGen.genRand.Next( num2 ) / 2;
				num3 += num2 / 8;
				x = num2 + num3 * -num;
				if( ++num4 > 1000 )
				{
					num *= -1;
					num4 = 0;
				}
			}
		}

		private static void FloatingIslandsFunc( GenerationProgress progress )
		{
			numIslandHouses = 0;
			houseCount = 0;
			progress.Message = Lang.gen[12].Value;
			for( int k = 0; k < (int)((double)Main.maxTilesX * 0.0008) + skyLakes; k++ )
			{
				int num = 1000;
				int x = WorldGen.genRand.Next( (int)((double)Main.maxTilesX * 0.1), (int)((double)Main.maxTilesX * 0.9) );
				while( --num > 0 )
				{
					bool flag2 = true;
					while( x > Main.maxTilesX / 2 - 80 && x < Main.maxTilesX / 2 + 80 )
					{
						x = WorldGen.genRand.Next( (int)((double)Main.maxTilesX * 0.1), (int)((double)Main.maxTilesX * 0.9) );
					}
					for( int l = 0; l < numIslandHouses; l++ )
					{
						if( x > floatingIslandHouseX[l] - 180 && x < floatingIslandHouseX[l] + 180 )
						{
							flag2 = false;
							break;
						}
					}
					if( flag2 )
					{
						flag2 = false;
						int ysurf = 0;
						int num4 = 200;
						while( (double)num4 < Main.worldSurface )
						{
							if( Main.tile[x, num4].active() )
							{
								ysurf = num4;
								flag2 = true;
								break;
							}
							num4++;
						}
						if( flag2 )
						{
							int y = WorldGen.genRand.Next( 90, ysurf - 100 );
							y = Math.Min( y, (int)worldSurfaceMin - 50 );
							if( k < skyLakes )
							{
								isSkyLake[numIslandHouses] = true;
								WorldGen.CloudLake( x, y );
							}
							else
							{
								WorldGen.CloudIsland( x, y );
							}
							floatingIslandHouseX[numIslandHouses] = x;
							floatingIslandHouseY[numIslandHouses] = y;
							numIslandHouses++;
						}
					}
				}
			}
		}

		private static void MushroomPatchesFunc( GenerationProgress progress )
		{
			progress.Message = Lang.gen[13].Value;
			for( int k = 0; k < Main.maxTilesX / 500; k++ )
			{
				int num = 0;
				bool flag2 = true;
				while( flag2 )
				{
					int num2 = WorldGen.genRand.Next( (int)((double)Main.maxTilesX * 0.3), (int)((double)Main.maxTilesX * 0.7) );
					int num3 = WorldGen.genRand.Next( (int)Main.rockLayer, Main.maxTilesY - 350 );
					flag2 = false;
					int num4 = 60;
					for( int l = num2 - num4; l < num2 + num4; l += 3 )
					{
						for( int m = num3 - num4; m < num3 + num4; m += 3 )
						{
							if( Main.tile[l, m].type == 147 || Main.tile[l, m].type == 161 || Main.tile[l, m].type == 162 )
							{
								flag2 = true;
								break;
							}
							if( WorldGen.UndergroundDesertLocation.Contains( new Point( l, m ) ) )
							{
								flag2 = true;
								break;
							}
						}
					}
					if( !flag2 )
					{
						WorldGen.ShroomPatch( num2, num3 );
					}
					num++;
					if( num > 100 )
					{
						break;
					}
				}
			}
			for( int n = 0; n < Main.maxTilesX; n++ )
			{
				for( int num5 = (int)Main.worldSurface; num5 < Main.maxTilesY; num5++ )
				{
					if( Main.tile[n, num5].active() )
					{
						grassSpread = 0;
						WorldGen.SpreadGrass( n, num5, 59, 70, false, 0 );
						if( Main.tile[n, num5].type == 70 && WorldGen.genRand.Next( 20 ) == 0 )
						{
							int num6;
							if( WorldGen.genRand.Next( 5 ) == 0 )
							{
								num6 = 2;
							}
							else
							{
								num6 = 1;
							}
							int num7 = WorldGen.genRand.Next( 2, 6 );
							int num8 = num5 - num7;
							bool flag3 = true;
							for( int num9 = n - num6; num9 <= n + num6; num9++ )
							{
								if( Main.tile[num9, num8].active() )
								{
									flag3 = false;
								}
								if( Main.tileBrick[(int)Main.tile[num9, num8 - 1].type] )
								{
									flag3 = false;
								}
								if( Main.tileBrick[(int)Main.tile[num9, num8 + 1].type] )
								{
									flag3 = false;
								}
							}
							if( Main.tile[n - num6 - 1, num8].type == 190 )
							{
								flag3 = false;
							}
							if( Main.tile[n + num6 + 1, num8].type == 190 )
							{
								flag3 = false;
							}
							for( int num10 = num8; num10 < num5; num10++ )
							{
								if( Main.tile[n, num10].active() )
								{
									flag3 = false;
								}
								if( Main.tileBrick[(int)Main.tile[n - 1, num10].type] )
								{
									flag3 = false;
								}
								if( Main.tileBrick[(int)Main.tile[n + 1, num10].type] )
								{
									flag3 = false;
								}
							}
							if( flag3 )
							{
								for( int num11 = n - num6; num11 <= n + num6; num11++ )
								{
									WorldGen.PlaceTile( num11, num8, 190, true, true, -1, 0 );
								}
								for( int num12 = num8; num12 < num5; num12++ )
								{
									WorldGen.PlaceTile( n, num12, 190, true, true, -1, 0 );
								}
							}
						}
					}
				}
			}
		}

		private static void SiltFunc( GenerationProgress progress )
		{
			progress.Message = Lang.gen[15].Value;
			for( int k = 0; k < (int)((double)(Main.maxTilesX * Main.maxTilesY) * 0.0001); k++ )
			{
				WorldGenUtils.TileRunner( WorldGen.genRand.Next( 0, Main.maxTilesX ), WorldGen.genRand.Next( (int)rockLayerMax, Main.maxTilesY ), (double)WorldGen.genRand.Next( 5, 12 ), WorldGen.genRand.Next( 15, 50 ), 123, false, 0f, 0f, false, true );
			}
			for( int l = 0; l < (int)((double)(Main.maxTilesX * Main.maxTilesY) * 0.0005); l++ )
			{
				WorldGenUtils.TileRunner( WorldGen.genRand.Next( 0, Main.maxTilesX ), WorldGen.genRand.Next( (int)rockLayerMax, Main.maxTilesY ), (double)WorldGen.genRand.Next( 2, 5 ), WorldGen.genRand.Next( 2, 5 ), 123, false, 0f, 0f, false, true );
			}
		}
		
		private static void ShiniesFunc( GenerationProgress progress )
		{
			// worldSurfaceLow is actually the highest surface tile. In practice you might want to use WorldGen.rockLayer or other WorldGen values.
			progress.Message = "Shinies";
			

			// COPPER (top, mid, - )

			for( int i = 0; i < (int)(Main.maxTilesX * Main.maxTilesY * 0.000070); i++ )
				WorldGenUtils.TileRunner( WorldGen.genRand.Next( 0, Main.maxTilesX ), WorldGen.genRand.Next( (int)worldSurfaceMin, (int)worldSurfaceMax ), WorldGen.genRand.Next( 4, 6 ), WorldGen.genRand.Next( 2, 6 ), copperOreId );

			for( int i = 0; i < (int)(Main.maxTilesX * Main.maxTilesY * 0.000090); i++ )
				WorldGenUtils.TileRunner( WorldGen.genRand.Next( 0, Main.maxTilesX ), WorldGen.genRand.Next( (int)worldSurfaceMax, (int)(Main.maxTilesY * 0.6) ), WorldGen.genRand.Next( 4, 7 ), WorldGen.genRand.Next( 3, 7 ), copperOreId );

			//for( int i = 0; i < (int)((Main.maxTilesX * Main.maxTilesY) * 0.0002); i++ )
			//	WorldGenUtils.TileRunner( WorldGen.genRand.Next( 0, Main.maxTilesX ), WorldGen.genRand.Next( (int)rockLayerLow, Main.maxTilesY ), WorldGen.genRand.Next( 4, 9 ), WorldGen.genRand.Next( 4, 8 ), copperOreId );

			// IRON (top, mid, - )

			for( int i = 0; i < (int)(Main.maxTilesX * Main.maxTilesY * 0.000040); i++ )
				WorldGenUtils.TileRunner( WorldGen.genRand.Next( 0, Main.maxTilesX ), WorldGen.genRand.Next( (int)worldSurfaceMin, (int)worldSurfaceMax ), WorldGen.genRand.Next( 4, 6 ), WorldGen.genRand.Next( 2, 6 ), ironOreId );

			for( int i = 0; i < (int)(Main.maxTilesX * Main.maxTilesY * 0.000120); i++ )
				WorldGenUtils.TileRunner( WorldGen.genRand.Next( 0, Main.maxTilesX ), WorldGen.genRand.Next( (int)worldSurfaceMax, (int)(Main.maxTilesY * 0.75) ), WorldGen.genRand.Next( 4, 8 ), WorldGen.genRand.Next( 3, 7 ), ironOreId );

			//for( int i = 0; i < (int)((Main.maxTilesX * Main.maxTilesY) * 0.0002); i++ )
			//	WorldGenUtils.TileRunner( WorldGen.genRand.Next( 0, Main.maxTilesX ), WorldGen.genRand.Next( (int)rockLayerLow, Main.maxTilesY ), WorldGen.genRand.Next( 4, 9 ), WorldGen.genRand.Next( 4, 8 ), ironOreId );

			// SILVER (topt, mid, low )

			//for( int i = 0; i < (int)(Main.maxTilesX * Main.maxTilesY * 0.00017); i++ )
			//	WorldGenUtils.TileRunner( WorldGen.genRand.Next( 0, Main.maxTilesX ), WorldGen.genRand.Next( (int)worldSurfaceMin, (int)worldSurfaceMax ), WorldGen.genRand.Next( 4, 9 ), WorldGen.genRand.Next( 4, 8 ), silverOreId );

			for( int i = 0; i < (int)(Main.maxTilesX * Main.maxTilesY * 0.000050); i++ )
				WorldGenUtils.TileRunner( WorldGen.genRand.Next( 0, Main.maxTilesX ), WorldGen.genRand.Next( (int)worldSurfaceMax, (int)rockLayerMin ), WorldGen.genRand.Next( 3, 6 ), WorldGen.genRand.Next( 3, 6 ), silverOreId );

			for( int i = 0; i < (int)(Main.maxTilesX * Main.maxTilesY * 0.000150); i++ )
				WorldGenUtils.TileRunner( WorldGen.genRand.Next( 0, Main.maxTilesX ), WorldGen.genRand.Next( (int)rockLayerMin, Main.maxTilesY ), WorldGen.genRand.Next( 5, 9 ), WorldGen.genRand.Next( 4, 8 ), silverOreId );

			// GOLD (topt, mid, low )

			//for( int i = 0; i < (int)(Main.maxTilesX * Main.maxTilesY * 0.00012); i++ )
			//	WorldGenUtils.TileRunner( WorldGen.genRand.Next( 0, Main.maxTilesX ), WorldGen.genRand.Next( (int)worldSurfaceMin, (int)worldSurfaceMax ), WorldGen.genRand.Next( 4, 9 ), WorldGen.genRand.Next( 4, 8 ), goldOreId );

			for( int i = 0; i < (int)(Main.maxTilesX * Main.maxTilesY * 0.000040); i++ )
				WorldGenUtils.TileRunner( WorldGen.genRand.Next( 0, Main.maxTilesX ), WorldGen.genRand.Next( (int)worldSurfaceMax, (int)rockLayerMin ), WorldGen.genRand.Next( 3, 5 ), WorldGen.genRand.Next( 3, 6 ), goldOreId );

			for( int i = 0; i < (int)(Main.maxTilesX * Main.maxTilesY * 0.000130); i++ )
				WorldGenUtils.TileRunner( WorldGen.genRand.Next( 0, Main.maxTilesX ), WorldGen.genRand.Next( (int)rockLayerMin, Main.maxTilesY ), WorldGen.genRand.Next( 4, 9 ), WorldGen.genRand.Next( 4, 8 ), goldOreId );


			// COAL

			for( int i = 0; i < (int)((Main.maxTilesX * Main.maxTilesY) * 0.00003); i++ )
				WorldGenUtils.TileRunner( WorldGen.genRand.Next( 0, Main.maxTilesX ), WorldGen.genRand.Next( (int)worldSurfaceMin, (int)(Main.maxTilesY * 0.666) ), WorldGen.genRand.Next( 7, 10 ), WorldGen.genRand.Next( 20, 32 ), ModMain.instance.TileType( "Coal" ) );

			if( MyWorld.evilCombo == EvilCombo.Corruption )
			{
				for( int i = 0; i < (int)((Main.maxTilesX * Main.maxTilesY) * 0.000020); i++ )
				{
					WorldGenUtils.TileRunner( WorldGen.genRand.Next( 0, Main.maxTilesX ), WorldGen.genRand.Next( (int)worldSurfaceMax, Main.maxTilesY ), WorldGen.genRand.Next( 3, 4 ), WorldGen.genRand.Next( 3, 6 ), TileID.Demonite );
				}
			}
			else
			{
				for( int i = 0; i < (int)((Main.maxTilesX * Main.maxTilesY) * 0.000020); i++ )
				{
					WorldGenUtils.TileRunner( WorldGen.genRand.Next( 0, Main.maxTilesX ), WorldGen.genRand.Next( (int)worldSurfaceMax, Main.maxTilesY ), WorldGen.genRand.Next( 3, 4 ), WorldGen.genRand.Next( 3, 6 ), TileID.Crimtane );
				}
			}
		}

		private static void WebsFunc( GenerationProgress progress )
		{
			progress.Message = Lang.gen[17].Value;
			for( int k = 0; k < (int)((double)(Main.maxTilesX * Main.maxTilesY) * 0.0006); k++ )
			{
				int num = WorldGen.genRand.Next( 20, Main.maxTilesX - 20 );
				int num2 = WorldGen.genRand.Next( (int)worldSurfaceMax, Main.maxTilesY - 20 );
				if( k < moundCaveCount )
				{
					num = moundCaveX[k];
					num2 = moundCaveY[k];
				}
				if( !Main.tile[num, num2].active() )
				{
					if( (double)num2 <= Main.worldSurface )
					{
						if( Main.tile[num, num2].wall <= 0 )
						{
							return;
						}
					}
					while( !Main.tile[num, num2].active() && num2 > (int)worldSurfaceMin )
					{
						num2--;
					}
					num2++;
					int num3 = 1;
					if( WorldGen.genRand.Next( 2 ) == 0 )
					{
						num3 = -1;
					}
					while( !Main.tile[num, num2].active() && num > 10 && num < Main.maxTilesX - 10 )
					{
						num += num3;
					}
					num -= num3;
					if( (double)num2 > Main.worldSurface || Main.tile[num, num2].wall > 0 )
					{
						WorldGenUtils.TileRunner( num, num2, (double)WorldGen.genRand.Next( 4, 11 ), WorldGen.genRand.Next( 2, 4 ), 51, true, (float)num3, -1f, false, false );
					}
				}
			}
		}

		private static void UnderworldFunc( GenerationProgress progress )
		{
			progress.Message = Lang.gen[18].Value;
			progress.Set( 0f );
			int num = Main.maxTilesY - WorldGen.genRand.Next( 150, 190 );
			for( int k = 0; k < Main.maxTilesX; k++ )
			{
				num += WorldGen.genRand.Next( -3, 4 );
				if( num < Main.maxTilesY - 190 )
				{
					num = Main.maxTilesY - 190;
				}
				if( num > Main.maxTilesY - 160 )
				{
					num = Main.maxTilesY - 160;
				}
				for( int l = num - 20 - WorldGen.genRand.Next( 3 ); l < Main.maxTilesY; l++ )
				{
					if( l >= num )
					{
						Main.tile[k, l].active( false );
						Main.tile[k, l].lava( false );
						Main.tile[k, l].liquid = 0;
					}
					else
					{
						Main.tile[k, l].type = 57;
					}
				}
			}
			int num2 = Main.maxTilesY - WorldGen.genRand.Next( 40, 70 );
			for( int m = 10; m < Main.maxTilesX - 10; m++ )
			{
				num2 += WorldGen.genRand.Next( -10, 11 );
				if( num2 > Main.maxTilesY - 60 )
				{
					num2 = Main.maxTilesY - 60;
				}
				if( num2 < Main.maxTilesY - 100 )
				{
					num2 = Main.maxTilesY - 120;
				}
				for( int n = num2; n < Main.maxTilesY - 10; n++ )
				{
					if( !Main.tile[m, n].active() )
					{
						Main.tile[m, n].lava( true );
						Main.tile[m, n].liquid = byte.MaxValue;
					}
				}
			}
			for( int num3 = 0; num3 < Main.maxTilesX; num3++ )
			{
				if( WorldGen.genRand.Next( 50 ) == 0 )
				{
					int num4 = Main.maxTilesY - 65;
					while( !Main.tile[num3, num4].active() && num4 > Main.maxTilesY - 135 )
					{
						num4--;
					}
					WorldGenUtils.TileRunner( WorldGen.genRand.Next( 0, Main.maxTilesX ), num4 + WorldGen.genRand.Next( 20, 50 ), (double)WorldGen.genRand.Next( 15, 20 ), 1000, 57, true, 0f, (float)WorldGen.genRand.Next( 1, 3 ), true, true );
				}
			}
			Liquid.QuickWater( -2, -1, -1 );
			for( int num5 = 0; num5 < Main.maxTilesX; num5++ )
			{
				float num6 = (float)num5 / (float)(Main.maxTilesX - 1);
				progress.Set( num6 / 2f + 0.5f );
				if( WorldGen.genRand.Next( 13 ) == 0 )
				{
					int num7 = Main.maxTilesY - 65;
					while( (Main.tile[num5, num7].liquid > 0 || Main.tile[num5, num7].active()) && num7 > Main.maxTilesY - 140 )
					{
						num7--;
					}
					WorldGenUtils.TileRunner( num5, num7 - WorldGen.genRand.Next( 2, 5 ), (double)WorldGen.genRand.Next( 5, 30 ), 1000, 57, true, 0f, (float)WorldGen.genRand.Next( 1, 3 ), true, true );
					float num8 = (float)WorldGen.genRand.Next( 1, 3 );
					if( WorldGen.genRand.Next( 3 ) == 0 )
					{
						num8 *= 0.5f;
					}
					if( WorldGen.genRand.Next( 2 ) == 0 )
					{
						WorldGenUtils.TileRunner( num5, num7 - WorldGen.genRand.Next( 2, 5 ), (double)((int)((float)WorldGen.genRand.Next( 5, 15 ) * num8)), (int)((float)WorldGen.genRand.Next( 10, 15 ) * num8), 57, true, 1f, 0.3f, false, true );
					}
					if( WorldGen.genRand.Next( 2 ) == 0 )
					{
						num8 = (float)WorldGen.genRand.Next( 1, 3 );
						WorldGenUtils.TileRunner( num5, num7 - WorldGen.genRand.Next( 2, 5 ), (double)((int)((float)WorldGen.genRand.Next( 5, 15 ) * num8)), (int)((float)WorldGen.genRand.Next( 10, 15 ) * num8), 57, true, -1f, 0.3f, false, true );
					}
					WorldGenUtils.TileRunner( num5 + WorldGen.genRand.Next( -10, 10 ), num7 + WorldGen.genRand.Next( -10, 10 ), (double)WorldGen.genRand.Next( 5, 15 ), WorldGen.genRand.Next( 5, 10 ), -2, false, (float)WorldGen.genRand.Next( -1, 3 ), (float)WorldGen.genRand.Next( -1, 3 ), false, true );
					if( WorldGen.genRand.Next( 3 ) == 0 )
					{
						WorldGenUtils.TileRunner( num5 + WorldGen.genRand.Next( -10, 10 ), num7 + WorldGen.genRand.Next( -10, 10 ), (double)WorldGen.genRand.Next( 10, 30 ), WorldGen.genRand.Next( 10, 20 ), -2, false, (float)WorldGen.genRand.Next( -1, 3 ), (float)WorldGen.genRand.Next( -1, 3 ), false, true );
					}
					if( WorldGen.genRand.Next( 5 ) == 0 )
					{
						WorldGenUtils.TileRunner( num5 + WorldGen.genRand.Next( -15, 15 ), num7 + WorldGen.genRand.Next( -15, 10 ), (double)WorldGen.genRand.Next( 15, 30 ), WorldGen.genRand.Next( 5, 20 ), -2, false, (float)WorldGen.genRand.Next( -1, 3 ), (float)WorldGen.genRand.Next( -1, 3 ), false, true );
					}
				}
			}
			for( int num9 = 0; num9 < Main.maxTilesX; num9++ )
			{
				WorldGenUtils.TileRunner( WorldGen.genRand.Next( 20, Main.maxTilesX - 20 ), WorldGen.genRand.Next( Main.maxTilesY - 180, Main.maxTilesY - 10 ), (double)WorldGen.genRand.Next( 2, 7 ), WorldGen.genRand.Next( 2, 7 ), -2, false, 0f, 0f, false, true );
			}
			for( int num10 = 0; num10 < Main.maxTilesX; num10++ )
			{
				if( !Main.tile[num10, Main.maxTilesY - 145].active() )
				{
					Main.tile[num10, Main.maxTilesY - 145].liquid = byte.MaxValue;
					Main.tile[num10, Main.maxTilesY - 145].lava( true );
				}
				if( !Main.tile[num10, Main.maxTilesY - 144].active() )
				{
					Main.tile[num10, Main.maxTilesY - 144].liquid = byte.MaxValue;
					Main.tile[num10, Main.maxTilesY - 144].lava( true );
				}
			}
			for( int num11 = 0; num11 < (int)((double)(Main.maxTilesX * Main.maxTilesY) * 0.0008); num11++ )
			{
				WorldGenUtils.TileRunner( WorldGen.genRand.Next( 0, Main.maxTilesX ), WorldGen.genRand.Next( Main.maxTilesY - 140, Main.maxTilesY ), (double)WorldGen.genRand.Next( 2, 7 ), WorldGen.genRand.Next( 3, 7 ), 58, false, 0f, 0f, false, true );
			}
			WorldGen.AddHellHouses();
		}

		private static void LakesFunc( GenerationProgress progress )
		{
			progress.Message = Lang.gen[19].Value;
			int num = WorldGen.genRand.Next( 2, (int)((double)Main.maxTilesX * 0.005) );
			for( int k = 0; k < num; k++ )
			{
				float value = (float)k / (float)num;
				progress.Set( value );
				int num2 = WorldGen.genRand.Next( 300, Main.maxTilesX - 300 );
				while( num2 > Main.maxTilesX / 2 - 100 && num2 < Main.maxTilesX / 2 + 100 )
				{
					num2 = WorldGen.genRand.Next( 300, Main.maxTilesX - 300 );
				}
				int num3 = (int)worldSurfaceMin - 20;
				while( !Main.tile[num2, num3].active() )
				{
					num3++;
				}
				WorldGen.Lakinater( num2, num3 );
			}
		}

		// calls external function. Might break.
		private static void DungeonFunc( GenerationProgress progress )
		{
			if( dungeonSide == -1 )
			{
				int x = WorldGen.genRand.Next( (int)((double)Main.maxTilesX * 0.05), (int)((double)Main.maxTilesX * 0.2) );
				int y = (int)((Main.worldSurface + Main.rockLayer) / 2.0) + WorldGen.genRand.Next( -200, 200 );
				WorldGen.MakeDungeon( x, y );
			}
			else
			{
				int x = WorldGen.genRand.Next( (int)((double)Main.maxTilesX * 0.8), (int)((double)Main.maxTilesX * 0.95) );
				int y = (int)((Main.worldSurface + Main.rockLayer) / 2.0) + WorldGen.genRand.Next( -200, 200 );
				WorldGen.MakeDungeon( x, y );
			}
		}

		private static void CorruptionFunc( GenerationProgress progress )
		{
			if( MyWorld.evilCombo == EvilCombo.Crimson )
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
						if( num5 < 200 && JungleX > num3 && JungleX < num4 )
						{
							num5++;
							flag2 = false;
						}
					}
					WorldGen.CrimStart( num2, (int)worldSurfaceMin - 10 );
					for( int m = num3; m < num4; m++ )
					{
						int num8 = (int)worldSurfaceMin;
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
						int i2 = num11;
						bool flag3 = false;
						int num12 = (int)worldSurfaceMin;
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
									grassSpread = 0;
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
			}
			else
			{
				progress.Message = Lang.gen[20].Value;
				int num19 = 0;
				while( (double)num19 < (double)Main.maxTilesX * 0.00045 )
				{
					float value2 = (float)((double)num19 / ((double)Main.maxTilesX * 0.00045));
					progress.Set( value2 );
					bool flag5 = false;
					int xLonger = 0;
					int num21 = 0;
					int num22 = 0;
					while( !flag5 )
					{
						int num23 = 0;
						flag5 = true;
						int num24 = Main.maxTilesX / 2;
						int num25 = 200;
						xLonger = WorldGen.genRand.Next( 320, Main.maxTilesX - 320 );
						num21 = xLonger - WorldGen.genRand.Next( 200 ) - 100;
						num22 = xLonger + WorldGen.genRand.Next( 200 ) + 100;
						if( num21 < 285 )
						{
							num21 = 285;
						}
						if( num22 > Main.maxTilesX - 285 )
						{
							num22 = Main.maxTilesX - 285;
						}
						if( xLonger > num24 - num25 && xLonger < num24 + num25 )
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
						if( xLonger > WorldGen.UndergroundDesertLocation.X && xLonger < WorldGen.UndergroundDesertLocation.X + WorldGen.UndergroundDesertLocation.Width )
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
						if( num23 < 200 && JungleX > num21 && JungleX < num22 )
						{
							num23++;
							flag5 = false;
						}
					}
					int num28 = 0;
					for( int chasmX = num21; chasmX < num22; chasmX++ )
					{
						if( num28 > 0 )
						{
							num28--;
						}
						if( chasmX == xLonger || num28 == 0 )
						{
							int chasmY = (int)worldSurfaceMin;
							while( (double)chasmY < Main.worldSurface - 1.0 )
							{
								if( Main.tile[chasmX, chasmY].active() || (Main.tile[chasmX, chasmY].wall > 0 && Main.tile[chasmX,chasmY].wall != WallID.Cloud) )
								{
									if( chasmX == xLonger )
									{
										num28 = 20;
										WorldGen.ChasmRunner( chasmX, chasmY, WorldGen.genRand.Next( 150 ) + 150, true );
										break;
									}
									if( WorldGen.genRand.Next( 35 ) == 0 && num28 == 0 )
									{
										num28 = 30;
										bool makeOrb = true;
										WorldGen.ChasmRunner( chasmX, chasmY, WorldGen.genRand.Next( 50 ) + 50, makeOrb );
										break;
									}
									break;
								}
								else
								{
									chasmY++;
								}
							}
						}
						int yMin = (int)worldSurfaceMin;
						while( (double)yMin < Main.worldSurface - 1.0 )
						{
							if( Main.tile[chasmX, yMin].active() )
							{
								int yMax = yMin + WorldGen.genRand.Next( 10, 14 );
								for( int y = yMin; y < yMax; y++ )
								{
									if( (Main.tile[chasmX, y].type == 59 || Main.tile[chasmX, y].type == 60) && chasmX >= num21 + WorldGen.genRand.Next( 5 ) && chasmX < num22 - WorldGen.genRand.Next( 5 ) )
									{
										Main.tile[chasmX, y].type = TileID.Dirt;
									}
								}
								break;
							}
							yMin++;
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
						int i2 = num35;
						bool flag6 = false;
						int num36 = (int)worldSurfaceMin;
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
									grassSpread = 0;
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
								int startX = num37 - 13;
								int endX = num37 + 13;
								int startY = num38 - 13;
								int endY = num38 + 13;
								for( int x = startX; x < endX; x++ )
								{
									if( x > 10 && x < Main.maxTilesX - 10 )
									{
										for( int y = startY; y < endY; y++ )
										{
											if( Math.Abs( x - num37 ) + Math.Abs( y - num38 ) < 9 + WorldGen.genRand.Next( 11 ) && WorldGen.genRand.Next( 3 ) != 0 && Main.tile[x, y].type != 31 )
											{
												Main.tile[x, y].active( true );
												Main.tile[x, y].type = 25;
												if( Math.Abs( x - num37 ) <= 1 && Math.Abs( y - num38 ) <= 1 )
												{
													Main.tile[x, y].active( false );
												}
											}
											if( Main.tile[x, y].type != 31 && Math.Abs( x - num37 ) <= 2 + WorldGen.genRand.Next( 3 ) && Math.Abs( y - num38 ) <= 2 + WorldGen.genRand.Next( 3 ) )
											{
												Main.tile[x, y].active( false );
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
		}

		private static void SlushFunc( GenerationProgress progress )
		{
			for( int k = snowTop; k < snowBottom; k++ )
			{
				for( int l = snowMinX[k]; l < snowMaxX[k]; l++ )
				{
					int type = (int)Main.tile[l, k].type;
					if( type == 123 )
					{
						Main.tile[l, k].type = 224;
					}
					else if( type == 59 )
					{
						bool flag2 = true;
						int num = 3;
						for( int m = l - num; m <= l + num; m++ )
						{
							for( int n = k - num; n <= k + num; n++ )
							{
								if( Main.tile[m, n].type == 60 || Main.tile[m, n].type == 70 || Main.tile[m, n].type == 71 || Main.tile[m, n].type == 72 )
								{
									flag2 = false;
									break;
								}
							}
						}
						if( flag2 )
						{
							Main.tile[l, k].type = 224;
						}
					}
					else if( type == 1 )
					{
						Main.tile[l, k].type = 161;
					}
				}
			}
		}


		private static void MudCavesToGrass2Func( GenerationProgress progress )
		{
			progress.Message = Lang.gen[21].Value;
			for( int k = 0; k < moundCaveCount; k++ )
			{
				int i2 = moundCaveX[k];
				int j2 = moundCaveY[k];
				WorldGen.CaveOpenater( i2, j2 );
				WorldGen.Cavinator( i2, j2, WorldGen.genRand.Next( 40, 50 ) );
			}
		}

		private static void BeachesFunc( GenerationProgress progress )
		{
			int num = 0;
			int num2 = 0;
			int num3 = 20;
			int num4 = Main.maxTilesX - 20;
			progress.Message = Lang.gen[22].Value;
			for( int k = 0; k < 2; k++ )
			{
				if( k == 0 )
				{
					int num5 = 0;
					int num6 = WorldGen.genRand.Next( 125, 200 ) + 50;
					if( dungeonSide == 1 )
					{
						num6 = 275;
					}
					int num7 = 0;
					float num8 = 1f;
					int num9 = 0;
					while( !Main.tile[num6 - 1, num9].active() )
					{
						num9++;
					}
					num = num9;
					num9 += WorldGen.genRand.Next( 1, 5 );
					for( int l = num6 - 1; l >= num5; l-- )
					{
						num7++;
						if( num7 < 3 )
						{
							num8 += (float)WorldGen.genRand.Next( 10, 20 ) * 0.2f;
						}
						else if( num7 < 6 )
						{
							num8 += (float)WorldGen.genRand.Next( 10, 20 ) * 0.15f;
						}
						else if( num7 < 9 )
						{
							num8 += (float)WorldGen.genRand.Next( 10, 20 ) * 0.1f;
						}
						else if( num7 < 15 )
						{
							num8 += (float)WorldGen.genRand.Next( 10, 20 ) * 0.07f;
						}
						else if( num7 < 50 )
						{
							num8 += (float)WorldGen.genRand.Next( 10, 20 ) * 0.05f;
						}
						else if( num7 < 75 )
						{
							num8 += (float)WorldGen.genRand.Next( 10, 20 ) * 0.04f;
						}
						else if( num7 < 100 )
						{
							num8 += (float)WorldGen.genRand.Next( 10, 20 ) * 0.03f;
						}
						else if( num7 < 125 )
						{
							num8 += (float)WorldGen.genRand.Next( 10, 20 ) * 0.02f;
						}
						else if( num7 < 150 )
						{
							num8 += (float)WorldGen.genRand.Next( 10, 20 ) * 0.01f;
						}
						else if( num7 < 175 )
						{
							num8 += (float)WorldGen.genRand.Next( 10, 20 ) * 0.005f;
						}
						else if( num7 < 200 )
						{
							num8 += (float)WorldGen.genRand.Next( 10, 20 ) * 0.001f;
						}
						else if( num7 < 230 )
						{
							num8 += (float)WorldGen.genRand.Next( 10, 20 ) * 0.01f;
						}
						else if( num7 < 235 )
						{
							num8 += (float)WorldGen.genRand.Next( 10, 20 ) * 0.05f;
						}
						else if( num7 < 240 )
						{
							num8 += (float)WorldGen.genRand.Next( 10, 20 ) * 0.1f;
						}
						else if( num7 < 245 )
						{
							num8 += (float)WorldGen.genRand.Next( 10, 20 ) * 0.05f;
						}
						else if( num7 < 255 )
						{
							num8 += (float)WorldGen.genRand.Next( 10, 20 ) * 0.01f;
						}
						if( num7 == 235 )
						{
							num4 = l;
						}
						if( num7 == 235 )
						{
							num3 = l;
						}
						int num10 = WorldGen.genRand.Next( 15, 20 );
						int num11 = 0;
						while( (float)num11 < (float)num9 + num8 + (float)num10 )
						{
							if( (float)num11 < (float)num9 + num8 * 0.75f - 3f )
							{
								Main.tile[l, num11].active( false );
								if( num11 > num9 )
								{
									Main.tile[l, num11].liquid = byte.MaxValue;
								}
								else if( num11 == num9 )
								{
									Main.tile[l, num11].liquid = 127;
								}
							}
							else if( num11 > num9 )
							{
								Main.tile[l, num11].type = 53;
								Main.tile[l, num11].active( true );
							}
							Main.tile[l, num11].wall = 0;
							num11++;
						}
					}
				}
				else
				{
					int num5 = Main.maxTilesX - WorldGen.genRand.Next( 125, 200 ) - 50;
					int num6 = Main.maxTilesX;
					if( dungeonSide == -1 )
					{
						num5 = Main.maxTilesX - 275;
					}
					float num12 = 1f;
					int num13 = 0;
					int num14 = 0;
					while( !Main.tile[num5, num14].active() )
					{
						num14++;
					}
					num2 = num14;
					num14 += WorldGen.genRand.Next( 1, 5 );
					for( int m = num5; m < num6; m++ )
					{
						num13++;
						if( num13 < 3 )
						{
							num12 += (float)WorldGen.genRand.Next( 10, 20 ) * 0.2f;
						}
						else if( num13 < 6 )
						{
							num12 += (float)WorldGen.genRand.Next( 10, 20 ) * 0.15f;
						}
						else if( num13 < 9 )
						{
							num12 += (float)WorldGen.genRand.Next( 10, 20 ) * 0.1f;
						}
						else if( num13 < 15 )
						{
							num12 += (float)WorldGen.genRand.Next( 10, 20 ) * 0.07f;
						}
						else if( num13 < 50 )
						{
							num12 += (float)WorldGen.genRand.Next( 10, 20 ) * 0.05f;
						}
						else if( num13 < 75 )
						{
							num12 += (float)WorldGen.genRand.Next( 10, 20 ) * 0.04f;
						}
						else if( num13 < 100 )
						{
							num12 += (float)WorldGen.genRand.Next( 10, 20 ) * 0.03f;
						}
						else if( num13 < 125 )
						{
							num12 += (float)WorldGen.genRand.Next( 10, 20 ) * 0.02f;
						}
						else if( num13 < 150 )
						{
							num12 += (float)WorldGen.genRand.Next( 10, 20 ) * 0.01f;
						}
						else if( num13 < 175 )
						{
							num12 += (float)WorldGen.genRand.Next( 10, 20 ) * 0.005f;
						}
						else if( num13 < 200 )
						{
							num12 += (float)WorldGen.genRand.Next( 10, 20 ) * 0.001f;
						}
						else if( num13 < 230 )
						{
							num12 += (float)WorldGen.genRand.Next( 10, 20 ) * 0.01f;
						}
						else if( num13 < 235 )
						{
							num12 += (float)WorldGen.genRand.Next( 10, 20 ) * 0.05f;
						}
						else if( num13 < 240 )
						{
							num12 += (float)WorldGen.genRand.Next( 10, 20 ) * 0.1f;
						}
						else if( num13 < 245 )
						{
							num12 += (float)WorldGen.genRand.Next( 10, 20 ) * 0.05f;
						}
						else if( num13 < 255 )
						{
							num12 += (float)WorldGen.genRand.Next( 10, 20 ) * 0.01f;
						}
						if( num13 == 235 )
						{
							num4 = m;
						}
						int num15 = WorldGen.genRand.Next( 15, 20 );
						int num16 = 0;
						while( (float)num16 < (float)num14 + num12 + (float)num15 )
						{
							if( (float)num16 < (float)num14 + num12 * 0.75f - 3f && (double)num16 < Main.worldSurface - 2.0 )
							{
								Main.tile[m, num16].active( false );
								if( num16 > num14 )
								{
									Main.tile[m, num16].liquid = byte.MaxValue;
								}
								else if( num16 == num14 )
								{
									Main.tile[m, num16].liquid = 127;
								}
							}
							else if( num16 > num14 )
							{
								Main.tile[m, num16].type = 53;
								Main.tile[m, num16].active( true );
							}
							Main.tile[m, num16].wall = 0;
							num16++;
						}
					}
				}
			}
			while( !Main.tile[num3, num].active() )
			{
				num++;
			}
			num++;
			while( !Main.tile[num4, num2].active() )
			{
				num2++;
			}
			num2++;
		}

		private static void GemsFunc( GenerationProgress progress )
		{
			progress.Message = Lang.gen[23].Value;

			for( int type = 63; type <= 68; type++ ) // run code for each gem type.
			{
				float count = 0f;
				if( type == 67 )
				{
					count = (float)Main.maxTilesX * 0.5f;
				}
				else if( type == 66 )
				{
					count = (float)Main.maxTilesX * 0.45f;
				}
				else if( type == 63 )
				{
					count = (float)Main.maxTilesX * 0.3f;
				}
				else if( type == 65 )
				{
					count = (float)Main.maxTilesX * 0.25f;
				}
				else if( type == 64 )
				{
					count = (float)Main.maxTilesX * 0.1f;
				}
				else if( type == 68 )
				{
					count = (float)Main.maxTilesX * 0.05f;
				}
				count *= 0.2f;
				for( int i = 0; i < count; i++ )
				{
					int x = WorldGen.genRand.Next( 0, Main.maxTilesX );
					int y = WorldGen.genRand.Next( (int)Main.worldSurface, Main.maxTilesY );
					while( Main.tile[x, y].type != 1 ) // find stone block
					{
						x = WorldGen.genRand.Next( 0, Main.maxTilesX );
						y = WorldGen.genRand.Next( (int)Main.worldSurface, Main.maxTilesY );
					}
					WorldGenUtils.TileRunner( x, y, WorldGen.genRand.Next( 2, 6 ), WorldGen.genRand.Next( 3, 7 ), type );
				}
			}

			for( int l = 0; l < 2; l++ )
			{
				int num5 = 1;
				int num6 = 5;
				int num7 = Main.maxTilesX - 5;
				if( l == 1 )
				{
					num5 = -1;
					num6 = Main.maxTilesX - 5;
					num7 = 5;
				}
				for( int x = num6; x != num7; x += num5 )
				{
					for( int y = 10; y < Main.maxTilesY - 10; y++ )
					{
						if( Main.tile[x, y].active() && Main.tile[x, y + 1].active() && Main.tileSand[(int)Main.tile[x, y].type] && Main.tileSand[(int)Main.tile[x, y + 1].type] )
						{
							ushort type = Main.tile[x, y].type;
							int num9 = x + num5;
							int num10 = y + 1;
							if( !Main.tile[num9, y].active() && !Main.tile[num9, y + 1].active() )
							{
								while( !Main.tile[num9, num10].active() )
								{
									num10++;
								}
								num10--;
								Main.tile[x, y].active( false );
								Main.tile[num9, num10].active( true );
								Main.tile[num9, num10].type = type;
							}
						}
					}
				}
			}
		}

		private static void GravitatingSandFunc( GenerationProgress progress )
		{
			progress.Message = Lang.gen[24].Value;
			for( int k = 0; k < Main.maxTilesX; k++ )
			{
				float value = (float)k / (float)(Main.maxTilesX - 1);
				progress.Set( value );
				bool flag2 = false;
				int num = 0;
				for( int l = Main.maxTilesY - 1; l > 0; l-- )
				{
					if( WorldGen.SolidOrSlopedTile( k, l ) )
					{
						ushort type = Main.tile[k, l].type;
						if( flag2 && l < (int)Main.worldSurface && l != num - 1 && TileID.Sets.Falling[(int)type] )
						{
							for( int m = l; m < num; m++ )
							{
								Main.tile[k, m].type = type;
								Main.tile[k, m].active( true );
							}
						}
						flag2 = true;
						num = l;
					}
				}
			}
		}

		private static void CleanUpDirtFunc( GenerationProgress progress )
		{
			progress.Message = Lang.gen[25].Value;
			for( int k = 3; k < Main.maxTilesX - 3; k++ )
			{
				float num = (float)k / (float)Main.maxTilesX;
				progress.Set( 0.5f * num );
				bool flag2 = true;
				int num2 = 0;
				while( (double)num2 < Main.worldSurface )
				{
					if( flag2 )
					{
						if( Main.tile[k, num2].wall == 2 || Main.tile[k, num2].wall == 40 || Main.tile[k, num2].wall == 64 )
						{
							Main.tile[k, num2].wall = 0;
						}
						if( Main.tile[k, num2].type != 53 && Main.tile[k, num2].type != 112 && Main.tile[k, num2].type != 234 )
						{
							if( Main.tile[k - 1, num2].wall == 2 || Main.tile[k - 1, num2].wall == 40 || Main.tile[k - 1, num2].wall == 40 )
							{
								Main.tile[k - 1, num2].wall = 0;
							}
							if( (Main.tile[k - 2, num2].wall == 2 || Main.tile[k - 2, num2].wall == 40 || Main.tile[k - 2, num2].wall == 40) && WorldGen.genRand.Next( 2 ) == 0 )
							{
								Main.tile[k - 2, num2].wall = 0;
							}
							if( (Main.tile[k - 3, num2].wall == 2 || Main.tile[k - 3, num2].wall == 40 || Main.tile[k - 3, num2].wall == 40) && WorldGen.genRand.Next( 2 ) == 0 )
							{
								Main.tile[k - 3, num2].wall = 0;
							}
							if( Main.tile[k + 1, num2].wall == 2 || Main.tile[k + 1, num2].wall == 40 || Main.tile[k + 1, num2].wall == 40 )
							{
								Main.tile[k + 1, num2].wall = 0;
							}
							if( (Main.tile[k + 2, num2].wall == 2 || Main.tile[k + 2, num2].wall == 40 || Main.tile[k + 2, num2].wall == 40) && WorldGen.genRand.Next( 2 ) == 0 )
							{
								Main.tile[k + 2, num2].wall = 0;
							}
							if( (Main.tile[k + 3, num2].wall == 2 || Main.tile[k + 3, num2].wall == 40 || Main.tile[k + 3, num2].wall == 40) && WorldGen.genRand.Next( 2 ) == 0 )
							{
								Main.tile[k + 3, num2].wall = 0;
							}
							if( Main.tile[k, num2].active() )
							{
								flag2 = false;
							}
						}
					}
					else if( Main.tile[k, num2].wall == 0 && Main.tile[k, num2 + 1].wall == 0 && Main.tile[k, num2 + 2].wall == 0 && Main.tile[k, num2 + 3].wall == 0 && Main.tile[k, num2 + 4].wall == 0 && Main.tile[k - 1, num2].wall == 0 && Main.tile[k + 1, num2].wall == 0 && Main.tile[k - 2, num2].wall == 0 && Main.tile[k + 2, num2].wall == 0 && !Main.tile[k, num2].active() && !Main.tile[k, num2 + 1].active() && !Main.tile[k, num2 + 2].active() && !Main.tile[k, num2 + 3].active() )
					{
						flag2 = true;
					}
					num2++;
				}
			}
			for( int l = Main.maxTilesX - 5; l >= 5; l-- )
			{
				float num3 = (float)l / (float)Main.maxTilesX;
				progress.Set( 1f - 0.5f * num3 );
				bool flag3 = true;
				int num4 = 0;
				while( (double)num4 < Main.worldSurface )
				{
					if( flag3 )
					{
						if( Main.tile[l, num4].wall == 2 || Main.tile[l, num4].wall == 40 || Main.tile[l, num4].wall == 64 )
						{
							Main.tile[l, num4].wall = 0;
						}
						if( Main.tile[l, num4].type != 53 )
						{
							if( Main.tile[l - 1, num4].wall == 2 || Main.tile[l - 1, num4].wall == 40 || Main.tile[l - 1, num4].wall == 40 )
							{
								Main.tile[l - 1, num4].wall = 0;
							}
							if( (Main.tile[l - 2, num4].wall == 2 || Main.tile[l - 2, num4].wall == 40 || Main.tile[l - 2, num4].wall == 40) && WorldGen.genRand.Next( 2 ) == 0 )
							{
								Main.tile[l - 2, num4].wall = 0;
							}
							if( (Main.tile[l - 3, num4].wall == 2 || Main.tile[l - 3, num4].wall == 40 || Main.tile[l - 3, num4].wall == 40) && WorldGen.genRand.Next( 2 ) == 0 )
							{
								Main.tile[l - 3, num4].wall = 0;
							}
							if( Main.tile[l + 1, num4].wall == 2 || Main.tile[l + 1, num4].wall == 40 || Main.tile[l + 1, num4].wall == 40 )
							{
								Main.tile[l + 1, num4].wall = 0;
							}
							if( (Main.tile[l + 2, num4].wall == 2 || Main.tile[l + 2, num4].wall == 40 || Main.tile[l + 2, num4].wall == 40) && WorldGen.genRand.Next( 2 ) == 0 )
							{
								Main.tile[l + 2, num4].wall = 0;
							}
							if( (Main.tile[l + 3, num4].wall == 2 || Main.tile[l + 3, num4].wall == 40 || Main.tile[l + 3, num4].wall == 40) && WorldGen.genRand.Next( 2 ) == 0 )
							{
								Main.tile[l + 3, num4].wall = 0;
							}
							if( Main.tile[l, num4].active() )
							{
								flag3 = false;
							}
						}
					}
					else if( Main.tile[l, num4].wall == 0 && Main.tile[l, num4 + 1].wall == 0 && Main.tile[l, num4 + 2].wall == 0 && Main.tile[l, num4 + 3].wall == 0 && Main.tile[l, num4 + 4].wall == 0 && Main.tile[l - 1, num4].wall == 0 && Main.tile[l + 1, num4].wall == 0 && Main.tile[l - 2, num4].wall == 0 && Main.tile[l + 2, num4].wall == 0 && !Main.tile[l, num4].active() && !Main.tile[l, num4 + 1].active() && !Main.tile[l, num4 + 2].active() && !Main.tile[l, num4 + 3].active() )
					{
						flag3 = true;
					}
					num4++;
				}
			}
		}

		// calls external function. external 'dungeonX' variable.
		private static void PyramidsFunc( GenerationProgress progress )
		{
			for( int k = 0; k < pyramidCount; k++ )
			{
				int num = PyramidX[k];
				int num2 = PyramidY[k];
				if( num > 300 && num < Main.maxTilesX - 300 && (dungeonSide >= 0 || (double)num >= (double)WorldGen.dungeonX + (double)Main.maxTilesX * 0.15) )
				{
					if( dungeonSide <= 0 || (double)num <= (double)WorldGen.dungeonX - (double)Main.maxTilesX * 0.15 )
					{
						while( !Main.tile[num, num2].active() && (double)num2 < Main.worldSurface )
						{
							num2++;
						}
						if( (double)num2 < Main.worldSurface && Main.tile[num, num2].type == 53 )
						{
							int num3 = Main.maxTilesX;
							for( int l = 0; l < k; l++ )
							{
								int num4 = Math.Abs( num - PyramidX[l] );
								if( num4 < num3 )
								{
									num3 = num4;
								}
							}
							if( num3 >= 250 )
							{
								num2--;
								WorldGen.Pyramid( num, num2 );
							}
						}
					}
				}
			}
		}

		private static void DirtRockWallRunnerFunc( GenerationProgress progress )
		{
			for( int k = 0; k < Main.maxTilesX; k++ )
			{
				int num = WorldGen.genRand.Next( 10, Main.maxTilesX - 10 );
				int num2 = WorldGen.genRand.Next( 10, (int)Main.worldSurface );
				if( Main.tile[num, num2].wall == 2 )
				{
					WorldGen.DirtyRockRunner( num, num2 );
				}
			}
		}


		// calls external function.
		private static void LivingTreesFunc( GenerationProgress progress )
		{
			float num = (float)(Main.maxTilesX / 4200);
			int num2 = WorldGen.genRand.Next( 0, (int)(3f * num) );
			for( int k = 0; k < num2; k++ )
			{
				bool flag2 = false;
				int num3 = 0;
				while( !flag2 )
				{
					num3++;
					if( num3 > 1000 )
					{
						flag2 = true;
					}
					int num4 = WorldGen.genRand.Next( 300, Main.maxTilesX - 300 );
					if( num4 <= Main.maxTilesX / 2 - 100 || num4 >= Main.maxTilesX / 2 + 100 )
					{
						int num5 = 0;
						while( !Main.tile[num4, num5].active() && (double)num5 < Main.worldSurface )
						{
							num5++;
						}
						if( Main.tile[num4, num5].type == 0 )
						{
							num5--;
							if( num5 > 150 )
							{
								bool flag3 = true;
								for( int l = num4 - 50; l < num4 + 50; l++ )
								{
									for( int m = num5 - 50; m < num5 + 50; m++ )
									{
										if( Main.tile[l, m].active() )
										{
											int type = (int)Main.tile[l, m].type;
											if( type == 41 || type == 43 || type == 44 || type == 189 || type == 196 )
											{
												flag3 = false;
											}
										}
									}
								}
								if( flag3 )
								{
									flag2 = WorldGen.GrowLivingTree( num4, num5 );
								}
							}
						}
					}
				}
			}
			Main.tileSolid[192] = false;
		}

		private static void WoodTreeWallsFunc( GenerationProgress progress )
		{
			for( int k = 25; k < Main.maxTilesX - 25; k++ )
			{
				int num = 25;
				while( (double)num < Main.worldSurface )
				{
					if( Main.tile[k, num].type == 191 || Main.tile[k, num - 1].type == 191 || Main.tile[k - 1, num].type == 191 || Main.tile[k + 1, num].type == 191 || Main.tile[k, num + 1].type == 191 )
					{
						bool flag2 = true;
						for( int l = k - 1; l <= k + 1; l++ )
						{
							for( int m = num - 1; m <= num + 1; m++ )
							{
								if( l != k && m != num && Main.tile[l, m].type != 191 && Main.tile[l, m].wall != 78 )
								{
									flag2 = false;
								}
							}
						}
						if( flag2 )
						{
							Main.tile[k, num].wall = 78;
						}
					}
					num++;
				}
			}
		}

		private static void AltarsFunc( GenerationProgress progress )
		{
			progress.Message = Lang.gen[26].Value;
			int num = (int)((float)(Main.maxTilesX * Main.maxTilesY) * 0.00002f);
			for( int i = 0; i < num; i++ )
			{
				progress.Set( (float)i / (float)num );
				for( int j = 0; j < 10000; j++ )
				{
					int x = WorldGen.genRand.Next( 1, Main.maxTilesX - 3 );
					int y = (int)(worldSurfaceMax + 20.0);
					if( MyWorld.evilCombo == EvilCombo.Corruption )
					{
						WorldGen.Place3x2( x, y, TileID.DemonAltar, 0 );
					}
					else if( MyWorld.evilCombo == EvilCombo.Crimson )
					{
						WorldGen.Place3x2( x, y, TileID.DemonAltar, 1 );
					}
					if( Main.tile[x, y].type == TileID.DemonAltar )
					{
						break;
					}
				}
			}
		}

		private static void WetJungleFunc( GenerationProgress progress )
		{
			for( int k = 0; k < Main.maxTilesX; k++ )
			{
				int i2 = k;
				int num = (int)worldSurfaceMin;
				while( (double)num < Main.worldSurface - 1.0 )
				{
					if( Main.tile[i2, num].active() )
					{
						if( Main.tile[i2, num].type == 60 )
						{
							Main.tile[i2, num - 1].liquid = byte.MaxValue;
							Main.tile[i2, num - 2].liquid = byte.MaxValue;
							break;
						}
						break;
					}
					else
					{
						num++;
					}
				}
			}
		}

		private static void RemoveWaterFromSandFunc( GenerationProgress progress )
		{
			for( int k = 400; k < Main.maxTilesX - 400; k++ )
			{
				int i2 = k;
				int num = (int)worldSurfaceMin;
				while( (double)num < Main.worldSurface - 1.0 )
				{
					if( Main.tile[i2, num].active() )
					{
						ushort type = Main.tile[i2, num].type;
						if( type == 53 || type == 396 || type == 397 || type == 404 || type == 407 )
						{
							int num2 = num;
							while( (double)num2 > worldSurfaceMin )
							{
								num2--;
								Main.tile[i2, num2].liquid = 0;
							}
							break;
						}
						break;
					}
					else
					{
						num++;
					}
				}
			}
			Main.tileSolid[192] = true;
		}

		// calls external function (biome).
		private static void HivesFunc( GenerationProgress progress )
		{
			progress.Message = Lang.gen[71].Value;
			float num = (float)(Main.maxTilesX / 4200);
			float num2 = (float)(1 + WorldGen.genRand.Next( (int)(5f * num), (int)(8f * num) ));
			int num3 = 10000;
			while( num2 > 0f && num3 > 0 )
			{
				num3--;
				Point point = WorldGen.RandomWorldPoint( (int)(Main.worldSurface + Main.rockLayer) >> 1, 20, 300, 20 );
				if( Biomes<HiveBiome>.Place( point, structures ) )
				{
					num2 -= 1f;
					int num4 = WorldGen.genRand.Next( 5 );
					int num5 = 0;
					int num6 = 10000;
					while( num5 < num4 && num6 > 0 )
					{
						float num7 = WorldGen.genRand.NextFloat() * 60f + 30f;
						float num8 = WorldGen.genRand.NextFloat() * 6.28318548f;
						int num9 = (int)(Math.Cos( (double)num8 ) * (double)num7) + point.X;
						int y = (int)(Math.Sin( (double)num8 ) * (double)num7) + point.Y;
						num6--;
						if( num9 > 50 && num9 < Main.maxTilesX - 50 && Biomes<HoneyPatchBiome>.Place( num9, y, structures ) )
						{
							num5++;
						}
					}
				}
			}
		}

		private static void JungleShrinesFunc( GenerationProgress progress )
		{
			int xRand = WorldGen.genRand.Next( 40, Main.maxTilesX - 40 );
			int yRand = WorldGen.genRand.Next( (int)(Main.worldSurface + Main.rockLayer) / 2, Main.maxTilesY - 400 );
			float amt = WorldGen.genRand.Next( 7, 12 ) * (float)(Main.maxTilesX / 4200);
			int i = 0;
			while( (float)i < amt )
			{
				bool outsideJungle = true;
				while( outsideJungle )
				{
					xRand = WorldGen.genRand.Next( 40, Main.maxTilesX / 2 - 40 );
					if( dungeonSide < 0 )
					{
						xRand += Main.maxTilesX / 2;
					}
					yRand = WorldGen.genRand.Next( (int)(Main.worldSurface + Main.rockLayer) / 2, Main.maxTilesY - 400 );
					if( Main.tile[xRand, yRand].type == TileID.JungleGrass )
					{
						int margin = 30;
						outsideJungle = false;
						for( int x = xRand - margin; x < xRand + margin; x += 3 )
						{
							for( int y = yRand - margin; y < yRand + margin; y += 3 )
							{
								if( Main.tile[x, y].active() && (Main.tile[x, y].type == TileID.Hive || Main.tile[x, y].type == TileID.HoneyBlock || Main.tile[x, y].type == TileID.LihzahrdBrick || Main.tile[x, y].type == TileID.IridescentBrick || Main.tile[x, y].type == TileID.Mudstone) )
								{
									outsideJungle = false;
								}
								if( Main.tile[x, y].wall == 86 || Main.tile[x, y].wall == 87 )
								{
									outsideJungle = false;
								}
							}
						}
					}
					if( !outsideJungle )
					{
						int xSpan = WorldGen.genRand.Next( 2, 4 );
						int ySpan = WorldGen.genRand.Next( 2, 4 );

						int jungleShrineWall = 0;
						if( jungleShrineTile == 120 )
						{
							jungleShrineWall = 24;
						}
						else if( jungleShrineTile == 45 )
						{
							jungleShrineWall = 10;
						}

						for( int x = xRand - xSpan - 1; x <= xRand + xSpan + 1; x++ )
						{
							for( int y = yRand - ySpan - 1; y <= yRand + ySpan + 1; y++ )
							{
								Main.tile[x, y].active( true );
								Main.tile[x, y].type = jungleShrineTile;
								Main.tile[x, y].liquid = 0;
								Main.tile[x, y].lava( false );
							}
						}
						for( int x = xRand - xSpan; x <= xRand + xSpan; x++ )
						{
							for( int y = yRand - ySpan; y <= yRand + ySpan; y++ )
							{
								Main.tile[x, y].active( false );
								Main.tile[x, y].wall = (byte)jungleShrineWall;
							}
						}

						// randomize torch position.
						bool placedTorch = false;
						int failsafe = 0;
						while( !placedTorch && failsafe < 100 )
						{
							failsafe++;
							int x = WorldGen.genRand.Next( xRand - xSpan, xRand + xSpan + 1 );
							int y = WorldGen.genRand.Next( yRand - ySpan, yRand + ySpan - 2 );
							WorldGen.PlaceTile( x, y, TileID.Torches, true, false, -1, 3 );
							if( Main.tile[x, y].type == TileID.Torches )
							{
								placedTorch = true;
							}
						}

						for( int x = xRand - xSpan - 1; x <= xRand + xSpan + 1; x++ )
						{
							for( int y = yRand + ySpan - 2; y <= yRand + ySpan; y++ )
							{
								Main.tile[x, y].active( false );
							}
						}
						for( int x = xRand - xSpan - 1; x <= xRand + xSpan + 1; x++ )
						{
							for( int y = yRand + ySpan - 2; y <= yRand + ySpan - 1; y++ )
							{
								Main.tile[x, y].active( false );
							}
						}
						for( int x = xRand - xSpan - 1; x <= xRand + xSpan + 1; x++ )
						{
							int num19 = 4;
							int y = yRand + ySpan + 2;
							while( !Main.tile[x, y].active() && y < Main.maxTilesY && num19 > 0 )
							{
								Main.tile[x, y].active( true );
								Main.tile[x, y].type = 59;
								y++;
								num19--;
							}
						}
						xSpan -= WorldGen.genRand.Next( 1, 3 );
						int y2 = yRand - ySpan - 2;
						while( xSpan > -1 )
						{
							for( int x2 = xRand - xSpan - 1; x2 <= xRand + xSpan + 1; x2++ )
							{
								Main.tile[x2, y2].active( true );
								Main.tile[x2, y2].type = jungleShrineTile;
							}
							xSpan -= WorldGen.genRand.Next( 1, 3 );
							y2--;
						}
						jungleChestX[jungleChestCount] = xRand;
						jungleChestY[jungleChestCount] = yRand;
						jungleChestCount++;
					}
				}
				i++;
			}
			Main.tileSolid[137] = false;
		}

		private static void SmoothWorldFunc( GenerationProgress progress )
		{
			progress.Message = Lang.gen[60].Value;
			for( int k = 20; k < Main.maxTilesX - 20; k++ )
			{
				float value = (float)k / (float)Main.maxTilesX;
				progress.Set( value );
				for( int l = 20; l < Main.maxTilesY - 20; l++ )
				{
					if( Main.tile[k, l].type != 48 && Main.tile[k, l].type != 137 && Main.tile[k, l].type != 232 && Main.tile[k, l].type != 191 && Main.tile[k, l].type != 151 && Main.tile[k, l].type != 274 )
					{
						if( !Main.tile[k, l - 1].active() )
						{
							if( WorldGen.SolidTile( k, l ) && TileID.Sets.CanBeClearedDuringGeneration[(int)Main.tile[k, l].type] )
							{
								if( !Main.tile[k - 1, l].halfBrick() && !Main.tile[k + 1, l].halfBrick() && Main.tile[k - 1, l].slope() == 0 && Main.tile[k + 1, l].slope() == 0 )
								{
									if( WorldGen.SolidTile( k, l + 1 ) )
									{
										if( !WorldGen.SolidTile( k - 1, l ) && !Main.tile[k - 1, l + 1].halfBrick() && WorldGen.SolidTile( k - 1, l + 1 ) && WorldGen.SolidTile( k + 1, l ) && !Main.tile[k + 1, l - 1].active() )
										{
											if( WorldGen.genRand.Next( 2 ) == 0 )
											{
												WorldGen.SlopeTile( k, l, 2 );
											}
											else
											{
												WorldGen.PoundTile( k, l );
											}
										}
										else if( !WorldGen.SolidTile( k + 1, l ) && !Main.tile[k + 1, l + 1].halfBrick() && WorldGen.SolidTile( k + 1, l + 1 ) && WorldGen.SolidTile( k - 1, l ) && !Main.tile[k - 1, l - 1].active() )
										{
											if( WorldGen.genRand.Next( 2 ) == 0 )
											{
												WorldGen.SlopeTile( k, l, 1 );
											}
											else
											{
												WorldGen.PoundTile( k, l );
											}
										}
										else if( WorldGen.SolidTile( k + 1, l + 1 ) && WorldGen.SolidTile( k - 1, l + 1 ) && !Main.tile[k + 1, l].active() && !Main.tile[k - 1, l].active() )
										{
											WorldGen.PoundTile( k, l );
										}
										if( WorldGen.SolidTile( k, l ) )
										{
											if( WorldGen.SolidTile( k - 1, l ) && WorldGen.SolidTile( k + 1, l + 2 ) && !Main.tile[k + 1, l].active() && !Main.tile[k + 1, l + 1].active() && !Main.tile[k - 1, l - 1].active() )
											{
												WorldGen.KillTile( k, l, false, false, false );
											}
											else if( WorldGen.SolidTile( k + 1, l ) && WorldGen.SolidTile( k - 1, l + 2 ) && !Main.tile[k - 1, l].active() && !Main.tile[k - 1, l + 1].active() && !Main.tile[k + 1, l - 1].active() )
											{
												WorldGen.KillTile( k, l, false, false, false );
											}
											else if( !Main.tile[k - 1, l + 1].active() && !Main.tile[k - 1, l].active() && WorldGen.SolidTile( k + 1, l ) && WorldGen.SolidTile( k, l + 2 ) )
											{
												if( WorldGen.genRand.Next( 5 ) == 0 )
												{
													WorldGen.KillTile( k, l, false, false, false );
												}
												else if( WorldGen.genRand.Next( 5 ) == 0 )
												{
													WorldGen.PoundTile( k, l );
												}
												else
												{
													WorldGen.SlopeTile( k, l, 2 );
												}
											}
											else if( !Main.tile[k + 1, l + 1].active() && !Main.tile[k + 1, l].active() && WorldGen.SolidTile( k - 1, l ) && WorldGen.SolidTile( k, l + 2 ) )
											{
												if( WorldGen.genRand.Next( 5 ) == 0 )
												{
													WorldGen.KillTile( k, l, false, false, false );
												}
												else if( WorldGen.genRand.Next( 5 ) == 0 )
												{
													WorldGen.PoundTile( k, l );
												}
												else
												{
													WorldGen.SlopeTile( k, l, 1 );
												}
											}
										}
									}
									if( WorldGen.SolidTile( k, l ) && !Main.tile[k - 1, l].active() && !Main.tile[k + 1, l].active() )
									{
										WorldGen.KillTile( k, l, false, false, false );
									}
								}
							}
							else if( !Main.tile[k, l].active() && Main.tile[k, l + 1].type != 151 && Main.tile[k, l + 1].type != 274 )
							{
								if( Main.tile[k + 1, l].type != 190 && Main.tile[k + 1, l].type != 48 && Main.tile[k + 1, l].type != 232 && WorldGen.SolidTile( k - 1, l + 1 ) && WorldGen.SolidTile( k + 1, l ) && !Main.tile[k - 1, l].active() && !Main.tile[k + 1, l - 1].active() )
								{
									WorldGen.PlaceTile( k, l, (int)Main.tile[k, l + 1].type, false, false, -1, 0 );
									if( WorldGen.genRand.Next( 2 ) == 0 )
									{
										WorldGen.SlopeTile( k, l, 2 );
									}
									else
									{
										WorldGen.PoundTile( k, l );
									}
								}
								if( Main.tile[k - 1, l].type != 190 && Main.tile[k - 1, l].type != 48 && Main.tile[k - 1, l].type != 232 && WorldGen.SolidTile( k + 1, l + 1 ) && WorldGen.SolidTile( k - 1, l ) && !Main.tile[k + 1, l].active() && !Main.tile[k - 1, l - 1].active() )
								{
									WorldGen.PlaceTile( k, l, (int)Main.tile[k, l + 1].type, false, false, -1, 0 );
									if( WorldGen.genRand.Next( 2 ) == 0 )
									{
										WorldGen.SlopeTile( k, l, 1 );
									}
									else
									{
										WorldGen.PoundTile( k, l );
									}
								}
							}
						}
						else if( !Main.tile[k, l + 1].active() && WorldGen.genRand.Next( 2 ) == 0 && WorldGen.SolidTile( k, l ) && !Main.tile[k - 1, l].halfBrick() && !Main.tile[k + 1, l].halfBrick() && Main.tile[k - 1, l].slope() == 0 && Main.tile[k + 1, l].slope() == 0 && WorldGen.SolidTile( k, l - 1 ) )
						{
							if( WorldGen.SolidTile( k - 1, l ) && !WorldGen.SolidTile( k + 1, l ) && WorldGen.SolidTile( k - 1, l - 1 ) )
							{
								WorldGen.SlopeTile( k, l, 3 );
							}
							else if( WorldGen.SolidTile( k + 1, l ) && !WorldGen.SolidTile( k - 1, l ) && WorldGen.SolidTile( k + 1, l - 1 ) )
							{
								WorldGen.SlopeTile( k, l, 4 );
							}
						}
						if( TileID.Sets.Conversion.Sand[(int)Main.tile[k, l].type] )
						{
							Tile.SmoothSlope( k, l, false );
						}
					}
				}
			}
			for( int m = 20; m < Main.maxTilesX - 20; m++ )
			{
				for( int n = 20; n < Main.maxTilesY - 20; n++ )
				{
					if( WorldGen.genRand.Next( 2 ) == 0 && !Main.tile[m, n - 1].active() && Main.tile[m, n].type != 137 && Main.tile[m, n].type != 48 && Main.tile[m, n].type != 232 && Main.tile[m, n].type != 191 && Main.tile[m, n].type != 151 && Main.tile[m, n].type != 274 && Main.tile[m, n].type != 75 && Main.tile[m, n].type != 76 && WorldGen.SolidTile( m, n ) && Main.tile[m - 1, n].type != 137 && Main.tile[m + 1, n].type != 137 )
					{
						if( WorldGen.SolidTile( m, n + 1 ) && WorldGen.SolidTile( m + 1, n ) && !Main.tile[m - 1, n].active() )
						{
							WorldGen.SlopeTile( m, n, 2 );
						}
						if( WorldGen.SolidTile( m, n + 1 ) && WorldGen.SolidTile( m - 1, n ) && !Main.tile[m + 1, n].active() )
						{
							WorldGen.SlopeTile( m, n, 1 );
						}
					}
					if( Main.tile[m, n].slope() == 1 && !WorldGen.SolidTile( m - 1, n ) )
					{
						WorldGen.SlopeTile( m, n, 0 );
						WorldGen.PoundTile( m, n );
					}
					if( Main.tile[m, n].slope() == 2 && !WorldGen.SolidTile( m + 1, n ) )
					{
						WorldGen.SlopeTile( m, n, 0 );
						WorldGen.PoundTile( m, n );
					}
				}
			}
			Main.tileSolid[137] = true;
			Main.tileSolid[190] = false;
			Main.tileSolid[192] = false;
		}

		private static void SettleLiquidsFunc( GenerationProgress progress )
		{
			progress.Message = Lang.gen[27].Value;
			Liquid.QuickWater( 3, -1, -1 );
			WorldGen.WaterCheck();
			int k = 0;
			Liquid.quickSettle = true;
			while( k < 10 )
			{
				int num = Liquid.numLiquid + LiquidBuffer.numLiquidBuffer;
				k++;
				float num2 = 0f;
				while( Liquid.numLiquid > 0 )
				{
					float num3 = (float)(num - (Liquid.numLiquid + LiquidBuffer.numLiquidBuffer)) / (float)num;
					if( Liquid.numLiquid + LiquidBuffer.numLiquidBuffer > num )
					{
						num = Liquid.numLiquid + LiquidBuffer.numLiquidBuffer;
					}
					if( num3 > num2 )
					{
						num2 = num3;
					}
					else
					{
						num3 = num2;
					}
					if( k == 1 )
					{
						progress.Set( num3 / 3f + 0.33f );
					}
					int num4 = 10;
					if( k > num4 )
					{
					}
					Liquid.UpdateLiquid();
				}
				WorldGen.WaterCheck();
				progress.Set( (float)k * 0.1f / 3f + 0.66f );
			}
			Liquid.quickSettle = false;
			Main.tileSolid[190] = true;
		}

		private static void WaterfallsFunc( GenerationProgress progress )
		{
			// slopes tiles.
			progress.Message = Lang.gen[69].Value;
			for( int k = 20; k < Main.maxTilesX - 20; k++ )
			{
				float num = (float)k / (float)Main.maxTilesX;
				progress.Set( num * 0.5f );
				for( int l = 20; l < Main.maxTilesY - 20; l++ )
				{
					if( WorldGen.SolidTile( k, l ) && !Main.tile[k - 1, l].active() && WorldGen.SolidTile( k, l + 1 ) && !Main.tile[k + 1, l].active() && (Main.tile[k - 1, l].liquid > 0 || Main.tile[k + 1, l].liquid > 0) )
					{
						bool flag2 = true;
						int num2 = WorldGen.genRand.Next( 8, 20 );
						int num3 = WorldGen.genRand.Next( 8, 20 );
						num2 = l - num2;
						num3 += l;
						for( int m = num2; m <= num3; m++ )
						{
							if( Main.tile[k, m].halfBrick() )
							{
								flag2 = false;
							}
						}
						if( (Main.tile[k, l].type == 75 || Main.tile[k, l].type == 76) && WorldGen.genRand.Next( 10 ) != 0 )
						{
							flag2 = false;
						}
						if( flag2 )
						{
							WorldGen.PoundTile( k, l );
						}
					}
				}
			}
			for( int n = 20; n < Main.maxTilesX - 20; n++ )
			{
				float num4 = (float)n / (float)Main.maxTilesX;
				progress.Set( num4 * 0.5f + 0.5f );
				for( int num5 = 20; num5 < Main.maxTilesY - 20; num5++ )
				{
					if( Main.tile[n, num5].type != 48 && Main.tile[n, num5].type != 232 && WorldGen.SolidTile( n, num5 ) && WorldGen.SolidTile( n, num5 + 1 ) )
					{
						if( !WorldGen.SolidTile( n + 1, num5 ) && Main.tile[n - 1, num5].halfBrick() && Main.tile[n - 2, num5].liquid > 0 )
						{
							WorldGen.PoundTile( n, num5 );
						}
						if( !WorldGen.SolidTile( n - 1, num5 ) && Main.tile[n + 1, num5].halfBrick() && Main.tile[n + 2, num5].liquid > 0 )
						{
							WorldGen.PoundTile( n, num5 );
						}
					}
				}
			}
		}

		// calls external function.
		private static void IceFunc( GenerationProgress progress )
		{
			for( int k = 10; k < Main.maxTilesX - 10; k++ )
			{
				for( int l = (int)Main.worldSurface; l < Main.maxTilesY - 100; l++ )
				{
					if( Main.tile[k, l].liquid > 0 && !Main.tile[k, l].lava() )
					{
						WorldGen.MakeWateryIceThing( k, l );
					}
				}
			}
			Main.tileSolid[226] = false;
			Main.tileSolid[162] = false;
		}

		private static void WallVarietyFunc( GenerationProgress progress )
		{
			progress.Message = Lang.gen[79].Value;
			float num = (float)(Main.maxTilesX * Main.maxTilesY) / 5040000f;
			int k = (int)(300f * num);
			int num2 = k;
			ShapeData shapeData = new ShapeData();
			while( k > 0 )
			{
				progress.Set( 1f - (float)k / (float)num2 );
				Point point = WorldGen.RandomWorldPoint( (int)worldSurface, 2, 190, 2 );
				Tile tile = Main.tile[point.X, point.Y];
				Tile tile2 = Main.tile[point.X, point.Y - 1];
				byte b = 0;
				if( tile.type == 59 || tile.type == 60 )
				{
					b = (byte)(204 + WorldGen.genRand.Next( 4 ));
				}
				else if( tile.type == 1 && tile2.wall == 0 )
				{
					if( (double)point.Y < rockLayer )
					{
						b = (byte)(196 + WorldGen.genRand.Next( 4 ));
					}
					else if( point.Y < lavaLine )
					{
						b = (byte)(212 + WorldGen.genRand.Next( 4 ));
					}
					else
					{
						b = (byte)(208 + WorldGen.genRand.Next( 4 ));
					}
				}
				if( tile.active() && b != 0 && !tile2.active() )
				{
					bool foundInvalidTile = false;
					bool flag2 = WorldUtils.Gen( new Point( point.X, point.Y - 1 ), new ShapeFloodFill( 1000 ), Actions.Chain( new GenAction[]
					{
					new Modifiers.IsNotSolid(),
					new Actions.Blank().Output(shapeData),
					new Actions.ContinueWrapper(Actions.Chain(new GenAction[]
					{
						new Modifiers.IsTouching(true, new ushort[]
						{
							60,
							147,
							161,
							396,
							397
						}),
						new Actions.Custom(delegate(int x, int y, object[] args)
						{
							foundInvalidTile = true;
							return true;
						})
					}))
					} ) );
					if( shapeData.Count > 50 && flag2 && !foundInvalidTile )
					{
						WorldUtils.Gen( new Point( point.X, point.Y ), new ModShapes.OuterOutline( shapeData, true, true ), new Actions.PlaceWall( b, true ) );
						k--;
					}
					shapeData.Clear();
				}
			}
		}

		// calls external function PlaceTrap
		private static void TrapsFunc( GenerationProgress progress )
		{
			progress.Message = Lang.gen[34].Value;
			for( int i = 0; i < (int)((double)Main.maxTilesX * 0.05); i++ )
			{
				float value = (float)((double)i / ((double)Main.maxTilesX * 0.05));
				progress.Set( value );
				for( int l = 0; l < 1150; l++ )
				{
					int x = WorldGen.genRand.Next( 200, Main.maxTilesX - 200 );
					int y = WorldGen.genRand.Next( (int)Main.worldSurface, Main.maxTilesY - 210 );
					if( Main.tile[x, y].wall == 0 && WorldGen.placeTrap( x, y, -1 ) )
					{
						break;
					}
				}
			}
		}

		// calls external function.
		private static void LifeCrystalsFunc( GenerationProgress progress )
		{
			dub2 = (float)(Main.maxTilesX / 4200);
			progress.Message = Lang.gen[28].Value;
			for( int k = 0; k < (int)((double)(Main.maxTilesX * Main.maxTilesY) * 2E-05); k++ )
			{
				float value = (float)((double)k / ((double)(Main.maxTilesX * Main.maxTilesY) * 2E-05));
				progress.Set( value );
				bool flag2 = false;
				int num = 0;
				while( !flag2 )
				{
					if( WorldGen.AddLifeCrystal( WorldGen.genRand.Next( 40, Main.maxTilesX - 40 ), WorldGen.genRand.Next( (int)(worldSurfaceMax + 20.0), Main.maxTilesY - 300 ) ) )
					{
						flag2 = true;
					}
					else
					{
						num++;
						if( num >= 10000 )
						{
							flag2 = true;
						}
					}
				}
			}
			Main.tileSolid[225] = false;
		}

		private static void StatuesFunc( GenerationProgress progress )
		{
			progress.Message = Lang.gen[29].Value;
			int num = 0;
			int num2 = (int)((float)(WorldGen.statueList.Length * 2) * dub2);
			for( int k = 0; k < num2; k++ )
			{
				if( num >= WorldGen.statueList.Length )
				{
					num = 0;
				}
				int x = (int)WorldGen.statueList[num].X;
				int y = (int)WorldGen.statueList[num].Y;
				float value = (float)(k / num2);
				progress.Set( value );
				bool flag2 = false;
				int num3 = 0;
				while( !flag2 )
				{
					int num4 = WorldGen.genRand.Next( 20, Main.maxTilesX - 20 );
					int num5 = WorldGen.genRand.Next( (int)(worldSurfaceMax + 20.0), Main.maxTilesY - 300 );
					while( !Main.tile[num4, num5].active() )
					{
						num5++;
					}
					num5--;
					WorldGen.PlaceTile( num4, num5, x, true, true, -1, y );
					if( Main.tile[num4, num5].active() && (int)Main.tile[num4, num5].type == x )
					{
						flag2 = true;
						if( WorldGen.StatuesWithTraps.Contains( num ) )
						{
							WorldGen.PlaceStatueTrap( num4, num5 );
						}
						num++;
					}
					else
					{
						num3++;
						if( num3 >= 10000 )
						{
							flag2 = true;
						}
					}
				}
			}
		}

		// cave houses. Calls external func (biome)
		private static void BuriedChestsFunc( GenerationProgress progress )
		{
			progress.Message = Lang.gen[30].Value;
			Main.tileSolid[226] = true;
			Main.tileSolid[162] = true;
			Main.tileSolid[225] = true;
			for( int k = 0; k < (int)((double)(Main.maxTilesX * Main.maxTilesY) * 1.6E-05); k++ )
			{
				float value = (float)((double)k / ((double)(Main.maxTilesX * Main.maxTilesY) * 1.6E-05));
				progress.Set( value );
				bool flag2 = false;
				int num = 0;
				while( !flag2 )
				{
					float num2 = (float)WorldGen.genRand.Next( (int)(5f * dub2), (int)(8f * dub2 + 1f) );
					int num3 = WorldGen.genRand.Next( 20, Main.maxTilesX - 20 );
					int num4 = WorldGen.genRand.Next( (int)(worldSurfaceMax + 20.0), Main.maxTilesY - 230 );
					if( (float)k <= num2 )
					{
						num4 = WorldGen.genRand.Next( Main.maxTilesY - 200, Main.maxTilesY - 50 );
					}
					int num5 = 0;
					while( Main.wallDungeon[(int)Main.tile[num3, num4].wall] )
					{
						num5++;
						num3 = WorldGen.genRand.Next( 1, Main.maxTilesX );
						num4 = WorldGen.genRand.Next( (int)(worldSurfaceMax + 20.0), Main.maxTilesY - 230 );
						if( num5 < 1000 && (float)k <= num2 )
						{
							num4 = WorldGen.genRand.Next( Main.maxTilesY - 200, Main.maxTilesY - 50 );
						}
					}
					if( (float)k > num2 )
					{
						for( int l = 10; l > 0; l-- )
						{
							int x = WorldGen.genRand.Next( 80, Main.maxTilesX - 80 );
							int y = WorldGen.genRand.Next( (int)(worldSurfaceMax + 20.0), Main.maxTilesY - 230 );
							if( Biomes<CaveHouseBiome>.Place( x, y, structures ) )
							{
								flag2 = true;
								break;
							}
						}
					}
					else if( WorldGen.AddBuriedChest( num3, num4, 0, false, -1 ) )
					{
						flag2 = true;
					}
					num++;
					if( num >= 1000 )
					{
						flag2 = true;
					}
				}
			}
			int num6 = (int)(2f * (float)(Main.maxTilesX * Main.maxTilesY) / 5040000f);
			int num7 = 1000;
			while( num7 >= 0 && num6 >= 0 )
			{
				if( Biomes<CaveHouseBiome>.Place( WorldGen.RandomRectanglePoint( WorldGen.UndergroundDesertLocation ), structures ) )
				{
					num6--;
				}
				num7--;
			}
			Main.tileSolid[226] = false;
			Main.tileSolid[162] = false;
			Main.tileSolid[225] = false;
		}

		private static void SurfaceChestsFunc( GenerationProgress progress )
		{
			progress.Message = Lang.gen[31].Value;
			for( int k = 0; k < (int)((double)Main.maxTilesX * 0.005); k++ )
			{
				float value = (float)((double)k / ((double)Main.maxTilesX * 0.005));
				progress.Set( value );
				bool stop = false;
				int failsafe = 0;
				while( !stop )
				{
					int x = WorldGen.genRand.Next( 300, Main.maxTilesX - 300 );
					int y = WorldGen.genRand.Next( (int)worldSurfaceMin, (int)Main.worldSurface );
					bool flag3 = false;
					if( Main.tile[x, y].wall == 2 && !Main.tile[x, y].active() )
					{
						flag3 = true;
					}
					if( flag3 && WorldGen.AddBuriedChest( x, y, 0, true, -1 ) )
					{
						stop = true;
					}
					else
					{
						failsafe++;
						if( failsafe >= 2000 )
						{
							stop = true;
						}
					}
				}
			}
		}



		private static void JungleShrinesChestsFunc( GenerationProgress progress )
		{
			progress.Message = Lang.gen[32].Value;
			for( int i = 0; i < jungleChestCount; i++ )
			{
				progress.Set( i / jungleChestCount );
				int nextJungleChestItem = WorldGen.GetNextJungleChestItem();
				if( !WorldGen.AddBuriedChest( jungleChestX[i] + WorldGen.genRand.Next( 2 ), jungleChestY[i], nextJungleChestItem, false, 10 ) )
				{
					for( int x = jungleChestX[i] - 1; x <= jungleChestX[i] + 1; x++ )
					{
						for( int y = jungleChestY[i]; y <= jungleChestY[i] + 2; y++ )
						{
							WorldGen.KillTile( x, y, false, false, false );
						}
					}
					for( int x = jungleChestX[i] - 1; x <= jungleChestX[i] + 1; x++ )
					{
						for( int y = jungleChestY[i]; y <= jungleChestY[i] + 3; y++ )
						{
							if( y < Main.maxTilesY )
							{
								Main.tile[x, y].slope( 0 );
								Main.tile[x, y].halfBrick( false );
							}
						}
					}
					WorldGen.AddBuriedChest( jungleChestX[i], jungleChestY[i], nextJungleChestItem, false, 10 );
				}
			}
		}

		private static void WaterChestsFunc( GenerationProgress progress )
		{
			progress.Message = Lang.gen[33].Value;
			int num = 0;
			int num2 = 0;
			while( (float)num2 < 9f * dub2 )
			{
				float value = (float)num2 / (9f * dub2);
				progress.Set( value );
				num++;
				int contain;
				if( WorldGen.genRand.Next( 15 ) == 0 )
				{
					contain = 863;
				}
				else if( num == 1 )
				{
					contain = 186;
				}
				else if( num == 2 )
				{
					contain = 277;
				}
				else
				{
					contain = 187;
					num = 0;
				}
				bool flag2 = false;
				while( !flag2 )
				{
					int num3 = WorldGen.genRand.Next( 1, Main.maxTilesX );
					int num4 = WorldGen.genRand.Next( 1, Main.maxTilesY - 200 );
					while( Main.tile[num3, num4].liquid < 200 || Main.tile[num3, num4].lava() )
					{
						num3 = WorldGen.genRand.Next( 1, Main.maxTilesX );
						num4 = WorldGen.genRand.Next( 1, Main.maxTilesY - 200 );
					}
					flag2 = WorldGen.AddBuriedChest( num3, num4, contain, false, 17 );
				}
				flag2 = false;
				while( !flag2 )
				{
					int num5 = WorldGen.genRand.Next( 1, Main.maxTilesX );
					int num6 = WorldGen.genRand.Next( (int)Main.worldSurface, Main.maxTilesY - 200 );
					while( Main.tile[num5, num6].liquid < 200 || Main.tile[num5, num6].lava() )
					{
						num5 = WorldGen.genRand.Next( 1, Main.maxTilesX );
						num6 = WorldGen.genRand.Next( 1, Main.maxTilesY - 200 );
					}
					flag2 = WorldGen.AddBuriedChest( num5, num6, contain, false, 17 );
				}
				num2++;
			}
		}

		private static void GemCavesFunc( GenerationProgress progress )
		{
			progress.Message = Lang.gen[64].Value;
			WorldGen.maxTileCount = 300;
			for( int i = 0; i < (int)((double)Main.maxTilesX * 0.003); i++ )
			{
				float value = (float)((double)i / ((double)Main.maxTilesX * 0.003));
				progress.Set( value );
				int num = 0;
				int x = WorldGen.genRand.Next( 200, Main.maxTilesX - 200 );
				int y = WorldGen.genRand.Next( (int)Main.rockLayer + 30, Main.maxTilesY - 230 );
				int num2 = WorldGen.countTiles( x, y, false, false );
				while( (num2 >= 300 || num2 < 50 || WorldGen.lavaCount > 0 || WorldGen.iceCount > 0 || WorldGen.rockCount == 0) && num < 1000 )
				{
					num++;
					x = WorldGen.genRand.Next( 200, Main.maxTilesX - 200 );
					y = WorldGen.genRand.Next( (int)Main.rockLayer + 30, Main.maxTilesY - 230 );
					num2 = WorldGen.countTiles( x, y, false, false );
				}
				if( num < 1000 )
				{
					WorldGen.gemCave( x, y );
				}
			}
		}

		private static void MossFunc( GenerationProgress progress )
		{
			progress.Message = Lang.gen[61].Value;
			WorldGen.randMoss();
			WorldGen.maxTileCount = 2500;
			for( int k = 0; k < (int)((double)Main.maxTilesX * 0.01); k++ )
			{
				float value = (float)((double)k / ((double)Main.maxTilesX * 0.01));
				progress.Set( value );
				int num = 0;
				int x = WorldGen.genRand.Next( 200, Main.maxTilesX - 200 );
				int y = WorldGen.genRand.Next( (int)(Main.worldSurface + Main.rockLayer) / 2, waterLine );
				int num2 = WorldGen.countTiles( x, y, false, false );
				while( (num2 >= 2500 || num2 < 10 || WorldGen.lavaCount > 0 || WorldGen.iceCount > 0 || WorldGen.rockCount == 0) && num < 1000 )
				{
					num++;
					x = WorldGen.genRand.Next( 200, Main.maxTilesX - 200 );
					y = WorldGen.genRand.Next( (int)Main.rockLayer + 30, Main.maxTilesY - 230 );
					num2 = WorldGen.countTiles( x, y, false, false );
				}
				if( num < 1000 )
				{
					WorldGen.setMoss( x, y );
					WorldGen.Spread.Moss( x, y );
				}
			}
			for( int l = 0; l < Main.maxTilesX; l++ )
			{
				int num3 = WorldGen.genRand.Next( 50, Main.maxTilesX - 50 );
				int num4 = WorldGen.genRand.Next( (int)(Main.worldSurface + Main.rockLayer) / 2, lavaLine );
				if( Main.tile[num3, num4].type == 1 )
				{
					WorldGen.setMoss( num3, num4 );
					Main.tile[num3, num4].type = (ushort)WorldGen.mossTile;
				}
			}
			float num5 = (float)Main.maxTilesX * 0.05f;
			while( num5 > 0f )
			{
				int num6 = WorldGen.genRand.Next( 50, Main.maxTilesX - 50 );
				int num7 = WorldGen.genRand.Next( (int)(Main.worldSurface + Main.rockLayer) / 2, lavaLine );
				if( Main.tile[num6, num7].type == 1 && (!Main.tile[num6 - 1, num7].active() || !Main.tile[num6 + 1, num7].active() || !Main.tile[num6, num7 - 1].active() || !Main.tile[num6, num7 + 1].active()) )
				{
					WorldGen.setMoss( num6, num7 );
					Main.tile[num6, num7].type = (ushort)WorldGen.mossTile;
					num5 -= 1f;
				}
			}
			num5 = (float)Main.maxTilesX * 0.065f;
			while( num5 > 0f )
			{
				int num8 = WorldGen.genRand.Next( 50, Main.maxTilesX - 50 );
				int num9 = WorldGen.genRand.Next( waterLine, Main.maxTilesY - 200 );
				if( Main.tile[num8, num9].type == 1 && (!Main.tile[num8 - 1, num9].active() || !Main.tile[num8 + 1, num9].active() || !Main.tile[num8, num9 - 1].active() || !Main.tile[num8, num9 + 1].active()) )
				{
					int num10 = 25;
					int num11 = 0;
					for( int m = num8 - num10; m < num8 + num10; m++ )
					{
						for( int n = num9 - num10; n < num9 + num10; n++ )
						{
							if( Main.tile[m, n].liquid > 0 && Main.tile[m, n].lava() )
							{
								num11++;
							}
						}
					}
					if( num11 > 20 )
					{
						Main.tile[num8, num9].type = 381;
						num5 -= 1f;
					}
					else
					{
						num5 -= 0.002f;
					}
				}
			}
			for( int num12 = 0; num12 < Main.maxTilesX; num12++ )
			{
				for( int num13 = 0; num13 < Main.maxTilesY; num13++ )
				{
					if( Main.tile[num12, num13].active() && Main.tileMoss[(int)Main.tile[num12, num13].type] )
					{
						for( int num14 = 0; num14 < 4; num14++ )
						{
							int num15 = num12;
							int num16 = num13;
							if( num14 == 0 )
							{
								num15--;
							}
							if( num14 == 1 )
							{
								num15++;
							}
							if( num14 == 2 )
							{
								num16--;
							}
							if( num14 == 3 )
							{
								num16++;
							}
							try
							{
								grassSpread = 0;
								WorldGen.SpreadGrass( num15, num16, 1, (int)Main.tile[num12, num13].type, true, 0 );
							}
							catch
							{
								grassSpread = 0;
								WorldGen.SpreadGrass( num15, num16, 1, (int)Main.tile[num12, num13].type, false, 0 );
							}
						}
					}
				}
			}
		}

		private static void IceWallsFunc( GenerationProgress progress )
		{
			progress.Message = Lang.gen[63].Value;
			WorldGen.maxTileCount = 1500;
			for( int k = 0; k < (int)((double)Main.maxTilesX * 0.04); k++ )
			{
				float num = (float)((double)k / ((double)Main.maxTilesX * 0.04));
				progress.Set( num * 0.66f );
				int num2 = 0;
				int x = WorldGen.genRand.Next( 200, Main.maxTilesX - 200 );
				int y = WorldGen.genRand.Next( (int)(Main.worldSurface + Main.rockLayer) / 2, Main.maxTilesY - 220 );
				int num3 = WorldGen.countTiles( x, y, false, true );
				while( (num3 >= 1500 || num3 < 10) && num2 < 500 )
				{
					num2++;
					x = WorldGen.genRand.Next( 200, Main.maxTilesX - 200 );
					y = WorldGen.genRand.Next( (int)(Main.worldSurface + Main.rockLayer) / 2, Main.maxTilesY - 220 );
					num3 = WorldGen.countTiles( x, y, false, true );
				}
				if( num2 < 500 )
				{
					int num4 = WorldGen.genRand.Next( 2 );
					if( WorldGen.iceCount > 0 )
					{
						if( num4 == 0 )
						{
							num4 = 40;
						}
						else if( num4 == 1 )
						{
							num4 = 71;
						}
					}
					else if( WorldGen.lavaCount > 0 )
					{
						num4 = 79;
					}
					else
					{
						num4 = WorldGen.genRand.Next( 4 );
						if( num4 == 0 )
						{
							num4 = 59;
						}
						else if( num4 == 1 )
						{
							num4 = 61;
						}
						else if( num4 == 2 )
						{
							num4 = 170;
						}
						else if( num4 == 3 )
						{
							num4 = 171;
						}
					}
					WorldGen.Spread.Wall( x, y, num4 );
				}
			}
			WorldGen.maxTileCount = 1500;
			for( int l = 0; l < (int)((double)Main.maxTilesX * 0.02); l++ )
			{
				float num5 = (float)((double)l / ((double)Main.maxTilesX * 0.02));
				progress.Set( num5 * 0.34f + 0.66f );
				int num6 = 0;
				int num7 = WorldGen.genRand.Next( 200, Main.maxTilesX - 200 );
				int num8 = WorldGen.genRand.Next( (int)Main.worldSurface, lavaLine );
				int num9 = 0;
				if( Main.tile[num7, num8].wall == 64 )
				{
					num9 = WorldGen.countTiles( num7, num8, true, false );
				}
				while( (num9 >= 1500 || num9 < 10) && num6 < 1000 )
				{
					num6++;
					num7 = WorldGen.genRand.Next( 200, Main.maxTilesX - 200 );
					num8 = WorldGen.genRand.Next( (int)Main.worldSurface, lavaLine );
					if( !Main.wallHouse[(int)Main.tile[num7, num8].wall] )
					{
						if( Main.tile[num7, num8].wall == 64 )
						{
							num9 = WorldGen.countTiles( num7, num8, true, false );
						}
						else
						{
							num9 = 0;
						}
					}
				}
				if( num6 < 1000 )
				{
					WorldGen.Spread.Wall2( num7, num8, 15 );
				}
			}
		}

		// big jungle trees? calls external function.
		private static void JungleTreesFunc( GenerationProgress progress )
		{
			for( int k = 0; k < Main.maxTilesX; k++ )
			{
				for( int l = (int)Main.worldSurface - 1; l < Main.maxTilesY - 350; l++ )
				{
					if( WorldGen.genRand.Next( 10 ) == 0 )
					{
						WorldGen.GrowUndergroundTree( k, l );
					}
				}
			}
		}

		// calls external function.
		private static void FloatingIslandHousesFunc( GenerationProgress progress )
		{
			for( int k = 0; k < numIslandHouses; k++ )
			{
				if( !isSkyLake[k] )
				{
					WorldGen.IslandHouse( floatingIslandHouseX[k], floatingIslandHouseY[k] );
				}
			}
		}

		private static void QuickCleanupFunc( GenerationProgress progress )
		{
			Main.tileSolid[137] = false;
			Main.tileSolid[130] = false;
			for( int k = 20; k < Main.maxTilesX - 20; k++ )
			{
				for( int l = 20; l < Main.maxTilesY - 20; l++ )
				{
					if( Main.tile[k, l].type != 19 && TileID.Sets.CanBeClearedDuringGeneration[(int)Main.tile[k, l].type] )
					{
						if( Main.tile[k, l].topSlope() || Main.tile[k, l].halfBrick() )
						{
							if( !WorldGen.SolidTile( k, l + 1 ) )
							{
								Main.tile[k, l].active( false );
							}
							if( Main.tile[k + 1, l].type == 137 || Main.tile[k - 1, l].type == 137 )
							{
								Main.tile[k, l].active( false );
							}
						}
						else if( Main.tile[k, l].bottomSlope() )
						{
							if( !WorldGen.SolidTile( k, l - 1 ) )
							{
								Main.tile[k, l].active( false );
							}
							if( Main.tile[k + 1, l].type == 137 || Main.tile[k - 1, l].type == 137 )
							{
								Main.tile[k, l].active( false );
							}
						}
					}
				}
			}
		}

		private static void PotsFunc( GenerationProgress progress )
		{
			Main.tileSolid[137] = true;
			Main.tileSolid[130] = true;
			progress.Message = Lang.gen[35].Value;
			for( int k = 0; k < (int)((double)(Main.maxTilesX * Main.maxTilesY) * 0.0008); k++ )
			{
				float num = (float)((double)k / ((double)(Main.maxTilesX * Main.maxTilesY) * 0.0008));
				progress.Set( num );
				bool flag2 = false;
				int num2 = 0;
				while( !flag2 )
				{
					int num3 = WorldGen.genRand.Next( (int)worldSurfaceMax, Main.maxTilesY - 10 );
					if( (double)num > 0.93 )
					{
						num3 = Main.maxTilesY - 150;
					}
					else if( (double)num > 0.75 )
					{
						num3 = (int)worldSurfaceMin;
					}
					int num4 = WorldGen.genRand.Next( 1, Main.maxTilesX );
					bool flag3 = false;
					for( int l = num3; l < Main.maxTilesY; l++ )
					{
						if( !flag3 )
						{
							if( Main.tile[num4, l].active() && Main.tileSolid[(int)Main.tile[num4, l].type] && !Main.tile[num4, l - 1].lava() )
							{
								flag3 = true;
							}
						}
						else
						{
							int style = WorldGen.genRand.Next( 0, 4 );
							int num5 = 0;
							if( l < Main.maxTilesY - 5 )
							{
								num5 = (int)Main.tile[num4, l + 1].type;
							}
							if( num5 == 147 || num5 == 161 || num5 == 162 )
							{
								style = WorldGen.genRand.Next( 4, 7 );
							}
							if( num5 == 60 )
							{
								style = WorldGen.genRand.Next( 7, 10 );
							}
							if( Main.wallDungeon[(int)Main.tile[num4, l].wall] )
							{
								style = WorldGen.genRand.Next( 10, 13 );
							}
							if( num5 == 41 || num5 == 43 || num5 == 44 )
							{
								style = WorldGen.genRand.Next( 10, 13 );
							}
							if( num5 == 22 || num5 == 23 || num5 == 25 )
							{
								style = WorldGen.genRand.Next( 16, 19 );
							}
							if( num5 == 199 || num5 == 203 || num5 == 204 || num5 == 200 )
							{
								style = WorldGen.genRand.Next( 22, 25 );
							}
							if( num5 == 367 )
							{
								style = WorldGen.genRand.Next( 31, 34 );
							}
							if( num5 == 226 )
							{
								style = WorldGen.genRand.Next( 28, 31 );
							}
							if( l > Main.maxTilesY - 200 )
							{
								style = WorldGen.genRand.Next( 13, 16 );
							}
							if( WorldGen.PlacePot( num4, l, 28, style ) )
							{
								flag2 = true;
								break;
							}
							num2++;
							if( num2 >= 10000 )
							{
								flag2 = true;
								break;
							}
						}
					}
				}
			}
		}

		private static void HellforgeFunc( GenerationProgress progress )
		{
			progress.Message = Lang.gen[36].Value;
			for( int k = 0; k < Main.maxTilesX / 200; k++ )
			{
				float value = (float)(k / (Main.maxTilesX / 200));
				progress.Set( value );
				bool flag2 = false;
				int num = 0;
				while( !flag2 )
				{
					int num2 = WorldGen.genRand.Next( 1, Main.maxTilesX );
					int num3 = WorldGen.genRand.Next( Main.maxTilesY - 250, Main.maxTilesY - 5 );
					try
					{
						if( Main.tile[num2, num3].wall != 13 )
						{
							if( Main.tile[num2, num3].wall != 14 )
							{
								continue;
							}
						}
						while( !Main.tile[num2, num3].active() )
						{
							num3++;
						}
						num3--;
						WorldGen.PlaceTile( num2, num3, 77, false, false, -1, 0 );
						if( Main.tile[num2, num3].type == 77 )
						{
							flag2 = true;
						}
						else
						{
							num++;
							if( num >= 10000 )
							{
								flag2 = true;
							}
						}
					}
					catch
					{
					}
				}
			}
		}

		private static void SpreadingGrassFunc( GenerationProgress progress )
		{
			progress.Message = Lang.gen[37].Value;
			for( int k = 0; k < Main.maxTilesX; k++ )
			{
				int i2 = k;
				bool flag2 = true;
				int num = 0;
				while( (double)num < Main.worldSurface - 1.0 )
				{
					if( Main.tile[i2, num].active() )
					{
						if( flag2 && Main.tile[i2, num].type == 0 )
						{
							try
							{
								grassSpread = 0;
								WorldGen.SpreadGrass( i2, num, 0, 2, true, 0 );
							}
							catch
							{
								grassSpread = 0;
								WorldGen.SpreadGrass( i2, num, 0, 2, false, 0 );
							}
						}
						if( (double)num > worldSurfaceMax )
						{
							break;
						}
						flag2 = false;
					}
					else if( Main.tile[i2, num].wall == 0 )
					{
						flag2 = true;
					}
					num++;
				}
			}
		}

		private static void PilesFunc( GenerationProgress progress )
		{
			Main.tileSolid[190] = false;
			Main.tileSolid[196] = false;
			Main.tileSolid[189] = false;
			Main.tileSolid[202] = false;
			int num = 0;
			while( (double)num < (double)Main.maxTilesX * 0.06 )
			{
				bool flag2 = false;
				while( !flag2 )
				{
					int num2 = WorldGen.genRand.Next( 25, Main.maxTilesX - 25 );
					int num3 = WorldGen.genRand.Next( (int)Main.worldSurface, Main.maxTilesY - 300 );
					if( !Main.tile[num2, num3].active() )
					{
						int num4 = 186;
						while( !Main.tile[num2, num3 + 1].active() && num3 < Main.maxTilesY - 5 )
						{
							num3++;
						}
						int num5 = WorldGen.genRand.Next( 22 );
						if( num5 >= 16 && num5 <= 22 )
						{
							num5 = WorldGen.genRand.Next( 22 );
						}
						if( (Main.tile[num2, num3 + 1].type == 0 || Main.tile[num2, num3 + 1].type == 1 || Main.tileMoss[(int)Main.tile[num2, num3 + 1].type]) && WorldGen.genRand.Next( 5 ) == 0 )
						{
							num5 = WorldGen.genRand.Next( 23, 29 );
							num4 = 187;
						}
						if( num3 > Main.maxTilesY - 300 || Main.wallDungeon[(int)Main.tile[num2, num3].wall] || Main.tile[num2, num3 + 1].type == 30 || Main.tile[num2, num3 + 1].type == 19 || Main.tile[num2, num3 + 1].type == 25 || Main.tile[num2, num3 + 1].type == 203 )
						{
							num5 = WorldGen.genRand.Next( 7 );
							num4 = 186;
						}
						if( Main.tile[num2, num3 + 1].type == 147 || Main.tile[num2, num3 + 1].type == 161 || Main.tile[num2, num3 + 1].type == 162 )
						{
							num5 = WorldGen.genRand.Next( 26, 32 );
							num4 = 186;
						}
						if( Main.tile[num2, num3 + 1].type == 60 )
						{
							num4 = 187;
							num5 = WorldGen.genRand.Next( 6 );
						}
						if( (Main.tile[num2, num3 + 1].type == 57 || Main.tile[num2, num3 + 1].type == 58) && WorldGen.genRand.Next( 3 ) < 2 )
						{
							num4 = 187;
							num5 = WorldGen.genRand.Next( 6, 9 );
						}
						if( Main.tile[num2, num3 + 1].type == 226 )
						{
							num4 = 187;
							num5 = WorldGen.genRand.Next( 18, 23 );
						}
						if( Main.tile[num2, num3 + 1].type == 70 )
						{
							num5 = WorldGen.genRand.Next( 32, 35 );
							num4 = 186;
						}
						if( num4 == 186 && num5 >= 7 && num5 <= 15 && WorldGen.genRand.Next( 75 ) == 0 )
						{
							num4 = 187;
							num5 = 17;
						}
						if( Main.wallDungeon[(int)Main.tile[num2, num3].wall] && WorldGen.genRand.Next( 3 ) != 0 )
						{
							flag2 = true;
						}
						else
						{
							WorldGen.PlaceTile( num2, num3, num4, true, false, -1, num5 );
							if( Main.tile[num2, num3].type == 186 || Main.tile[num2, num3].type == 187 )
							{
								flag2 = true;
							}
							if( flag2 && num4 == 186 && num5 <= 7 )
							{
								int num6 = WorldGen.genRand.Next( 1, 5 );
								for( int k = 0; k < num6; k++ )
								{
									int num7 = num2 + WorldGen.genRand.Next( -10, 11 );
									int num8 = num3 - WorldGen.genRand.Next( 5 );
									if( !Main.tile[num7, num8].active() )
									{
										while( !Main.tile[num7, num8 + 1].active() && num8 < Main.maxTilesY - 5 )
										{
											num8++;
										}
										int x = WorldGen.genRand.Next( 12, 36 );
										WorldGen.PlaceSmallPile( num7, num8, x, 0, 185 );
									}
								}
							}
						}
					}
				}
				num++;
			}
			int num9 = 0;
			while( (double)num9 < (double)Main.maxTilesX * 0.01 )
			{
				bool flag3 = false;
				while( !flag3 )
				{
					int num10 = WorldGen.genRand.Next( 25, Main.maxTilesX - 25 );
					int num11 = WorldGen.genRand.Next( Main.maxTilesY - 300, Main.maxTilesY - 10 );
					if( !Main.tile[num10, num11].active() )
					{
						int num12 = 186;
						while( !Main.tile[num10, num11 + 1].active() && num11 < Main.maxTilesY - 5 )
						{
							num11++;
						}
						int num13 = WorldGen.genRand.Next( 22 );
						if( num13 >= 16 && num13 <= 22 )
						{
							num13 = WorldGen.genRand.Next( 22 );
						}
						if( num11 > Main.maxTilesY - 300 || Main.wallDungeon[(int)Main.tile[num10, num11].wall] || Main.tile[num10, num11 + 1].type == 30 || Main.tile[num10, num11 + 1].type == 19 )
						{
							num13 = WorldGen.genRand.Next( 7 );
						}
						if( (Main.tile[num10, num11 + 1].type == 57 || Main.tile[num10, num11 + 1].type == 58) && WorldGen.genRand.Next( 3 ) < 2 )
						{
							num12 = 187;
							num13 = WorldGen.genRand.Next( 6, 9 );
						}
						if( Main.tile[num10, num11 + 1].type == 147 || Main.tile[num10, num11 + 1].type == 161 || Main.tile[num10, num11 + 1].type == 162 )
						{
							num13 = WorldGen.genRand.Next( 26, 32 );
						}
						WorldGen.PlaceTile( num10, num11, num12, true, false, -1, num13 );
						if( Main.tile[num10, num11].type == 186 || Main.tile[num10, num11].type == 187 )
						{
							flag3 = true;
						}
						if( flag3 && num12 == 186 && num13 <= 7 )
						{
							int num14 = WorldGen.genRand.Next( 1, 5 );
							for( int l = 0; l < num14; l++ )
							{
								int num15 = num10 + WorldGen.genRand.Next( -10, 11 );
								int num16 = num11 - WorldGen.genRand.Next( 5 );
								if( !Main.tile[num15, num16].active() )
								{
									while( !Main.tile[num15, num16 + 1].active() && num16 < Main.maxTilesY - 5 )
									{
										num16++;
									}
									int x2 = WorldGen.genRand.Next( 12, 36 );
									WorldGen.PlaceSmallPile( num15, num16, x2, 0, 185 );
								}
							}
						}
					}
				}
				num9++;
			}
			int num17 = 0;
			while( (double)num17 < (double)Main.maxTilesX * 0.003 )
			{
				bool flag4 = false;
				while( !flag4 )
				{
					int num18 = 186;
					int num19 = WorldGen.genRand.Next( 25, Main.maxTilesX - 25 );
					int num20 = WorldGen.genRand.Next( 10, (int)Main.worldSurface );
					if( !Main.tile[num19, num20].active() )
					{
						while( !Main.tile[num19, num20 + 1].active() && num20 < Main.maxTilesY - 5 )
						{
							num20++;
						}
						int num21 = WorldGen.genRand.Next( 7, 13 );
						if( num20 > Main.maxTilesY - 300 || Main.wallDungeon[(int)Main.tile[num19, num20].wall] || Main.tile[num19, num20 + 1].type == 30 || Main.tile[num19, num20 + 1].type == 19 || Main.tile[num19, num20 + 1].type == 53 || Main.tile[num19, num20 + 1].type == 25 || Main.tile[num19, num20 + 1].type == 203 )
						{
							num21 = -1;
						}
						if( Main.tile[num19, num20 + 1].type == 147 || Main.tile[num19, num20 + 1].type == 161 || Main.tile[num19, num20 + 1].type == 162 )
						{
							num21 = WorldGen.genRand.Next( 26, 32 );
						}
						if( Main.tile[num19, num20 + 1].type == 2 || Main.tile[num19 - 1, num20 + 1].type == 2 || Main.tile[num19 + 1, num20 + 1].type == 2 )
						{
							num18 = 187;
							num21 = WorldGen.genRand.Next( 14, 17 );
						}
						if( Main.tile[num19, num20 + 1].type == 151 || Main.tile[num19, num20 + 1].type == 274 )
						{
							num18 = 186;
							num21 = WorldGen.genRand.Next( 7 );
						}
						if( num21 >= 0 )
						{
							WorldGen.PlaceTile( num19, num20, num18, true, false, -1, num21 );
						}
						if( (int)Main.tile[num19, num20].type == num18 )
						{
							flag4 = true;
						}
					}
				}
				num17++;
			}
			int num22 = 0;
			while( (double)num22 < (double)Main.maxTilesX * 0.0035 )
			{
				bool flag5 = false;
				while( !flag5 )
				{
					int num23 = WorldGen.genRand.Next( 25, Main.maxTilesX - 25 );
					int num24 = WorldGen.genRand.Next( 10, (int)Main.worldSurface );
					if( !Main.tile[num23, num24].active() && Main.tile[num23, num24].wall > 0 )
					{
						int num25 = 186;
						while( !Main.tile[num23, num24 + 1].active() && num24 < Main.maxTilesY - 5 )
						{
							num24++;
						}
						int num26 = WorldGen.genRand.Next( 7, 13 );
						if( num24 > Main.maxTilesY - 300 || Main.wallDungeon[(int)Main.tile[num23, num24].wall] || Main.tile[num23, num24 + 1].type == 30 || Main.tile[num23, num24 + 1].type == 19 )
						{
							num26 = -1;
						}
						if( Main.tile[num23, num24 + 1].type == 25 )
						{
							num26 = WorldGen.genRand.Next( 7 );
						}
						if( Main.tile[num23, num24 + 1].type == 147 || Main.tile[num23, num24 + 1].type == 161 || Main.tile[num23, num24 + 1].type == 162 )
						{
							num26 = WorldGen.genRand.Next( 26, 32 );
						}
						if( Main.tile[num23, num24 + 1].type == 2 || Main.tile[num23 - 1, num24 + 1].type == 2 || Main.tile[num23 + 1, num24 + 1].type == 2 )
						{
							num25 = 187;
							num26 = WorldGen.genRand.Next( 14, 17 );
						}
						if( Main.tile[num23, num24 + 1].type == 151 || Main.tile[num23, num24 + 1].type == 274 )
						{
							num25 = 186;
							num26 = WorldGen.genRand.Next( 7 );
						}
						if( num26 >= 0 )
						{
							WorldGen.PlaceTile( num23, num24, num25, true, false, -1, num26 );
						}
						if( (int)Main.tile[num23, num24].type == num25 )
						{
							flag5 = true;
						}
						if( flag5 && num26 <= 7 )
						{
							int num27 = WorldGen.genRand.Next( 1, 5 );
							for( int m = 0; m < num27; m++ )
							{
								int num28 = num23 + WorldGen.genRand.Next( -10, 11 );
								int num29 = num24 - WorldGen.genRand.Next( 5 );
								if( !Main.tile[num28, num29].active() )
								{
									while( !Main.tile[num28, num29 + 1].active() && num29 < Main.maxTilesY - 5 )
									{
										num29++;
									}
									int x3 = WorldGen.genRand.Next( 12, 36 );
									WorldGen.PlaceSmallPile( num28, num29, x3, 0, 185 );
								}
							}
						}
					}
				}
				num22++;
			}
			int num30 = 0;
			while( (double)num30 < (double)Main.maxTilesX * 0.6 )
			{
				bool flag6 = false;
				while( !flag6 )
				{
					int num31 = WorldGen.genRand.Next( 25, Main.maxTilesX - 25 );
					int num32 = WorldGen.genRand.Next( (int)Main.worldSurface, Main.maxTilesY - 20 );
					if( Main.tile[num31, num32].wall == 87 && WorldGen.genRand.Next( 2 ) == 0 )
					{
						num31 = WorldGen.genRand.Next( 25, Main.maxTilesX - 25 );
						num32 = WorldGen.genRand.Next( (int)Main.worldSurface, Main.maxTilesY - 20 );
					}
					if( !Main.tile[num31, num32].active() )
					{
						while( !Main.tile[num31, num32 + 1].active() && num32 < Main.maxTilesY - 5 )
						{
							num32++;
						}
						int num33 = WorldGen.genRand.Next( 2 );
						int num34 = WorldGen.genRand.Next( 36 );
						if( num34 >= 28 && num34 <= 35 )
						{
							num34 = WorldGen.genRand.Next( 36 );
						}
						if( num33 == 1 )
						{
							num34 = WorldGen.genRand.Next( 25 );
							if( num34 >= 16 && num34 <= 24 )
							{
								num34 = WorldGen.genRand.Next( 25 );
							}
						}
						if( num32 > Main.maxTilesY - 300 )
						{
							if( num33 == 0 )
							{
								num34 = WorldGen.genRand.Next( 12, 28 );
							}
							if( num33 == 1 )
							{
								num34 = WorldGen.genRand.Next( 6, 16 );
							}
						}
						if( Main.wallDungeon[(int)Main.tile[num31, num32].wall] || Main.tile[num31, num32 + 1].type == 30 || Main.tile[num31, num32 + 1].type == 19 || Main.tile[num31, num32 + 1].type == 25 || Main.tile[num31, num32 + 1].type == 203 || Main.tile[num31, num32].wall == 87 )
						{
							if( num33 == 0 && num34 < 12 )
							{
								num34 += 12;
							}
							if( num33 == 1 && num34 < 6 )
							{
								num34 += 6;
							}
							if( num33 == 1 && num34 >= 17 )
							{
								num34 -= 10;
							}
						}
						if( Main.tile[num31, num32 + 1].type == 147 || Main.tile[num31, num32 + 1].type == 161 || Main.tile[num31, num32 + 1].type == 162 )
						{
							if( num33 == 0 && num34 < 12 )
							{
								num34 += 36;
							}
							if( num33 == 1 && num34 >= 20 )
							{
								num34 += 6;
							}
							if( num33 == 1 && num34 < 6 )
							{
								num34 += 25;
							}
						}
						if( Main.tile[num31, num32 + 1].type == 151 || Main.tile[num31, num32 + 1].type == 274 )
						{
							if( num33 == 0 )
							{
								num34 = WorldGen.genRand.Next( 12, 28 );
							}
							if( num33 == 1 )
							{
								num34 = WorldGen.genRand.Next( 12, 19 );
							}
						}
						flag6 = ((Main.wallDungeon[(int)Main.tile[num31, num32].wall] && WorldGen.genRand.Next( 3 ) != 0) || WorldGen.PlaceSmallPile( num31, num32, num34, num33, 185 ));
						if( flag6 && num33 == 1 && num34 >= 6 && num34 <= 15 )
						{
							int num35 = WorldGen.genRand.Next( 1, 5 );
							for( int n = 0; n < num35; n++ )
							{
								int num36 = num31 + WorldGen.genRand.Next( -10, 11 );
								int num37 = num32 - WorldGen.genRand.Next( 5 );
								if( !Main.tile[num36, num37].active() )
								{
									while( !Main.tile[num36, num37 + 1].active() && num37 < Main.maxTilesY - 5 )
									{
										num37++;
									}
									int x4 = WorldGen.genRand.Next( 12, 36 );
									WorldGen.PlaceSmallPile( num36, num37, x4, 0, 185 );
								}
							}
						}
					}
				}
				num30++;
			}
			int num38 = 0;
			while( (float)num38 < (float)Main.maxTilesX * 0.02f )
			{
				bool flag7 = false;
				while( !flag7 )
				{
					int num39 = WorldGen.genRand.Next( 25, Main.maxTilesX - 25 );
					int num40 = WorldGen.genRand.Next( 15, (int)Main.worldSurface );
					if( !Main.tile[num39, num40].active() )
					{
						while( !Main.tile[num39, num40 + 1].active() && num40 < Main.maxTilesY - 5 )
						{
							num40++;
						}
						int num41 = WorldGen.genRand.Next( 2 );
						int num42 = WorldGen.genRand.Next( 11 );
						if( num41 == 1 )
						{
							num42 = WorldGen.genRand.Next( 5 );
						}
						if( Main.tile[num39, num40 + 1].type == 147 || Main.tile[num39, num40 + 1].type == 161 || Main.tile[num39, num40 + 1].type == 162 )
						{
							if( num41 == 0 && num42 < 12 )
							{
								num42 += 36;
							}
							if( num41 == 1 && num42 >= 20 )
							{
								num42 += 6;
							}
							if( num41 == 1 && num42 < 6 )
							{
								num42 += 25;
							}
						}
						if( Main.tile[num39, num40 + 1].type == 2 && num41 == 1 )
						{
							num42 = WorldGen.genRand.Next( 38, 41 );
						}
						if( Main.tile[num39, num40 + 1].type == 151 || Main.tile[num39, num40 + 1].type == 274 )
						{
							if( num41 == 0 )
							{
								num42 = WorldGen.genRand.Next( 12, 28 );
							}
							if( num41 == 1 )
							{
								num42 = WorldGen.genRand.Next( 12, 19 );
							}
						}
						if( !Main.wallDungeon[(int)Main.tile[num39, num40].wall] && Main.tile[num39, num40 + 1].type != 30 && Main.tile[num39, num40 + 1].type != 19 && Main.tile[num39, num40 + 1].type != 41 && Main.tile[num39, num40 + 1].type != 43 && Main.tile[num39, num40 + 1].type != 44 && Main.tile[num39, num40 + 1].type != 45 && Main.tile[num39, num40 + 1].type != 46 && Main.tile[num39, num40 + 1].type != 47 && Main.tile[num39, num40 + 1].type != 175 && Main.tile[num39, num40 + 1].type != 176 && Main.tile[num39, num40 + 1].type != 177 && Main.tile[num39, num40 + 1].type != 53 && Main.tile[num39, num40 + 1].type != 25 && Main.tile[num39, num40 + 1].type != 203 )
						{
							flag7 = WorldGen.PlaceSmallPile( num39, num40, num42, num41, 185 );
						}
					}
				}
				num38++;
			}
			int num43 = 0;
			while( (float)num43 < (float)Main.maxTilesX * 0.15f )
			{
				bool flag8 = false;
				while( !flag8 )
				{
					int num44 = WorldGen.genRand.Next( 25, Main.maxTilesX - 25 );
					int num45 = WorldGen.genRand.Next( 15, (int)Main.worldSurface );
					if( !Main.tile[num44, num45].active() )
					{
						if( Main.tile[num44, num45].wall != 2 )
						{
							if( Main.tile[num44, num45].wall != 40 )
							{
								continue;
							}
						}
						while( !Main.tile[num44, num45 + 1].active() && num45 < Main.maxTilesY - 5 )
						{
							num45++;
						}
						int num46 = WorldGen.genRand.Next( 2 );
						int num47 = WorldGen.genRand.Next( 11 );
						if( num46 == 1 )
						{
							num47 = WorldGen.genRand.Next( 5 );
						}
						if( Main.tile[num44, num45 + 1].type == 147 || Main.tile[num44, num45 + 1].type == 161 || Main.tile[num44, num45 + 1].type == 162 )
						{
							if( num46 == 0 && num47 < 12 )
							{
								num47 += 36;
							}
							if( num46 == 1 && num47 >= 20 )
							{
								num47 += 6;
							}
							if( num46 == 1 && num47 < 6 )
							{
								num47 += 25;
							}
						}
						if( Main.tile[num44, num45 + 1].type == 2 && num46 == 1 )
						{
							num47 = WorldGen.genRand.Next( 38, 41 );
						}
						if( Main.tile[num44, num45 + 1].type == 151 || Main.tile[num44, num45 + 1].type == 274 )
						{
							if( num46 == 0 )
							{
								num47 = WorldGen.genRand.Next( 12, 28 );
							}
							if( num46 == 1 )
							{
								num47 = WorldGen.genRand.Next( 12, 19 );
							}
						}
						if( !Main.wallDungeon[(int)Main.tile[num44, num45].wall] && Main.tile[num44, num45 + 1].type != 30 && Main.tile[num44, num45 + 1].type != 19 && Main.tile[num44, num45 + 1].type != 41 && Main.tile[num44, num45 + 1].type != 43 && Main.tile[num44, num45 + 1].type != 44 && Main.tile[num44, num45 + 1].type != 45 && Main.tile[num44, num45 + 1].type != 46 && Main.tile[num44, num45 + 1].type != 47 && Main.tile[num44, num45 + 1].type != 175 && Main.tile[num44, num45 + 1].type != 176 && Main.tile[num44, num45 + 1].type != 177 && Main.tile[num44, num45 + 1].type != 25 && Main.tile[num44, num45 + 1].type != 203 )
						{
							flag8 = WorldGen.PlaceSmallPile( num44, num45, num47, num46, 185 );
						}
					}
				}
				num43++;
			}
			Main.tileSolid[190] = true;
			Main.tileSolid[192] = true;
			Main.tileSolid[196] = true;
			Main.tileSolid[189] = true;
			Main.tileSolid[202] = true;
			Main.tileSolid[225] = true;
		}

		// cactuses?
		private static void Moss2Func( GenerationProgress progress )
		{
			progress.Message = Lang.gen[38].Value;
			int num = 8;
			int num2 = 400;
			int num3 = 4;
			int num4 = 275;
			for( int k = 0; k < 3; k++ )
			{
				int num5;
				int num6;
				bool flag2;
				int maxValue;
				switch( k )
				{
					default:
						num5 = 5;
						num6 = num4;
						flag2 = false;
						maxValue = num3;
						break;
					case 1:
						num5 = num2;
						num6 = Main.maxTilesX - num2;
						flag2 = true;
						maxValue = num;
						break;
					case 2:
						num5 = Main.maxTilesX - num4;
						num6 = Main.maxTilesX - 5;
						flag2 = false;
						maxValue = num3;
						break;
				}
				for( int l = num5; l < num6; l++ )
				{
					if( WorldGen.genRand.Next( maxValue ) == 0 )
					{
						int num7 = 0;
						while( (double)num7 < Main.worldSurface - 1.0 )
						{
							Tile tile = Main.tile[l, num7];
							if( tile.active() && tile.type == 53 )
							{
								Tile tile2 = Main.tile[l, num7 - 1];
								if( !tile2.active() && tile2.wall == 0 )
								{
									if( flag2 )
									{
										WorldGen.PlantCactus( l, num7 );
										break;
									}
									if( Main.tile[l, num7 - 2].liquid == 255 && Main.tile[l, num7 - 3].liquid == 255 && Main.tile[l, num7 - 4].liquid == 255 )
									{
										if( WorldGen.genRand.Next( 2 ) == 0 )
										{
											WorldGen.PlaceTile( l, num7 - 1, 81, true, false, -1, 0 );
											break;
										}
										WorldGen.PlaceTile( l, num7 - 1, 324, true, false, -1, WorldGen.genRand.Next( 2 ) );
										break;
									}
									else if( Main.tile[l, num7 - 2].liquid == 0 )
									{
										WorldGen.PlaceTile( l, num7 - 1, 324, true, false, -1, WorldGen.genRand.Next( 2 ) );
										break;
									}
								}
							}
							num7++;
						}
					}
				}
			}
		}


		private static void SpawnPointFunc( GenerationProgress progress )
		{
			int num = 5;
			bool flag2 = true;
			while( flag2 )
			{
				int num2 = Main.maxTilesX / 2 + WorldGen.genRand.Next( -num, num + 1 );
				for( int k = 0; k < Main.maxTilesY; k++ )
				{
					if( Main.tile[num2, k].active() )
					{
						Main.spawnTileX = num2;
						Main.spawnTileY = k;
						break;
					}
				}
				flag2 = false;
				num++;
				if( (double)Main.spawnTileY > Main.worldSurface )
				{
					flag2 = true;
				}
				if( Main.tile[Main.spawnTileX, Main.spawnTileY - 1].liquid > 0 )
				{
					flag2 = true;
				}
			}
			int num3 = 10;
			while( (double)Main.spawnTileY > Main.worldSurface )
			{
				int num4 = WorldGen.genRand.Next( Main.maxTilesX / 2 - num3, Main.maxTilesX / 2 + num3 );
				for( int l = 0; l < Main.maxTilesY; l++ )
				{
					if( Main.tile[num4, l].active() )
					{
						Main.spawnTileX = num4;
						Main.spawnTileY = l;
						break;
					}
				}
				num3++;
			}
		}


		private static void GrassWallFunc( GenerationProgress progress )
		{
			WorldGen.maxTileCount = 3500;
			for( int k = 50; k < Main.maxTilesX - 50; k++ )
			{
				int num = 0;
				while( (double)num < Main.worldSurface - 10.0 )
				{
					if( WorldGen.genRand.Next( 4 ) == 0 )
					{
						bool flag2 = false;
						int num2 = -1;
						int num3 = -1;
						if( Main.tile[k, num].active() && Main.tile[k, num].type == 2 && (Main.tile[k, num].wall == 2 || Main.tile[k, num].wall == 63) )
						{
							for( int l = k - 1; l <= k + 1; l++ )
							{
								for( int m = num - 1; m <= num + 1; m++ )
								{
									if( Main.tile[l, m].wall == 0 && !WorldGen.SolidTile( l, m ) )
									{
										flag2 = true;
									}
								}
							}
							if( flag2 )
							{
								for( int n = k - 1; n <= k + 1; n++ )
								{
									for( int num4 = num - 1; num4 <= num + 1; num4++ )
									{
										if( (Main.tile[n, num4].wall == 2 || Main.tile[n, num4].wall == 15) && !WorldGen.SolidTile( n, num4 ) )
										{
											num2 = n;
											num3 = num4;
										}
									}
								}
							}
						}
						if( flag2 && num2 > -1 && num3 > -1 && WorldGen.countDirtTiles( num2, num3 ) < WorldGen.maxTileCount )
						{
							try
							{
								WorldGen.Spread.Wall2( num2, num3, 63 );
							}
							catch
							{
							}
						}
					}
					num++;
				}
			}
			for( int num5 = 5; num5 < Main.maxTilesX - 5; num5++ )
			{
				int num6 = 10;
				while( (double)num6 < Main.worldSurface - 1.0 )
				{
					if( Main.tile[num5, num6].wall == 63 && WorldGen.genRand.Next( 10 ) == 0 )
					{
						Main.tile[num5, num6].wall = 65;
					}
					if( Main.tile[num5, num6].active() && Main.tile[num5, num6].type == 0 )
					{
						bool flag3 = false;
						for( int num7 = num5 - 1; num7 <= num5 + 1; num7++ )
						{
							for( int num8 = num6 - 1; num8 <= num6 + 1; num8++ )
							{
								if( Main.tile[num5, num6].wall == 63 || Main.tile[num5, num6].wall == 65 )
								{
									flag3 = true;
									break;
								}
							}
						}
						if( flag3 )
						{
							WorldGen.SpreadGrass( num5, num6, 0, 2, true, 0 );
						}
					}
					num6++;
				}
			}
		}

		private static void GuideFunc( GenerationProgress progress )
		{
			int num = NPC.NewNPC( Main.spawnTileX * 16, Main.spawnTileY * 16, 22, 0, 0f, 0f, 0f, 0f, 255 );
			Main.npc[num].homeTileX = Main.spawnTileX;
			Main.npc[num].homeTileY = Main.spawnTileY;
			Main.npc[num].direction = 1;
			Main.npc[num].homeless = true;
		}

		private static void SunflowersFunc( GenerationProgress progress )
		{
			progress.Message = Lang.gen[39].Value;
			int num = 0;
			while( (double)num < (double)Main.maxTilesX * 0.002 )
			{
				int num2 = Main.maxTilesX / 2;
				int num3 = WorldGen.genRand.Next( Main.maxTilesX );
				int num4 = num3 - WorldGen.genRand.Next( 10 ) - 7;
				int num5 = num3 + WorldGen.genRand.Next( 10 ) + 7;
				if( num4 < 0 )
				{
					num4 = 0;
				}
				if( num5 > Main.maxTilesX - 1 )
				{
					num5 = Main.maxTilesX - 1;
				}
				for( int k = num4; k < num5; k++ )
				{
					int num6 = 1;
					while( (double)num6 < Main.worldSurface - 1.0 )
					{
						if( Main.tile[k, num6].type == 2 && Main.tile[k, num6].active() && !Main.tile[k, num6 - 1].active() )
						{
							WorldGen.PlaceTile( k, num6 - 1, 27, true, false, -1, 0 );
						}
						if( Main.tile[k, num6].active() )
						{
							break;
						}
						num6++;
					}
				}
				num++;
			}
		}

		private static void PlantingTreesFunc( GenerationProgress progress )
		{
			progress.Message = Lang.gen[40].Value;

			// big trees(?)
			int i = 0;
			while( i < Main.maxTilesX * 0.003 )
			{
				int randX = WorldGen.genRand.Next( 50, Main.maxTilesX - 50 );
				int margin = WorldGen.genRand.Next( 25, 50 );
				for( int x = randX - margin; x < randX + margin; x++ )
				{
					int y = 20; // march y to the surface
					while( y < Main.worldSurface )
					{
						WorldGen.GrowEpicTree( x, y );
						y++;
					}
				}
				i++;
			}

			WorldGen.AddTrees();
		}

		private static void HerbsFunc( GenerationProgress progress )
		{
			progress.Message = Lang.gen[41].Value;

			for( int i = 0; i < Main.maxTilesX * 1.7f; i++ )
			{
				WorldGen.PlantAlch();
			}
		}

		private static void DyePlantsFunc( GenerationProgress progress )
		{
			for( int i = 0; i < Main.maxTilesX; i++ )
			{
				int x = WorldGen.genRand.Next( 100, Main.maxTilesX - 100 );
				int y = WorldGen.genRand.Next( 100, Main.maxTilesY - 200 );
				WorldGen.plantDye( x, y, false );
			}
			for( int i = 0; i < Main.maxTilesX / 8; i++ )
			{
				int x = WorldGen.genRand.Next( 100, Main.maxTilesX - 100 );
				int y = WorldGen.genRand.Next( 100, Main.maxTilesY - 200 );
				WorldGen.plantDye( x, y, true );
			}
		}

		private static void HerbsAndHoneyFunc( GenerationProgress progress )
		{
			for( int x = 100; x < Main.maxTilesX - 100; x++ )
			{
				for( int y = (int)Main.worldSurface; y < Main.maxTilesY - 100; y++ )
				{
					if( Main.tile[x, y].wall == WallID.HiveUnsafe )
					{
						if( Main.tile[x, y].liquid > 0 )
						{
							Main.tile[x, y].honey( true );
						}
						if( WorldGen.genRand.Next( 3 ) == 0 )
						{
							WorldGen.PlaceTight( x, y );
						}
					}
					if( Main.tile[x, y].wall == WallID.SpiderUnsafe )
					{
						Main.tile[x, y].liquid = 0;
						Main.tile[x, y].lava( false );
					}
					if( Main.tile[x, y].wall == WallID.SpiderUnsafe && !Main.tile[x, y].active() && WorldGen.genRand.Next( 10 ) != 0 )
					{
						int margin = WorldGen.genRand.Next( 2, 5 );
						int startX = x - margin;
						int endX = x + margin;
						int startY = y - margin;
						int endY = y + margin;
						bool isSolidMargin = false;
						for( int i = startX; i <= endX; i++ )
						{
							for( int j = startY; j <= endY; j++ )
							{
								if( WorldGen.SolidTile( i, j ) )
								{
									isSolidMargin = true;
									break;
								}
							}
						}
						if( isSolidMargin )
						{
							WorldGen.PlaceTile( x, y, TileID.Cobweb, true, false, -1, 0 );
							WorldGen.TileFrame( x, y, false, false );
						}
					}
				}
			}
		}

		// also pumpkins if halloween.
		private static void WeedsFunc( GenerationProgress progress )
		{
			progress.Message = Lang.gen[42].Value;

			if( Main.halloween )
			{
				for( int x = 40; x < Main.maxTilesX - 40; x++ )
				{
					int y = 50;
					while( y < Main.worldSurface )
					{
						if( Main.tile[x, y].active() && Main.tile[x, y].type == 2 && WorldGen.genRand.Next( 15 ) == 0 )
						{
							WorldGen.PlacePumpkin( x, y - 1 );
							int num2 = WorldGen.genRand.Next( 5 );
							for( int l = 0; l < num2; l++ )
							{
								WorldGen.GrowPumpkin( x, y - 1, 254 );
							}
						}
						y++;
					}
				}
			}
			
			WorldGen.AddPlants();
		}

		private static void GlowingMushroomsAndJunglePlantsFunc( GenerationProgress progress )
		{
			for( int x = 0; x < Main.maxTilesX; x++ )
			{
				for( int y = 0; y < Main.maxTilesY; y++ )
				{
					if( Main.tile[x, y].active() )
					{
						if( y >= (int)Main.worldSurface && Main.tile[x, y].type == TileID.MushroomGrass && !Main.tile[x, y - 1].active() )
						{
							WorldGen.GrowShroom( x, y );
							if( !Main.tile[x, y - 1].active() )
							{
								WorldGen.PlaceTile( x, y - 1, TileID.MushroomPlants, true, false, -1, 0 );
							}
						}

						if( Main.tile[x, y].type == TileID.JungleGrass && !Main.tile[x, y - 1].active() )
						{
							WorldGen.PlaceTile( x, y - 1, TileID.JunglePlants, true, false, -1, 0 );
						}
					}
				}
			}
		}

		private static void JunglePlantsFunc( GenerationProgress progress )
		{
			for( int i = 0; i < Main.maxTilesX * 100; i++ )
			{
				int x = WorldGen.genRand.Next( 40, Main.maxTilesX / 2 - 40 );
				if( dungeonSide == -1 )
				{
					x += Main.maxTilesX / 2;
				}
				int y = WorldGen.genRand.Next( Main.maxTilesY - 300 );
				while( !Main.tile[x, y].active() && y < Main.maxTilesY - 300 )
				{
					y++;
				}
				if( Main.tile[x, y].active() && Main.tile[x, y].type == TileID.JungleGrass )
				{
					y--;
					WorldGen.PlaceJunglePlant( x, y, 233, WorldGen.genRand.Next( 8 ), 0 );
					if( Main.tile[x, y].type != 233 )
					{
						WorldGen.PlaceJunglePlant( x, y, 233, WorldGen.genRand.Next( 12 ), 1 );
					}
				}
			}
		}
		
		private static void VinesFunc( GenerationProgress progress )
		{
			progress.Message = Lang.gen[43].Value;
			for( int x = 0; x < Main.maxTilesX; x++ )
			{
				int lengthLeft = 0;
				int yMarch = 0;
				while( yMarch < Main.worldSurface )
				{
					if( lengthLeft > 0 && !Main.tile[x, yMarch].active() )
					{
						Main.tile[x, yMarch].active( true );
						Main.tile[x, yMarch].type = TileID.Vines;
						lengthLeft--;
					}
					else
					{
						lengthLeft = 0;
					}
					if( Main.tile[x, yMarch].active() && !Main.tile[x, yMarch].bottomSlope() && (Main.tile[x, yMarch].type == TileID.Grass || (Main.tile[x, yMarch].type == TileID.LeafBlock && WorldGen.genRand.Next( 4 ) == 0)) && WorldGen.genRand.Next( 5 ) < 3 )
					{
						lengthLeft = WorldGen.genRand.Next( 1, 10 );
					}
					yMarch++;
				}
				lengthLeft = 0;
				for( int y = 0; y < Main.maxTilesY; y++ )
				{
					if( lengthLeft > 0 && !Main.tile[x, y].active() )
					{
						Main.tile[x, y].active( true );
						Main.tile[x, y].type = TileID.JungleVines;
						lengthLeft--;
					}
					else
					{
						lengthLeft = 0;
					}
					if( Main.tile[x, y].active() && Main.tile[x, y].type == TileID.JungleGrass && !Main.tile[x, y].bottomSlope() )
					{
						if( x < Main.maxTilesX - 1 && y < Main.maxTilesY - 2 && Main.tile[x + 1, y].active() && Main.tile[x + 1, y].type == TileID.JungleGrass && !Main.tile[x + 1, y].bottomSlope() && WorldGen.genRand.Next( 40 ) == 0 )
						{
							bool flag2 = true;
							for( int x2 = x; x2 < x + 2; x2++ )
							{
								for( int y2 = y + 1; y2 < y + 3; y2++ )
								{
									if( Main.tile[x2, y2].active() && (!Main.tileCut[Main.tile[x2, y2].type] || Main.tile[x2, y2].type == TileID.BeeHive) )
									{
										flag2 = false;
										break;
									}
									if( Main.tile[x2, y2].liquid > 0 || Main.wallHouse[Main.tile[x2, y2].wall] )
									{
										flag2 = false;
										break;
									}
								}
								if( !flag2 )
								{
									break;
								}
							}
							if( flag2 && WorldGenUtils.CountNearBlocksTypes( x, y, 20, 1, new int[] { TileID.BeeHive } ) > 0 )
							{
								flag2 = false;
							}
							if( flag2 )
							{
								for( int i = x; i < x + 2; i++ )
								{
									for( int j = y + 1; j < y + 3; j++ )
									{
										WorldGen.KillTile( i, j, false, false, false );
									}
								}
								for( int x2 = x; x2 < x + 2; x2++ )
								{
									for( int y2 = y + 1; y2 < y + 3; y2++ )
									{
										Main.tile[x2, y2].active( true );
										Main.tile[x2, y2].type = TileID.BeeHive;
										Main.tile[x2, y2].frameX = (short)((x2 - x) * 18);
										Main.tile[x2, y2].frameY = (short)((y2 - y - 1) * 18);
									}
								}
								break;
							}
						}
						if( WorldGen.genRand.Next( 5 ) < 3 )
						{
							lengthLeft = WorldGen.genRand.Next( 1, 10 );
						}
					}
				}
				lengthLeft = 0;
				for( int y = 0; y < Main.maxTilesY; y++ )
				{
					if( lengthLeft > 0 && !Main.tile[x, y].active() )
					{
						Main.tile[x, y].active( true );
						Main.tile[x, y].type = TileID.CrimsonVines;
						lengthLeft--;
					}
					else
					{
						lengthLeft = 0;
					}
					if( Main.tile[x, y].active() && Main.tile[x, y].type == TileID.FleshGrass && WorldGen.genRand.Next( 5 ) < 3 )
					{
						lengthLeft = WorldGen.genRand.Next( 1, 10 );
					}
				}
			}
		}


		private static void FlowersFunc( GenerationProgress progress )
		{
			progress.Message = Lang.gen[44].Value;
			int num = 0;
			while( (double)num < (double)Main.maxTilesX * 0.005 )
			{
				int num2 = WorldGen.genRand.Next( 20, Main.maxTilesX - 20 );
				int num3 = WorldGen.genRand.Next( 5, 15 );
				int num4 = WorldGen.genRand.Next( 15, 30 );
				int num5 = 1;
				while( (double)num5 < Main.worldSurface - 1.0 )
				{
					if( Main.tile[num2, num5].active() )
					{
						for( int k = num2 - num3; k < num2 + num3; k++ )
						{
							for( int l = num5 - num4; l < num5 + num4; l++ )
							{
								if( Main.tile[k, l].type == 3 || Main.tile[k, l].type == 24 )
								{
									Main.tile[k, l].frameX = (short)(WorldGen.genRand.Next( 6, 8 ) * 18);
									if( Main.tile[k, l].type == 3 && WorldGen.genRand.Next( 2 ) == 0 )
									{
										Main.tile[k, l].frameX = (short)(WorldGen.genRand.Next( 9, 11 ) * 18);
									}
								}
							}
						}
						break;
					}
					num5++;
				}
				num++;
			}
		}

		private static void MushroomsFunc( GenerationProgress progress )
		{
			progress.Message = Lang.gen[45].Value;
			int num = 0;
			while( (double)num < (double)Main.maxTilesX * 0.002 )
			{
				int num2 = WorldGen.genRand.Next( 20, Main.maxTilesX - 20 );
				int num3 = WorldGen.genRand.Next( 4, 10 );
				int num4 = WorldGen.genRand.Next( 15, 30 );
				int num5 = 1;
				while( (double)num5 < Main.worldSurface - 1.0 )
				{
					if( Main.tile[num2, num5].active() )
					{
						for( int k = num2 - num3; k < num2 + num3; k++ )
						{
							for( int l = num5 - num4; l < num5 + num4; l++ )
							{
								if( Main.tile[k, l].type == 3 || Main.tile[k, l].type == 24 )
								{
									Main.tile[k, l].frameX = 144;
								}
								else if( Main.tile[k, l].type == 201 )
								{
									Main.tile[k, l].frameX = 270;
								}
							}
						}
						break;
					}
					num5++;
				}
				num++;
			}
		}

		private static void StalacFunc( GenerationProgress progress )
		{
			for( int k = 20; k < Main.maxTilesX - 20; k++ )
			{
				for( int l = (int)Main.worldSurface; l < Main.maxTilesY - 20; l++ )
				{
					if( !Main.tile[k, l].active() && WorldGen.genRand.Next( 5 ) == 0 )
					{
						if( (Main.tile[k, l - 1].type == 1 || Main.tile[k, l - 1].type == 147 || Main.tile[k, l - 1].type == 161 || Main.tile[k, l - 1].type == 25 || Main.tile[k, l - 1].type == 203 || Main.tileStone[(int)Main.tile[k, l - 1].type] || Main.tileMoss[(int)Main.tile[k, l - 1].type]) && !Main.tile[k, l].active() && !Main.tile[k, l + 1].active() )
						{
							Main.tile[k, l - 1].slope( 0 );
						}
						if( (Main.tile[k, l + 1].type == 1 || Main.tile[k, l + 1].type == 147 || Main.tile[k, l + 1].type == 161 || Main.tile[k, l + 1].type == 25 || Main.tile[k, l + 1].type == 203 || Main.tileStone[(int)Main.tile[k, l + 1].type] || Main.tileMoss[(int)Main.tile[k, l + 1].type]) && !Main.tile[k, l].active() && !Main.tile[k, l - 1].active() )
						{
							Main.tile[k, l + 1].slope( 0 );
						}
						WorldGen.PlaceTight( k, l, 165, false );
					}
				}
				for( int m = 5; m < (int)Main.worldSurface; m++ )
				{
					if( (Main.tile[k, m - 1].type == 147 || Main.tile[k, m - 1].type == 161) && WorldGen.genRand.Next( 5 ) == 0 )
					{
						if( !Main.tile[k, m].active() && !Main.tile[k, m + 1].active() )
						{
							Main.tile[k, m - 1].slope( 0 );
						}
						WorldGen.PlaceTight( k, m, 165, false );
					}
					if( (Main.tile[k, m - 1].type == 25 || Main.tile[k, m - 1].type == 203) && WorldGen.genRand.Next( 5 ) == 0 )
					{
						if( !Main.tile[k, m].active() && !Main.tile[k, m + 1].active() )
						{
							Main.tile[k, m - 1].slope( 0 );
						}
						WorldGen.PlaceTight( k, m, 165, false );
					}
					if( (Main.tile[k, m + 1].type == 25 || Main.tile[k, m + 1].type == 203) && WorldGen.genRand.Next( 5 ) == 0 )
					{
						if( !Main.tile[k, m].active() && !Main.tile[k, m - 1].active() )
						{
							Main.tile[k, m + 1].slope( 0 );
						}
						WorldGen.PlaceTight( k, m, 165, false );
					}
				}
			}
		}

		private static void GemsInIceBiomeFunc( GenerationProgress progress )
		{
			int i = 0;
			while( (double)i < (double)Main.maxTilesX * 0.25 )
			{
				int num2 = WorldGen.genRand.Next( (int)(Main.worldSurface + Main.rockLayer) / 2, lavaLine );
				int num3 = WorldGen.genRand.Next( snowMinX[num2], snowMaxX[num2] );
				if( Main.tile[num3, num2].active() && (Main.tile[num3, num2].type == 147 || Main.tile[num3, num2].type == 161 || Main.tile[num3, num2].type == 162 || Main.tile[num3, num2].type == 224) )
				{
					int num4 = WorldGen.genRand.Next( 1, 4 );
					int num5 = WorldGen.genRand.Next( 1, 4 );
					int num6 = WorldGen.genRand.Next( 1, 4 );
					int num7 = WorldGen.genRand.Next( 1, 4 );
					int num8 = WorldGen.genRand.Next( 12 );
					int style;
					if( num8 < 3 )
					{
						style = 0;
					}
					else if( num8 < 6 )
					{
						style = 1;
					}
					else if( num8 < 8 )
					{
						style = 2;
					}
					else if( num8 < 10 )
					{
						style = 3;
					}
					else if( num8 < 11 )
					{
						style = 4;
					}
					else
					{
						style = 5;
					}
					for( int k = num3 - num4; k < num3 + num5; k++ )
					{
						for( int l = num2 - num6; l < num2 + num7; l++ )
						{
							if( !Main.tile[k, l].active() )
							{
								WorldGen.PlaceTile( k, l, 178, true, false, -1, style );
							}
						}
					}
				}
				i++;
			}
		}

		// passthrough gem tiles.
		private static void RandomGemsFunc( GenerationProgress progress )
		{
			for( int k = 0; k < Main.maxTilesX; k++ )
			{
				int num = WorldGen.genRand.Next( 20, Main.maxTilesX - 20 );
				int num2 = WorldGen.genRand.Next( (int)Main.rockLayer, Main.maxTilesY - 300 );
				if( !Main.tile[num, num2].active() && !Main.tile[num, num2].lava() && !Main.wallDungeon[(int)Main.tile[num, num2].wall] && Main.tile[num, num2].wall != 27 )
				{
					int num3 = WorldGen.genRand.Next( 12 );
					int style;
					if( num3 < 3 )
					{
						style = 0;
					}
					else if( num3 < 6 )
					{
						style = 1;
					}
					else if( num3 < 8 )
					{
						style = 2;
					}
					else if( num3 < 10 )
					{
						style = 3;
					}
					else if( num3 < 11 )
					{
						style = 4;
					}
					else
					{
						style = 5;
					}
					WorldGen.PlaceTile( num, num2, 178, true, false, -1, style );
				}
			}
		}

		private static void MossGrassFunc( GenerationProgress progress )
		{
			for( int k = 5; k < Main.maxTilesX - 5; k++ )
			{
				for( int l = 5; l < Main.maxTilesY - 5; l++ )
				{
					if( Main.tile[k, l].active() && Main.tileMoss[(int)Main.tile[k, l].type] )
					{
						for( int m = 0; m < 4; m++ )
						{
							int num = k;
							int num2 = l;
							if( m == 0 )
							{
								num--;
							}
							if( m == 1 )
							{
								num++;
							}
							if( m == 2 )
							{
								num2--;
							}
							if( m == 3 )
							{
								num2++;
							}
							if( !Main.tile[num, num2].active() )
							{
								WorldGen.PlaceTile( num, num2, 184, true, false, -1, 0 );
							}
						}
					}
				}
			}
		}

		private static void MudWallsInJungleFunc( GenerationProgress progress )
		{
			int num = 0;
			int num2 = 0;
			bool flag2 = false;
			for( int k = 5; k < Main.maxTilesX - 5; k++ )
			{
				int num3 = 0;
				while( (double)num3 < Main.worldSurface + 20.0 )
				{
					if( Main.tile[k, num3].active() && Main.tile[k, num3].type == 60 )
					{
						num = k;
						flag2 = true;
						break;
					}
					num3++;
				}
				if( flag2 )
				{
					break;
				}
			}
			flag2 = false;
			for( int l = Main.maxTilesX - 5; l > 5; l-- )
			{
				int num4 = 0;
				while( (double)num4 < Main.worldSurface + 20.0 )
				{
					if( Main.tile[l, num4].active() && Main.tile[l, num4].type == 60 )
					{
						num2 = l;
						flag2 = true;
						break;
					}
					num4++;
				}
				if( flag2 )
				{
					break;
				}
			}
			for( int m = num; m <= num2; m++ )
			{
				int num5 = 0;
				while( (double)num5 < Main.worldSurface + 20.0 )
				{
					if( ((m >= num + 2 && m <= num2 - 2) || WorldGen.genRand.Next( 2 ) != 0) && ((m >= num + 3 && m <= num2 - 3) || WorldGen.genRand.Next( 3 ) != 0) && (Main.tile[m, num5].wall == 2 || Main.tile[m, num5].wall == 59) )
					{
						Main.tile[m, num5].wall = 15;
					}
					num5++;
				}
			}
		}

		private static void LarvaFunc( GenerationProgress progress )
		{
			for( int n = 0; n < WorldGen.numLarva; n++ )
			{
				int x = WorldGen.larvaX[n];
				int y = WorldGen.larvaY[n];
				for( int i = x - 1; i <= x + 1; i++ )
				{
					for( int j = y - 2; j <= y + 1; j++ )
					{
						if( j != y + 1 )
						{
							Main.tile[i, j].active( false );
						}
						else
						{
							Main.tile[i, j].active( true );
							Main.tile[i, j].type = TileID.Hive;
							Main.tile[i, j].slope( 0 );
							Main.tile[i, j].halfBrick( false );
						}
					}
				}
				WorldGen.PlaceTile( x, y, 231, true, false, -1, 0 );
			}
			Main.tileSolid[232] = true;
			Main.tileSolid[162] = true;
		}

		private static void SettleLiquidsAgainFunc( GenerationProgress progress )
		{
			progress.Message = Lang.gen[27].Value;
			Liquid.QuickWater( 3, -1, -1 );
			WorldGen.WaterCheck();
			int k = 0;
			Liquid.quickSettle = true;
			while( k < 10 )
			{
				int num = Liquid.numLiquid + LiquidBuffer.numLiquidBuffer;
				k++;
				float num2 = 0f;
				while( Liquid.numLiquid > 0 )
				{
					float num3 = (float)(num - (Liquid.numLiquid + LiquidBuffer.numLiquidBuffer)) / (float)num;
					if( Liquid.numLiquid + LiquidBuffer.numLiquidBuffer > num )
					{
						num = Liquid.numLiquid + LiquidBuffer.numLiquidBuffer;
					}
					if( num3 > num2 )
					{
						num2 = num3;
					}
					else
					{
						num3 = num2;
					}
					if( k == 1 )
					{
						progress.Set( num3 / 3f + 0.33f );
					}
					int num4 = 10;
					if( k > num4 )
					{
					}
					Liquid.UpdateLiquid();
				}
				WorldGen.WaterCheck();
				progress.Set( (float)k * 0.1f / 3f + 0.66f );
			}
			Liquid.quickSettle = false;
		}

		private static void TileCleanupFunc( GenerationProgress progress )
		{
			for( int x = 40; x < Main.maxTilesX - 40; x++ )
			{
				for( int y = 40; y < Main.maxTilesY - 40; y++ )
				{
					if( !Main.tile[x, y].active() && Main.tile[x, y].liquid == 0 && WorldGen.genRand.Next( 3 ) != 0 && WorldGen.SolidTile( x, y - 1 ) )
					{
						int num = WorldGen.genRand.Next( 15, 21 );
						for( int m = y - 2; m >= y - num; m-- )
						{
							if( Main.tile[x, m].liquid >= 128 )
							{
								int num2 = 373;
								if( Main.tile[x, m].lava() )
								{
									num2 = 374;
								}
								else if( Main.tile[x, m].honey() )
								{
									num2 = 375;
								}
								int maxValue = y - m;
								if( WorldGen.genRand.Next( maxValue ) <= 1 )
								{
									Main.tile[x, y].type = (ushort)num2;
									Main.tile[x, y].frameX = 0;
									Main.tile[x, y].frameY = 0;
									Main.tile[x, y].active( true );
									break;
								}
							}
						}
						if( !Main.tile[x, y].active() )
						{
							num = WorldGen.genRand.Next( 3, 11 );
							for( int n = y + 1; n <= y + num; n++ )
							{
								if( Main.tile[x, n].liquid >= 200 )
								{
									int num3 = 373;
									if( Main.tile[x, n].lava() )
									{
										num3 = 374;
									}
									else if( Main.tile[x, n].honey() )
									{
										num3 = 375;
									}
									int num4 = n - y;
									if( WorldGen.genRand.Next( num4 * 3 ) <= 1 )
									{
										Main.tile[x, y].type = (ushort)num3;
										Main.tile[x, y].frameX = 0;
										Main.tile[x, y].frameY = 0;
										Main.tile[x, y].active( true );
										break;
									}
								}
							}
						}
						if( !Main.tile[x, y].active() && WorldGen.genRand.Next( 3 ) != 0 )
						{
							Tile tile = Main.tile[x, y - 1];
							if( TileID.Sets.Conversion.Sandstone[(int)tile.type] || TileID.Sets.Conversion.HardenedSand[(int)tile.type] )
							{
								Main.tile[x, y].type = 461;
								Main.tile[x, y].frameX = 0;
								Main.tile[x, y].frameY = 0;
								Main.tile[x, y].active( true );
							}
						}
					}
					if( Main.tile[x, y].type == 137 )
					{
						if( Main.tile[x, y].frameY <= 52 )
						{
							int num5 = -1;
							if( Main.tile[x, y].frameX >= 18 )
							{
								num5 = 1;
							}
							if( Main.tile[x + num5, y].halfBrick() || Main.tile[x + num5, y].slope() != 0 )
							{
								Main.tile[x + num5, y].active( false );
							}
						}
					}
					else if( Main.tile[x, y].type == 162 && Main.tile[x, y + 1].liquid == 0 )
					{
						Main.tile[x, y].active( false );
					}
					if( Main.tile[x, y].wall == 13 || Main.tile[x, y].wall == 14 )
					{
						Main.tile[x, y].liquid = 0;
					}
					if( Main.tile[x, y].type == TileID.ShadowOrbs )
					{
						int frameX = (int)(Main.tile[x, y].frameX / 18);
						int evilComboStyle = 0;
						int num8 = x;
						evilComboStyle += frameX / 2;
						if( MyWorld.evilCombo == EvilCombo.Corruption )
						{
							evilComboStyle = 0;
						}
						else if( MyWorld.evilCombo == EvilCombo.Crimson )
						{
							evilComboStyle = 1;
						}
						frameX %= 2;
						num8 -= frameX;
						int frameY = (int)(Main.tile[x, y].frameY / 18);
						int num10 = 0;
						int num11 = y;
						num10 += frameY / 2;
						frameY %= 2;
						num11 -= frameY;
						for( int i = 0; i < 2; i++ )
						{
							for( int j = 0; j < 2; j++ )
							{
								int xOffset = num8 + i;
								int yOffset = num11 + j;
								Main.tile[xOffset, yOffset].active( true );
								Main.tile[xOffset, yOffset].slope( 0 );
								Main.tile[xOffset, yOffset].halfBrick( false );
								Main.tile[xOffset, yOffset].type = 31;
								Main.tile[xOffset, yOffset].frameX = (short)(i * 18 + 36 * evilComboStyle);
								Main.tile[xOffset, yOffset].frameY = (short)(j * 18 + 36 * num10);
							}
						}
					}
					if( Main.tile[x, y].type == 12 )
					{
						int num16 = (int)(Main.tile[x, y].frameX / 18);
						int num17 = 0;
						int num18 = x;
						num17 += num16 / 2;
						num16 %= 2;
						num18 -= num16;
						int num19 = (int)(Main.tile[x, y].frameY / 18);
						int num20 = 0;
						int num21 = y;
						num20 += num19 / 2;
						num19 %= 2;
						num21 -= num19;
						for( int num22 = 0; num22 < 2; num22++ )
						{
							for( int num23 = 0; num23 < 2; num23++ )
							{
								int num24 = num18 + num22;
								int num25 = num21 + num23;
								Main.tile[num24, num25].active( true );
								Main.tile[num24, num25].slope( 0 );
								Main.tile[num24, num25].halfBrick( false );
								Main.tile[num24, num25].type = 12;
								Main.tile[num24, num25].frameX = (short)(num22 * 18 + 36 * num17);
								Main.tile[num24, num25].frameY = (short)(num23 * 18 + 36 * num20);
							}
							if( !Main.tile[num22, y + 2].active() )
							{
								Main.tile[num22, y + 2].active( true );
								if( !Main.tileSolid[(int)Main.tile[num22, y + 2].type] || Main.tileSolidTop[(int)Main.tile[num22, y + 2].type] )
								{
									Main.tile[num22, y + 2].type = 0;
								}
							}
							Main.tile[num22, y + 2].slope( 0 );
							Main.tile[num22, y + 2].halfBrick( false );
						}
					}
					if( TileID.Sets.BasicChest[(int)Main.tile[x, y].type] )
					{
						int num26 = (int)(Main.tile[x, y].frameX / 18);
						int num27 = 0;
						int num28 = x;
						int num29 = y - (int)(Main.tile[x, y].frameY / 18);
						while( num26 >= 2 )
						{
							num27++;
							num26 -= 2;
						}
						num28 -= num26;
						int num30 = Chest.FindChest( num28, num29 );
						if( num30 != -1 )
						{
							int type = Main.chest[num30].item[0].type;
							if( type != 1156 )
							{
								if( type != 1260 )
								{
									switch( type )
									{
										case 1569:
											num27 = 25;
											break;
										case 1571:
											num27 = 24;
											break;
										case 1572:
											num27 = 27;
											break;
									}
								}
								else
								{
									num27 = 26;
								}
							}
							else
							{
								num27 = 23;
							}
						}
						for( int num31 = 0; num31 < 2; num31++ )
						{
							for( int num32 = 0; num32 < 2; num32++ )
							{
								int num33 = num28 + num31;
								int num34 = num29 + num32;
								Main.tile[num33, num34].active( true );
								Main.tile[num33, num34].slope( 0 );
								Main.tile[num33, num34].halfBrick( false );
								Main.tile[num33, num34].type = 21;
								Main.tile[num33, num34].frameX = (short)(num31 * 18 + 36 * num27);
								Main.tile[num33, num34].frameY = (short)(num32 * 18);
							}
							if( !Main.tile[num31, y + 2].active() )
							{
								Main.tile[num31, y + 2].active( true );
								if( !Main.tileSolid[(int)Main.tile[num31, y + 2].type] || Main.tileSolidTop[(int)Main.tile[num31, y + 2].type] )
								{
									Main.tile[num31, y + 2].type = 0;
								}
							}
							Main.tile[num31, y + 2].slope( 0 );
							Main.tile[num31, y + 2].halfBrick( false );
						}
					}
					if( Main.tile[x, y].type == 28 )
					{
						int num35 = (int)(Main.tile[x, y].frameX / 18);
						int num36 = 0;
						int num37 = x;
						while( num35 >= 2 )
						{
							num36++;
							num35 -= 2;
						}
						num37 -= num35;
						int num38 = (int)(Main.tile[x, y].frameY / 18);
						int num39 = 0;
						int num40 = y;
						while( num38 >= 2 )
						{
							num39++;
							num38 -= 2;
						}
						num40 -= num38;
						for( int num41 = 0; num41 < 2; num41++ )
						{
							for( int num42 = 0; num42 < 2; num42++ )
							{
								int num43 = num37 + num41;
								int num44 = num40 + num42;
								Main.tile[num43, num44].active( true );
								Main.tile[num43, num44].slope( 0 );
								Main.tile[num43, num44].halfBrick( false );
								Main.tile[num43, num44].type = 28;
								Main.tile[num43, num44].frameX = (short)(num41 * 18 + 36 * num36);
								Main.tile[num43, num44].frameY = (short)(num42 * 18 + 36 * num39);
							}
							if( !Main.tile[num41, y + 2].active() )
							{
								Main.tile[num41, y + 2].active( true );
								if( !Main.tileSolid[(int)Main.tile[num41, y + 2].type] || Main.tileSolidTop[(int)Main.tile[num41, y + 2].type] )
								{
									Main.tile[num41, y + 2].type = 0;
								}
							}
							Main.tile[num41, y + 2].slope( 0 );
							Main.tile[num41, y + 2].halfBrick( false );
						}
					}
					if( Main.tile[x, y].type == 26 )
					{
						int num45 = (int)(Main.tile[x, y].frameX / 18);
						int num46 = 0;
						int num47 = x;
						int num48 = y - (int)(Main.tile[x, y].frameY / 18);
						while( num45 >= 3 )
						{
							num46++;
							num45 -= 3;
						}
						num47 -= num45;
						for( int num49 = 0; num49 < 3; num49++ )
						{
							for( int num50 = 0; num50 < 2; num50++ )
							{
								int num51 = num47 + num49;
								int num52 = num48 + num50;
								Main.tile[num51, num52].active( true );
								Main.tile[num51, num52].slope( 0 );
								Main.tile[num51, num52].halfBrick( false );
								Main.tile[num51, num52].type = 26;
								Main.tile[num51, num52].frameX = (short)(num49 * 18 + 54 * num46);
								Main.tile[num51, num52].frameY = (short)(num50 * 18);
							}
							if( !Main.tile[num47 + num49, num48 + 2].active() || !Main.tileSolid[(int)Main.tile[num47 + num49, num48 + 2].type] || Main.tileSolidTop[(int)Main.tile[num47 + num49, num48 + 2].type] )
							{
								Main.tile[num47 + num49, num48 + 2].active( true );
								if( !TileID.Sets.Platforms[(int)Main.tile[num47 + num49, num48 + 2].type] && (!Main.tileSolid[(int)Main.tile[num47 + num49, num48 + 2].type] || Main.tileSolidTop[(int)Main.tile[num47 + num49, num48 + 2].type]) )
								{
									Main.tile[num47 + num49, num48 + 2].type = 0;
								}
							}
							Main.tile[num47 + num49, num48 + 2].slope( 0 );
							Main.tile[num47 + num49, num48 + 2].halfBrick( false );
							if( Main.tile[num47 + num49, num48 + 3].type == 28 && Main.tile[num47 + num49, num48 + 3].frameY % 36 >= 18 )
							{
								Main.tile[num47 + num49, num48 + 3].type = 0;
								Main.tile[num47 + num49, num48 + 3].active( false );
							}
						}
						for( int num53 = 0; num53 < 3; num53++ )
						{
							if( (Main.tile[num47 - 1, num48 + num53].type == 28 || Main.tile[num47 - 1, num48 + num53].type == 12) && Main.tile[num47 - 1, num48 + num53].frameX % 36 < 18 )
							{
								Main.tile[num47 - 1, num48 + num53].type = 0;
								Main.tile[num47 - 1, num48 + num53].active( false );
							}
							if( (Main.tile[num47 + 3, num48 + num53].type == 28 || Main.tile[num47 + 3, num48 + num53].type == 12) && Main.tile[num47 + 3, num48 + num53].frameX % 36 >= 18 )
							{
								Main.tile[num47 + 3, num48 + num53].type = 0;
								Main.tile[num47 + 3, num48 + num53].active( false );
							}
						}
					}
					if( Main.tile[x, y].type == 237 && Main.tile[x, y + 1].type == 232 )
					{
						Main.tile[x, y + 1].type = 226;
					}
				}
			}
		}

		// calls external functions.
		// thin ice cave filled
		// sword shrine
		// campsite
		// mining explosives.
		// mahogany tree (underground)
		// corruption pit
		private static void MicroBiomesFunc( GenerationProgress progress )
		{
			progress.Message = Lang.gen[76].Value;
			float sizeMult = (float)(Main.maxTilesX * Main.maxTilesY) / 5040000f;
			float sizeX = (float)Main.maxTilesX / 4200f;

			int amt = (int)((float)WorldGen.genRand.Next( 3, 6 ) * sizeMult);
			int i = 0;
			while( i < amt )
			{
				if( Biomes<ThinIceBiome>.Place( WorldGen.RandomWorldPoint( (int)Main.worldSurface + 20, 50, 200, 50 ), structures ) )
				{
					i++;
				}
			}

			progress.Set( 0.1f );
			amt = (int)Math.Ceiling( (double)sizeMult );
			i = 0;
			while( i < amt )
			{
				Point origin;
				origin.Y = (int)worldSurface + WorldGen.genRand.Next( 50, 100 );
				if( WorldGen.genRand.Next( 2 ) == 0 )
				{
					origin.X = WorldGen.genRand.Next( 50, (int)((float)Main.maxTilesX * 0.3f) );
				}
				else
				{
					origin.X = WorldGen.genRand.Next( (int)((float)Main.maxTilesX * 0.7f), Main.maxTilesX - 50 );
				}
				if( Biomes<EnchantedSwordBiome>.Place( origin, structures ) )
				{
					i++;
				}
			}

			progress.Set( 0.2f );
			amt = (int)(WorldGen.genRand.Next( 6, 12 ) * sizeMult);
			i = 0;
			while( i < amt )
			{
				if( Biomes<CampsiteBiome>.Place( WorldGen.RandomWorldPoint( (int)Main.worldSurface, 50, 200, 50 ), structures ) )
				{
					i++;
				}
			}

			amt = (int)(WorldGen.genRand.Next( 14, 30 ) * sizeMult);
			i = 0;
			while( i < amt )
			{
				if( Biomes<MiningExplosivesBiome>.Place( WorldGen.RandomWorldPoint( (int)rockLayer, 50, 200, 50 ), structures ) )
				{
					i++;
				}
			}

			progress.Set( 0.3f );
			amt = (int)(WorldGen.genRand.Next( 6, 12 ) * sizeX);
			i = 0;
			int j = 0;
			while( i < amt && j < 20000 ) // either of the 2 can fail and prevent infinite loop.
			{
				if( Biomes<MahoganyTreeBiome>.Place( WorldGen.RandomWorldPoint( (int)Main.worldSurface + 50, 50, 500, 50 ), structures ) )
				{
					i++;
				}
				j++;
			}

			progress.Set( 0.4f );
			if( MyWorld.evilCombo == EvilCombo.Corruption )
			{
				amt = (int)((float)WorldGen.genRand.Next( 1, 3 ) * sizeMult);
				i = 0;
				while( i < amt )
				{
					if( Biomes<CorruptionPitBiome>.Place( WorldGen.RandomWorldPoint( (int)Main.worldSurface, 50, 500, 50 ), structures ) )
					{
						i++;
					}
				}
			}
			TrackGenerator.Run( (int)(10f * sizeMult), (int)(sizeMult * 25f) + 250 );
			progress.Set( 1f );
		}

		private static void FinalCleanupFunc( GenerationProgress progress )
		{
			for( int k = 0; k < Main.maxTilesX; k++ )
			{
				for( int l = 0; l < Main.maxTilesY; l++ )
				{
					if( Main.tile[k, l].active() && (!WorldGen.SolidTile( k, l + 1 ) || !WorldGen.SolidTile( k, l + 2 )) )
					{
						ushort type = Main.tile[k, l].type;
						if( type <= 112 )
						{
							if( type != 53 )
							{
								if( type == 112 )
								{
									Main.tile[k, l].type = 398;
								}
							}
							else
							{
								Main.tile[k, l].type = 397;
							}
						}
						else if( type != 123 )
						{
							if( type != 224 )
							{
								if( type == 234 )
								{
									Main.tile[k, l].type = 399;
								}
							}
							else
							{
								Main.tile[k, l].type = 147;
							}
						}
						else
						{
							Main.tile[k, l].type = 1;
						}
					}
				}
			}
			WorldGen.noTileActions = false;
			WorldGen.gen = false;
			Main.AnglerQuestSwap();
		}



		private static void DungeonChestsFunc( GenerationProgress progress )
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
							item.SetDefaults( ModMain.instance.ItemType( "SapphirePickaxe" ) );
							break;
						case 2:
							item.SetDefaults( ModMain.instance.ItemType( "Blueshift" ) );
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
		
		private static void HellcastleFunc( GenerationProgress progress )
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
						WorldGen.PlaceTile( i, j, ModMain.instance.TileType( "ImperviousBrick" ), false, true );
						Main.tile[posX, posY].slope( 0 );
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

			int x = dungeonSide == -1 ? (Main.maxTilesX / 4) * 3 : (Main.maxTilesX / 4) * 1;
			int y = Main.maxTilesY - 155;
			int width = 100;
			int height = 60;
			int width2 = width / 2;
			int height2 = height / 2;
			for( int i = x - width2; i < x + width2; i++ )
			{
				for( int j = y - height2; j < y + height2; j++ )
				{
					WorldGen.PlaceTile( i, j, ModMain.instance.TileType( "ImperviousBrick" ), false, true );
					Main.tile[i, j].slope( 0 );
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
					WorldGen.PlaceTile( i, j, ModMain.instance.TileType( "ResistantWood" ), false, true );
					Main.tile[i, j].slope( 0 );
				}
			}
			for( int i = x + width2 - 4; i < x + width2; i++ )
			{
				for( int j = y - 2; j < y + 2; j++ )
				{
					WorldGen.PlaceTile( i, j, ModMain.instance.TileType( "ResistantWood" ), false, true );
					Main.tile[i, j].slope( 0 );
				}
			}
			for( int i = 0; i < 3; i++ )
			{
				generateUpperLootRooms( WorldGen.genRand.Next( x - width2, x + width2 ), y - height2 - WorldGen.genRand.Next( 5, 20 ) );
			}
		}

		private static void CloudsFunc( GenerationProgress progress )
		{
			progress.Message = Lang.gen[6].Value;
			
			for( int i = 0; i < (int)(Main.maxTilesX * Main.maxTilesY * 0.000090); i++ )
			{
				int x = WorldGen.genRand.Next( 10, Main.maxTilesX - 10 );
				int y = WorldGen.genRand.Next( 90, (int)worldSurfaceMin );
				WorldGenUtils.WallRunner( x, y, WorldGen.genRand.Next( 10, 30 ), WorldGen.genRand.Next( 30, 75 ), WallID.Cloud, false, 6f, 0.7f, false, false );
			}
		}


		private static void HMGemsFunc( GenerationProgress progress )
		{
			//progress.Message = Lang.gen[23].Value;

			float count = Main.maxTilesX * 0.1f;
			count *= 0.2f;

			for( int i = 0; i < count; i++ )
			{
				int x = WorldGen.genRand.Next( 0, Main.maxTilesX );
				int y = WorldGen.genRand.Next( (int)Main.rockLayer, Main.maxTilesY );
				while( Main.tile[x, y].type != 1 ) // find stone block
				{
					x = WorldGen.genRand.Next( 0, Main.maxTilesX );
					y = WorldGen.genRand.Next( (int)Main.rockLayer, Main.maxTilesY );
				}
				WorldGenUtils.TileRunner( x, y, WorldGen.genRand.Next( 2, 6 ), WorldGen.genRand.Next( 3, 7 ), ModMain.instance.TileType("OpalBlock") );
			}
		}



		private static void SHMShiniesFunc( GenerationProgress progress )
		{
			// worldSurfaceLow is actually the highest surface tile. In practice you might want to use WorldGen.rockLayer or other WorldGen values.
			//progress.Message = "Superhardmode Shinies";


			// COPPER (top, mid, - )

			for( int i = 0; i < (int)(Main.maxTilesX * Main.maxTilesY * 0.000070); i++ )
				WorldGenUtils.TileRunner( WorldGen.genRand.Next( 0, Main.maxTilesX ), WorldGen.genRand.Next( (int)(Main.maxTilesY * 0.8), (int)(Main.maxTilesY * 0.9) ), WorldGen.genRand.Next( 4, 5 ), WorldGen.genRand.Next( 3, 5 ), ModMain.instance.TileType( "Cinderplate" ) );

			for( int i = 0; i < (int)(Main.maxTilesX * Main.maxTilesY * 0.000070); i++ )
				WorldGenUtils.TileRunner( WorldGen.genRand.Next( 0, Main.maxTilesX ), WorldGen.genRand.Next( (int)(Main.maxTilesY * 0.6), (int)Main.maxTilesY ), WorldGen.genRand.Next( 3, 5 ), WorldGen.genRand.Next( 3, 6 ), ModMain.instance.TileType( "Cinderplate" ) );
			
		}
	}
}
