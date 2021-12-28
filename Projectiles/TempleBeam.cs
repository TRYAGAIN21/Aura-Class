using AuraClass.AuraDamageClass;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AuraClass.Projectiles
{
	public class TempleBeam : AuraProjectile
	{
		public override bool UsesAuraAI() => false;

		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Temple Beam");
		}

		private const int projectileAuraRange = 66;

		public override void SafeSetDefaults() 
		{
			projectile.width = 6;
			projectile.height = 6;
			projectile.aiStyle = -1;
			projectile.friendly = true;

			projectile.penetrate = -1;
		}

		private int target = -1;
		private bool firstTick = true;
		public override void SafeAI()
		{
			if (firstTick)
            {
				Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 91);
				if (projectile.ai[1] != 0f)
                {
					TargetClosest();

					NPC npc = Main.npc[target];
					if (npc != null && npc.active)
                    {
						Vector2 velocity = npc.Center - projectile.Center;
						velocity.Normalize();
						velocity *= 12f;

						projectile.velocity = velocity;
					}
				}

				firstTick = false;
			}

			int RealRangeNormal = projectileAuraRange * 16;
			int RealRangePrefix = auraRangePrefix * 16;
			int RealRange = RealRangeNormal + RealRangePrefix;

			int RealPlayerRange = AuraDamagePlayer.ModPlayer(Main.player[projectile.owner]).auraSize * 16;

			Vector2 vectorToAuraPosition = projectile.Center - Main.player[projectile.owner].Center;
			float distanceToAuraPosition = vectorToAuraPosition.Length();

			if (distanceToAuraPosition > (RealRange + RealPlayerRange))
			{
				projectile.Kill();
			}

			projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
		}

		public override void Kill(int timeLeft)
        {
			int num = 10;
			for (int index1 = 0; index1 < num; ++index1)
			{
				Vector2 position = projectile.Center - (projectile.velocity * index1 / 2);
				int index2 = Dust.NewDust(position, 6, 6, 174, 0.0f, 0.0f, 100, default(Color), 2.1f);
				Dust dust = Main.dust[index2];
				dust.fadeIn = 0.2f;
				dust.scale *= 0.66f;
				dust.velocity *= 0f;
				Main.dust[index2].noGravity = true;
			}
		}

		public void TargetClosest()
		{
			float num = 0f;
			float num2 = 0f;
			bool flag = false;
			int num3 = -1;
			for (int i = 0; i < Main.maxNPCs; i++)
			{
				if (!Main.npc[i].active || !Main.npc[i].CanBeChasedBy(this))
				{
					continue;
				}
				float num4 = Math.Abs(Main.npc[i].position.X + (float)(Main.npc[i].width / 2) - projectile.position.X + (float)(projectile.width / 2)) + Math.Abs(Main.npc[i].position.Y + (float)(Main.npc[i].height / 2) - projectile.position.Y + (float)(projectile.height / 2));
				if (projectile.direction != 0)
				{
					num4 += 1000f;
				}
				if (!flag || num4 < num)
				{
					flag = true;
					num3 = -1;
					num2 = Math.Abs(Main.npc[i].position.X + (float)(Main.npc[i].width / 2) - projectile.position.X + (float)(projectile.width / 2)) + Math.Abs(Main.npc[i].position.Y + (float)(Main.npc[i].height / 2) - projectile.position.Y + (float)(projectile.height / 2));
					num = num4;
					target = i;
				}
			}
		}
	}
}
