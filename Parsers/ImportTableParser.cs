using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

using Core.PE;
using Core.PE.Imports;
using Utilities;

namespace Parsers
{
    public class ImportTableParser
    {
        private Dictionary<string, ImportByName[]> imports;

        public ImportTableParser(Dictionary<string, ImportByName[]> Imports)
        {
            imports = Imports;
        }

        public void Parse()
        {
            if (imports.Count == 0)
            {
                Logger.Log(ELogTypes.Error, "No import table");
                return;
            }

            Console.WriteLine("Press ENTER to see the parsed import name table");
            Console.ReadLine();
            Console.WriteLine();
            Console.WriteLine("\t\t\tImport Table");
            Logger.Log(ELogTypes.Info, string.Format("{0} DLLs", imports.Count));
            Console.WriteLine();

            foreach (KeyValuePair<string, ImportByName[]> kv in imports)
            {
                Console.WriteLine("\t{0} ({1} functions)", kv.Key, kv.Value.Length);
                Console.WriteLine("Hint\tFunction Name");
                Console.WriteLine();

                for (int i = 0; i < kv.Value.Length; i++)
                {
                    new ImportByNameParser(ref kv.Value[i]).Parse();
                }

                Console.ReadKey();
                Console.WriteLine();
            }
            Logger.Log(ELogTypes.Info, "Done parsing import table");
            Console.WriteLine();
        }

        private class ImportByNameParser
        {
            private ImportByName name;

            public ImportByNameParser(ref ImportByName importByName)
            {
                name = importByName;
            }

            public void Parse()
            {
                Console.WriteLine("0x{0:X4}\t{1}", name.Hint, Encoding.ASCII.GetString(name.Name));
            }
        }
    }

}
