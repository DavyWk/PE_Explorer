using  System;

using Core.PE;
using Core.PE.Headers;

namespace Parsers
{
    class PortableExecutableParser
    {
        private PortableExecutable pe;
        public PortableExecutableParser(PortableExecutable portableExecutable)
        {
            pe = portableExecutable;
        }

        public void Parse()
        {
            new ImageCharacteristicsParser(pe.peHeader.fileHeader.Characteristics).Parse();
            new SubsystemParser(pe.peHeader.optionalHeader.Subsystem).Parse();

            Console.WriteLine();

            Console.WriteLine("\t\t\t Optional Header");
            new OptionalHeaderParser(ref pe.peHeader.optionalHeader).Parse();
            Console.WriteLine();

            Console.WriteLine("\t\t\t Sections");
            Console.WriteLine();
            Console.WriteLine("Section alignment : 0x{0:X}", pe.peHeader.optionalHeader.SectionAlignment);
            Console.WriteLine("Number of sections : {0}", pe.peHeader.fileHeader.NumberOfSections);
            Console.WriteLine();
            for (int i = 0; i < pe.sections.Count; i++)
            {
                SectionHeader sh = pe.sections[i];
                new SectionParser(ref sh).Parse();
            }
        }
    }
}
