using AuraClass.AuraDamageClass;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AuraClass.Items.Weapons.Auras
{
    public class TheClimax : AuraItem
    {
        public override bool Autoload(ref string name)
        {
            return false;
        }

        public override void SafeSetDefaults()
        {
            item.damage = 52;
            item.width = 36;
            item.height = 36;
            item.noMelee = true;
            item.useTime = 24;
            item.useAnimation = 24;
            item.autoReuse = true;
            item.channel = true;
            item.useStyle = 4;
            item.value = 0;
            item.rare = 4;
            item.shoot = mod.ProjectileType("TheClimax");
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<WoodenAura>());
            recipe.AddIngredient(ModContent.ItemType<SandAura>());
            recipe.AddIngredient(ModContent.ItemType<HiveAura>());
            recipe.AddIngredient(ModContent.ItemType<WavesOfFire>());
            recipe.AddIngredient(ModContent.ItemType<JungleBlossom>());
            recipe.AddIngredient(ModContent.ItemType<TemplesGuard>());
            recipe.AddIngredient(ModContent.ItemType<ShrimpyBubble>());
            recipe.AddIngredient(ModContent.ItemType<MoonRock>());
            recipe.AddTile(134); //Mythril Anvil
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}

