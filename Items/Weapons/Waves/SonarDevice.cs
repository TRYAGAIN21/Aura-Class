using AuraClass.AuraDamageClass;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AuraClass.Items.Weapons.Waves
{
    public class SonarDevice : AuraItem
    {
        public override void SetStaticDefaults()
        {
            //DisplayName.SetDefault("Sonar Device");
            //Tooltip.SetDefault("Enemies hit by this will be tracked and will be dealt more damage if hit" +
            //    "\nThis weapon and the Radar Device does not recieve this damage increase");
            Item.staff[item.type] = true;
        }

        public override void SafeSetDefaults()
        {
            item.damage = 84;
            item.width = 34;
            item.height = 38;
            item.noMelee = true;
            item.useTime = 34;
            item.useAnimation = 34;
            item.autoReuse = true;
            item.useStyle = 5;
            item.value = 0;
            item.rare = 5;
            item.shoot = mod.ProjectileType("SonarDevice");
            item.value = Item.sellPrice(0, 5, 0, 0);
            item.shootSpeed = 10f;

            item.GetGlobalItem<NormalGlobalItem>().glowmaskTex = ModContent.GetTexture("AuraClass/Items/Weapons/Waves/SonarDevice_Mask");
        }

        public override bool CanUseItem(Player player)
        {
            return true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("RadarDevice"));
            recipe.AddIngredient(ItemID.SoulofMight, 15);
            recipe.AddIngredient(ItemID.IronBar, 20);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("RadarDevice"));
            recipe.AddIngredient(ItemID.SoulofMight, 15);
            recipe.AddIngredient(ItemID.LeadBar, 20);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}

