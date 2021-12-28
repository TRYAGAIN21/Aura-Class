using AuraClass.AuraDamageClass;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.GameContent.Dyes;
using Terraria.GameContent.UI;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;
using static Terraria.ModLoader.ModContent;

namespace AuraClass.Items.Armor
{
	[AutoloadEquip(EquipType.Body)]
	public class DarkEnergyBreastplate : ModItem
	{
        public override void SetStaticDefaults()
        {
            //DisplayName.SetDefault("Dark Energy Breastplate");
            //Tooltip.SetDefault("27% increased aura damage");
        }

        public override void SetDefaults()
        {
            item.width = 42;
            item.height = 34;
            item.value = 250;
            item.rare = 10;
            item.defense = 30;
            item.value = Item.sellPrice(0, 14, 0, 0);
        }

        public override void UpdateEquip(Player player)
        {
            AuraDamagePlayer modPlayer = AuraDamagePlayer.ModPlayer(player);
            modPlayer.auraDamageMult *= 1.27f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<Items.Materials.DarkEnergyFragment>(), 20);
            recipe.AddIngredient(3467, 16); //Luminite Bar
            recipe.AddTile(412); //Ancient Manipulator
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}