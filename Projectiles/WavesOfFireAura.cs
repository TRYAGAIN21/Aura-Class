using AuraClass.AuraDamageClass;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AuraClass.Projectiles
{
	public class WavesOfFireAura : AuraProjectile
	{
		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Waves of Fire");
		}

		public override void SafeSetDefaults() {
			projectile.extraUpdates = 0;
			projectile.aiStyle = -1;
			projectile.friendly = true;
			projectile.usesLocalNPCImmunity = true;
			projectile.localNPCHitCooldown = 23;

			auraRange = 40;
			dustType = 35;
		}

		private int Counter;
		public override bool PreAI()
		{
			Counter++;
			if (Counter > 180)
			{
				Projectile.NewProjectile(Main.player[projectile.owner].Center, Vector2.Zero, mod.ProjectileType("WaveOfFire"), projectile.damage * 2, 0f, Main.myPlayer, 0, projectile.whoAmI);
				Counter = 0;
			}
			return true;
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			if (Main.rand.Next(3) == 0 && Collision.CanHitLine(Main.player[projectile.owner].Center, 1, 1, target.Center, 1, 1))
            {
				target.AddBuff(BuffID.OnFire, 120);
			}
		}
	}
}
