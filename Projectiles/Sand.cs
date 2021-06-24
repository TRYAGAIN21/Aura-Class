using AuraClass.AuraDamageClass;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AuraClass.Projectiles
{
	public class Sand : ModProjectile
	{
		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Sand Ball");
		}

		public override void SetDefaults() 
		{
			projectile.width = 14;
			projectile.height = 14;
			projectile.aiStyle = -1;
			projectile.friendly = true;
		}

		public override void AI() 
		{
			Player player = Main.player[projectile.owner];

			projectile.spriteDirection = projectile.direction;

			Vector2 AuraPosition = player.Center;

			Vector2 vectorToAuraPosition = AuraPosition - projectile.Center;
			float distanceToAuraPosition = vectorToAuraPosition.Length();

			if (distanceToAuraPosition > 192f)
			{
				projectile.timeLeft = 0;
			}

			projectile.rotation += 0.3f;
		}
	}
}
