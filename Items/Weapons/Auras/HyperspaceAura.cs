using AuraClass.AuraDamageClass;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AuraClass.Items.Weapons.Auras
{
    public class HyperspaceAura : AuraItem
    {
        public override void SetStaticDefaults()
        {
            //DisplayName.SetDefault("Hyperspace Aura");
            //Tooltip.SetDefault("Creates up to three hyperspace beams that home in on a nearby enemy and then split" +
            //    "\n'Fracture your enemies with energy from a galaxy far, far away'");
        }

        public override void SafeSetDefaults()
        {
            item.damage = 68;
            item.width = 52;
            item.height = 42;
            item.noMelee = true;
            item.useTime = 16;
            item.useAnimation = 16;
            item.autoReuse = true;
            item.channel = true;
            item.useStyle = 4;
            item.value = 0;
            item.rare = 10;
            item.shoot = mod.ProjectileType("HyperspaceAura");
            item.value = Item.sellPrice(0, 10, 0, 0);
            decayRate = 0.4f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<Items.Materials.DarkEnergyFragment>(), 18); //Dark Energy Fragment
            recipe.AddTile(412); //Ancient Manipulator
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}

