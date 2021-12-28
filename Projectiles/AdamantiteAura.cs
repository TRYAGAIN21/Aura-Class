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
	public class AdamantiteAura : AuraProjectile
	{
		public override string Texture => "AuraClass/Assets/BlankTexture";

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Adamantite Aura");
		}

		public override void SafeSetDefaults() {
			projectile.aiStyle = -1;
			projectile.friendly = true;
			projectile.usesLocalNPCImmunity = true;
			projectile.localNPCHitCooldown = 22;

			auraRange = 38; 
			dustType = 90;
		}

		public override bool PreAI()
		{
			for (int i = 0; i < Main.maxPlayers; i++)
			{
				Player player = Main.player[i];
				if (!((player.Center - projectile.Center).Length() > CurrentRange))
				{
					player.GetModPlayer<AuraDamageClass.AuraDamagePlayer>().adamReset = 300;
					player.GetModPlayer<AuraDamageClass.AuraDamagePlayer>().adamAura = true;
				}
			}
			return true;
		}
	}
}
