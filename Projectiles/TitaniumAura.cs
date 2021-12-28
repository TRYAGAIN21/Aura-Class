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
	public class TitaniumAura : AuraProjectile
	{
		public override string Texture => "AuraClass/Assets/BlankTexture";

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Titanium Aura");
			Main.projFrames[projectile.type] = 4;
		}

		public override void SafeSetDefaults() {
			projectile.aiStyle = -1;
			projectile.friendly = true;
			projectile.usesLocalNPCImmunity = true;
			projectile.localNPCHitCooldown = 22;

			auraRange = 38;
			dustType = -1;
		}

		public override bool PreAI()
		{
			projectile.rotation += 0.05f;

			Player player = Main.player[projectile.owner];
			if (Main.rand.NextBool())
			{
				int num5 = Dust.NewDust(player.position, player.width, player.height, 91, 0f, 0f, 200, default(Color), 0.5f);
				Main.dust[num5].noGravity = true;
				Main.dust[num5].velocity *= 0.75f;
				Main.dust[num5].fadeIn = 1.3f;
				Vector2 vector = new Vector2(Main.rand.Next(-100, 101), Main.rand.Next(-100, 101));
				vector.Normalize();
				vector *= (float)Main.rand.Next(50, 100) * 0.04f;
				Main.dust[num5].velocity = vector;
				vector.Normalize();
				vector *= 34f;
				Main.dust[num5].position = player.Center - vector;
			}
			return true;
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			Mod mod = ModLoader.GetMod("AuraClass");

			Texture2D auraTex = mod.GetTexture("Projectiles/TitaniumShard");
			Vector2 textureVect = new Vector2((float)auraTex.Width / 2, (float)(auraTex.Height / (2 * Main.projFrames[projectile.type])));

			float spinSpeed = CurrentRange;
			Vector2 spinningpoint = new Vector2(0f, 0f - spinSpeed);

			int frame = 0;
			int ShardCount = 0;

			while (true)
			{
				ShardCount++;

				if (ShardCount > 19)
					break;

				if (ShardCount == 0
					|| ShardCount == 0 + Main.projFrames[projectile.type]
					|| ShardCount == 0 + (Main.projFrames[projectile.type] * 2)
					|| ShardCount == 0 + (Main.projFrames[projectile.type] * 3)
					|| ShardCount == 0 + (Main.projFrames[projectile.type] * 4)
					|| ShardCount == 0 + (Main.projFrames[projectile.type] * 5))
				{
					frame = 0;
				}

				if (ShardCount == 1 
					|| ShardCount == 1 + Main.projFrames[projectile.type] 
					|| ShardCount == 1 + (Main.projFrames[projectile.type] * 2) 
					|| ShardCount == 1 + (Main.projFrames[projectile.type] * 3) 
					|| ShardCount == 1 + (Main.projFrames[projectile.type] * 4) 
					|| ShardCount == 1 + (Main.projFrames[projectile.type] * 5))
                {
					frame = 1;
				}

				if (ShardCount == 2
					|| ShardCount == 2 + Main.projFrames[projectile.type]
					|| ShardCount == 2 + (Main.projFrames[projectile.type] * 2)
					|| ShardCount == 2 + (Main.projFrames[projectile.type] * 3)
					|| ShardCount == 2 + (Main.projFrames[projectile.type] * 4)
					|| ShardCount == 2 + (Main.projFrames[projectile.type] * 5))
				{
					frame = 2;
				}

				if (ShardCount == 3
					|| ShardCount == 3 + Main.projFrames[projectile.type]
					|| ShardCount == 3 + (Main.projFrames[projectile.type] * 2)
					|| ShardCount == 3 + (Main.projFrames[projectile.type] * 3)
					|| ShardCount == 3 + (Main.projFrames[projectile.type] * 4)
					|| ShardCount == 3 + (Main.projFrames[projectile.type] * 5))
				{
					frame = 3;
				}


				Rectangle textureRect = new Rectangle(0, frame * (auraTex.Height / Main.projFrames[projectile.type]), (auraTex.Width), (auraTex.Height / Main.projFrames[projectile.type]));

				Vector2 position9 = spinningpoint.RotatedBy(projectile.rotation + MathHelper.ToRadians((ShardCount + 1f) * 20f)) + projectile.Center - Main.screenPosition;
				Main.spriteBatch.Draw(auraTex, position9, textureRect, Lighting.GetColor((int)(projectile.Center.X / 16), (int)(projectile.Center.Y / 16)), projectile.rotation + MathHelper.ToRadians(((ShardCount + 1f) * 20f) - 90f), auraTex.Size() * 0.5f, projectile.scale, SpriteEffects.None, 0f);
			}
			return false;
		}
	}
}
