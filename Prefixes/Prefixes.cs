using AuraClass.AuraDamageClass;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria;
using System;

namespace AuraClass.Prefixes
{
    public class AuraPrefix : ModPrefix
    {
        internal static List<byte> AuraPrefixes;
        internal float damageMult = 1f;
        internal float speedMult = 1f;
        internal int rangeBonus = 0;
        internal int critBonus = 0;
        internal float decayMult = 1f;

        public override PrefixCategory Category => PrefixCategory.Custom;

        public AuraPrefix() { }

        public AuraPrefix(float damageMult, float speedMult, int rangeBonus, int critBonus, float decayMult)
        {
            this.damageMult = damageMult;
            this.speedMult = speedMult;
            this.rangeBonus = rangeBonus;
            this.critBonus = critBonus;
            this.decayMult = decayMult;
        }

        public override bool Autoload(ref string name)
        {
            if (base.Autoload(ref name))
            {
                AuraPrefixes = new List<byte>();
                AddAuraPrefix(AuraPrefixType.Relentless, 1.15f, 0.90f, 4, 5, 0.85f);
                AddAuraPrefix(AuraPrefixType.Empowered, 1.10f, 1f, 2, 2, 0.88f);
                AddAuraPrefix(AuraPrefixType.Energized, 1f, 0.85f, 0, 0, 0.92f);
                AddAuraPrefix(AuraPrefixType.Menacing, 1f, 1f, 2, 4, 1f);
                AddAuraPrefix(AuraPrefixType.Lively, 1f, 0.90f, 0, 0, 0.95f);
                AddAuraPrefix(AuraPrefixType.Compact, 1.15f, 1f, -4, 0, 1f);
                AddAuraPrefix(AuraPrefixType.Fading, 0.90f, 1f, -2, 0, 1.02f);
                AddAuraPrefix(AuraPrefixType.Unsynthesized, 1f, 1.10f, 0, 0, 1f);
                AddAuraPrefix(AuraPrefixType.Decaying, 1f, 1f, 0, 0, 1.06f);
            }
            return false;
        }

        public override void Apply(Item item)
        {
            item.GetGlobalItem<AuraDamageClass.AuraGlobalItem>().rangeBoostPrefix = rangeBonus;
            item.GetGlobalItem<AuraDamageClass.AuraGlobalItem>().decayMultPrefix = decayMult;
        }

        public override bool CanRoll(Item item)
        {
            if (this.Aura)
            {
                return item.maxStack == 1 && item.damage > 0 && item.useStyle > 0;
            }
            return false;
        }

        public override void SetStats(ref float damageMult, ref float knockbackMult, ref float useTimeMult, ref float scaleMult, ref float shootSpeedMult, ref float manaMult, ref int critBonus)
        {
            damageMult = this.damageMult;
            useTimeMult = this.speedMult;
            critBonus = this.critBonus;
        }

        private void AddAuraPrefix(AuraPrefixType prefixType, float damageMult = 1f, float speedMult = 1f, int rangeBonus = 0, int critBonus = 0, float decayMult = 1f)
        {
            mod.AddPrefix(prefixType.ToString(), new AuraPrefix(damageMult, speedMult, rangeBonus, critBonus, decayMult));
            AuraPrefixes.Add(mod.GetPrefix(prefixType.ToString()).Type);
        }

        public bool Aura { get; }
    }

    public class AccessoryPrefix : ModPrefix
    {
        internal static List<byte> AccessoryPrefixes;
        internal float decayMult = 1f;
        internal int rangeBonus = 0;

        public override PrefixCategory Category => PrefixCategory.Accessory;

        public AccessoryPrefix() { }

        public AccessoryPrefix(float decayMult, int rangeBonus)
        {
            this.decayMult = decayMult;
            this.rangeBonus = rangeBonus;
        }

        public override bool Autoload(ref string name)
        {
            if (base.Autoload(ref name))
            {
                AccessoryPrefixes = new List<byte>();

                // Range
                AddAccessoryPrefix(AccessoryPrefixType.Radiating, 1f, 6);
                AddAccessoryPrefix(AccessoryPrefixType.Pulsing, 1f, 4);
                AddAccessoryPrefix(AccessoryPrefixType.Vibrating, 1f, 2);

                // Decay Rate
                AddAccessoryPrefix(AccessoryPrefixType.Synchronizing, 0.94f, 0);
                AddAccessoryPrefix(AccessoryPrefixType.Stabilizing, 0.96f, 0);
                AddAccessoryPrefix(AccessoryPrefixType.Regulating, 0.98f, 0);
            }
            return false;
        }

        public override float RollChance(Item item)
        {
            return 1f;
        }

        public override void Apply(Item item)
        {
            item.GetGlobalItem<NormalGlobalItem>().auraDecayMult = decayMult;
            item.GetGlobalItem<NormalGlobalItem>().auraSizeIncrease = rangeBonus;
        }

        public override bool CanRoll(Item item)
        {
            return true;
        }

        private void AddAccessoryPrefix(AccessoryPrefixType prefixType, float decayMult = 1f, int rangeBonus = 0)
        {
            mod.AddPrefix(prefixType.ToString(), new AccessoryPrefix(decayMult, rangeBonus));
            AccessoryPrefixes.Add(mod.GetPrefix(prefixType.ToString()).Type);
        }

        public bool Aura { get; }
    }

    public enum AuraPrefixType : byte
    {
        Relentless,
        Empowered,
        Energized,
        Menacing,
        Lively,
        Compact,
        Fading,
        Unsynthesized,
        Decaying
    }

    public enum AccessoryPrefixType : byte
    {
        Radiating,
        Pulsing,
        Vibrating,
        Synchronizing,
        Stabilizing,
        Regulating
    }
}