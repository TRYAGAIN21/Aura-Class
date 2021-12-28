using AuraClass.AuraDamageClass;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AuraClass.Projectiles
{
	public class TheCorrosionCloud : AuraProjectile
	{
		public override string Texture => "AuraClass/Assets/BlankTexture";

		public override bool UsesAuraAI() => false;

		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Corroding Cloud");
		}

		public override void SafeSetDefaults() 
		{
			projectile.width = 10;
			projectile.height = 10;
			projectile.aiStyle = -1;
			projectile.friendly = true;
			projectile.alpha = 255;
			projectile.tileCollide = false;
			projectile.penetrate = -1;
		}

		private Vector2 idlePos;

		private int moveCD = 60;

		public override void FirstTick()
		{
			idlePos = projectile.Center;
		}

		public override bool? SafeCanHitNPC(NPC target)
		{
			return false;
		}

		public override void SafeAI()
		{
			Player player = Main.player[projectile.owner];
			NPC target = Main.npc[(int)projectile.ai[0]];
			NPC target_2 = null;
			NPC target_3 = null;
			float locateDistance = 960f;

			if (!target.active || !target.GetGlobalNPC<AuraClassNPC>().corroding || !target.CanBeChasedBy(this))
            {
				target = null;
				for (int k = 0; k < Main.maxNPCs; k++)
				{
					NPC npc = Main.npc[k];

					if (npc.CanBeChasedBy(this) && npc.GetGlobalNPC<AuraClassNPC>().corroding && (npc.Center - projectile.Center).Length() <= locateDistance)
					{
						target = npc;
					}
				}
			}

			if (target_2 == null)
            {
				for (int k = 0; k < Main.maxNPCs; k++)
				{
					NPC npc = Main.npc[k];

					if (npc.CanBeChasedBy(this) && npc != target && npc.GetGlobalNPC<AuraClassNPC>().corroding && (npc.Center - projectile.Center).Length() <= locateDistance)
					{
						target_2 = npc;
						if (target == null)
						{
							target = npc;
							target_2 = null;
						}
					}
				}
			}

			if (target_3 == null)
			{
				for (int k = 0; k < Main.maxNPCs; k++)
				{
					NPC npc = Main.npc[k];

					if (npc.CanBeChasedBy(this) && npc != target && npc != target_2 && npc.GetGlobalNPC<AuraClassNPC>().corroding && (npc.Center - projectile.Center).Length() <= locateDistance)
					{
						target_3 = npc;
						if (target_2 == null)
                        {
							target_2 = npc;
							target_3 = null;
						}
					}
				}
			}

			for (int i = 0; i < 1000; i++)
			{
				if (Main.projectile[i].active && Main.projectile[i].type == projectile.type && i != projectile.whoAmI)
				{
					projectile.active = false;
					return;
				}
			}

			projectile.timeLeft = 2;
			if (target == null && target_2 == null && target_3 == null)
            {
				projectile.Kill();
				return;
            }

			moveCD--;
			if (moveCD <= 0)
			{
				float spread = 5f;
				idlePos = new Vector2(target.Center.X + (Main.rand.NextFloat(-spread * 16f, spread * 16f)), target.Center.Y - (Main.rand.NextFloat(200f, 450f)));
				if (target_3 != null)
                {
					NPC mainTarget_Middle = target;
					if (target_2.Center.X < target.Center.X && target_2.Center.X > target_3.Center.X || target_2.Center.X < target_3.Center.X && target_2.Center.X > target.Center.X)
					{
						mainTarget_Middle = target_2;
					}
					if (target_3.Center.X < target.Center.X && target_3.Center.X > target_2.Center.X || target_3.Center.X < target_2.Center.X && target_3.Center.X > target.Center.X)
					{
						mainTarget_Middle = target_3;
					}

					NPC mainTarget_Highest = target;
					if (target_2.Center.Y < target.Center.Y && target_2.Center.Y < target_3.Center.X)
					{
						mainTarget_Highest = target_2;
					}
					if (target_3.Center.Y < target.Center.Y && target_3.Center.Y < target_2.Center.Y)
					{
						mainTarget_Highest = target_3;
					}

					idlePos = new Vector2(mainTarget_Middle.Center.X + (Main.rand.NextFloat(-spread * 16f, spread * 16f)), mainTarget_Highest.Center.Y - (Main.rand.NextFloat(200f, 450f)));
				}
				else if (target_2 != null)
                {
					NPC mainTarget_Highest = target_2.Center.Y < target.Center.Y ? target_2 : target;

					Vector2 betweenTargets = new Vector2(target.Center.X - ((target.Center.X - target_2.Center.X) / 2), mainTarget_Highest.Center.Y);
					idlePos = new Vector2(betweenTargets.X + (Main.rand.NextFloat(-spread * 16f, spread * 16f)), betweenTargets.Y - (Main.rand.NextFloat(200f, 450f)));
				}

				moveCD = 60;

				for (int i = 0; i < 3; i++)
                {
					if (target_2 == null && i == 1 || target_3 == null && i == 2)
                    {
						continue;
                    }

					NPC currentTarget = i == 2 ? target_3 : (i == 1 ? target_2: target);
					Vector2 vectorToTargetPosition = currentTarget.Center - projectile.Center;

					vectorToTargetPosition.Normalize();
					vectorToTargetPosition *= 8f;

					Projectile.NewProjectile(projectile.Center, vectorToTargetPosition, mod.ProjectileType("TheCorrosionDroplet"), 20, 0f, Main.myPlayer);
				}
			}

			Vector2 vectorToIdlePosition = idlePos - projectile.Center;

			vectorToIdlePosition.Normalize();
			vectorToIdlePosition *= 8f;

			projectile.velocity = vectorToIdlePosition;

			float distanceToIdlePos = (idlePos - projectile.Center).Length();
			if (distanceToIdlePos < 10f)
            {
				projectile.velocity = new Vector2(0f, 0f);
            }

			for (int num612 = 0; num612 < 4; num612++)
			{
				int num584 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 31, (0f - projectile.velocity.X) * 0.2f, (0f - projectile.velocity.Y) * 0.2f, 100, new Color(195, 75, 75), 2f);
				Main.dust[num584].noGravity = true;
				Dust dust174 = Main.dust[num584];
				Dust dust2 = dust174;
				dust2.velocity *= 2f;
			}
		}
	}
}
