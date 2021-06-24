using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AuraClass
{
    public class AuraClassNPC : GlobalNPC
    {
        public override void NPCLoot(NPC npc)
        {
            if (Main.player[(int)Player.FindClosest(npc.position, npc.width, npc.height)].ZoneGlowshroom)
            {
                if (Main.rand.Next(4) == 0 && Main.expertMode)
                {
                    Item.NewItem(npc.getRect(), ModContent.ItemType<Items.MushroomPowder>(), 1);
                }
                else if (Main.rand.Next(6) == 0 && !Main.expertMode)
                {
                    Item.NewItem(npc.getRect(), ModContent.ItemType<Items.MushroomPowder>(), 1);
                }
            }

            if (npc.type == NPCID.QueenBee)
            {
                if (Main.rand.Next(4) == 0 && !Main.expertMode)
                {
                    Item.NewItem(npc.getRect(), mod.ItemType("HiveAura"));
                }
            }

            if (npc.type == NPCID.Antlion)
            {
                if (Main.rand.Next(25) == 0)
                {
                    Item.NewItem(npc.getRect(), mod.ItemType("SandAura"));
                }
            }

            if (npc.type == NPCID.WallofFlesh && !Main.expertMode)
            {
                if (Main.rand.Next(4) == 0)
                {
                    Item.NewItem(npc.getRect(), mod.ItemType("AuraEmblem"));
                }
            }
        }
    }
}