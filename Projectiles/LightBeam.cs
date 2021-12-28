using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using Terraria;
using Terraria.Enums;
using Terraria.GameContent.Shaders;
using Terraria.Graphics.Effects;
using Terraria.ID;
using Terraria.ModLoader;
using AuraClass.AuraDamageClass;

namespace AuraClass.Projectiles
{
	public class LightBeam : AuraProjectile
	{
		public override bool UsesAuraAI() => false;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Magnifying Beam");
		}

		private int timeMax = 120;
		private int Distance;

		public override void SafeSetDefaults()
		{
			projectile.aiStyle = -1;
			projectile.width = 18;
			projectile.height = 18;

			projectile.alpha = 50;
			projectile.timeLeft = timeMax;
			projectile.penetrate = -1;
			projectile.friendly = true;
			projectile.tileCollide = false;
			projectile.ignoreWater = true;
			projectile.light = 1f;

			projectile.scale = 0.1f;

			projectile.usesLocalNPCImmunity = true;
			projectile.localNPCHitCooldown = 20;
		}

		private bool moonlight = !Main.dayTime;
		private bool magmaMode;
		private bool chlorophyteMode;
		private bool firstTick = true;
		private float turnSpeed;

		public override void FirstTick()
		{
			moonlight = !Main.dayTime;
		}

		public override void AI()
		{
			Player projOwner = Main.player[projectile.owner];

			magmaMode = projectile.ai[1] == 1;
			chlorophyteMode = projectile.ai[1] == 2;

			if (firstTick)
            {
				timeMax += chlorophyteMode ? 120 : (magmaMode ? 60 : 0);
				projectile.timeLeft = timeMax;
				firstTick = false;
			}

			//projectile.light = (chlorophyteMode ? 3f : (magmaMode ? 1.5f : (moonlight ? 0.5f : 1f))) * projectile.scale;
			Distance = (int)(projectile.ai[0]);

			if (magmaMode || chlorophyteMode)
            {
				turnSpeed = chlorophyteMode ? 0.016f : 0.008f;
				UpdateAim(projOwner.MountedCenter);
			}

			projectile.rotation = projectile.velocity.ToRotation();

			projectile.scale += projectile.timeLeft < (timeMax / 3) ? -((float)(((magmaMode || chlorophyteMode) ? 1.5f : (moonlight ? 1f : 0.5f)) / (timeMax / 3))) : (projectile.scale >= ((magmaMode || chlorophyteMode) ? 1.5f : (moonlight ? 1f : 0.5f)) ? 0f : (float)(((magmaMode || chlorophyteMode) ? 1.5f : (moonlight ? 1f : 0.5f)) / (timeMax / 3)));

			projectile.netUpdate = true;

			projectile.Center = projOwner.MountedCenter;

			CastLights();
		}

		private void UpdateAim(Vector2 source)
		{
			Vector2 aim = Vector2.Normalize(Main.MouseWorld - source);
			if (aim.HasNaNs())
			{
				aim = -Vector2.UnitY;
			}

			aim = Vector2.Normalize(Vector2.Lerp(Vector2.Normalize(projectile.velocity), aim, turnSpeed));

			if (aim != projectile.velocity)
			{
				projectile.netUpdate = true;
			}
			projectile.velocity = aim;
		}

		public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
		{
			float radius = ((Distance * 16) / 2);

			float _ = float.NaN;
			return Collision.CheckAABBvLineCollision(targetHitbox.TopRight(), targetHitbox.Size(), projectile.Center, projectile.Center + new Vector2(radius, 0).RotatedBy(projectile.rotation));
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			var player = Main.player[projectile.owner];
			Mod mod = ModLoader.GetMod("AuraClass");

			Vector2 radius = projectile.velocity;
			radius.Normalize();
			radius *= ((Distance * 16) / 2);

			Vector2 mountedCenter = player.MountedCenter;
			Texture2D projTex = Main.projectileTexture[projectile.type];
			Texture2D endTex = mod.GetTexture("Projectiles/LightBeam_End");

			var drawPosition = mountedCenter + radius;
			var remainingVectorToEnd = mountedCenter - drawPosition;
			var remainingVectorToPlayer = drawPosition - mountedCenter;

			float rotation = remainingVectorToEnd.ToRotation() - MathHelper.PiOver2;

			while (true)
			{
				bool end = drawPosition == (mountedCenter + radius);
				float length = remainingVectorToEnd.Length();
				float length_2 = remainingVectorToPlayer.Length();

				if (length < 25f || length_2 < 25f || float.IsNaN(length))
					break;

				drawPosition += remainingVectorToEnd * 30 / length;
				remainingVectorToEnd = mountedCenter - drawPosition;

				Color color = (chlorophyteMode ? new Color(200, 254, 202) : (magmaMode ? new Color(255, 149, 149) : (moonlight ? new Color(191, 186, 255) : new Color(255, 245, 186)))) * ((255 - (float)(projectile.alpha)) / 255f);
				spriteBatch.Draw(end ? endTex : projTex, drawPosition - Main.screenPosition, null, color, rotation, projTex.Size() * 0.5f, new Vector2(projectile.scale * 0.5f, 1f), SpriteEffects.None, 0f);
			}
			return false;
		}

		private void CastLights()
		{
			float radius = ((Distance * 16) / 2);

			Color beamColor = GetBeamColor();

			DelegateMethods.v3_1 = beamColor.ToVector3();
			Utils.PlotTileLine(projectile.Center, projectile.Center + projectile.velocity * radius, 30 / 2, DelegateMethods.CastLight);
		}

		private Color GetBeamColor()
		{
			return chlorophyteMode ? new Color(200, 254, 202) : (magmaMode ? new Color(255, 149, 149) : (moonlight ? new Color(191, 186, 255) : new Color(255, 245, 186)));
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			if (!chlorophyteMode)
            {
				target.AddBuff(magmaMode ? 24 : (moonlight ? 44 : 24), 180);
				//target.AddBuff(magmaMode ? 323 : (moonlight ? 44 : 24), 180); Will be added in 1.4 since hellfire is a 1.4 debuff
			}
		}

		public override bool ShouldUpdatePosition() => false;
	}
}
