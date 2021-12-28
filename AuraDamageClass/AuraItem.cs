using AuraClass;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;
using Terraria.Localization;

namespace AuraClass.AuraDamageClass
{
	public abstract class AuraItem : ModItem
	{
		public override bool CloneNewInstances => true;

		public virtual void SafeSetDefaults() { }

		public float decayRate = 0f;
		public sealed override void SetDefaults() 
		{
			decayRate = 0f;
			item.melee = false;
			item.ranged = false;
			item.magic = false;
			item.thrown = false;
			item.summon = false;
			item.GetGlobalItem<AuraDamageClass.AuraGlobalItem>().aura = true;
			SafeSetDefaults();
		}

		public override void GetWeaponCrit(Player player, ref int crit)
        {
			AuraDamagePlayer modPlayer = AuraDamagePlayer.ModPlayer(player);

			crit = crit + modPlayer.auraCrit;
        }

		public override void ModifyWeaponDamage(Player player, ref float add, ref float mult, ref float flat)
		{
			if (item.type == ModContent.ItemType<Items.Weapons.Auras.DirtAura>())
            {
				int dirt = 0;
				for (int i = 0; i < 58; i++)
				{
					if (player.inventory[i].type == ItemID.DirtBlock && player.inventory[i].stack > 0)
					{
						dirt += player.inventory[i].stack;
					}
				}
				flat = (dirt / 666f);
			}
			else
            {
				add += AuraDamagePlayer.ModPlayer(player).auraDamageAdd;
				mult *= AuraDamagePlayer.ModPlayer(player).auraDamageMult;
			}
		}

		public override void ModifyTooltips(List<TooltipLine> tooltips) 
		{
			if (item.type == ModContent.ItemType<Items.Weapons.Auras.DirtAura>())
            {
				foreach (TooltipLine line in tooltips)
				{
					if (line.mod == "Terraria" && line.Name == "ItemName")
					{
						line.overrideColor = new Color(151, 107, 75);
					}
				}
			}

			TooltipLine tt = tooltips.FirstOrDefault(x => x.Name == "Damage" && x.mod == "Terraria");
			int index = tooltips.FindIndex(x => x.Name == "Damage" && x.mod == "Terraria");
			if (tt != null)
			{
				string[] split = tt.text.Split(' ');
				tt.text = split.First() + $" {Language.GetTextValue("Mods.AuraClass.Tooltip.AuraDamage")} " + split.Last();
			}

			TooltipLine tt3 = tooltips.FirstOrDefault(x => x.Name == "Knockback" && x.mod == "Terraria");
			TooltipLine tt4 = tooltips.FirstOrDefault(x => x.Name == "PrefixKnockback" && x.mod == "Terraria");
			if (tt3 != null)
			{
				float decayRateFinder = (decayRate * AuraDamagePlayer.ModPlayer(Main.player[item.owner]).auraDecayMult * item.GetGlobalItem<AuraDamageClass.AuraGlobalItem>().decayMultPrefix);
				string decayRateTooltip = "No";
				if (decayRateFinder > 0f)
                {
					if (decayRateFinder <= 0.2f)
					{
						decayRateTooltip = "Very Low";
					}
					else if (decayRateFinder <= 0.4f)
                    {
						decayRateTooltip = "Low";
					}
					else if (decayRateFinder <= 0.6f)
					{
						decayRateTooltip = "Average";
					}
					else if (decayRateFinder <= 0.8f)
					{
						decayRateTooltip = "High";
					}
					else if (decayRateFinder < 1f)
					{
						decayRateTooltip = "Very high";
					}
					else
					{
						decayRateTooltip = "Insanely high";
					}
				}
				tt3.text = decayRateTooltip + $" {Language.GetTextValue("Mods.AuraClass.Common.Tooltips.DecayRate")}";
			}
			if (tt4 != null)
            {
				tt4.text = "";
			}

			int ttindex = tooltips.FindLastIndex(t => (t.mod == "Terraria" || t.mod == mod.Name) && (t.isModifier || t.Name.StartsWith("Tooltip") || t.Name.Equals("Material")));
			if (ttindex != -1)
			{
				if (item.GetGlobalItem<AuraDamageClass.AuraGlobalItem>().rangeBoostPrefix != 0)
				{
					TooltipLine tt2 = new TooltipLine(mod, "PrefixAuraRange", (item.GetGlobalItem<AuraDamageClass.AuraGlobalItem>().rangeBoostPrefix < 0 ? "" : "+") + $"{item.GetGlobalItem<AuraDamageClass.AuraGlobalItem>().rangeBoostPrefix}" + $" {Language.GetTextValue("Mods.AuraClass.Common.Tooltips.IncreasesAuraRange")}")
					{
						isModifier = true,
						isModifierBad = item.GetGlobalItem<AuraDamageClass.AuraGlobalItem>().rangeBoostPrefix < 0
					};
					tooltips.Insert(++ttindex, tt2); //Make new line
				}

				if (item.GetGlobalItem<AuraDamageClass.AuraGlobalItem>().decayMultPrefix != 1f)
				{
					float decayRateTooltip = (1f - item.GetGlobalItem<AuraDamageClass.AuraGlobalItem>().decayMultPrefix) * 100f;
					if (decayRateTooltip < 0f)
                    {
						decayRateTooltip *= -1f;
					}
					string decayRateTooltipSimplifier = (Math.Round(decayRateTooltip)).ToString() + "%";

					TooltipLine tt2 = new TooltipLine(mod, "PrefixDecayRate", (item.GetGlobalItem<AuraDamageClass.AuraGlobalItem>().decayMultPrefix > 1f ? "+" : "-") + decayRateTooltipSimplifier + $" {Language.GetTextValue("Mods.AuraClass.Common.Tooltips.DecayRate")}")
					{
						isModifier = true,
						isModifierBad = item.GetGlobalItem<AuraDamageClass.AuraGlobalItem>().decayMultPrefix > 1f
					};
					tooltips.Insert(++ttindex, tt2); //Make new line
				}
			}
		}

