using AuraClass.AuraDamageClass;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AuraClass.Projectiles
{
	public class TestAuraAura : AuraProjectile
	{
		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Aura");
		}

		public override void SafeSetDefaults() {
			projectile.extraUpdates = 0;
			projectile.width = 192;
			projectile.height = 192;
			projectile.aiStyle = -1;
			projectile.friendly = true;
			projectile.penetrate = -1;
			projectile.magic = true;
			projectile.scale = 1f;
			projectile.alpha = 0;
			projectile.alpha = 255;
			projectile.usesLocalNPCImmunity = true;
			projectile.localNPCHitCooldown = 25;
		}

		public int customCounter;

		public override void SafeAI() 
		{
			Player projOwner = Main.player[projectile.owner];

			AuraDamagePlayer modPlayer = AuraDamagePlayer.ModPlayer(projOwner);

			Vector2 ownerMountedCenter = projOwner.RotatedRelativePoint(projOwner.MountedCenter, true);

			projOwner.heldProj = projectile.whoAmI;

			projOwner.itemTime = projOwner.itemAnimation;

			projectile.position.X = ownerMountedCenter.X - (float)(projectile.width / 2);
			projectile.position.Y = ownerMountedCenter.Y - (float)(projectile.height / 2);

			if (!projOwner.channel)
			{
				projectile.Kill();
			}

			for (int i = 0; i < 360; i++) // I could do 360 here, instead of 90 (and then remove the "* 4" from the deg variable), and it still works well
			{
				double deg = i;
				double rad = (deg) * (Math.PI / 180);
				double dist = 96 + modPlayer.auraSize * 16;

				float x2 = projectile.Center.X - (int)(Math.Cos(rad) * dist) - projOwner.width + 24 / 2;
				float y2 = projectile.Center.Y - (int)(Math.Sin(rad) * dist) - projOwner.height + 64 / 2;

				if (++customCounter % 10 == 0)
				{
					int dust = Dust.NewDust(new Vector2(x2 - projectile.velocity.X, y2 - projectile.velocity.Y), 1, 1, mod.DustType("WoodenAuraDust"), 0f, 0f, 0, default(Color), 1f);
					Main.dust[dust].noGravity = true;
				}
			}
		}

		private void UpdatePlayer(Player player)
		{
			// Multiplayer support here, only run this code if the client running it is the owner of the projectile
			if (projectile.owner == Main.myPlayer)
			{
				projectile.netUpdate = true;
				player.itemTime = 10; // Set item time to 2 frames while we are used
				player.itemAnimation = 10; // Set item animation time to 2 frames while we are used
			}
		}
	}
}
