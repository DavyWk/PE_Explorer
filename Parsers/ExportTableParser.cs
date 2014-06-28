using System;
using System.Text;

using Utilities;
using Core;
using Core.PE.Exports;

namespace Parsers
{
    class ExportTableParser
    {
        private readonly Export[] exports;
        public ExportTableParser(Export[] Exports)
        {
            exports = Exports;
        }

        public void Parse()
        {
            if(exports.Length == 0) // no export table
            {
                Logger.Log(ELogTypes.Info,"No export table");
            }

            Console.WriteLine("Press ENTER to see the parsed export table");
            Console.ReadLine();
            Console.WriteLine();

            Console.WriteLine("\t\t\tExport Table");
            Logger.Log(ELogTypes.Info,string.Format("{0} functions",exports.Length));
            Console.WriteLine();
            Console.WriteLine("Ordinal: RVA\tFunction Name");
            Console.WriteLine();

            for(int i = 0; i < exports.Length; i++)
            {
                Export e = exports[i];
                Console.WriteLine("{0:X4}: 0x{1:X}\t{2}", e.Ordinal.ToString("D4"), e.Address, Encoding.ASCII.GetString(e.Name));
            }

            Console.WriteLine();
            Logger.Log(ELogTypes.Info, "Done parsing export table");
        }
    }
}
