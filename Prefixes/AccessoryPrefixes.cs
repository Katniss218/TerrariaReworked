using Terraria;
using Terraria.ModLoader;

namespace TerrariaReworked.Prefixes
{
	public class AccessoryPrefixes : ModPrefix
	{
		private float damage; // 0-1

		private int crit; // 1 - 100

		private float moveSpeed;

		private float meleeSpeed;

		private int defense;

		private int statManaMax2;

		private int tier;

		// see documentation for vanilla weights and more information
		// note: a weight of 0f can still be rolled. see CanRoll to exclude prefixes.
		// note: if you use PrefixCategory.Custom, actually use ChoosePrefix instead, see ExampleInstancedGlobalItem
		public override float RollChance( Item item )
		{
			return 1f;
		}

		// determines if it can roll at all.
		// use this to control if a prefixes can be rolled or not
		public override bool CanRoll( Item item )
		{
			return true;
		}

		// change your category this way, defaults to Custom
		public override PrefixCategory Category { get { return PrefixCategory.Accessory; } }

		public AccessoryPrefixes()
		{

		}

		public AccessoryPrefixes( int tier )
		{
			this.tier = tier;
		}

		// Allow multiple prefix autoloading this way (permutations of the same prefix)
		public override bool Autoload( ref string name )
		{
			if( base.Autoload( ref name ) )
			{
				mod.AddPrefix( "Gothic", new AccessoryPrefixes( 1 ) { crit = 1 } );
				mod.AddPrefix( "Gifted", new AccessoryPrefixes( 3 ) { crit = 3 } );

				mod.AddPrefix( "Resistant", new AccessoryPrefixes( 5 ) { defense = 5 } );
				mod.AddPrefix( "Epic", new AccessoryPrefixes( 5 ) { crit = 5 } );
				mod.AddPrefix( "Enraged", new AccessoryPrefixes( 5 ) { damage = 0.05f } );
				mod.AddPrefix( "Vigorous", new AccessoryPrefixes( 5 ) { moveSpeed = 0.05f } );
				mod.AddPrefix( "Hectic", new AccessoryPrefixes( 5 ) { meleeSpeed = 0.05f } );
			}
			return false;
		}

		public override void Apply( Item item )
		{
			item.GetGlobalItem<AEUpdate1>().damage = this.damage;
			item.GetGlobalItem<AEUpdate1>().crit = this.crit;
			item.GetGlobalItem<AEUpdate1>().moveSpeed = this.moveSpeed;
			item.GetGlobalItem<AEUpdate1>().meleeSpeed = this.meleeSpeed;
			item.GetGlobalItem<AEUpdate1>().defense = this.defense;
			item.GetGlobalItem<AEUpdate1>().statManaMax2 = this.statManaMax2;
		}

		public override void ModifyValue( ref float valueMult )
		{
			if( tier == 1 )
				valueMult *= 0.1025f;
			else if( tier == 2 )
				valueMult *= 0.2100f;
			else if( tier == 3 )
				valueMult *= 0.3225f;
			else if( tier == 4 )
				valueMult *= 0.4400f;
			else if( tier == 5 )
				valueMult *= 0.5525f;
			//float multiplier = 1f * (1f + (float)this.tier * 0.05f);
			//valueMult *= multiplier;
		}
	}
}