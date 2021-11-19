using System;

namespace BlackRiver.Desktop
{
    public static class BlackRiverGlobal
    {
        public static bool IsAdminLogin { get; set; }
        public static string LoginToken { get; set; } = Guid.Empty.ToString();
    }
}
