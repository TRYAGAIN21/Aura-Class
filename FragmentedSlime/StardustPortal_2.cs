using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AuraClass.Projectiles.FragmentedSlime
{
	public class StardustPortal_2 : ModProjectile
	{
		public override bool Autoload(ref string name)
		{
			return false;
		}

		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Stardust Gateway");
			Main.projFrames[projectile.type] = 8;
		}

		private float[] portalAI = new float[4];

		public override void SetDefaults() 
		{
			projectile.width = 20;
			projectile.height = 20;
			projectile.aiStyle = -1;
			projectile.hostile = true;
			projectile.ignoreWater = true;
			projectile.tileCollide = false;
			projectile.penetrate = -1;
			projectile.alpha = 255;
		}

        public override void AI()
        {
			NPC master = Main.npc[(int)projectile.ai[0]];
            Player player = Main.player[master.target];

			if (portalAI[3] == 0f)
            {
				portalAI[0] = (((float)(master.life) * 0.01f) / (Main.expertMode ? 4 : 2));
				portalAI[1] = (((float)(master.life) * 0.01f) / (Main.expertMode ? 4 : 1)) * 5f;
				portalAI[3] = 1f;
			}

			if (projectile.alpha > 0)
            {
				projectile.alpha -= 255 / 30;
            }

			if (master != null && !(master.life <= 0f) && master.active && master.type == ModContent.NPCType<NPCs.FragmentSlime>())
            {
				projectile.timeLeft = 31;
			}

            if (master.life <= 0f)
            {
				projectile.alpha += 255 / 30;
			}

			if ((new Vector2(master.Center.X, master.Center.Y - 80f) - projectile.Center).Length() < 50f)
            {
				projectile.velocity *= new Vector2(0.8f, 0.8f);
			}
			else
            {
				Vector2 velocity = new Vector2(master.Center.X, master.Center.Y - 80f) - projectile.Center;
				velocity.Normalize();
				velocity *= 10f;

				projectile.velocity = velocity;
			}

			if (player != null && !player.dead)
			{
				portalAI[0]--;
				if (portalAI[0] <= 0f)
				{
					Projectile.NewProjectile(projectile.Center, (player.Center - projectile.Center).SafeNormalize(Vector2.Zero).RotatedByRandom(MathHelper.ToRadians(2f)) * 15f, 462, 25, 0f, Main.myPlayer);
					portalAI[0] += (((float)(master.life) * 0.01f) / (Main.expertMode ? 4 : 2));

					if (portalAI[0] < 5f)
                    {
						portalAI[0] = 5f;
					}
				}

				if (NPC.CountNPCS(409) < 3)
				{
					portalAI[1]--;
					if (portalAI[1] <= 0f)
					{
						NPC twinkle = Main.npc[NPC.NewNPC((int)projectile.Center.X, (int)projectile.Center.Y, 409)];
						twinkle.GetGlobalNPC<NPCs.GlobalFragmentedNPC>().StardustSlimeMaster = master;
						twinkle.GetGlobalNPC<NPCs.GlobalFragmentedNPC>().StardustSlimeMinion = true;
						twinkle.target = master.target;

						portalAI[1] += (((float)(master.life) * 0.01f) / (Main.expertMode ? 2 : 1)) * 3f;
					}
				}
			}

			projectile.frameCounter++;
			if (projectile.frameCounter > 5)
			{
				projectile.frame++;
				if (projectile.frame > Main.projFrames[projectile.type] - 1)
				{
					projectile.frame = 0;
				}
				projectile.frameCounter = 0;
			}
		}

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			Mod mod = ModLoader.GetMod("AuraClass");

			int textureFrames = Main.projFrames[projectile.type];
			Texture2D texture = Main.projectileTexture[projectile.type];
			Rectangle textureRect = new Rectangle(0, projectile.frame * (texture.Height / textureFrames), (texture.Width), (texture.Height / textureFrames));
			Vector2 textureVect = new Vector2((float)texture.Width / 2, (float)(texture.Height / (2 * textureFrames)));

			spriteBatch.Draw(texture, projectile.Center - Main.screenPosition, new Rectangle?(textureRect), Color.White * ((255 - projectile.alpha) / 255f), projectile.rotation, textureVect, projectile.scale, SpriteEffects.None, 0f);
			return false;
		}
	}
}
