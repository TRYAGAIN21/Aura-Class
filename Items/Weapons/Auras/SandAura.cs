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
            DisplayName.SetDefault("Desert Sands");
            Tooltip.SetDefault("Sometimes shoots a sand ball at nearby enemies");
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
        }

        public override void SafeShoot()
        {
            return;
        }

        public override bool CanUseItem(Player player)
        {
            // Ensures no more than one spear can be thrown out, use this when using autoReuse
            return player.ownedProjectileCounts[item.shoot] < 1;
        }
    }
}

