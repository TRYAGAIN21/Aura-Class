using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.ModLoader;

namespace AuraClass.Sky
{
    public class ClimaxSky : CustomSky
    {
        private bool isActive = false;
        private bool increase = true;
        private float intensity = 0f;

        public override void Update(GameTime gameTime)
        {
            const float increment = 0.01f;
            if (Main.LocalPlayer.GetModPlayer<AuraDamageClass.AuraDamagePlayer>().ClimaxActive)
            {
                intensity += increment;
                if (intensity > 1f)
                {
                    intensity = 1f;
                }
            }
            else
            {
                intensity -= increment;
                if (intensity < 0f)
                {
                    intensity = 0f;
                    Deactivate();
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth)
        {
            if (maxDepth >= 0 && minDepth < 0)
            {
                Color color = Main.DiscoColor;

                spriteBatch.Draw(ModContent.GetTexture("AuraClass/Sky/ClimaxSky"),
                    new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), color * intensity);

                spriteBatch.Draw(ModContent.GetTexture("AuraClass/Sky/ClimaxSky"),
                   new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), color * intensity);
            }
        }

        public override float GetCloudAlpha()
        {
            return 0.5f - (intensity * 0.5f);
        }

        public override void Activate(Vector2 position, params object[] args)
        {
            isActive = true;
            increase = true;
        }

        public override void Deactivate(params object[] args)
        {
            isActive = false;
            increase = true;
        }

        public override void Reset()
        {
            isActive = false;
            increase = true;
        }

        public override bool IsActive()
        {
            return isActive;
        }

        public override Color OnTileColor(Color inColor)
        {
            return Color.Lerp(Main.DiscoColor, base.OnTileColor(inColor), 1f - intensity);
        }
    }
}