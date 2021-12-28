using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;

namespace AuraClass.NPCs
{
	public class FragmentationSlime : ModNPC
	{
        public override bool Autoload(ref string name)
        {
            return false;
        }

        public override string Texture => "Terraria/NPC_244";
        
        public override void SetStaticDefaults() {
			//DisplayName.SetDefault("Fragmentation Slime");
			Main.npcFrameCount[npc.type] = 4;
		}

		public override void SetDefaults() {
			npc.aiStyle = -1;
            npc.width = 60;
			npc.height = 42;
			npc.defense = 0;
			npc.damage = 25;
			npc.lifeMax = 100;
			npc.knockBackResist = 0.5f;
			npc.npcSlots = 1f;
			npc.noGravity = false;
			npc.HitSound = SoundID.NPCHit1;
			npc.DeathSound = SoundID.NPCDeath1;
			npc.value = Item.buyPrice(silver: 10);

			//banner = npc.type;
			//bannerItem = ItemType<Items.FragmentSlimeBanner>();
		}

        private float[] slimeAI = new float[4];

        private int frame;
		public override void FindFrame(int frameHeight)
        {
            int num2 = 0;
            if (npc.aiAction == 0)
            {
                num2 = ((npc.velocity.Y < 0f) ? 2 : ((npc.velocity.Y > 0f) ? 3 : ((npc.velocity.X != 0f) ? 1 : 0)));
            }
            else if (npc.aiAction == 1)
            {
                num2 = 4;
            }

            npc.frameCounter += 1;
            if (num2 > 0)
            {
                npc.frameCounter += 1;
            }
            if (num2 == 4)
            {
                npc.frameCounter += 1;
            }
            if (npc.frameCounter >= 8)
            {
                frame++;
                npc.frameCounter = 0;
            }
            if (frame >= Main.npcFrameCount[npc.type] - 1)
            {
                frame = 0;
            }
        }

        private bool MimicStomp;
        public override bool PreAI() 
        {
			npc.color = AuraClass.FragmentationSlimeColor(1f);
			if (slimeAI[2] > 1f)
            {
                slimeAI[2] -= 1f;
            }
            npc.aiAction = 0;
            if (slimeAI[2] == 0f)
            {
                slimeAI[0] = -100f;
                slimeAI[2] = 1f;
                npc.TargetClosest();
            }

            float jumpCooldown = -1000f;
            if (npc.velocity.Y == 0f)
            {
                if (npc.collideY && npc.oldVelocity.Y != 0f && Collision.SolidCollision(npc.position, npc.width, npc.height))
                {
                    npc.position.X -= npc.velocity.X + (float)npc.direction;
                }
                if (slimeAI[3] == npc.position.X)
                {
                    npc.direction *= -1;
                    slimeAI[2] = 200f;
                }
                slimeAI[3] = 0f;
                npc.velocity.X *= 0.8f;
                if ((double)npc.velocity.X > -0.1 && (double)npc.velocity.X < 0.1)
                {
                    npc.velocity.X = 0f;
                }
                slimeAI[0] += 2f;
                int jumpState = 0;
                if (slimeAI[0] >= 0f)
                {
                    jumpState = 1;
                }
                if (slimeAI[0] >= jumpCooldown && slimeAI[0] <= jumpCooldown * 0.5f)
                {
                    jumpState = 2;
                }
                if (slimeAI[0] >= jumpCooldown * 2f && slimeAI[0] <= jumpCooldown * 1.5f)
                {
                    jumpState = 3;
                }
                if (slimeAI[0] >= jumpCooldown * 3f && slimeAI[0] <= jumpCooldown * 2.5f)
                {
                    jumpState = 4;
                }
                if (jumpState > 0)
                {
                    npc.netUpdate = true;
                    if (slimeAI[2] == 1f)
                    {
                        npc.TargetClosest();
                    }

                    npc.velocity.Y = -6f * ((float)(jumpState) * (jumpState == 1 ? 1f : 0.5f));
                    npc.velocity.X += (float)(3 * npc.direction);
                    slimeAI[0] = -120f;
                    if (jumpState == 1)
                    {
                        slimeAI[0] -= jumpCooldown;
                    }
                    else if (jumpState == 2)
                    {
                        slimeAI[0] -= jumpCooldown * 2f;
                    }
                    else if (jumpState == 3)
                    {
                        slimeAI[0] -= jumpCooldown * 3f;
                    }
                    else
                    {
                        MimicStomp = true;
                    }
                }
                else if (slimeAI[0] >= -30f)
                {
                    npc.aiAction = 1;
                }
            }
            else if (npc.target < 255 && ((npc.direction == 1 && npc.velocity.X < 3f) || (npc.direction == -1 && npc.velocity.X > -3f)))
            {
                if (npc.collideX && Math.Abs(npc.velocity.X) == 0.2f)
                {
                    npc.position.X -= 1.4f * (float)npc.direction;
                }
                if (npc.collideY && npc.oldVelocity.Y != 0f && Collision.SolidCollision(npc.position, npc.width, npc.height))
                {
                    npc.position.X -= npc.velocity.X + (float)npc.direction;
                }
                if ((npc.direction == -1 && (double)npc.velocity.X < 0.01) || (npc.direction == 1 && (double)npc.velocity.X > -0.01))
                {
                    npc.velocity.X += 0.2f * (float)npc.direction;
                }
                else
                {
                    npc.velocity.X *= 0.93f;
                }
            }
            npc.rotation = 0;
            return false;
		}

        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            Mod mod = ModLoader.GetMod("MechaBosses");

