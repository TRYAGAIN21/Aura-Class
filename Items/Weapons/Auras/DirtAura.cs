using AuraClass.AuraDamageClass;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using System.Linq;

namespace AuraClass.Items.Weapons.Auras
{
    public class DirtAura : AuraItem
    {
        public override bool Autoload(ref string name)
        {
            return ModLoader.GetMod("JoostMod") != null;
        }

        public override void SetStaticDefaults()
        {
            //DisplayName.SetDefault("Soil Aura");
            //Tooltip.SetDefault("Does 1 more damage for every 666 blocks of dirt in your inventory\n" +
            //    "Using this weapon constantly consumes dirt equal to 1/50th of the damage bonus");
        }

        public override void SafeSetDefaults()
        {
            item.damage = 1;
            item.width = 32;
            item.height = 32;
            item.useTime = 15;
            item.useAnimation = 15;
            item.useStyle = 5;
            item.knockBack = 4;
            item.rare = 2;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.noMelee = true;
            item.channel = true;
            item.value = Item.sellPrice(0, 0, 0, 10);
            item.shoot = mod.ProjectileType("DirtAura");
        }

        public override void AddRecipes()
        {
            Mod JoostMod = ModLoader.GetMod("JoostMod");
            if (JoostMod != null)
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(ItemID.DirtBlock, 666);
                recipe.AddTile(TileID.DemonAltar);
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
        }
    }
}

