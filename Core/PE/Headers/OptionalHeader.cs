using System.Runtime.InteropServices;
using System.IO;

namespace Core.PE.Headers
{
    
    [StructLayout(LayoutKind.Sequential,Pack = 1)]
    public struct OptionalHeader
    {
        // Standard fields
        public ushort Magic;
        public byte MajorLinkerVersion;
        public byte MinorLinkerVersion;
        public uint SizeOfCode;
        public uint SizeOfInitializatedData;
        public uint SizeOfUninitializedData;
        public uint AddressOfEntryPoint;
        public uint BaseOfCode;
        public uint BaseOfData;

        // NT additional fields
        public uint ImageBase;
        public uint SectionAlignment;
        public uint FileAlignment;
        public ushort MajorOperatingSystemVersion;
        public ushort MinorOperatingSystemVersion;
        public ushort MajorImageVersion;
        public ushort MinorImageVersion;
        public ushort MajorSubsystemVersion;
        public ushort MinorSubsystemVersion;
        public uint Win32VersionValue;
        public uint SizeOfImage;
        public uint SizeOfHeaders;
        public uint CheckSum;
        public ushort Subsystem;
        public ushort DllCharacteristics;
        public uint SizeOfStackReserve;
        public uint SizeOfStackCommit;
        public uint SizeOfHeapReserve;
        public uint SizeOfHeapCommit;
        public uint LoaderFlags;
        public uint NumberOfRvaAndSizes;
        
        // Data directory array [16]
        public DataDirectory ExportDirectory;
        public DataDirectory ImportDirectory;
        public DataDirectory RessourceDirectory;
        public DataDirectory ExceptionDirectory;
        public DataDirectory SecurityDirectory;
        public DataDirectory BaseRelocationTable;
        public DataDirectory DebugData;
        public DataDirectory ArchitectureData;
        public DataDirectory GlobalPtr; // size must be 0
        public DataDirectory ThreadLocalStorageDirectory;
        public DataDirectory ConfigurationDirectory;
        public DataDirectory BoundImportDirectory;
        public DataDirectory ImportAddressTable;
        public DataDirectory DelayImport;
        public DataDirectory DOTNETMetadata;
        public DataDirectory Reserved;

        public OptionalHeader(BinaryReader br)
        {
            Magic = br.ReadUInt16();
            MajorLinkerVersion = br.ReadByte();
            MinorLinkerVersion = br.ReadByte();
            SizeOfCode = br.ReadUInt32();
            SizeOfInitializatedData = br.ReadUInt32();
            SizeOfUninitializedData = br.ReadUInt32();
            AddressOfEntryPoint = br.ReadUInt32();
            BaseOfCode = br.ReadUInt32();
            BaseOfData = br.ReadUInt32();

            ImageBase = br.ReadUInt32();
            SectionAlignment = br.ReadUInt32();
            FileAlignment = br.ReadUInt32();
            MajorOperatingSystemVersion = br.ReadUInt16();
            MinorOperatingSystemVersion = br.ReadUInt16();
            MajorImageVersion = br.ReadUInt16();
            MinorImageVersion = br.ReadUInt16();
            MajorSubsystemVersion = br.ReadUInt16();
            MinorSubsystemVersion = br.ReadUInt16();
            Win32VersionValue = br.ReadUInt32();
            SizeOfImage = br.ReadUInt32();
            SizeOfHeaders = br.ReadUInt32();
            CheckSum = br.ReadUInt32();
            Subsystem = br.ReadUInt16();
            DllCharacteristics = br.ReadUInt16();
            SizeOfStackReserve = br.ReadUInt32();
            SizeOfStackCommit = br.ReadUInt32();
            SizeOfHeapReserve = br.ReadUInt32();
            SizeOfHeapCommit = br.ReadUInt32();
            LoaderFlags = br.ReadUInt32();
            NumberOfRvaAndSizes = br.ReadUInt32();

            // 16 data directories
            ExportDirectory = new DataDirectory(br);
            ImportDirectory = new DataDirectory(br);
            RessourceDirectory = new DataDirectory(br);
            ExceptionDirectory = new DataDirectory(br);
            SecurityDirectory = new DataDirectory(br);
            BaseRelocationTable = new DataDirectory(br);
            DebugData = new DataDirectory(br);
            ArchitectureData = new DataDirectory(br);
            GlobalPtr = new DataDirectory(br);
            ThreadLocalStorageDirectory = new DataDirectory(br);
            ConfigurationDirectory = new DataDirectory(br);
            BoundImportDirectory = new DataDirectory(br);
            ImportAddressTable = new DataDirectory(br);
            DelayImport = new DataDirectory(br);
            DOTNETMetadata = new DataDirectory(br);
            Reserved = new DataDirectory(br);
            
        }
    }

    [StructLayout(LayoutKind.Sequential,Pack = 1)]
    public struct DataDirectory
    {
        public uint VirtualAddress;
        public uint Size;

        public DataDirectory(BinaryReader br)
        {
            VirtualAddress = br.ReadUInt32();
            Size = br.ReadUInt32();
        }
    }
}
