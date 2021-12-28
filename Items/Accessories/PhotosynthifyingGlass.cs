using AuraClass.AuraDamageClass;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AuraClass.Items.Accessories
{
	public class PhotosythifyingGlass : ModItem
	{
		public override void SetStaticDefaults() {
			//Tooltip.SetDefault("Auras will be accompanied by a concentrated beam of light" +
				//"\nThe beam spans double the length of your current aura" +
				//"\nDoes not activate while undergound unless you are standing in enough light" +
				//"\nCan activate while in the underworld" +
				//"\nThis beam will quickly aim towards the cursor");
		}

		public override void SetDefaults() {
			item.rare = 7;
			item.accessory = true;
			item.width = 36;
			item.height = 36;
			item.value = Item.sellPrice(0, 5, 0, 0);
		}

		public override void UpdateAccessory(Player player, bool hideVisual) {
			AuraDamagePlayer modPlayer = AuraDamagePlayer.ModPlayer(player);
			modPlayer.photosynthifyingGlass = true;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<Items.Accessories.MagmafyingGlass>());
			recipe.AddIngredient(1006, 18); //Chlorophyte Bar
			recipe.AddTile(134); //Mythril Anvil
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
