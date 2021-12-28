using AuraClass.AuraDamageClass;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AuraClass.Projectiles
{
	public class Sand : AuraProjectile
	{
		public override bool UsesAuraAI() => false;

		public override void SetStaticDefaults() 
		{
			Main.projFrames[projectile.type] = 9;
			DisplayName.SetDefault("Sand Ball");
		}

		private const int BaseAuraRange = 20;

		public override void SafeSetDefaults() 
		{
			projectile.width = 14;
			projectile.height = 14;
			projectile.aiStyle = -1;
			projectile.friendly = true;
		}

		private bool sandChoice;

		public override void SafeAI()
		{
			if (!sandChoice)
			{
				projectile.frame = Main.rand.Next(Main.projFrames[projectile.type]);
				sandChoice = true;
			}

			projectile.spriteDirection = projectile.direction;

			int RealRangeNormal = BaseAuraRange * 16;
			int RealRangePrefix = auraRangePrefix * 16;
			int RealRange = RealRangeNormal + RealRangePrefix;

			int RealPlayerRange = AuraDamagePlayer.ModPlayer(Main.player[projectile.owner]).auraSize * 16;

			Projectile aura = Main.projectile[(int)projectile.ai[1]];
			Vector2 vectorToAuraPosition = projectile.Center - aura.Center;
			float distanceToAuraPosition = vectorToAuraPosition.Length();

			if (distanceToAuraPosition > (RealRange + RealPlayerRange) / 2)
			{
				projectile.Kill();
			}

			projectile.rotation += 0.3f * projectile.direction;
		}

		public override bool? SafeCanHitNPC(NPC target)
		{
			if (target.whoAmI != projectile.ai[0])
			{
				return false;
			}
			return true;
		}

		public override void Kill(int timeLeft)
        {
			if (projectile.frame != 7 - 1 && projectile.frame != 8 - 1)
            {
				for (int i = 0; i < 10; i++)
				{
                    if (projectile.frame != 9 - 1 && projectile.frame != 6 - 1)
                    {
                        Dust.NewDust(projectile.Center, 0, 0, 124, 0, 0, 100, Color.White, 1f);
                    }
					else if (projectile.frame != 5 - 1)
					{
						Dust.NewDust(projectile.Center, 0, 0, 233, 0, 0, 100, Color.White, 1f);
					}
					else
                    {
						Dust.NewDust(projectile.Center, 0, 0, 232, 0, 0, 100, Color.White, 1f);
					}
				}
			}
			else
            {
				for (int i = 0; i < 5; i++)
				{
					Dust.NewDust(projectile.Center, 0, 0, 233, 0, 0, 100, Color.White, 1f);
					Dust.NewDust(projectile.Center, 0, 0, 232, 0, 0, 100, Color.White, 1f);
				}
			}
		}
	}
}
