using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;

namespace AuraClass.NPCs
{
	public class GlobalFragmentedNPC : GlobalNPC
	{
        public override bool InstancePerEntity => true;

        public NPC StardustSlimeMaster;
        public bool StardustSlimeMinion;
        public override bool PreAI(NPC npc)
        {
            if (StardustSlimeMinion)
            {
                npc.dontTakeDamage = true;
                if (StardustSlimeMaster.life <= 0)
                {
                    npc.alpha += 255 / 30;
                    if (npc.alpha >= 255)
                    {
                        npc.life = -1;
                        npc.active = false;
                    }
                }
            }
            return base.PreAI(npc);
        }
	}
}