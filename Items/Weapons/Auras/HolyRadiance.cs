using AuraClass.AuraDamageClass;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AuraClass.Items.Weapons.Auras
{
    public class HolyRadiance : AuraItem
    {
        public override void SetStaticDefaults()
        {
            //DisplayName.SetDefault("Holy Radiance");
            //Tooltip.SetDefault("Weak enemies inside the aura will be completely stopped" +
            //    "\nEnemies with more than 1250 Max Life are immune to this effect" +
            //    "\nEnemies with 80% Knockback Resistance or more are also immune to this effect");
        }

        public override void SafeSetDefaults()
        {
            item.damage = 47;
            item.width = 40;
            item.height = 40;
            item.noMelee = true;
            item.useTime = 20;
            item.useAnimation = 20;
            item.autoReuse = true;
            item.channel = true;
            item.useStyle = 4;
            item.value = 0;
            item.rare = 5;
            item.shoot = mod.ProjectileType("HolyRadiance");
            item.GetGlobalItem<NormalGlobalItem>().glowmaskTex = ModContent.GetTexture("AuraClass/Items/Weapons/Auras/HolyRadiance_Mask");
            item.value = Item.sellPrice(0, 5, 0, 0);
            decayRate = 0.1f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(1225, 8); //Hallowed Bar
            recipe.AddIngredient(549, 10); //Soul of Sight
            recipe.AddTile(134); //Mythril Anvil
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}

