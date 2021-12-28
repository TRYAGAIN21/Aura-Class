using AuraClass.AuraDamageClass;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AuraClass.Items.Weapons.Auras
{
    public class WavesOfFire : AuraItem
    {
        public override void SetStaticDefaults()
        {
            //DisplayName.SetDefault("Waves of Fire");
            //Tooltip.SetDefault("Has a chance to set enemies on fire" +
            //    "\nCannot set fire to enemies through walls" +
            //   "\nA Wave of Fire will be created every few seconds");
        }

        public override void SafeSetDefaults()
        {
            item.damage = 38;
            item.width = 32;
            item.height = 32;
            item.noMelee = true;
            item.useTime = 23;
            item.useAnimation = 23;
            item.autoReuse = true;
            item.channel = true;
            item.useStyle = 4;
            item.value = 0;
            item.rare = 3;
            item.shoot = mod.ProjectileType("WavesOfFireAura");
            item.GetGlobalItem<NormalGlobalItem>().glowmaskTex = ModContent.GetTexture("AuraClass/Items/Weapons/Auras/WavesOfFire_Mask");
            item.value = Item.sellPrice(0, 0, 60, 0);
            decayRate = 0.6f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.HellstoneBar, 18);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}

