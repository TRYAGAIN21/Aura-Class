using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using AuraClass.Items.Weapons.Auras;

namespace AuraClass
{
    public class AuraClassNPC : GlobalNPC
    {
        public override bool InstancePerEntity => true;

        public bool tracked;
        public bool tracked2;
        public bool corroding;
        public bool irradiated;

        public bool hasCorrosionCloud;
        public int irradiatingCount;

        private int irradiatedCD;

        public override void ResetEffects(NPC npc)
        {
            tracked = false;
            tracked2 = false;
            corroding = false;
            irradiated = false;
            irradiatingCount = 0;
        }

        private int Counter;
        public override bool PreAI(NPC npc)
        {
            bool hasIrradiatingAura = false;
            int counter = 0;
            int counterMax = 0;
            if (irradiated)
            {
                for (int num485 = 0; num485 < 200; num485++)
                {
                    if (Main.npc[num485].active && num485 != npc.whoAmI && Main.npc[num485].CanBeChasedBy(null))
                    {
                        counterMax++;
                        if (npc.whoAmI < Main.npc[num485].whoAmI)
                            counter++;

                        if (counter >= counterMax && counterMax != 0 || counterMax == 0)
                            hasIrradiatingAura = true;
                    }
                }
            }    

            if (corroding)
            {
                if (npc.CanBeChasedBy(this) && Main.player[Main.myPlayer].ownedProjectileCounts[mod.ProjectileType("TheCorrosionCloud")] <= 0)
                {
                    Projectile.NewProjectile(npc.Center - new Vector2(0f, 300f), Vector2.Zero, mod.ProjectileType("TheCorrosionCloud"), 20, 0f, Main.myPlayer, npc.whoAmI);
                }
            }
            if (hasIrradiatingAura)
            {
                Aura(npc.Center, npc, 240f, 75);
                irradiatedCD--;
                if (irradiatedCD <= 0)
                {
                    for (int k = 0; k < Main.maxNPCs; k++)
                    {
                        if (Counter > 4)
                        {
                            Counter = 0;
                            break;
                        }
                        NPC target = Main.npc[k];

                        if (target.CanBeChasedBy(this) && target.whoAmI != npc.whoAmI)
                        {
                            Vector2 vectorToTargetPosition = target.Center - npc.Center;
                            float distanceToTargetPosition = vectorToTargetPosition.Length();

                            if (distanceToTargetPosition <= 240f)
                            {
                                vectorToTargetPosition.Normalize();
                                vectorToTargetPosition *= 4f;

                                Counter += 1;

                                Projectile.NewProjectile(npc.Center, vectorToTargetPosition, mod.ProjectileType("TheIrradiatorFlames"), 20, 0f, Main.myPlayer, npc.whoAmI);
                            }
                        }
                    }
                    irradiatedCD = 10;
                }
                else { Counter = 0; }
            }
            return base.PreAI(npc);
        }

        public override void ModifyHitByProjectile(NPC npc, Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            float damageMult = 1f;
            if ((tracked || tracked2) && projectile.type != ModContent.ProjectileType<Projectiles.RadarDevice>() && projectile.type != ModContent.ProjectileType<Projectiles.SonarDevice>())
            {
                damageMult += tracked2 ? .5f : .25f;
            }

            damage = (int)(damage * damageMult);
        }

        public override bool PreDraw(NPC npc, SpriteBatch spriteBatch, Color drawColor)
        {
            Mod mod = ModLoader.GetMod("AuraClass");

            //if (irradiated)
            //{
            //    Texture2D texture = mod.GetTexture("Projectiles/TheIrradiator");
            //    spriteBatch.Draw(texture, npc.Center - Main.screenPosition, null, Color.White * ((255 - 10f) / 255f), npc.rotation, texture.Size() * 0.5f, 2f, SpriteEffects.None, 0f);
            //}
            return base.PreDraw(npc, spriteBatch, drawColor);
        }

