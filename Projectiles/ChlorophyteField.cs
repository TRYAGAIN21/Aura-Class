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
	public class ChlorophyteField : AuraProjectile
	{
		public override string Texture => "AuraClass/Assets/BlankTexture";

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Chlorophyte Aura");
		}

		public override void SafeSetDefaults() {
			projectile.aiStyle = -1;
			projectile.friendly = true;
			projectile.usesLocalNPCImmunity = true;
			projectile.localNPCHitCooldown = 22;

			auraRange = 50;
			dustType = 157;
		}

		private int sporeTimer;

		public override bool PreAI()
		{
			sporeTimer++;
			if (sporeTimer > 15)
			{
				for (int k = 0; k < 3; k++)
				{
					Projectile.NewProjectile(Main.player[projectile.owner].Center, new Vector2(0f, Main.rand.NextFloat(7.5f, 12.5f)).RotatedByRandom(MathHelper.ToRadians(360f)), mod.ProjectileType("ChlorophyteSpore"), (int)(projectile.damage * 0.5f), 0f, Main.myPlayer);
				}
				sporeTimer = 0;
			}
			return true;
		}
	}
}
