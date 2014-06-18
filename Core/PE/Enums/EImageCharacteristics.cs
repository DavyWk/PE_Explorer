namespace PE_Explorer.Core.PE.Enums
{
    // The comments are from winnt.h, Copyright (c) Microsoft Corporation
   public enum EImageCharacteristics : ushort
   {
       RelocStripped = 0x0001, // Relocation info stripped from file.
       Executable = 0x0002, // File is executable  (i.e. no unresolved external references).
       LineNumbersStripped = 0x0004, // Line nunbers stripped from file.
       SymbolTableStripped = 0x0008, // Local symbols stripped from file.
       LargeAddress = 0x0020, // App can handle >2gb addresses
       Machine32Bit = 0x1000, // 32 bit word machine.
       DebugStripped = 0x200, // Debugging info stripped from file in .DBG file
       RemovableRunFromSwap = 0x0400, // If Image is on removable media, copy and run from the swap file.
       NetRunFromSwap = 0x0800, // If Image is on Net, copy and run from the swap file.
       System = 0x1000, // System File.
       DLL = 0x2000, // File is a DLL.
       UPSystemOnly = 0x4000, // File should only be run on a UP machine
       BytesReversed = 0x8000, // Bytes of machine word are reversed
   }
}
