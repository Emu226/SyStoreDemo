using SimplyStore.WinForms.Database;
using SimplyStore.WinForms.Models;
using System.Collections.Generic;
using System.Linq;

namespace SimplyStore.WinForms.Services
{
    public class DataService
    {
        private readonly SimplyStoreContext _context;

        public DataService()
        {
            _context = new SimplyStoreContext();
            _context.Database.EnsureCreated(); // Erstellt DB falls nicht vorhanden
        }

        public List<Article> GetAllArticles()
        {
            return _context.Articles.ToList();
        }

        public void AddArticle(Article article)
        {
            // Wenn ein verknüpfter Speicherort mitgegeben wurde, diesen zuerst speichern
            if (article.Storage != null && article.Storage.Id == 0)
            {
                _context.Storages.Add(article.Storage);
            }

            _context.Articles.Add(article);
            _context.SaveChanges();
        }

        public Article? GetArticleById(int id)
        {
            return _context.Articles.FirstOrDefault(a => a.Id == id);
        }

        // Später: Update, Delete usw.
    }
}
