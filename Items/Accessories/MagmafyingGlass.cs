using AuraClass.AuraDamageClass;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AuraClass.Items.Accessories
{
    public class MagmafyingGlass : ModItem
    {
        public override void SetStaticDefaults()
        {
            //Tooltip.SetDefault("Auras will be accompanied by a concentrated beam of light" +
                //"\nThe beam spans 50% longer than the length of your current aura" +
                //"\nDoes not activate while underground" +
                //"\nCan activate while in the underworld" +
                //"\nThis beam will slowly aim towards the cursor" +
                //"\nInflicts hellfire"); Will be added in 1.4 since hellfire is a 1.4 debuff
                //"\nInflicts on fire");
        }

        public override void SetDefaults()
        {
            item.rare = 3;
            item.accessory = true;
            item.width = 40;
            item.height = 40;
            item.value = 20000;
            item.value = Item.sellPrice(0, 0, 75, 0);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            AuraDamagePlayer modPlayer = AuraDamagePlayer.ModPlayer(player);
            modPlayer.magmafyingGlass = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<Items.Accessories.MagnifyingGlass>());
            recipe.AddIngredient(175, 15); //Hellstone Bar
            recipe.AddTile(77); //Hellforge
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
