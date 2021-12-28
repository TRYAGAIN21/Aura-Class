using AuraClass.AuraDamageClass;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AuraClass.Items.Weapons.Auras
{
    public class TheCorrosion : AuraItem
    {
        public override void SafeSetDefaults()
        {
            item.damage = 52;
            item.width = 36;
            item.height = 36;
            item.noMelee = true;
            item.useTime = 24;
            item.useAnimation = 24;
            item.autoReuse = true;
            item.channel = true;
            item.useStyle = 4;
            item.value = 0;
            item.rare = 4;
            item.shoot = mod.ProjectileType("TheCorrosion");
            item.GetGlobalItem<NormalGlobalItem>().glowmaskTex = ModContent.GetTexture("AuraClass/Items/Weapons/Auras/TheCorrosion_Mask");
            item.value = Item.sellPrice(0, 4, 0, 0);
            decayRate = 0.8f;
        }

        public override void PostUpdate()
        {
            Lighting.AddLight((int)((item.position.X + item.width / 2) / 16f), (int)((item.position.Y + item.height / 2) / 16f), 253f / 255f, 226f / 255f, 9f / 255f);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(1332, 18); //Ichor
            recipe.AddIngredient(521, 15); //Soul of Night
            recipe.AddTile(134); //Mythril Anvil
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}

