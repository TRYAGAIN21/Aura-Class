using AuraClass.AuraDamageClass;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AuraClass.Projectiles
{
	public class NightsShroudAura : AuraProjectile
	{
		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Night's Aura");
		}

		public override void SafeSetDefaults() {
			projectile.extraUpdates = 0;
			projectile.aiStyle = -1;
			projectile.friendly = true;
			projectile.usesLocalNPCImmunity = true;
			projectile.localNPCHitCooldown = 24;

			dustType = 186;
			auraRange = 18;
		}

		public override bool PreAI()
		{
			if (Main.player[projectile.owner].ownedProjectileCounts[mod.ProjectileType("NightsShroudSmoke")] <= 0)
			{
				Projectile.NewProjectile(Main.player[projectile.owner].Center, Vector2.Zero, mod.ProjectileType("NightsShroudSmoke"), 0, 0f, Main.myPlayer);
			}
			return true;
		}
	}
}
