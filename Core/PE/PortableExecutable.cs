using System;
using System.IO;
using System.Collections.Generic;

using Utils;
using Core.Utilities;
using Core.DOS;
using Core.PE.Headers;
using Core.PE.ImportTable;



namespace Core.PE
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


         /*  TESTING
          
            long offset = Utilities.Utils.RVAToFileOffset(this,peHeader.optionalHeader.ImportDirectory.VirtualAddress);
            br.BaseStream.Seek(offset,SeekOrigin.Begin);
            ImportDescriptor id = new ImportDescriptor(br);
            br.BaseStream.Seek(Utilities.Utils.RVAToFileOffset(this,id.OriginalFirstThunk),SeekOrigin.Begin);
            ImportNameTable nameTable = new ImportNameTable(br);

            br.BaseStream.Seek(Utilities.Utils.RVAToFileOffset(this,nameTable.Names[0].AddressOfData),SeekOrigin.Begin);
            ImportByName name = new ImportByName(br);
            Logger.Log(ELogTypes.INFO, "Found first IMPORT_BY_NAME");
            Logger.Log(ELogTypes.INFO,string.Format("Hint : 0x{0:X}  API : {1}",name.Hint,new string(name.Name)));
          */
        }

      public void Dispose()
        {
            br.Dispose();
        }
    }
}
