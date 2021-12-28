using AuraClass.AuraDamageClass;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AuraClass.Items.Weapons.Auras
{
    public class MoonRock : AuraItem
    {
        public override void SafeSetDefaults()
        {
            item.damage = 52;
            item.width = 40;
            item.height = 40;
            item.noMelee = true;
            item.useTime = 16;
            item.useAnimation = 16;
            item.autoReuse = true;
            item.channel = true;
            item.useStyle = 4;
            item.value = 0;
            item.rare = 10;
            item.shoot = mod.ProjectileType("MoonRock");
            item.value = Item.sellPrice(0, 20, 0, 0);
            decayRate = 0.72f;
        }
    }
}

