using AuraClass.AuraDamageClass;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AuraClass.Items.Weapons.Auras
{
    public class PalladiumAura : AuraItem
    {
        public override void SetStaticDefaults()
        {
            //DisplayName.SetDefault("Palladium Aura");
            //Tooltip.SetDefault("Players and friendly npcs inside this aura will have increased life regen" +
            //    "\nThis effect always applies to the player wielding the aura");
        }

        public override void SafeSetDefaults()
        {
            item.damage = 43;
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
            item.shoot = mod.ProjectileType("PalladiumAura");
            item.GetGlobalItem<NormalGlobalItem>().glowmaskTex = ModContent.GetTexture("AuraClass/Items/Weapons/Auras/PalladiumAura_Mask");
            item.value = Item.sellPrice(0, 1, 16, 0);
            decayRate = 1f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(1184, 12); //Palladium Bar
            recipe.AddTile(16); //Anvil
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}

