using AuraClass.AuraDamageClass;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AuraClass.Projectiles
{
	public class CrimsonAuraAura : AuraProjectile
	{
		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Blood Aura");
		}

		public override void SafeSetDefaults() {
			projectile.extraUpdates = 0;
			projectile.aiStyle = -1;
			projectile.friendly = true;
			projectile.usesLocalNPCImmunity = true;
			projectile.localNPCHitCooldown = 24;

			dustType = 5;
			auraRange = 24;
		}

		private int waterTimer;
		private int Counter;

		public override void SafeAI()
		{
			int RealRangeNormal = auraRange * 16;
			int RealRangePrefix = auraRangePrefix * 16;
			int RealRange = RealRangeNormal + RealRangePrefix;

			int RealPlayerRange = AuraDamagePlayer.ModPlayer(Main.player[projectile.owner]).auraSize * 16;

			waterTimer++;
			if (waterTimer > 120)
			{
				for (int k = 0; k < Main.maxNPCs; k++)
				{
					if (Counter > 4)
					{
						Counter = 0;
						break;
					}
					NPC npc = Main.npc[k];

					if (npc.CanBeChasedBy(this))
					{
						Vector2 vectorToTargetPosition = npc.Center - projectile.Center;
						float distanceToTargetPosition = vectorToTargetPosition.Length();

						if (distanceToTargetPosition <= (RealRange + RealPlayerRange) / 2)
						{
							vectorToTargetPosition.Normalize();
							vectorToTargetPosition *= 8f;

							Counter += 1;

							Projectile.NewProjectile(Main.player[projectile.owner].Center + npc.velocity, vectorToTargetPosition, mod.ProjectileType("BloodDroplet"), 20, 0f, Main.myPlayer, npc.whoAmI, projectile.whoAmI);
						}
					}
				}
				waterTimer = 0;
			}
			else { Counter = 0; }
		}
	}
}
