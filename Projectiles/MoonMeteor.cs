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
	public class MoonMeteor : AuraProjectile
	{
		public override string Texture => "AuraClass/Items/Weapons/Auras/MoonRock";

		public override bool UsesAuraAI() => false;

		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Moon Meteor");
		}

		public override void SafeSetDefaults() 
		{
			projectile.width = 40;
			projectile.height = 40;
			projectile.aiStyle = -1;
			projectile.friendly = true;
			projectile.ignoreWater = true;
			projectile.tileCollide = false;
			projectile.penetrate = -1;
			projectile.extraUpdates = 2;
		}

		public override void FirstTick()
		{
			projectile.scale = Main.rand.NextFloat(0.5f, 1.5f);
			projectile.Size = new Vector2((int)((float)(projectile.width) * projectile.scale), ((float)(projectile.height) * projectile.scale));
		}

		//private int dustTimer = 5;
		public override void SafeAI()
		{
			projectile.rotation += (0.5f / (projectile.extraUpdates + 1)) * projectile.direction;

			Vector2 targetPos = new Vector2(projectile.ai[0], projectile.ai[1]);
			if ((targetPos - projectile.Center).Length() < 40f)
            {
				projectile.Kill();
            }

			/*dustTimer--;
			if (dustTimer <= 0)
            {
				DelegateMethods.v3_1 = new Vector3(0.6f, 1f, 1f) * 0.2f;
				Utils.PlotTileLine(projectile.Center, projectile.Center + projectile.velocity * 10f, 8f, DelegateMethods.CastLightOpen);
				projectile.alpha = 0;
				projectile.scale = 1.1f;
				projectile.frame = Main.rand.Next(14);
				float num101 = 16f;
				for (int num102 = 0; (float)num102 < num101; num102++)
				{
					Vector2 spinningpoint5 = Vector2.UnitX * 0f;
					spinningpoint5 += -Vector2.UnitY.RotatedBy((float)num102 * ((float)Math.PI * 2f / num101)) * new Vector2(1f, 4f);
					spinningpoint5 = spinningpoint5.RotatedBy(projectile.velocity.ToRotation());
					int num103 = Dust.NewDust(projectile.Center, 0, 0, 229);
					Main.dust[num103].scale = 1.5f;
					Main.dust[num103].noGravity = true;
					Main.dust[num103].position = projectile.Center + spinningpoint5;
					Main.dust[num103].velocity = projectile.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 1f;
				}
				dustTimer = 10;
			}*/
		}

		public override void Kill(int timeLeft)
		{
			for (int k = 0; k < 3; k++)
			{
				Vector2 vectorToTargetPosition = new Vector2(projectile.Center.X - 10f, projectile.Center.Y - 10f) - projectile.Center;
				float distanceToTargetPosition = vectorToTargetPosition.Length();

				vectorToTargetPosition.Normalize();
				vectorToTargetPosition *= 12f;

				Main.PlaySound(2, (int)Main.player[projectile.owner].Center.X, (int)Main.player[projectile.owner].Center.Y, 88);
				Projectile.NewProjectile(projectile.Center, vectorToTargetPosition.RotatedByRandom(MathHelper.ToRadians(180f)), mod.ProjectileType("MoonStar"), (int)(projectile.damage * 0.8f), 0f, Main.myPlayer);
			}

			int num20 = 4;
			int num22 = 10;
			int num23 = 20;
			int num24 = 20;
			int num25 = 4;
			float num26 = 1.5f;
			int num27 = (int)(6f * projectile.scale);
			int num28 = 229;

			projectile.position = projectile.Center;
			projectile.width = (projectile.height = 16 * num27);
			projectile.Center = projectile.position;
			projectile.Damage();
			Main.PlaySound(2, projectile.position, 89);
			for (int num29 = 0; num29 < num20; num29++)
			{
				int num30 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 31, 0f, 0f, 100, default(Color), 1.5f);
				Main.dust[num30].position = projectile.Center + Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * projectile.width / 2f;
			}
			for (int num32 = 0; num32 < num23; num32++)
			{
				Dust dust17 = Dust.NewDustDirect(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, num28, 0f, 0f, 100, default(Color), 1.5f);
				dust17.position = projectile.Center + Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * projectile.width / 2f;
				Dust dust18 = dust17;
				Dust dust2 = dust18;
				dust2.velocity *= 2f;
				dust17.noGravity = true;
				dust17.fadeIn = num26;
			}
			for (int num33 = 0; num33 < num22; num33++)
			{
				int num34 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, num28, 0f, 0f, 0, default(Color), 2.7f);
				Main.dust[num34].position = projectile.Center + Vector2.UnitX.RotatedByRandom(3.1415927410125732).RotatedBy(projectile.velocity.ToRotation()) * projectile.width / 2f;
				Main.dust[num34].noGravity = true;
				Dust dust19 = Main.dust[num34];
				Dust dust2 = dust19;
				dust2.velocity *= 3f;
			}
			for (int num35 = 0; num35 < num24; num35++)
			{
				int num36 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 31, 0f, 0f, 0, default(Color), 1.5f);
				Main.dust[num36].position = projectile.Center + Vector2.UnitX.RotatedByRandom(3.1415927410125732).RotatedBy(projectile.velocity.ToRotation()) * projectile.width / 2f;
				Main.dust[num36].noGravity = true;
				Dust dust20 = Main.dust[num36];
				Dust dust2 = dust20;
				dust2.velocity *= 3f;
			}
			for (int num37 = 0; num37 < num25; num37++)
			{
				int num38 = Gore.NewGore(projectile.position + new Vector2((float)(projectile.width * Main.rand.Next(100)) / 100f, (float)(projectile.height * Main.rand.Next(100)) / 100f) - Vector2.One * 10f, default(Vector2), Main.rand.Next(61, 64));
				Main.gore[num38].position = projectile.Center + Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * projectile.width / 2f;
				Gore gore = Main.gore[num38];
				Gore gore2 = gore;
				gore2.position -= Vector2.One * 16f;
				if (Main.rand.Next(2) == 0)
				{
					Main.gore[num38].position.Y -= 30f;
				}
				gore = Main.gore[num38];
				gore2 = gore;
				gore2.velocity *= 0.3f;
				Main.gore[num38].velocity.X += (float)Main.rand.Next(-10, 11) * 0.05f;
				Main.gore[num38].velocity.Y += (float)Main.rand.Next(-10, 11) * 0.05f;
			}
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			Mod mod = ModLoader.GetMod("AuraClass");

			Texture2D texture = Main.projectileTexture[projectile.type];
			Vector2 textureVect = new Vector2((float)texture.Width / 2, (float)(texture.Height / 2));
			
			spriteBatch.Draw(texture, projectile.Center - Main.screenPosition, null, Lighting.GetColor((int)(projectile.Center.X / 16), (int)(projectile.Center.Y / 16)) * ((255 - projectile.alpha) / 255f), projectile.rotation, textureVect, projectile.scale, SpriteEffects.None, 0f);
			return false;
		}

		public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			if (target.defense < 9999)
			{
				damage = damage + (target.defense / 2);
			}
		}
	}
}
