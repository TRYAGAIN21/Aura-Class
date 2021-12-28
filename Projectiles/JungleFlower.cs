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
	public class JungleFlower : AuraProjectile
	{
		public override bool UsesAuraAI() => false;

		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Blossomed Flower");
			Main.projFrames[projectile.type] = 2;
		}

		private const int projectileAuraRange = 29;
		private const int timeMax = 180;

		public override void SafeSetDefaults() 
		{
			projectile.width = 10;
			projectile.height = 10;
			projectile.aiStyle = -1;
			projectile.friendly = true;
			projectile.ignoreWater = true;
			projectile.tileCollide = false;
			projectile.timeLeft = timeMax * (projectile.extraUpdates + 1);
			projectile.scale = 0.5f;
			projectile.penetrate = -1;
		}

		private Vector2 HostPos;
		public override void FirstTick()
        {
			HostPos = Main.player[projectile.owner].Center;
			projectile.rotation += projectile.ai[0] == -1 ? 45f : 0f;
		}

		private bool lastPhase = false;
		private bool OutsideAura = false;
		public override void SafeAI()
		{
			int RealRangeNormal = projectileAuraRange * 16;
			int RealRangePrefix = auraRangePrefix * 16;
			int RealRange = RealRangeNormal + RealRangePrefix;
			int RealPlayerRange = AuraDamagePlayer.ModPlayer(Main.player[projectile.owner]).auraSize * 16;
			int TrueRange = (RealRange + RealPlayerRange) / 2;

			int TripleRangeNormal = (projectileAuraRange * 3) * 16;
			int TripleRange = TripleRangeNormal + RealRangePrefix;
			int TrueTripleRange = (TripleRange + RealPlayerRange) / 2;

			Vector2 vectorToAuraPosition = projectile.Center - HostPos;
			float distanceToAuraPosition = vectorToAuraPosition.Length();

			if (distanceToAuraPosition > (RealRange + RealPlayerRange) / 2)
			{
				OutsideAura = true;
			}
			if (OutsideAura)
			{
				if (projectile.timeLeft <= timeMax / 2 && projectile.timeLeft > (timeMax / 2 - 30))
				{
					projectile.scale += 0.5f / 30;
					projectile.rotation += 0.2f;
				}
				if (projectile.timeLeft <= (timeMax / 2 - 30))
				{
					if (!lastPhase)
					{
						projectile.velocity = new Vector2(0f, 0f);
						Vector2 baseVel = projectile.ai[0] == -1 ? Vector2.UnitX.RotatedBy((Math.PI * 2 / 8 * 0.5)) : Vector2.UnitX;
						for (int k = 0; k < 8; k++)
						{
							Projectile.NewProjectile(projectile.Center, 20f * baseVel.RotatedBy(Math.PI * 2 / 8 * k), mod.ProjectileType("JunglePetal"), projectile.damage, 0f, Main.myPlayer);
						}

						projectile.frame = 1;
						for (int k = 0; k < Main.maxNPCs; k++)
						{
							NPC npc = Main.npc[k];
							if (npc.CanBeChasedBy(this) && (projectile.Center - Main.player[projectile.owner].Center).Length() <= TrueTripleRange)
							{
								Vector2 velocity = npc.Center - projectile.Center;
								velocity.Normalize();

								projectile.velocity = velocity * 30f;
								lastPhase = true;
								break;
							}
							else if (k == Main.maxNPCs - 1)
                            {
								Vector2 velocity = projectile.Center - HostPos;
								velocity.Normalize();

								projectile.velocity = velocity * 30f;
								lastPhase = true;
								break;
							}
						}
						lastPhase = true;
					}
					if (projectile.velocity.X != 0f || projectile.velocity.Y != 0f)
					{
						projectile.rotation += 0.2f;
					}
				}
				else
                {
					projectile.velocity *= 0.94f;
				}
			}
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			Mod mod = ModLoader.GetMod("AuraClass");

			int textureFrames = Main.projFrames[projectile.type];
			float textureDrawX = (int)projectile.position.X + projectile.width / 2;
			float textureDrawY = (int)(projectile.position.Y + projectile.height / 2);
			Texture2D texture = mod.GetTexture("Projectiles/JungleFlower");
			Vector2 texturePos = new Vector2(textureDrawX, textureDrawY);
			Rectangle textureRect = new Rectangle(0, projectile.frame * (texture.Height / textureFrames), (texture.Width), (texture.Height / textureFrames));
			Vector2 textureVect = new Vector2((float)texture.Width / 2, (float)(texture.Height / (2 * textureFrames)));

			spriteBatch.Draw(texture, texturePos - Main.screenPosition, new Rectangle?(textureRect), Lighting.GetColor((int)(projectile.Center.X / 16), (int)(projectile.Center.Y / 16)) * ((255 - projectile.alpha) / 255f), projectile.rotation, textureVect, projectile.scale, SpriteEffects.None, 0f);
			return false;
		}
	}
}
