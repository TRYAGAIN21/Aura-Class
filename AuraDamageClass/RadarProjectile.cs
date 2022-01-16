using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AuraClass.AuraDamageClass
{
	// This class handles everything for our custom damage class
	// Any class that we wish to be using our custom damage class will derive from this class, instead of ModItem
	public abstract class RadarProjectile : ModProjectile
	{
		public override bool CloneNewInstances => true;

		// Custom items should override this to set their defaults
		public virtual void SafeSetDefaults() 
		{

		}

		public virtual void SafeAI()
		{

		}

		public sealed override void SetDefaults() 
		{
			SafeSetDefaults();
			projectile.melee = false;
			projectile.ranged = false;
			projectile.magic = false;
			projectile.thrown = false;
			projectile.minion = false;
		}

		public sealed override void AI()
        {
			Player radarOwner = Main.player[projectile.owner];
			if (radarOwner.ownedProjectileCounts[ModContent.ProjectileType<Projectiles.RadarDevice>()] > 0)
            {
				Aura(projectile, 24f * 16f, mod.DustType("RadarDevice"));
			}
			if (radarOwner.ownedProjectileCounts[ModContent.ProjectileType<Projectiles.SonarDevice>()] > 0)
			{
				Aura(projectile, 38f * 16f, 92);
			}
			SafeAI();
		}

		public static void Aura(Projectile projectile, float distance, int dustid = DustID.GoldFlame, bool checkDuration = false)
		{
			Player p = Main.player[Main.myPlayer];

			const int baseDistance = 500;
			const int baseMax = 20;

			int dustMax = (int)(distance / baseDistance * baseMax);
			if (dustMax < 40)
				dustMax = 40;
			if (dustMax > 40)
				dustMax = 40;

			float dustScale = distance / baseDistance;
			if (dustScale < 0.75f)
				dustScale = 0.75f;
			if (dustScale > 2f)
				dustScale = 2f;

			for (int i = 0; i < dustMax; i++)
			{
				Vector2 spawnPos = p.Center + Main.rand.NextVector2CircularEdge(distance, distance);
				if (p.Distance(spawnPos) > 1500)
					continue;
				Dust dust = Main.dust[Dust.NewDust(spawnPos, 0, 0, dustid, 0, 0, 100, Color.White, dustScale)];
				dust.velocity = p.velocity;
				dust.noGravity = true;
			}
		}
	}
}
