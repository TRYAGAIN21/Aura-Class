using AuraClass.AuraDamageClass;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AuraClass.Items.Weapons.Auras
{
    public class WavesOfFire : AuraItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Waves of Fire");
            Tooltip.SetDefault("10% chance to inflict On Fire!");
        }

        public override void SafeSetDefaults()
        {
            item.damage = 38;
            item.width = 32;
            item.height = 32;
            item.noMelee = true;
            item.useTime = 23;
            item.useAnimation = 23;
            item.autoReuse = true;
            item.channel = true;
            item.useStyle = 4;
            item.value = 0;
            item.rare = 3;
            item.shoot = mod.ProjectileType("WavesOfFireAura");
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
            recipe.AddIngredient(ItemID.HellstoneBar, 18);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}

