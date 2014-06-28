using System.IO;
using System.Runtime.InteropServices;

namespace Core.PE.Exports
{
    [StructLayout(LayoutKind.Sequential,Pack=1)]
    public struct ExportDirectory
    {
        public uint Characteristics;
        public uint TimeDateStamp;
        public ushort MajorVersion;
        public ushort MinorVersion;
        public uint Name;
        public uint Base;
        public uint NumberOfFunctions;
        public uint NumberOfNames;
        public uint AddressOfFunctions;
        public uint AddressOfNames;
        public uint AddressOfNameOrinals;

        public ExportDirectory(BinaryReader br)
        {
            Characteristics = br.ReadUInt32();
            TimeDateStamp = br.ReadUInt32();
            MajorVersion = br.ReadUInt16();
            MinorVersion = br.ReadUInt16();
            Name = br.ReadUInt32();
            Base = br.ReadUInt32();
            NumberOfFunctions = br.ReadUInt32();
            NumberOfNames = br.ReadUInt32();
            AddressOfFunctions = br.ReadUInt32();
            AddressOfNames = br.ReadUInt32();
            AddressOfNameOrinals = br.ReadUInt32();
        }
    }
}
