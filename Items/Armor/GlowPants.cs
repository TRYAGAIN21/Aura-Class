using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace AuraClass.Items.Armor
{
	[AutoloadEquip(EquipType.Legs)]
	public class GlowPants : ModItem
	{
		public override void SetStaticDefaults() {
			Tooltip.SetDefault("");
		}

		public override void SetDefaults() {
			item.width = 18;
			item.height = 18;
			item.value = 10000;
			item.rare = 1;
			item.defense = 7;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.GlowingMushroom, 90);
			recipe.AddIngredient(ModContent.ItemType<Items.MushroomPowder>(), 4);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}