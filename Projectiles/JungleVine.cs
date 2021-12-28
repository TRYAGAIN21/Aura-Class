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
	public class JungleVine : AuraProjectile
	{
		public override bool UsesAuraAI() => false;

		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Blossoming Vine");
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 20;
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;
			Main.projFrames[projectile.type] = 9;
		}

		private const int projectileAuraRange = 87;

		public override void SafeSetDefaults() 
		{
			projectile.width = 10;
			projectile.height = 10;
			projectile.aiStyle = -1;
			projectile.friendly = true;
			projectile.ignoreWater = true;
			projectile.tileCollide = false;
			projectile.alpha = 255;
			projectile.timeLeft = 120 * (projectile.extraUpdates + 1);

			projectile.penetrate = -1;
		}

		private bool homing;
		private int homeTimer = 30; 
		private int targetID = -1;
		private int CurrentRange = projectileAuraRange;
		private int segment_2 = 1;
		private int segment_3 = 1;
		private int segment_4 = 1;
		private int segment_5 = 1;
		private int segment_6 = 1;
		private int segment_7 = 1; 
		private int segment_8 = 1;
		private int segment_9 = 1;
		private int segment_10 = 1;
		private int segment_11 = 1;
		private int segment_12 = 1;
		private int segment_13 = 1;
		private int segment_14 = 1;
		private int segment_15 = 1;
		private int segment_16 = 1;
		private int segment_17 = 1;
		private int segment_18 = 1;
		private int segment_19 = 1;

		public override void FirstTick()
		{
			segment_2 = Main.rand.Next(2, 9);
			segment_3 = Main.rand.Next(2, 9);
			segment_4 = Main.rand.Next(2, 9);
			segment_5 = Main.rand.Next(2, 9);
			segment_6 = Main.rand.Next(2, 9);
			segment_7 = Main.rand.Next(2, 9);
			segment_8 = Main.rand.Next(2, 9);
			segment_9 = Main.rand.Next(2, 9);
			segment_10 = Main.rand.Next(2, 9);
			segment_11 = Main.rand.Next(2, 9);
			segment_12 = Main.rand.Next(2, 9);
			segment_13 = Main.rand.Next(2, 9);
			segment_14 = Main.rand.Next(2, 9);
			segment_15 = Main.rand.Next(2, 9);
			segment_16 = Main.rand.Next(2, 9);
			segment_17 = Main.rand.Next(2, 9);
			segment_18 = Main.rand.Next(2, 9);
			segment_19 = Main.rand.Next(2, 9);
			if (projectile.ai[1] != 0f)
			{
				homeTimer = 15;
			}
		}

		public override void SafeAI()
		{
			homing = true;
			if (homeTimer > 0)
            {
				homing = false;
				homeTimer--;
			}

			int RealRangeNormal = projectileAuraRange * 16;
			int RealRangePrefix = auraRangePrefix * 16;
			int RealRange = RealRangeNormal + RealRangePrefix;

			int RealPlayerRange = AuraDamagePlayer.ModPlayer(Main.player[projectile.owner]).auraSize * 16;

			CurrentRange = (RealRange + RealPlayerRange) / 2;

			Vector2 vectorToAuraPosition = projectile.Center - Main.player[projectile.owner].Center;
			float distanceToAuraPosition = vectorToAuraPosition.Length();

			if (distanceToAuraPosition > (RealRange + RealPlayerRange) / 2)
			{
				projectile.Kill();
			}

			projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);

			if (!homing)
            {
				return;
            }

			NPC target = null;
			if (Main.npc[(int)projectile.ai[0]] != null)
            {
				target = Main.npc[(int)projectile.ai[0]];
			}
			if (Main.npc[(int)projectile.ai[0]] == null || !Main.npc[(int)projectile.ai[0]].active || !Main.npc[(int)projectile.ai[0]].CanBeChasedBy(projectile))
            {
				for (int i = 0; i < Main.maxNPCs; i++)
				{
					NPC test = Main.npc[i];
					if (test.CanBeChasedBy(this))
					{
						TargetClosest();

						target = Main.npc[targetID];
						if (Main.npc[targetID] == null)
						{
							target = test;
						}
						break;
					}
				}
			}

			if (target != null && target.active && target.CanBeChasedBy(projectile) && (target.Center - Main.player[projectile.owner].Center).Length() <= ((RealRange + RealPlayerRange) / 2))
			{
				Vector2 goalVelocity = (target.Center - projectile.Center).SafeNormalize(Vector2.Zero) * 16f;
				projectile.velocity += (goalVelocity - projectile.velocity) / 14f;
			}
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			Mod mod = ModLoader.GetMod("AuraClass");

			int textureFrames = Main.projFrames[projectile.type];
			Texture2D texture = mod.GetTexture("Projectiles/JungleVine");
			Vector2 textureVect = new Vector2((float)texture.Width / 2, (float)(texture.Height / (2 * textureFrames)));

			for (int k = 1; k < projectile.oldPos.Length / (projectile.ai[1] == 2f ? 4 : (projectile.ai[1] == 1f ? 2 : 1)); k++)
			{
				int frame = 0;
				if (k == 2)
                {
					frame = segment_2;
				}
				if (k == 3)
				{
					frame = segment_3;
				}
				if (k == 4)
				{
					frame = segment_4;
				}
				if (k == 5)
				{
					frame = segment_5;
					if (projectile.ai[1] == 2f)
					{
						frame = 8;
					}
				}
				if (k == 6)
				{
					frame = segment_6;
				}
				if (k == 7)
				{
					frame = segment_7;
				}
				if (k == 8)
				{
					frame = segment_8;
				}
				if (k == 9)
				{
					frame = segment_9;
				}
				if (k == 10)
				{
					frame = segment_10;
					if (projectile.ai[1] == 1f)
                    {
						frame = 8;
					}
				}
				if (k == 11)
				{
					frame = segment_11;
				}
				if (k == 12)
				{
					frame = segment_12;
				}
				if (k == 13)
				{
					frame = segment_13;
				}
				if (k == 14)
				{
					frame = segment_14;
				}
				if (k == 15)
				{
					frame = segment_15;
				}
				if (k == 16)
				{
					frame = segment_16;
				}
				if (k == 17)
				{
					frame = segment_17;
				}
				if (k == 18)
				{
					frame = segment_18;
				}
				if (k == 19)
				{
					frame = segment_19;
				}
				if (k == 20)
				{
					frame = 8;
				}
				float rotation = (projectile.oldPos[k - 1] - projectile.oldPos[k]).ToRotation();

				Rectangle textureRect = new Rectangle(0, frame * (texture.Height / textureFrames), (texture.Width), (texture.Height / textureFrames));

				Vector2 drawOrigin = new Vector2((float)texture.Width / 2, (float)(texture.Height / (2 * textureFrames)));
				Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
				spriteBatch.Draw(texture, drawPos, new Rectangle?(textureRect), Lighting.GetColor((int)(projectile.oldPos[k].X / 16), (int)(projectile.oldPos[k].Y / 16)), (k == 1 ? projectile.rotation : (rotation + MathHelper.ToRadians(90f))), textureVect, projectile.scale, SpriteEffects.None, 0f);
			}
			return false;
		}

		public override void Kill(int timeLeft)
		{
			for (int k = 0; k < projectile.oldPos.Length / (projectile.ai[1] == 2f ? 4 : (projectile.ai[1] == 1f ? 2 : 1)); k++)
			{
				int num = 4;
				for (int index1 = 0; index1 < num; ++index1)
				{
					Vector2 position = projectile.oldPos[k];
					int index2 = Dust.NewDust(position, 6, 6, 2, 0.0f, 0.0f, 100, default(Color), 2.1f);
					Dust dust = Main.dust[index2];
					dust.fadeIn = 0.2f;
					dust.scale *= 0.66f;
					Main.dust[index2].noGravity = true;
				}
			}

			if (!(projectile.ai[1] >= 2f))
			{
				for (int i = 0; i < projectile.oldPos.Length / (projectile.ai[1] == 2f ? 4 : (projectile.ai[1] == 1f ? 2 : 1)); i++)
				{
					if (i == 0)
					{
						Vector2 velocity = projectile.velocity.RotatedBy(MathHelper.ToRadians(5f));
						velocity.Normalize();
						Projectile.NewProjectile(projectile.Center, velocity * 10f, projectile.type, projectile.damage, 0f, Main.myPlayer, 0f, projectile.ai[1] + 1);
					}
					if (i == (projectile.oldPos.Length / (projectile.ai[1] == 2f ? 4 : (projectile.ai[1] == 1f ? 2 : 1))) / 2 - 1)
					{
						Vector2 velocity = projectile.velocity.RotatedBy(MathHelper.ToRadians(-5f));
						velocity.Normalize();
						Projectile.NewProjectile(projectile.oldPos[i], velocity * 10f, projectile.type, projectile.damage, 0f, Main.myPlayer, 0f, projectile.ai[1] + 1);
					}
				}
			}
		}

		private void TargetClosest()
		{
			float num = 0f;
			float num2 = 0f;
			bool flag = false;
			int num3 = -1;
			for (int i = 0; i < Main.maxNPCs; i++)
			{
				if (!Main.npc[i].active || !Main.npc[i].CanBeChasedBy(this) || !((Main.npc[i].Center - Main.player[projectile.owner].Center).Length() <= this.CurrentRange))
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
					targetID = i;
				}
			}
		}

		public override bool? SafeCanHitNPC(NPC target)
        {
			return null;
        }

		public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
			homeTimer = 15;
		}
	}
}
