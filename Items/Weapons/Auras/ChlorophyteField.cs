using AuraClass.AuraDamageClass;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AuraClass.Items.Weapons.Auras
{
    public class ChlorophyteField : AuraItem
    {
        public override void SetStaticDefaults()
        {
            //DisplayName.SetDefault("Chlorophyte Field");
            //Tooltip.SetDefault("Rapidly spews spores in random directions");
        }

        public override void SafeSetDefaults()
        {
            item.damage = 46;
            item.width = 78;
            item.height = 78;
            item.noMelee = true;
            item.useTime = 18;
            item.useAnimation = 18;
            item.autoReuse = true;
            item.channel = true;
            item.useStyle = 4;
            item.value = 0;
            item.rare = 7;
            item.shoot = mod.ProjectileType("ChlorophyteField");
            item.GetGlobalItem<NormalGlobalItem>().glowmaskTex = ModContent.GetTexture("AuraClass/Items/Weapons/Auras/ChlorophyteField_Mask");
            item.value = Item.sellPrice(0, 5, 75, 0);
            decayRate = 0.1f;
        }
    }
}

