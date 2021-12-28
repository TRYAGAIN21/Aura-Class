using AuraClass.AuraDamageClass;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AuraClass.Items.Weapons.Auras
{
    public class CrimsonAura : AuraItem
    {
        public override void SetStaticDefaults()
        {
            //DisplayName.SetDefault("Blood Orb");
            //Tooltip.SetDefault("Sometimes creates flesh balls and sends them towards 5 nearby enemies");
        }

        public override void SafeSetDefaults()
        {
            item.damage = 20;
            item.width = 30;
            item.height = 30;
            item.noMelee = true;
            item.useTime = 24;
            item.useAnimation = 24;
            item.autoReuse = true;
            item.channel = true;
            item.useStyle = 4;
            item.value = 0;
            item.rare = 1;
            item.shoot = mod.ProjectileType("CrimsonAuraAura");
            item.value = Item.sellPrice(0, 0, 30, 0);
            decayRate = 0.2f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.CrimtaneBar, 10);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}

