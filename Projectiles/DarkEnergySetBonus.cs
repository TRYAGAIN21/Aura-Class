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
	public class DarkEnergySetBonus : AuraProjectile
	{
		public override string Texture => "AuraClass/Assets/BlankTexture";

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Dark Energy Aura");
		}

		public override void SafeSetDefaults() {
			projectile.aiStyle = -1;
			projectile.friendly = true;

			auraRange = 17;
			dustType = ModContent.DustType<Dusts.DarkEnergyDust>();
		}

		public override bool PreAI()
        {
			projectile.damage = (int)(150f * ((float)(Main.player[projectile.owner].statLife) / (float)(Main.player[projectile.owner].statLifeMax2)));
			auraRange = (int)(17f * (1f - (float)(Main.player[projectile.owner].statLife) / (float)(Main.player[projectile.owner].statLifeMax2) + 1f));
			return true;
        }

		public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			int defenseReduction = (int)(50f * (1f - (float)(Main.player[projectile.owner].statLife) / (float)(Main.player[projectile.owner].statLifeMax2)));
			if (defenseReduction > (target.defense / 2))
				defenseReduction = (target.defense / 2);

			damage = damage + defenseReduction;
		}
	}
}
