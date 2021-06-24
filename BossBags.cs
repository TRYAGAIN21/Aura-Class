using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AuraClass
{
	public class BossBags : GlobalItem
	{
		public override void OpenVanillaBag(string context, Player player, int arg)
		{
			if (context == "bossBag" && arg == ItemID.QueenBeeBossBag)
			{
				if (Main.rand.Next(4) == 0)
				{
					player.QuickSpawnItem(mod.ItemType("HiveAura"));
				}
			}

			if (context == "bossBag" && arg == ItemID.WallOfFleshBossBag)
			{
				if (Main.rand.Next(4) == 0)
				{
					player.QuickSpawnItem(mod.ItemType("AuraEmblem"));
				}
			}
		}
	}
}
