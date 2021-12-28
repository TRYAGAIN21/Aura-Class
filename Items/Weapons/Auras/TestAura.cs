using AuraClass.AuraDamageClass;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace AuraClass.Items.Weapons.Auras
{
    public class TestAura : AuraItem
    {
        public override bool Autoload(ref string name)
        {
            return false;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Test Aura");
            Tooltip.SetDefault("This is a test");
        }

        public override void SafeSetDefaults()
        {
            item.damage = 6;
            item.width = 16;
            item.height = 16;
            item.noMelee = true;
            item.useTime = 25;
            item.useAnimation = 25;
            item.autoReuse = true;
            item.channel = true;
            item.useStyle = 4;
            item.value = 0;
            item.rare = 10;
            item.shoot = mod.ProjectileType("TestAuraAura");
            item.noUseGraphic = true;
        }
    }
}

