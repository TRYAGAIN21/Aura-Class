using AuraClass.AuraDamageClass;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AuraClass.Items.Weapons.Auras
{
    public class TitaniumAura : AuraItem
    {
        public override void SetStaticDefaults()
        {
            //DisplayName.SetDefault("Titanium Aura");
            //Tooltip.SetDefault("A defensive aura of titanium shards will generate after being hit" +
            //    "\nTitanium shards shatter after 10 seconds" +
            //    "\nTitanium shards home in when they are about to shatter" +
            //    "\nYour immunity frames are increased while using this aura");
        }

        public override void SafeSetDefaults()
        {
            item.damage = 46;
            item.width = 34;
            item.height = 34;
            item.noMelee = true;
            item.useTime = 20;
            item.useAnimation = 20;
            item.autoReuse = true;
            item.channel = true;
            item.useStyle = 4;
            item.value = 0;
            item.rare = 4;
            item.shoot = mod.ProjectileType("TitaniumAura");
            item.GetGlobalItem<NormalGlobalItem>().glowmaskTex = ModContent.GetTexture("AuraClass/Items/Weapons/Auras/TitaniumAura_Mask");
            item.value = Item.sellPrice(0, 3, 50, 0);
            decayRate = 0.75f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(1198, 13); //Titanium Bar
            recipe.AddTile(134); //Mythril Anvil
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}

