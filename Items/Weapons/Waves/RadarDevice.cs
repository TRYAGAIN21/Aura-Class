using AuraClass.AuraDamageClass;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework;
using System;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;

namespace AuraClass.Items.Weapons.Waves
{
    public class RadarDevice : AuraItem
    {
        public override void SetStaticDefaults()
        {
            //DisplayName.SetDefault("Radar Device");
            //Tooltip.SetDefault("Enemies hit by this will be tracked and will be dealt more damage if hit" +
            //    "\nThis weapon and the Sonar Device does not recieve this damage increase");
            Item.staff[item.type] = true;
        }

        public override void SafeSetDefaults()
        {
            item.damage = 14;
            item.width = 30;
            item.height = 30;
            item.noMelee = true;
            item.useTime = 30;
            item.useAnimation = 30;
            item.autoReuse = true;
            item.useStyle = 5;
            item.value = 0;
            item.rare = 1;
            item.shoot = mod.ProjectileType("RadarDevice");
            item.shootSpeed = 8f;
            item.value = Item.sellPrice(0, 0, 30, 0);

            item.GetGlobalItem<NormalGlobalItem>().glowmaskTex = ModContent.GetTexture("AuraClass/Items/Weapons/Waves/RadarDevice_Mask");
        }

        public override bool CanUseItem(Player player)
        {
            return true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            //recipe.AddIngredient(ItemID.IronBar, 17);
            recipe.AddIngredient(ItemID.IronBar, 15);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            //recipe.AddIngredient(ItemID.LeadBar, 17);
            recipe.AddIngredient(ItemID.LeadBar, 15);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}

