using System;
using System.IO;
using System.Collections.Generic;

using Utilities;
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
        public SectionHeader[] sections;
        public Dictionary<string, ImportByName[]> imports = new Dictionary<string, ImportByName[]>();
        // <name,api[]>

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

            // SECTIONS
            List<SectionHeader> shList = new List<SectionHeader>();
            for(int i = 1; i <= peHeader.fileHeader.NumberOfSections; i++)
            {
                shList.Add(new SectionHeader(br));
            }
            sections = shList.ToArray();


            // IMPORT TABLE
            long offset = Utils.RVAToFileOffset(this,peHeader.optionalHeader.ImportDirectory.VirtualAddress);

            if (offset == 0) // null import table
                return;

            br.BaseStream.Seek(offset,SeekOrigin.Begin);
            ImportDescriptor id;

            while((id = new ImportDescriptor(br)).OriginalFirstThunk != 0)
            {

                br.BaseStream.Seek(Utils.RVAToFileOffset(this, id.Name), SeekOrigin.Begin);
                List<char> dllName = new List<char>();
                char c;

                while ((c = br.ReadChar()) != '\0')
                {
                    dllName.Add(c);
                }
                dllName.Add(c); // NULL terminator


                br.BaseStream.Seek(Utils.RVAToFileOffset(this, (id.OriginalFirstThunk != 0 ? id.OriginalFirstThunk : id.FirstThunk)), SeekOrigin.Begin);
                ImportNameTable nameTable = new ImportNameTable(br);
                List<ImportByName> names = new List<ImportByName>();

                for(int i = 0; i < nameTable.Names.Length; i++)
                {
                   
                    int ord;
                    const uint ordFlag = 0x80000000;
                    if ((ord = (int)(nameTable.Names[i].AddressOfData - ordFlag)) > 0) // if import by ordinal
                    {
                        const string ordStr = "Import by Ordinal" + "\0";
                        ImportByName ordImport = new ImportByName
                            {
                                Hint = (ushort)ord,
                                Name = ordStr.ToCharArray(),
                            };
                        names.Add(ordImport);
                    }
                    else
                    {
                        br.BaseStream.Seek(Utils.RVAToFileOffset(this, nameTable.Names[i].AddressOfData), SeekOrigin.Begin);
                        names.Add(new ImportByName(br));
                    }
                    
                }




                imports.Add(new string(dllName.ToArray()), names.ToArray());

                offset += 20; // size of ImportDescriptor
                br.BaseStream.Seek(offset, SeekOrigin.Begin);
            }
            
        }

        public void Dispose()
        {
            br.Dispose();
        }
    }
}
