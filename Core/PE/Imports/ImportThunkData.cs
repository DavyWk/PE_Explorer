using System.IO;
using System.Runtime.InteropServices;

namespace Core.PE.Imports
{
    [StructLayout(LayoutKind.Sequential,Pack=1)]
    public struct ImportThunkData
    {
        public uint AddressOfData;
        public ImportThunkData(BinaryReader br)
        {
            AddressOfData = br.ReadUInt32();
        }
    }
}
