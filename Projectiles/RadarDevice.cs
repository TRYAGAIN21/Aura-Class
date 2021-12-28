using AuraClass.AuraDamageClass;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AuraClass.Projectiles
{
	public class RadarDevice : RadarProjectile
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Radar Wave");
		}

		public override void SafeSetDefaults() {
			projectile.width = 16;
			projectile.height = 16;
			projectile.friendly = true;
			projectile.penetrate = -1;
			projectile.ignoreWater = true;
			projectile.alpha = 255;
			projectile.timeLeft = 300;
		}

		public override void SafeAI()
		{
			Player player = Main.player[projectile.owner];

			projectile.spriteDirection = projectile.direction;

			Vector2 AuraPosition = player.Center;

			Vector2 vectorToAuraPosition = AuraPosition - projectile.Center;
			float distanceToAuraPosition = vectorToAuraPosition.Length();

			if (distanceToAuraPosition > 384f)
			{
				projectile.timeLeft = 0;
			}

			for (int num92 = 0; num92 < 5; num92++)
			{
				float num93 = projectile.velocity.X / 3f * (float)num92;
				float num94 = projectile.velocity.Y / 3f * (float)num92;
				int num95 = 4;
				int num96 = Dust.NewDust(new Vector2(projectile.position.X + (float)num95, projectile.position.Y + (float)num95), projectile.width - num95 * 2, projectile.height - num95 * 2, mod.DustType("RadarDevice"), 0f, 0f, 100, default(Color), 1.2f);
				Main.dust[num96].noGravity = true;
				Dust dust15 = Main.dust[num96];
				Dust dust2 = dust15;
				dust2.velocity *= 0.1f;
				dust15 = Main.dust[num96];
				dust2 = dust15;
				dust2.velocity += projectile.velocity * 0.1f;
				Main.dust[num96].position.X -= num93;
				Main.dust[num96].position.Y -= num94;
			}
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

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			int debuffTime = 30;
			target.AddBuff(ModContent.BuffType<Buffs.Tracked>(), debuffTime * 60);
		}
	}
}