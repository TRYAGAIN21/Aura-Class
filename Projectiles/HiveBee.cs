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
	public class HiveBee : AuraProjectile
	{
		public override bool UsesAuraAI() => false;

		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Hive Bee");
			Main.projFrames[projectile.type] = 4;
		}

		private const int BaseAuraRange = 36;

		private int num;
		private int num2;

		public override void SafeSetDefaults() 
		{
			projectile.width = 10;
			projectile.height = 10;
			projectile.aiStyle = -1;
			projectile.friendly = true;
			projectile.penetrate = 3;
			projectile.maxPenetrate = 3;
			projectile.timeLeft = 600;
		}

		private Vector2 position;

		public override bool PreAI() 
		{
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
			projectile.ai[0] += 1f;
			if (projectile.ai[0] > 30f)
			{
				projectile.ai[0] = 30f;
				for (int k = 0; k < Main.maxNPCs; k++)
				{
					NPC npc = Main.npc[k];

					if (npc.CanBeChasedBy(projectile) && (!npc.wet || npc.honeyWet))
					{
						float distance2TargetPosition = (npc.Center - player.Center).Length();

						if (distance2TargetPosition <= (RealRange + RealPlayerRange) / 2)
						{
							target = true;
							targetCenter = npc.Center;
							targetSize = npc.Size;
						}
					}
				}
			}

			if (!player.channel || projectile.wet && !projectile.honeyWet)
			{
				projectile.Kill();
			}
			else
            {
				projectile.timeLeft = 2;
            }

			projectile.spriteDirection = projectile.direction;

			float distanceToTargetPosition = (targetCenter - player.Center).Length();

			if (!target)
            {
				num2++;
				if (num2 > 120)
                {
					position = player.Center + new Vector2(Main.rand.NextFloat((RealRange + RealPlayerRange) / 2, -((RealRange + RealPlayerRange) / 2)), Main.rand.NextFloat((RealRange + RealPlayerRange) / 2, -((RealRange + RealPlayerRange) / 2)));
					num2 = 0;
                }
            }
			
			if ((position - player.Center).Length() > (RealRange + RealPlayerRange) / 2 || !Collision.CanHitLine(projectile.Center, 1, 1, position, 1, 1))
			{
				for (int r = 0; r < 1000; r++)
                {
					if (Collision.CanHitLine(projectile.Center, 1, 1, position, 1, 1) && (position - player.Center).Length() <= (RealRange + RealPlayerRange) / 2)
                    {
						break;
                    }
					position = player.Center + new Vector2(Main.rand.NextFloat((RealRange + RealPlayerRange) / 2, -((RealRange + RealPlayerRange) / 2)), Main.rand.NextFloat((RealRange + RealPlayerRange) / 2, -((RealRange + RealPlayerRange) / 2)));
				}
			}

			Vector2 vectorToIdlePosition = position - projectile.Center;
			float distanceToIdlePosition = vectorToIdlePosition.Length();

			Vector2 vectorToAuraPosition = player.Center - projectile.Center;
			float distanceToAuraPosition = vectorToAuraPosition.Length();

			float speed = 8f;
			float inertia = 20f;

			if (target && distanceToAuraPosition <= (RealRange + RealPlayerRange) / 2 && distanceToTargetPosition <= (RealRange + RealPlayerRange) / 2)
			{
				if (Collision.CanHitLine(projectile.Center, 1, 1, targetCenter, (int)targetSize.X, (int)targetSize.Y))
                {
					float num382 = 6f;
					float num383 = 0.1f;

					Vector2 vector35 = new Vector2(projectile.position.X + (float)projectile.width * 0.5f, projectile.position.Y + (float)projectile.height * 0.5f);
					float num384 = targetCenter.X - vector35.X;
					float num385 = targetCenter.Y - vector35.Y;
					float num386 = (float)Math.Sqrt(num384 * num384 + num385 * num385);
					float num387 = num386;
					num386 = num382 / num386;
					num384 *= num386;
					num385 *= num386;
					if (projectile.velocity.X < num384)
					{
						projectile.velocity.X += num383;
						if (projectile.velocity.X < 0f && num384 > 0f)
						{
							projectile.velocity.X += num383 * 2f;
						}
					}
					else if (projectile.velocity.X > num384)
					{
						projectile.velocity.X -= num383;
						if (projectile.velocity.X > 0f && num384 < 0f)
						{
							projectile.velocity.X -= num383 * 2f;
						}
					}
					if (projectile.velocity.Y < num385)
					{
						projectile.velocity.Y += num383;
						if (projectile.velocity.Y < 0f && num385 > 0f)
						{
							projectile.velocity.Y += num383 * 2f;
						}
					}
					else if (projectile.velocity.Y > num385)
					{
						projectile.velocity.Y -= num383;
						if (projectile.velocity.Y > 0f && num385 < 0f)
						{
							projectile.velocity.Y -= num383 * 2f;
						}
					}
				}
			}
			else
			{
				if (!Collision.CanHitLine(projectile.Center, 1, 1, player.position, player.width, player.height)) { num++; if (num > 180) { projectile.tileCollide = false; } }
                else { num = 0; projectile.tileCollide = true; }

				if (distanceToAuraPosition > (RealRange + RealPlayerRange) / 2)
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
			for (int num421 = 0; num421 < 6; num421++)
			{
				int num422 = Dust.NewDust(projectile.position, projectile.width, projectile.height, Main.halloween ? ModContent.DustType<Dusts.Zombee>() : 150, projectile.velocity.X, projectile.velocity.Y, 50);
				Main.dust[num422].noGravity = true;
				Main.dust[num422].scale = 1f;
			}
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			if (oldVelocity.X != projectile.velocity.X)
			{
				projectile.velocity.X = -oldVelocity.X * 0.2f;
			}
			if (oldVelocity.Y != projectile.velocity.Y)
			{
				projectile.velocity.Y = -oldVelocity.Y * 0.2f;
			}
			return false;
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			Mod mod = ModLoader.GetMod("AuraClass");

			int textureFrames = Main.projFrames[projectile.type];
			Texture2D texture = ModContent.GetTexture("AuraClass/Projectiles/HiveBee");
			Rectangle textureRect = new Rectangle(0, projectile.frame * (texture.Height / textureFrames), (texture.Width), (texture.Height / textureFrames));
			Vector2 textureVect = new Vector2((float)texture.Width / 2, (float)(texture.Height / (2 * textureFrames)));
			for (int i = 0; i < (Main.halloween ? 2 : 1); i++)
			{
				lightColor = Lighting.GetColor((int)(projectile.Center.X / 16), (int)(projectile.Center.Y / 16));
				if (Main.halloween)
				{
					texture = ModContent.GetTexture("AuraClass/Projectiles/HiveBee_Zombee");
					lightColor = Lighting.GetColor((int)(projectile.Center.X / 16), (int)(projectile.Center.Y / 16));
					if (i == 1)
					{
						texture = ModContent.GetTexture("AuraClass/Projectiles/HiveBee_Zombee_Mask");
						lightColor = Color.White;
						if (texture == null)
							break;
					}
					lightColor *= ((255 - projectile.alpha) / 255f);
				}

				spriteBatch.Draw(texture, projectile.Center - Main.screenPosition, new Rectangle?(textureRect), lightColor, projectile.rotation, textureVect, projectile.scale, projectile.spriteDirection == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0f);
			}
			return false;
		}
	}
}
