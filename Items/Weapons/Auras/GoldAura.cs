using AuraClass.AuraDamageClass;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AuraClass.Items.Weapons.Auras
{
    public class GoldAura : AuraItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Gold Aura");
            Tooltip.SetDefault("Generates a Gold Aura that will damage enemies inside it.");
        }

        public override void SafeSetDefaults()
        {
            item.damage = 13;
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
            item.shoot = mod.ProjectileType("GoldAuraAura");
        }

        public override void SafeShoot()
        {
            return;
        }

        public override bool CanUseItem(Player player)
        {
            // Ensures no more than one spear can be thrown out, use this when using autoReuse
            return player.ownedProjectileCounts[item.shoot] < 1;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.GoldBar, 10);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}

