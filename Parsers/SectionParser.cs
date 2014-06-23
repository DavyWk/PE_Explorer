using System;
using System.Text;

using Core.PE.Enums;
using Core.PE.Headers;


namespace Parsers
{
    public class SectionParser
    {
        private readonly SectionHeader[] headers;
        private readonly uint alignment;
        public SectionParser(SectionHeader[] sectionHeaders, uint sectionAlignment)
        {
            headers = sectionHeaders;
            alignment = sectionAlignment;
        }

        public void Parse()
        {
            Console.WriteLine("\t\t\t Sections");
            Console.WriteLine();
            Console.WriteLine("Section alignment : 0x{0:X}", alignment);
            Console.WriteLine("Number of sections : {0}", headers.Length);
            Console.WriteLine();

            for (int i = 0; i < headers.Length; i++)
            {
                new SectionHeaderParser(ref headers[i]).Parse();
            }
        }


        private class SectionHeaderParser
        {
            private readonly SectionHeader header;
            public SectionHeaderParser(ref SectionHeader sectionHeader)
            {
                header = sectionHeader;
            }

            public void Parse()
            {
                Console.WriteLine("Section Name : {0}", new string(header.Name));
                Console.WriteLine("Virtual Size : 0x{0} bytes", header.VirtualSize);
                Console.WriteLine("Virtual Address : 0x{0:X}", header.VirtualAddress);
                Console.WriteLine("Size of Raw Data : 0x{0:X} bytes", header.SizeOfRawData);
                Console.WriteLine("Pointer to Raw Data : 0x{0:X}", header.PointerToRawData);
                Console.WriteLine("Characteristics :{0}", new SectionCharacteristicsParser(header.Characteristics).Parse());

                Console.WriteLine();
            }

            private class SectionCharacteristicsParser
            {
                private uint characteristics;
                public SectionCharacteristicsParser(uint sectionCharacteristics)
                {
                    characteristics = sectionCharacteristics;
                }

                public string Parse()
                { // returns a formatted string with all of the section's characteristics

                    StringBuilder sb = new StringBuilder(string.Empty);
                    if (characteristics == 0)
                        return sb.ToString();

                    if ((characteristics & (uint)ESectionCharacteristics.Code) > 0)
                        sb.Append(" CODE |");
                    if ((characteristics & (uint)ESectionCharacteristics.InitializedData) > 0)
                        sb.Append(" INITIALIZED DATA |");
                    if ((characteristics & (uint)ESectionCharacteristics.UninitializedData) > 0)
                        sb.Append(" UNINITIALIZED DATA |");

                    if ((characteristics & (uint)ESectionCharacteristics.Discardable) > 0)
                        sb.Append(" DISCARDABLE |");
                    if ((characteristics & (uint)ESectionCharacteristics.NotCached) > 0)
                        sb.Append(" NOT CACHED |");
                    if ((characteristics & (uint)ESectionCharacteristics.NotPaged) > 0)
                        sb.Append(" NOT PAGED |");
                    if ((characteristics & (uint)ESectionCharacteristics.Shared) > 0)
                        sb.Append(" SHARED |");
                    if ((characteristics & (uint)ESectionCharacteristics.Execute) > 0)
                        sb.Append(" EXECUTE |");
                    if ((characteristics & (uint)ESectionCharacteristics.Read) > 0)
                        sb.Append(" READ |");
                    if ((characteristics & (uint)ESectionCharacteristics.Write) > 0)
                        sb.Append(" WRITE |");

                    if ((characteristics & (uint)ESectionCharacteristics.Info) > 0)
                        sb.Append("INFO |");
                    if ((characteristics & (uint)ESectionCharacteristics.Remove) > 0)
                        sb.Append("REMOVE |");
                    if ((characteristics & (uint)ESectionCharacteristics.ComDat) > 0)
                        sb.Append("COMDAT |");


                    return sb.ToString();
                }
            }


        }
    }
}

