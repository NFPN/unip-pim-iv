using System;

namespace BlackRiver.Desktop
{
    public static class BlackRiverGlobal
    {
        public static bool IsAdminLogin { get; set; }

        public static string FirstUseFolder = @$"{ Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) }\BlackRiver";
        public static string FirstUseFile = @$"{FirstUseFolder}\first.bin";
    }
}
