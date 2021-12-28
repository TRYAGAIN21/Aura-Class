using AuraClass.AuraDamageClass;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AuraClass.Projectiles
{
	public class NightsShroudSmoke : AuraProjectile
	{
		public override bool UsesAuraAI() => false;

		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Smoke Aura");
		}

		public override void SafeSetDefaults() 
		{
			projectile.extraUpdates = 0;
			projectile.aiStyle = -1;
			projectile.friendly = true;
			projectile.penetrate = -1;
			projectile.scale = 1f;
			projectile.alpha = 255;
		}

		private const int BaseAuraRange = 19;

		public override bool PreAI()
		{
			int SecondaryAuraRange = BaseAuraRange + 14;

			int RealRangeNormal = BaseAuraRange * 16;
			int RealSecondaryRangeNormal = SecondaryAuraRange * 16;
			int RealRangePrefix = auraRangePrefix * 16;
			int RealRange = RealRangeNormal + RealRangePrefix;
			int RealSecondaryRange = RealSecondaryRangeNormal + RealRangePrefix;

			int RealPlayerRange = AuraDamagePlayer.ModPlayer(Main.player[projectile.owner]).auraSize * 16;

			projectile.width = RealSecondaryRange + RealPlayerRange;
			projectile.height = RealSecondaryRange + RealPlayerRange;

			projectile.position.X = Main.player[projectile.owner].Center.X - (float)((RealSecondaryRange + RealPlayerRange) / 2);
			projectile.position.Y = Main.player[projectile.owner].Center.Y - (float)((RealSecondaryRange + RealPlayerRange) / 2);

			for (int k = 0; k < Main.maxNPCs; k++)
			{
				NPC npc = Main.npc[k];

				if (npc.active && !npc.dontTakeDamage && !npc.friendly && npc.lifeMax > 5)
				{
					float distance2TargetPosition = (npc.Center - Main.player[projectile.owner].Center).Length();

					if (distance2TargetPosition > (RealRange + RealPlayerRange) / 2 && distance2TargetPosition < (RealSecondaryRange + RealPlayerRange) / 2)
					{
						npc.AddBuff(BuffID.Confused, 120);
					}
				}
			}

			if (!Main.player[projectile.owner].channel)
			{
				projectile.Kill();
			}
			else
			{
				projectile.timeLeft = 2;
			}

			const int baseDistance = 500;
			const int baseMax = 20;

			int dustMax = (int)(((RealRange + RealPlayerRange) / 2) / baseDistance * baseMax);
			if (dustMax < 40) { dustMax = 40; }
			if (dustMax > 40) { dustMax = 40; }

			int dustMax2 = (int)(((RealSecondaryRange + RealPlayerRange) / 2) / baseDistance * baseMax);
			if (dustMax2 < 40) { dustMax2 = 40; }
			if (dustMax2 > 40) { dustMax2 = 40; }

			float dustScale = ((RealRange + RealPlayerRange) / 2) / baseDistance;
			if (dustScale < 0.75f) { dustScale = 0.75f; }
			if (dustScale > 2f) { dustScale = 2f; }

			float dustScale2 = ((RealSecondaryRange + RealPlayerRange) / 2) / baseDistance;
			if (dustScale2 < 0.75f) { dustScale2 = 0.75f; }
			if (dustScale2 > 2f) { dustScale2 = 2f; }

			for (int i = 0; i < dustMax; i++)
			{
				Vector2 spawnPos = projectile.Center + Main.rand.NextVector2CircularEdge((RealRange + RealPlayerRange) / 2, (RealRange + RealPlayerRange) / 2);
				if (Main.player[projectile.owner].Distance(spawnPos) > 1500)
					continue;
				Dust dust = Main.dust[Dust.NewDust(spawnPos, 0, 0, 31, 0, 0, 100, Color.White, dustScale)];
				dust.velocity = Main.player[projectile.owner].velocity;
				if (Main.rand.Next(3) == 0)
				{
					dust.velocity += Vector2.Normalize(projectile.Center - dust.position) * Main.rand.NextFloat(5f) * -1f;
					dust.position += dust.velocity * 5f;
				}
				dust.noGravity = true;
			}
			for (int i = 0; i < dustMax2; i++)
			{
				Vector2 spawnPos = projectile.Center + Main.rand.NextVector2CircularEdge((RealSecondaryRange + RealPlayerRange) / 2, (RealSecondaryRange + RealPlayerRange) / 2);
				if (Main.player[projectile.owner].Distance(spawnPos) > 1500)
					continue;
				Dust dust = Main.dust[Dust.NewDust(spawnPos, 0, 0, 31, 0, 0, 100, Color.White, dustScale2)];
				dust.velocity = Main.player[projectile.owner].velocity;
				if (Main.rand.Next(3) == 0)
				{
					dust.velocity += Vector2.Normalize(projectile.Center - dust.position) * Main.rand.NextFloat(5f);
					dust.position += dust.velocity * 5f;
				}
				dust.noGravity = true;
			}
			return true;
		}
	}
}
