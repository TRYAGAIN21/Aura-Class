using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using System;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;
using Terraria.ID;

namespace AuraClass
{
	public class AuraClass : Mod
	{
		static internal AuraClass instance;

		static float[] ColorTimer = new float[2];
		public static Color ShardColor(int transitionSpeed)
		{
			Color redColor = new Color(255, 149, 149);
			Color blueColor = new Color(149, 249, 255);
			ColorTimer[0] += 1f / transitionSpeed;
			if (ColorTimer[0] >= 200)
			{
				ColorTimer[0] = 0;
			}

			if (ColorTimer[0] < 100)
				return Color.Lerp(redColor, blueColor, ColorTimer[0] / 100);
			else
				return Color.Lerp(blueColor, redColor, (ColorTimer[0] - 100) / 100);
		}

		public static Color FragmentationSlimeColor(float transitionSpeed)
		{
			Color solarColor = new Color(254, 105, 47);
			Color vortexColor = new Color(34, 221, 151);
			Color nebulaColor = new Color(254, 126, 229);
			Color stardustColor = new Color(104, 214, 255);
			Color abyssColor = new Color(100, 25, 135);

			ColorTimer[1] += 0.5f * transitionSpeed;
			if (ColorTimer[1] >= 500)
			{
				ColorTimer[1] = 0;
			}

			if (ColorTimer[1] < 100)
				return Color.Lerp(solarColor, vortexColor, ColorTimer[1] / 100);
			else if (ColorTimer[1] < 200)
				return Color.Lerp(vortexColor, nebulaColor, (ColorTimer[1] - 100) / 100);
			else if (ColorTimer[1] < 300)
				return Color.Lerp(nebulaColor, stardustColor, (ColorTimer[1] - 200) / 100);
			else if (ColorTimer[1] < 400)
				return Color.Lerp(stardustColor, abyssColor, (ColorTimer[1] - 300) / 100);
			else
				return Color.Lerp(abyssColor, solarColor, (ColorTimer[1] - 400) / 100);
		}

		public override void Load()
		{
			SkyManager.Instance["AuraClass:Climax"] = new Sky.ClimaxSky();
		}

