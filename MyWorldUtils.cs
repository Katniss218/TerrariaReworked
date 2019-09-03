
using System.Collections.Generic;
using Terraria.World.Generation;

namespace TerrariaReworked
{
	public static class MyWorldUtils
	{

		/// <summary>
		/// Removes worldgen tasks with the specified names.
		/// </summary>
		/// <param name="tasks">The list of tasks to remove from.</param>
		/// <param name="taskNames">The names of the tasks to remove.</param>
		public static void RemoveWorldGenTasks( ref List<GenPass> tasks, params string[] taskNames )
		{
			if( taskNames.Length > tasks.Count )
			{
				throw new System.Exception( "RemoveWorldGenTasks called with an invalid argument: (taskNames.Length > tasks.Count)" );
			}
			int index;
			for( int i = 0; i < taskNames.Length; i++ )
			{
				index = tasks.FindIndex( genpass => genpass.Name.Equals( taskNames[i] ) );
				if( index == -1 )
				{
					throw new System.Exception( "Worldgen task with the name '" + taskNames[i] + "' doesn't exist in the list." );
				}
				tasks.RemoveAt( index );
			}
		}

		public static void ReplaceWorldGenTask( ref List<GenPass> tasks, string taskName, GenPass pass )
		{
			int index = tasks.FindIndex( genpass => genpass.Name.Equals( taskName ) );
			if( index == -1 )
			{
				throw new System.Exception( "Worldgen task with the name '" + taskName + "' doesn't exist in the list." );
			}
			tasks.RemoveAt( index );
			tasks.Insert( index, pass );
		}
	}
}