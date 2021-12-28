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
	[AutoloadEquip(EquipType.Legs)]
	public class DarkEnergyLeggings : ModItem
	{
        public override void SetStaticDefaults()
        {
            //DisplayName.SetDefault("Dark Energy Leggings");
            //Tooltip.SetDefault("10% increased movement speed");
        }

        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 22;
            item.value = 250;
            item.rare = 10;
            item.defense = 18;
            item.value = Item.sellPrice(0, 10, 50, 0);
        }

        public override void UpdateEquip(Player player)
        {
            player.moveSpeed += 0.1f;

            AuraDamagePlayer modPlayer = AuraDamagePlayer.ModPlayer(player);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<Items.Materials.DarkEnergyFragment>(), 15);
            recipe.AddIngredient(3467, 12); //Luminite Bar
            recipe.AddTile(412); //Ancient Manipulator
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}