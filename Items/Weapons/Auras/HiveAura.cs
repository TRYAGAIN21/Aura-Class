using AuraClass.AuraDamageClass;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AuraClass.Items.Weapons.Auras
{
    public class HiveAura : AuraItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Bee Hive");
            Tooltip.SetDefault("3 Bees will guard the hive");
        }

        public override void SafeSetDefaults()
        {
            item.damage = 25;
            item.width = 28;
            item.height = 28;
            item.noMelee = true;
            item.useTime = 27;
            item.useAnimation = 27;
            item.autoReuse = true;
            item.channel = true;
            item.useStyle = 4;
            item.value = 0;
            item.rare = 3;
            item.shoot = mod.ProjectileType("HiveAuraAura");
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

