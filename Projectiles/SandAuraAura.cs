using AuraClass.AuraDamageClass;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AuraClass.Projectiles
{
	public class SandAuraAura : AuraProjectile
	{
		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Desert Aura");
		}

		public override void SafeSetDefaults() {
			projectile.extraUpdates = 0;
			projectile.aiStyle = -1;
			projectile.friendly = true;
			projectile.usesLocalNPCImmunity = true;
			projectile.localNPCHitCooldown = 19;

			dustType = 124;
			auraRange = 20;
		}

		private int sandTimer;
		private int Counter;

		public override void SafeAI()
		{
			int RealRangeNormal = auraRange * 16;
			int RealRangePrefix = auraRangePrefix * 16;
			int RealRange = RealRangeNormal + RealRangePrefix;

			int RealPlayerRange = AuraDamagePlayer.ModPlayer(Main.player[projectile.owner]).auraSize * 16;

			sandTimer++;
			if (sandTimer > 60)
            {
				for (int k = 0; k < Main.maxNPCs; k++)
				{
					if (Counter > 2)
                    {
						Counter = 0;
						break;
                    }
					NPC npc = Main.npc[k];

					if (npc.CanBeChasedBy(this) && Collision.CanHitLine(Main.player[projectile.owner].Center, 1, 1, npc.Center, 1, 1))
					{
						Vector2 vectorToTargetPosition = npc.Center - projectile.Center;
						float distanceToTargetPosition = vectorToTargetPosition.Length();

						if (distanceToTargetPosition <= (RealRange + RealPlayerRange) / 2)
						{
							vectorToTargetPosition.Normalize();
							vectorToTargetPosition *= 8f;

							Counter += 1;

							Projectile.NewProjectile(Main.player[projectile.owner].Center + npc.velocity, vectorToTargetPosition, mod.ProjectileType("Sand"), 20, 0f, Main.myPlayer, npc.whoAmI, projectile.whoAmI);
						}
					}
				}
				sandTimer = 0;
			}
			else { Counter = 0; }
        }
	}
}
