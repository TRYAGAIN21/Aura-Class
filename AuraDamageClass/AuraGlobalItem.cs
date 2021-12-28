using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;
using Terraria.Localization;

namespace AuraClass.AuraDamageClass
{
    public class AuraGlobalItem : GlobalItem
    {
        public override bool InstancePerEntity => true;

        public int rangeBoostPrefix = 0;
		public float decayMultPrefix = 1f;
		public bool aura = false;

		public override GlobalItem Clone(Item item, Item itemClone)
		{
			AuraGlobalItem myClone = (AuraGlobalItem)base.Clone(item, itemClone);
			myClone.rangeBoostPrefix = rangeBoostPrefix;
			myClone.decayMultPrefix = decayMultPrefix;
			return myClone;
		}

		public override void UpdateAccessory(Item item, Player player, bool hideVisual)
        {
			AuraDamagePlayer modPlayer = AuraDamagePlayer.ModPlayer(player);

			/*if (item.type == 1301)
            {
				modPlayer.auraCrit += 8;
			}
			if (item.type == 1248)
			{
				modPlayer.auraCrit += 10;
			}
			if (item.type == 3015)
			{
				modPlayer.auraCrit += 5;
			}
			if (item.type == 1865 || item.type == 3110)
			{
				modPlayer.auraCrit += 2;
			}
			if ((item.type == 899 && Main.dayTime) || (item.type == 899 && (!Main.dayTime || Main.eclipse)))
			{
				modPlayer.auraCrit += 2;
			}
			if (item.prefix == 67)
			{
				modPlayer.auraCrit += 2;
			}
			if (item.prefix == 68)
			{
				modPlayer.auraCrit += 4;
			}*/
		}

		public bool Aura { get; }
    }
}
