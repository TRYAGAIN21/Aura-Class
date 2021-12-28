using Terraria;
using AuraClass.AuraDamageClass;
using Terraria.ID;
using Terraria.ModLoader;

namespace AuraClass.Items.Armor
{
	[AutoloadEquip(EquipType.Body)]
	public class GlowShirt : ModItem
	{
		public override void SetStaticDefaults() {
			//DisplayName.SetDefault("Glow Shirt");
			//Tooltip.SetDefault("");
		}

		public override void SetDefaults() {
			item.width = 18;
			item.height = 18;
			item.value = 10000;
			item.rare = 1;
			item.defense = 7;
			item.GetGlobalItem<NormalGlobalItem>().glowmaskTex = ModContent.GetTexture("AuraClass/Items/Armor/GlowShirt_Mask");
			item.value = Item.sellPrice(0, 0, 70, 0);
		}

		public override void UpdateEquip(Player player)
		{
			AuraDamagePlayer modPlayer = AuraDamagePlayer.ModPlayer(player);
			modPlayer.auraDamageMult += 0.07f;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.GlowingMushroom, 50);
			recipe.AddIngredient(ModContent.ItemType<Items.MushroomPowder>(), 5);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}