using AuraClass.AuraDamageClass;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AuraClass.Projectiles
{
	public class JunglePetal : AuraProjectile
	{
		public override string Texture => "AuraClass/Projectiles/JungleBlossom";

		public override bool UsesAuraAI() => false;

		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Blossomed Petal");
		}

		public override void SafeSetDefaults() 
		{
			projectile.width = 14;
			projectile.height = 14;
			projectile.aiStyle = -1;
			projectile.friendly = true;
			projectile.ignoreWater = true;
			projectile.tileCollide = false;
			projectile.penetrate = -1;
			projectile.timeLeft = 90;
		}

		public override void SafeAI()
		{
			projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
		}
	}
}
