using System;
using System.IO;

using Parsers;
using Core.PE;
using Utils;

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

            new PortableExecutableParser(pe).Parse();


                Console.ReadLine();
        }

        private static void Exit(int exitCode = 1)
        { // lazyness @ its best
            Console.ReadLine();
            Environment.Exit(exitCode);
        }
            
    }
}
