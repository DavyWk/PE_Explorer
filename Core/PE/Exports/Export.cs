namespace Core.PE.Exports
{
    public struct Export 
    { // This structure is NOT from Microsoft.
        public ushort Ordinal;
        public byte[] Name;
        public uint Address;

        // no constructor because we can't initialize it with a single BinaryReader
        // will be initialized explicitly
    }
    
}
