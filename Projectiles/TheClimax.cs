using AuraClass.AuraDamageClass;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using System.Collections.Generic;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AuraClass.Projectiles
{
	public class TheClimax : AuraProjectile // "WITH GREAT RANGE COMES INFINITE POWER" - TRYAGAIN211 2021
	{
		public override string Texture => "AuraClass/Assets/BlankTexture";

		public override bool IsClimax() => true;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("The Climax");
		}

		public override void SafeSetDefaults() {
			projectile.aiStyle = -1;
			projectile.friendly = true;
			projectile.usesLocalNPCImmunity = true;
			projectile.localNPCHitCooldown = 22;

			auraRange = 10;
			dustType = -1;
		}

		public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
			return true; //INFINITE RANGE
        }

		public override void SafeAI()
		{
			Main.player[projectile.owner].GetModPlayer<AuraDamageClass.AuraDamagePlayer>().ClimaxActive = true;
			if (!SkyManager.Instance["AuraClass:Climax"].IsActive())
				SkyManager.Instance.Activate("AuraClass:Climax");
		}
	}
}
