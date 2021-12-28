using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AuraClass.Items
{
    public class MushroomPowder : ModItem
    {
        public override void SetStaticDefaults()
        {
            //DisplayName.SetDefault("Luminous Powder");
            //Tooltip.SetDefault("Spreads the fungus");
        }

        public override void SetDefaults()
        {
            item.useStyle = 1;
            item.shootSpeed = 4f;
            item.shoot = mod.ProjectileType("MushroomPowder");
            item.width = 16;
            item.height = 24;
            item.maxStack = 99;
            item.consumable = true;
            item.UseSound = SoundID.Item1;
            item.useAnimation = 15;
            item.useTime = 15;
            item.noMelee = true;
            item.GetGlobalItem<NormalGlobalItem>().glowmaskTex = ModContent.GetTexture("AuraClass/Items/MushroomPowder_Mask");
            item.value = Item.sellPrice(0, 1, 0, 0);
        }
    }
}

