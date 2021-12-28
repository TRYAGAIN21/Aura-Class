using AuraClass.AuraDamageClass;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AuraClass.Items.Weapons.Auras
{
    public class NightsShroud : AuraItem
    {
        public override void SetStaticDefaults()
        {
            //DisplayName.SetDefault("Night's Shroud");
            //Tooltip.SetDefault("A secondary aura made of smoke will form and confuse enemies that enter it");
        }

        public override void SafeSetDefaults()
        {
            item.damage = 20;
            item.width = 36;
            item.height = 36;
            item.noMelee = true;
            item.useTime = 24;
            item.useAnimation = 24;
            item.autoReuse = true;
            item.channel = true;
            item.useStyle = 4;
            item.value = 0;
            item.rare = 1;
            item.shoot = mod.ProjectileType("NightsShroudAura");
            item.GetGlobalItem<NormalGlobalItem>().glowmaskTex = ModContent.GetTexture("AuraClass/Items/Weapons/Auras/NightsShroud_Mask");
            item.value = Item.sellPrice(0, 0, 30, 0);
            decayRate = 0.2f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.DemoniteBar, 10);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}

