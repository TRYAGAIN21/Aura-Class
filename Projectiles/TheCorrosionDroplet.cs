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
	public class TheCorrosionDroplet : AuraProjectile
	{
		public override string Texture => "AuraClass/Assets/BlankTexture";

		public override bool UsesAuraAI() => false;

		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Ichor Droplet");
		}

		public override void SafeSetDefaults() 
		{
			projectile.width = 10;
			projectile.height = 10;
			projectile.aiStyle = -1;
			projectile.friendly = true;
			projectile.alpha = 255;
			projectile.timeLeft = 120;
			projectile.tileCollide = false;

			projectile.extraUpdates = 3;
		}

		public override void SafeAI()
		{
			Player player = Main.player[projectile.owner];

			for (int num92 = 0; num92 < 3; num92++)
			{
				float num93 = projectile.velocity.X / 3f * (float)num92;
				float num94 = projectile.velocity.Y / 3f * (float)num92;
				int num95 = 4;
				int num96 = Dust.NewDust(new Vector2(projectile.position.X + (float)num95, projectile.position.Y + (float)num95), projectile.width - num95 * 2, projectile.height - num95 * 2, 170, 0f, 0f, 100, default(Color), 1.2f);
				Main.dust[num96].noGravity = true;
				Dust dust15 = Main.dust[num96];
				Dust dust2 = dust15;
				dust2.velocity *= 0.1f;
				dust15 = Main.dust[num96];
				dust2 = dust15;
				dust2.velocity += projectile.velocity * 0.1f;
				Main.dust[num96].position.X -= num93;
				Main.dust[num96].position.Y -= num94;
			}
			if (Main.rand.Next(5) == 0)
			{
				int num97 = 4;
				int num98 = Dust.NewDust(new Vector2(projectile.position.X + (float)num97, projectile.position.Y + (float)num97), projectile.width - num97 * 2, projectile.height - num97 * 2, 170, 0f, 0f, 100, default(Color), 0.6f);
				Dust dust16 = Main.dust[num98];
				Dust dust2 = dust16;
				dust2.velocity *= 0.25f;
				dust16 = Main.dust[num98];
				dust2 = dust16;
				dust2.velocity += projectile.velocity * 0.5f;
			}
		}

		public override void Kill(int timeLeft)
		{
			for (int num612 = 0; num612 < 30; num612++)
			{
				int num613 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 170, projectile.velocity.X * 0.1f, projectile.velocity.Y * 0.1f, 100);
				Main.dust[num613].noGravity = true;
				Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 170, projectile.velocity.X * 0.1f, projectile.velocity.Y * 0.1f, 100, default(Color), 0.5f);
			}
		}
	}
}
