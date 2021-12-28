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
	public class MoonBolt : AuraProjectile
	{
		public override bool UsesAuraAI() => false;

		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Moon Bolt");
			Main.projFrames[projectile.type] = 4;
		}

		public override void SafeSetDefaults() 
		{
			projectile.width = 10;
			projectile.height = 10;
			projectile.aiStyle = -1;
			projectile.friendly = true;
			projectile.ignoreWater = true;
			projectile.tileCollide = false;
			projectile.extraUpdates = 5;
			projectile.timeLeft = 60 * (projectile.extraUpdates + 1);
		}

		public override void SafeAI()
		{
			projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);

			projectile.frameCounter++;
			if (projectile.frameCounter > 5 * (projectile.extraUpdates + 1))
			{
				projectile.frame++;
				if (projectile.frame > 3)
				{
					projectile.frame = 0;
				}
				projectile.frameCounter = 0;
			}
		}

		public override void Kill(int timeLeft)
		{
			Main.PlaySound(SoundID.Item10, projectile.position);
			int num539 = 3;
			for (int num541 = 0; num541 < num539; num541++)
			{
				Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, 229, projectile.velocity.X * 0.05f, projectile.velocity.Y * 0.05f, 150, default(Color), 1.2f)].noGravity = true;
			}
		}

		public override Color? GetAlpha(Color lightColor)
        {
			return Color.White;
        }

		public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			if (target.defense < 9999)
			{
				damage = damage + (target.defense / 2);
			}
		}

		public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
		{
			Player player = Main.player[projectile.owner];

			Rectangle playerHitbox = new Rectangle(0, 0, (player.width), (player.height));

			float point = 0f;
			if (Collision.CheckAABBvLineCollision(playerHitbox.TopLeft(), playerHitbox.Size(), projectile.position, projectile.position + new Vector2(projectile.width, projectile.height), projectile.width, ref point))
			{
				projectile.Kill();
			}
			return null;
		}
	}
}
