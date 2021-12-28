using AuraClass.AuraDamageClass;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AuraClass.Projectiles
{
	public class TempleFireball : AuraProjectile
	{
		public override bool UsesAuraAI() => false;

		public override string Texture => "AuraClass/Assets/BlankTexture";

		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Temple Fireball");
		}

		private const int BaseAuraRange = 44;

		public override void SafeSetDefaults() 
		{
			projectile.width = 16;
			projectile.height = 16;
			projectile.aiStyle = -1;
			projectile.friendly = true;
			projectile.alpha = 255;
		}

		public override void SafeAI()
		{
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

			for (int num92 = 0; num92 < 5; num92++)
			{
				float num93 = projectile.velocity.X / 3f * (float)num92;
				float num94 = projectile.velocity.Y / 3f * (float)num92;
				int num95 = 4;
				int num96 = Dust.NewDust(new Vector2(projectile.position.X + (float)num95, projectile.position.Y + (float)num95), projectile.width - num95 * 2, projectile.height - num95 * 2, 6, 0f, 0f, 100, default(Color), 1.2f);
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

		public override bool? SafeCanHitNPC(NPC target)
		{
			if (target.whoAmI != projectile.ai[0])
			{
				return false;
			}
			return null;
		}

		public override void Kill(int timeLeft)
        {
			Main.PlaySound(SoundID.Item10, projectile.position);
			for (int num583 = 0; num583 < 40; num583++)
			{
				int num584 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 6, (0f - projectile.velocity.X) * 0.2f, (0f - projectile.velocity.Y) * 0.2f, 100, default(Color), 2f);
				Main.dust[num584].noGravity = true;
				Dust dust174 = Main.dust[num584];
				Dust dust2 = dust174;
				dust2.velocity *= 4f;
			}
		}
	}
}
