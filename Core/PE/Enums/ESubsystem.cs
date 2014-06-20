namespace Core.PE.Enums
{
    public enum ESubsystem : ushort
    {
        Unknown = 0,
        Native = 1,
        Windows_GUI = 2,
        Windows_CUI = 3,
        OS2_CUI = 5,
        POSIX_CUI = 7,
        WindowsCE_GUI = 9,
        EFI_Application = 10, // Extensible Firmware Interface
        EFI_BootServiceDriver = 11,
        EFI_RuntimeDriver = 12,
        EFI_ROM = 13,
        XBOX = 14, // lol
        Windows_BootApplication = 16
    }
}