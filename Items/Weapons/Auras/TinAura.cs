using AuraClass.AuraDamageClass;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AuraClass.Items.Weapons.Auras
{
    public class TinAura : AuraItem
    {
        public override void SetStaticDefaults()
        {
            //DisplayName.SetDefault("Tin Aura");
            //Tooltip.SetDefault("");
        }

        public override void SafeSetDefaults()
        {
            item.damage = 9;
            item.width = 32;
            item.height = 32;
            item.noMelee = true;
            item.useTime = 25;
            item.useAnimation = 25;
            item.autoReuse = true;
            item.channel = true;
            item.useStyle = 4;
            item.value = 0;
            item.rare = 1;
            item.shoot = mod.ProjectileType("TinAuraAura");
            item.value = Item.sellPrice(0, 0, 1, 50);
            decayRate = 0.65f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.TinBar, 10);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}

