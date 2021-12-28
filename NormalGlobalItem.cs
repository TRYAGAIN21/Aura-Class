using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.IO;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Localization;

namespace AuraClass
{
    public class NormalGlobalItem : GlobalItem
    {
        public override bool InstancePerEntity => true;
        public override bool CloneNewInstances => true;

        public Texture2D glowmaskTex = null;
        public Color glowmaskColor = Color.White;

        public float auraDecayMult = 1f;
        public int auraSizeIncrease = 0;

        public override void PostDrawInInventory(Item item, SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            Texture2D tex = glowmaskTex;
            if (tex != null)
            {
                drawColor = glowmaskColor;
                spriteBatch.Draw(tex, position, frame, drawColor, 0f, origin, scale, SpriteEffects.None, 0f);
            }
        }

        public override void PostDrawInWorld(Item item, SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            Texture2D tex = glowmaskTex;
            if (tex != null)
            {
                float x = (float)(item.width / 2f - tex.Width / 2f);
                float y = (float)(item.height - tex.Height);
                lightColor = glowmaskColor;
                alphaColor = lightColor;
                spriteBatch.Draw(tex, new Vector2(item.position.X - Main.screenPosition.X + (float)(tex.Width / 2) + x, item.position.Y - Main.screenPosition.Y + (float)(tex.Height / 2) + y + 2f), new Rectangle?(new Rectangle(0, 0, tex.Width, tex.Height)), lightColor, rotation, new Vector2((float)(tex.Width / 2), (float)(tex.Height / 2)), scale, SpriteEffects.None, 0f);
            }
        }

        public override bool NewPreReforge(Item item)
        {
            auraDecayMult = 1f;
            auraSizeIncrease = 0;
            return base.NewPreReforge(item);
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
		{
			int ttindex = tooltips.FindLastIndex(t => (t.mod == "Terraria" || t.mod == mod.Name) && (t.isModifier || t.Name.StartsWith("Tooltip") || t.Name.Equals("Material")));
			if (ttindex != -1)
			{
                if (auraSizeIncrease > 0)
                {
                    TooltipLine tt2 = new TooltipLine(mod, "PrefixAuraRangeAccessory", $"+{auraSizeIncrease}" + $" {Language.GetTextValue("Mods.AuraClass.Common.Tooltips.IncreasesAuraRange")}")
                    {
                        isModifier = true,
                        isModifierBad = false
                    };
                    tooltips.Insert(++ttindex, tt2); //Make new line
                }

                if (auraDecayMult < 1f)
                {
                    float decayRateTooltip = (1f - auraDecayMult) * 100f;
                    string decayRateTooltipSimplifier = "-" + (Math.Round(decayRateTooltip)).ToString() + "%";

                    TooltipLine tt2 = new TooltipLine(mod, "PrefixDecayRateAccessory", decayRateTooltipSimplifier + $" {Language.GetTextValue("Mods.AuraClass.Common.Tooltips.DecayRate")}")
                    {
                        isModifier = true,
                        isModifierBad = false
                    };
                    tooltips.Insert(++ttindex, tt2); //Make new line
                }
            }
		}

        public override void UpdateAccessory(Item item, Player player, bool hideVisual)
        {
            AuraDamageClass.AuraDamagePlayer modPlayer = AuraDamageClass.AuraDamagePlayer.ModPlayer(player);
            modPlayer.auraSize += auraSizeIncrease;
            modPlayer.auraDecayMult -= (1f - auraDecayMult);
        }
    }
}