            Texture2D texture = Main.npcTexture[npc.type];
            Rectangle rect = new Rectangle(0, frame * (texture.Height / Main.npcFrameCount[npc.type]), (texture.Width), (texture.Height / Main.npcFrameCount[npc.type]));
            Vector2 vect = new Vector2((float)(texture.Width / 2), (float)(texture.Height / (2 * Main.npcFrameCount[npc.type])));

            spriteBatch.Draw(texture, npc.Center - Main.screenPosition, new Rectangle?(rect), Lighting.GetColor((int)(npc.Center.X / 16), (int)(npc.Center.Y / 16), npc.color), npc.rotation, vect, npc.scale, SpriteEffects.None, 0f);
            return false;
        }

		public override void HitEffect(int hitDirection, double damage)
        {
			if (npc.life > 0)
			{
				for (int num257 = 0; (double)num257 < damage / (double)npc.lifeMax * 100.0; num257++)
				{
					Dust.NewDust(npc.position, npc.width, npc.height, 4, hitDirection, -1f, npc.alpha, npc.color);
				}
			}
			else
			{
				for (int num258 = 0; num258 < 50; num258++)
				{
					Dust.NewDust(npc.position, npc.width, npc.height, 4, 2 * hitDirection, -2f, npc.alpha, npc.color);
				}
			}
		}

		public override bool CheckDead()
        {
			if (Main.netMode != 1)
			{
				int num259 = 5;
				for (int num260 = 0; num260 < num259; num260++)
				{
                    if (num260 == 0 && !NPC.downedTowerSolar || num260 == 1 && !NPC.downedTowerVortex || num260 == 2 && !NPC.downedTowerNebula || num260 == 3 && !NPC.downedTowerStardust)
                    {
                        continue;
                    }
                    if (num260 == 4 && !NPC.downedAncientCultist)
                    {
                        break;
                    }
					int num261 = NPC.NewNPC((int)(npc.position.X + (float)(npc.width / 2)), (int)(npc.position.Y + (float)npc.height), ModContent.NPCType<NPCs.FragmentSlime>());
					Main.npc[num261].velocity.X = npc.velocity.X * 2f;
					Main.npc[num261].velocity.Y = npc.velocity.Y;
					Main.npc[num261].velocity.X += (float)Main.rand.Next(-20, 20) * 0.1f + (float)(num260 * npc.direction) * 0.3f;
					Main.npc[num261].velocity.Y -= (float)Main.rand.Next(0, 10) * 0.1f + (float)num260;
					Main.npc[num261].ai[0] = num260;
                    Main.npc[num261].ai[1] = 1f;
                    if (Main.netMode == 2 && num261 < 200)
					{
						NetMessage.SendData(23, -1, -1, null, num261);
					}
				}
			}
			return base.CheckDead();
		}
	}
}
