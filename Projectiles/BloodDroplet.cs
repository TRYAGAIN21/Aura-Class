using AuraClass.AuraDamageClass;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AuraClass.Projectiles
{
	public class BloodDroplet : AuraProjectile
	{
		public override bool UsesAuraAI() => false;

		public override void SetStaticDefaults() 
		{
			Main.projFrames[projectile.type] = 13;
			DisplayName.SetDefault("Flesh Ball");
		}

		private const int BaseAuraRange = 24;

		public override void SafeSetDefaults() 
		{
			projectile.width = 14;
			projectile.height = 14;
			projectile.aiStyle = -1;
			projectile.friendly = true;
			projectile.tileCollide = false;
		}

		private bool fleshChoice;

		public override void SafeAI()
		{
			if (!fleshChoice)
			{
				projectile.frame = Main.rand.Next(Main.projFrames[projectile.type]);
				fleshChoice = true;
			}

			if (Main.rand.Next(5) == 0)
			{
				Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 5, 0f, 0f, 100, Color.White, 0.6f);
			}

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

			projectile.rotation += 0.2f * projectile.direction;
		}

		public override void Kill(int timeLeft)
		{
			for (int i = 0; i < 20; i++)
			{
				Dust.NewDust(projectile.Center, 0, 0, 5, 0, 0, 100, Color.White, 1f);
			}
		}

		public override bool? SafeCanHitNPC(NPC target)
		{
			if (target.whoAmI != projectile.ai[0])
			{
				return false;
			}
			return true;
		}
	}
}
