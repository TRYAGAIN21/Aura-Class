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
	public class ShrimpySharkron : AuraProjectile
	{
		public override bool UsesAuraAI() => false;

		public override void SetStaticDefaults() 
		{
			Main.projFrames[projectile.type] = 6;
			DisplayName.SetDefault("Shrimpy Sharkron");
		}

		private const int projectileAuraRange = 80;

		public override void SafeSetDefaults() 
		{
			projectile.width = 10;
			projectile.height = 10;
			projectile.aiStyle = -1;
			projectile.friendly = true;
			projectile.ignoreWater = true;
			projectile.alpha = 255;

			projectile.penetrate = 3;
			projectile.maxPenetrate = 3;

			projectile.usesLocalNPCImmunity = true;
			projectile.localNPCHitCooldown = -1;
		}

		private NPC previousTarget = null;
		private NPC previousTarget_2 = null;

		private bool firstTick = true;
		private bool fall;

		public override void SafeAI()
		{
			if (firstTick)
            {
				Main.PlaySound(4, (int)projectile.position.X, (int)projectile.position.Y, 19);
				firstTick = false;
			}

			projectile.alpha -= 40;
			if (projectile.alpha < 0)
			{
				projectile.alpha = 0;
			}
			projectile.spriteDirection = projectile.direction;

			if (fall)
			{
				projectile.velocity.X *= 0.98f;
				projectile.velocity.Y += 0.05f;
				projectile.rotation = projectile.velocity.ToRotation();
				if (projectile.direction == -1)
				{
					projectile.rotation += (float)Math.PI;
				}
				projectile.frameCounter++;
				if (projectile.frameCounter > 3)
				{
					projectile.frame++;
					if (projectile.frame > 5)
					{
						projectile.frame = 0;
					}
					projectile.frameCounter = 0;
				}
				return;
			}

			int RealRangeNormal = projectileAuraRange * 16;
			int RealRangePrefix = auraRangePrefix * 16;
			int RealRange = RealRangeNormal + RealRangePrefix;

			int RealPlayerRange = AuraDamagePlayer.ModPlayer(Main.player[projectile.owner]).auraSize * 16;

			Vector2 vectorToAuraPosition = projectile.Center - Main.player[projectile.owner].Center;
			float distanceToAuraPosition = vectorToAuraPosition.Length();

			NPC target = null;
			if (Main.npc[(int)projectile.ai[0]] != null)
            {
				target = Main.npc[(int)projectile.ai[0]];
			}
			if (projectile.penetrate < projectile.maxPenetrate || Main.npc[(int)projectile.ai[0]] == null || !Main.npc[(int)projectile.ai[0]].active)
            {
				for (int k = 0; k < Main.maxNPCs; k++)
				{
					NPC npc = Main.npc[k];

					if (npc.CanBeChasedBy(projectile) && previousTarget != npc && previousTarget_2 != npc && Collision.CanHitLine(projectile.Center, projectile.width, projectile.height, npc.Center, 1, 1))
					{
						float distance2TargetPosition = (npc.Center - Main.player[projectile.owner].Center).Length();

						if (distance2TargetPosition <= (RealRange + RealPlayerRange) / 2)
						{
							target = npc;
						}
					}
				}
			}

			if (target != null && target.active && previousTarget != target && previousTarget_2 != target && target.CanBeChasedBy(projectile) && (target.Center - Main.player[projectile.owner].Center).Length() <= ((RealRange + RealPlayerRange) / 2))
			{
				Vector2 goalVelocity = (target.Center - projectile.Center).SafeNormalize(Vector2.Zero) * 16f;
				projectile.velocity += (goalVelocity - projectile.velocity) / 10f;
			}

			if (distanceToAuraPosition > (RealRange + RealPlayerRange) / 2)
			{
				fall = true;
			}

			projectile.frameCounter++;
			if (projectile.frameCounter > 3)
			{
				projectile.frame++;
				if (projectile.frame > 5)
				{
					projectile.frame = 0;
				}
				projectile.frameCounter = 0;
			}

			projectile.rotation = projectile.velocity.ToRotation();
			if (projectile.direction == -1)
			{
				projectile.rotation += (float)Math.PI;
			}
		}

		public override void Kill(int timeLeft)
        {
			for (int num325 = 0; num325 < 15; num325++)
			{
				int num326 = Dust.NewDust(projectile.Center - Vector2.One * 10f, 50, 50, 5, 0f, -2f);
				Dust dust112 = Main.dust[num326];
				Dust dust2 = dust112;
				dust2.velocity /= 2f;
			}
			int num327 = 0;
			int num328 = 10;
			num327 = Gore.NewGore(projectile.Center, projectile.velocity * 0.3f, 584);
			Gore gore17 = Main.gore[num327];
			Gore gore2 = gore17;
			gore2.timeLeft /= num328;
			num327 = Gore.NewGore(projectile.Center, projectile.velocity * 0.4f, 585);
			gore17 = Main.gore[num327];
			gore2 = gore17;
			gore2.timeLeft /= num328;
			num327 = Gore.NewGore(projectile.Center, projectile.velocity * 0.5f, 586);
			gore17 = Main.gore[num327];
			gore2 = gore17;
			gore2.timeLeft /= num328;
		}

		public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
		{
			width = 10;
			height = 10;
			fallThrough = true;
			return base.TileCollideStyle(ref width, ref height, ref fallThrough);
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			Mod mod = ModLoader.GetMod("AuraClass");

			int textureFrames = Main.projFrames[projectile.type];
			float textureDrawX = (int)projectile.position.X + projectile.width / 2;
			float textureDrawY = (int)((projectile.position.Y + projectile.height / 2) - 1);
			Texture2D texture = mod.GetTexture("Projectiles/ShrimpySharkron");
			Vector2 texturePos = new Vector2(textureDrawX, textureDrawY);
			Rectangle textureRect = new Rectangle(0, projectile.frame * (texture.Height / textureFrames), (texture.Width), (texture.Height / textureFrames));
			Vector2 textureVect = new Vector2((float)texture.Width / 2, (float)(texture.Height / (2 * textureFrames)));

			spriteBatch.Draw(texture, texturePos - Main.screenPosition, new Rectangle?(textureRect), Lighting.GetColor((int)(projectile.Center.X / 16), (int)(projectile.Center.Y / 16)), projectile.rotation, textureVect, 1f, projectile.spriteDirection == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0f);
			return false;
		}

		public override bool? SafeCanHitNPC(NPC target)
        {
			if (previousTarget == target || previousTarget_2 == target || fall)
            {
				return false;
            }
			return null;
        }

		public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
			if (previousTarget == null)
            {
				previousTarget = target;
			}
			else if (previousTarget_2 == null)
            {
				previousTarget_2 = target;
			}

			if (target.defense < 9999)
			{
				damage = damage + (target.defense / 4);
			}
		}
	}
}
