using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AuraClass.Dusts
{
	public class DarkEnergyDust : ModDust
	{
		public override void OnSpawn(Dust dust)
		{
			dust.scale *= 1.25f;
		}

		public override bool MidUpdate(Dust dust)
		{
			if (!dust.noGravity)
			{
				dust.velocity.Y += 0.05f;
				dust.scale *= 0.95f;
			}

			if (!dust.noLight)
			{
				float strength = dust.scale * 1.4f;
				if (strength > 1f)
				{
					strength = 1f;
				}

				Lighting.AddLight(dust.position, 0.05f * strength, 0.05f * strength, 0.05f * strength);
			}

			return false;
		}

		public override Color? GetAlpha(Dust dust, Color lightColor)
		{
			return Color.White;
		}
	}
}