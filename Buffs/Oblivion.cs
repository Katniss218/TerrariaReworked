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

			Rectangle r = player.getRect();

			float size = r.Width * 0.0001f * r.Height;
			if( size < 1 ) size = 1;
			if( size > 50 ) size = 50;

			for( int i = 0; i < size; i++ )
			{
				int dust = Dust.NewDust( r.Location.ToVector2(), r.Width, r.Height, ModMain.instance.DustType( "OblivionDust" ), 0f, 0f, 100, default, 3f );
				Main.dust[dust].noGravity = true;
				
				dust = Dust.NewDust( r.Location.ToVector2(), r.Width, r.Height, ModMain.instance.DustType( "OblivionDust" ), Main.rand.NextFloat( -0.3f, 0.3f ), Main.rand.NextFloat( -0.3f, 0.3f ), 100, default, 2f );
				Main.dust[dust].noGravity = true;
			}
		}

		public override void Update( NPC npc, ref int buffIndex )
		{
			npc.lifeRegen = -Strength;

			Rectangle r = npc.getRect();

			float size = r.Width * 0.0001f * r.Height;
			if( size < 1 ) size = 1;
			if( size > 50 ) size = 50;

			for( int i = 0; i < size; i++ )
			{
				int dust = Dust.NewDust( r.Location.ToVector2(), r.Width, r.Height, ModMain.instance.DustType( "OblivionDust" ), 0f, 0f, 100, default, 3f );
				Main.dust[dust].noGravity = true;

				dust = Dust.NewDust( r.Location.ToVector2(), r.Width, r.Height, ModMain.instance.DustType( "OblivionDust" ), Main.rand.NextFloat( -0.3f, 0.3f ), Main.rand.NextFloat( -0.3f, 0.3f ), 100, default, 2f );
				Main.dust[dust].noGravity = true;
			}
		}
	}
}