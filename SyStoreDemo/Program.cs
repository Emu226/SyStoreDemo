using SimplyStore.WinForms.Models;
using SimplyStore.WinForms.Services;
using SyStoreDemo.View;

namespace SimplyStore.WinForms
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Demo-Daten einfügen (nur beim ersten Mal)
            var dataService = new DataService();
            if (dataService.GetAllArticles().Count == 0)
            {
                var demoStorage = new Storage { Name = "Werkstatt-Regal 1", IDCode = "STO:1001", Description = "Hauptregal" };
                var demoArticle = new Article
                {
                    Name = "Werkzeugkoffer",
                    IDCode = "ART:2001",
                    Description = "Mit Hammer, Schraubendreher",
                    Price = 45.00m,
                    Quantity = 1,
                    Category = "Werkzeug",
                    Storage = demoStorage,
                    LastModified = DateTime.Now
                };

                dataService.AddArticle(demoArticle);
            }

            Application.Run(new MainForm());
        }
    }
}
