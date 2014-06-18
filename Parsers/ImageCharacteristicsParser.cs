using System;

using PE_Explorer.Utils;
using PE_Explorer.Core.PE.Enums;

namespace PE_Explorer.Parsers
{
    class ImageCharacteristicsParser
    {
        private uint characteritics;
        public ImageCharacteristicsParser(uint imageCharacteristics)
        {
            characteritics = imageCharacteristics;
        }

        public void Parse()
        {
            string print = "Unknown";

            if ((characteritics & (uint)EImageCharacteristics.System) > 0)
                print = "System";
            else if ((characteritics & (uint)EImageCharacteristics.DLL) > 0)
                print = "DLL";
            else if ((characteritics & (uint)EImageCharacteristics.Executable) > 0)
                print = "Executable";

            Logger.Log(ELogTypes.INFO, string.Format("{0} file detected", print));

        }
    }
}
