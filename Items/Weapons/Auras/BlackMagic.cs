using AuraClass.AuraDamageClass;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AuraClass.Items.Weapons.Auras
{
    public class BlackMagic : AuraItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Black Magic");
            Tooltip.SetDefault("'Dark magic can also be used by even the purest of heroes'");
        }

        public override void SafeSetDefaults()
        {
            item.damage = 24;
            item.width = 32;
            item.height = 32;
            item.noMelee = true;
            item.useTime = 23;
            item.useAnimation = 23;
            item.autoReuse = true;
            item.channel = true;
            item.useStyle = 4;
            item.value = 0;
            item.rare = 2;
            item.shoot = mod.ProjectileType("BlackMagicAura");
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

