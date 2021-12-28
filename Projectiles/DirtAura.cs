using AuraClass.AuraDamageClass;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AuraClass.Projectiles
{
	public class DirtAura : AuraProjectile
	{
		public override bool Autoload(ref string name)
		{
			return ModLoader.GetMod("JoostMod") != null;
		}

		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Soil Aura");
		}

		public override void SafeSetDefaults() {
			projectile.extraUpdates = 0;
			projectile.aiStyle = -1;
			projectile.friendly = true;

			dustType = 0;
			auraRange = 18;
		}
	}
}
