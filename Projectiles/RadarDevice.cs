using AuraClass.AuraDamageClass;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AuraClass.Projectiles
{
	public class RadarDevice : WaveProjectile
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Radar");
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;    //The length of old position to be recorded
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;        //The recording mode
		}

		public override void SafeSetDefaults() {
			projectile.width = 20;
			projectile.height = 20;
			projectile.friendly = true;
			projectile.penetrate = -1;
			projectile.ignoreWater = true;
		}

		public int customCounter;

		public override void SafeAI()
        {
			projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);

			Player player = Main.player[projectile.owner];

			projectile.spriteDirection = projectile.direction;

			Vector2 AuraPosition = player.Center;

			Vector2 vectorToAuraPosition = AuraPosition - projectile.Center;
			float distanceToAuraPosition = vectorToAuraPosition.Length();

			if (distanceToAuraPosition > 384f)
			{
				projectile.timeLeft = 0;
			}

			Aura(projectile, 384, mod.DustType("RadarDevice"));
		}

		public override void Kill(int timeLeft)
        {

		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			if (projectile.velocity.X != oldVelocity.X)
			{
				projectile.velocity.X = -oldVelocity.X;
			}
			if (projectile.velocity.Y != oldVelocity.Y)
			{
				projectile.velocity.Y = -oldVelocity.Y;
			}
			return false;
		}
	}
}