using AuraClass.AuraDamageClass;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AuraClass.Items.Weapons.Auras
{
    public class ToxifyingMist : AuraItem
    {
        public override void SetStaticDefaults()
        {
            //DisplayName.SetDefault("Toxifying Mist");
            //Tooltip.SetDefault("Summons Lil' Apparitions to fight for you" +
            //    "\n5 apparitions will create auras and float around outside the main aura" +
            //    "\n'Guaranteed to scare, or your life back!'");
        }

        public override void SafeSetDefaults()
        {
            item.damage = 27;
            item.width = 40;
            item.height = 40;
            item.noMelee = true;
            item.useTime = 32;
            item.useAnimation = 32;
            item.autoReuse = true;
            item.channel = true;
            item.useStyle = 4;
            item.value = 0;
            item.rare = 5;
            item.shoot = mod.ProjectileType("ToxifyingMist");
            item.value = Item.sellPrice(0, 5, 0, 0);
            decayRate = 0.25f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(1225, 12); //Hallowed Bar
            recipe.AddRecipeGroup("AuraClass:AnyTombstone", 2);
            recipe.AddIngredient(547, 20); //Soul of Fright
            recipe.AddTile(134); //Mythril Anvil
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}

