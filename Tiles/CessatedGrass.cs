using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrariaReworked.Tiles
{
	public class CessatedGrass : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileSolid[Type] = true;
			Main.tileBlockLight[Type] = true;
			Main.tileBrick[Type] = true;

			// merge with dirt
			Main.tileMerge[Type][TileID.Dirt] = true;
			Main.tileMerge[TileID.Dirt][Type] = true;

			// merge with other grass types (optional)
			Main.tileMerge[Type][2] = true;
			Main.tileMerge[2][Type] = true;

			Main.tileMerge[Type][23] = true;
			Main.tileMerge[23][Type] = true;

			Main.tileMerge[Type][109] = true;
			Main.tileMerge[109][Type] = true;

			Main.tileMerge[Type][199] = true;
			Main.tileMerge[199][Type] = true;

			// (?)
			TileID.Sets.Grass[Type] = true;

			// clentaminator (and probably other things) will treat it like grass.
			TileID.Sets.Conversion.Grass[Type] = true;

			// Allow proper use of the grass sprite sheet.
			TileID.Sets.NeedsGrassFraming[Type] = true;
			TileID.Sets.NeedsGrassFramingDirt[Type] = TileID.Dirt; // ModContent.TileType<name>();

			drop = 0; // drops dirt.

			AddMapEntry( new Color( 50, 132, 60 ) );
			this.soundType = 0;
		}
		


		public override bool CreateDust( int x, int y, ref int type )
		{
			Dust.NewDust( new Vector2( x, y ) * 16f, 16, 16, 60, 0f, 0f, 1, new Color( 255, 255, 255 ), 1f );
			Dust.NewDust( new Vector2( x, y ) * 16f, 16, 16, 1, 0f, 0f, 1, new Color( 100, 100, 100 ), 1f );
			return false;
		}

		// Token: 0x06000CFA RID: 3322 RVA: 0x0005C11C File Offset: 0x0005A31C
		public override void KillTile( int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem )
		{
			if( fail && !effectOnly )
			{
				Main.tile[i, j].type = TileID.Dirt;
			}
		}
		
		// Token: 0x06000CFB RID: 3323 RVA: 0x0005C13E File Offset: 0x0005A33E
		public override int SaplingGrowthType( ref int style )
		{
			style = 0;
			return TileID.Saplings; //ModContent.TileType<CustomSapling>();
		}



		public override void RandomUpdate( int i, int j )
		{
			int num7 = i;
			int num8 = j;

			int startX = num7 - 1;

			int endX = num7 + 2;
			int startY = num8 - 1;
			int endY = num8 + 2;

			int type = (int)Main.tile[num7, num8].type;

			bool flag2 = false;
			for( int x = startX; x < endX; x++ )
			{
				for( int y = startY; y < endY; y++ )
				{
					if( (num7 != x || num8 != y) && Main.tile[x, y].active() )
					{
						// spread from thorny bushes.
						/*if( type == 32 ) // corr thbu
						{
							type = 23;
						}
						if( type == 352 ) // crim thbu
						{
							type = 199;
						}*/

						//if( Main.tile[num22, num23].type == 0 || (type == 23 && Main.tile[num22, num23].type == 2) || (type == 199 && Main.tile[num22, num23].type == 2) || (type == 23 && Main.tile[num22, num23].type == 109) )
						//{
							WorldGen.SpreadGrass( x, y, 0, type, false, Main.tile[num7, num8].color() );
							//if( type == 23 )
							//{
								WorldGen.SpreadGrass( x, y, 2, type, false, Main.tile[num7, num8].color() );
							/*}
							if( type == 23 )
							{
								WorldGen.SpreadGrass( x, y, 109, type, false, Main.tile[num7, num8].color() );
							}
							if( type == 199 )
							{
								WorldGen.SpreadGrass( x, y, 2, type, false, Main.tile[num7, num8].color() );
							}
							if( type == 199 )
							{
								WorldGen.SpreadGrass( x, y, 109, type, false, Main.tile[num7, num8].color() );
							}*/
							if( (int)Main.tile[x, y].type == type )
							{
								WorldGen.SquareTileFrame( x, y, true );
								flag2 = true;
							}
						//}
						/*if( Main.tile[x, y].type == 0 || (type == 109 && Main.tile[x, y].type == 2) )// || (type == 109 && Main.tile[num22, num23].type == 23) || (type == 109 && Main.tile[num22, num23].type == 199) )
						{
							WorldGen.SpreadGrass( x, y, 0, type, false, Main.tile[num7, num8].color() );
							if( type == 109 )
							{
								WorldGen.SpreadGrass( x, y, 2, type, false, Main.tile[num7, num8].color() );
							}
							if( type == 109 )
							{
								WorldGen.SpreadGrass( x, y, 23, type, false, Main.tile[num7, num8].color() );
							}
							if( type == 109 )
							{
								WorldGen.SpreadGrass( x, y, 199, type, false, Main.tile[num7, num8].color() );
							}
							if( (int)Main.tile[x, y].type == type )
							{
								WorldGen.SquareTileFrame( x, y, true );
								flag2 = true;
							}
						}*/
					}
				}
			}
			if( Main.netMode == 2 && flag2 )
			{
				NetMessage.SendTileSquare( -1, num7, num8, 3, TileChangeType.None );
			}

			base.RandomUpdate( i, j );
		}

		/*public override bool TileFrame( int i, int j, ref bool resetFrame, ref bool noBreak )
		{
			//{
			int num = Main.tile[i,j].type;
				bool flag8 = true;
				WorldGen.TileMergeAttemptWeird( num, -1, Main.tileSolid, ref num56, ref num61, ref num58, ref num59, ref num55, ref num57, ref num60, ref num62 );
				int num66 = TileID.Sets.NeedsGrassFramingDirt[num];
				if( num == 60 || num == 70 )
				{
					num66 = 59;
				}
				else if( Main.tileMoss[num] )
				{
					num66 = 1;
				}
				else if( num == 2 )
				{
					WorldGen.TileMergeAttempt( num66, 23, ref num56, ref num61, ref num58, ref num59, ref num55, ref num57, ref num60, ref num62 );
				}
				else if( num == 23 )
				{
					WorldGen.TileMergeAttempt( num66, 2, ref num56, ref num61, ref num58, ref num59, ref num55, ref num57, ref num60, ref num62 );
				}
				if( num56 != num && num56 != num66 && (num61 == num || num61 == num66) )
				{
					if( num58 == num66 && num59 == num )
					{
						if( num63 == 0 )
						{
							rectangle.X = 0;
							rectangle.Y = 198;
						}
						else if( num63 == 1 )
						{
							rectangle.X = 18;
							rectangle.Y = 198;
						}
						else
						{
							rectangle.X = 36;
							rectangle.Y = 198;
						}
					}
					else if( num58 == num && num59 == num66 )
					{
						if( num63 == 0 )
						{
							rectangle.X = 54;
							rectangle.Y = 198;
						}
						else if( num63 == 1 )
						{
							rectangle.X = 72;
							rectangle.Y = 198;
						}
						else
						{
							rectangle.X = 90;
							rectangle.Y = 198;
						}
					}
				}
				else if( num61 != num && num61 != num66 && (num56 == num || num56 == num66) )
				{
					if( num58 == num66 && num59 == num )
					{
						if( num63 == 0 )
						{
							rectangle.X = 0;
							rectangle.Y = 216;
						}
						else if( num63 == 1 )
						{
							rectangle.X = 18;
							rectangle.Y = 216;
						}
						else
						{
							rectangle.X = 36;
							rectangle.Y = 216;
						}
					}
					else if( num58 == num && num59 == num66 )
					{
						if( num63 == 0 )
						{
							rectangle.X = 54;
							rectangle.Y = 216;
						}
						else if( num63 == 1 )
						{
							rectangle.X = 72;
							rectangle.Y = 216;
						}
						else
						{
							rectangle.X = 90;
							rectangle.Y = 216;
						}
					}
				}
				else if( num58 != num && num58 != num66 && (num59 == num || num59 == num66) )
				{
					if( num56 == num66 && num61 == num )
					{
						if( num63 == 0 )
						{
							rectangle.X = 72;
							rectangle.Y = 144;
						}
						else if( num63 == 1 )
						{
							rectangle.X = 72;
							rectangle.Y = 162;
						}
						else
						{
							rectangle.X = 72;
							rectangle.Y = 180;
						}
					}
					else if( num61 == num && num56 == num66 )
					{
						if( num63 == 0 )
						{
							rectangle.X = 72;
							rectangle.Y = 90;
						}
						else if( num63 == 1 )
						{
							rectangle.X = 72;
							rectangle.Y = 108;
						}
						else
						{
							rectangle.X = 72;
							rectangle.Y = 126;
						}
					}
				}
				else if( num59 != num && num59 != num66 && (num58 == num || num58 == num66) )
				{
					if( num56 == num66 && num61 == num )
					{
						if( num63 == 0 )
						{
							rectangle.X = 90;
							rectangle.Y = 144;
						}
						else if( num63 == 1 )
						{
							rectangle.X = 90;
							rectangle.Y = 162;
						}
						else
						{
							rectangle.X = 90;
							rectangle.Y = 180;
						}
					}
					else if( num61 == num && num59 == num56 )
					{
						if( num63 == 0 )
						{
							rectangle.X = 90;
							rectangle.Y = 90;
						}
						else if( num63 == 1 )
						{
							rectangle.X = 90;
							rectangle.Y = 108;
						}
						else
						{
							rectangle.X = 90;
							rectangle.Y = 126;
						}
					}
				}
				else if( num56 == num && num61 == num && num58 == num && num59 == num )
				{
					if( num55 != num && num57 != num && num60 != num && num62 != num )
					{
						if( num62 == num66 )
						{
							if( num63 == 0 )
							{
								rectangle.X = 108;
								rectangle.Y = 324;
							}
							else if( num63 == 1 )
							{
								rectangle.X = 126;
								rectangle.Y = 324;
							}
							else
							{
								rectangle.X = 144;
								rectangle.Y = 324;
							}
						}
						else if( num57 == num66 )
						{
							if( num63 == 0 )
							{
								rectangle.X = 108;
								rectangle.Y = 342;
							}
							else if( num63 == 1 )
							{
								rectangle.X = 126;
								rectangle.Y = 342;
							}
							else
							{
								rectangle.X = 144;
								rectangle.Y = 342;
							}
						}
						else if( num60 == num66 )
						{
							if( num63 == 0 )
							{
								rectangle.X = 108;
								rectangle.Y = 360;
							}
							else if( num63 == 1 )
							{
								rectangle.X = 126;
								rectangle.Y = 360;
							}
							else
							{
								rectangle.X = 144;
								rectangle.Y = 360;
							}
						}
						else if( num55 == num66 )
						{
							if( num63 == 0 )
							{
								rectangle.X = 108;
								rectangle.Y = 378;
							}
							else if( num63 == 1 )
							{
								rectangle.X = 126;
								rectangle.Y = 378;
							}
							else
							{
								rectangle.X = 144;
								rectangle.Y = 378;
							}
						}
						else if( num63 == 0 )
						{
							rectangle.X = 144;
							rectangle.Y = 234;
						}
						else if( num63 == 1 )
						{
							rectangle.X = 198;
							rectangle.Y = 234;
						}
						else
						{
							rectangle.X = 252;
							rectangle.Y = 234;
						}
					}
					else if( num55 != num && num62 != num )
					{
						if( num63 == 0 )
						{
							rectangle.X = 36;
							rectangle.Y = 306;
						}
						else if( num63 == 1 )
						{
							rectangle.X = 54;
							rectangle.Y = 306;
						}
						else
						{
							rectangle.X = 72;
							rectangle.Y = 306;
						}
					}
					else if( num57 != num && num60 != num )
					{
						if( num63 == 0 )
						{
							rectangle.X = 90;
							rectangle.Y = 306;
						}
						else if( num63 == 1 )
						{
							rectangle.X = 108;
							rectangle.Y = 306;
						}
						else
						{
							rectangle.X = 126;
							rectangle.Y = 306;
						}
					}
					else if( num55 != num && num57 == num && num60 == num && num62 == num )
					{
						if( num63 == 0 )
						{
							rectangle.X = 54;
							rectangle.Y = 108;
						}
						else if( num63 == 1 )
						{
							rectangle.X = 54;
							rectangle.Y = 144;
						}
						else
						{
							rectangle.X = 54;
							rectangle.Y = 180;
						}
					}
					else if( num55 == num && num57 != num && num60 == num && num62 == num )
					{
						if( num63 == 0 )
						{
							rectangle.X = 36;
							rectangle.Y = 108;
						}
						else if( num63 == 1 )
						{
							rectangle.X = 36;
							rectangle.Y = 144;
						}
						else
						{
							rectangle.X = 36;
							rectangle.Y = 180;
						}
					}
					else if( num55 == num && num57 == num && num60 != num && num62 == num )
					{
						if( num63 == 0 )
						{
							rectangle.X = 54;
							rectangle.Y = 90;
						}
						else if( num63 == 1 )
						{
							rectangle.X = 54;
							rectangle.Y = 126;
						}
						else
						{
							rectangle.X = 54;
							rectangle.Y = 162;
						}
					}
					else if( num55 == num && num57 == num && num60 == num && num62 != num )
					{
						if( num63 == 0 )
						{
							rectangle.X = 36;
							rectangle.Y = 90;
						}
						else if( num63 == 1 )
						{
							rectangle.X = 36;
							rectangle.Y = 126;
						}
						else
						{
							rectangle.X = 36;
							rectangle.Y = 162;
						}
					}
				}
				else if( num56 == num && num61 == num66 && num58 == num && num59 == num && num55 == -1 && num57 == -1 )
				{
					if( num63 == 0 )
					{
						rectangle.X = 108;
						rectangle.Y = 18;
					}
					else if( num63 == 1 )
					{
						rectangle.X = 126;
						rectangle.Y = 18;
					}
					else
					{
						rectangle.X = 144;
						rectangle.Y = 18;
					}
				}
				else if( num56 == num66 && num61 == num && num58 == num && num59 == num && num60 == -1 && num62 == -1 )
				{
					if( num63 == 0 )
					{
						rectangle.X = 108;
						rectangle.Y = 36;
					}
					else if( num63 == 1 )
					{
						rectangle.X = 126;
						rectangle.Y = 36;
					}
					else
					{
						rectangle.X = 144;
						rectangle.Y = 36;
					}
				}
				else if( num56 == num && num61 == num && num58 == num66 && num59 == num && num57 == -1 && num62 == -1 )
				{
					if( num63 == 0 )
					{
						rectangle.X = 198;
						rectangle.Y = 0;
					}
					else if( num63 == 1 )
					{
						rectangle.X = 198;
						rectangle.Y = 18;
					}
					else
					{
						rectangle.X = 198;
						rectangle.Y = 36;
					}
				}
				else if( num56 == num && num61 == num && num58 == num && num59 == num66 && num55 == -1 && num60 == -1 )
				{
					if( num63 == 0 )
					{
						rectangle.X = 180;
						rectangle.Y = 0;
					}
					else if( num63 == 1 )
					{
						rectangle.X = 180;
						rectangle.Y = 18;
					}
					else
					{
						rectangle.X = 180;
						rectangle.Y = 36;
					}
				}
				else if( num56 == num && num61 == num66 && num58 == num && num59 == num )
				{
					if( num57 != -1 )
					{
						if( num63 == 0 )
						{
							rectangle.X = 54;
							rectangle.Y = 108;
						}
						else if( num63 == 1 )
						{
							rectangle.X = 54;
							rectangle.Y = 144;
						}
						else
						{
							rectangle.X = 54;
							rectangle.Y = 180;
						}
					}
					else if( num55 != -1 )
					{
						if( num63 == 0 )
						{
							rectangle.X = 36;
							rectangle.Y = 108;
						}
						else if( num63 == 1 )
						{
							rectangle.X = 36;
							rectangle.Y = 144;
						}
						else
						{
							rectangle.X = 36;
							rectangle.Y = 180;
						}
					}
				}
				else if( num56 == num66 && num61 == num && num58 == num && num59 == num )
				{
					if( num62 != -1 )
					{
						if( num63 == 0 )
						{
							rectangle.X = 54;
							rectangle.Y = 90;
						}
						else if( num63 == 1 )
						{
							rectangle.X = 54;
							rectangle.Y = 126;
						}
						else
						{
							rectangle.X = 54;
							rectangle.Y = 162;
						}
					}
					else if( num60 != -1 )
					{
						if( num63 == 0 )
						{
							rectangle.X = 36;
							rectangle.Y = 90;
						}
						else if( num63 == 1 )
						{
							rectangle.X = 36;
							rectangle.Y = 126;
						}
						else
						{
							rectangle.X = 36;
							rectangle.Y = 162;
						}
					}
				}
				else if( num56 == num && num61 == num && num58 == num && num59 == num66 )
				{
					if( num55 != -1 )
					{
						if( num63 == 0 )
						{
							rectangle.X = 54;
							rectangle.Y = 90;
						}
						else if( num63 == 1 )
						{
							rectangle.X = 54;
							rectangle.Y = 126;
						}
						else
						{
							rectangle.X = 54;
							rectangle.Y = 162;
						}
					}
					else if( num60 != -1 )
					{
						if( num63 == 0 )
						{
							rectangle.X = 54;
							rectangle.Y = 108;
						}
						else if( num63 == 1 )
						{
							rectangle.X = 54;
							rectangle.Y = 144;
						}
						else
						{
							rectangle.X = 54;
							rectangle.Y = 180;
						}
					}
				}
				else if( num56 == num && num61 == num && num58 == num66 && num59 == num )
				{
					if( num57 != -1 )
					{
						if( num63 == 0 )
						{
							rectangle.X = 36;
							rectangle.Y = 90;
						}
						else if( num63 == 1 )
						{
							rectangle.X = 36;
							rectangle.Y = 126;
						}
						else
						{
							rectangle.X = 36;
							rectangle.Y = 162;
						}
					}
					else if( num62 != -1 )
					{
						if( num63 == 0 )
						{
							rectangle.X = 36;
							rectangle.Y = 108;
						}
						else if( num63 == 1 )
						{
							rectangle.X = 36;
							rectangle.Y = 144;
						}
						else
						{
							rectangle.X = 36;
							rectangle.Y = 180;
						}
					}
				}
				else if( (num56 == num66 && num61 == num && num58 == num && num59 == num) || (num56 == num && num61 == num66 && num58 == num && num59 == num) || (num56 == num && num61 == num && num58 == num66 && num59 == num) || (num56 == num && num61 == num && num58 == num && num59 == num66) )
				{
					if( num63 == 0 )
					{
						rectangle.X = 18;
						rectangle.Y = 18;
					}
					else if( num63 == 1 )
					{
						rectangle.X = 36;
						rectangle.Y = 18;
					}
					else
					{
						rectangle.X = 54;
						rectangle.Y = 18;
					}
				}
				if( (num56 == num || num56 == num66) && (num61 == num || num61 == num66) && (num58 == num || num58 == num66) && (num59 == num || num59 == num66) )
				{
					if( num55 != num && num55 != num66 && (num57 == num || num57 == num66) && (num60 == num || num60 == num66) && (num62 == num || num62 == num66) )
					{
						if( num63 == 0 )
						{
							rectangle.X = 54;
							rectangle.Y = 108;
						}
						else if( num63 == 1 )
						{
							rectangle.X = 54;
							rectangle.Y = 144;
						}
						else
						{
							rectangle.X = 54;
							rectangle.Y = 180;
						}
					}
					else if( num57 != num && num57 != num66 && (num55 == num || num55 == num66) && (num60 == num || num60 == num66) && (num62 == num || num62 == num66) )
					{
						if( num63 == 0 )
						{
							rectangle.X = 36;
							rectangle.Y = 108;
						}
						else if( num63 == 1 )
						{
							rectangle.X = 36;
							rectangle.Y = 144;
						}
						else
						{
							rectangle.X = 36;
							rectangle.Y = 180;
						}
					}
					else if( num60 != num && num60 != num66 && (num55 == num || num55 == num66) && (num57 == num || num57 == num66) && (num62 == num || num62 == num66) )
					{
						if( num63 == 0 )
						{
							rectangle.X = 54;
							rectangle.Y = 90;
						}
						else if( num63 == 1 )
						{
							rectangle.X = 54;
							rectangle.Y = 126;
						}
						else
						{
							rectangle.X = 54;
							rectangle.Y = 162;
						}
					}
					else if( num62 != num && num62 != num66 && (num55 == num || num55 == num66) && (num60 == num || num60 == num66) && (num57 == num || num57 == num66) )
					{
						if( num63 == 0 )
						{
							rectangle.X = 36;
							rectangle.Y = 90;
						}
						else if( num63 == 1 )
						{
							rectangle.X = 36;
							rectangle.Y = 126;
						}
						else
						{
							rectangle.X = 36;
							rectangle.Y = 162;
						}
					}
				}
				if( num56 != num66 && num56 != num && num61 == num && num58 != num66 && num58 != num && num59 == num && num62 != num66 && num62 != num )
				{
					if( num63 == 0 )
					{
						rectangle.X = 90;
						rectangle.Y = 270;
					}
					else if( num63 == 1 )
					{
						rectangle.X = 108;
						rectangle.Y = 270;
					}
					else
					{
						rectangle.X = 126;
						rectangle.Y = 270;
					}
				}
				else if( num56 != num66 && num56 != num && num61 == num && num58 == num && num59 != num66 && num59 != num && num60 != num66 && num60 != num )
				{
					if( num63 == 0 )
					{
						rectangle.X = 144;
						rectangle.Y = 270;
					}
					else if( num63 == 1 )
					{
						rectangle.X = 162;
						rectangle.Y = 270;
					}
					else
					{
						rectangle.X = 180;
						rectangle.Y = 270;
					}
				}
				else if( num61 != num66 && num61 != num && num56 == num && num58 != num66 && num58 != num && num59 == num && num57 != num66 && num57 != num )
				{
					if( num63 == 0 )
					{
						rectangle.X = 90;
						rectangle.Y = 288;
					}
					else if( num63 == 1 )
					{
						rectangle.X = 108;
						rectangle.Y = 288;
					}
					else
					{
						rectangle.X = 126;
						rectangle.Y = 288;
					}
				}
				else if( num61 != num66 && num61 != num && num56 == num && num58 == num && num59 != num66 && num59 != num && num55 != num66 && num55 != num )
				{
					if( num63 == 0 )
					{
						rectangle.X = 144;
						rectangle.Y = 288;
					}
					else if( num63 == 1 )
					{
						rectangle.X = 162;
						rectangle.Y = 288;
					}
					else
					{
						rectangle.X = 180;
						rectangle.Y = 288;
					}
				}
				else if( num56 != num && num56 != num66 && num61 == num && num58 == num && num59 == num && num60 != num && num60 != num66 && num62 != num && num62 != num66 )
				{
					if( num63 == 0 )
					{
						rectangle.X = 144;
						rectangle.Y = 216;
					}
					else if( num63 == 1 )
					{
						rectangle.X = 198;
						rectangle.Y = 216;
					}
					else
					{
						rectangle.X = 252;
						rectangle.Y = 216;
					}
				}
				else if( num61 != num && num61 != num66 && num56 == num && num58 == num && num59 == num && num55 != num && num55 != num66 && num57 != num && num57 != num66 )
				{
					if( num63 == 0 )
					{
						rectangle.X = 144;
						rectangle.Y = 252;
					}
					else if( num63 == 1 )
					{
						rectangle.X = 198;
						rectangle.Y = 252;
					}
					else
					{
						rectangle.X = 252;
						rectangle.Y = 252;
					}
				}
				else if( num58 != num && num58 != num66 && num61 == num && num56 == num && num59 == num && num57 != num && num57 != num66 && num62 != num && num62 != num66 )
				{
					if( num63 == 0 )
					{
						rectangle.X = 126;
						rectangle.Y = 234;
					}
					else if( num63 == 1 )
					{
						rectangle.X = 180;
						rectangle.Y = 234;
					}
					else
					{
						rectangle.X = 234;
						rectangle.Y = 234;
					}
				}
				else if( num59 != num && num59 != num66 && num61 == num && num56 == num && num58 == num && num55 != num && num55 != num66 && num60 != num && num60 != num66 )
				{
					if( num63 == 0 )
					{
						rectangle.X = 162;
						rectangle.Y = 234;
					}
					else if( num63 == 1 )
					{
						rectangle.X = 216;
						rectangle.Y = 234;
					}
					else
					{
						rectangle.X = 270;
						rectangle.Y = 234;
					}
				}
				else if( num56 != num66 && num56 != num && (num61 == num66 || num61 == num) && num58 == num66 && num59 == num66 )
				{
					if( num63 == 0 )
					{
						rectangle.X = 36;
						rectangle.Y = 270;
					}
					else if( num63 == 1 )
					{
						rectangle.X = 54;
						rectangle.Y = 270;
					}
					else
					{
						rectangle.X = 72;
						rectangle.Y = 270;
					}
				}
				else if( num61 != num66 && num61 != num && (num56 == num66 || num56 == num) && num58 == num66 && num59 == num66 )
				{
					if( num63 == 0 )
					{
						rectangle.X = 36;
						rectangle.Y = 288;
					}
					else if( num63 == 1 )
					{
						rectangle.X = 54;
						rectangle.Y = 288;
					}
					else
					{
						rectangle.X = 72;
						rectangle.Y = 288;
					}
				}
				else if( num58 != num66 && num58 != num && (num59 == num66 || num59 == num) && num56 == num66 && num61 == num66 )
				{
					if( num63 == 0 )
					{
						rectangle.X = 0;
						rectangle.Y = 270;
					}
					else if( num63 == 1 )
					{
						rectangle.X = 0;
						rectangle.Y = 288;
					}
					else
					{
						rectangle.X = 0;
						rectangle.Y = 306;
					}
				}
				else if( num59 != num66 && num59 != num && (num58 == num66 || num58 == num) && num56 == num66 && num61 == num66 )
				{
					if( num63 == 0 )
					{
						rectangle.X = 18;
						rectangle.Y = 270;
					}
					else if( num63 == 1 )
					{
						rectangle.X = 18;
						rectangle.Y = 288;
					}
					else
					{
						rectangle.X = 18;
						rectangle.Y = 306;
					}
				}
				else if( num56 == num && num61 == num66 && num58 == num66 && num59 == num66 )
				{
					if( num63 == 0 )
					{
						rectangle.X = 198;
						rectangle.Y = 288;
					}
					else if( num63 == 1 )
					{
						rectangle.X = 216;
						rectangle.Y = 288;
					}
					else
					{
						rectangle.X = 234;
						rectangle.Y = 288;
					}
				}
				else if( num56 == num66 && num61 == num && num58 == num66 && num59 == num66 )
				{
					if( num63 == 0 )
					{
						rectangle.X = 198;
						rectangle.Y = 270;
					}
					else if( num63 == 1 )
					{
						rectangle.X = 216;
						rectangle.Y = 270;
					}
					else
					{
						rectangle.X = 234;
						rectangle.Y = 270;
					}
				}
				else if( num56 == num66 && num61 == num66 && num58 == num && num59 == num66 )
				{
					if( num63 == 0 )
					{
						rectangle.X = 198;
						rectangle.Y = 306;
					}
					else if( num63 == 1 )
					{
						rectangle.X = 216;
						rectangle.Y = 306;
					}
					else
					{
						rectangle.X = 234;
						rectangle.Y = 306;
					}
				}
				else if( num56 == num66 && num61 == num66 && num58 == num66 && num59 == num )
				{
					if( num63 == 0 )
					{
						rectangle.X = 144;
						rectangle.Y = 306;
					}
					else if( num63 == 1 )
					{
						rectangle.X = 162;
						rectangle.Y = 306;
					}
					else
					{
						rectangle.X = 180;
						rectangle.Y = 306;
					}
				}
				if( num56 != num && num56 != num66 && num61 == num && num58 == num && num59 == num )
				{
					if( (num60 == num66 || num60 == num) && num62 != num66 && num62 != num )
					{
						if( num63 == 0 )
						{
							rectangle.X = 0;
							rectangle.Y = 324;
						}
						else if( num63 == 1 )
						{
							rectangle.X = 18;
							rectangle.Y = 324;
						}
						else
						{
							rectangle.X = 36;
							rectangle.Y = 324;
						}
					}
					else if( (num62 == num66 || num62 == num) && num60 != num66 && num60 != num )
					{
						if( num63 == 0 )
						{
							rectangle.X = 54;
							rectangle.Y = 324;
						}
						else if( num63 == 1 )
						{
							rectangle.X = 72;
							rectangle.Y = 324;
						}
						else
						{
							rectangle.X = 90;
							rectangle.Y = 324;
						}
					}
				}
				else if( num61 != num && num61 != num66 && num56 == num && num58 == num && num59 == num )
				{
					if( (num55 == num66 || num55 == num) && num57 != num66 && num57 != num )
					{
						if( num63 == 0 )
						{
							rectangle.X = 0;
							rectangle.Y = 342;
						}
						else if( num63 == 1 )
						{
							rectangle.X = 18;
							rectangle.Y = 342;
						}
						else
						{
							rectangle.X = 36;
							rectangle.Y = 342;
						}
					}
					else if( (num57 == num66 || num57 == num) && num55 != num66 && num55 != num )
					{
						if( num63 == 0 )
						{
							rectangle.X = 54;
							rectangle.Y = 342;
						}
						else if( num63 == 1 )
						{
							rectangle.X = 72;
							rectangle.Y = 342;
						}
						else
						{
							rectangle.X = 90;
							rectangle.Y = 342;
						}
					}
				}
				else if( num58 != num && num58 != num66 && num56 == num && num61 == num && num59 == num )
				{
					if( (num57 == num66 || num57 == num) && num62 != num66 && num62 != num )
					{
						if( num63 == 0 )
						{
							rectangle.X = 54;
							rectangle.Y = 360;
						}
						else if( num63 == 1 )
						{
							rectangle.X = 72;
							rectangle.Y = 360;
						}
						else
						{
							rectangle.X = 90;
							rectangle.Y = 360;
						}
					}
					else if( (num62 == num66 || num62 == num) && num57 != num66 && num57 != num )
					{
						if( num63 == 0 )
						{
							rectangle.X = 0;
							rectangle.Y = 360;
						}
						else if( num63 == 1 )
						{
							rectangle.X = 18;
							rectangle.Y = 360;
						}
						else
						{
							rectangle.X = 36;
							rectangle.Y = 360;
						}
					}
				}
				else if( num59 != num && num59 != num66 && num56 == num && num61 == num && num58 == num )
				{
					if( (num55 == num66 || num55 == num) && num60 != num66 && num60 != num )
					{
						if( num63 == 0 )
						{
							rectangle.X = 0;
							rectangle.Y = 378;
						}
						else if( num63 == 1 )
						{
							rectangle.X = 18;
							rectangle.Y = 378;
						}
						else
						{
							rectangle.X = 36;
							rectangle.Y = 378;
						}
					}
					else if( (num60 == num66 || num60 == num) && num55 != num66 && num55 != num )
					{
						if( num63 == 0 )
						{
							rectangle.X = 54;
							rectangle.Y = 378;
						}
						else if( num63 == 1 )
						{
							rectangle.X = 72;
							rectangle.Y = 378;
						}
						else
						{
							rectangle.X = 90;
							rectangle.Y = 378;
						}
					}
				}
				if( (num56 == num || num56 == num66) && (num61 == num || num61 == num66) && (num58 == num || num58 == num66) && (num59 == num || num59 == num66) && num55 != -1 && num57 != -1 && num60 != -1 && num62 != -1 )
				{
					if( (i + j) % 2 == 1 )
					{
						if( num63 == 0 )
						{
							rectangle.X = 108;
							rectangle.Y = 198;
						}
						else if( num63 == 1 )
						{
							rectangle.X = 126;
							rectangle.Y = 198;
						}
						else
						{
							rectangle.X = 144;
							rectangle.Y = 198;
						}
					}
					else if( num63 == 0 )
					{
						rectangle.X = 18;
						rectangle.Y = 18;
					}
					else if( num63 == 1 )
					{
						rectangle.X = 36;
						rectangle.Y = 18;
					}
					else
					{
						rectangle.X = 54;
						rectangle.Y = 18;
					}
				}
				WorldGen.TileMergeAttempt( -2, num66, ref num56, ref num61, ref num58, ref num59, ref num55, ref num57, ref num60, ref num62 );
			//}
			return false;
		}*/
	}
}