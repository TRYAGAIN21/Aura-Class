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
	public class MoonStar : AuraProjectile
	{
		public override bool UsesAuraAI() => false;

		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Moon Star");
		}

		public override void SafeSetDefaults() 
		{
			projectile.width = 10;
			projectile.height = 10;
			projectile.aiStyle = -1;
			projectile.friendly = true;
			projectile.ignoreWater = true;
			projectile.tileCollide = false;
			projectile.penetrate = -1;
			projectile.extraUpdates = 2;
			projectile.timeLeft = 120;
		}

		public override void SafeAI()
		{
			projectile.rotation += (Math.Abs(projectile.velocity.X) + Math.Abs(projectile.velocity.Y)) * 0.01f * (float)projectile.direction;

			projectile.light = 0.9f;
			if (Main.rand.Next(10) == 0)
			{
				Vector2 position30 = projectile.position;
				int width27 = projectile.width;
				int height27 = projectile.height;
				Main.dust[Dust.NewDust(position30, width27, height27, 229, 0f, 0f, 150, default(Color), 1.2f)].noGravity = true;
			}
		}

		public override void Kill(int timeLeft)
		{
			AuraDamagePlayer modPlayer = AuraDamagePlayer.ModPlayer(Main.player[projectile.owner]);

			int RealRangeNormal = 70 * 16;
			int RealRangePrefix = auraRangePrefix * 16;
			int RealRange = RealRangeNormal + RealRangePrefix;

			int RealPlayerRange = modPlayer.auraSize * 16;

			int CurrentRange = (RealRange + RealPlayerRange) / 2;

			float num481 = projectile.Center.X;
			float num482 = projectile.Center.Y;
			float num483 = (float)CurrentRange;
			bool flag17 = false;
			int num484 = -1;
			for (int num485 = 0; num485 < 200; num485++)
			{
				if (Main.npc[num485].CanBeChasedBy(this) && Main.player[projectile.owner].Distance(Main.npc[num485].Center) <= num483)
				{
					float num486 = Main.npc[num485].position.X + (float)(Main.npc[num485].width / 2);
					float num487 = Main.npc[num485].position.Y + (float)(Main.npc[num485].height / 2);
					float num488 = Math.Abs(Main.player[projectile.owner].position.X + (float)(Main.player[projectile.owner].width / 2) - num486) + Math.Abs(Main.player[projectile.owner].position.Y + (float)(Main.player[projectile.owner].height / 2) - num487);
					if (num488 < num483)
					{
						num483 = num488;
						num481 = num486;
						num482 = num487;
						flag17 = true;
						num484 = num485;
					}
				}
			}

			if (num484 != -1)
			{
				Vector2 vectorToTargetPosition = Main.npc[num484].Center - projectile.Center;
				vectorToTargetPosition.Normalize();
				vectorToTargetPosition *= 8f;

				Projectile.NewProjectile(projectile.Center, vectorToTargetPosition, mod.ProjectileType("MoonBolt"), (int)(projectile.damage * 0.8f), 0f, Main.myPlayer);
			}
			else
			{
				Vector2 vectorToTargetPosition = Main.player[projectile.owner].Center - projectile.Center;
				vectorToTargetPosition.Normalize();
				vectorToTargetPosition *= 8f;

				Projectile.NewProjectile(projectile.Center, vectorToTargetPosition, mod.ProjectileType("MoonBolt"), (int)(projectile.damage * 0.8f), 0f, Main.myPlayer);
			}

			Main.PlaySound(SoundID.Item10, projectile.position);
			for (int num541 = 0; num541 < 15; num541++)
			{
				Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, 229, projectile.velocity.X * 0.1f, projectile.velocity.Y * 0.1f, 150, default(Color), 1.2f)].noGravity = true;
			}
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			Mod mod = ModLoader.GetMod("AuraClass");

			Texture2D texture = Main.projectileTexture[projectile.type];
			Vector2 textureVect = new Vector2((float)texture.Width / 2, (float)(texture.Height / 2));

			spriteBatch.Draw(texture, projectile.Center - Main.screenPosition, null, Color.White * ((255 - projectile.alpha) / 255f), projectile.rotation, textureVect, projectile.scale, SpriteEffects.None, 0f);
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
