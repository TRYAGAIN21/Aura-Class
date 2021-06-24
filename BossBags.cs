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
			// This method shows adding items to Fishrons boss bag. 
			// Typically you'll also want to also add an item to the non-expert boss drops, that code can be found in ExampleGlobalNPC.NPCLoot. Use this and that to add drops to bosses.
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