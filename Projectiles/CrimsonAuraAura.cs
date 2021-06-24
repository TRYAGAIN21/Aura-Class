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
			DisplayName.SetDefault("Aura");
		}

		public override void SafeSetDefaults() {
			projectile.extraUpdates = 0;
			projectile.width = 384;
			projectile.height = 384;
			projectile.aiStyle = -1;
			projectile.friendly = true;
			projectile.penetrate = -1;
			projectile.scale = 1f;
			projectile.alpha = 255;
			projectile.usesLocalNPCImmunity = true;
			projectile.localNPCHitCooldown = 24;
		}

		public int customCounter;
		public int orbTimer;

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

			Aura(projectile, 384 / 2 + modPlayer.auraSize * 16, mod.DustType("CrimsonAuraDust"));

			/*orbTimer++;

			float speedX = 3f;
			float speedY = 3f;

			if (orbTimer > 25)
            {
				Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(360));

				Projectile.NewProjectile(projOwner.position.X, projOwner.position.Y, perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("BloodOrb"), projectile.damage + 15, 0f, projectile.owner, 0f, 0f);
				orbTimer = 0;
			}*/
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
