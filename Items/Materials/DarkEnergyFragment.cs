using AuraClass.AuraDamageClass;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AuraClass.Items.Materials
{
    public class DarkEnergyFragment : AuraItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dark Energy Fragment");
            Tooltip.SetDefault("'The essence of the void lingers in this fragment'");
            ItemID.Sets.ItemIconPulse[item.type] = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
        }

        public override void SafeSetDefaults()
        {
            item.width = 28;
            item.height = 28;
            item.maxStack = 999;
            item.value = 0;
            item.rare = 9;
        }

        public override void PostUpdate()
        {
            Lighting.AddLight(item.Center, Color.WhiteSmoke.ToVector3() * 0.55f * Main.essScale);
        }
    }
}

