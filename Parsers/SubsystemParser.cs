using PE_Explorer.Utils;
using PE_Explorer.Core.PE.Headers.Enums;

namespace PE_Explorer.Parsers
{
    public class SubsystemParser
    {
        private ESubsystem subsys;
        
        public SubsystemParser(ushort subsystem)
        {
            subsys = (ESubsystem)subsystem;
        }

        public void Parse()
        {
            string output = "Unknown";

            switch(subsys)
            {
                case ESubsystem.Unknown:
                    break;
                case ESubsystem.Native:
                    output = "Native";
                    break;
                case ESubsystem.Windows_GUI:
                    output = "Windows GUI";
                    break;
                case ESubsystem.Windows_CUI:
                    output = "Windows CUI";
                    break;
                case ESubsystem.OS2_CUI:
                    output = "OS/2 CUI";
                    break;
                case ESubsystem.POSIX_CUI:
                    output = "POSIX CUI";
                    break;
                case ESubsystem.WindowsCE_GUI:
                    output = "Windows CE GUI";
                    break;
                case ESubsystem.EFI_Application:
                    output = "EFI Application";
                    break;
                case ESubsystem.EFI_BootServiceDriver:
                    output = "EFI Boot servie driver";
                    break;
                case ESubsystem.EFI_RuntimeDriver:
                    output = "EFI Runtime driver";
                    break;
                case ESubsystem.EFI_ROM:
                    output = "EFI ROM";
                    break;
                case ESubsystem.XBOX:
                    output = "XBOX";
                    break;
                case ESubsystem.Windows_BootApplication:
                    output = "Windows boot application";
                    break;
                default:
                    break;
            }
            Logger.Log(ELogTypes.INFO, string.Format("{0} subsystem", output));
        }
    }
}
