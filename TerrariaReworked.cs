using Katniss;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Reflection;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace TerrariaReworked
{
	class TerrariaReworked : Mod
	{
		//private static Recipe emptyRecipe;
		public const int BerserkerDustDamage = 75;

		public static readonly int[] disabledItems = new int[]
		{
			ItemID.WoodHelmet,
			ItemID.WoodBreastplate,
			ItemID.WoodGreaves,
			ItemID.BorealWoodHelmet,
			ItemID.BorealWoodBreastplate,
			ItemID.BorealWoodGreaves ,
			ItemID.PalmWoodHelmet,
			ItemID.PalmWoodBreastplate,
			ItemID.PalmWoodGreaves,
			ItemID.EbonwoodHelmet,
			ItemID.EbonwoodBreastplate,
			ItemID.EbonwoodGreaves,
			ItemID.ShadewoodHelmet,
			ItemID.ShadewoodBreastplate,
			ItemID.ShadewoodGreaves,
			ItemID.PearlwoodHelmet,
			ItemID.PearlwoodBreastplate,
			ItemID.PearlwoodGreaves,

			// HM pickaxes/axes (only drills/chainsaws allowed)
			ItemID.CobaltPickaxe,
			ItemID.CobaltWaraxe,
			ItemID.PalladiumPickaxe,
			ItemID.PalladiumWaraxe,
			ItemID.MythrilPickaxe,
			ItemID.MythrilWaraxe,
			ItemID.OrichalcumPickaxe,
			ItemID.OrichalcumWaraxe,
			ItemID.AdamantitePickaxe,
			ItemID.AdamantiteWaraxe,
			ItemID.TitaniumPickaxe,
			ItemID.TitaniumWaraxe,
			ItemID.ChlorophytePickaxe,
			ItemID.ChlorophyteGreataxe,
			ItemID.ChlorophyteWarhammer,
			ItemID.PickaxeAxe,
			ItemID.Picksaw,

			ItemID.Katana,
			ItemID.Gatligator,
			ItemID.DD2ElderCrystal,
			ItemID.DD2ElderCrystalStand,
			ItemID.MusicBoxDD2
		};

		public static readonly int[] disabledNPCs = new int[]
		{
			NPCID.DukeFishron
		};

		public static TerrariaReworked instance { get; private set; }

		public static int hellcastleX;
		public static int hellcastleY;

		public TerrariaReworked()
		{
			// By default, all Autoload properties are True. You only need to change this if you know what you are doing.
			Properties = new ModProperties()
			{
				Autoload = true,
				AutoloadGores = true,
				AutoloadSounds = true,
				AutoloadBackgrounds = true
			};
		}

		public override void Load()
		{
			instance = (TerrariaReworked)ModLoader.GetMod( "TerrariaReworked" );
		}

		public override void Unload()
		{

		}

		public override void PostSetupContent()
		{
			// Misc stuff.
			Main.itemTexture[ItemID.AvengerEmblem] = GetTexture( "Items/Vanilla/AvengerEmblem" );
			Main.itemTexture[ItemID.Coal] = GetTexture( "Items/Vanilla/Coal" );
			Main.itemTexture[ItemID.Sunfury] = GetTexture( "Items/Vanilla/Sunfury" );
			Main.itemTexture[ItemID.Minishark] = GetTexture( "Items/Vanilla/Minishark" );
			Main.itemTexture[ItemID.Megashark] = GetTexture( "Items/Vanilla/Megashark" );
			Main.itemTexture[ItemID.BladeofGrass] = GetTexture( "Items/Vanilla/BladeofGrass" );
			Main.itemTexture[ItemID.ChlorophyteOre] = GetTexture( "Items/Vanilla/ChlorophyteOre" );
			Main.itemTexture[ItemID.ChlorophyteBar] = GetTexture( "Items/Vanilla/ChlorophyteBar" );
			Main.itemTexture[ItemID.FieryGreatsword] = GetTexture( "Items/Vanilla/FieryGreatsword" );
			Main.itemTexture[ItemID.MoltenPickaxe] = GetTexture( "Items/Vanilla/MoltenPickaxe" );
			Main.itemTexture[ItemID.MoltenHamaxe] = GetTexture( "Items/Vanilla/MoltenHamaxe" );
			Main.itemTexture[ItemID.SpellTome] = GetTexture( "Items/Vanilla/SpellTome" );
			Main.itemTexture[ItemID.CursedFlames] = GetTexture( "Items/Vanilla/CursedFlames" );
			Main.itemTexture[ItemID.GoldenShower] = GetTexture( "Items/Vanilla/GoldenShower" );
			Main.itemTexture[ItemID.CrystalStorm] = GetTexture( "Items/Vanilla/CrystalStorm" );

			// Copper-Tier Ore
			Main.itemTexture[ItemID.CopperBroadsword] = GetTexture( "Items/Vanilla/CopperBroadsword" );
			Main.itemTexture[ItemID.CopperShortsword] = GetTexture( "Items/Vanilla/CopperShortsword" );
			Main.itemTexture[ItemID.CopperBow] = GetTexture( "Items/Vanilla/CopperBow" );
			Main.itemTexture[ItemID.CopperPickaxe] = GetTexture( "Items/Vanilla/CopperPickaxe" );
			Main.itemTexture[ItemID.CopperAxe] = GetTexture( "Items/Vanilla/CopperAxe" );
			Main.itemTexture[ItemID.CopperHammer] = GetTexture( "Items/Vanilla/CopperHammer" );

			Main.itemTexture[ItemID.TinBroadsword] = GetTexture( "Items/Vanilla/TinBroadsword" );
			Main.itemTexture[ItemID.TinShortsword] = GetTexture( "Items/Vanilla/TinShortsword" );
			Main.itemTexture[ItemID.TinBow] = GetTexture( "Items/Vanilla/TinBow" );
			Main.itemTexture[ItemID.TinPickaxe] = GetTexture( "Items/Vanilla/TinPickaxe" );
			Main.itemTexture[ItemID.TinAxe] = GetTexture( "Items/Vanilla/TinAxe" );
			Main.itemTexture[ItemID.TinHammer] = GetTexture( "Items/Vanilla/TinHammer" );

			// Iron-Tier Ore
			Main.itemTexture[ItemID.IronBroadsword] = GetTexture( "Items/Vanilla/IronBroadsword" );
			Main.itemTexture[ItemID.IronShortsword] = GetTexture( "Items/Vanilla/IronShortsword" );
			Main.itemTexture[ItemID.IronBow] = GetTexture( "Items/Vanilla/IronBow" );
			Main.itemTexture[ItemID.IronPickaxe] = GetTexture( "Items/Vanilla/IronPickaxe" );
			Main.itemTexture[ItemID.IronAxe] = GetTexture( "Items/Vanilla/IronAxe" );
			Main.itemTexture[ItemID.IronHammer] = GetTexture( "Items/Vanilla/IronHammer" );

			Main.itemTexture[ItemID.LeadBroadsword] = GetTexture( "Items/Vanilla/LeadBroadsword" );
			Main.itemTexture[ItemID.LeadShortsword] = GetTexture( "Items/Vanilla/LeadShortsword" );
			Main.itemTexture[ItemID.LeadBow] = GetTexture( "Items/Vanilla/LeadBow" );
			Main.itemTexture[ItemID.LeadPickaxe] = GetTexture( "Items/Vanilla/LeadPickaxe" );
			Main.itemTexture[ItemID.LeadAxe] = GetTexture( "Items/Vanilla/LeadAxe" );
			Main.itemTexture[ItemID.LeadHammer] = GetTexture( "Items/Vanilla/LeadHammer" );

			// Silver-Tier Ore
			Main.itemTexture[ItemID.SilverBroadsword] = GetTexture( "Items/Vanilla/SilverBroadsword" );
			Main.itemTexture[ItemID.SilverShortsword] = GetTexture( "Items/Vanilla/SilverShortsword" );
			Main.itemTexture[ItemID.SilverBow] = GetTexture( "Items/Vanilla/SilverBow" );
			Main.itemTexture[ItemID.SilverPickaxe] = GetTexture( "Items/Vanilla/SilverPickaxe" );
			Main.itemTexture[ItemID.SilverAxe] = GetTexture( "Items/Vanilla/SilverAxe" );
			Main.itemTexture[ItemID.SilverHammer] = GetTexture( "Items/Vanilla/SilverHammer" );

			Main.itemTexture[ItemID.TungstenBroadsword] = GetTexture( "Items/Vanilla/TungstenBroadsword" );
			Main.itemTexture[ItemID.TungstenShortsword] = GetTexture( "Items/Vanilla/TungstenShortsword" );
			Main.itemTexture[ItemID.TungstenBow] = GetTexture( "Items/Vanilla/TungstenBow" );
			Main.itemTexture[ItemID.TungstenPickaxe] = GetTexture( "Items/Vanilla/TungstenPickaxe" );
			Main.itemTexture[ItemID.TungstenAxe] = GetTexture( "Items/Vanilla/TungstenAxe" );
			Main.itemTexture[ItemID.TungstenHammer] = GetTexture( "Items/Vanilla/TungstenHammer" );

			// Gold-Tier Ore
			Main.itemTexture[ItemID.GoldBroadsword] = GetTexture( "Items/Vanilla/GoldBroadsword" );
			Main.itemTexture[ItemID.GoldShortsword] = GetTexture( "Items/Vanilla/GoldShortsword" );
			Main.itemTexture[ItemID.GoldBow] = GetTexture( "Items/Vanilla/GoldBow" );
			Main.itemTexture[ItemID.GoldPickaxe] = GetTexture( "Items/Vanilla/GoldPickaxe" );
			Main.itemTexture[ItemID.GoldAxe] = GetTexture( "Items/Vanilla/GoldAxe" );
			Main.itemTexture[ItemID.GoldHammer] = GetTexture( "Items/Vanilla/GoldHammer" );

			Main.itemTexture[ItemID.PlatinumBroadsword] = GetTexture( "Items/Vanilla/PlatinumBroadsword" );
			Main.itemTexture[ItemID.PlatinumShortsword] = GetTexture( "Items/Vanilla/PlatinumShortsword" );
			Main.itemTexture[ItemID.PlatinumBow] = GetTexture( "Items/Vanilla/PlatinumBow" );
			Main.itemTexture[ItemID.PlatinumPickaxe] = GetTexture( "Items/Vanilla/PlatinumPickaxe" );
			Main.itemTexture[ItemID.PlatinumAxe] = GetTexture( "Items/Vanilla/PlatinumAxe" );
			Main.itemTexture[ItemID.PlatinumHammer] = GetTexture( "Items/Vanilla/PlatinumHammer" );

			// Cobalt-Tier Ore
			Main.itemTexture[ItemID.CobaltSword] = GetTexture( "Items/Vanilla/CobaltSword" );
			Main.itemTexture[ItemID.CobaltDrill] = GetTexture( "Items/Vanilla/CobaltDrill" );
			SetProjectileTexture( ProjectileID.CobaltDrill, GetTexture( "Projectiles/Vanilla/CobaltDrill" ) );
			Main.itemTexture[ItemID.CobaltChainsaw] = GetTexture( "Items/Vanilla/CobaltChainsaw" );
			SetProjectileTexture( ProjectileID.CobaltChainsaw, GetTexture( "Projectiles/Vanilla/CobaltChainsaw" ) );

			Main.itemTexture[ItemID.PalladiumSword] = GetTexture( "Items/Vanilla/PalladiumSword" );
			Main.itemTexture[ItemID.PalladiumDrill] = GetTexture( "Items/Vanilla/PalladiumDrill" );
			SetProjectileTexture( ProjectileID.PalladiumDrill, GetTexture( "Projectiles/Vanilla/PalladiumDrill" ) );
			Main.itemTexture[ItemID.PalladiumChainsaw] = GetTexture( "Items/Vanilla/PalladiumChainsaw" );
			SetProjectileTexture( ProjectileID.PalladiumChainsaw, GetTexture( "Projectiles/Vanilla/PalladiumChainsaw" ) );

			// Mythril-Tier Ore
			Main.itemTexture[ItemID.MythrilSword] = GetTexture( "Items/Vanilla/MythrilSword" );
			Main.itemTexture[ItemID.MythrilDrill] = GetTexture( "Items/Vanilla/MythrilDrill" );
			SetProjectileTexture( ProjectileID.MythrilDrill, GetTexture( "Projectiles/Vanilla/MythrilDrill" ) );
			Main.itemTexture[ItemID.MythrilChainsaw] = GetTexture( "Items/Vanilla/MythrilChainsaw" );
			SetProjectileTexture( ProjectileID.MythrilChainsaw, GetTexture( "Projectiles/Vanilla/MythrilChainsaw" ) );

			Main.itemTexture[ItemID.OrichalcumSword] = GetTexture( "Items/Vanilla/OrichalcumSword" );
			Main.itemTexture[ItemID.OrichalcumDrill] = GetTexture( "Items/Vanilla/OrichalcumDrill" );
			SetProjectileTexture( ProjectileID.OrichalcumDrill, GetTexture( "Projectiles/Vanilla/OrichalcumDrill" ) );
			Main.itemTexture[ItemID.OrichalcumChainsaw] = GetTexture( "Items/Vanilla/OrichalcumChainsaw" );
			SetProjectileTexture( ProjectileID.OrichalcumChainsaw, GetTexture( "Projectiles/Vanilla/OrichalcumChainsaw" ) );

			// Adamantite-Tier Ore
			Main.itemTexture[ItemID.AdamantiteSword] = GetTexture( "Items/Vanilla/AdamantiteSword" );
			Main.itemTexture[ItemID.AdamantiteDrill] = GetTexture( "Items/Vanilla/AdamantiteDrill" );
			SetProjectileTexture( ProjectileID.AdamantiteDrill, GetTexture( "Projectiles/Vanilla/AdamantiteDrill" ) );
			Main.itemTexture[ItemID.AdamantiteChainsaw] = GetTexture( "Items/Vanilla/AdamantiteChainsaw" );
			SetProjectileTexture( ProjectileID.AdamantiteChainsaw, GetTexture( "Projectiles/Vanilla/AdamantiteChainsaw" ) );
			Main.itemTexture[ItemID.AdamantiteGlaive] = GetTexture( "Items/Vanilla/AdamantiteGlaive" );
			SetProjectileTexture( ProjectileID.AdamantiteGlaive, GetTexture( "Projectiles/Vanilla/AdamantiteGlaive" ) );

			Main.itemTexture[ItemID.TitaniumSword] = GetTexture( "Items/Vanilla/TitaniumSword" );
			Main.itemTexture[ItemID.TitaniumDrill] = GetTexture( "Items/Vanilla/TitaniumDrill" );
			SetProjectileTexture( ProjectileID.TitaniumDrill, GetTexture( "Projectiles/Vanilla/TitaniumDrill" ) );
			Main.itemTexture[ItemID.TitaniumChainsaw] = GetTexture( "Items/Vanilla/TitaniumChainsaw" );
			SetProjectileTexture( ProjectileID.TitaniumChainsaw, GetTexture( "Projectiles/Vanilla/TitaniumChainsaw" ) );

			// Caesium (Chlorophyte)-Tier Ore
			Main.itemTexture[ItemID.ChlorophyteOre] = GetTexture( "Items/Vanilla/ChlorophyteOre" );
			Main.itemTexture[ItemID.ChlorophyteBar] = GetTexture( "Items/Vanilla/ChlorophyteBar" );

			Main.tileTexture[TileID.Chlorophyte] = GetTexture( "Tiles/Vanilla/ChlorophyteOre" );

			SetNPCTexture( NPCID.Pixie, GetTexture( "NPCs/Vanilla/Pixie" ) );

			for( int i = 0; i < disabledItems.Length; i++ )
			{
				Main.itemTexture[disabledItems[i]] = GetTexture( "Items/_DisabledItem" );
			}

		}

		public override void AddRecipes()
		{
			this.RemoveOldRecipes();
			this.SetupNewRecipeGroups();
			this.SetupNewRecipes();
		}

		private void RemoveOldRecipes()
		{
			RecipeUtils.RemoveAllRecipesWithResult( ItemID.AvengerEmblem );
			RecipeUtils.RemoveAllRecipesWithResult( ItemID.BladeofGrass );

			// Remove all recipes using the disabled items.
			for( int i = 0; i < disabledItems.Length; i++ )
			{
				RecipeUtils.RemoveAllRecipesWithResult( disabledItems[i] );
				RecipeUtils.RemoveAllRecipesUsing( disabledItems[i] );
			}
		}

		private void SetupNewRecipeGroups()
		{
			RecipeUtils.RegisterRecipeGroup( "TerrariaReworked:CopperWatch", ItemID.CopperWatch, ItemID.TinWatch );
			RecipeUtils.RegisterRecipeGroup( "TerrariaReworked:SilverWatch", ItemID.SilverWatch, ItemID.TungstenWatch );
			RecipeUtils.RegisterRecipeGroup( "TerrariaReworked:GoldWatch", ItemID.GoldWatch, ItemID.PlatinumWatch );
			RecipeUtils.RegisterRecipeGroup( "TerrariaReworked:SilverBroadsword", ItemID.SilverBroadsword, ItemID.TungstenBroadsword );
			RecipeUtils.RegisterRecipeGroup( "TerrariaReworked:SilverPickaxe", ItemID.SilverPickaxe, ItemID.TungstenPickaxe );
			RecipeUtils.RegisterRecipeGroup( "TerrariaReworked:SilverAxe", ItemID.SilverAxe, ItemID.TungstenAxe );
			RecipeUtils.RegisterRecipeGroup( "TerrariaReworked:SilverHammer", ItemID.SilverHammer, ItemID.TungstenHammer );
			RecipeUtils.RegisterRecipeGroup( "TerrariaReworked:SilverBow", ItemID.SilverBow, ItemID.TungstenBow );
		}

		private void SetupNewRecipes()
		{
			ModRecipe rec;

			rec = new ModRecipe( this );
			rec.AddIngredient( ItemID.WarriorEmblem, 1 );
			rec.AddIngredient( ItemID.RangerEmblem, 1 );
			rec.AddIngredient( ItemID.SorcererEmblem, 1 );
			rec.AddIngredient( ItemID.SummonerEmblem, 1 );
			rec.AddTile( TileID.TinkerersWorkbench );
			rec.SetResult( ItemID.AvengerEmblem );
			rec.AddRecipe();

			rec = new ModRecipe( this );
			rec.AddRecipeGroup( "TerrariaReworked:SilverBroadsword", 1 );
			rec.AddIngredient( ItemID.JungleSpores, 15 );
			rec.AddIngredient( ItemID.Stinger, 12 );
			rec.AddTile( TileID.Anvils );
			rec.SetResult( ItemID.BladeofGrass );
			rec.AddRecipe();


			rec = new ModRecipe( this );
			rec.AddIngredient( ItemID.Coal, 2 );
			rec.AddRecipeGroup( "Wood", 1 );
			rec.SetResult( ItemID.Torch, 6 );
			rec.AddRecipe();
		}
		
		public static void SetNPCTexture( int npc, Texture2D tex )
		{
			if( npc < 0 || npc > Main.npcTexture.Length || npc > Main.NPCLoaded.Length )
			{
				throw new Exception( "[SetNPCTexture]: NPC texture index out of range." );
			}
			if( tex == null )
			{
				throw new Exception( "[SetNPCTexture]: NPC texture can't be null." );
			}
			Main.npcTexture[npc] = tex;
			Main.NPCLoaded[npc] = true;
		}

		public static void SetProjectileTexture( int proj, Texture2D tex )
		{
			if( proj < 0 || proj > Main.projectileTexture.Length || proj > Main.projectileLoaded.Length )
			{
				throw new Exception( "[SetProjectileTexture]: Projectile texture index out of range." );
			}
			if( tex == null )
			{
				throw new Exception( "[SetProjectileTexture]: Projectile texture can't be null." );
			}
			Main.projectileTexture[proj] = tex;
			Main.projectileLoaded[proj] = true;
		}

		public static void DrawShadowSprites( NPC npc, int numSprites, int spread )
		{
			// shadow trail thing effect!

			Color lightingcolor = Lighting.GetColor( (int)((double)npc.position.X + (double)npc.width * 0.5) / 16, (int)(((double)npc.position.Y + (double)npc.height * 0.5) / 16.0) );
			Vector2 center = new Vector2( 72f, 106f );

			//float addHeight = 0 * npc.scale;  // difference between texture frame height and npc real height.

			SpriteEffects spriteEffects = SpriteEffects.None;
			if( npc.spriteDirection == 1 )
			{
				spriteEffects = SpriteEffects.FlipHorizontally;
			}
			int len = (numSprites * spread) - 1;
			int lenPlus1 = len + 1;
			int lenPlus1TimesTwo = lenPlus1 * 2;
			for( int i = len; i >= 0; i -= spread )
			{
				Vector2[] oldposArray = npc.oldPos;
				Color color = npc.GetAlpha( lightingcolor );
				color.R = (byte)((int)color.R * (lenPlus1 - i) / lenPlus1TimesTwo);
				color.G = (byte)((int)color.G * (lenPlus1 - i) / lenPlus1TimesTwo);
				color.B = (byte)((int)color.B * (lenPlus1 - i) / lenPlus1TimesTwo);
				color.A = (byte)((int)color.A * (lenPlus1 - i) / lenPlus1TimesTwo);
				Main.spriteBatch.Draw( Main.npcTexture[npc.type],
					new Vector2(
						npc.oldPos[i].X - Main.screenPosition.X + (float)(npc.width / 2) - (float)Main.npcTexture[npc.type].Width * npc.scale / 2f + center.X * npc.scale,
						npc.oldPos[i].Y - Main.screenPosition.Y + (float)npc.height - (float)Main.npcTexture[npc.type].Height * npc.scale / (float)Main.npcFrameCount[npc.type] + 4f + center.Y * npc.scale /*+ addHeight*/
					), new Rectangle?( npc.frame ), color, npc.rotation, center, npc.scale, spriteEffects, 0f );
			}
		}
	}
}