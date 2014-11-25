using System;

using Core.PE.Headers;

namespace Parsers
{
    class OptionalHeaderParser
    {
        private readonly OptionalHeader header;

        public OptionalHeaderParser(ref OptionalHeader optionalHeader)
        {
            header = optionalHeader;
        }

        public void Parse()
        {
            Console.WriteLine("\t\t\t Optional Header");

            Console.WriteLine("Image base : 0x{0:X}",header.ImageBase);
            Console.WriteLine("Size of image : 0x{0:X} bytes", header.SizeOfImage);
            Console.WriteLine("Entry point offset : 0x{0:X}",header.AddressOfEntryPoint);
            Console.WriteLine("File alignment : 0x{0:X} bytes", header.FileAlignment);
            Console.WriteLine("Base of code : 0x{0:X}", header.ImageBase + header.BaseOfCode);
            Console.WriteLine("Base of data : 0x{0:X}", header.ImageBase + header.BaseOfData);
            Console.WriteLine("Stack reserved : 0x{0:X} bytes", header.SizeOfStackReserve);
            Console.WriteLine("Heap reserved : 0x{0:X} bytes", header.SizeOfHeapReserve);
            Console.WriteLine("IsDotNET : {0}", (header.DOTNETMetadata.VirtualAddress != 0));

            Console.WriteLine();
        }
    }
}
