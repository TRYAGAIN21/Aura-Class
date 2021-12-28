using AuraClass.AuraDamageClass;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AuraClass.Items.Weapons.Auras
{
    public class TemplesGuard : AuraItem
    {
        public override void SetStaticDefaults()
        {
            //DisplayName.SetDefault("Temple's Guard");
            //Tooltip.SetDefault("Fires fireballs at up to 5 nearby enemies" +
            //    "\nWhen attacked and below 50% health you will fire a laser towards the attacker" +
            //    "\nIf hit by a projectile it will fire towards the nearest enemy");
        }

        public override void SafeSetDefaults()
        {
            item.damage = 54;
            item.width = 28;
            item.height = 28;
            item.noMelee = true;
            item.useTime = 15;
            item.useAnimation = 15;
            item.autoReuse = true;
            item.channel = true;
            item.useStyle = 4;
            item.value = 0;
            item.rare = 7;
            item.shoot = mod.ProjectileType("TemplesGuard");
            item.GetGlobalItem<NormalGlobalItem>().glowmaskTex = ModContent.GetTexture("AuraClass/Items/Weapons/Auras/TemplesGuard_Mask");
            item.value = Item.sellPrice(0, 7, 0, 0);
            decayRate = 0.4f;
        }
    }
}

