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
	public abstract class AuraProjectile : ModProjectile
	{
		public override bool CloneNewInstances => true;

		// Custom items should override this to set their defaults
		public virtual void SafeSetDefaults() 
		{

		}

		public virtual void SafeAI()
		{

		}

		int baseWidth;
		int baseHeight;

		// By making the override sealed, we prevent derived classes from further overriding the method and enforcing the use of SafeSetDefaults()
		// We do this to ensure that the vanilla damage types are always set to false, which makes the custom damage type work
		public sealed override void SetDefaults() {
			SafeSetDefaults();
			// all vanilla damage types must be false for custom damage types to work
			projectile.melee = false;
			projectile.ranged = false;
			projectile.magic = false;
			projectile.thrown = false;
			projectile.minion = false;

			baseWidth = projectile.width;
			baseHeight = projectile.height;

			projectile.tileCollide = false;
		}

		public sealed override void AI()
        {
			SafeAI();
			Player player = Main.player[projectile.owner];

			AuraDamagePlayer modPlayer = AuraDamagePlayer.ModPlayer(player);

			if (modPlayer.auraSize > 0)
			{
				projectile.width = (int)(baseWidth + modPlayer.auraSize * 32);
				projectile.height = (int)(baseHeight + modPlayer.auraSize * 32);
			}
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
				if (p.Distance(spawnPos) > 1500) //dont spawn dust if its pointless
					continue;
				Dust dust = Main.dust[Dust.NewDust(spawnPos, 0, 0, dustid, 0, 0, 100, Color.White, dustScale)];
				dust.velocity = projectile.velocity;
				dust.noGravity = true;
			}
		}
	}
}
