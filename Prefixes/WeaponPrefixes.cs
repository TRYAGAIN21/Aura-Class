using AuraClass.AuraDamageClass;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria;

namespace AuraClass.Prefixes
{
    #region Stringless
    public class StringlessPrefix : ModPrefix
    {
        private readonly byte _power;

        public override void SetStats(ref float damageMult, ref float knockbackMult, ref float useTimeMult, ref float scaleMult, ref float shootSpeedMult, ref float manaMult, ref int critBonus)
        {
            useTimeMult = 1.1f;
            damageMult = 0.85f;
            shootSpeedMult = 0.65f;
            knockbackMult = 0.75f;
        }

        public override bool CanRoll(Item item)
        {
            if (item.useAmmo == AmmoID.Arrow)
            {
                return true;
            }
            else
            {
                return false;
            }
            return false;
        }

        public override bool Autoload(ref string name)
        {
            if (!base.Autoload(ref name))
            {
                return false;
            }

            mod.AddPrefix("Stringless", new StringlessPrefix(1));
            return false;
        }

        public override PrefixCategory Category => PrefixCategory.Ranged;

        public StringlessPrefix()
        {

        }

        public StringlessPrefix(byte power)
        {
            _power = power;
        }

        public override void Apply(Item item)
        {
            item.GetGlobalItem<AuraGlobalItem>().Stringless = _power;
        }

        public bool Aura { get; }
    }
    #endregion

    #region Heavy
    public class HeavyPrefix : ModPrefix
    {
        private readonly byte _power;

        public override void SetStats(ref float damageMult, ref float knockbackMult, ref float useTimeMult, ref float scaleMult, ref float shootSpeedMult, ref float manaMult, ref int critBonus)
        {
            useTimeMult = 1.40f;
            damageMult = 1.15f;
            knockbackMult = 1.05f;
        }

        public override bool CanRoll(Item item)
        {
            return true;
        }

        public override bool Autoload(ref string name)
        {
            if (!base.Autoload(ref name))
            {
                return false;
            }

            mod.AddPrefix("Heavy", new HeavyPrefix(1));
            return false;
        }

        public override PrefixCategory Category => PrefixCategory.Melee;

        public HeavyPrefix()
        {

        }

        public HeavyPrefix(byte power)
        {
            _power = power;
        }

        public override void Apply(Item item)
        {
            item.GetGlobalItem<AuraGlobalItem>().Heavy = _power;
        }
    }
    #endregion

    #region Celestial
    public class CelestialPrefix : ModPrefix
    {
        private readonly byte _power;

        public override void SetStats(ref float damageMult, ref float knockbackMult, ref float useTimeMult, ref float scaleMult, ref float shootSpeedMult, ref float manaMult, ref int critBonus)
        {
            damageMult = 1.30f;
        }

        public override bool CanRoll(Item item)
        {
            if (this.Aura)
            {
                return true;
            }
            else
            {
                return false;
            }
            return false;
        }

        public override bool Autoload(ref string name)
        {
            if (!base.Autoload(ref name))
            {
                return false;
            }

            mod.AddPrefix("Celestial", new CelestialPrefix(1));
            return false;
        }

        public override PrefixCategory Category => PrefixCategory.Custom;

        public CelestialPrefix()
        {

        }

        public CelestialPrefix(byte power)
        {
            _power = power;
        }

        public override void Apply(Item item)
        {
            if (this.Aura)
            {
                item.GetGlobalItem<AuraGlobalItem>().Celestial = _power;
            }
        }

        public bool Aura { get; }
    }
    #endregion

    #region Mythic
    public class MythicPrefix : ModPrefix
    {
        private readonly byte _power;

        public override void SetStats(ref float damageMult, ref float knockbackMult, ref float useTimeMult, ref float scaleMult, ref float shootSpeedMult, ref float manaMult, ref int critBonus)
        {
            damageMult = 1.20f;
        }

        public override bool CanRoll(Item item)
        {
            if (this.Aura)
            {
                return true;
            }
            else
            {
                return false;
            }
            return false;
        }

        public override bool Autoload(ref string name)
        {
            if (!base.Autoload(ref name))
            {
                return false;
            }

            mod.AddPrefix("Mythic", new MythicPrefix(1));
            return false;
        }

        public override PrefixCategory Category => PrefixCategory.Custom;

        public MythicPrefix()
        {

        }

        public MythicPrefix(byte power)
        {
            _power = power;
        }

        public override void Apply(Item item)
        {
            if (this.Aura)
            {
                item.GetGlobalItem<AuraGlobalItem>().Mythic = _power;
            }
        }

        public bool Aura { get; }
    }
    #endregion

    #region Empowered
    public class EmpoweredPrefix : ModPrefix
    {
        private readonly byte _power;

        public override void SetStats(ref float damageMult, ref float knockbackMult, ref float useTimeMult, ref float scaleMult, ref float shootSpeedMult, ref float manaMult, ref int critBonus)
        {
            damageMult = 1.15f;
        }

