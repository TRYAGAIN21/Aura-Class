using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AuraClass.Dusts
{
	public class MushroomPowder : ModDust
    {
		public override bool Update(Dust dust)
		{
			dust.scale *= 0.995f;
			dust.velocity.Y *= 0.94f;
			dust.velocity.X *= 0.94f;
			float num94 = dust.scale * 0.8f;
			if (num94 > 1f)
			{
				num94 = 1f;
			}
			if (dust.type == 21)
			{
				num94 = dust.scale * 0.4f;
				Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num94 * 0.8f, num94 * 0.3f, num94);
			}
			else if (dust.type == 231)
			{
				num94 = dust.scale * 0.4f;
				Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num94, num94 * 0.5f, num94 * 0.3f);
			}
			else
			{
				Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num94 * 0.3f, num94 * 0.6f, num94);
			}

			return false;
		}
	}
}