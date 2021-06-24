using AuraClass.AuraDamageClass;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AuraClass.Projectiles
{
	public class HiveBee1 : ModProjectile
	{
		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Bee");
			Main.projFrames[projectile.type] = 2;
		}

		public override void SetDefaults() 
		{
			projectile.width = 10;
			projectile.height = 10;
			projectile.aiStyle = -1;
			projectile.friendly = true;
			projectile.tileCollide = false;
		}

		private const int Frame1 = 0;
		private const int Frame2 = 1;

		public override void AI() 
		{
			Player player = Main.player[projectile.owner];

			if (!player.channel)
			{
				projectile.Kill();
			}

			projectile.spriteDirection = projectile.direction;

			Vector2 idlePosition = player.Center;
			Vector2 AuraPosition = player.Center;
			idlePosition.X += 96f;

			float distanceFromTarget = 192f;
			Vector2 targetCenter = projectile.position;
			bool foundTarget = false;

			Vector2 vectorToIdlePosition = idlePosition - projectile.Center;
			float distanceToIdlePosition = vectorToIdlePosition.Length();

			Vector2 vectorToAuraPosition = AuraPosition - projectile.Center;
			float distanceToAuraPosition = vectorToAuraPosition.Length();

			if (!foundTarget)
			{
				// This code is required either way, used for finding a target
				for (int i = 0; i < Main.maxNPCs; i++)
				{
					NPC npc = Main.npc[i];
					if (npc.CanBeChasedBy())
					{
						float between = Vector2.Distance(npc.Center, projectile.Center);
						bool closest = Vector2.Distance(projectile.Center, targetCenter) > between;
						bool inRange = between < distanceFromTarget;
						bool lineOfSight = Collision.CanHitLine(projectile.position, projectile.width, projectile.height, npc.position, npc.width, npc.height);
						// Additional check for this specific minion behavior, otherwise it will stop attacking once it dashed through an enemy while flying though tiles afterwards
						// The number depends on various parameters seen in the movement code below. Test different ones out until it works alright
						bool closeThroughWall = between < 2000f;
						if (((closest && inRange) || !foundTarget) && (lineOfSight || closeThroughWall))
						{
							distanceFromTarget = between;
							targetCenter = npc.Center;
							foundTarget = true;
						}
					}
				}
			}

			float speed = 8f;
			float inertia = 20f;

			if (foundTarget && distanceToAuraPosition < 192f && distanceFromTarget < 192f)
			{
				// Minion has a target: attack (here, fly towards the enemy)
				if (distanceFromTarget > 40f)
				{
					// The immediate range around the target (so it doesn't latch onto it when close)
					Vector2 direction = targetCenter - projectile.Center;
					direction.Normalize();
					direction *= speed;
					projectile.velocity = (projectile.velocity * (inertia - 1) + direction) / inertia;
				}
			}
			else
			{
				// Minion doesn't have a target: return to player and idle
				if (distanceToAuraPosition > 192f)
				{
					// Speed up the minion if it's away from the player
					speed = 20f;
					inertia = 60f;
				}
				else
				{
					// Slow down the minion if closer to the player
					speed = 4f;
					inertia = 80f;
				}
				if (distanceToIdlePosition > 40f)
				{
					// The immediate range around the player (when it passively floats about)

					// This is a simple movement formula using the two parameters and its desired direction to create a "homing" movement
					vectorToIdlePosition.Normalize();
					vectorToIdlePosition *= speed;
					projectile.velocity = (projectile.velocity * (inertia - 1) + vectorToIdlePosition) / inertia;
				}
				else if (projectile.velocity == Vector2.Zero)
				{
					// If there is a case where it's not moving at all, give it a little "poke"
					projectile.velocity.X = -0.15f;
					projectile.velocity.Y = -0.05f;
				}
			}

			projectile.rotation = projectile.velocity.X * 0.05f;

			projectile.frameCounter++;

			if (projectile.frameCounter < 5)
			{
				projectile.frame = Frame1;
			}
			else if (projectile.frameCounter < 10)
			{
				projectile.frame = Frame2;
			}
			else
			{
				projectile.frameCounter = 0;
				projectile.frame = Frame1;
			}
		}
	}

	public class HiveBee2 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Bee");
			Main.projFrames[projectile.type] = 2;
		}

		public override void SetDefaults()
		{
			projectile.width = 10;
			projectile.height = 10;
			projectile.aiStyle = -1;
			projectile.friendly = true;
			projectile.tileCollide = false;
		}

		private const int Frame1 = 0;
		private const int Frame2 = 1;

		public override void AI()
		{
			Player player = Main.player[projectile.owner];

			if (!player.channel)
			{
				projectile.Kill();
			}

			projectile.spriteDirection = projectile.direction;

			Vector2 idlePosition = player.Center;
			Vector2 AuraPosition = player.Center;
			idlePosition.X -= 96f;

			float distanceFromTarget = 192f;
			Vector2 targetCenter = projectile.position;
			bool foundTarget = false;

			Vector2 vectorToIdlePosition = idlePosition - projectile.Center;
			float distanceToIdlePosition = vectorToIdlePosition.Length();

			Vector2 vectorToAuraPosition = AuraPosition - projectile.Center;
			float distanceToAuraPosition = vectorToAuraPosition.Length();

			if (!foundTarget)
			{
				// This code is required either way, used for finding a target
				for (int i = 0; i < Main.maxNPCs; i++)
				{
					NPC npc = Main.npc[i];
					if (npc.CanBeChasedBy())
					{
						float between = Vector2.Distance(npc.Center, projectile.Center);
						bool closest = Vector2.Distance(projectile.Center, targetCenter) > between;
						bool inRange = between < distanceFromTarget;
						bool lineOfSight = Collision.CanHitLine(projectile.position, projectile.width, projectile.height, npc.position, npc.width, npc.height);
						// Additional check for this specific minion behavior, otherwise it will stop attacking once it dashed through an enemy while flying though tiles afterwards
						// The number depends on various parameters seen in the movement code below. Test different ones out until it works alright
						bool closeThroughWall = between < 2000f;
						if (((closest && inRange) || !foundTarget) && (lineOfSight || closeThroughWall))
						{
							distanceFromTarget = between;
							targetCenter = npc.Center;
							foundTarget = true;
						}
					}
				}
			}

			float speed = 8f;
			float inertia = 20f;

			if (foundTarget && distanceToAuraPosition < 192f && distanceFromTarget < 192f)
			{
				// Minion has a target: attack (here, fly towards the enemy)
				if (distanceFromTarget > 40f)
				{
					// The immediate range around the target (so it doesn't latch onto it when close)
					Vector2 direction = targetCenter - projectile.Center;
					direction.Normalize();
					direction *= speed;
					projectile.velocity = (projectile.velocity * (inertia - 1) + direction) / inertia;
				}
			}
			else
			{
				// Minion doesn't have a target: return to player and idle
				if (distanceToAuraPosition > 192f)
				{
					// Speed up the minion if it's away from the player
					speed = 20f;
					inertia = 60f;
				}
				else
				{
					// Slow down the minion if closer to the player
					speed = 4f;
					inertia = 80f;
				}
				if (distanceToIdlePosition > 40f)
				{
					// The immediate range around the player (when it passively floats about)

					// This is a simple movement formula using the two parameters and its desired direction to create a "homing" movement
					vectorToIdlePosition.Normalize();
					vectorToIdlePosition *= speed;
					projectile.velocity = (projectile.velocity * (inertia - 1) + vectorToIdlePosition) / inertia;
				}
				else if (projectile.velocity == Vector2.Zero)
				{
					// If there is a case where it's not moving at all, give it a little "poke"
					projectile.velocity.X = -0.15f;
					projectile.velocity.Y = -0.05f;
				}
			}

			projectile.rotation = projectile.velocity.X * 0.05f;

			projectile.frameCounter++;

			if (projectile.frameCounter < 5)
			{
				projectile.frame = Frame1;
			}
			else if (projectile.frameCounter < 10)
			{
				projectile.frame = Frame2;
			}
			else
			{
				projectile.frameCounter = 0;
				projectile.frame = Frame1;
			}
		}
	}

	public class HiveBee3 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Bee");
			Main.projFrames[projectile.type] = 2;
		}

		public override void SetDefaults()
		{
			projectile.width = 10;
			projectile.height = 10;
			projectile.aiStyle = -1;
			projectile.friendly = true;
			projectile.tileCollide = false;
		}

		private const int Frame1 = 0;
		private const int Frame2 = 1;

		public override void AI()
		{
			Player player = Main.player[projectile.owner];

			if (!player.channel)
			{
				projectile.Kill();
			}

			projectile.spriteDirection = projectile.direction;

			Vector2 idlePosition = player.Center;
			Vector2 AuraPosition = player.Center;
			idlePosition.Y -= 96f;

			float distanceFromTarget = 192f;
			Vector2 targetCenter = projectile.position;
			bool foundTarget = false;

			Vector2 vectorToIdlePosition = idlePosition - projectile.Center;
			float distanceToIdlePosition = vectorToIdlePosition.Length();

			Vector2 vectorToAuraPosition = AuraPosition - projectile.Center;
			float distanceToAuraPosition = vectorToAuraPosition.Length();

			if (!foundTarget)
			{
				// This code is required either way, used for finding a target
				for (int i = 0; i < Main.maxNPCs; i++)
				{
					NPC npc = Main.npc[i];
					if (npc.CanBeChasedBy())
					{
						float between = Vector2.Distance(npc.Center, projectile.Center);
						bool closest = Vector2.Distance(projectile.Center, targetCenter) > between;
						bool inRange = between < distanceFromTarget;
						bool lineOfSight = Collision.CanHitLine(projectile.position, projectile.width, projectile.height, npc.position, npc.width, npc.height);
						// Additional check for this specific minion behavior, otherwise it will stop attacking once it dashed through an enemy while flying though tiles afterwards
						// The number depends on various parameters seen in the movement code below. Test different ones out until it works alright
						bool closeThroughWall = between < 2000f;
						if (((closest && inRange) || !foundTarget) && (lineOfSight || closeThroughWall))
						{
							distanceFromTarget = between;
							targetCenter = npc.Center;
							foundTarget = true;
						}
					}
				}
			}

			float speed = 8f;
			float inertia = 20f;

			if (foundTarget && distanceToAuraPosition < 192f && distanceFromTarget < 192f)
			{
				// Minion has a target: attack (here, fly towards the enemy)
				if (distanceFromTarget > 40f)
				{
					// The immediate range around the target (so it doesn't latch onto it when close)
					Vector2 direction = targetCenter - projectile.Center;
					direction.Normalize();
					direction *= speed;
					projectile.velocity = (projectile.velocity * (inertia - 1) + direction) / inertia;
				}
			}
			else
			{
				// Minion doesn't have a target: return to player and idle
				if (distanceToAuraPosition > 192f)
				{
					// Speed up the minion if it's away from the player
					speed = 20f;
					inertia = 60f;
				}
				else
				{
					// Slow down the minion if closer to the player
					speed = 4f;
					inertia = 80f;
				}
				if (distanceToIdlePosition > 40f)
				{
					// The immediate range around the player (when it passively floats about)

					// This is a simple movement formula using the two parameters and its desired direction to create a "homing" movement
					vectorToIdlePosition.Normalize();
					vectorToIdlePosition *= speed;
					projectile.velocity = (projectile.velocity * (inertia - 1) + vectorToIdlePosition) / inertia;
				}
				else if (projectile.velocity == Vector2.Zero)
				{
					// If there is a case where it's not moving at all, give it a little "poke"
					projectile.velocity.X = -0.15f;
					projectile.velocity.Y = -0.05f;
				}
			}

			projectile.rotation = projectile.velocity.X * 0.05f;

			projectile.frameCounter++;

			if (projectile.frameCounter < 5)
			{
				projectile.frame = Frame1;
			}
			else if (projectile.frameCounter < 10)
			{
				projectile.frame = Frame2;
			}
			else
			{
				projectile.frameCounter = 0;
				projectile.frame = Frame1;
			}
		}
	}
}
