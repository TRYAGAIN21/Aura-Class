using AuraClass.AuraDamageClass;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AuraClass.Projectiles
{
	public class ToxifyingMist : AuraProjectile
	{
		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Misty Aura");
		}

		public override void SafeSetDefaults() {
			projectile.extraUpdates = 0;
			projectile.aiStyle = -1;
			projectile.friendly = true;
			projectile.usesLocalNPCImmunity = true;
			projectile.localNPCHitCooldown = 32;

			dustType = 31;
			auraRange = 16;
		}

		private int ghostTimer;
		private int ghostAmount;

		public override bool PreAI()
		{
			ghostAmount = Main.player[projectile.owner].ownedProjectileCounts[mod.ProjectileType("ToxifyingGhost")];
			if (Main.player[projectile.owner].ownedProjectileCounts[mod.ProjectileType("ToxifyingGhostAura")] < 5)
			{
				float numberProjectiles = 5;
				float rotation = MathHelper.ToRadians(180);
				for (int i = 0; i < 5; ++i)
				{
					Vector2 perturbedSpeed = new Vector2(0f, -7.5f).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * 1f; // Watch out for dividing by 0 if there is only 1 projectile.
					Projectile.NewProjectile(Main.player[projectile.owner].position, new Vector2(perturbedSpeed.X, perturbedSpeed.Y), mod.ProjectileType("ToxifyingGhostAura"), projectile.damage, 0f, projectile.owner, 0f, 0f);
				}
				ghostTimer = 0;
			}

			ghostTimer++;

			if (ghostAmount < 15 && ghostTimer > 30)
			{
				for (int i = 0; i < (Main.rand.Next(2, 3) + 1); ++i)
				{
					ghostAmount++;
					if (ghostAmount < 15)
                    {
						Projectile.NewProjectile(Main.player[projectile.owner].position, new Vector2(Main.rand.NextFloat(7.5f, -7.5f), Main.rand.NextFloat(7.5f, -7.5f)), mod.ProjectileType("ToxifyingGhost"), projectile.damage, 0f, projectile.owner, 0f, 0f);
					}
				}
				ghostTimer = 0;
			}

			return true;
		}
	}
}