        public static void Aura(Vector2 position, NPC npc, float distance, int dustid = DustID.GoldFlame)
        {
            Player p = Main.player[Main.myPlayer];

            if (dustid == -1)
            {
                return;
            }

            const int baseDistance = 500;
            const int baseMax = 20;

            int dustMax = (int)(distance / baseDistance * baseMax);
            if (dustMax < 40) { dustMax = 40; }
            if (dustMax > 40) { dustMax = 40; }

            float dustScale = distance / baseDistance;
            if (dustScale < 0.75f) { dustScale = 0.75f; }
            if (dustScale > 2f) { dustScale = 2f; }

            for (int i = 0; i < dustMax; i++)
            {
                Vector2 spawnPos = position + Main.rand.NextVector2CircularEdge(distance, distance);
                if (p.Distance(spawnPos) > 1500) { continue; }

                Dust dust = Main.dust[Dust.NewDust(spawnPos, 0, 0, dustid, 0, 0, 100, Color.White, dustScale)];
                dust.velocity = npc.velocity;
                dust.noGravity = true;
            }
        }

        public override void DrawEffects(NPC npc, ref Color drawColor)
        {
            if (tracked)
            {
                drawColor = new Color(0, 255, 11);
            }
            if (tracked2)
            {
                drawColor = new Color(101, 132, 255);
            }
        }

        public override void NPCLoot(NPC npc)
        {
            // All Aura Class items from bosses/loot bags have double the drop rate compared to other drops from the same source
            if (!npc.SpawnedFromStatue)
            {
                if (Main.player[(int)Player.FindClosest(npc.position, npc.width, npc.height)].ZoneGlowshroom)
                {
                    if (Main.rand.NextBool(Main.expertMode ? 4 : 6))
                    {
                        Item.NewItem(npc.getRect(), ModContent.ItemType<Items.MushroomPowder>(), 1);
                    }
                }

                switch (npc.type)
                {
                    case NPCID.QueenBee:
                        if (Main.rand.NextBool(4 / 2) && !Main.expertMode)
                            Item.NewItem(npc.Hitbox, ModContent.ItemType<Items.Weapons.Auras.HiveAura>());
                        break;

                    case NPCID.Antlion:
                        if (Main.rand.NextBool(15))
                            Item.NewItem(npc.Hitbox, ModContent.ItemType<Items.Weapons.Auras.SandAura>());
                        break;

                    case NPCID.WallofFlesh:
                        if (Main.rand.NextBool(4 / 2) && !Main.expertMode)
                            Item.NewItem(npc.Hitbox, ModContent.ItemType<Items.Accessories.AuraEmblem>(), 1, false, -1);
                        break;

                    case NPCID.DukeFishron:
                        if (Main.rand.NextBool(5 / 2) && !Main.expertMode)
                            Item.NewItem(npc.Hitbox, ModContent.ItemType<Items.Weapons.Auras.ShrimpyBubble>());
                        break;

                    case NPCID.Plantera:
                        if (Main.rand.NextBool(7 / 2) && !Main.expertMode)
                            Item.NewItem(npc.Hitbox, ModContent.ItemType<Items.Weapons.Auras.JungleBlossom>());
                        break;

                    case NPCID.Golem:
                        if (Main.rand.NextBool(7 / 2) && !Main.expertMode)
                            Item.NewItem(npc.Hitbox, ModContent.ItemType<Items.Weapons.Auras.TemplesGuard>());
                        break;

                    case NPCID.CultistBoss:
                        Item.NewItem(npc.Hitbox, ModContent.ItemType<Items.Materials.DarkEnergyFragment>(), Main.expertMode ? Main.rand.Next(24, 100) : Main.rand.Next(12, 60));
                        break;

                    case NPCID.MoonLordCore:
                        if (Main.rand.NextBool(9 / 2) && !Main.expertMode)
                            Item.NewItem(npc.Hitbox, ModContent.ItemType<Items.Weapons.Auras.MoonRock>());
                        break;

                    default:
                        break;
                }
            }
        }
    }
}