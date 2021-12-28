using AuraClass.AuraDamageClass;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AuraClass.Projectiles
{
	public class TestAuraAura : AuraProjectile
	{
		public override bool Autoload(ref string name)
		{
			return false;
		}

		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Testing Aura");
		}

		public override void SafeSetDefaults() {
			projectile.extraUpdates = 0;
			projectile.aiStyle = -1;
			projectile.friendly = true;
			projectile.penetrate = -1;
			projectile.scale = 1f;
			projectile.alpha = 255;
			projectile.usesLocalNPCImmunity = true;
			projectile.localNPCHitCooldown = 25;

			dustType = 226;
			auraRange = 50;
		}
	}
}
