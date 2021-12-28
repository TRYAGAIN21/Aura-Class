using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using Terraria.Localization;

namespace AuraClass.NPCs
{
	public class FragmentSlime : ModNPC
	{
        public override bool Autoload(ref string name)
        {
            return false;
        }
        
        public override void SetStaticDefaults() {
			//DisplayName.SetDefault("Fragmented Slime"); //this should never appear ingame
            NPCID.Sets.TrailCacheLength[npc.type] = 5;
            NPCID.Sets.TrailingMode[npc.type] = 0;
            Main.npcFrameCount[npc.type] = 2;
		}

		public override void SetDefaults() {
            npc.width = 32;
			npc.height = 24;
			npc.defense = 60;
			npc.damage = 100;
			npc.lifeMax = 15000;
			npc.knockBackResist = 0f;
			npc.npcSlots = 0.1f;
			npc.HitSound = SoundID.NPCHit1;
			npc.DeathSound = SoundID.NPCDeath1;
			npc.value = Item.buyPrice(silver: 10);
            npc.noGravity = true;

			//banner = npc.type;
			//bannerItem = ItemType<Items.FragmentSlimeBanner>();
		}

        private float[] slimeAI = new float[10];
        private float[] state = new float[2];

        private int shieldFrame;
        private int shieldFrameCounter;
        private int Yframe;

        private bool firstTick = true;

        private float rotation;
        public override bool PreAI() 
        {
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC fragmentedFriend = Main.npc[i];
                if (npc.ai[0] == 2f)
                {
                    state[1] = 0f;
                    if (fragmentedFriend.type == npc.type && fragmentedFriend.ai[0] == 1f)
                    {
                        state[1] = 1f;
                    }
                }
                if (npc.ai[0] == 1f)
                {
                    state[1] = 0f;
                    if (fragmentedFriend.type == npc.type && fragmentedFriend.ai[0] == 0f)
                    {
                        state[1] = 1f;
                    }
                }
            }

            npc.dontTakeDamage = false;
            if (npc.ai[0] == 1f)
            {
                npc.dontTakeDamage = true;
                rotation += 1f;
            }

            if (firstTick)
            {
                npc.life = npc.lifeMax;
                firstTick = false;
            }

            float JumpCD = 0f;
            if (npc.life > 0)
            {
                JumpCD += (((float)(npc.life) * 0.01f) / (Main.expertMode ? 2 : 1));
            }
            state[0] = JumpCD;

            if (npc.ai[1] != 1f) //For spawning with cheat sheet/spawning by itself
            {
                npc.ai[0] = Main.rand.Next(5);
                npc.ai[1] = 1f;
            }

            string name = $"{Language.GetTextValue("Mods.AuraClass.Common.Slime.SlimeSolar")}";
            if (npc.ai[0] == 1f)
            {
                name = $"{Language.GetTextValue("Mods.AuraClass.Common.Slime.SlimeVortex")}";
            }
            if (npc.ai[0] == 2f)
            {
                name = $"{Language.GetTextValue("Mods.AuraClass.Common.Slime.SlimeNebula")}";
            }
            if (npc.ai[0] == 3f)
            {
                name = $"{Language.GetTextValue("Mods.AuraClass.Common.Slime.SlimeStardust")}";
            }
            if (npc.ai[0] == 4f)
            {
                name = $"{Language.GetTextValue("Mods.AuraClass.Common.Slime.SlimeDarkEnergy")}";
            }
            npc.GivenName = name;

            npc.TargetClosest();
            Player target = Main.player[npc.target];

