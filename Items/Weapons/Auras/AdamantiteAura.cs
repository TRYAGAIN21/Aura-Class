using AuraClass.AuraDamageClass;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AuraClass.Items.Weapons.Auras
{
    public class AdamantiteAura : AuraItem
    {
        public override void SetStaticDefaults()
        {
            //DisplayName.SetDefault("Adamantite Aura");
            //Tooltip.SetDefault("Players inside this aura will temporarily have increased aura range for 10 seconds" +
            //    "\nThis effect always applies to the player wielding the aura" +
            //    "\nThis aura does not receive this effect");
        }

        public override void SafeSetDefaults()
        {
            item.damage = 48;
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
            item.shoot = mod.ProjectileType("AdamantiteAura");
            item.value = Item.sellPrice(0, 3, 0, 0);
            decayRate = 0.5f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(391, 12); //Adamantite Bar
            recipe.AddTile(134); //Mythril Anvil
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}

