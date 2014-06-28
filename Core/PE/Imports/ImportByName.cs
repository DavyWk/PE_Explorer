using System.IO;
using System.Collections.Generic;

using Core.Utilities;

namespace Core.PE.Imports
{
    public struct ImportByName
    {
        public ushort Hint;
        public byte[] Name;
        
        public ImportByName(BinaryReader br)
        {
            Hint = br.ReadUInt16();

            Name = Utils.ReadString(br);
        }
    }
}
