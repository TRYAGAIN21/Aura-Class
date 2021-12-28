using Terraria;
using AuraClass.AuraDamageClass;
using Terraria.ModLoader;
using Terraria.ID;

namespace AuraClass.Items.Armor
{
	[AutoloadEquip(EquipType.Legs)]
	public class GlowPants : ModItem
	{
		public override void SetStaticDefaults() {
			//Tooltip.SetDefault("");
		}

		public override void SetDefaults() {
			item.width = 18;
			item.height = 18;
			item.value = 10000;
			item.rare = 1;
			item.defense = 6;
			item.GetGlobalItem<NormalGlobalItem>().glowmaskTex = ModContent.GetTexture("AuraClass/Items/Armor/GlowPants_Mask");
			item.value = Item.sellPrice(0, 0, 60, 0);
		}

		public override void UpdateEquip(Player player)
		{
			AuraDamagePlayer modPlayer = AuraDamagePlayer.ModPlayer(player);
			modPlayer.auraDamageMult += 0.07f;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.GlowingMushroom, 45);
			recipe.AddIngredient(ModContent.ItemType<Items.MushroomPowder>(), 4);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}