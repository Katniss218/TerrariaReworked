using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace TerrariaReworked.Buffs
{
	public class Oblivion : ModBuff
	{
		public const int Strength = 100;

		public override void SetDefaults()
		{
			DisplayName.SetDefault( "Oblivion" );
			Description.SetDefault( "You are disappearing into oblivion" );
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
			longerExpertDebuff = true;
		}

		public override void Update( Player player, ref int buffIndex )
		{
			player.lifeRegen = -Strength;

			for( int i = 0; i < 2; i++ )
			{
				Rectangle r = player.getRect();
				int dust = Dust.NewDust( r.Location.ToVector2(), r.Width, r.Height, 54, 0f, 0f, 100, new Color(), 3f );
				Main.dust[dust].noGravity = true;

				int dust2 = Dust.NewDust( r.Location.ToVector2(), r.Width, r.Height, 58, 0f, 0f, 100, new Color(), 3f );
				Main.dust[dust2].noGravity = true;
			}
		}

		public override void Update( NPC npc, ref int buffIndex )
		{
			npc.lifeRegen = -Strength;

			for( int i = 0; i < 2; i++ )
			{
				Rectangle r = npc.getRect();
				int dust = Dust.NewDust( r.Location.ToVector2(), r.Width, r.Height, 54, 0f, 0f, 100, new Color(), 3f );
				Main.dust[dust].noGravity = true;

				int dust2 = Dust.NewDust( r.Location.ToVector2(), r.Width, r.Height, 58, 0f, 0f, 100, new Color(), 3f );
				Main.dust[dust2].noGravity = true;
			}
		}
	}
}