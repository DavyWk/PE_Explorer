using System;
using System.IO;
using System.Collections.Generic;

using Core.PE;
using Core.PE.ImportTable;
using Utilities;

namespace Parsers
{
    public class ImportTableParser
    {
       private PortableExecutable pe;
       public ImportTableParser(PortableExecutable portableExecutable)
        {
            pe = portableExecutable;
        }

       public void Parse()
       {
           Console.WriteLine("Press ENTER to see the parsed import name table");
           Console.ReadLine();
           Console.WriteLine();
           Console.WriteLine("\t\t\tImport Table");
           Logger.Log(ELogTypes.Info, string.Format("{0} DLLs",pe.imports.Count));
           Console.WriteLine();

           foreach(KeyValuePair<string,ImportByName[]> kv in pe.imports)
           {
               Console.WriteLine("\t{0} ({1} functions)", kv.Key,kv.Value.Length);
               Console.WriteLine("Hint\tFunction Name");
               Console.WriteLine();
               for(int i = 0; i < kv.Value.Length; i++)
               {
                   new ImportByNameParser(kv.Value[i]).Parse();
               }
               Console.ReadKey();
               Console.WriteLine();
           }
           Utilities.Logger.Log(Utilities.ELogTypes.Info, "Done parsing import table");
           Console.ReadLine();
       }

        private class ImportByNameParser
        {
            private ImportByName name;

            public ImportByNameParser(ImportByName importByName)
            {
                name = importByName;
            }

            public void Parse()
            {
                Console.WriteLine("0x{0:X}\t{1}", name.Hint, new string(name.Name));
            }
        }
    }

}
