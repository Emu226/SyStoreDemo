using System.Runtime.Versioning;
using System.Windows.Forms;

namespace SimplyStore.WinForms
{
    internal static class ApplicationConfiguration
    {
        [SupportedOSPlatform("windows")]
        public static void Initialize()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
        }
    }
}
