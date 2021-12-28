using AuraClass.AuraDamageClass;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AuraClass.Projectiles
{
	public class ChlorophyteSpore : AuraProjectile
	{
		public override bool UsesAuraAI() => false;

		public override string Texture => "Terraria/Projectile_228";

		public override void SetStaticDefaults() 
		{
			Main.projFrames[projectile.type] = 5;
			DisplayName.SetDefault("Chlorophyte Spore");
		}

		private const int projectileAuraRange = 50;

		public override void SafeSetDefaults() 
		{
			projectile.width = 30; 
			projectile.height = 30;
			projectile.aiStyle = -1;
			projectile.friendly = true;
			projectile.tileCollide = false;

			projectile.penetrate = -1;
		}

		public override void SafeAI()
		{
			int RealRangeNormal = projectileAuraRange * 16;
			int RealRangePrefix = auraRangePrefix * 16;
			int RealRange = RealRangeNormal + RealRangePrefix;

			int RealPlayerRange = AuraDamagePlayer.ModPlayer(Main.player[projectile.owner]).auraSize * 16;

			Vector2 vectorToAuraPosition = projectile.Center - Main.player[projectile.owner].Center;
			float distanceToAuraPosition = vectorToAuraPosition.Length();

			if (distanceToAuraPosition > (RealRange + RealPlayerRange) / 2)
			{
				projectile.velocity *= 0.94f;
				projectile.alpha += 10;
				if (projectile.alpha > 255)
				{
					projectile.Kill();
				}
			}


			projectile.frameCounter++;
			if (projectile.frameCounter > 5)
			{
				projectile.frame++;
				if (projectile.frame > 4)
				{
					projectile.frame = 0;
				}
				projectile.frameCounter = 0;
			}
		}
	}
}
