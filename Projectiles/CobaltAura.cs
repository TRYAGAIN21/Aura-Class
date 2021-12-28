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
	public class CobaltAura : AuraProjectile
	{
		public override string Texture => "AuraClass/Assets/BlankTexture";

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Cobalt Aura");
		}

		public override void SafeSetDefaults() {
			projectile.aiStyle = -1;
			projectile.friendly = true;
			projectile.usesLocalNPCImmunity = true;
			projectile.localNPCHitCooldown = 22;

			auraRange = 38;
			dustType = 111;
		}

		public override bool PreAI()
		{
			for (int i = 0; i < Main.maxPlayers; i++)
			{
				Player player = Main.player[i];
				if (!((player.Center - projectile.Center).Length() > CurrentRange))
				{
					player.GetModPlayer<AuraDamageClass.AuraDamagePlayer>().cobaReset = 300;
					player.GetModPlayer<AuraDamageClass.AuraDamagePlayer>().cobaAura = true;
				}
			}
			return true;
		}
	}
}
