using AuraClass.AuraDamageClass;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AuraClass.Projectiles
{
	public class WaveOfFire : AuraProjectile
	{
		public override bool UsesAuraAI() => false;

		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Wave of Fire");
		}

		public override void SafeSetDefaults() {
			projectile.Size = new Vector2(1, 1);
			projectile.extraUpdates = 4;
			projectile.aiStyle = -1;
			projectile.friendly = true;
			projectile.penetrate = -1;
			projectile.alpha = 255;
			projectile.usesLocalNPCImmunity = true;
			projectile.localNPCHitCooldown = -1;
		}

		public override bool PreAI()
		{
			Aura(Main.player[projectile.owner].Center, projectile, projectile.width / 2, 6);
			projectile.Size += new Vector2(2, 2);

			projectile.position.X = Main.player[projectile.owner].Center.X - (float)(projectile.width / 2);
			projectile.position.Y = Main.player[projectile.owner].Center.Y - (float)(projectile.height / 2);
			if (projectile.width >= Main.projectile[(int)projectile.ai[1]].width && projectile.height >= Main.projectile[(int)projectile.ai[1]].height)
            {
				projectile.Kill();
            }
			return true;
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			if (Collision.CanHitLine(Main.player[projectile.owner].Center, 1, 1, target.Center, 1, 1))
			{
				target.AddBuff(BuffID.OnFire, 120);
			}
		}
	}
}
