using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AuraClass.Dusts
{
	public class RadarDevice : ModDust
    {
		public override bool Update(Dust dust)
		{
			float num13 = dust.scale * 0.5f;
			if (num13 > 1f)
			{
				num13 = 1f;
			}
			float num14 = num13;
			float num15 = num13;
			float num16 = num13;
			num14 *= 0f;
			num15 *= 0.25f;
			num16 *= 1f;
			Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num13 * num14, num13 * num15, num13 * num16);
			return true;
		}
	}
}