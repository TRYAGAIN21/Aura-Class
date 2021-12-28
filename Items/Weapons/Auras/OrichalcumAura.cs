using AuraClass.AuraDamageClass;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AuraClass.Items.Weapons.Auras
{
    public class OrichalcumAura : AuraItem
    {
        public override void SetStaticDefaults()
        {
            //DisplayName.SetDefault("Orichalcum Aura");
            //Tooltip.SetDefault("Rapidly spews petals in a rotating pattern");
        }

        public override void SafeSetDefaults()
        {
            item.damage = 37;
            item.width = 40;
            item.height = 40;
            item.noMelee = true;
            item.useTime = 22;
            item.useAnimation = 22;
            item.autoReuse = true;
            item.channel = true;
            item.useStyle = 4;
            item.value = 0;
            item.rare = 4;
            item.shoot = mod.ProjectileType("OrichalcumAura");
            item.GetGlobalItem<NormalGlobalItem>().glowmaskTex = ModContent.GetTexture("AuraClass/Items/Weapons/Auras/OrichalcumAura_Mask");
            item.value = Item.sellPrice(0, 2, 50, 0);
            decayRate = 0.7f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(1191, 12); //Orichalcum Bar
            recipe.AddTile(134); //Mythril Anvil
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}

