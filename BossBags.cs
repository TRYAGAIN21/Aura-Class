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
			// All Aura Class items from bosses/loot bags have double the drop rate compared to other drops from the same source
			if (context == "bossBag")
			{
				if (Main.rand.NextBool(4 / 2) && arg == ItemID.QueenBeeBossBag)
                {
					player.QuickSpawnItem(mod.ItemType("HiveAura"));
				}
				if (Main.rand.NextBool(4 / 2) && arg == ItemID.WallOfFleshBossBag)
				{
					player.QuickSpawnItem(mod.ItemType("AuraEmblem"));
				}
				if (Main.rand.NextBool(5 / 2) && arg == ItemID.FishronBossBag)
				{
					player.QuickSpawnItem(mod.ItemType("ShrimpyBubble"));
				}
				if (Main.rand.NextBool(7 / 2) && arg == ItemID.PlanteraBossBag)
				{
					player.QuickSpawnItem(mod.ItemType("JungleBlossom"));
				}
				if (Main.rand.NextBool(7 / 2) && arg == ItemID.GolemBossBag)
				{
					player.QuickSpawnItem(mod.ItemType("TemplesGuard"));
				}
				if (Main.rand.NextBool(9 / 2) && arg == ItemID.MoonLordBossBag)
				{
					player.QuickSpawnItem(mod.ItemType("MoonRock"));
				}
			}

			if (context == "lockBox")
			{
				if (Main.rand.NextBool(7 / 2) && arg == ItemID.LockBox)
				{
					player.QuickSpawnItem(mod.ItemType("BlackMagic"));
				}
			}
		}
	}
}