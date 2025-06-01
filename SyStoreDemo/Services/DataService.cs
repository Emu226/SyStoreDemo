using SimplyStore.WinForms.Database;
using SimplyStore.WinForms.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;

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

        // ═══ ARTIKEL FUNKTIONEN ═══

        public List<Article> GetAllArticles()
        {
            return _context.Articles.Include(a => a.Storage).ToList();
        }

        public void AddArticle(Article article)
        {
            // Wenn ein verknüpfter Speicherort mitgegeben wurde, diesen zuerst speichern
            if (article.Storage != null && article.Storage.Id == 0)
            {
                _context.Storages.Add(article.Storage);
            }

            article.LastModified = DateTime.Now;
            _context.Articles.Add(article);
            _context.SaveChanges();
        }

        public Article? GetArticleById(int id)
        {
            return _context.Articles.Include(a => a.Storage).FirstOrDefault(a => a.Id == id);
        }

        /// <summary>
        /// Sucht Artikel anhand der ID-Code (das Herzstück für handschriftliche IDs!)
        /// </summary>
        public Article? GetArticleByIDCode(string idCode)
        {
            if (string.IsNullOrWhiteSpace(idCode))
                return null;

            return _context.Articles
                .Include(a => a.Storage)
                .FirstOrDefault(a => string.Equals(a.IDCode, idCode.Trim(), StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Sucht Artikel mit ähnlichen ID-Codes (für Tippfehler-Toleranz)
        /// </summary>
        public List<Article> FindSimilarArticles(string idCode)
        {
            if (string.IsNullOrWhiteSpace(idCode))
                return new List<Article>();

            string searchTerm = idCode.Trim().ToLower();

            return _context.Articles
                .Include(a => a.Storage)
                .Where(a => a.IDCode.ToLower().Contains(searchTerm) ||
                           a.Name.ToLower().Contains(searchTerm))
                .Take(5)
                .ToList();
        }

        public void UpdateArticle(Article article)
        {
            article.LastModified = DateTime.Now;
            _context.Articles.Update(article);
            _context.SaveChanges();
        }

        public void DeleteArticle(int id)
        {
            var article = _context.Articles.Find(id);
            if (article != null)
            {
                _context.Articles.Remove(article);
                _context.SaveChanges();
            }
        }

        // ═══ LAGER FUNKTIONEN ═══

        public List<Storage> GetAllStorages()
        {
            return _context.Storages.Include(s => s.Articles).ToList();
        }

        public void AddStorage(Storage storage)
        {
            _context.Storages.Add(storage);
            _context.SaveChanges();
        }

        public Storage? GetStorageById(int id)
        {
            return _context.Storages.Include(s => s.Articles).FirstOrDefault(s => s.Id == id);
        }

        /// <summary>
        /// Sucht Lager anhand der ID-Code
        /// </summary>
        public Storage? GetStorageByIDCode(string idCode)
        {
            if (string.IsNullOrWhiteSpace(idCode))
                return null;

            return _context.Storages
                .Include(s => s.Articles)
                .FirstOrDefault(s => string.Equals(s.IDCode, idCode.Trim(), StringComparison.OrdinalIgnoreCase));
        }

        public void UpdateStorage(Storage storage)
        {
            _context.Storages.Update(storage);
            _context.SaveChanges();
        }

        // ═══ ID-MANAGEMENT FUNKTIONEN ═══

        /// <summary>
        /// Prüft ob eine ID bereits verwendet wird (Artikel oder Lager)
        /// </summary>
        public bool IsIDCodeTaken(string idCode)
        {
            if (string.IsNullOrWhiteSpace(idCode))
                return false;

            return _context.Articles.Any(a => string.Equals(a.IDCode, idCode.Trim(), StringComparison.OrdinalIgnoreCase)) ||
                   _context.Storages.Any(s => string.Equals(s.IDCode, idCode.Trim(), StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Universelle Suche nach ID-Code (findet Artikel oder Lager)
        /// </summary>
        public (string Type, object? Item) FindByIDCode(string idCode)
        {
            if (string.IsNullOrWhiteSpace(idCode))
                return ("None", null);

            // Zuerst nach Artikel suchen
            var article = GetArticleByIDCode(idCode);
            if (article != null)
                return ("Article", article);

            // Dann nach Lager suchen
            var storage = GetStorageByIDCode(idCode);
            if (storage != null)
                return ("Storage", storage);

            return ("None", null);
        }

        // ═══ AKTIONS-HISTORIE ═══

        public void LogAction(int articleId, string actionType, string details)
        {
            var action = new ArticleAction
            {
                ArticleId = articleId,
                ActionType = actionType,
                Details = details,
                Timestamp = DateTime.Now
            };

            _context.ArticleActions.Add(action);
            _context.SaveChanges();
        }

        public List<ArticleAction> GetArticleHistory(int articleId)
        {
            return _context.ArticleActions
                .Where(a => a.ArticleId == articleId)
                .OrderByDescending(a => a.Timestamp)
                .Take(20)
                .ToList();
        }

        // ═══ DEMO-DATEN FUNKTIONEN ═══

        public void CreateDemoData()
        {
            // Lösche existierende Demo-Daten
            _context.ArticleActions.RemoveRange(_context.ArticleActions);
            _context.Articles.RemoveRange(_context.Articles);
            _context.Storages.RemoveRange(_context.Storages);
            _context.SaveChanges();

            // Erstelle Demo-Lager
            var storages = new List<Storage>
            {
                new Storage { IDCode = "STO0001", Name = "Hauptlager", Description = "Zentrales Warenlager" },
                new Storage { IDCode = "WERK-REGAL", Name = "Werkstatt-Regal", Description = "Regal in der Werkstatt" },
                new Storage { IDCode = "FAHRZEUG", Name = "Service-Fahrzeug", Description = "Mobile Werkzeugkiste" }
            };

            _context.Storages.AddRange(storages);
            _context.SaveChanges();

            // Erstelle Demo-Artikel mit schönen handschriftlichen IDs
            var articles = new List<Article>
            {
                new Article { IDCode = "WERK01", Name = "Werkzeugkoffer", Description = "Kompletter Werkzeugkoffer", Price = 45.00m, Quantity = 1, Category = "Werkzeug", StorageId = storages[1].Id },
                new Article { IDCode = "BOHR-15", Name = "Bohrmaschine 15V", Description = "Akkubohrmaschine 15 Volt", Price = 89.99m, Quantity = 2, Category = "Werkzeug", StorageId = storages[0].Id },
                new Article { IDCode = "SCHR-M6", Name = "Schrauben M6", Description = "Edelstahl-Schrauben M6x30", Price = 0.15m, Quantity = 50, Category = "Befestigung", StorageId = storages[0].Id },
                new Article { IDCode = "IDX0042", Name = "Hammer", Description = "Schlosser-Hammer 500g", Price = 25.50m, Quantity = 3, Category = "Werkzeug", StorageId = storages[1].Id },
                new Article { IDCode = "KABEL-RD", Name = "Kabel rot", Description = "Stromkabel rot 2.5mm²", Price = 2.30m, Quantity = 25, Category = "Elektro", StorageId = storages[2].Id }
            };

            foreach (var article in articles)
            {
                article.LastModified = DateTime.Now.AddDays(-new Random().Next(1, 30));
            }

            _context.Articles.AddRange(articles);
            _context.SaveChanges();

            // Erstelle Demo-Aktionen
            var actions = new List<ArticleAction>
            {
                new ArticleAction { ArticleId = articles[0].Id, ActionType = "Create", Details = "Artikel erstellt", Timestamp = DateTime.Now.AddDays(-5) },
                new ArticleAction { ArticleId = articles[1].Id, ActionType = "Move", Details = "Von Lager zu Werkstatt", Timestamp = DateTime.Now.AddDays(-2) },
                new ArticleAction { ArticleId = articles[2].Id, ActionType = "QuantityChange", Details = "Menge von 30 auf 50 erhöht", Timestamp = DateTime.Now.AddDays(-1) }
            };

            _context.ArticleActions.AddRange(actions);
            _context.SaveChanges();
        }

        public void ClearAllData()
        {
            _context.ArticleActions.RemoveRange(_context.ArticleActions);
            _context.Articles.RemoveRange(_context.Articles);
            _context.Storages.RemoveRange(_context.Storages);
            _context.SaveChanges();
        }
    }
}