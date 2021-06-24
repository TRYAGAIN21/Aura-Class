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
			projectile.localNPCHitCooldown = 19;
		}

		public int customCounter;
		public int sandTimer;

		public float shootSpeed;

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

			shootSpeed = 8f;

			Aura(projectile, 384 / 2 + modPlayer.auraSize * 16, mod.DustType("SandAuraDust"));

			Vector2 move = Vector2.Zero;
			float distance = 193f;
			bool target = false;
			for (int k = 0; k < 200; k++)
			{
				NPC npc = Main.npc[k];

				Vector2 targetPos = npc.Center;

				if (npc.active && !npc.dontTakeDamage && !npc.friendly && npc.lifeMax > 5)
				{
					Vector2 newMove = targetPos - projOwner.Center;
					float distanceTo = (float)Math.Sqrt(newMove.X * newMove.X + newMove.Y * newMove.Y);
					if (distanceTo < distance)
					{
						move = newMove;
						distance = distanceTo;
						target = true;
					}
				}
			}
			if (target)
			{
				sandTimer++;
				if (sandTimer > 60)
                {
					move.Normalize();
					move *= shootSpeed;

					Projectile.NewProjectile(projOwner.Center.X, projOwner.Center.Y, move.X, move.Y, mod.ProjectileType("Sand"), 20, 0f, Main.myPlayer, 0f, 0f);
					sandTimer = 0;
				}
			}
			else
            {
				sandTimer = 0;
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