		public override bool NewPreReforge()
		{
			item.GetGlobalItem<AuraDamageClass.AuraGlobalItem>().rangeBoostPrefix = 0;
			item.GetGlobalItem<AuraDamageClass.AuraGlobalItem>().decayMultPrefix = 1f;
			return base.NewPreReforge();
		}

		public override int ChoosePrefix(UnifiedRandom rand)
		{
			if (item.maxStack == 1 && item.useStyle > 0)
			{
				return rand.Next(Prefixes.AuraPrefix.AuraPrefixes); // Chooses from 8 different prefixes
			}
			return -1;
		}

		public override bool CanUseItem(Player player)
		{
			if (item.type == ModContent.ItemType<Items.Weapons.Auras.DirtAura>()) // If you are using soil aura
            {
				//dont mind just joost code
				int dirt = 0;
				for (int i = 0; i < 58; i++)
				{
					if (player.inventory[i].type == ItemID.DirtBlock && player.inventory[i].stack > 0)
					{
						dirt += player.inventory[i].stack;
					}
				}
				int amount = (dirt / 666) / 50;
				for (int i = 0; i < 58 && amount > 0; i++)
				{
					if (player.inventory[i].stack > 0 && player.inventory[i].type == ItemID.DirtBlock)
					{
						if (player.inventory[i].stack >= amount)
						{
							player.inventory[i].stack -= amount;
							amount = 0;
						}
						else
						{
							amount -= player.inventory[i].stack;
							player.inventory[i].stack = 0;
						}
						if (player.inventory[i].stack <= 0)
						{
							player.inventory[i].SetDefaults(0, false);
						}
						if (amount <= 0)
						{
							break;
						}
					}
				}
			}
			return player.ownedProjectileCounts[item.shoot] < 1;
		}

		public bool Aura { get; } = true;
	}
}
