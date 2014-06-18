using System.IO;
using System.Runtime.InteropServices;

namespace PE_Explorer.Core.PE.ImportTable
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct ImportDescriptor
    {
        public uint OriginalFirstThunk; // rva to import name table
        public uint TimeDateStamp;
        public uint ForwarderChain;
        public uint Name;
        public uint FirstThunk; // rva to import table

        public ImportDescriptor(BinaryReader br)
        {
            OriginalFirstThunk = br.ReadUInt32();
            TimeDateStamp = br.ReadUInt32();
            ForwarderChain = br.ReadUInt32();
            Name = br.ReadUInt32();
            FirstThunk = br.ReadUInt32();
        }
    }
}