            if (npc.life <= npc.lifeMax / 2)
            {
                slimeAI[5] = 1f;
            }
            if (npc.velocity.Y == 0f)
            {
                npc.velocity.X = 0f;
                if (slimeAI[3] == 0f && npc.ai[0] == 0f)
                {
                    Projectile proj = Main.projectile[Projectile.NewProjectile(new Vector2(npc.Center.X, npc.Center.Y - 32f), Vector2.Zero, 696 - (slimeAI[1] == 0f ? 2 : (slimeAI[1] == 2f ? 1 : 0)), 25, 0f, Main.myPlayer)];
                    proj.friendly = false;
                    proj.melee = false;
                    proj.ranged = false;
                    proj.hostile = true;
                    proj.magic = true;

                    for (int i = 0; i < (slimeAI[1] == 0f ? 2 : (slimeAI[1] == 2f ? 4 : 6)); i++)
                    {
                        int speed = i + 1;
                        if (speed == 2 || speed == 4 || speed == 6 || speed == 8 || speed == 10)
                        {
                            speed *= -1;
                            speed++;
                        }

                        Projectile proj_2 = Main.projectile[Projectile.NewProjectile(npc.Center, new Vector2(3f * speed, 0f), ModContent.ProjectileType<Projectiles.FragmentedSlime.SolarShot>(), 25, 0f, Main.myPlayer)];
                    }

                    if (slimeAI[5] == 1f)
                    {
                        for (int i = 0; i < (slimeAI[1] == 0f ? 2 : (slimeAI[1] == 2f ? 4 : 8)); i++)
                        {
                            Projectile.NewProjectile(new Vector2(npc.Center.X, npc.Center.Y - 16f), (new Vector2(npc.Center.X, npc.Center.Y - 16f) - npc.Center).SafeNormalize(Vector2.Zero).RotatedByRandom(MathHelper.ToRadians(60f)) * (slimeAI[1] == 0f ? Main.rand.NextFloat(2f, 6f) : (slimeAI[1] == 2f ? Main.rand.NextFloat(6f, 14f) : Main.rand.NextFloat(8f, 20f))), 467, 25, 0f, Main.myPlayer);
                        }
                    }
                    slimeAI[3] = 1f;
                }

                slimeAI[0] += 2f;
                if (Main.expertMode || npc.ai[0] == 3f)
                {
                    slimeAI[0] += 1f;
                }
                if (slimeAI[0] > state[0])
                {
                    slimeAI[0] = 0f;
                    if (npc.ai[0] == 0f)
                    {
                        slimeAI[1]++;

                        slimeAI[3] = 0f;

                        npc.velocity.Y = -(slimeAI[5] != 1f ? 16f : 24f) * (slimeAI[1] == 3f ? 0.75f : (slimeAI[1] == 2f ? 1f : (slimeAI[1] == 1f ? 1.25f : 1.5f)));
                        npc.velocity.X += 5f * npc.direction;

                        if (slimeAI[1] == 3f)
                        {
                            slimeAI[0] -= (0f + (((float)(npc.life) * 0.01f) / (Main.expertMode ? 2 : 1))) * 5f;
                            slimeAI[1] = 0f;
                        }
                    }
                    if (npc.ai[0] == 3f)
                    {
                        slimeAI[0] -= 250f;
                        npc.velocity.Y = -24f;
                        npc.velocity.X += 10f * npc.direction;
                        slimeAI[4] = 0;
                        slimeAI[3] = 0;
                    }
                }
            }
            else
            {
                if (npc.ai[0] == 3f && slimeAI[4] < 5)
                {
                    for (int i = 0; i < 1; i++)
                    {
                        if (npc.velocity.Y >= 0f && !(npc.life <= npc.lifeMax - (npc.lifeMax / 4)))
                        {
                            break;
                        }
                        slimeAI[3]++;
                        if (slimeAI[3] > 5)
                        {
                            slimeAI[4]++;
                            Projectile.NewProjectile(npc.Center, Vector2.Zero, ModContent.ProjectileType<Projectiles.FragmentedSlime.StardustPortal>(), 25, 0f, Main.myPlayer, npc.target);
                            slimeAI[3] = 0;
                        }
                    }
                }
            }

            if (npc.ai[0] == 3f && slimeAI[5] == 1f && slimeAI[6] != 1f)
            {
                Projectile.NewProjectile(npc.Center, new Vector2(3f, 3f), ModContent.ProjectileType<Projectiles.FragmentedSlime.StardustPortal_2>(), 25, 0f, Main.myPlayer, npc.whoAmI);
                slimeAI[6] = 1f;
            }

            npc.velocity.Y += 1f;
            if (npc.ai[0] == 0f)
            {
                if (npc.velocity.Y >= 0f)
                {
                    npc.velocity.Y += 3f;
                    npc.velocity.X *= 0.8f;
                }
            }

            float gravity = 0.3f;
            float maxFallSpeed = npc.ai[0] == 0f ? 20f : 10f;

