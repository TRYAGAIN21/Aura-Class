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
	public class GlowHat : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Glow Hat");
            Tooltip.SetDefault("");
        }
        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 16;
            item.value = 250;
            item.rare = 1;
            item.defense = 7;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ItemType<Items.Armor.GlowShirt>() && legs.type == ItemType<Items.Armor.GlowPants>();
        }

        public override void DrawHair(ref bool drawHair, ref bool drawAltHair)
        {
            drawHair = false;
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "+6 Aura Range";

            AuraDamagePlayer modPlayer = AuraDamagePlayer.ModPlayer(player);
            modPlayer.auraSize += 6;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.GlowingMushroom, 85);
            recipe.AddIngredient(ModContent.ItemType<Items.MushroomPowder>(), 3);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}