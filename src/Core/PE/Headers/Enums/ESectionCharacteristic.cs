namespace PE_Explorer.Core.PE.Headers.Enums
{
    public enum ESectionCharacteristics : uint
    {
        // Contains
        Code = 0x20,
        InitializedData = 0x00000040,
        UninitializedData = 0x00000080,

        // Object files ONLY
        Info = 0x00000200,    // contains comments and other information
        Remove = 0x00000800,  // will not be a part of the image
        ComDat = 0x00001000, // contains COMDAT data

        // Memory
        Discardable = 0x02000000,
        NotCached = 0x04000000,
        NotPaged = 0x08000000,
        Shared = 0x10000000,
        Execute = 0x20000000,
        Read = 0x40000000,
        Write = 0x80000000,

    }
}
