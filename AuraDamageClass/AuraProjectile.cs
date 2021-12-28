using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AuraClass.AuraDamageClass
{
	public abstract class AuraProjectile : ModProjectile
	{
		public override bool CloneNewInstances => true;

		public virtual void SafeAI() { }

		public virtual void SafeSetDefaults() { }

		public int dustType;
		public int auraRange;
		public int auraRangePrefix;

		public float auraSpeedPrefix;

		public virtual bool UsesAuraAI()
		{
			return true;
		}

		public virtual bool IsClimax()
		{
			return false;
		}

		public sealed override void SetDefaults() {
			SafeSetDefaults();
			projectile.melee = false;
			projectile.ranged = false;
			projectile.magic = false;
			projectile.thrown = false;
			projectile.minion = false;

			if (UsesAuraAI())
			{
				projectile.penetrate = -1;
				projectile.alpha = 255;

				projectile.tileCollide = false;
				projectile.Size = new Vector2(1, 1);
			}
		}

		private int lightBeam_Timer = 180;
		public int CurrentRange;

		private bool firstTick = true;

		public virtual void FirstTick() { }

		public override void AI()
		{
			SafeAI();
			if (firstTick)
            {
				FirstTick();

				Player player = Main.player[projectile.owner];
				AuraDamagePlayer modPlayer = AuraDamagePlayer.ModPlayer(player);

				if (!player.HeldItem.IsAir && projectile.type != mod.ProjectileType("DarkEnergySetBonus"))
                {
					auraRangePrefix = player.HeldItem.GetGlobalItem<AuraDamageClass.AuraGlobalItem>().rangeBoostPrefix;
				}

				if (projectile.type != ModContent.ProjectileType<Projectiles.CobaltAura>())
                {
					projectile.damage = (int)(projectile.damage * modPlayer.auraDamageAddCoba);
				}
				firstTick = false;
			}
			if (UsesAuraAI())
            {
				Player player = Main.player[projectile.owner];
				AuraDamagePlayer modPlayer = AuraDamagePlayer.ModPlayer(player);

				projectile.localNPCHitCooldown = (int)((projectile.type == mod.ProjectileType("DarkEnergySetBonus") ? 15 : player.HeldItem.useTime) * (modPlayer.auraSpeedMult + (modPlayer.auraSpeedMultMyth - 1f)));
				if (projectile.type == ModContent.ProjectileType<Projectiles.MythrilAura>())
                {
					projectile.localNPCHitCooldown = (int)(player.HeldItem.useTime * modPlayer.auraSpeedMult);
				}

				int RealRangeNormal2 = auraRange;
				int RealRangePrefix2 = auraRangePrefix;
				int RealRange2 = RealRangeNormal2 + RealRangePrefix2;
				int RealPlayerRange2 = (modPlayer.auraSize + modPlayer.auraSizeAdam);
				float TrueRange = (float)(projectile.type == mod.ProjectileType("DarkEnergySetBonus") ? RealRangeNormal2 : (RealRange2 + RealPlayerRange2));

				int RealRangeNormal = auraRange * 16;
				int RealRangePrefix = auraRangePrefix * 16;
				int RealRange = RealRangeNormal + RealRangePrefix;

				int RealPlayerRange = (modPlayer.auraSize + modPlayer.auraSizeAdam) * 16;
				if (projectile.type == ModContent.ProjectileType<Projectiles.AdamantiteAura>())
                {
					RealPlayerRange2 = modPlayer.auraSize;
					RealPlayerRange = modPlayer.auraSize * 16;

					TrueRange = (float)(RealRange2 + RealPlayerRange2);
				}
				CurrentRange = projectile.type == mod.ProjectileType("DarkEnergySetBonus") ? (RealRangeNormal / 2) : ((RealRange + RealPlayerRange) / 2);

				projectile.width = projectile.type == mod.ProjectileType("DarkEnergySetBonus") ? RealRangeNormal : (RealRange + RealPlayerRange);
				projectile.height = projectile.type == mod.ProjectileType("DarkEnergySetBonus") ? RealRangeNormal : (RealRange + RealPlayerRange);

				if (projectile.type != mod.ProjectileType("DarkEnergySetBonus"))
                {
					player.heldProj = projectile.whoAmI;
				}

				projectile.position.X = player.Center.X - (float)(projectile.type == mod.ProjectileType("DarkEnergySetBonus") ? (RealRangeNormal / 2) : (((RealRange + RealPlayerRange) / 2)));
				projectile.position.Y = player.Center.Y - (float)(projectile.type == mod.ProjectileType("DarkEnergySetBonus") ? (RealRangeNormal / 2) : (((RealRange + RealPlayerRange) / 2)));

				if (projectile.type != ModContent.ProjectileType<Projectiles.ShrimpyBubble>())
				{
					Aura(player.Center, projectile, projectile.type == mod.ProjectileType("DarkEnergySetBonus") ? (RealRangeNormal / 2) : ((RealRange + RealPlayerRange) / 2), dustType);
				}

				if (projectile.owner == Main.myPlayer && player.channel)
				{
					projectile.netUpdate = true;
					player.itemTime = 2;
					player.itemAnimation = 2;
					projectile.timeLeft = 2;
				}

				if (projectile.owner == Main.myPlayer)
				{
					if (player.itemTime == 0 || player.itemAnimation == 0 || player.stoned || projectile.type == mod.ProjectileType("DarkEnergySetBonus"))
					{
						if (projectile.type != mod.ProjectileType("DarkEnergySetBonus") || (modPlayer.darkEnergyCooldown > 0 || !modPlayer.darkEnergySet) && projectile.type == mod.ProjectileType("DarkEnergySetBonus"))
							projectile.Kill();
					}
				}

				if (player.GetModPlayer<AuraDamagePlayer>().magnifyingGlass || player.GetModPlayer<AuraDamagePlayer>().magmafyingGlass || player.GetModPlayer<AuraDamagePlayer>().photosynthifyingGlass)
				{
					MagnifyingGlass(player, player.GetModPlayer<AuraDamagePlayer>().magmafyingGlass, player.GetModPlayer<AuraDamagePlayer>().photosynthifyingGlass, TrueRange);
				}
			}
		}

		private void MagnifyingGlass(Player player, bool HellUpgrade, bool JungleUpgrade, float Range)
        {
			bool fireable = true;
			if (player.position.Y / 16f > Main.rockLayer)
			{
				fireable = false;

				bool JungleUpgradeLight = Lighting.Brightness((int)(player.position.X / 16), (int)(player.position.Y / 16)) >= 0.5f;
				if ((HellUpgrade || JungleUpgrade) && !(player.position.Y / 16f <= Main.maxTilesY - 200) || JungleUpgrade && JungleUpgradeLight)
                {
					fireable = true;
				}
			}

			if (fireable)
            {
				lightBeam_Timer--;
				if (lightBeam_Timer <= 0)
				{
					Vector2 velocity = Main.MouseWorld - player.Center;
					velocity.Normalize();

					float rangeMultiplier = JungleUpgrade ? 2f : (HellUpgrade ? 1.5f : 1f);
					Projectile beam = Main.projectile[Projectile.NewProjectile(player.Center, velocity * 30f, ModContent.ProjectileType<Projectiles.LightBeam>(), JungleUpgrade ? 180 : (HellUpgrade ? 40 : (!Main.dayTime ? 10 : 20)), 0f, player.whoAmI, projectile.whoAmI)];
					beam.ai[0] = Range * rangeMultiplier;
					beam.ai[1] = JungleUpgrade ? 2 : (HellUpgrade ? 1 : 0);

					lightBeam_Timer = 240 + (JungleUpgrade ? 120 : (HellUpgrade ? 60 : 0));
				}
			}
		}

		public static void Aura(Vector2 position, Projectile projectile, float distance, int dustid = DustID.GoldFlame)
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
				if (Main.player[projectile.owner].frozen || Main.player[projectile.owner].HasBuff(BuffID.Cursed))
				{
					if (Main.player[projectile.owner].HasBuff(BuffID.Cursed)) { Dust dust = Main.dust[Dust.NewDust(spawnPos, 0, 0, 27, 0, 0, 100, Color.White, dustScale)]; dust.velocity = projectile.velocity + Main.player[projectile.owner].velocity; dust.noGravity = true; }
					else if (Main.player[projectile.owner].frozen) { Dust dust = Main.dust[Dust.NewDust(spawnPos, 0, 0, 15, 0, 0, 100, Color.White, dustScale)]; dust.velocity = projectile.velocity + Main.player[projectile.owner].velocity; dust.noGravity = true; }
				}
				else 
				{ 
					Dust dust = Main.dust[Dust.NewDust(spawnPos, 0, 0, dustid, 0, 0, 100, Color.White, dustScale)]; 
					dust.velocity = projectile.velocity + Main.player[projectile.owner].velocity; 
					dust.noGravity = true; 
					/*if (projectile.type == ModContent.ProjectileType<Projectiles.TheRadiator>()) 
					{ 
						dust.scale *= 3f; 
					}*/
				}
			}
		}

		public virtual bool? SafeCanHitNPC(NPC target) 
		{
			return null;
		}

		public sealed override bool? CanHitNPC(NPC target)
        {
			if (projectile.type != ModContent.ProjectileType<Projectiles.WaveOfFire>() && projectile.type != ModContent.ProjectileType<Projectiles.NightsShroudSmoke>())
			{
				if (UsesAuraAI())
				{
					int RealRangeNormal = auraRange * 16;
					int RealRangePrefix = auraRangePrefix * 16;
					int RealRange = RealRangeNormal + RealRangePrefix;

					int RealPlayerRange = AuraDamagePlayer.ModPlayer(Main.player[projectile.owner]).auraSize * 16;

					Vector2 vectorToTargetPosition = target.Center - projectile.Center;
					float distanceToTargetPosition = vectorToTargetPosition.Length();

					if (distanceToTargetPosition > (projectile.type == mod.ProjectileType("DarkEnergySetBonus") ? (RealRange / 2) : ((RealRange + RealPlayerRange) / 2)) || target.friendly || target.townNPC || Main.player[projectile.owner].frozen || Main.player[projectile.owner].HasBuff(BuffID.Cursed))
					{
						return false;
					}
					return SafeCanHitNPC(target);
				}
				return SafeCanHitNPC(target);
			}
			else if (projectile.type != ModContent.ProjectileType<Projectiles.NightsShroudSmoke>())
			{
				Vector2 vectorToTargetPosition = target.Center - projectile.Center;
				float distanceToTargetPosition = vectorToTargetPosition.Length();

				if (distanceToTargetPosition > projectile.width / 2 || distanceToTargetPosition > projectile.height / 2 || distanceToTargetPosition < (projectile.width - 10) / 2 || target.friendly || target.townNPC || Main.player[projectile.owner].frozen || Main.player[projectile.owner].HasBuff(BuffID.Cursed))
				{
					return false;
				}
			}
			return SafeCanHitNPC(target);
		}

		public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			if (UsesAuraAI())
            {
				int RealRangeNormal = auraRange * 32;
				int RealRangePrefix = auraRangePrefix * 32;
				int RealRange = RealRangeNormal + RealRangePrefix;

				int RealPlayerRange = AuraDamagePlayer.ModPlayer(Main.player[projectile.owner]).auraSize * 32;

				Vector2 vectorToTargetPosition = target.Center - Main.player[projectile.owner].Center;
				float distanceToTargetPosition = vectorToTargetPosition.Length();
				if (!Collision.CanHitLine(Main.player[projectile.owner].Center, 1, 1, target.Center, 1, 1) && projectile.type != mod.ProjectileType("DarkEnergySetBonus"))
				{
					damage = damage / 4;
				}

				if (Main.player[projectile.owner].channel && projectile.type != mod.ProjectileType("DarkEnergySetBonus"))
                {
					ModItem item = Main.player[projectile.owner].HeldItem.modItem;
					if (!Main.player[projectile.owner].HeldItem.IsAir)
					{
						if (item is AuraItem weapon)
						{
							float mult = distanceToTargetPosition / (float)CurrentRange;
							mult = MathHelper.Clamp(mult, 0f, (float)CurrentRange);

							float damageMultiplier = MathHelper.Lerp(1f, 1f - (weapon.decayRate * AuraDamagePlayer.ModPlayer(Main.player[projectile.owner]).auraDecayMult * Main.player[projectile.owner].HeldItem.GetGlobalItem<AuraDamageClass.AuraGlobalItem>().decayMultPrefix), mult);
							damage = (int)(damage * damageMultiplier);
							//CombatText.NewText(new Rectangle((int)Main.player[projectile.owner].position.X, (int)Main.player[projectile.owner].position.Y, Main.player[projectile.owner].width, Main.player[projectile.owner].height), CombatText.HealLife, damageMultiplier + " (Decay Rate Mult)");
						}
					}
				}
			}
		}
	}
}
