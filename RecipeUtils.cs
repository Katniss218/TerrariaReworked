using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Katniss
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
			int removedThisManyRecipes = 0;
			
			// Removes all recipes containing any ingredients of type 'ingredientType'.
			removedThisManyRecipes += rec.RemoveAll( x => x != null && x.requiredItem != null && x.requiredItem.ToList().Find( y => y.type == ingredientId ) != null );

			Main.recipe = rec.ToArray();
			Array.Resize( ref Main.recipe, Recipe.maxRecipes );
			Recipe.numRecipes -= removedThisManyRecipes;

			// remove any introduced 'null' recipes after the last one.
			/*for( int i = Recipe.numRecipes; i < Main.recipe.Length; i++ )
			{
				if( Main.recipe[i] == null )
				{
					Main.recipe[i] = new Recipe();
				}
			}*/
			//Recipe.maxRecipes = Main.recipe.Length;
			//ModRecipe.numRecipes 
			return removedThisManyRecipes;
		}
	}
}