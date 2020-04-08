using Microsoft.Xna.Framework;
using System;
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
									bool isMudBlock = Main.tileStone[type] && tile.type != 1;
									if( !TileID.Sets.CanBeClearedDuringGeneration[(int)tile.type] )
									{
										isMudBlock = true;
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
													isMudBlock = true;
												}
											}
											else if( type == 59 && (double)y < Main.worldSurface + (double)WorldGen.genRand.Next( -50, 50 ) )
											{
												isMudBlock = true;
											}
										}
										else if( typeAtXY != 53 )
										{
											if( typeAtXY == 147 )
											{
												isMudBlock = true;
											}
										}
										else
										{
											if( type == 40 )
											{
												isMudBlock = true;
											}
											if( (double)y < Main.worldSurface && type != 59 )
											{
												isMudBlock = true;
											}
										}
									}
									else if( typeAtXY <= 196 )
									{
										if( typeAtXY - 189 <= 1 || typeAtXY == 196 )
										{
											isMudBlock = true;
										}
									}
									else if( typeAtXY - 367 > 1 )
									{
										if( typeAtXY - 396 <= 1 )
										{
											isMudBlock = !TileID.Sets.Ore[type];
										}
									}
									else if( type == 59 )
									{
										isMudBlock = true;
									}


									if( !isMudBlock )
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