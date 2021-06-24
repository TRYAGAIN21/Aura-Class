using AuraClass;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace AuraClass.AuraDamageClass
{
	// This class handles everything for our custom damage class
	// Any class that we wish to be using our custom damage class will derive from this class, instead of ModItem
	public abstract class AuraItem : AuraClassItem
	{
		public override bool CloneNewInstances => true;

		// Custom items should override this to set their defaults
		public virtual void SafeShoot()
        {

        }

		public virtual void SafeSetDefaults()
        {

        }

		// By making the override sealed, we prevent derived classes from further overriding the method and enforcing the use of SafeSetDefaults()
		// We do this to ensure that the vanilla damage types are always set to false, which makes the custom damage type work
		public sealed override void SetDefaults() {
			// all vanilla damage types must be false for custom damage types to work
			item.melee = false;
			item.ranged = false;
			item.magic = false;
			item.thrown = false;
			item.summon = false;
			SafeSetDefaults();
		}

		// As a modder, you could also opt to make these overrides also sealed. Up to the modder
		public override void ModifyWeaponDamage(Player player, ref float add, ref float mult, ref float flat) {
			add += AuraDamagePlayer.ModPlayer(player).auraDamageAdd;
			mult *= AuraDamagePlayer.ModPlayer(player).auraDamageMult;
		}

		// Because we want the damage tooltip to show our custom damage, we need to modify it
		public override void ModifyTooltips(List<TooltipLine> tooltips) 
		{
			TooltipLine tt = tooltips.FirstOrDefault(x => x.Name == "Damage" && x.mod == "Terraria");
			int index = tooltips.FindIndex(x => x.Name == "Damage" && x.mod == "Terraria");
			if (tt != null)
			{
				// take reverse for 'damage',  grab translation
				string[] split = tt.text.Split(' ');
				// todo: translation alchemical
				tt.text = split.First() + " aura " + split.Last();
			}
		}

		public override int ChoosePrefix(UnifiedRandom rand)
		{
			if (this.Aura && rand.NextBool(50))
			{
				return mod.PrefixType("Celestial");
			}
			else if (this.Aura && rand.NextBool(30))
			{
				return mod.PrefixType("Mythic");
			}
			else if (this.Aura && rand.NextBool(15))
			{
				return mod.PrefixType("Empowered");
			}
			else if (this.Aura && rand.NextBool(10))
			{
				return mod.PrefixType("Mystic");
			}
			else if (this.Aura && rand.NextBool(5))
			{
				return mod.PrefixType("Energized");
			}
			else if (this.Aura && rand.NextBool(7))
			{
				return mod.PrefixType("Overworked");
			}
			else if (this.Aura && rand.NextBool(5))
			{
				return mod.PrefixType("Crumbling");
			}
			else if (this.Aura && rand.NextBool(4))
			{
				return mod.PrefixType("Broken");
			}
			else if (this.Aura && rand.NextBool(3))
			{
				return mod.PrefixType("Decaying");
			}
			else if (this.Aura && rand.NextBool(2))
			{
				return mod.PrefixType("Damaged");
			}
			else if (this.Aura)
			{
				return mod.PrefixType("Scratched");
			}
			return -1;
		}

		public sealed override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			SafeShoot();
			return true;
		}

		public bool Aura { get; } = true;
	}
}
