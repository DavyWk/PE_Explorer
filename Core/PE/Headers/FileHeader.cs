using System;
using System.IO;
using System.Runtime.InteropServices;

using PE_Explorer.Core.PE.Headers.Enums;

namespace PE_Explorer.Core.PE.Headers
{
    [StructLayout(LayoutKind.Sequential,Pack = 1 )]
    public struct FileHeader
    {
        public ushort Machine; // defined in Core.PE.Headers.Enums.EMachine
        public ushort NumberOfSections;
        public uint TimeDateStamp;
        public uint PointerToSymbolTable;
        public uint NumberOfSymbols;
        public ushort SizeOfOptionalHeader;
        public ushort Characteristics; // defined in Core.PE.Headers.Enums.EImageCharacteristic

        public FileHeader(BinaryReader br)
        {
            Machine = br.ReadUInt16();
            NumberOfSections = br.ReadUInt16();
            TimeDateStamp = br.ReadUInt32();
            PointerToSymbolTable = br.ReadUInt32();
            NumberOfSymbols = br.ReadUInt32();
            SizeOfOptionalHeader = br.ReadUInt16();
            Characteristics = br.ReadUInt16();

            if (Machine == (ushort)EMachine.IA64 || Machine == (ushort)EMachine.AMD64
                || Machine == (ushort)EMachine.Alpha64 || Machine == (ushort)EMachine.APX64)
                throw new NotSupportedException("64-bit PE, not supported.");
            else if (Machine != (ushort)EMachine.I386)
                throw new BadImageFormatException("Unknown file format");
        }
    }

}
