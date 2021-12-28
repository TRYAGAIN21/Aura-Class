using AuraClass.AuraDamageClass;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AuraClass.Items.Weapons.Auras
{
    public class MythrilAura : AuraItem
    {
        public override void SetStaticDefaults()
        {
            //DisplayName.SetDefault("Mythril Aura");
            //Tooltip.SetDefault("Players inside this aura will temporarily have increased aura attack speed for 10 seconds" +
            //    "\nThis effect always applies to the player wielding the aura" +
            //    "\nThis aura does not receive this effect");
        }

        public override void SafeSetDefaults()
        {
            item.damage = 35;
            item.width = 40;
            item.height = 40;
            item.noMelee = true;
            item.useTime = 19;
            item.useAnimation = 19;
            item.autoReuse = true;
            item.channel = true;
            item.useStyle = 4;
            item.value = 0;
            item.rare = 4;
            item.shoot = mod.ProjectileType("MythrilAura");
            item.GetGlobalItem<NormalGlobalItem>().glowmaskTex = ModContent.GetTexture("AuraClass/Items/Weapons/Auras/MythrilAura_Mask");
            item.value = Item.sellPrice(0, 2, 0, 0);
            decayRate = 0.8f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(382, 10); //Mythril Bar
            recipe.AddTile(134); //Mythril Anvil
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}

