using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AuraClass.Dusts
{
	public class DirtAura : ModDust
    {
		public override bool Update(Dust dust)
		{
			dust.position += dust.velocity;
			dust.rotation += dust.velocity.X * 0.15f;
			dust.scale *= 0.99f;
			float light = 1f * dust.scale;
			Lighting.AddLight(dust.position, light, light, light);
			if (dust.scale < 0.75f)
			{
				dust.active = false;
			}
			return false;
		}
	}
}