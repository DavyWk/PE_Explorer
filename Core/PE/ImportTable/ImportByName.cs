using System.IO;
using System.Collections.Generic;

namespace Core.PE.ImportTable
{
    public struct ImportByName
    {
        public ushort Hint;
        public char[] Name;

        public ImportByName(BinaryReader br)
        {
            Hint = br.ReadUInt16();

            List<char> chars = new List<char>();
            char c;
            while((c = br.ReadChar()) != '\0')
            {
                chars.Add(c);
            }
            chars.Add(c); // null terminator

            Name = chars.ToArray();
        }
    }
}