        public override bool CanRoll(Item item)
        {
            if (this.Aura)
            {
                return true;
            }
            else
            {
                return false;
            }
            return false;
        }

        public override bool Autoload(ref string name)
        {
            if (!base.Autoload(ref name))
            {
                return false;
            }

            mod.AddPrefix("Empowered", new EmpoweredPrefix(1));
            return false;
        }

        public override PrefixCategory Category => PrefixCategory.Custom;

        public EmpoweredPrefix()
        {
            
        }

        public EmpoweredPrefix(byte power)
        {
            _power = power;
        }

        public override void Apply(Item item)
        {
            if (this.Aura)
            {
                item.GetGlobalItem<AuraGlobalItem>().Empowered = _power;
            }
        }

        public bool Aura { get; }
    }
    #endregion

    #region Mystic
    public class MysticPrefix : ModPrefix
    {
        private readonly byte _power;

        public override void SetStats(ref float damageMult, ref float knockbackMult, ref float useTimeMult, ref float scaleMult, ref float shootSpeedMult, ref float manaMult, ref int critBonus)
        {
            damageMult = 1.10f;
        }

        public override bool CanRoll(Item item)
        {
            if (this.Aura)
            {
                return true;
            }
            else
            {
                return false;
            }
            return false;
        }

        public override bool Autoload(ref string name)
        {
            if (!base.Autoload(ref name))
            {
                return false;
            }

            mod.AddPrefix("Mystic", new MysticPrefix(1));
            return false;
        }

        public override PrefixCategory Category => PrefixCategory.Custom;

        public MysticPrefix()
        {

        }

        public MysticPrefix(byte power)
        {
            _power = power;
        }

        public override void Apply(Item item)
        {
            if (this.Aura)
            {
                item.GetGlobalItem<AuraGlobalItem>().Mystic = _power;
            }
        }

        public bool Aura { get; }
    }
    #endregion

    #region Energized
    public class EnergizedPrefix : ModPrefix
    {
        private readonly byte _power;

        public override void SetStats(ref float damageMult, ref float knockbackMult, ref float useTimeMult, ref float scaleMult, ref float shootSpeedMult, ref float manaMult, ref int critBonus)
        {
            damageMult = 1.07f;
        }

        public override bool CanRoll(Item item)
        {
            if (this.Aura)
            {
                return true;
            }
            else
            {
                return false;
            }
            return false;
        }

        public override bool Autoload(ref string name)
        {
            if (!base.Autoload(ref name))
            {
                return false;
            }

            mod.AddPrefix("Energized", new EnergizedPrefix(1));
            return false;
        }

        public override PrefixCategory Category => PrefixCategory.Custom;

        public EnergizedPrefix()
        {

        }

        public EnergizedPrefix(byte power)
        {
            _power = power;
        }

        public override void Apply(Item item)
        {
            if (this.Aura)
            {
                item.GetGlobalItem<AuraGlobalItem>().Energized = _power;
            }
        }

        public bool Aura { get; }
    }
    #endregion

    #region Overworked
    public class OverworkedPrefix : ModPrefix
    {
        private readonly byte _power;

        public override void SetStats(ref float damageMult, ref float knockbackMult, ref float useTimeMult, ref float scaleMult, ref float shootSpeedMult, ref float manaMult, ref int critBonus)
        {
            damageMult = 0.70f;
        }

        public override bool CanRoll(Item item)
        {
            if (this.Aura)
            {
                return true;
            }
            else
            {
                return false;
            }
            return false;
        }

        public override bool Autoload(ref string name)
        {
            if (!base.Autoload(ref name))
            {
                return false;
            }

            mod.AddPrefix("Overworked", new OverworkedPrefix(1));
            return false;
        }

        public override PrefixCategory Category => PrefixCategory.Custom;

        public OverworkedPrefix()
        {

        }

        public OverworkedPrefix(byte power)
        {
            _power = power;
        }

        public override void Apply(Item item)
        {
            if (this.Aura)
            {
                item.GetGlobalItem<AuraGlobalItem>().Overworked = _power;
            }
        }

        public bool Aura { get; }
    }
    #endregion

    #region Crumbling
    public class CrumblingPrefix : ModPrefix
    {
        private readonly byte _power;

        public override void SetStats(ref float damageMult, ref float knockbackMult, ref float useTimeMult, ref float scaleMult, ref float shootSpeedMult, ref float manaMult, ref int critBonus)
        {
            damageMult = 0.76f;
        }

        public override bool CanRoll(Item item)
        {
            if (this.Aura)
            {
                return true;
            }
            else
            {
                return false;
            }
            return false;
        }

        public override bool Autoload(ref string name)
        {
            if (!base.Autoload(ref name))
            {
                return false;
            }

            mod.AddPrefix("Crumbling", new CrumblingPrefix(1));
            return false;
        }

