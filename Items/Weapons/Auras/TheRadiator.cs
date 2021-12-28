using AuraClass.AuraDamageClass;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AuraClass.Items.Weapons.Auras
{
    public class TheRadiator : AuraItem
    {
        public override void SafeSetDefaults()
        {
            item.damage = 38;
            item.width = 48;
            item.height = 36;
            item.noMelee = true;
            item.useTime = 16;
            item.useAnimation = 16;
            item.autoReuse = true;
            item.channel = true;
            item.useStyle = 4;
            item.value = 0;
            item.rare = 4;
            item.shoot = mod.ProjectileType("TheRadiator");
            item.GetGlobalItem<NormalGlobalItem>().glowmaskTex = ModContent.GetTexture("AuraClass/Items/Weapons/Auras/TheRadiator_Mask");
            item.value = Item.sellPrice(0, 4, 0, 0);
            decayRate = 0.6f;
        }

        public override void PostUpdate()
        {
            Lighting.AddLight((int)((item.position.X + item.width / 2) / 16f), (int)((item.position.Y + item.height / 2) / 16f), 96f / 255f, 248f / 255f, 2f / 255f);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(522, 18); //Cursed Flame
            recipe.AddIngredient(521, 15); //Soul of Night
            recipe.AddTile(134); //Mythril Anvil
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}

