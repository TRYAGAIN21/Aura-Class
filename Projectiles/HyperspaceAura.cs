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
	public class HyperspaceAura : AuraProjectile
	{
		public override string Texture => "AuraClass/Assets/BlankTexture";

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Hyperspace Aura");
		}

		public override void SafeSetDefaults() {
			projectile.aiStyle = -1;
			projectile.friendly = true;
			projectile.usesLocalNPCImmunity = true;
			projectile.localNPCHitCooldown = 22;

			auraRange = 50;
			dustType = ModContent.DustType<Dusts.DarkEnergyDust>();
		}

		private int beamTimer;
		private int Counter;

		public override bool PreAI()
		{
			beamTimer++;
			if (beamTimer > 90)
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
						float distanceToTargetPosition = (npc.Center - projectile.Center).Length();
						if (distanceToTargetPosition <= CurrentRange)
						{
							Counter += 1;

							Projectile.NewProjectile(Main.player[projectile.owner].Center, new Vector2(0f, 5f).RotatedBy(MathHelper.ToRadians(Counter >= 2f ? -135f : (Counter != 0f ? 135f : 0f))), mod.ProjectileType("HyperspaceBeam"), ((projectile.damage / 2) + (projectile.damage / 4)), 0f, Main.myPlayer, npc.whoAmI);
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
