using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AuraClass.Projectiles.FragmentedSlime
{
	public class StardustPortal : ModProjectile
	{
		public override bool Autoload(ref string name)
		{
			return false;
		}

		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Stardust Portal");
			Main.projFrames[projectile.type] = 4;
		}

		private int shootTimer = 30;

		public override void SetDefaults() 
		{
			projectile.width = 20;
			projectile.height = 20;
			projectile.aiStyle = -1;
			projectile.hostile = true;
			projectile.ignoreWater = true;
			projectile.tileCollide = false;
			projectile.timeLeft = 120;
			projectile.penetrate = -1;
		}

        public override void AI()
        {
			if (projectile.timeLeft <= 30)
            {
				projectile.alpha += 255 / 30;
            }

			Player player = Main.player[(int)projectile.ai[0]];
			if (player != null && !player.dead)
            {
				if (projectile.timeLeft == 90 || projectile.timeLeft == 60 || projectile.timeLeft == 30)
				{
					Projectile.NewProjectile(projectile.Center, (player.Center - projectile.Center).SafeNormalize(Vector2.Zero).RotatedByRandom(MathHelper.ToRadians(5f)) * 15f, 435, 25, 0f, Main.myPlayer);
				}
			}

			projectile.frameCounter++;
			if (projectile.frameCounter > 5)
			{
				projectile.frame++;
				if (projectile.frame > Main.projFrames[projectile.type] - 1)
				{
					projectile.frame = 0;
				}
				projectile.frameCounter = 0;
			}
		}

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			Mod mod = ModLoader.GetMod("AuraClass");

			int textureFrames = Main.projFrames[projectile.type];
			Texture2D texture = Main.projectileTexture[projectile.type];
			Rectangle textureRect = new Rectangle(0, projectile.frame * (texture.Height / textureFrames), (texture.Width), (texture.Height / textureFrames));
			Vector2 textureVect = new Vector2((float)texture.Width / 2, (float)(texture.Height / (2 * textureFrames)));

			spriteBatch.Draw(texture, projectile.Center - Main.screenPosition, new Rectangle?(textureRect), Color.White * ((255 - projectile.alpha) / 255f), projectile.rotation, textureVect, projectile.scale, SpriteEffects.None, 0f);
			return false;
		}
	}
}
