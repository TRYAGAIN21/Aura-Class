using AuraClass.AuraDamageClass;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AuraClass.Projectiles
{
	public class ToxifyingGhostAura : AuraProjectile
	{
		public override bool UsesAuraAI() => false;

		public override string Texture => "AuraClass/Projectiles/ToxifyingGhost";

		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Lil' Mist Carrier");
			Main.projFrames[projectile.type] = 4;
		}

		private const int BaseAuraRange = 24;

		private int num;
		private int num2;

		private float auraRotation;

		public override void SafeSetDefaults() 
		{
			projectile.width = 10 * 16;
			projectile.height = 10 * 16;
			projectile.aiStyle = -1;
			projectile.friendly = true;
			projectile.penetrate = -1;
			projectile.timeLeft = 600;
			projectile.alpha = 125;
			projectile.tileCollide = false;
		}

		private Vector2 position;
		private bool outsideAuraTick;

		public override bool PreAI() 
		{
			auraRotation += 0.02f;

			Player player = Main.player[projectile.owner];

			if (projectile.ai[0] == 0f)
            {
				position = player.Center;
				projectile.ai[0] = 1f;
			}

			int RealRangeNormal = BaseAuraRange * 16;
			int RealRangePrefix = auraRangePrefix * 16;
			int RealRange = RealRangeNormal + RealRangePrefix;

			int RealPlayerRange = AuraDamagePlayer.ModPlayer(Main.player[projectile.owner]).auraSize * 16;

			bool target = false;
			Vector2 targetCenter = projectile.Center;
			Vector2 targetSize = projectile.Size;
			Vector2 targetPosition = projectile.position;

			if ((projectile.Center - player.Center).Length() > (RealRange + RealPlayerRange) / 2)
            {
				outsideAuraTick = true;
            }

			if (!player.channel)
			{
				projectile.Kill();
			}
			else
            {
				projectile.timeLeft = 2;
            }

			projectile.spriteDirection = -projectile.direction;

			float distanceToTargetPosition = (targetCenter - player.Center).Length();

			if (!target)
            {
				num2++;
				if (num2 > 120)
                {
					position = player.Center + new Vector2(Main.rand.NextFloat(((RealRange + RealPlayerRange) / 2) + ((RealRange + RealPlayerRange) / 4), -(((RealRange + RealPlayerRange) / 2) + ((RealRange + RealPlayerRange) / 4))), Main.rand.NextFloat(((RealRange + RealPlayerRange) / 2) + ((RealRange + RealPlayerRange) / 4), -(((RealRange + RealPlayerRange) / 2) + ((RealRange + RealPlayerRange) / 4))));
					num2 = 0;
                }
            }

			for (int i = 0; i < 20; i++)
			{
				if ((position - player.Center).Length() < (RealRange + RealPlayerRange) / 2 || (position - player.Center).Length() > (((RealRange + RealPlayerRange) / 2) + ((RealRange + RealPlayerRange) / 4)) || ((position - projectile.Center).Length() > 300f && outsideAuraTick))
				{
					for (int r = 0; r < 1000; r++)
					{
						position = player.Center + new Vector2(Main.rand.NextFloat(((RealRange + RealPlayerRange) / 2) + ((RealRange + RealPlayerRange) / 4), -(((RealRange + RealPlayerRange) / 2) + ((RealRange + RealPlayerRange) / 4))), Main.rand.NextFloat(((RealRange + RealPlayerRange) / 2) + ((RealRange + RealPlayerRange) / 4), -(((RealRange + RealPlayerRange) / 2) + ((RealRange + RealPlayerRange) / 4))));
					}
				}
				else
                {
					break;
                }
			}

			Vector2 vectorToIdlePosition = position - projectile.Center;
			float distanceToIdlePosition = vectorToIdlePosition.Length();

			Vector2 vectorToAuraPosition = player.Center - projectile.Center;
			float distanceToAuraPosition = vectorToAuraPosition.Length();

			float speed = 8f;
			float inertia = 20f;

			if (distanceToAuraPosition < (RealRange + RealPlayerRange) / 2 || distanceToAuraPosition > (((RealRange + RealPlayerRange) / 2) + ((RealRange + RealPlayerRange) / 4)))
			{
				speed = 20f;
				inertia = 60f;
			}
			else
			{
				speed = 4f;
				inertia = 80f;
			}

			if (distanceToIdlePosition > 40f)
			{
				vectorToIdlePosition.Normalize();
				vectorToIdlePosition *= speed;
				projectile.velocity = (projectile.velocity * (inertia - 1) + vectorToIdlePosition) / inertia;
			}
			else if (projectile.velocity == Vector2.Zero)
			{
				projectile.velocity.X = -0.15f;
				projectile.velocity.Y = -0.05f;
			}

			projectile.rotation = projectile.velocity.X * 0.05f;

			projectile.frameCounter++;
			if (projectile.frameCounter > 5)
            {
				projectile.frame++;
				if (projectile.frame > 3)
                {
					projectile.frame = 0;
				}
				projectile.frameCounter = 0;
			}
			return true;
		}

		public override void Kill(int timeLeft)
		{
			for (int num421 = 0; num421 < 8; num421++)
			{
				int num422 = Dust.NewDust(new Vector2(projectile.Center.X - (20 / 2), projectile.Center.Y - (26 / 2)), 20, 26, 31, projectile.velocity.X, projectile.velocity.Y, 50);
				Main.dust[num422].noGravity = true;
				Main.dust[num422].scale = 1f;
			}
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			Mod mod = ModLoader.GetMod("AuraClass");

			Texture2D mistTex = mod.GetTexture("Projectiles/ToxifyingGhostAura_Mist");
			Vector2 mistVect = new Vector2((float)mistTex.Width / 2, (float)(mistTex.Height / 2));

			Texture2D auraTex = mod.GetTexture("Projectiles/ToxifyingGhostAura");
			Vector2 auraVect = new Vector2((float)auraTex.Width / 2, (float)(auraTex.Height / 2));

			int textureFrames = Main.projFrames[projectile.type];
			float textureDrawX = (int)projectile.position.X + projectile.width / 2;
			float textureDrawY = (int)((projectile.position.Y + projectile.height / 2) - 1);
			Texture2D texture = mod.GetTexture("Projectiles/ToxifyingGhost");
			Vector2 texturePos = new Vector2(textureDrawX, textureDrawY);
			Rectangle textureRect = new Rectangle(0, projectile.frame * (texture.Height / textureFrames), (texture.Width), (texture.Height / textureFrames));
			Vector2 textureVect = new Vector2((float)texture.Width / 2, (float)(texture.Height / (2 * textureFrames)));

			for (int i = 0; i < 2; i++)
            {
				spriteBatch.Draw(mistTex, texturePos - Main.screenPosition, null, Lighting.GetColor((int)(projectile.Center.X / 16), (int)(projectile.Center.Y / 16)) * ((255 - projectile.alpha) / 255f), i != 0 ? (auraRotation + MathHelper.ToRadians(45f)) : auraRotation, mistVect, 1f, SpriteEffects.None, 0f);
			}
			spriteBatch.Draw(auraTex, texturePos - Main.screenPosition, null, Lighting.GetColor((int)(projectile.Center.X / 16), (int)(projectile.Center.Y / 16)) * ((255 - projectile.alpha) / 255f), auraRotation, auraVect, 1f, SpriteEffects.None, 0f);
			spriteBatch.Draw(texture, texturePos - Main.screenPosition, new Rectangle?(textureRect), Lighting.GetColor((int)(projectile.Center.X / 16), (int)(projectile.Center.Y / 16)) * ((255 - projectile.alpha) / 255f), projectile.rotation, textureVect, 1f, projectile.spriteDirection == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0f);
			return false;
		}
	}
}
