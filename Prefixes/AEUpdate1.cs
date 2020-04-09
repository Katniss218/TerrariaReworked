using System;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace TerrariaReworked
{
	// Token: 0x02000002 RID: 2
	public class AEUpdate1 : GlobalItem
	{
		public override bool InstancePerEntity
		{
			get
			{
				return true;
			}
		}

		public float damage;

		public int crit;

		public float moveSpeed;

		public float meleeSpeed;

		public int defense;

		public int statManaMax2;

		public override GlobalItem Clone( Item item, Item itemClone )
		{
			AEUpdate1 aeupdate = (AEUpdate1)base.Clone( item, itemClone );
			aeupdate.damage = this.damage;
			aeupdate.crit = this.crit;
			aeupdate.moveSpeed = this.moveSpeed;
			aeupdate.meleeSpeed = this.meleeSpeed;
			aeupdate.defense = this.defense;
			aeupdate.statManaMax2 = this.statManaMax2;
			return aeupdate;
		}
		
		public override bool NewPreReforge( Item item )
		{
			this.damage = 0;
			this.crit = 0;
			this.moveSpeed = 0;
			this.meleeSpeed = 0;
			this.defense = 0;
			this.statManaMax2 = 0;
			return base.NewPreReforge( item );
		}
		
		public override void ModifyTooltips( Item item, List<TooltipLine> tooltips )
		{

			if( this.damage > 0 )
			{
				tooltips.Add( new TooltipLine( base.mod, "damage", "+" + (this.damage * 100) + "% damage" )
				{
					isModifier = true
				} );
			}
			if( this.crit > 0 )
			{
				tooltips.Add( new TooltipLine( base.mod, "crit", "+" + this.crit + "% critical strike chance" )
				{
					isModifier = true
				} );
			}
			if( this.moveSpeed > 0 )
			{
				tooltips.Add( new TooltipLine( base.mod, "moveSpeed", "+" + (this.moveSpeed * 100) + "% movement speed" )
				{
					isModifier = true
				} );
			}
			if( this.meleeSpeed > 0 )
			{
				tooltips.Add( new TooltipLine( base.mod, "meleeSpeed", "+" + (this.meleeSpeed * 100) + "% melee speed" )
				{
					isModifier = true
				} );
			}
			if( this.defense > 0 )
			{
				tooltips.Add( new TooltipLine( base.mod, "defense", "+" + this.defense + " defense" )
				{
					isModifier = true
				} );
			}
			if( this.statManaMax2 > 0 )
			{
				tooltips.Add( new TooltipLine( base.mod, "statManaMax2", "+" + this.statManaMax2 + " mana" )
				{
					isModifier = true
				} );
			}

			if( this.damage < 0 )
			{
				tooltips.Add( new TooltipLine( base.mod, "damage", (this.damage * 100) + "% damage" )
				{
					isModifier = true,
					isModifierBad = true
				} );
			}
			if( this.moveSpeed < 0 )
			{
				tooltips.Add( new TooltipLine( base.mod, "moveSpeed", (this.moveSpeed * 100) + "% movement speed" )
				{
					isModifier = true,
					isModifierBad = true
				} );
			}
			if( this.meleeSpeed < 0 )
			{
				tooltips.Add( new TooltipLine( base.mod, "meleeSpeed", (this.meleeSpeed * 100) + "% melee speed" )
				{
					isModifier = true,
					isModifierBad = true
				} );
			}
			if( this.defense < 0 )
			{
				tooltips.Add( new TooltipLine( base.mod, "defense", this.defense + " defense" )
				{
					isModifier = true,
					isModifierBad = true
				} );
			}
		}

		public override void UpdateEquip( Item item, Player player )
		{
			if( item.prefix > 0 )
			{
				player.rangedDamage += this.damage;
				player.minionDamage += this.damage;
				player.meleeDamage += this.damage;
				player.magicDamage += this.damage;
				player.thrownDamage += this.damage;
				player.meleeCrit += this.crit;
				player.rangedCrit += this.crit;
				player.magicCrit += this.crit;
				player.thrownCrit += this.crit;
				player.moveSpeed += this.moveSpeed;
				player.meleeSpeed += this.meleeSpeed;
				player.statDefense += this.defense;
				player.statManaMax2 += this.statManaMax2;
			}
		}
		
		public override void NetSend( Item item, BinaryWriter writer )
		{
			writer.Write( this.damage );
			writer.Write( this.crit );
			writer.Write( this.moveSpeed );
			writer.Write( this.meleeSpeed );
			writer.Write( this.defense );
			writer.Write( this.statManaMax2 );
		}
		
		public override void NetReceive( Item item, BinaryReader reader )
		{
			this.damage = reader.ReadInt32();
			this.crit = reader.ReadInt32();
			this.moveSpeed = reader.ReadInt32();
			this.meleeSpeed = reader.ReadInt32();
			this.defense = reader.ReadInt32();
			this.statManaMax2 = reader.ReadInt32();
		}
	}
}