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
	[AutoloadEquip(EquipType.Head)]
	public class DarkEnergyHelmet : ModItem
	{
        public override void SetStaticDefaults()
        {
            //DisplayName.SetDefault("Dark Energy Helmet");
            //Tooltip.SetDefault("+12 aura range" +
            //    "\n17% increased aura attack speed");
        }
        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 22;
            item.value = 250;
            item.rare = 10;
            item.defense = 22;
            item.value = Item.sellPrice(0, 7, 0, 0);
        }

        public override void UpdateEquip(Player player)
        {
            AuraDamagePlayer modPlayer = AuraDamagePlayer.ModPlayer(player);
            modPlayer.auraSize += 12;
            modPlayer.auraSpeedMult *= 0.83f;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ItemType<Items.Armor.DarkEnergyBreastplate>() && legs.type == ItemType<Items.Armor.DarkEnergyLeggings>();
        }

        public override void DrawHair(ref bool drawHair, ref bool drawAltHair)
        {
            drawHair = false;
            drawAltHair = false;
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = Language.GetTextValue("Mods.AuraClass.SetBonus.DarkEnergy");

            AuraDamagePlayer modPlayer = AuraDamagePlayer.ModPlayer(player);
            modPlayer.darkEnergySet = true;
        }

        public override void ArmorSetShadows(Player player)
        {
            player.armorEffectDrawShadowLokis = true;
            player.armorEffectDrawOutlines = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<Items.Materials.DarkEnergyFragment>(), 10);
            recipe.AddIngredient(3467, 8); //Luminite Bar
            recipe.AddTile(412); //Ancient Manipulator
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}