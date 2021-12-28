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
	public class MoonRock : AuraProjectile
	{
		public override string Texture => "AuraClass/Assets/BlankTexture";

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Phantasmal Aura");
		}

		public override void SafeSetDefaults() {
			projectile.aiStyle = -1;
			projectile.friendly = true;
			projectile.usesLocalNPCImmunity = true;
			projectile.localNPCHitCooldown = 22;

			auraRange = 70;
			dustType = 229;
		}

		private int meteorTimer;

		public override bool PreAI()
		{
			Player player = Main.player[projectile.owner];
			meteorTimer++;
			if (meteorTimer > 10)
			{
				for (int k = 0; k < 3; k++)
				{
					float spread = 3f;
					Vector2 spawnPos = new Vector2(player.Center.X + (Main.rand.NextFloat((float)(-CurrentRange) * 0.75f, (float)(CurrentRange) * 0.75f)), player.Center.Y - Main.rand.NextFloat(700f, 800f));
					Vector2 targetPos = new Vector2(spawnPos.X + (Main.rand.NextFloat(-spread * 16f, spread * 16f)), player.Center.Y + (Main.rand.NextFloat((float)(-CurrentRange) * 0.5f, (float)(CurrentRange) * 0.5f)));
					Vector2 vectorToTargetPosition = targetPos - spawnPos;
					float distanceToTargetPosition = vectorToTargetPosition.Length();

					vectorToTargetPosition.Normalize();
					vectorToTargetPosition *= 12f;

					Main.PlaySound(2, (int)Main.player[projectile.owner].Center.X, (int)Main.player[projectile.owner].Center.Y, 88);
					Projectile.NewProjectile(spawnPos, vectorToTargetPosition, mod.ProjectileType("MoonMeteor"), (int)(projectile.damage * 0.8f), 0f, Main.myPlayer, targetPos.X, targetPos.Y);
				}
				meteorTimer = 0;
			}
			return true;
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
