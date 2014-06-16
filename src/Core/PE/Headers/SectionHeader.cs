using System.Runtime.InteropServices;
using System.IO;

using PE_Explorer.Core.PE.Headers.Enums;

namespace PE_Explorer.Core.PE.Headers
{
    [StructLayout(LayoutKind.Sequential,Pack = 1)]
    public struct SectionHeader
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public char[] Name;
        public uint VirtualSize; // OR PhysicalAddress
        public uint VirtualAddress; // First byte of the section when loaded in memory
        public uint SizeOfRawData; // Size of initialized data on disk
        public uint PointerToRawData; //
        public uint PointerToRelocations; // File pointer to the beginning of the relocation entries for the section
        public uint PointerToLinenumbers; 
        public ushort NumberOfRelocations; // Number of relocation entries
        public ushort NumberOfLinenumbers;
        public uint Characteristics;

        public SectionHeader(BinaryReader br)
        {
            Name = br.ReadChars(8);
            VirtualSize = br.ReadUInt32();
            VirtualAddress = br.ReadUInt32();
            SizeOfRawData = br.ReadUInt32();
            PointerToRawData = br.ReadUInt32();
            PointerToRelocations = br.ReadUInt32();
            PointerToLinenumbers = br.ReadUInt32();
            NumberOfRelocations = br.ReadUInt16();
            NumberOfLinenumbers = br.ReadUInt16();
            Characteristics = br.ReadUInt32();
            
        }
    }
}
