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
	public class ShrimpyBubble : AuraProjectile
	{
		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Bubble Aura");
		}

		public override void SafeSetDefaults() {
			projectile.extraUpdates = 0;
			projectile.aiStyle = -1;
			projectile.friendly = true;
			projectile.usesLocalNPCImmunity = true;
			projectile.localNPCHitCooldown = 24;

			auraRange = 20;
		}

		private int sharkronTimer;
		private int Counter;

		public override bool PreAI()
		{
			projectile.alpha = 175;
			int RealRangeNormal = auraRange * 16;
			int RealRangePrefix = auraRangePrefix * 16;
			int RealRange = RealRangeNormal + RealRangePrefix;

			int RealPlayerRange = AuraDamagePlayer.ModPlayer(Main.player[projectile.owner]).auraSize * 16;

			sharkronTimer++;
			if (sharkronTimer > 45)
			{
				for (int k = 0; k < Main.maxNPCs; k++)
				{
					if (Counter > 4)
					{
						Counter = 0;
						break;
					}
					NPC npc = Main.npc[k];

					if (npc.CanBeChasedBy(this) && Collision.CanHitLine(Main.player[projectile.owner].Center, 1, 1, npc.Center, 1, 1))
					{
						Vector2 vectorToTargetPosition = npc.Center - projectile.Center;
						float distanceToTargetPosition = vectorToTargetPosition.Length();

						if (distanceToTargetPosition <= ((RealRange + RealPlayerRange) * 4) / 2)
						{
							vectorToTargetPosition.Normalize();
							vectorToTargetPosition *= 8f;

							Counter += 1;

							Projectile.NewProjectile(Main.player[projectile.owner].Center + npc.velocity, vectorToTargetPosition, mod.ProjectileType("ShrimpySharkron"), ((projectile.damage / 2) + (projectile.damage / 4)), 0f, Main.myPlayer, npc.whoAmI);
						}
					}
				}
				sharkronTimer = 0;
			}
			else { Counter = 0; }
			return true;
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			Mod mod = ModLoader.GetMod("AuraClass");

			Texture2D auraTex = mod.GetTexture("Projectiles/ShrimpyBubble_Aura");
			Vector2 auraVect = new Vector2((float)auraTex.Width / 2, (float)(auraTex.Height / 2));

			float textureDrawX = (int)projectile.position.X + projectile.width / 2;
			float textureDrawY = (int)((projectile.position.Y + projectile.height / 2) - 1);
			Texture2D texture = mod.GetTexture("Projectiles/ShrimpyBubble");
			Vector2 texturePos = new Vector2(textureDrawX, textureDrawY);
			Vector2 textureVect = new Vector2((float)texture.Width / 2, (float)(texture.Height / 2));

			spriteBatch.Draw(auraTex, texturePos - Main.screenPosition, null, Lighting.GetColor((int)(projectile.Center.X / 16), (int)(projectile.Center.Y / 16)) * ((255 - projectile.alpha) / 255f), projectile.rotation, auraVect, 2f, SpriteEffects.None, 0f);
			spriteBatch.Draw(texture, texturePos - Main.screenPosition, null, Lighting.GetColor((int)(projectile.Center.X / 16), (int)(projectile.Center.Y / 16)) * ((255 - projectile.alpha) / 255f), projectile.rotation, textureVect, 1f, SpriteEffects.None, 0f);
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
