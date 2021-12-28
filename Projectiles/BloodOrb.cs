using AuraClass.AuraDamageClass;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AuraClass.Projectiles
{
	public class BloodOrb : ModProjectile
	{
		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Blood Orb");
		}

		public override void SetDefaults() 
		{
			projectile.width = 14;
			projectile.height = 14;
			projectile.aiStyle = -1;
			projectile.friendly = true;
			projectile.tileCollide = false;
		}

		public override void AI() 
		{
			Vector2 vectorToAuraPosition = Main.player[projectile.owner].Center - projectile.Center;
			float distanceToAuraPosition = vectorToAuraPosition.Length();

			if (distanceToAuraPosition > 192f)
			{
				projectile.timeLeft = 0;
			}

			projectile.rotation += 0.1f;
		}
	}
}
