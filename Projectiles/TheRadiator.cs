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
	public class TheRadiator : AuraProjectile
	{
		public override string Texture => "AuraClass/Assets/BlankTexture";

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Irradiating Aura");
		}

		public override void SafeSetDefaults() {
			projectile.aiStyle = -1;
			projectile.friendly = true;
			projectile.usesLocalNPCImmunity = true;
			projectile.localNPCHitCooldown = 22;

			auraRange = 50;
			dustType = 74;
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			int debuffTime = 15;
			if (!Collision.CanHitLine(Main.player[projectile.owner].Center, 1, 1, target.Center, 1, 1))
			{
				debuffTime /= 3;
			}
			target.AddBuff(ModContent.BuffType<Buffs.Irradiated>(), debuffTime * 60);
		}
	}
}
