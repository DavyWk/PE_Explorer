using System;
using System.IO;

using PE_Explorer.Parsers;
using PE_Explorer.Utils;
using PE_Explorer.Core.PE;

namespace PE_Explorer
{
    class Program
    {
        static void Main(string[] args)
        {
            PortableExecutable pe = null;
            BinaryReader br = null;

            Console.Title = "PE Explorer";
            Logger.Log(ELogTypes.INFO,"Copyright (c) 2014 DavyWk : https://github.com/DavyWk");
            Console.WriteLine();

            if(args.Length != 1)
            {
                Logger.Log(ELogTypes.ERROR, "Usage : \"PE Explorer pathtoPEfile\" ");
                Exit();
            }

            FileInfo fi = new FileInfo(args[0]);
            Logger.Log(ELogTypes.INFO, string.Format("Loading {0}", fi.Name));
            Logger.Log(ELogTypes.INFO, string.Format("File size : {0} bytes", fi.Length));
            Console.WriteLine();
            
            try
            {
                br = new BinaryReader(File.OpenRead(args[0]));
            }
            catch (Exception ex)
            {
                Logger.Log(ELogTypes.ERROR, "Error while opening the file : ");
                Logger.Log(ELogTypes.ERROR, ex.Message);
                Exit();
            }
           
            try
            {
                pe = new PortableExecutable(br);
            }
            catch (Exception ex)
            {
                Logger.Log(ELogTypes.ERROR,ex.Message);
                Exit();
            }

            new ImageCharacteristicsParser(pe.peHeader.fileHeader.Characteristics).Parse();
            Console.WriteLine("\t\t\t Optional Header");
            new OptionalHeaderParser(pe.peHeader.optionalHeader).Parse();
            Console.WriteLine();

            Console.WriteLine("\t\t\t Sections");
            Console.WriteLine();
            Console.WriteLine("Section alignment : 0x{0:X}", pe.peHeader.optionalHeader.SectionAlignment);
            Console.WriteLine("Number of sections : {0}", pe.peHeader.fileHeader.NumberOfSections);
            Console.WriteLine();
            for (int i = 0; i < pe.sections.Count; i++ )
            {
                new SectionParser(pe.sections[i]).Parse();
            }


                Console.ReadLine();
        }

        private static void Exit(int exitCode = 1)
        { // lazyness @ its best
            Console.ReadLine();
            Environment.Exit(exitCode);
        }
            
    }
}
