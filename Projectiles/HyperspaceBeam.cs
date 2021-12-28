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
	public class HyperspaceBeam : AuraProjectile
	{
		public override bool UsesAuraAI() => false;

		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Hyperspace Beam");
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 20;
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;
		}

		private const int projectileAuraRange = 75;

		public override void SafeSetDefaults() 
		{
			projectile.width = 10;
			projectile.height = 10;
			projectile.aiStyle = -1;
			projectile.friendly = true;
			projectile.ignoreWater = true;
			projectile.alpha = 255;
			projectile.usesLocalNPCImmunity = true;
			projectile.localNPCHitCooldown = -1;

			projectile.penetrate = -1;
		}

		private bool homing;
		private int homeTimer = 30;
		public override void SafeAI()
		{
			homing = true;
			if (homeTimer > 0 && projectile.ai[1] != 0f)
            {
				homing = false;
				homeTimer--;
			}

			int RealRangeNormal = projectileAuraRange * 16;
			int RealRangePrefix = auraRangePrefix * 16;
			int RealRange = RealRangeNormal + RealRangePrefix;

			int RealPlayerRange = AuraDamagePlayer.ModPlayer(Main.player[projectile.owner]).auraSize * 16;

			Vector2 vectorToAuraPosition = projectile.Center - Main.player[projectile.owner].Center;
			float distanceToAuraPosition = vectorToAuraPosition.Length();

			if (distanceToAuraPosition > (RealRange + RealPlayerRange) / 2)
			{
				projectile.Kill();
			}

			projectile.rotation = projectile.velocity.ToRotation();

			if (!homing)
            {
				return;
            }

			NPC target = null;
			if (Main.npc[(int)projectile.ai[0]] != null)
            {
				target = Main.npc[(int)projectile.ai[0]];
			}
			if (Main.npc[(int)projectile.ai[0]] == null || !Main.npc[(int)projectile.ai[0]].active || !Main.npc[(int)projectile.ai[0]].CanBeChasedBy(projectile))
            {
				for (int k = 0; k < Main.maxNPCs; k++)
				{
					NPC npc = Main.npc[k];

					if (npc.CanBeChasedBy(projectile) && Collision.CanHitLine(projectile.Center, projectile.width, projectile.height, npc.Center, 1, 1))
					{
						float distance2TargetPosition = (npc.Center - Main.player[projectile.owner].Center).Length();

						if (distance2TargetPosition <= (RealRange + RealPlayerRange) / 2)
						{
							target = npc;
						}
					}
				}
			}

			if (target != null && target.active && target.CanBeChasedBy(projectile) && (target.Center - Main.player[projectile.owner].Center).Length() <= ((RealRange + RealPlayerRange) / 2))
			{
				Vector2 goalVelocity = (target.Center - projectile.Center).SafeNormalize(Vector2.Zero) * 16f;
				projectile.velocity += (goalVelocity - projectile.velocity) / 10f;
			}
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			Mod mod = ModLoader.GetMod("AuraClass");

			Texture2D texture = mod.GetTexture("Projectiles/HyperspaceBeam_End");
			Texture2D startTexture = mod.GetTexture("Projectiles/HyperspaceBeam_Start");
			Texture2D trailTexture = mod.GetTexture("Projectiles/HyperspaceBeam");

			for (int k = 1; k < projectile.oldPos.Length / (projectile.ai[1] == 3f ? 8 : (projectile.ai[1] == 2f ? 4 : (projectile.ai[1] == 1f ? 2 : 1))); k++)
			{
				float rotation = (projectile.oldPos[k - 1] - projectile.oldPos[k]).ToRotation();

				Vector2 drawOrigin = new Vector2(trailTexture.Width * 0.5f, trailTexture.Height * 0.5f);
				Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
				spriteBatch.Draw(k >= ((projectile.oldPos.Length / (projectile.ai[1] == 2f ? 4 : (projectile.ai[1] == 1f ? 2 : 1))) + 1) ? startTexture : (k == 1 ? texture : trailTexture), drawPos, null, Color.White, k == 1 ? projectile.rotation : rotation, trailTexture.Size() * 0.5f, projectile.scale, SpriteEffects.None, 0f);
			}
			return false;
		}

		public override void Kill(int timeLeft)
		{
			for (int k = 0; k < projectile.oldPos.Length / (projectile.ai[1] == 3f ? 8 : (projectile.ai[1] == 2f ? 4 : (projectile.ai[1] == 1f ? 2 : 1))); k++)
			{
				int num = 2;
				for (int index1 = 0; index1 < num; ++index1)
				{
					Vector2 position = projectile.oldPos[k] - (projectile.velocity * index1 / 2);
					int index2 = Dust.NewDust(position, 6, 6, ModContent.DustType<Dusts.DarkEnergyDust>(), 0.0f, 0.0f, 100, default(Color), 2.1f);
					Dust dust = Main.dust[index2];
					dust.fadeIn = 0.2f;
					dust.scale *= 0.66f;
					dust.velocity *= 0f;
					Main.dust[index2].noGravity = true;
				}
			}
		}

		public override bool? SafeCanHitNPC(NPC target)
        {
			if (!homing)
            {
				return false;
            }
			return null;
        }

		public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
			if (!(projectile.ai[1] >= 3f))
            {
				for (int k = 0; k < 2; k++)
				{
					Vector2 velocity = projectile.velocity.RotatedBy(MathHelper.ToRadians(k == 0 ? -45f : 45f));
					velocity.Normalize();
					Projectile.NewProjectile(projectile.Center, velocity * 5f, projectile.type, projectile.damage, 0f, Main.myPlayer, target.whoAmI, projectile.ai[1] + 1);
				}
			}
			projectile.Kill();
		}
	}
}
