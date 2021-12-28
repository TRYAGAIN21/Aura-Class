using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AuraClass.Projectiles.FragmentedSlime
{
	public class SolarShot : ModProjectile
	{
		public override bool Autoload(ref string name)
		{
			return false;
		}

		public override string Texture => "Terraria/Projectile_668";

		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Solar Fireball");
		}

		public override void SetDefaults() 
		{
			projectile.width = 16;
			projectile.height = 16;
			projectile.aiStyle = -1;
			projectile.hostile = true;
			projectile.magic = true;
			projectile.ignoreWater = true;
			projectile.tileCollide = false;
			projectile.timeLeft = 60;
			projectile.penetrate = -1;
		}

		public override void AI()
        {
			int num = 3;
			for (int i = 0; i < num; i++)
			{
				if (Main.rand.Next(2) != 0)
				{
					Dust dust = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, 6, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 100, default(Color), 2f);
					dust.noGravity = true;
					dust.velocity *= 0.3f;
					if (Main.rand.Next(1) == 0)
					{
						dust.velocity.Y += (float)Math.Sign(dust.velocity.Y) * 1.2f;
						dust.fadeIn += 0.5f;
					}
				}
			}
			projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
        }

		public override void Kill(int timeLeft)
		{
			int num20 = 4;
			int num21 = 20;
			int num22 = 10;
			int num23 = 20;
			int num24 = 20;
			int num25 = 4;
			float num26 = 1.5f;
			int num27 = 6;
			int num28 = 6;

			projectile.position = projectile.Center;
			projectile.width = (projectile.height = 16 * num27);
			projectile.Center = projectile.position;
			projectile.Damage();
			Main.PlaySound(SoundID.Item100, projectile.position);
			for (int num29 = 0; num29 < num20; num29++)
			{
				int num30 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 31, 0f, 0f, 100, default(Color), 1.5f);
				Main.dust[num30].position = projectile.Center + Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * projectile.width / 2f;
			}
			for (int num31 = 0; num31 < num21; num31++)
			{
				Dust dust15 = Dust.NewDustDirect(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 6, 0f, 0f, 200, default(Color), 2.5f);
				dust15.position = projectile.Center + Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * projectile.width / 10f;
				Dust dust16 = dust15;
				Dust dust2 = dust16;
				dust2.velocity *= 16f;
				if (dust15.velocity.Y > -2f)
				{
					dust15.velocity.Y *= -0.4f;
				}
				dust15.noLight = true;
				dust15.noGravity = true;
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
				int num34 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 6, 0f, 0f, 0, default(Color), 2.7f);
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
	}
}
