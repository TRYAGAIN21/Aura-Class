using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace AuraClass.AuraDamageClass
{
    public class AuraGlobalItem : GlobalItem
    {
        public override bool InstancePerEntity => true;

        public byte Celestial;
        public byte Mythic;
        public byte Empowered;
        public byte Mystic;
        public byte Energized;
        public byte Overworked;
        public byte Crumbling;
        public byte Broken;
        public byte Decaying;
        public byte Damaged;
        public byte Scratched;
        public byte Heavy;
        public byte Stringless;

        public AuraGlobalItem()
        {
            Celestial = 0;
            Mythic = 0;
            Empowered = 0;
            Mystic = 0;
            Energized = 0;
            Overworked = 0;
            Crumbling = 0;
            Broken = 0;
            Decaying = 0;
            Damaged = 0;
            Scratched = 0;
            Heavy = 0;
            Stringless = 0;
            Aura = false;
        }

        public override GlobalItem Clone(Item item, Item itemClone)
        {
            AuraGlobalItem myClone = (AuraGlobalItem)base.Clone(item, itemClone);
            myClone.Celestial = Celestial;
            myClone.Mythic = Mythic;
            myClone.Empowered = Empowered;
            myClone.Mystic = Mystic;
            myClone.Energized = Energized;
            myClone.Overworked = Overworked;
            myClone.Crumbling = Crumbling;
            myClone.Broken = Broken;
            myClone.Decaying = Decaying;
            myClone.Damaged = Damaged;
            myClone.Scratched = Scratched;
            myClone.Heavy = Heavy;
            myClone.Stringless = Stringless;
            return myClone;
        }

        public override void NetSend(Item item, BinaryWriter writer)
        {
            writer.Write((byte)Celestial);
            writer.Write((byte)Mythic);
            writer.Write((byte)Empowered);
            writer.Write((byte)Mystic);
            writer.Write((byte)Energized);
            writer.Write((byte)Overworked);
            writer.Write((byte)Crumbling);
            writer.Write((byte)Broken);
            writer.Write((byte)Decaying);
            writer.Write((byte)Damaged);
            writer.Write((byte)Scratched);
            writer.Write((byte)Heavy);
            writer.Write((byte)Stringless);
        }

        public override void NetReceive(Item item, BinaryReader reader)
        {
            Celestial = reader.ReadByte();
            Mythic = reader.ReadByte();
            Empowered = reader.ReadByte();
            Mystic = reader.ReadByte();
            Energized = reader.ReadByte();
            Overworked = reader.ReadByte();
            Crumbling = reader.ReadByte();
            Broken = reader.ReadByte();
            Decaying = reader.ReadByte();
            Damaged = reader.ReadByte();
            Scratched = reader.ReadByte();
            Heavy = reader.ReadByte();
            Stringless = reader.ReadByte();
        }

        public bool Aura { get; }
    }
}