		public override void AddRecipes()
		{
			if (RecipeGroup.recipeGroupIDs.ContainsKey("Fragment"))
			{
				int index = RecipeGroup.recipeGroupIDs["Fragment"];
				RecipeGroup vanillaGroup = RecipeGroup.recipeGroups[index];
				vanillaGroup.ValidItems.Add(ModContent.ItemType<Items.Materials.DarkEnergyFragment>());
			}

			RecipeGroup group = new RecipeGroup(() => Lang.misc[37] + " Tombstone", 321, 1173, 1174, 1175, 1176, 1177, 3229, 3230, 3231, 3232, 3233);
			RecipeGroup.RegisterGroup("AuraClass:AnyTombstone", group);

			group = new RecipeGroup(() => Lang.misc[37] + " Gold Bar", 19, 706);
			RecipeGroup.RegisterGroup("AuraClass:AnyGoldBar", group);

			ModRecipe recipe = new ModRecipe(this);
			recipe.AddIngredient(ModContent.ItemType<Items.Accessories.AuraEmblem>());
			recipe.AddIngredient(ItemID.SoulofMight, 5);
			recipe.AddIngredient(ItemID.SoulofSight, 5);
			recipe.AddIngredient(ItemID.SoulofFright, 5);
			recipe.AddTile(TileID.TinkerersWorkbench);
			recipe.SetResult(ItemID.AvengerEmblem);
			recipe.AddRecipe();

			recipe = new ModRecipe(this);
			recipe.AddIngredient(3, 20); //Stone
			recipe.AddTile(283); //Heavy Workbench
			recipe.SetResult(321); //Tombstone
			recipe.AddRecipe();

			recipe = new ModRecipe(this);
			recipe.AddIngredient(9, 10); //Wood
			recipe.AddIngredient(3, 10); //Stone
			recipe.AddTile(283); //Heavy Workbench
			recipe.SetResult(1173); //Grave Marker
			recipe.AddRecipe();

			recipe = new ModRecipe(this);
			recipe.AddIngredient(9, 10); //Wood
			recipe.AddIngredient(3, 10); //Stone
			recipe.AddTile(283); //Heavy Workbench
			recipe.SetResult(1174); //Cross Grave Marker
			recipe.AddRecipe();

			recipe = new ModRecipe(this);
			recipe.AddIngredient(3, 20); //Stone
			recipe.AddTile(283); //Heavy Workbench
			recipe.SetResult(1175); //Headstone
			recipe.AddRecipe();

			recipe = new ModRecipe(this);
			recipe.AddIngredient(3, 20); //Stone
			recipe.AddTile(283); //Heavy Workbench
			recipe.SetResult(1176); //Gravestone
			recipe.AddRecipe();

			recipe = new ModRecipe(this);
			recipe.AddIngredient(3, 40); //Stone
			recipe.AddTile(283); //Heavy Workbench
			recipe.SetResult(1177); //Obelisk
			recipe.AddRecipe();

			recipe = new ModRecipe(this);
			recipe.AddRecipeGroup("AuraClass:AnyGoldBar", 8);
			recipe.AddIngredient(321); //Tombstone
			recipe.AddTile(283); //Heavy Workbench
			recipe.SetResult(3230); //Golden Tombstone
			recipe.AddRecipe();

			recipe = new ModRecipe(this);
			recipe.AddRecipeGroup("AuraClass:AnyGoldBar", 8);
			recipe.AddIngredient(1173); //Grave Marker
			recipe.AddTile(283); //Heavy Workbench
			recipe.SetResult(3231); //Golden Grave Marker
			recipe.AddRecipe();

			recipe = new ModRecipe(this);
			recipe.AddRecipeGroup("AuraClass:AnyGoldBar", 8);
			recipe.AddIngredient(1174); //Cross Grave Marker
			recipe.AddTile(283); //Heavy Workbench
			recipe.SetResult(3229); //Golden Cross Grave Marker
			recipe.AddRecipe();

			recipe = new ModRecipe(this);
			recipe.AddRecipeGroup("AuraClass:AnyGoldBar", 8);
			recipe.AddIngredient(1175); //Headstone
			recipe.AddTile(283); //Heavy Workbench
			recipe.SetResult(3233); //Golden Headstone
			recipe.AddRecipe();

			recipe = new ModRecipe(this);
			recipe.AddRecipeGroup("AuraClass:AnyGoldBar", 8);
			recipe.AddIngredient(1176); //Gravestone
			recipe.AddTile(283); //Heavy Workbench
			recipe.SetResult(3232); //Golden Gravestone
			recipe.AddRecipe();

			RecipeFinder finder = new RecipeFinder();
			finder.AddIngredient(3456); //Vortex Fragment
			finder.AddIngredient(3457); //Nebula Fragment
			finder.AddIngredient(3459); //Stardust Fragment
			finder.AddTile(412); //Ancient Manipulator
			finder.SetResult(3458); //Solar Fragment
			Recipe recipe2 = finder.FindExactRecipe();
			if (recipe2 != null)
			{
				RecipeEditor editor = new RecipeEditor(recipe2);
				editor.AddIngredient(ModContent.ItemType<Items.Materials.DarkEnergyFragment>());
			}

			finder = new RecipeFinder();
			finder.AddIngredient(3458); //Solar Fragment
			finder.AddIngredient(3457); //Nebula Fragment
			finder.AddIngredient(3459); //Stardust Fragment
			finder.AddTile(412); //Ancient Manipulator
			finder.SetResult(3456); //Vortex Fragment
			recipe2 = finder.FindExactRecipe();
			if (recipe2 != null)
			{
				RecipeEditor editor = new RecipeEditor(recipe2);
				editor.AddIngredient(ModContent.ItemType<Items.Materials.DarkEnergyFragment>());
			}

			finder = new RecipeFinder();
			finder.AddIngredient(3458); //Solar Fragment
			finder.AddIngredient(3456); //Vortex Fragment
			finder.AddIngredient(3459); //Stardust Fragment
			finder.AddTile(412); //Ancient Manipulator
			finder.SetResult(3457); //Nebula Fragment
			recipe2 = finder.FindExactRecipe();
			if (recipe2 != null)
			{
				RecipeEditor editor = new RecipeEditor(recipe2);
				editor.AddIngredient(ModContent.ItemType<Items.Materials.DarkEnergyFragment>());
			}

			finder = new RecipeFinder();
			finder.AddIngredient(3458); //Solar Fragment
			finder.AddIngredient(3456); //Vortex Fragment
			finder.AddIngredient(3457); //Nebula Fragment
			finder.AddTile(412); //Ancient Manipulator
			finder.SetResult(3459); //Stardust Fragment
			recipe2 = finder.FindExactRecipe();
			if (recipe2 != null)
			{
				RecipeEditor editor = new RecipeEditor(recipe2);
				editor.AddIngredient(ModContent.ItemType<Items.Materials.DarkEnergyFragment>());
			}

			finder = new RecipeFinder();
			finder.AddIngredient(3458, 6); //Solar Fragment
			finder.AddIngredient(3456, 6); //Vortex Fragment
			finder.AddIngredient(3457, 6); //Nebula Fragment
			finder.AddIngredient(3459, 6); //Stardust Fragment
			finder.AddTile(412); //Ancient Manipulator
			finder.SetResult(3572); //Lunar Hook
			recipe2 = finder.FindExactRecipe();
			if (recipe2 != null)
			{
				RecipeEditor editor = new RecipeEditor(recipe2);
				editor.AddIngredient(ModContent.ItemType<Items.Materials.DarkEnergyFragment>(), 6);
			}

			finder = new RecipeFinder();
			finder.AddIngredient(3458, 20); //Solar Fragment
			finder.AddIngredient(3456, 20); //Vortex Fragment
			finder.AddIngredient(3457, 20); //Nebula Fragment
			finder.AddIngredient(3459, 20); //Stardust Fragment
			finder.AddTile(412); //Ancient Manipulator
			finder.SetResult(3601); //Celestial Sigil
			recipe2 = finder.FindExactRecipe();
			if (recipe2 != null)
			{
				RecipeEditor editor = new RecipeEditor(recipe2);
				editor.AddIngredient(ModContent.ItemType<Items.Materials.DarkEnergyFragment>(), 20);
			}

			finder = new RecipeFinder();
			finder.AddIngredient(499, 4); //Greater Healing Potion
			finder.AddIngredient(3458); //Solar Fragment
			finder.AddIngredient(3456); //Vortex Fragment
			finder.AddIngredient(3457); //Nebula Fragment
			finder.AddIngredient(3459); //Stardust Fragment
			finder.AddTile(13); //Bottle
			finder.SetResult(3544, 4); //Super Healing Potion
			recipe2 = finder.FindExactRecipe();
			if (recipe2 != null)
			{
				RecipeEditor editor = new RecipeEditor(recipe2);
				editor.AddIngredient(ModContent.ItemType<Items.Materials.DarkEnergyFragment>());
			}

			recipe = new ModRecipe(this);
			recipe.AddIngredient(3458); //Solar Fragment
			recipe.AddIngredient(3456); //Vortex Fragment
			recipe.AddIngredient(3457); //Nebula Fragment
			recipe.AddIngredient(3459); //Stardust Fragment
			recipe.AddTile(412); //Ancient Manipulator
			recipe.SetResult(ModContent.ItemType<Items.Materials.DarkEnergyFragment>());
			recipe.AddRecipe();
		}

		public override void PostSetupContent()
		{
			RecipeBrowser_AddToCategory("Auras", "Weapons", "Assets/RecipeBrowser_Auras", (Item item) =>
			{
				return item.GetGlobalItem<AuraDamageClass.AuraGlobalItem>().aura;
			});
		}

		private void RecipeBrowser_AddToCategory(string name, string category, string texture, Predicate<Item> predicate)
		{
			Mod recipeBrowser = ModLoader.GetMod("RecipeBrowser");
			if (recipeBrowser != null && !Main.dedServ)
			{
				recipeBrowser.Call(new object[5]
				{
					"AddItemCategory",
					name,
					category,
					this.GetTexture(texture), // 24x24 icon
					predicate
				});
			}
		}
	}
}