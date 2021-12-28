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
            //DisplayName.SetDefault("Black Magic");
            //Tooltip.SetDefault("'Dark magic can also be used by even the purest of heroes'");
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
            item.GetGlobalItem<NormalGlobalItem>().glowmaskTex = ModContent.GetTexture("AuraClass/Items/Weapons/Auras/BlackMagic_Mask");
            item.value = Item.sellPrice(0, 2, 0, 0);
            decayRate = 0.5f;
        }
    }
}

