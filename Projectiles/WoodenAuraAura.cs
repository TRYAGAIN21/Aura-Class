using AuraClass.AuraDamageClass;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AuraClass.Projectiles
{
	public class WoodenAuraAura : AuraProjectile
	{
		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Wooden Aura");
		}

		public override void SafeSetDefaults() {
			projectile.extraUpdates = 0;
			projectile.aiStyle = -1;
			projectile.friendly = true;
			projectile.usesLocalNPCImmunity = true;
			projectile.localNPCHitCooldown = 25;

			dustType = 7;
			auraRange = 14;
		}
	}
}
