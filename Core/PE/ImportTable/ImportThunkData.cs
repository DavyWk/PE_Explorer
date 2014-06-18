using System.IO;

namespace PE_Explorer.Core.PE.ImportTable
{
    public struct ImportThunkData
    {
        public uint AddressOfData;
        public ImportThunkData(BinaryReader br)
        {
            AddressOfData = br.ReadUInt32();
        }
    }
}
