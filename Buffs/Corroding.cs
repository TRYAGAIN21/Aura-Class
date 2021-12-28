using Terraria;
using Terraria.ModLoader;

namespace AuraClass.Buffs
{
	public class Corroding : ModBuff
	{
		public override bool Autoload(ref string name, ref string texture) {
			texture = "Terraria/Buff_69";
			return base.Autoload(ref name, ref texture);
		}

		public override void SetDefaults() {
			DisplayName.SetDefault("Corroding");
			Description.SetDefault("Reduced protection and ichor droplets fall on you");
		}

		public override void Update(NPC npc, ref int buffIndex) 
		{
			npc.GetGlobalNPC<AuraClassNPC>().corroding = true;
			npc.AddBuff(69, 2);
		}
	}
}
