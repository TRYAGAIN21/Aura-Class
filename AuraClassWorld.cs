using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Generation;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.World.Generation;

namespace AuraClass
{
	public class AuraClassWorld : ModWorld
	{
		// We can use PostWorldGen for world generation tasks that don't need to happen between vanilla world generation steps.
		public override void PostWorldGen()
		{
			// Place some items in Ice Chests
			int[] itemsToPlaceInDungeonChests = { ModContent.ItemType<Items.Weapons.Auras.BlackMagic>() };
			int itemsToPlaceInDungeonChestsChoice = 0;
			for (int chestIndex = 0; chestIndex < 1000; chestIndex++)
			{
				Chest chest = Main.chest[chestIndex];
				// If you look at the sprite for Chests by extracting Tiles_21.xnb, you'll see that the 3rd chest is the Locked Gold Chest. Since we are counting from 0, this is where 2 comes from. 36 comes from the width of each tile including padding. 
				if (chest != null && Main.tile[chest.x, chest.y].type == TileID.Containers && Main.tile[chest.x, chest.y].frameX == 2 * 36)
				{
					for (int inventoryIndex = 0; inventoryIndex < 40; inventoryIndex++)
					{
						if (chest.item[inventoryIndex].type == ItemID.None)
						{
							chest.item[inventoryIndex].SetDefaults(itemsToPlaceInDungeonChests[itemsToPlaceInDungeonChestsChoice]);
							break;
						}
					}
				}
			}
		}
	}
}
