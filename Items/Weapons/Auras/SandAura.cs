using AuraClass.AuraDamageClass;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AuraClass.Items.Weapons.Auras
{
    public class SandAura : AuraItem
    {
        public override void SetStaticDefaults()
        {
            //DisplayName.SetDefault("Desert Sands");
            //Tooltip.SetDefault("Sometimes shoots multiple sand balls at 3 nearby enemies");
        }

        public override void SafeSetDefaults()
        {
            item.damage = 9;
            item.width = 32;
            item.height = 32;
            item.noMelee = true;
            item.useTime = 19;
            item.useAnimation = 19;
            item.autoReuse = true;
            item.channel = true;
            item.useStyle = 4;
            item.value = 0;
            item.rare = 1;
            item.shoot = mod.ProjectileType("SandAuraAura");
            item.value = Item.sellPrice(0, 0, 10, 0);
            decayRate = 0.2f;
        }
    }
}

