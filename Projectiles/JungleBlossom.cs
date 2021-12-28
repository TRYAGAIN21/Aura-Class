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
	public class JungleBlossom : AuraProjectile
	{
		public override string Texture => "AuraClass/Assets/BlankTexture";

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Blossoming Aura");
		}

		public override void SafeSetDefaults() {
			projectile.aiStyle = -1;
			projectile.friendly = true;
			projectile.usesLocalNPCImmunity = true;
			projectile.localNPCHitCooldown = 22;

			auraRange = 58;
			dustType = -1;
		}

		private int vineTimer = 480;
		private int flowerTimer;
		private int flowerDirection = 1;

		private int target = -1;
		public override bool PreAI()
		{
			projectile.rotation += 0.05f;

			vineTimer++;
			if (vineTimer > (120 * 4))
			{
				vineTimer = 120 * 4;
				for (int i = 0; i < Main.maxNPCs; i++)
				{
					NPC test = Main.npc[i];
					if (test.CanBeChasedBy(this))
                    {
						TargetClosest();

						if (Main.npc[target] != null && target != -1)
						{
							NPC npc = Main.npc[target];
							if (npc.CanBeChasedBy(this) && (npc.Center - projectile.Center).Length() <= CurrentRange)
							{
								Vector2 velocity = npc.Center - projectile.Center;
								velocity.Normalize();
								velocity *= 12f;

								Projectile.NewProjectile(Main.player[projectile.owner].Center, velocity, mod.ProjectileType("JungleVine"), (int)(projectile.damage * 1.5f), 0f, Main.myPlayer, npc.whoAmI);
								vineTimer = 0;
							}
							else
							{
								if (test != null && (test.Center - projectile.Center).Length() <= CurrentRange)
								{
									Vector2 velocity = test.Center - projectile.Center;
									velocity.Normalize();
									velocity *= 12f;

									Projectile.NewProjectile(Main.player[projectile.owner].Center, velocity, mod.ProjectileType("JungleVine"), (int)(projectile.damage * 1.5f), 0f, Main.myPlayer, test.whoAmI);
									vineTimer = 0;
								}
							}
							break;
						}
					}
				}
			}

			flowerTimer++;
			if (flowerTimer > 60)
			{
				Vector2 baseVel = flowerDirection == -1 ? Vector2.UnitX : Vector2.UnitX.RotatedBy(Math.PI * 2 / 4 * 0.5);
				for (int k = 0; k < 4; k++)
				{
					Projectile.NewProjectile(projectile.Center, 15f * baseVel.RotatedBy(Math.PI * 2 / 4 * k), mod.ProjectileType("JungleFlower"), projectile.damage, 0f, Main.myPlayer);
				}

				flowerDirection *= -1;
				flowerTimer = 0;
			}
			return true;
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			Mod mod = ModLoader.GetMod("AuraClass");

			Texture2D auraTex = mod.GetTexture("Projectiles/JungleBlossom");
			Vector2 textureVect = new Vector2((float)auraTex.Width / 2, (float)(auraTex.Height / (2 * Main.projFrames[projectile.type])));

			float spinSpeed = CurrentRange;
			Vector2 spinningpoint = new Vector2(0f, 0f - spinSpeed);

			int frame = 0;
			int PetalCount = 0;
			float PetalsMax = 20f;

			while (true)
			{
				PetalCount++;

				if (PetalCount > (int)PetalsMax - 1)
					break;

				Vector2 position9 = spinningpoint.RotatedBy(projectile.rotation + MathHelper.ToRadians((PetalCount + 1f) * PetalsMax)) + projectile.Center - Main.screenPosition;
				Main.spriteBatch.Draw(auraTex, position9, null, Lighting.GetColor((int)(projectile.Center.X / 16), (int)(projectile.Center.Y / 16)), projectile.rotation + MathHelper.ToRadians((PetalCount + 1f) * PetalsMax), auraTex.Size() * 0.5f, projectile.scale, SpriteEffects.None, 0f);
			}
			return false;
		}

		private void TargetClosest()
		{
			float num = 0f;
			float num2 = 0f;
			bool flag = false;
			int num3 = -1;
			target = -1;
			for (int i = 0; i < Main.maxNPCs; i++)
			{
				if (!Main.npc[i].active || !Main.npc[i].CanBeChasedBy(this))
				{
					continue;
				}
				float num4 = Math.Abs(Main.npc[i].position.X + (float)(Main.npc[i].width / 2) - projectile.position.X + (float)(projectile.width / 2)) + Math.Abs(Main.npc[i].position.Y + (float)(Main.npc[i].height / 2) - projectile.position.Y + (float)(projectile.height / 2));
				if (projectile.direction != 0)
				{
					num4 += 1000f;
				}
				if (!flag || num4 < num)
				{
					flag = true;
					num3 = -1;
					num2 = Math.Abs(Main.npc[i].position.X + (float)(Main.npc[i].width / 2) - projectile.position.X + (float)(projectile.width / 2)) + Math.Abs(Main.npc[i].position.Y + (float)(Main.npc[i].height / 2) - projectile.position.Y + (float)(projectile.height / 2));
					num = num4;
					target = i;
				}
			}
		}
	}
}