        public override PrefixCategory Category => PrefixCategory.Custom;

        public CrumblingPrefix()
        {

        }

        public CrumblingPrefix(byte power)
        {
            _power = power;
        }

        public override void Apply(Item item)
        {
            if (this.Aura)
            {
                item.GetGlobalItem<AuraGlobalItem>().Crumbling = _power;
            }
        }

        public bool Aura { get; }
    }
    #endregion

    #region Broken
    public class BrokenPrefix : ModPrefix
    {
        private readonly byte _power;

        public override void SetStats(ref float damageMult, ref float knockbackMult, ref float useTimeMult, ref float scaleMult, ref float shootSpeedMult, ref float manaMult, ref int critBonus)
        {
            damageMult = 0.83f;
        }

        public override bool CanRoll(Item item)
        {
            if (this.Aura)
            {
                return true;
            }
            else
            {
                return false;
            }
            return false;
        }

        public override bool Autoload(ref string name)
        {
            if (!base.Autoload(ref name))
            {
                return false;
            }

            mod.AddPrefix("Broken", new BrokenPrefix(1));
            return false;
        }

        public override PrefixCategory Category => PrefixCategory.Custom;

        public BrokenPrefix()
        {

        }

        public BrokenPrefix(byte power)
        {
            _power = power;
        }

        public override void Apply(Item item)
        {
            if (this.Aura)
            {
                item.GetGlobalItem<AuraGlobalItem>().Broken = _power;
            }
        }

        public bool Aura { get; }
    }
    #endregion

    #region Decaying
    public class DecayingPrefix : ModPrefix
    {
        private readonly byte _power;

        public override void SetStats(ref float damageMult, ref float knockbackMult, ref float useTimeMult, ref float scaleMult, ref float shootSpeedMult, ref float manaMult, ref int critBonus)
        {
            damageMult = 0.87f;
        }

        public override bool CanRoll(Item item)
        {
            if (this.Aura)
            {
                return true;
            }
            else
            {
                return false;
            }
            return false;
        }

        public override bool Autoload(ref string name)
        {
            if (!base.Autoload(ref name))
            {
                return false;
            }

            mod.AddPrefix("Decaying", new DecayingPrefix(1));
            return false;
        }

        public override PrefixCategory Category => PrefixCategory.Custom;

        public DecayingPrefix()
        {

        }

        public DecayingPrefix(byte power)
        {
            _power = power;
        }

        public override void Apply(Item item)
        {
            if (this.Aura)
            {
                item.GetGlobalItem<AuraGlobalItem>().Decaying = _power;
            }
        }

        public bool Aura { get; }
    }
    #endregion

    #region Damaged
    public class DamagedPrefix : ModPrefix
    {
        private readonly byte _power;

        public override void SetStats(ref float damageMult, ref float knockbackMult, ref float useTimeMult, ref float scaleMult, ref float shootSpeedMult, ref float manaMult, ref int critBonus)
        {
            damageMult = 0.91f;
        }

        public override bool CanRoll(Item item)
        {
            if (this.Aura)
            {
                return true;
            }
            else
            {
                return false;
            }
            return false;
        }

        public override bool Autoload(ref string name)
        {
            if (!base.Autoload(ref name))
            {
                return false;
            }

            mod.AddPrefix("Damaged", new DamagedPrefix(1));
            return false;
        }

        public override PrefixCategory Category => PrefixCategory.Custom;

        public DamagedPrefix()
        {

        }

        public DamagedPrefix(byte power)
        {
            _power = power;
        }

        public override void Apply(Item item)
        {
            if (this.Aura)
            {
                item.GetGlobalItem<AuraGlobalItem>().Damaged = _power;
            }
        }

        public bool Aura { get; }
    }
    #endregion

    #region Scratched
    public class ScratchedPrefix : ModPrefix
    {
        private readonly byte _power;

        public override void SetStats(ref float damageMult, ref float knockbackMult, ref float useTimeMult, ref float scaleMult, ref float shootSpeedMult, ref float manaMult, ref int critBonus)
        {
            damageMult = 0.94f;
        }

        public override bool CanRoll(Item item)
        {
            if (this.Aura)
            {
                return true;
            }
            else
            {
                return false;
            }
            return false;
        }

        public override bool Autoload(ref string name)
        {
            if (!base.Autoload(ref name))
            {
                return false;
            }

            mod.AddPrefix("Scratched", new ScratchedPrefix(1));
            return false;
        }

        public override PrefixCategory Category => PrefixCategory.Custom;

        public ScratchedPrefix()
        {

        }

        public ScratchedPrefix(byte power)
        {
            _power = power;
        }

        public override void Apply(Item item)
        {
            if (this.Aura)
            {
                item.GetGlobalItem<AuraGlobalItem>().Scratched = _power;
            }
        }

        public bool Aura { get; }
    }
    #endregion
}