            int num7 = (int)(npc.position.X + (float)(npc.width / 2)) / 16;
            int num8 = (int)(npc.position.Y + (float)(npc.height / 2)) / 16;
            if (WorldGen.InWorld(num7, num8) && Main.tile[num7, num8] == null)
            {
                gravity = 0f;
                npc.velocity.X = 0f;
                npc.velocity.Y = 0f;
            }

            if (npc.velocity.Y <= 0 && Collision.TileCollision(npc.position, new Vector2(0, -16), npc.width, npc.height) != new Vector2(0, -16))
            {
                npc.velocity.Y = Collision.TileCollision(npc.position, new Vector2(0, -16), npc.width, npc.height).Y;
            }
            else if (npc.velocity.Y <= -16 && Collision.TileCollision(npc.position + new Vector2(0, -16), new Vector2(0, -16), npc.width, npc.height + 16) != new Vector2(0, -16))
            {
                npc.velocity.Y = Collision.TileCollision(npc.position + new Vector2(0, -16), new Vector2(0, -16), npc.width, npc.height + 16).Y - 16;
            }
            else if (npc.velocity.Y >= 0 && Collision.TileCollision(npc.position, new Vector2(0, 16), npc.width, npc.height) != new Vector2(0, 16))
            {
                npc.velocity.Y = Collision.TileCollision(npc.position, new Vector2(0, 16), npc.width, npc.height).Y;
            }
            else if (npc.velocity.Y >= 16 && Collision.TileCollision(npc.position, new Vector2(0, 16), npc.width, npc.height + 16) != new Vector2(0, 16))
            {
                npc.velocity.Y = Collision.TileCollision(npc.position, new Vector2(0, 16), npc.width, npc.height + 16).Y + 16;
            }

            npc.velocity.Y += gravity;
            if (npc.velocity.Y > maxFallSpeed)
            {
                npc.velocity.Y = maxFallSpeed;
            }
            return false;
		}

        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            Mod mod = ModLoader.GetMod("AuraClass");

            int Xframe = 0;
            if (npc.ai[0] == 1f)
            {
                Xframe = 1;
            }
            if (npc.ai[0] == 2f)
            {
                Xframe = 2;
            }
            if (npc.ai[0] == 3f)
            {
                Xframe = 3;
            }
            if (npc.ai[0] == 4f)
            {
                Xframe = 4;
            }

            int Xframes = 5;
            Texture2D texture = Main.npcTexture[npc.type];
            Rectangle rect = new Rectangle(Xframe * (texture.Width / Xframes), Yframe * (texture.Height / Main.npcFrameCount[npc.type]), (texture.Width / Xframes), (texture.Height / Main.npcFrameCount[npc.type]));
            Vector2 vect = new Vector2((float)(texture.Width / (2 * Xframes)), (float)(texture.Height / (2 * Main.npcFrameCount[npc.type])));

            for (int k = 0; k < npc.oldPos.Length; k++)
            {
                Vector2 drawPos = npc.oldPos[k] - Main.screenPosition + vect + new Vector2(0f, npc.gfxOffY);
                Color color = (npc.color * ((255 - (float)(npc.alpha)) / 255f)) * ((float)(npc.oldPos.Length - k) / (float)npc.oldPos.Length);
                spriteBatch.Draw(texture, drawPos, null, color, npc.rotation, vect, npc.scale, npc.spriteDirection == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0f);
            }

            spriteBatch.Draw(texture, npc.Center - Main.screenPosition, new Rectangle?(rect), Lighting.GetColor((int)(npc.Center.X / 16), (int)(npc.Center.Y / 16)), npc.rotation, vect, npc.scale, SpriteEffects.None, 0f);
            return false;
        }

        public override void NPCLoot() 
        {
            int fragmentType = 3458;
            if (npc.ai[0] == 1f)
            {
                fragmentType = 3456;
            }
            if (npc.ai[0] == 2f)
            {
                fragmentType = 3457;
            }
            if (npc.ai[0] == 3f)
            {
                fragmentType = 3459;
            }
            if (npc.ai[0] == 4f)
            {
                fragmentType = ModContent.ItemType<Items.Materials.DarkEnergyFragment>();
            }
            Item.NewItem(npc.Hitbox, fragmentType, Main.rand.Next(20, 30) + (Main.expertMode ? 20 : 0));
        }
	}
}