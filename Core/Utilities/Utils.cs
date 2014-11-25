using System.IO;
using System.Text;
using System.Collections.Generic;

using Core.PE;
using Core.PE.Headers;

namespace Core.Utilities
{
    public static class Utils
    {
        /// <summary>
        /// Converts a RVA to a file offset
        /// </summary>
        /// <param name="pe">The PortbleExecutable</param>
        /// <param name="rva">The RVA to convert</param>
        /// <returns>A file offset corresponding to the RVA</returns>
        public static uint RVAToFileOffset(PortableExecutable pe, uint rva)
        {
            if (rva <= 0)
                return 0;

            SectionHeader section;
            for (int i = 0; i < pe.sections.Length; i++)
            {
                section = pe.sections[i];
                if (rva >= section.VirtualAddress && rva < section.VirtualAddress + section.VirtualSize)
                {
                    return section.PointerToRawData + (rva - section.VirtualAddress);
                }
            }

            return 0;
        }

        /// <summary>
        /// Reads a null-terminated string from a binary reader
        /// </summary>
        /// <param name="br">The BinaryReader to read from</param>
        /// <returns>A null-terminated byte array containning the string</returns>
        public static byte[] ReadString(BinaryReader br)
        {
            var list = new List<byte>();

            byte c;
            while ((c = br.ReadByte()) != '\0')
            {
                list.Add(c);
            }
            list.Add(c); // null terminator

            return list.ToArray();
        }
    }
}
