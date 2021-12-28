using Terraria;
using Terraria.ModLoader;

namespace AuraClass.Buffs
{
	public class Tracked2 : ModBuff
	{
		public override bool Autoload(ref string name, ref string texture) {
			texture = "Terraria/Buff_122";
			return base.Autoload(ref name, ref texture);
		}

		public override void SetDefaults() {
			DisplayName.SetDefault("Tracked");
			Description.SetDefault("You take more damage");
		}

		public override void Update(NPC npc, ref int buffIndex) 
		{
			npc.GetGlobalNPC<AuraClassNPC>().tracked2 = true;
		}
	}
}
