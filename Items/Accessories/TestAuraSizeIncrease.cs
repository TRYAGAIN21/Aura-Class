using AuraClass.AuraDamageClass;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AuraClass.Items.Accessories
{
	public class TestAuraSizeIncrease : ModItem
	{
		public override void SetStaticDefaults() {
			Tooltip.SetDefault("+3 aura range");
		}

		public override void SetDefaults() {
			item.rare = 4;
			item.accessory = true;
			item.width = 28;
			item.height = 28;
			item.value = 20000;
		}

		public override void UpdateAccessory(Player player, bool hideVisual) {
			AuraDamagePlayer modPlayer = AuraDamagePlayer.ModPlayer(player);
			modPlayer.auraSize += 3; // add 20% to the multiplicative bonus
		}
	}
}
