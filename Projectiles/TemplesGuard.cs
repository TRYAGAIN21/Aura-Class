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
	public class TemplesGuard : AuraProjectile
	{
		public override string Texture => "AuraClass/Assets/BlankTexture";

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Temple Aura");
		}

		public override void SafeSetDefaults() {
			projectile.aiStyle = -1;
			projectile.friendly = true;
			projectile.usesLocalNPCImmunity = true;
			projectile.localNPCHitCooldown = 22;

			auraRange = 44;
			dustType = 174;
		}

		private int beamTimer;
		private int Counter;

		public override bool PreAI()
		{
			beamTimer++;
			if (beamTimer > 120)
			{
				for (int k = 0; k < Main.maxNPCs; k++)
				{
					if (Counter > 4)
					{
						Counter = 0;
						break;
					}
					NPC npc = Main.npc[k];

					if (npc.CanBeChasedBy(this) && Collision.CanHitLine(Main.player[projectile.owner].Center, 1, 1, npc.Center, 1, 1))
					{
						Vector2 vectorToTargetPosition = npc.Center - projectile.Center;
						float distanceToTargetPosition = vectorToTargetPosition.Length();

						if (distanceToTargetPosition <= CurrentRange)
						{
							vectorToTargetPosition.Normalize();
							vectorToTargetPosition *= 12f;

							Counter += 1;

							Main.PlaySound(2, (int)Main.player[projectile.owner].Center.X, (int)Main.player[projectile.owner].Center.Y, 20);
							Projectile.NewProjectile(Main.player[projectile.owner].Center, vectorToTargetPosition, mod.ProjectileType("TempleFireball"), (int)(projectile.damage * 1.2f), 0f, Main.myPlayer, npc.whoAmI, projectile.whoAmI);
						}
					}
				}
				beamTimer = 0;
			}
			else { Counter = 0; }
			return true;
		}
	}
}
