using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

using Utilities;
using Core.Utilities;
using Core.DOS;
using Core.PE.Headers;
using Core.PE.Imports;
using Core.PE.Exports;



namespace Core.PE
{
    public class PortableExecutable : IDisposable
    {
        public DOSHeader dosHeader;
        public PEHeader peHeader;
        public SectionHeader[] sections;
        public Dictionary<string, ImportByName[]> imports = new Dictionary<string, ImportByName[]>();
        // <name,funcions[]>
        public Export[] exports;

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
            for(int i = 0; i < peHeader.fileHeader.NumberOfSections; i++)
            {
                shList.Add(new SectionHeader(br));
            }
            sections = shList.ToArray();

            long offset; // used for import and export

            #region Import stuff

            offset = Utils.RVAToFileOffset(this,peHeader.optionalHeader.ImportDirectory.VirtualAddress);

            if (offset > 0)
            {
                br.BaseStream.Seek(offset, SeekOrigin.Begin);
                ImportDescriptor id;

                while ((id = new ImportDescriptor(br)).OriginalFirstThunk != 0)
                {

                    br.BaseStream.Seek(Utils.RVAToFileOffset(this, id.Name), SeekOrigin.Begin);
                    string dllName = Encoding.ASCII.GetString(Utils.ReadString(br));


                    br.BaseStream.Seek(Utils.RVAToFileOffset(this, (id.OriginalFirstThunk != 0 ? id.OriginalFirstThunk : id.FirstThunk)), SeekOrigin.Begin);
                    ImportNameTable nameTable = new ImportNameTable(br);
                    List<ImportByName> names = new List<ImportByName>();

                    for (int i = 0; i < nameTable.Names.Length; i++)
                    {

                        int ord;
                        const uint ordFlag = 0x80000000; // should've been used with a binary AND, but making a substraction does 2 things at once
                        if ((ord = (int)(nameTable.Names[i].AddressOfData - ordFlag)) > 0) // if import by ordinal
                        {
                            const string ordStr = "Import by Ordinal";
                            ImportByName ordImport = new ImportByName
                            {
                                Hint = (ushort)ord,
                                Name = Encoding.ASCII.GetBytes(ordStr),
                            };
                            names.Add(ordImport);
                        }
                        else
                        {
                            br.BaseStream.Seek(Utils.RVAToFileOffset(this, nameTable.Names[i].AddressOfData), SeekOrigin.Begin);
                            names.Add(new ImportByName(br));
                        }

                    }

                    imports.Add(dllName, names.ToArray());

                    offset += 20; // size of ImportDescriptor
                    br.BaseStream.Seek(offset, SeekOrigin.Begin);
                }
            }

           

            #endregion

            #region Export stuff

            offset = Utils.RVAToFileOffset(this, peHeader.optionalHeader.ExportDirectory.VirtualAddress);

            if(offset > 0)
            {
                br.BaseStream.Seek(offset, SeekOrigin.Begin);
                ExportDirectory exportDir = new ExportDirectory(br);

                uint nExports = exportDir.NumberOfFunctions;

                uint namesOffset = Utils.RVAToFileOffset(this,exportDir.AddressOfNames);
                uint functionsOffset = Utils.RVAToFileOffset(this,exportDir.AddressOfFunctions);

                List<Export> lstExport = new List<Export>();
                for (int i = 0; i < nExports; i++)
                {
                    Export e = new Export();

                    br.BaseStream.Seek(namesOffset, SeekOrigin.Begin);
                    br.BaseStream.Seek(Utils.RVAToFileOffset(this, br.ReadUInt32()), SeekOrigin.Begin);
                    e.Name = Utils.ReadString(br);

                    br.BaseStream.Seek(functionsOffset, SeekOrigin.Begin);
                    e.Address = br.ReadUInt32();

                    e.Ordinal = (ushort)(i + exportDir.Base);

                    namesOffset += sizeof(uint);
                    functionsOffset += sizeof(uint);

                    lstExport.Add(e);
                }

                exports = lstExport.ToArray();

            }
        

            #endregion
        }

        public void Dispose()
        {
            br.Dispose();
        }
    }
}
