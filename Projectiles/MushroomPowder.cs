using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

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
				Convert((int)(projectile.position.X + projectile.width) / 16, (int)(projectile.position.Y + projectile.height) / 16, 2);
			}
		}

		public void Convert(int i, int j, int size = 4) {
			for (int k = i - size; k <= i + size; k++) {
				for (int l = j - size; l <= j + size; l++) {
					if (WorldGen.InWorld(k, l, 1) && Math.Abs(k - i) + Math.Abs(l - j) < Math.Sqrt(size * size + size * size)) {
						int type = Main.tile[k, l].type;

						//If the tile is Jungle Grass, convert to Mushroom Grass
						if (type == TileID.JungleGrass) {
							Main.tile[k, l].type = (ushort)TileID.MushroomGrass;
							WorldGen.SquareTileFrame(k, l, true);
							NetMessage.SendTileSquare(-1, k, l, 1);
						}
					}
				}
			}
		}
	}
}
