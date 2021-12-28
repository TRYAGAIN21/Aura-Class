using AuraClass.AuraDamageClass;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AuraClass.Projectiles
{
	public class PalladiumAura : AuraProjectile
	{
		public override string Texture => "AuraClass/Assets/BlankTexture";

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Palladium Aura");
		}

		public override void SafeSetDefaults() {
			projectile.aiStyle = -1;
			projectile.friendly = true;
			projectile.usesLocalNPCImmunity = true;
			projectile.localNPCHitCooldown = 22;

			auraRange = 36;
			dustType = 55;
		}

		private const int lifeRegen = 3;
		public override void SafeAI()
		{
			for (int i = 0; i < Main.maxPlayers; i++)
            {
				Player player = Main.player[i];
				if (!((player.Center - projectile.Center).Length() > CurrentRange))
                {
					player.lifeRegen += lifeRegen;
					player.lifeRegenCount++;
					if (Main.rand.NextBool())
					{
						int num5 = Dust.NewDust(player.position, player.width, player.height, 55, 0f, 0f, 200, default(Color), 0.5f);
						Main.dust[num5].noGravity = true;
						Main.dust[num5].velocity *= 0.75f;
						Main.dust[num5].fadeIn = 1.3f;
						Vector2 vector = new Vector2(Main.rand.Next(-100, 101), Main.rand.Next(-100, 101));
						vector.Normalize();
						vector *= (float)Main.rand.Next(50, 100) * 0.04f;
						Main.dust[num5].velocity = vector;
						vector.Normalize();
						vector *= 34f;
						Main.dust[num5].position = player.Center - vector;
					}
				}
            }

			for (int i = 0; i < 200; i++)
			{
				NPC npc = Main.npc[i];
				if (!((npc.Center - projectile.Center).Length() > CurrentRange) && (npc.friendly || npc.townNPC) && npc.life < npc.lifeMax)
				{
					npc.friendlyRegen += lifeRegen * 2;
					if (Main.rand.NextBool())
					{
						int num5 = Dust.NewDust(npc.position, npc.width, npc.height, 55, 0f, 0f, 200, default(Color), 0.5f);
						Main.dust[num5].noGravity = true;
						Main.dust[num5].velocity *= 0.75f;
						Main.dust[num5].fadeIn = 1.3f;
						Vector2 vector = new Vector2(Main.rand.Next(-100, 101), Main.rand.Next(-100, 101));
						vector.Normalize();
						vector *= (float)Main.rand.Next(50, 100) * 0.04f;
						Main.dust[num5].velocity = vector;
						vector.Normalize();
						vector *= 34f;
						Main.dust[num5].position = npc.Center - vector;
					}
				}
			}
		}
	}
}
