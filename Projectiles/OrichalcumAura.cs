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
	public class OrichalcumAura : AuraProjectile
	{
		public override string Texture => "AuraClass/Assets/BlankTexture";

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Orichalcum Aura");
			Main.projFrames[projectile.type] = 5;
		}

		public override void SafeSetDefaults() {
			projectile.aiStyle = -1;
			projectile.friendly = true;
			projectile.usesLocalNPCImmunity = true;
			projectile.localNPCHitCooldown = 22;

			auraRange = 42;
			dustType = -1;
		}

		private int petalTimer;
		private float rotation;
		public override bool PreAI()
		{
			projectile.rotation += 0.05f;
			projectile.frameCounter++;
			if (projectile.frameCounter > 5)
			{
				projectile.frame++;
				if (projectile.frame > 4)
				{
					projectile.frame = 0;
				}
				projectile.frameCounter = 0;
			}

			petalTimer++;
			if (petalTimer > 5)
			{
				Vector2 velocity = (new Vector2(Main.player[projectile.owner].Center.X, Main.player[projectile.owner].Center.Y - 40f) - Main.player[projectile.owner].Center).RotatedBy(MathHelper.ToRadians(rotation));
				velocity.Normalize();
				velocity *= 30f / 4f;

				for (int i = 0; i < 2; i++)
                {
					Projectile.NewProjectile(Main.player[projectile.owner].Center, velocity * (i == 1 ? -1 : 1), mod.ProjectileType("OrichalcumPetal"), ((projectile.damage / 2) + (projectile.damage / 4)), 0f, Main.myPlayer);
				}
				petalTimer = 0;

				rotation += 5f;
			}
			return true;
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			Mod mod = ModLoader.GetMod("AuraClass");

			Texture2D auraTex = mod.GetTexture("Projectiles/OrichalcumPetal");
			Vector2 textureVect = new Vector2((float)auraTex.Width / 2, (float)(auraTex.Height / (2 * Main.projFrames[projectile.type])));

			float spinSpeed = CurrentRange;
			Vector2 spinningpoint = new Vector2(0f, 0f - spinSpeed);
			for (int num185 = 0; (float)num185 < 20f; num185++)
			{
				int frame = projectile.frame;
				frame += num185;

				if (frame == 5 || frame == 5 + 5 || frame == 5 + 10 || frame == 5 + 15)
				{
					frame = 0;
				}
				if (frame == 6 || frame == 6 + 5 || frame == 6 + 10 || frame == 6 + 15)
				{
					frame = 1;
				}
				if (frame == 7 || frame == 7 + 5 || frame == 7 + 10 || frame == 7 + 15)
				{
					frame = 2;
				}
				if (frame == 8 || frame == 8 + 5 || frame == 8 + 10 || frame == 8 + 15)
				{
					frame = 3;
				}
				if (frame == 9 || frame == 9 + 5 || frame == 9 + 10 || frame == 9 + 15)
				{
					frame = 4;
				}

				Rectangle textureRect = new Rectangle(0, frame * (auraTex.Height / Main.projFrames[projectile.type]), (auraTex.Width), (auraTex.Height / Main.projFrames[projectile.type]));

				Vector2 position9 = spinningpoint.RotatedBy(projectile.rotation + MathHelper.ToRadians((num185 + 1f) * 20f)) + projectile.Center - Main.screenPosition;
				Main.spriteBatch.Draw(auraTex, position9, textureRect, Lighting.GetColor((int)(projectile.Center.X / 16), (int)(projectile.Center.Y / 16)), projectile.rotation + MathHelper.ToRadians((num185 + 1f) * 20f), auraTex.Size() * 0.5f, projectile.scale, SpriteEffects.None, 0f);
			}
			return false;
		}
	}
}
