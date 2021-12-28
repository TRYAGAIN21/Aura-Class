using AuraClass.AuraDamageClass;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AuraClass.Items.Weapons.Auras
{
    public class ShrimpyBubble : AuraItem
    {
        public override void SetStaticDefaults()
        {
            //DisplayName.SetDefault("Shrimpy Bubble");
            //Tooltip.SetDefault("Sometimes sends 5 sharkrons toward your opponents" +
            //    "\nSharkrons ignore half of the enemy's defense and the aura ignores all defense" +
            //    "\nInsanely good against multiple targets");
        }

        public override void SafeSetDefaults()
        {
            item.damage = 96;
            item.width = 32;
            item.height = 34;
            item.noMelee = true;
            item.useTime = 24;
            item.useAnimation = 24;
            item.autoReuse = true;
            item.channel = true;
            item.useStyle = 4;
            item.value = 0;
            item.rare = 8;
            item.shoot = mod.ProjectileType("ShrimpyBubble");
            decayRate = 0.94f;
        }
    }
}

