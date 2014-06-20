using System.IO;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Core.PE.ImportTable
{
    [StructLayout(LayoutKind.Sequential,Pack = 1)]
    public struct ImportNameTable
    {
        public ImportThunkData[] Names;

        public ImportNameTable(BinaryReader br)
        {
            List<ImportThunkData> list = new List<ImportThunkData>();

            ImportThunkData t;
            
            while((t = new ImportThunkData(br)).AddressOfData != 0)
            {
                list.Add(t);
            }
            //list.Add(new ImportThunkData(br)); // the last NULL one

            Names = list.ToArray();


        }
    }
}
