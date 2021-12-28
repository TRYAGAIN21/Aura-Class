using AuraClass.AuraDamageClass;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AuraClass.Items.Weapons.Auras
{
    public class JungleBlossom : AuraItem
    {
        public override void SetStaticDefaults()
        {
            //DisplayName.SetDefault("Jungle Blossom");
            //Tooltip.SetDefault("Creates homing flower vines every 8 seconds" +
            //    "\nCreates 4 flowers every second that slow down and then fire their petals" +
            //    "\nThese flowers also redirect themselves towards a nearby enemy after firing petals" +
            //    "\nThese vines last for 6 seconds and also split every 2 seconds");
        }

        public override void SafeSetDefaults()
        {
            item.damage = 38;
            item.width = 74;
            item.height = 74;
            item.noMelee = true;
            item.useTime = 27;
            item.useAnimation = 27;
            item.autoReuse = true;
            item.channel = true;
            item.useStyle = 4;
            item.value = 0;
            item.rare = 8;
            item.shoot = mod.ProjectileType("JungleBlossom");
            item.value = Item.sellPrice(0, 6, 0, 0);
            decayRate = 0.5f;
        }
    }
}

