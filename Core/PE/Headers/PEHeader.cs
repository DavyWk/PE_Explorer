using System.IO;
using System.Runtime.InteropServices;

namespace PE_Explorer.Core.PE.Headers
{
  
     [StructLayout(LayoutKind.Sequential,Pack = 1)]
    public struct PEHeader
    {
        public uint Signature;
        public FileHeader fileHeader;
        public OptionalHeader optionalHeader;

        public PEHeader(BinaryReader br)
        {
            Signature = br.ReadUInt32();
            fileHeader = new FileHeader(br);
            optionalHeader = new OptionalHeader(br);
        }

         public bool CheckSignature()
        {
            return Signature == 0x4550; // PE\0\0
        }
    }
}
