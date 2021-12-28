using AuraClass.AuraDamageClass;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AuraClass.Items.Accessories
{
	public class MagnifyingGlass : ModItem
	{
		public override void SetStaticDefaults() {
			//Tooltip.SetDefault("Auras will be accompanied by a concentrated beam of light" +
                //"\nThe beam spans the length of your current aura" +
				//"\nWill inflict on fire and is smaller while in daylight but will deal more damage" +
				//"\nDeals reduced damage while in moonlight but will inflict frostburn and is bigger" +
                //"\nDoes not activate while undergound");
		}

		public override void SetDefaults() {
			item.rare = 1;
			item.accessory = true;
			item.width = 36;
			item.height = 36;
			item.value = Item.sellPrice(0, 0, 2, 0);
		}

		public override void UpdateAccessory(Player player, bool hideVisual) {
			AuraDamagePlayer modPlayer = AuraDamagePlayer.ModPlayer(player);
			modPlayer.magnifyingGlass = true;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.IronBar, 5);
			recipe.AddIngredient(38, 8); //Lens
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();

			recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.LeadBar, 5);
			recipe.AddIngredient(38, 8); //Lens
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
