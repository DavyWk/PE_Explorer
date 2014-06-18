using System;
using System.IO;
using System.Collections.Generic;

using PE_Explorer.Utils;
using PE_Explorer.Core.DOS;
using PE_Explorer.Core.PE.Headers;
using PE_Explorer.Core.PE.ImportTable;



namespace PE_Explorer.Core.PE
{
    public class PortableExecutable : IDisposable
    {
        public DOSHeader dosHeader;
        public PEHeader peHeader;
        public List<SectionHeader> sections = new List<SectionHeader>();

        private BinaryReader br;

        public PortableExecutable(string fileName)
            : this(new BinaryReader(File.OpenRead(fileName)))
        { }

        public PortableExecutable(BinaryReader binaryReader)
        {
            br = binaryReader;

            dosHeader = new DOSHeader(br);
            br.BaseStream.Seek(dosHeader.GetPEHeaderOffset(), SeekOrigin.Begin); // e_lfanew offset
            peHeader = new PEHeader(br);

            for(int i = 1; i <= peHeader.fileHeader.NumberOfSections; i++)
            {
                sections.Add(new SectionHeader(br));
            }
            sections.TrimExcess();
            long offset = Utils.Utils.RVAToFileOffset(this,peHeader.optionalHeader.ImportDirectory.VirtualAddress);
            br.BaseStream.Seek(offset,SeekOrigin.Begin);
            ImportDescriptor id = new ImportDescriptor(br);
        }

      public void Dispose()
        {
            br.Dispose();
        }
    }
}
