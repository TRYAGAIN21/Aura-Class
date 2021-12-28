using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace AuraClass.AuraDamageClass
{
	public class AuraDamagePlayer : ModPlayer
	{
		public static AuraDamagePlayer ModPlayer(Player player)
		{
			return player.GetModPlayer<AuraDamagePlayer>();
		}

		public float auraDamageAdd;
		public float auraDamageMult = 1f;
		public float auraSpeedMult = 1f;
		public int auraSize;
		public int auraCrit;
		public float auraDecayMult = 1f;

		public float auraDamageAddCoba = 1f;
		public float auraSpeedMultMyth = 1f;
		public int auraSizeAdam;

		public bool magnifyingGlass;
		public bool magmafyingGlass;
		public bool photosynthifyingGlass;
		public bool darkEnergySet;

		public bool adamAura;
		public bool mythAura;
		public bool cobaAura;

		private int titaniumCD;
		private int templeCD;

		public int adamReset;
		public int mythReset;
		public int cobaReset;

		public bool ClimaxActive;

		public override void ResetEffects()
		{
			auraDamageAdd = 0f;
			auraDamageMult = 1f;
			auraSize = 0;
			auraCrit = 0;
			auraDecayMult = 1f;

			auraDamageAddCoba = 1f;
			auraSizeAdam = 0;

			auraSpeedMult = 1f;
			auraSpeedMultMyth = 1f;

			magnifyingGlass = false;
			magmafyingGlass = false;
			photosynthifyingGlass = false;
			darkEnergySet = false;

			ClimaxActive = false;
		}

		public override void PostUpdateBuffs()
		{
			/*if (player.HasBuff(26))
			{
				auraCrit += 2;
			}
			if (player.HasBuff(115))
			{
				auraCrit += 10;
			}*/

			UpdateArmorLights(0.15f, 0.45f, 0.9f, mod.GetEquipSlot("GlowHat", EquipType.Head), mod.GetEquipSlot("GlowShirt", EquipType.Body), mod.GetEquipSlot("GlowPants", EquipType.Legs));
			UpdateArmorLights(109, 27, 148, mod.GetEquipSlot("DarkEnergyHelmet", EquipType.Head), mod.GetEquipSlot("DarkEnergyBreastplate", EquipType.Body), mod.GetEquipSlot("DarkEnergyLeggings", EquipType.Legs));
		}

		public void UpdateArmorLights(int R, int G, int B, int headType = -1, int bodyType = -1, int legType = -1)
		{
			if (R > 255)
			{
				R = 255;
			}
			if (G > 255)
			{
				G = 255;
			}
			if (B > 255)
            {
				B = 255;
            }
			UpdateArmorLights((float)(R) / 255f, (float)(G) / 255f, (float)(B) / 255f, headType, bodyType, legType);
		}

		public void UpdateArmorLights(Vector3 rgb, int headType = -1, int bodyType = -1, int legType = -1)
        {
			UpdateArmorLights(rgb.X, rgb.Y, rgb.Z, headType, bodyType, legType);
		}

		public void UpdateArmorLights(float R, float G, float B, int headType = -1, int bodyType = -1, int legType = -1)
		{
			float GlowLevel = 0;
			float GlowLevelMax = 0;
			if (headType != -1)
            {
				GlowLevelMax++;
			}
			if (bodyType != -1)
			{
				GlowLevelMax++;
			}
			if (legType != -1)
			{
				GlowLevelMax++;
			}

			if (player.head == headType && headType != -1)
            {
				GlowLevel++;
            }
			if (player.body == bodyType && bodyType != -1)
			{
				GlowLevel++;
			}
			if (player.legs == legType && legType != -1)
			{
				GlowLevel++;
			}

			if (GlowLevel > GlowLevelMax)
            {
				GlowLevel = GlowLevelMax;
			}
			GlowLevelMax++;

			if (GlowLevel >= 1)
            {
				Lighting.AddLight((int)((player.position.X + (float)player.width) / 16f), (int)((player.position.Y + (float)(player.height / 2)) / 16f), R / (GlowLevelMax - GlowLevel), G / (GlowLevelMax - GlowLevel), B / (GlowLevelMax - GlowLevel));
			}
		}

		public int darkEnergyCooldown;
		public override void PostUpdateEquips()
		{
			if (darkEnergySet)
			{
				if (darkEnergyCooldown > 0)
					darkEnergyCooldown--;

				if (darkEnergyCooldown <= 0 && player.ownedProjectileCounts[mod.ProjectileType("DarkEnergySetBonus")] < 1)
				{
					Projectile.NewProjectile(player.Center, Vector2.Zero, mod.ProjectileType("DarkEnergySetBonus"), 150, 0, player.whoAmI);
				}
			}
		}

		public override void PostUpdateMiscEffects()
        {
			/*#region Chestplates
			if (player.body == 2277)
			{
				auraCrit += 5;
			}
			if (player.body == 374 || player.body == 1218)
			{
				auraCrit += 3;
			}
			if (player.body == 551 || player.body == 1004)
			{
				auraCrit += 7;
			}
			if (player.body == 1208)
			{
				auraCrit += 2;
			}
			if (player.body == 1213)
			{
				auraCrit += 6;
			}
			#endregion

			#region Leggings
			if (player.legs == 380)
			{
				auraCrit += 3;
			}
			if (player.legs == 404)
			{
				auraCrit += 4;
			}
			if (player.legs == 1005)
			{
				auraCrit += 8;
			}
			if (player.legs == 1209)
			{
				auraCrit += 1; //Wow so weak
			}
			#endregion*/

			adamReset--;
			if (adamReset <= 0)
			{
				adamAura = false;
			}

			mythReset--;
			if (mythReset <= 0)
			{
				mythAura = false;
			}

			cobaReset--;
			if (cobaReset <= 0)
			{
				cobaAura = false;
			}

			titaniumCD--;
			if (titaniumCD <= 0)
            {
				titaniumCD = 0;
			}

			templeCD--;
			if (templeCD <= 0)
			{
				templeCD = 0;
			}

			if (adamAura)
            {
				auraSizeAdam += 10;
				if (Main.rand.NextBool())
				{
					int num5 = Dust.NewDust(player.position, player.width, player.height, 90, 0f, 0f, 200, default(Color), 0.5f);
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
			}

			if (mythAura)
			{
				auraSpeedMultMyth *= 0.85f;
				if (Main.rand.NextBool())
				{
					int num5 = Dust.NewDust(player.position, player.width, player.height, 110, 0f, 0f, 200, default(Color), 0.5f);
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
			}

			if (cobaAura)
			{
				auraDamageAddCoba += 0.2f;
				if (Main.rand.NextBool())
				{
					int num5 = Dust.NewDust(player.position, player.width, player.height, 111, 0f, 0f, 200, default(Color), 0.5f);
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
			}
		}

		public override bool PreHurt(bool pvp, bool quiet, ref int damage, ref int hitDirection, ref bool crit, ref bool customDamage, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
        {
			if (player.ownedProjectileCounts[mod.ProjectileType("TitaniumAura")] > 0)
            {
				player.immuneTime += 30;

				if (titaniumCD <= 0)
                {
					if (player.ownedProjectileCounts[mod.ProjectileType("TitaniumShard")] <= 0)
                    {
						for (int i = 0; i < 5; ++i)
						{
							Projectile.NewProjectile(player.Center, Vector2.Zero, mod.ProjectileType("TitaniumShard"), 120, 0f, player.whoAmI, i, 0f);
						}
						titaniumCD = 300;
					}
				}
			}
			return base.PreHurt(pvp, quiet, ref damage, ref hitDirection, ref crit, ref customDamage, ref playSound, ref genGore, ref damageSource);
		}

		public override void PostUpdate()
		{
			int addedCrit = 1000;
			if (addedCrit > Main.player[player.whoAmI].meleeCrit)
				addedCrit = Main.player[player.whoAmI].meleeCrit;
			if (addedCrit > Main.player[player.whoAmI].rangedCrit)
				addedCrit = Main.player[player.whoAmI].rangedCrit;
			if (addedCrit > Main.player[player.whoAmI].thrownCrit)
				addedCrit = Main.player[player.whoAmI].thrownCrit;
			if (addedCrit > Main.player[player.whoAmI].magicCrit)
				addedCrit = Main.player[player.whoAmI].magicCrit;
			if (addedCrit < 0)
				addedCrit = 0;
			auraCrit += addedCrit;
		}

		public override void OnHitByNPC(NPC npc, int damage, bool crit)
        {
			darkEnergyCooldown = 240;
			if (player.statLife < (player.statLifeMax2 / 2) && player.ownedProjectileCounts[mod.ProjectileType("TemplesGuard")] > 0 && templeCD <= 0)
			{
				Vector2 velocity = npc.Center - player.Center;
				velocity.Normalize();
				velocity *= 12f;

				Projectile.NewProjectile(player.Center, velocity, mod.ProjectileType("TempleBeam"), (int)(player.HeldItem.damage * 1.2f), 0f, player.whoAmI, npc.whoAmI);
				templeCD = 60;
			}
		}

		public override void OnHitByProjectile(Projectile proj, int damage, bool crit)
        {
			darkEnergyCooldown = 120;
			if (player.statLife < (player.statLifeMax2 / 2) && player.ownedProjectileCounts[mod.ProjectileType("TemplesGuard")] > 0 && templeCD <= 0)
			{
				Projectile.NewProjectile(player.Center, Vector2.Zero, mod.ProjectileType("TempleBeam"), (int)(player.HeldItem.damage * 1.2f), 0f, player.whoAmI, 0f, 1f);
				templeCD = 60;
			}
		}

		public static readonly PlayerLayer GlowHatGlowmask = new PlayerLayer("AuraClass", "GlowHat_Head_Mask", PlayerLayer.Head, delegate (PlayerDrawInfo drawInfo)
		{
			if (drawInfo.shadow != 0f)
			{
				return;
			}
			Player drawPlayer = drawInfo.drawPlayer;
			Mod mod = ModLoader.GetMod("AuraClass");
			Texture2D texture = mod.GetTexture("Items/Armor/GlowHat_Head_Mask");
			float drawX = (int)drawInfo.position.X + drawPlayer.width / 2;
			float drawY = (int)drawInfo.position.Y + drawPlayer.height - drawPlayer.bodyFrame.Height / 2 + 4f;
			Vector2 origin = drawInfo.bodyOrigin;
			Vector2 position = new Vector2(drawX, drawY) + drawPlayer.bodyPosition - Main.screenPosition;
			float alpha = (255 - drawPlayer.immuneAlpha) / 255f;
			Color color = Color.White * drawPlayer.stealth;
			Rectangle frame = drawPlayer.bodyFrame;
			float rotation = drawPlayer.bodyRotation;
			SpriteEffects spriteEffects = drawInfo.spriteEffects;
			DrawData drawData = new DrawData(texture, position, frame, color * alpha, rotation, origin, 1f, spriteEffects, 0);
			drawData.shader = drawInfo.headArmorShader;
			Main.playerDrawData.Add(drawData);
		});
		public static readonly PlayerLayer GlowShirtGlowmask = new PlayerLayer("GlowShirtGlowmask", "GlowShirt_Mask", PlayerLayer.Body, delegate (PlayerDrawInfo drawInfo)
		{
			if (drawInfo.shadow != 0f)
			{
				return;
			}
			Player drawPlayer = drawInfo.drawPlayer;
			Mod mod = ModLoader.GetMod("AuraClass");
			Texture2D texture = mod.GetTexture("Items/Armor/GlowShirt_Body_Mask");
			float drawX = (int)drawInfo.position.X + drawPlayer.width / 2;
			float drawY = (int)drawInfo.position.Y + drawPlayer.height - drawPlayer.bodyFrame.Height / 2 + 4f;
			Vector2 origin = drawInfo.bodyOrigin;
			Vector2 position = new Vector2(drawX, drawY) + drawPlayer.bodyPosition - Main.screenPosition;
			float alpha = (255 - drawPlayer.immuneAlpha) / 255f;
			Color color = Color.White * drawPlayer.stealth;
			Rectangle frame = drawPlayer.bodyFrame;
			float rotation = drawPlayer.bodyRotation;
			SpriteEffects spriteEffects = drawInfo.spriteEffects;
			DrawData drawData = new DrawData(texture, position, frame, color * alpha, rotation, origin, 1f, spriteEffects, 0);
			drawData.shader = drawInfo.bodyArmorShader;
			Main.playerDrawData.Add(drawData);
		});
		public static readonly PlayerLayer GlowShirtArmGlowmask = new PlayerLayer("GlowShirtArmGlowmask", "GlowShirt_Arms_Mask", PlayerLayer.Arms, delegate (PlayerDrawInfo drawInfo)
		{
			if (drawInfo.shadow != 0f)
			{
				return;
			}
			Player drawPlayer = drawInfo.drawPlayer;
			Mod mod = ModLoader.GetMod("AuraClass");
			Texture2D texture = mod.GetTexture("Items/Armor/GlowShirt_Arms_Mask");
			float drawX = (int)drawInfo.position.X + drawPlayer.width / 2;
			float drawY = (int)drawInfo.position.Y + drawPlayer.height - drawPlayer.bodyFrame.Height / 2 + 4f;
			Vector2 origin = drawInfo.bodyOrigin;
			Vector2 position = new Vector2(drawX, drawY) + drawPlayer.bodyPosition - Main.screenPosition;
			float alpha = (255 - drawPlayer.immuneAlpha) / 255f;
			Color color = Color.White * drawPlayer.stealth;
			Rectangle frame = drawPlayer.bodyFrame;
			float rotation = drawPlayer.bodyRotation;
			SpriteEffects spriteEffects = drawInfo.spriteEffects;
			DrawData drawData = new DrawData(texture, position, frame, color * alpha, rotation, origin, 1f, spriteEffects, 0);
			drawData.shader = drawInfo.bodyArmorShader;
			Main.playerDrawData.Add(drawData);
		});
		public static readonly PlayerLayer GlowShirtArm = new PlayerLayer("GlowShirtArm", "GlowShirt_Arm", PlayerLayer.Arms, delegate (PlayerDrawInfo drawInfo)
		{
			if (drawInfo.shadow != 0f)
			{
				return;
			}
			Player drawPlayer = drawInfo.drawPlayer;
			Mod mod = ModLoader.GetMod("AuraClass");
			Texture2D texture = mod.GetTexture("Items/Armor/GlowShirt_Arm");
			float drawX = (int)drawInfo.position.X + drawPlayer.width / 2;
			float drawY = (int)drawInfo.position.Y + drawPlayer.height - drawPlayer.bodyFrame.Height / 2 + 4f;
			Vector2 origin = drawInfo.bodyOrigin;
			Vector2 position = new Vector2(drawX, drawY) + drawPlayer.bodyPosition - Main.screenPosition;
			float alpha = (255 - drawPlayer.immuneAlpha) / 255f;
			Color color = drawPlayer.GetImmuneAlphaPure(Lighting.GetColor((int)((double)drawPlayer.position.X + (double)drawPlayer.width * 0.5) / 16, (int)(((double)drawPlayer.position.Y + (double)drawPlayer.height * 0.25) / 16.0), drawPlayer.skinColor), drawInfo.shadow);
			Rectangle frame = drawPlayer.bodyFrame;
			float rotation = drawPlayer.bodyRotation;
			SpriteEffects spriteEffects = drawInfo.spriteEffects;
			DrawData drawData = new DrawData(texture, position, frame, color * alpha, rotation, origin, 1f, spriteEffects, 0);
			Main.playerDrawData.Add(drawData);
		});
		public static readonly PlayerLayer GlowPantsGlowmask = new PlayerLayer("GlowPantsGlowmask", "GlowPants_Mask", PlayerLayer.Legs, delegate (PlayerDrawInfo drawInfo)
		{
			if (drawInfo.shadow != 0f)
			{
				return;
			}
			Player drawPlayer = drawInfo.drawPlayer;
			Mod mod = ModLoader.GetMod("AuraClass");
			Texture2D texture = mod.GetTexture("Items/Armor/GlowPants_Legs_Mask");
			float drawX = (int)drawInfo.position.X + drawPlayer.width / 2;
			float drawY = (int)drawInfo.position.Y + drawPlayer.height - drawPlayer.legFrame.Height / 2 + 4f;
			Vector2 origin = drawInfo.bodyOrigin;
			Vector2 position = new Vector2(drawX, drawY) + drawPlayer.bodyPosition - Main.screenPosition;
			float alpha = (255 - drawPlayer.immuneAlpha) / 255f;
			Color color = Color.White * drawPlayer.stealth;
			Rectangle frame = drawPlayer.legFrame;
			float rotation = drawPlayer.bodyRotation;
			SpriteEffects spriteEffects = drawInfo.spriteEffects;
			DrawData drawData = new DrawData(texture, position, frame, color * alpha, rotation, origin, 1f, spriteEffects, 0);
			drawData.shader = drawInfo.legArmorShader;
			Main.playerDrawData.Add(drawData);
		});
		public static readonly PlayerLayer itemGlowmask = new PlayerLayer("AuraClass", "itemGlowmask", PlayerLayer.HeldItem, delegate (PlayerDrawInfo drawInfo)
		{
			Mod mod = AuraClass.instance;
			SpriteBatch spriteBatch = Main.spriteBatch;
			Player drawPlayer = drawInfo.drawPlayer;
			Item item = drawPlayer.HeldItem;
			if (item.GetGlobalItem<NormalGlobalItem>().glowmaskTex != null)
			{
				Texture2D tex = item.GetGlobalItem<NormalGlobalItem>().glowmaskTex;
				Color color = item.GetGlobalItem<NormalGlobalItem>().glowmaskColor;
				float rot = drawPlayer.itemRotation;
				if (Item.staff[item.type])
				{
					rot = drawPlayer.itemRotation + 0.785f * drawPlayer.direction;
				}
				int offsetX = 0;
				int offsetY = 0;
				Vector2 origin = new Vector2(0f, (float)Main.itemTexture[item.type].Height);
				if (drawPlayer.gravDir == -1f)
				{
					if (drawPlayer.direction == -1)
					{
						rot += 1.57f;
						origin = new Vector2((float)Main.itemTexture[item.type].Width, 0f);
						offsetX -= Main.itemTexture[item.type].Width;
					}
					else
					{
						rot -= 1.57f;
						origin = Vector2.Zero;
					}
				}
				else if (drawPlayer.direction == -1)
				{
					origin = new Vector2((float)Main.itemTexture[item.type].Width, (float)Main.itemTexture[item.type].Height);
					offsetX -= Main.itemTexture[item.type].Width;
				}
				Rectangle frame = new Rectangle(0, 0, tex.Width, tex.Height);
				SpriteEffects effects = SpriteEffects.None;
				if (drawPlayer.direction == -1)
				{
					effects = SpriteEffects.FlipHorizontally;
				}
				if (drawPlayer.gravDir == -1f)
				{
					effects |= SpriteEffects.FlipVertically;
				}
				DrawData data = new DrawData(tex, new Vector2((int)(drawInfo.itemLocation.X - Main.screenPosition.X + origin.X + offsetX), (int)(drawInfo.itemLocation.Y - Main.screenPosition.Y + offsetY)), new Rectangle?(frame), color, rot, origin, drawPlayer.HeldItem.scale, effects, 0);
				Main.playerDrawData.Add(data);
			}
		});
		public static readonly PlayerLayer itemHoliday = new PlayerLayer("AuraClass", "itemHoliday", PlayerLayer.HeldItem, delegate (PlayerDrawInfo drawInfo)
		{
			Mod mod = AuraClass.instance;
			SpriteBatch spriteBatch = Main.spriteBatch;
			Player drawPlayer = drawInfo.drawPlayer;
			Item item = drawPlayer.HeldItem;
			Texture2D tex = ModContent.GetTexture("AuraClass/Items/Weapons/Auras/HiveAura_Halloween");
			Color color = Lighting.GetColor((int)(drawInfo.itemLocation.X / 16f), (int)(drawInfo.itemLocation.Y / 16f));
			float rot = drawPlayer.itemRotation;
			if (Item.staff[item.type])
			{
				rot = drawPlayer.itemRotation + 0.785f * drawPlayer.direction;
			}
			int offsetX = 0;
			int offsetY = 0;
			Vector2 origin = new Vector2(0f, (float)Main.itemTexture[item.type].Height);
			if (drawPlayer.gravDir == -1f)
			{
				if (drawPlayer.direction == -1)
				{
					rot += 1.57f;
					origin = new Vector2((float)Main.itemTexture[item.type].Width, 0f);
					offsetX -= Main.itemTexture[item.type].Width;
				}
				else
				{
					rot -= 1.57f;
					origin = Vector2.Zero;
				}
			}
			else if (drawPlayer.direction == -1)
			{
				origin = new Vector2((float)Main.itemTexture[item.type].Width, (float)Main.itemTexture[item.type].Height);
				offsetX -= Main.itemTexture[item.type].Width;
			}
			Rectangle frame = new Rectangle(0, 0, tex.Width, tex.Height);
			SpriteEffects effects = SpriteEffects.None;
			if (drawPlayer.direction == -1)
			{
				effects = SpriteEffects.FlipHorizontally;
			}
			if (drawPlayer.gravDir == -1f)
			{
				effects |= SpriteEffects.FlipVertically;
			}
			for (int i = 0; i < 2; i++)
			{
				if (i == 1)
				{
					tex = ModContent.GetTexture("AuraClass/Items/Weapons/Auras/HiveAura_Halloween_Mask");
					color = Color.White;
					if (tex == null)
						break;
				}

				DrawData data = new DrawData(tex, new Vector2((int)(drawInfo.itemLocation.X - Main.screenPosition.X + origin.X + offsetX), (int)(drawInfo.itemLocation.Y - Main.screenPosition.Y + offsetY)), new Rectangle?(frame), color, rot, origin, drawPlayer.HeldItem.scale, effects, 0);
				Main.playerDrawData.Add(data);
			}
		});
		public override void ModifyDrawLayers(List<PlayerLayer> layers)
		{
			//Armor
			if (player.head == mod.GetEquipSlot("GlowHat", EquipType.Head))
			{
				int headLayer = layers.FindIndex(l => l == PlayerLayer.Head);
				if (headLayer > -1)
				{
					layers.Insert(headLayer + 1, GlowHatGlowmask);
				}
			}
			if (player.body == mod.GetEquipSlot("GlowShirt", EquipType.Body))
			{
				int armsLayer = layers.FindIndex(l => l == PlayerLayer.Arms);
				if (armsLayer > -1)
				{
					layers.Insert(armsLayer + 1, GlowShirtArm);
					layers.Insert(armsLayer + 2, GlowShirtArmGlowmask);
				}
				int bodyLayer = layers.FindIndex(l => l == PlayerLayer.Body);
				if (bodyLayer > -1)
				{
					layers.Insert(bodyLayer + 1, GlowShirtGlowmask);
				}
			}
			if (player.legs == mod.GetEquipSlot("GlowPants", EquipType.Legs))
			{
				int legsLayer = layers.FindIndex(l => l == PlayerLayer.Legs);
				if (legsLayer > -1)
				{
					layers.Insert(legsLayer + 1, GlowPantsGlowmask);
				}
			}

			for (int i = 0; i < layers.Count; i++)
			{
				if (PlayerLayer.HeldItem.visible && player.HeldItem.type != ItemID.None && !player.HeldItem.noUseGraphic && (player.itemAnimation > 0 || player.HeldItem.holdStyle == 1) && player.HeldItem.type == mod.ItemType("HiveAura") && Main.halloween)
				{
					if (layers[i] == PlayerLayer.HeldItem)
					{
						layers.Insert(i + 1, itemHoliday);
						itemHoliday.visible = true;
					}
				}
				if (PlayerLayer.HeldItem.visible && player.HeldItem.type != ItemID.None && !player.HeldItem.noUseGraphic && (player.itemAnimation > 0 || player.HeldItem.holdStyle == 1) && player.HeldItem.GetGlobalItem<NormalGlobalItem>().glowmaskTex != null)
				{
					if (layers[i] == PlayerLayer.HeldItem)
					{
						layers.Insert(i + 1, itemGlowmask);
						itemGlowmask.visible = true;
					}
				}
			}
		}
	}
}
