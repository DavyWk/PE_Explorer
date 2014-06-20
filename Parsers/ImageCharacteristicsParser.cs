using System;

using Utils;
using Core.Utilities;
using Core.PE.Enums;


namespace Parsers
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
