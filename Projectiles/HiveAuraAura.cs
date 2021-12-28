using AuraClass.AuraDamageClass;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AuraClass.Projectiles
{
	public class HiveAuraAura : AuraProjectile
	{
		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Hive Aura");
		}

		private int beeTimer;

		public override void SafeSetDefaults() 
		{
			projectile.extraUpdates = 0;
			projectile.aiStyle = -1;
			projectile.friendly = true;
			projectile.usesLocalNPCImmunity = true;
			projectile.localNPCHitCooldown = 27;

			dustType = 152;
			auraRange = 36;
		}

		public override bool PreAI() 
		{
			beeTimer++;
			if (beeTimer > 100)
            {
				beeTimer = 0;
            }

			if (Main.player[projectile.owner].ownedProjectileCounts[mod.ProjectileType("HiveBee")] < 15 && beeTimer > 30 && (!Main.player[projectile.owner].wet || Main.player[projectile.owner].honeyWet))
            {
				for (int r = 0; r < Main.rand.Next(2, 3); ++r)
                {
					Projectile.NewProjectile(Main.player[projectile.owner].position, new Vector2(Main.rand.NextFloat(7.5f, -7.5f), Main.rand.NextFloat(7.5f, -7.5f)), mod.ProjectileType("HiveBee"), projectile.damage, 0f, projectile.owner, 0f, 0f);
				}
				beeTimer = 0;
			}

			return true;
		}
	}
}
