using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;

namespace TerrariaReworked
{
	public class WorldGenUtils
	{
		public static void TileRunner( int i, int j, double strength, int steps, int type, bool addTile = false, float speedX = 0f, float speedY = 0f, bool noYChange = false, bool @override = true )
		{
			float stepsFloat = (float)steps;

			Vector2 vec1 = new Vector2( i, j );
			Vector2 vec2 = new Vector2( WorldGen.genRand.Next( -10, 11 ) * 0.1f, WorldGen.genRand.Next( -10, 11 ) * 0.1f );

			if( speedX != 0f || speedY != 0f )
			{
				vec2.X = speedX;
				vec2.Y = speedY;
			}
			bool isGraniteBlock = (type == 368);
			bool isMarbleBlock = (type == 367);
			while( strength > 0.0 && stepsFloat > 0f )
			{
				if( vec1.Y < 0f && stepsFloat > 0f && type == 59 )
				{
					stepsFloat = 0f;
				}
				strength = strength * (double)(stepsFloat / (float)steps);
				stepsFloat -= 1f;
				int startX = (int)((double)vec1.X - strength * 0.5);
				int endX = (int)((double)vec1.X + strength * 0.5);
				int startY = (int)((double)vec1.Y - strength * 0.5);
				int endY = (int)((double)vec1.Y + strength * 0.5);
				if( startX < 1 )
				{
					startX = 1;
				}
				if( endX > Main.maxTilesX - 1 )
				{
					endX = Main.maxTilesX - 1;
				}
				if( startY < 1 )
				{
					startY = 1;
				}
				if( endY > Main.maxTilesY - 1 )
				{
					endY = Main.maxTilesY - 1;
				}
				for( int x = startX; x < endX; x++ )
				{
					for( int y = startY; y < endY; y++ )
					{
						if( (double)(Math.Abs( (float)x - vec1.X ) + Math.Abs( (float)y - vec1.Y )) < strength * 0.5 * (1.0 + (double)WorldGen.genRand.Next( -10, 11 ) * 0.015) )
						{
							if( WorldGenPasses.mudWall && (double)y > Main.worldSurface && Main.tile[x, y - 1].wall != 2 && y < Main.maxTilesY - 210 - WorldGen.genRand.Next( 3 ) )
							{
								if( y > WorldGenPasses.lavaLine - WorldGen.genRand.Next( 0, 4 ) - 50 )
								{
									if( Main.tile[x, y - 1].wall != 64 && Main.tile[x, y + 1].wall != 64 && Main.tile[x - 1, y].wall != 64 && Main.tile[x, y + 1].wall != 64 )
									{
										WorldGen.PlaceWall( x, y, 15, true );
									}
								}
								else if( Main.tile[x, y - 1].wall != 15 && Main.tile[x, y + 1].wall != 15 && Main.tile[x - 1, y].wall != 15 && Main.tile[x, y + 1].wall != 15 )
								{
									WorldGen.PlaceWall( x, y, 64, true );
								}
							}
							// cover with lava if 'type' passed == -2.
							if( type < 0 )
							{
								if( type == -2 && Main.tile[x, y].active() && (y < WorldGenPasses.waterLine || y > WorldGenPasses.lavaLine) )
								{
									Main.tile[x, y].liquid = byte.MaxValue;
									if( y > WorldGenPasses.lavaLine )
									{
										Main.tile[x, y].lava( true );
									}
								}
								Main.tile[x, y].active( false );
							}
							else
							{
								if( isGraniteBlock && (double)(Math.Abs( (float)x - vec1.X ) + Math.Abs( (float)y - vec1.Y )) < strength * 0.3 * (1.0 + (double)WorldGen.genRand.Next( -10, 11 ) * 0.01) )
								{
									WorldGen.PlaceWall( x, y, 180, true );
								}
								if( isMarbleBlock && (double)(Math.Abs( (float)x - vec1.X ) + Math.Abs( (float)y - vec1.Y )) < strength * 0.3 * (1.0 + (double)WorldGen.genRand.Next( -10, 11 ) * 0.01) )
								{
									WorldGen.PlaceWall( x, y, 178, true );
								}
								if( @override || !Main.tile[x, y].active() )
								{
									Tile tile = Main.tile[x, y];
									bool placementBlocked = Main.tileStone[type] && tile.type != 1;
									if( !TileID.Sets.CanBeClearedDuringGeneration[(int)tile.type] )
									{
										placementBlocked = true;
									}
									ushort typeAtXY = tile.type;
									if( typeAtXY <= 147 )
									{
										if( typeAtXY <= 45 )
										{
											if( typeAtXY != 1 )
											{
												if( typeAtXY == 45 )
												{
													placementBlocked = true;
												}
											}
											else if( type == 59 && (double)y < Main.worldSurface + (double)WorldGen.genRand.Next( -50, 50 ) )
											{
												placementBlocked = true;
											}
										}
										else if( typeAtXY != 53 )
										{
											if( typeAtXY == 147 )
											{
												placementBlocked = true;
											}
										}
										else
										{
											if( type == 40 )
											{
												placementBlocked = true;
											}
											if( (double)y < Main.worldSurface && type != 59 )
											{
												placementBlocked = true;
											}
										}
									}
									else if( typeAtXY <= 196 )
									{
										if( typeAtXY - 189 <= 1 || typeAtXY == 196 )
										{
											placementBlocked = true;
										}
									}
									else if( typeAtXY - 367 > 1 )
									{
										if( typeAtXY - 396 <= 1 )
										{
											placementBlocked = !TileID.Sets.Ore[type];
										}
									}
									else if( type == 59 )
									{
										placementBlocked = true;
									}


									if( !placementBlocked )
									{
										tile.type = (ushort)type;
									}
								}
								if( addTile )
								{
									Main.tile[x, y].active( true );
									Main.tile[x, y].liquid = 0;
									Main.tile[x, y].lava( false );
								}
								if( noYChange && (double)y < Main.worldSurface && type != 59 )
								{
									Main.tile[x, y].wall = 2;
								}
								if( type == 59 && y > WorldGenPasses.waterLine && Main.tile[x, y].liquid > 0 )
								{
									Main.tile[x, y].lava( false );
									Main.tile[x, y].liquid = 0;
								}
							}
						}
					}
				}
				vec1 += vec2;
				if( strength > 50.0 )
				{
					vec1 += vec2;
					stepsFloat -= 1f;
					vec2.Y += (float)WorldGen.genRand.Next( -10, 11 ) * 0.05f;
					vec2.X += (float)WorldGen.genRand.Next( -10, 11 ) * 0.05f;
					if( strength > 100.0 )
					{
						vec1 += vec2;
						stepsFloat -= 1f;
						vec2.Y += (float)WorldGen.genRand.Next( -10, 11 ) * 0.05f;
						vec2.X += (float)WorldGen.genRand.Next( -10, 11 ) * 0.05f;
						if( strength > 150.0 )
						{
							vec1 += vec2;
							stepsFloat -= 1f;
							vec2.Y += (float)WorldGen.genRand.Next( -10, 11 ) * 0.05f;
							vec2.X += (float)WorldGen.genRand.Next( -10, 11 ) * 0.05f;
							if( strength > 200.0 )
							{
								vec1 += vec2;
								stepsFloat -= 1f;
								vec2.Y += (float)WorldGen.genRand.Next( -10, 11 ) * 0.05f;
								vec2.X += (float)WorldGen.genRand.Next( -10, 11 ) * 0.05f;
								if( strength > 250.0 )
								{
									vec1 += vec2;
									stepsFloat -= 1f;
									vec2.Y += (float)WorldGen.genRand.Next( -10, 11 ) * 0.05f;
									vec2.X += (float)WorldGen.genRand.Next( -10, 11 ) * 0.05f;
									if( strength > 300.0 )
									{
										vec1 += vec2;
										stepsFloat -= 1f;
										vec2.Y += (float)WorldGen.genRand.Next( -10, 11 ) * 0.05f;
										vec2.X += (float)WorldGen.genRand.Next( -10, 11 ) * 0.05f;
										if( strength > 400.0 )
										{
											vec1 += vec2;
											stepsFloat -= 1f;
											vec2.Y += (float)WorldGen.genRand.Next( -10, 11 ) * 0.05f;
											vec2.X += (float)WorldGen.genRand.Next( -10, 11 ) * 0.05f;
											if( strength > 500.0 )
											{
												vec1 += vec2;
												stepsFloat -= 1f;
												vec2.Y += (float)WorldGen.genRand.Next( -10, 11 ) * 0.05f;
												vec2.X += (float)WorldGen.genRand.Next( -10, 11 ) * 0.05f;
												if( strength > 600.0 )
												{
													vec1 += vec2;
													stepsFloat -= 1f;
													vec2.Y += (float)WorldGen.genRand.Next( -10, 11 ) * 0.05f;
													vec2.X += (float)WorldGen.genRand.Next( -10, 11 ) * 0.05f;
													if( strength > 700.0 )
													{
														vec1 += vec2;
														stepsFloat -= 1f;
														vec2.Y += (float)WorldGen.genRand.Next( -10, 11 ) * 0.05f;
														vec2.X += (float)WorldGen.genRand.Next( -10, 11 ) * 0.05f;
														if( strength > 800.0 )
														{
															vec1 += vec2;
															stepsFloat -= 1f;
															vec2.Y += (float)WorldGen.genRand.Next( -10, 11 ) * 0.05f;
															vec2.X += (float)WorldGen.genRand.Next( -10, 11 ) * 0.05f;
															if( strength > 900.0 )
															{
																vec1 += vec2;
																stepsFloat -= 1f;
																vec2.Y += (float)WorldGen.genRand.Next( -10, 11 ) * 0.05f;
																vec2.X += (float)WorldGen.genRand.Next( -10, 11 ) * 0.05f;
															}
														}
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
				vec2.X += (float)WorldGen.genRand.Next( -10, 11 ) * 0.05f;
				if( vec2.X > 1f )
				{
					vec2.X = 1f;
				}
				if( vec2.X < -1f )
				{
					vec2.X = -1f;
				}
				if( !noYChange )
				{
					vec2.Y += (float)WorldGen.genRand.Next( -10, 11 ) * 0.05f;
					if( vec2.Y > 1f )
					{
						vec2.Y = 1f;
					}
					if( vec2.Y < -1f )
					{
						vec2.Y = -1f;
					}
				}
				else if( type != 59 && strength < 3.0 )
				{
					if( vec2.Y > 1f )
					{
						vec2.Y = 1f;
					}
					if( vec2.Y < -1f )
					{
						vec2.Y = -1f;
					}
				}
				if( type == 59 && !noYChange )
				{
					if( (double)vec2.Y > 0.5 )
					{
						vec2.Y = 0.5f;
					}
					if( (double)vec2.Y < -0.5 )
					{
						vec2.Y = -0.5f;
					}
					if( (double)vec1.Y < Main.rockLayer + 100.0 )
					{
						vec2.Y = 1f;
					}
					if( vec1.Y > (float)(Main.maxTilesY - 300) )
					{
						vec2.Y = -1f;
					}
				}
			}
		}

		public static void WallRunner( int i, int j, double strength, int steps, int type, bool addWall = false, float speedX = 0f, float speedY = 0f, bool noYChange = false, bool replace = true )
		{
			float stepsFloat = (float)steps;

			Vector2 vec1 = new Vector2( i, j );
			Vector2 vec2 = new Vector2( WorldGen.genRand.Next( -10, 11 ) * 0.1f, WorldGen.genRand.Next( -10, 11 ) * 0.1f );

			if( speedX != 0f || speedY != 0f )
			{
				vec2.X = speedX;
				vec2.Y = speedY;
			}
			while( strength > 0.0 && stepsFloat > 0f )
			{
				if( vec1.Y < 0f && stepsFloat > 0f && type == 59 )
				{
					stepsFloat = 0f;
				}
				strength = strength * (double)(stepsFloat / (float)steps);
				stepsFloat -= 1f;
				int startX = (int)((double)vec1.X - strength * 0.5);
				int endX = (int)((double)vec1.X + strength * 0.5);
				int startY = (int)((double)vec1.Y - strength * 0.5);
				int endY = (int)((double)vec1.Y + strength * 0.5);

				if( startX < 1 )
				{
					startX = 1;
				}
				if( endX > Main.maxTilesX - 1 )
				{
					endX = Main.maxTilesX - 1;
				}
				if( startY < 1 )
				{
					startY = 1;
				}
				if( endY > Main.maxTilesY - 1 )
				{
					endY = Main.maxTilesY - 1;
				}

				for( int x = startX; x < endX; x++ )
				{
					for( int y = startY; y < endY; y++ )
					{
						if( (double)(Math.Abs( (float)x - vec1.X ) + Math.Abs( (float)y - vec1.Y )) < strength * 0.5 * (1.0 + (double)WorldGen.genRand.Next( -10, 11 ) * 0.015) )
						{
							if( type >= 0 )
							{
								if( replace || Main.tile[x,y].wall == 0 )
								{
									Main.tile[x, y].wall = (ushort)type;
								}
							}
						}
					}
				}
				vec1 += vec2;
				if( strength > 50.0 )
				{
					vec1 += vec2;
					stepsFloat -= 1f;
					vec2.Y += (float)WorldGen.genRand.Next( -10, 11 ) * 0.05f;
					vec2.X += (float)WorldGen.genRand.Next( -10, 11 ) * 0.05f;
					if( strength > 100.0 )
					{
						vec1 += vec2;
						stepsFloat -= 1f;
						vec2.Y += (float)WorldGen.genRand.Next( -10, 11 ) * 0.05f;
						vec2.X += (float)WorldGen.genRand.Next( -10, 11 ) * 0.05f;
						if( strength > 150.0 )
						{
							vec1 += vec2;
							stepsFloat -= 1f;
							vec2.Y += (float)WorldGen.genRand.Next( -10, 11 ) * 0.05f;
							vec2.X += (float)WorldGen.genRand.Next( -10, 11 ) * 0.05f;
							if( strength > 200.0 )
							{
								vec1 += vec2;
								stepsFloat -= 1f;
								vec2.Y += (float)WorldGen.genRand.Next( -10, 11 ) * 0.05f;
								vec2.X += (float)WorldGen.genRand.Next( -10, 11 ) * 0.05f;
								if( strength > 250.0 )
								{
									vec1 += vec2;
									stepsFloat -= 1f;
									vec2.Y += (float)WorldGen.genRand.Next( -10, 11 ) * 0.05f;
									vec2.X += (float)WorldGen.genRand.Next( -10, 11 ) * 0.05f;
									if( strength > 300.0 )
									{
										vec1 += vec2;
										stepsFloat -= 1f;
										vec2.Y += (float)WorldGen.genRand.Next( -10, 11 ) * 0.05f;
										vec2.X += (float)WorldGen.genRand.Next( -10, 11 ) * 0.05f;
										if( strength > 400.0 )
										{
											vec1 += vec2;
											stepsFloat -= 1f;
											vec2.Y += (float)WorldGen.genRand.Next( -10, 11 ) * 0.05f;
											vec2.X += (float)WorldGen.genRand.Next( -10, 11 ) * 0.05f;
											if( strength > 500.0 )
											{
												vec1 += vec2;
												stepsFloat -= 1f;
												vec2.Y += (float)WorldGen.genRand.Next( -10, 11 ) * 0.05f;
												vec2.X += (float)WorldGen.genRand.Next( -10, 11 ) * 0.05f;
												if( strength > 600.0 )
												{
													vec1 += vec2;
													stepsFloat -= 1f;
													vec2.Y += (float)WorldGen.genRand.Next( -10, 11 ) * 0.05f;
													vec2.X += (float)WorldGen.genRand.Next( -10, 11 ) * 0.05f;
													if( strength > 700.0 )
													{
														vec1 += vec2;
														stepsFloat -= 1f;
														vec2.Y += (float)WorldGen.genRand.Next( -10, 11 ) * 0.05f;
														vec2.X += (float)WorldGen.genRand.Next( -10, 11 ) * 0.05f;
														if( strength > 800.0 )
														{
															vec1 += vec2;
															stepsFloat -= 1f;
															vec2.Y += (float)WorldGen.genRand.Next( -10, 11 ) * 0.05f;
															vec2.X += (float)WorldGen.genRand.Next( -10, 11 ) * 0.05f;
															if( strength > 900.0 )
															{
																vec1 += vec2;
																stepsFloat -= 1f;
																vec2.Y += (float)WorldGen.genRand.Next( -10, 11 ) * 0.05f;
																vec2.X += (float)WorldGen.genRand.Next( -10, 11 ) * 0.05f;
															}
														}
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
				vec2.X += (float)WorldGen.genRand.Next( -10, 11 ) * 0.05f;
				if( vec2.X > 1f )
				{
					vec2.X = 1f;
				}
				if( vec2.X < -1f )
				{
					vec2.X = -1f;
				}
				if( !noYChange )
				{
					vec2.Y += (float)WorldGen.genRand.Next( -10, 11 ) * 0.05f;
					if( vec2.Y > 1f )
					{
						vec2.Y = 1f;
					}
					if( vec2.Y < -1f )
					{
						vec2.Y = -1f;
					}
				}
				else if( type != 59 && strength < 3.0 )
				{
					if( vec2.Y > 1f )
					{
						vec2.Y = 1f;
					}
					if( vec2.Y < -1f )
					{
						vec2.Y = -1f;
					}
				}
				if( type == 59 && !noYChange )
				{
					if( (double)vec2.Y > 0.5 )
					{
						vec2.Y = 0.5f;
					}
					if( (double)vec2.Y < -0.5 )
					{
						vec2.Y = -0.5f;
					}
					if( (double)vec1.Y < Main.rockLayer + 100.0 )
					{
						vec2.Y = 1f;
					}
					if( vec1.Y > (float)(Main.maxTilesY - 300) )
					{
						vec2.Y = -1f;
					}
				}
			}
		}


		public static ushort corruptOre = TileID.Demonite;
		public static ushort corruptStone = TileID.Ebonstone;
		public static ushort corruptSand = TileID.Ebonsand;
		public static ushort corruptGrass = TileID.CorruptGrass;
		public static ushort corruptAltar = TileID.DemonAltar;
		public static short corruptAltarData = 0;
		public static ushort corruptOrb = TileID.ShadowOrbs;
		public static short corruptOrbFrameX = 0; // 0 or 36

		public static ushort corruptWall = WallID.EbonstoneUnsafe;

		// Terraria.WorldGen
		// Token: 0x06000753 RID: 1875 RVA: 0x0035CC5C File Offset: 0x0035AE5C
		public static void AddOrb( int x, int y )
		{
			if( x < 10 || x > Main.maxTilesX - 10 )
			{
				return;
			}
			if( y < 10 || y > Main.maxTilesY - 10 )
			{
				return;
			}
			for( int i = x - 1; i < x + 1; i++ )
			{
				for( int j = y - 1; j < y + 1; j++ )
				{
					if( Main.tile[i, j].active() && Main.tile[i, j].type == corruptOrb )
					{
						return;
					}
				}
			}
			short num = corruptOrbFrameX;
			
			Main.tile[x - 1, y - 1].active( true );
			Main.tile[x - 1, y - 1].type = corruptOrb;
			Main.tile[x - 1, y - 1].frameX = num;
			Main.tile[x - 1, y - 1].frameY = 0;
			Main.tile[x, y - 1].active( true );
			Main.tile[x, y - 1].type = corruptOrb;
			Main.tile[x, y - 1].frameX = (short)(18 + num);
			Main.tile[x, y - 1].frameY = 0;
			Main.tile[x - 1, y].active( true );
			Main.tile[x - 1, y].type = corruptOrb;
			Main.tile[x - 1, y].frameX = num;
			Main.tile[x - 1, y].frameY = 18;
			Main.tile[x, y].active( true );
			Main.tile[x, y].type = corruptOrb;
			Main.tile[x, y].frameX = (short)(18 + num);
			Main.tile[x, y].frameY = 18;
		}


		// Terraria.WorldGen
		// Token: 0x06000829 RID: 2089 RVA: 0x0039B2D0 File Offset: 0x003994D0
		public static void ChasmRunner( int i, int j, int _steps, bool makeOrb = false )
		{
			bool flag = false;
			bool dontMakeOrbs = false;
			bool flag3 = false;
			if( !makeOrb )
			{
				dontMakeOrbs = true;
			}

			float steps = _steps;

			Vector2 pos = new Vector2( i, j );
			Vector2 vector2;
			vector2.X = WorldGen.genRand.Next( -10, 11 ) * 0.1f;
			vector2.Y = WorldGen.genRand.Next( 11 ) * 0.2f + 0.5f;
			int num2 = 5;
			double num3 = WorldGen.genRand.Next( 5 ) + 7;
			while( num3 > 0.0 )
			{
				if( steps > 0f )
				{
					num3 += WorldGen.genRand.Next( 3 );
					num3 -= WorldGen.genRand.Next( 3 );
					if( num3 < 7.0 )
					{
						num3 = 7.0;
					}
					if( num3 > 20.0 )
					{
						num3 = 20.0;
					}
					if( steps == 1f && num3 < 10.0 )
					{
						num3 = 10.0;
					}
				}
				else if( pos.Y > Main.worldSurface + 45.0 )
				{
					num3 -= WorldGen.genRand.Next( 4 );
				}
				if( pos.Y > Main.rockLayer && steps > 0f )
				{
					steps = 0f;
				}
				steps -= 1f;
				if( !flag && pos.Y > Main.worldSurface + 20.0 )
				{
					flag = true;
					WorldGenUtils.ChasmRunnerSideways( (int)pos.X, (int)pos.Y, -1, WorldGen.genRand.Next( 20, 40 ) );
					WorldGenUtils.ChasmRunnerSideways( (int)pos.X, (int)pos.Y, 1, WorldGen.genRand.Next( 20, 40 ) );
				}
				int startX;
				int endX;
				int startY;
				int endY;
				if( steps > num2 )
				{
					startX = (int)(pos.X - num3 * 0.5);
					endX = (int)(pos.X + num3 * 0.5);
					startY = (int)(pos.Y - num3 * 0.5);
					endY = (int)(pos.Y + num3 * 0.5);
					if( startX < 0 )
					{
						startX = 0;
					}
					if( endX > Main.maxTilesX - 1 )
					{
						endX = Main.maxTilesX - 1;
					}
					if( startY < 0 )
					{
						startY = 0;
					}
					if( endY > Main.maxTilesY )
					{
						endY = Main.maxTilesY;
					}
					for( int x = startX; x < endX; x++ )
					{
						for( int y = startY; y < endY; y++ )
						{
							if( (double)(Math.Abs( (float)x - pos.X ) + Math.Abs( (float)y - pos.Y )) < num3 * 0.5 * (1.0 + (double)WorldGen.genRand.Next( -10, 11 ) * 0.015) && Main.tile[x, y].type != corruptOrb && Main.tile[x, y].type != corruptOre )
							{
								Main.tile[x, y].active( false );
							}
						}
					}
				}
				if( steps <= 2f && (double)pos.Y < Main.worldSurface + 45.0 )
				{
					steps = 2f;
				}
				if( steps <= 0f )
				{
					if( !dontMakeOrbs )
					{
						dontMakeOrbs = true;
						WorldGenUtils.AddOrb( (int)pos.X, (int)pos.Y );
					}
					else if( !flag3 )
					{
						flag3 = false;
						bool flag4 = false;
						int num8 = 0;
						while( !flag4 )
						{
							int x = WorldGen.genRand.Next( (int)pos.X - 25, (int)pos.X + 25 );
							int y = WorldGen.genRand.Next( (int)pos.Y - 50, (int)pos.Y );
							if( x < 5 )
							{
								x = 5;
							}
							if( x > Main.maxTilesX - 5 )
							{
								x = Main.maxTilesX - 5;
							}
							if( y < 5 )
							{
								y = 5;
							}
							if( y > Main.maxTilesY - 5 )
							{
								y = Main.maxTilesY - 5;
							}
							if( (double)y > Main.worldSurface )
							{
								WorldGen.Place3x2( x, y, corruptAltar, 0 );
								if( Main.tile[x, y].type == corruptAltar )
								{
									flag4 = true;
								}
								else
								{
									num8++;
									if( num8 >= 10000 )
									{
										flag4 = true;
									}
								}
							}
							else
							{
								flag4 = true;
							}
						}
					}
				}
				pos += vector2;
				vector2.X += (float)WorldGen.genRand.Next( -10, 11 ) * 0.01f;
				if( (double)vector2.X > 0.3 )
				{
					vector2.X = 0.3f;
				}
				if( (double)vector2.X < -0.3 )
				{
					vector2.X = -0.3f;
				}
				startX = (int)((double)pos.X - num3 * 1.1);
				endX = (int)((double)pos.X + num3 * 1.1);
				startY = (int)((double)pos.Y - num3 * 1.1);
				endY = (int)((double)pos.Y + num3 * 1.1);
				if( startX < 1 )
				{
					startX = 1;
				}
				if( endX > Main.maxTilesX - 1 )
				{
					endX = Main.maxTilesX - 1;
				}
				if( startY < 0 )
				{
					startY = 0;
				}
				if( endY > Main.maxTilesY )
				{
					endY = Main.maxTilesY;
				}
				for( int m = startX; m < endX; m++ )
				{
					for( int n = startY; n < endY; n++ )
					{
						if( (double)(Math.Abs( (float)m - pos.X ) + Math.Abs( (float)n - pos.Y )) < num3 * 1.1 * (1.0 + (double)WorldGen.genRand.Next( -10, 11 ) * 0.015) )
						{
							if( Main.tile[m, n].type != corruptStone && n > j + WorldGen.genRand.Next( 3, 20 ) )
							{
								Main.tile[m, n].active( true );
							}
							if( steps <= num2 )
							{
								Main.tile[m, n].active( true );
							}
							if( Main.tile[m, n].type != corruptOrb )
							{
								Main.tile[m, n].type = corruptStone;
							}
						}
					}
				}
				for( int num11 = startX; num11 < endX; num11++ )
				{
					for( int num12 = startY; num12 < endY; num12++ )
					{
						if( (double)(Math.Abs( (float)num11 - pos.X ) + Math.Abs( (float)num12 - pos.Y )) < num3 * 1.1 * (1.0 + (double)WorldGen.genRand.Next( -10, 11 ) * 0.015) )
						{
							if( Main.tile[num11, num12].type != corruptOrb )
							{
								Main.tile[num11, num12].type = corruptStone;
							}
							if( steps <= num2 )
							{
								Main.tile[num11, num12].active( true );
							}
							if( num12 > j + WorldGen.genRand.Next( 3, 20 ) )
							{
								Main.tile[num11, num12].wall = corruptWall;
							}
						}
					}
				}
			}
		}

		// Terraria.WorldGen
		// Token: 0x06000825 RID: 2085 RVA: 0x00399948 File Offset: 0x00397B48
		public static void ChasmRunnerSideways( int i, int j, int direction, int steps )
		{
			float num = (float)steps;
			Vector2 vector;
			vector.X = (float)i;
			vector.Y = (float)j;
			Vector2 vector2;
			vector2.X = (float)WorldGen.genRand.Next( 10, 21 ) * 0.1f * (float)direction;
			vector2.Y = (float)WorldGen.genRand.Next( -10, 10 ) * 0.01f;
			double num2 = (double)(WorldGen.genRand.Next( 5 ) + 7);
			while( num2 > 0.0 )
			{
				if( num > 0f )
				{
					num2 += (double)WorldGen.genRand.Next( 3 );
					num2 -= (double)WorldGen.genRand.Next( 3 );
					if( num2 < 7.0 )
					{
						num2 = 7.0;
					}
					if( num2 > 20.0 )
					{
						num2 = 20.0;
					}
					if( num == 1f && num2 < 10.0 )
					{
						num2 = 10.0;
					}
				}
				else
				{
					num2 -= (double)WorldGen.genRand.Next( 4 );
				}
				if( (double)vector.Y > Main.rockLayer && num > 0f )
				{
					num = 0f;
				}
				num -= 1f;
				int startX = (int)((double)vector.X - num2 * 0.5);
				int endX = (int)((double)vector.X + num2 * 0.5);
				int startY = (int)((double)vector.Y - num2 * 0.5);
				int endY = (int)((double)vector.Y + num2 * 0.5);
				if( startX < 0 )
				{
					startX = 0;
				}
				if( endX > Main.maxTilesX - 1 )
				{
					endX = Main.maxTilesX - 1;
				}
				if( startY < 0 )
				{
					startY = 0;
				}
				if( endY > Main.maxTilesY )
				{
					endY = Main.maxTilesY;
				}
				for( int k = startX; k < endX; k++ )
				{
					for( int l = startY; l < endY; l++ )
					{
						if( (double)(Math.Abs( (float)k - vector.X ) + Math.Abs( (float)l - vector.Y )) < num2 * 0.5 * (1.0 + (double)WorldGen.genRand.Next( -10, 11 ) * 0.015) && Main.tile[k, l].type != corruptOrb && Main.tile[k, l].type != corruptOre )
						{
							Main.tile[k, l].active( false );
						}
					}
				}
				vector += vector2;
				vector2.Y += (float)WorldGen.genRand.Next( -10, 10 ) * 0.1f;
				if( vector.Y < (float)(j - 20) )
				{
					vector2.Y += (float)WorldGen.genRand.Next( 20 ) * 0.01f;
				}
				if( vector.Y > (float)(j + 20) )
				{
					vector2.Y -= (float)WorldGen.genRand.Next( 20 ) * 0.01f;
				}
				if( (double)vector2.Y < -0.5 )
				{
					vector2.Y = -0.5f;
				}
				if( (double)vector2.Y > 0.5 )
				{
					vector2.Y = 0.5f;
				}
				vector2.X += (float)WorldGen.genRand.Next( -10, 11 ) * 0.01f;
				if( direction == -1 )
				{
					if( (double)vector2.X > -0.5 )
					{
						vector2.X = -0.5f;
					}
					if( vector2.X < -2f )
					{
						vector2.X = -2f;
					}
				}
				else if( direction == 1 )
				{
					if( (double)vector2.X < 0.5 )
					{
						vector2.X = 0.5f;
					}
					if( vector2.X > 2f )
					{
						vector2.X = 2f;
					}
				}
				startX = (int)((double)vector.X - num2 * 1.1);
				endX = (int)((double)vector.X + num2 * 1.1);
				startY = (int)((double)vector.Y - num2 * 1.1);
				endY = (int)((double)vector.Y + num2 * 1.1);
				if( startX < 1 )
				{
					startX = 1;
				}
				if( endX > Main.maxTilesX - 1 )
				{
					endX = Main.maxTilesX - 1;
				}
				if( startY < 0 )
				{
					startY = 0;
				}
				if( endY > Main.maxTilesY )
				{
					endY = Main.maxTilesY;
				}
				for( int x = startX; x < endX; x++ )
				{
					for( int y = startY; y < endY; y++ )
					{
						if( Math.Abs( x - vector.X ) + Math.Abs( y - vector.Y ) < num2 * 1.1 * (1.0 + WorldGen.genRand.Next( -10, 11 ) * 0.015) && Main.tile[x, y].wall != 3 )
						{
							if( Main.tile[x, y].type != corruptStone && y > j + WorldGen.genRand.Next( 3, 20 ) )
							{
								Main.tile[x, y].active( true );
							}
							Main.tile[x, y].active( true );
							if( Main.tile[x, y].type != corruptOrb && Main.tile[x, y].type != corruptOre )
							{
								Main.tile[x, y].type = corruptStone;
							}
							if( Main.tile[x, y].wall == WallID.DirtUnsafe )
							{
								Main.tile[x, y].wall = 0;
							}
						}
					}
				}
				for( int x = startX; x < endX; x++ )
				{
					for( int y = startY; y < endY; y++ )
					{
						if( Math.Abs( x - vector.X ) + Math.Abs( y - vector.Y ) < num2 * 1.1 * (1.0 + WorldGen.genRand.Next( -10, 11 ) * 0.015) && Main.tile[x, y].wall != corruptWall )
						{
							if( Main.tile[x, y].type != corruptOrb && Main.tile[x, y].type != corruptOre )
							{
								Main.tile[x, y].type = corruptStone;
							}
							Main.tile[x, y].active( true );
							WorldGen.PlaceWall( x, y, corruptWall, true );
						}
					}
				}
			}
			if( WorldGen.genRand.Next( 3 ) == 0 )
			{
				int x = (int)vector.X;
				int y = (int)vector.Y;
				while( !Main.tile[x, y].active() )
				{
					y++;
				}
				WorldGen.TileRunner( x, y, WorldGen.genRand.Next( 2, 6 ), WorldGen.genRand.Next( 3, 7 ), corruptOre, false, 0f, 0f, false, true );
			}
		}



		public static int CountNearBlocksTypes( int i, int j, int radius, int cap = 0, params int[] tiletypes )
		{
			if( tiletypes.Length == 0 )
			{
				return 0;
			}
			int xMin = i - radius;
			int xMax = i + radius;
			int yMin = j - radius;
			int yMax = j + radius;
			int num4 = Utils.Clamp<int>( xMin, 0, Main.maxTilesX - 1 );
			xMax = Utils.Clamp<int>( xMax, 0, Main.maxTilesX - 1 );
			yMin = Utils.Clamp<int>( yMin, 0, Main.maxTilesY - 1 );
			yMax = Utils.Clamp<int>( yMax, 0, Main.maxTilesY - 1 );
			int acc = 0;
			for( int x = num4; x < xMax; x++ )
			{
				for( int y = yMin; y < yMax; y++ )
				{
					if( Main.tile[x, y].active() )
					{
						int k = 0;
						while( k < tiletypes.Length )
						{
							if( tiletypes[k] == (int)Main.tile[x, y].type )
							{
								acc++;
								if( cap > 0 && acc >= cap )
								{
									return acc;
								}
								break;
							}
							else
							{
								k++;
							}
						}
					}
				}
			}
			return acc;
		}
	}
}