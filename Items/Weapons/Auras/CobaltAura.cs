using AuraClass.AuraDamageClass;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AuraClass.Items.Weapons.Auras
{
    public class CobaltAura : AuraItem
    {
        public override void SetStaticDefaults()
        {
            //DisplayName.SetDefault("Cobalt Aura");
            //Tooltip.SetDefault("Players inside this aura will temporarily have increased aura damage for 10 seconds" +
            //    "\nThis effect always applies to the player wielding the aura" +
            //    "\nThis aura does not receive this effect");
        }

        public override void SafeSetDefaults()
        {
            item.damage = 60;
            item.width = 40;
            item.height = 40;
            item.noMelee = true;
            item.useTime = 30;
            item.useAnimation = 30;
            item.autoReuse = true;
            item.channel = true;
            item.useStyle = 4;
            item.value = 0;
            item.rare = 4;
            item.value = Item.sellPrice(0, 1, 16, 0);
            item.shoot = mod.ProjectileType("CobaltAura");
            decayRate = 0.45f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(381, 10); //Cobalt Bar
            recipe.AddTile(16); //Anvil
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}

