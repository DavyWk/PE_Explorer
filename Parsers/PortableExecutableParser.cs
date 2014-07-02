using Core;

namespace Parsers
{
    class PortableExecutableParser
    {
        private readonly PortableExecutable pe;
        public PortableExecutableParser(PortableExecutable portableExecutable)
        {
            pe = portableExecutable;
        }

        public void Parse()
        {
            new ImageCharacteristicsParser(pe.peHeader.fileHeader.Characteristics).Parse();
            new SubsystemParser(pe.peHeader.optionalHeader.Subsystem).Parse();
            new OptionalHeaderParser(ref pe.peHeader.optionalHeader).Parse();
            new SectionParser(pe.sections, pe.peHeader.optionalHeader.SectionAlignment).Parse();
            new ImportTableParser(pe.imports).Parse();
            new ExportTableParser(pe.exports).Parse();
        }
    }
}
