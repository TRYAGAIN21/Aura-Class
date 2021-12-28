using AuraClass.AuraDamageClass;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AuraClass.Projectiles
{
	public class OrichalcumPetal : AuraProjectile
	{
		public override bool UsesAuraAI() => false;

		public override void SetStaticDefaults() 
		{
			Main.projFrames[projectile.type] = 5;
			DisplayName.SetDefault("Orichalcum Petal");
		}

		public override void SafeSetDefaults() 
		{
			projectile.width = 14;
			projectile.height = 14;
			projectile.aiStyle = -1;
			projectile.friendly = true;
			projectile.penetrate = -1;
			projectile.extraUpdates = 3;
		}

		private const int projectileAuraRange = 42;

		private bool fly;
		//private bool flyUp;
		public override void SafeAI()
		{
			int RealRangeNormal = projectileAuraRange * 16;
			int RealRangePrefix = auraRangePrefix * 16;
			int RealRange = RealRangeNormal + RealRangePrefix;
			int RealPlayerRange = AuraDamagePlayer.ModPlayer(Main.player[projectile.owner]).auraSize * 16;

			int TrueRange = (RealRange + RealPlayerRange) / 2;

			Vector2 vectorToAuraPosition = projectile.Center - Main.player[projectile.owner].Center;

			if (vectorToAuraPosition.Length() > TrueRange && !fly)
			{
				fly = true;
				//flyUp = (projectile.Center.Y < Main.player[projectile.owner].Center.Y);
			}

			projectile.rotation = projectile.velocity.ToRotation();

			projectile.frameCounter++;
			if (projectile.frameCounter > 5 * (projectile.extraUpdates + 1))
			{
				projectile.frame++;
				if (projectile.frame > 4)
				{
					projectile.frame = 0;
				}
				projectile.frameCounter = 0;
			}

			if (fly)
            {
				//projectile.velocity.Y += flyUp ? -0.2f : 0.2f;
				//projectile.velocity.X *= 0.97f;
				projectile.velocity.Y *= 0.98f;

				if (projectile.velocity.X < 0.1f && projectile.velocity.X > -0.1f)
                {
					projectile.velocity.X = projectile.direction == -1 ? -0.1f : 0.1f;
				}
				projectile.velocity.X *= 1.02f;

				if (projectile.velocity.X > (15f / (projectile.extraUpdates + 1) * 2) || projectile.velocity.X < (-15f / (projectile.extraUpdates + 1) * 2))
                {
					projectile.velocity.X = projectile.velocity.X < 0f ? (-15f / (projectile.extraUpdates + 1) * 2) : (15f / (projectile.extraUpdates + 1) * 2);
				}

				Vector2 vectorToPlayerPosition = projectile.Center - Main.player[projectile.owner].Center;
				if (vectorToPlayerPosition.Length() > (94f * 16f))
                {
					projectile.Kill();
                }
			}
		}

		public override bool? SafeCanHitNPC(NPC target)
        {
			if (fly)
            {
				return false;
            }
			return null;
        }

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			Mod mod = ModLoader.GetMod("AuraClass");

			int textureFrames = Main.projFrames[projectile.type];
			float textureDrawX = (int)projectile.position.X + projectile.width / 2;
			float textureDrawY = (int)((projectile.position.Y + projectile.height / 2) - 1);
			Texture2D texture = Main.projectileTexture[projectile.type];
			Vector2 texturePos = new Vector2(textureDrawX, textureDrawY);
			Rectangle textureRect = new Rectangle(0, projectile.frame * (texture.Height / textureFrames), (texture.Width), (texture.Height / textureFrames));
			Vector2 textureVect = new Vector2((float)texture.Width / 2, (float)(texture.Height / (2 * textureFrames)));

			spriteBatch.Draw(texture, texturePos - Main.screenPosition, new Rectangle?(textureRect), Lighting.GetColor((int)(projectile.Center.X / 16), (int)(projectile.Center.Y / 16)) * ((255 - projectile.alpha) / 255f), projectile.rotation, textureVect, 1f, projectile.spriteDirection == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0f);
			return false;
		}
	}
}
