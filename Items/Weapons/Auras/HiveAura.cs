using AuraClass.AuraDamageClass;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AuraClass.Items.Weapons.Auras
{
    public class HiveAura : AuraItem
    {
        public override void SetStaticDefaults()
        {
            //DisplayName.SetDefault("The Bee Hive");
            //Tooltip.SetDefault("'Can you bee-leive it?'");
        }

        public override void SafeSetDefaults()
        {
            item.damage = 25;
            item.width = 28;
            item.height = 28;
            item.noMelee = true;
            item.useTime = 27;
            item.useAnimation = 27;
            item.autoReuse = true;
            item.channel = true;
            item.useStyle = 4;
            item.value = 0;
            item.rare = 3;
            item.shoot = mod.ProjectileType("HiveAuraAura");
            item.value = Item.sellPrice(0, 2, 0, 0);
            decayRate = 0.4f;
        }

        public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            if (Main.halloween)
            {
                Texture2D tex = ModContent.GetTexture("AuraClass/Items/Weapons/Auras/HiveAura_Halloween");
                if (tex != null)
                {
                    spriteBatch.Draw(tex, position, frame, drawColor, 0f, origin, scale, SpriteEffects.None, 0f);
                }
                return false;
            }
            return base.PreDrawInInventory(spriteBatch, position, frame, drawColor, itemColor, origin, scale);
        }

        public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
        {
            if (Main.halloween)
            {
                Texture2D tex = ModContent.GetTexture("AuraClass/Items/Weapons/Auras/HiveAura_Halloween");
                if (tex != null)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        if (i == 1)
                        {
                            tex = ModContent.GetTexture("AuraClass/Items/Weapons/Auras/HiveAura_Halloween_Mask");
                            lightColor = Color.White;
                            if (tex == null)
                                break;
                        }

                        float x = (float)(item.width / 2f - tex.Width / 2f);
                        float y = (float)(item.height - tex.Height);
                        spriteBatch.Draw(tex, new Vector2(item.position.X - Main.screenPosition.X + (float)(tex.Width / 2) + x, item.position.Y - Main.screenPosition.Y + (float)(tex.Height / 2) + y + 2f), new Rectangle?(new Rectangle(0, 0, tex.Width, tex.Height)), lightColor, rotation, new Vector2((float)(tex.Width / 2), (float)(tex.Height / 2)), scale, SpriteEffects.None, 0f);
                    }
                }
                return false;
            }
            return base.PreDrawInWorld(spriteBatch, lightColor, alphaColor, ref rotation, ref scale, whoAmI);
        }
    }
}

