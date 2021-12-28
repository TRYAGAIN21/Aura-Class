using Terraria;
using Terraria.ModLoader;

namespace AuraClass.Buffs
{
	public class Irradiated : ModBuff
	{
		public override bool Autoload(ref string name, ref string texture) {
			texture = "Terraria/Buff_39";
			return base.Autoload(ref name, ref texture);
		}

		public override void SetDefaults() {
			DisplayName.SetDefault("Irradiated");
			Description.SetDefault("Losing life and flames attack your friends");
		}

		public override void Update(NPC npc, ref int buffIndex) 
		{
			npc.GetGlobalNPC<AuraClassNPC>().irradiated = true;
			npc.AddBuff(39, 2);
		}
	}
}
