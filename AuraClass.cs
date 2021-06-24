using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AuraClass
{
	public class AuraClass : Mod
	{
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(this);
			recipe.AddIngredient(ModContent.ItemType<Items.Accessories.AuraEmblem>());
			recipe.AddIngredient(ItemID.SoulofMight, 5);
			recipe.AddIngredient(ItemID.SoulofSight, 5);
			recipe.AddIngredient(ItemID.SoulofFright, 5);
			recipe.AddTile(TileID.TinkerersWorkbench);
			recipe.SetResult(ItemID.AvengerEmblem);
			recipe.AddRecipe();
		}
	}
}