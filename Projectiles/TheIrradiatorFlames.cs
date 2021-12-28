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
	public class TheIrradiatorFlames : AuraProjectile
	{
		public override string Texture => "AuraClass/Assets/BlankTexture";

		public override bool UsesAuraAI() => false;

		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Cursed Flames");
		}

		public override void SafeSetDefaults() 
		{
			projectile.width = 10;
			projectile.height = 10;
			projectile.aiStyle = -1;
			projectile.friendly = true;
			projectile.alpha = 255;
			projectile.timeLeft = 60;
			projectile.tileCollide = false;
			projectile.penetrate = -1;
			projectile.extraUpdates = 8;
		}

		public override void SafeAI()
		{
			Player player = Main.player[projectile.owner];

			float dustScale = 1f;
			if (projectile.ai[1] == 0f)
				dustScale = 0.25f;
			else if (projectile.ai[1] == 1f)
				dustScale = 0.5f;
			else if (projectile.ai[1] == 2f)
				dustScale = 0.75f;

			Dust dust = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, 75, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 100);

			dust.noGravity = true;
			dust.scale *= 3f;
			dust.velocity *= 1.2f;
			dust.scale *= dustScale;

			projectile.ai[1] += 1f;
		}

		public override bool? SafeCanHitNPC(NPC target)
		{
			if (target != Main.npc[(int)projectile.ai[0]])
            {
				return null;
            }
			return false;
		}
	}
}
