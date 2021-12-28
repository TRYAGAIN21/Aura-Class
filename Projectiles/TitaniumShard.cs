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
	public class TitaniumShard : AuraProjectile
	{
		public override bool UsesAuraAI() => false;

		public override void SetStaticDefaults() 
		{
			Main.projFrames[projectile.type] = 4;
			DisplayName.SetDefault("Titanium Shard");
		}

		public override void SafeSetDefaults() 
		{
			projectile.width = 14;
			projectile.height = 14;
			projectile.aiStyle = -1;
			projectile.friendly = true;
			projectile.alpha = 125;
			projectile.timeLeft = 600;
			projectile.tileCollide = false;

			projectile.penetrate = -1;
		}

		private const int projectileAuraRange = 38;

		private int ShardCount;
		private int chaseTimer = 15;
		private float rotation;
		private bool firstTick = true;
		public override void SafeAI()
		{
			Player player = Main.player[projectile.owner];

			int RealRangeNormal = projectileAuraRange * 16;
			int RealRangePrefix = auraRangePrefix * 16;
			int RealRange = RealRangeNormal + RealRangePrefix;
			int RealPlayerRange = AuraDamagePlayer.ModPlayer(player).auraSize * 16;

			int TrueRange = (RealRange + RealPlayerRange) / 2;

			if (firstTick)
			{
				ShardCount = player.ownedProjectileCounts[mod.ProjectileType("TitaniumShard")];
				projectile.frame = Main.rand.Next(4);
				firstTick = false;
			}

			NPC target = null;
			if (projectile.timeLeft <= (600 / 4))
			{
				for (int k = 0; k < Main.maxNPCs; k++)
				{
					NPC npc = Main.npc[k];

					if (npc.CanBeChasedBy(projectile) && Collision.CanHitLine(projectile.Center, projectile.width, projectile.height, npc.Center, 1, 1))
					{
						float distance2TargetPosition = (npc.Center - Main.player[projectile.owner].Center).Length();

						if (distance2TargetPosition <= TrueRange)
						{
							target = npc;
						}
					}
				}
			}
			else
            {
				rotation = (projectile.Center - player.Center).ToRotation() - MathHelper.ToRadians(90f);

				projectile.rotation += 0.2f;
				float spinSpeed = 50f;
				Vector2 spinningpoint = new Vector2(0f, 0f - spinSpeed).RotatedBy(MathHelper.ToRadians((projectile.ai[0] + 1f) * (360f / 5f)));

				Vector2 position9 = spinningpoint.RotatedBy(projectile.rotation) + player.Center;
				projectile.position = position9;
			}

			if (projectile.timeLeft <= (600 / 4))
            {
				float num199 = 3f;
				for (int num200 = 0; (float)num200 < num199; num200++)
				{
					int num201 = Dust.NewDust(projectile.position, 1, 1, 264);
					Main.dust[num201].position = projectile.Center - projectile.velocity / num199 * num200;
					Main.dust[num201].velocity *= 0f;
					Main.dust[num201].noGravity = true;
					Main.dust[num201].alpha = 200;
					Main.dust[num201].scale = 0.5f;
					Main.dust[num201].color = Color.White;
				}

				projectile.penetrate = 1;
				rotation = projectile.velocity.ToRotation() - MathHelper.ToRadians(90f);
				projectile.tileCollide = true;

				chaseTimer--;
				if (target != null && chaseTimer <= 0)
				{
					if (target.active && target.CanBeChasedBy(projectile))
					{
						Vector2 goalVelocity = (target.Center - projectile.Center).SafeNormalize(Vector2.Zero) * 16f;
						projectile.velocity += (goalVelocity - projectile.velocity) / 10f;
					}
				}
				else
				{
					Vector2 velocity = projectile.Center - player.Center;
					velocity.Normalize();
					velocity *= 10f;

					projectile.velocity = velocity;
				}
				if (chaseTimer <= 0)
                {
					chaseTimer = 0;
				}
			}
		}

		public override void Kill(int timeLeft)
		{
			for (int i = 0; i < 5; i++)
			{
				Dust dust = Main.dust[Dust.NewDust(projectile.Center, 0, 0, 264, 0, 0, 100, Color.White, 1f)];
			}
			for (int i = 0; i < 10; i++)
			{
				Dust dust = Main.dust[Dust.NewDust(projectile.Center, 0, 0, 146, 0, 0, 100, AuraClass.ShardColor(4), 1f)];
				dust.alpha = projectile.alpha;
			}
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

			spriteBatch.Draw(texture, texturePos - Main.screenPosition, new Rectangle?(textureRect), AuraClass.ShardColor(4) * ((255 - projectile.alpha) / 255f), rotation, textureVect, 1f, projectile.spriteDirection == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0f);
			return false;
		}
	}
}
