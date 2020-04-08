using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace TerrariaReworked
{
	public class RecipeUtils
	{
		public static void RegisterRecipeGroup( string id, params int[] itemIds )
		{
			RecipeGroup	group = new RecipeGroup( () => Language.GetTextValue( "LegacyMisc.37" ) + " " + Lang.GetItemNameValue( itemIds[0] ), itemIds );
			RecipeGroup.RegisterGroup( id, group );
		}

		public static void RegisterRecipeGroup( string id, Func<string> getName, params int[] itemIds )
		{
			RecipeGroup group = new RecipeGroup( getName, itemIds );
			RecipeGroup.RegisterGroup( id, group );
		}

		public static int RemoveAllRecipesWithResult( int resultId )
		{
			List<Recipe> rec = Main.recipe.ToList();
			int removedThisManyRecipes = 0;

			removedThisManyRecipes += rec.RemoveAll( x => x != null && x.createItem != null && x.createItem.type == resultId );
			
			Main.recipe = rec.ToArray();
			Array.Resize( ref Main.recipe, Recipe.maxRecipes );
			Recipe.numRecipes -= removedThisManyRecipes;
			//Main.reci
			//Recipe.
			return removedThisManyRecipes;
		}

		public static int RemoveAllRecipesUsing( int ingredientId )
		{
			List<Recipe> rec = Main.recipe.ToList();

			int acc = 0;
			for( int i = rec.Count - 1; i >= 0; i-- )
			{
				if( rec[i] == null )
				{
					rec.RemoveAt( i );
					acc++;
					continue;
				}
				if( rec[i].requiredItem == null )
				{
					rec.RemoveAt( i );
					acc++;
					continue;
				}
				for( int j = 0; j < rec[i].requiredItem.Length; j++ )
				{
					if( rec[i].requiredItem[j].type == ingredientId )
					{
						rec.RemoveAt( i );
						acc++;
						break;
					}
				}
			} 
			
			Main.recipe = rec.ToArray();
			Array.Resize( ref Main.recipe, Recipe.maxRecipes );
			Recipe.numRecipes -= acc;
			
			return acc;
		}
	}
}
 