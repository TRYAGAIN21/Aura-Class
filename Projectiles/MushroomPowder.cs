using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Terraria.Localization;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace AuraClass.Projectiles
{
	public class MushroomPowder : ModProjectile
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Luminous Powder");
		}

		public override void SetDefaults() {
			projectile.width = 64;
			projectile.height = 64;
			projectile.friendly = true;
			projectile.aiStyle = -1;
			projectile.alpha = 255;
			projectile.penetrate = -1;
			projectile.tileCollide = false;
			projectile.ignoreWater = true;
		}

		public override void AI() {
			projectile.velocity *= 0.95f;
			projectile.ai[0] += 1f;
			if (projectile.ai[0] == 180f)
			{
				projectile.Kill();
			}
			if (projectile.ai[1] == 0f)
			{
				projectile.ai[1] = 1f;

				for (int num64 = 0; num64 < 30; num64++)
				{
					Dust.NewDust(projectile.position, projectile.width, projectile.height, 20, projectile.velocity.X, projectile.velocity.Y, 50, Color.DarkBlue);
				}
			}

			if (projectile.owner == Main.myPlayer)
            {
				int minTileX = (int)(projectile.position.X / 16f);
				int maxTileX = (int)((projectile.position.X + projectile.width) / 16f);
				int minTileY = (int)(projectile.position.Y / 16f);
				int maxTileY = (int)((projectile.position.Y + projectile.height) / 16f);
				if (minTileX < 0)
				{
					minTileX = 0;
				}
				if (maxTileX > Main.maxTilesX)
				{
					maxTileX = Main.maxTilesX;
				}
				if (minTileY < 0)
				{
					minTileY = 0;
				}
				if (maxTileY > Main.maxTilesY)
				{
					maxTileY = Main.maxTilesY;
				}
				for (int i = minTileX; i <= maxTileX; i++)
				{
					for (int j = minTileY; j <= maxTileY; j++)
					{
						if (Main.tile[i, j] != null && Main.tile[i, j].active() && Main.tile[i, j].type == (ushort)TileID.JungleGrass)
						{
							Main.tile[i, j].type = (ushort)TileID.MushroomGrass;
						}
					}
				}
			}

			Rectangle hitbox = new Rectangle((int)projectile.position.X, (int)projectile.position.Y, projectile.width, projectile.height);
			for (int num34 = 0; num34 < 200; num34++)
			{
				foreach (int i in UsefulInts.ConvertableZombies)
				{
					if (Main.npc[num34].type == i && Main.npc[num34].active)
					{
						Rectangle value3 = new Rectangle((int)Main.npc[num34].position.X, (int)Main.npc[num34].position.Y, Main.npc[num34].width, Main.npc[num34].height);
						if (hitbox.Intersects(value3))
						{
							Main.npc[num34].Transform(Main.rand.NextBool() ? 255 : 254);
						}
					}
				}
			}
		}
	}
}
