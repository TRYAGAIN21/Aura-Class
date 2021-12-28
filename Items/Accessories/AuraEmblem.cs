using AuraClass.AuraDamageClass;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AuraClass.Items.Accessories
{
	public class AuraEmblem : ModItem
	{
		public override void SetStaticDefaults() {
			//Tooltip.SetDefault("15% increased aura damage");
		}

		public override void SetDefaults() {
			item.rare = 4;
			item.accessory = true;
			item.width = 28;
			item.height = 28;
			item.value = 20000;
			item.value = Item.sellPrice(0, 2, 0, 0);
		}

		public override void UpdateAccessory(Player player, bool hideVisual) {
			AuraDamagePlayer modPlayer = AuraDamagePlayer.ModPlayer(player);
			modPlayer.auraDamageMult *= 1.15f;
		}
	}
}
