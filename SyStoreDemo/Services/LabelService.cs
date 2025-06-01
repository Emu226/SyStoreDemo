using System;
using System.Collections.Generic;
using System.Linq;

namespace SimplyStore.WinForms.Services
{
    public class LabelService
    {
        private readonly IDService _idService;
        private readonly DataService _dataService;

        public LabelService(IDService idService, DataService dataService)
        {
            _idService = idService;
            _dataService = dataService;
        }

        /// <summary>
        /// Erstellt einen druckfähigen Etikettentext mit ID und Name
        /// </summary>
        public string CreatePrintableLabel(string id, string itemName)
        {
            string formattedID = _idService.FormatIDForLabel(id);

            // Einfaches Text-Format für Etiketten
            return $"┌─────────────────┐\n" +
                   $"│ {formattedID,-15} │\n" +
                   $"│                 │\n" +
                   $"│ {TruncateText(itemName, 15),-15} │\n" +
                   $"└─────────────────┘";
        }

        /// <summary>
        /// Schlägt ähnliche IDs vor basierend auf einem Präfix
        /// </summary>
        public List<string> GetSuggestedIDs(string prefix = "IDX")
        {
            var suggestions = new List<string>();

            for (int i = 1; i <= 5; i++)
            {
                string suggestedID = prefix switch
                {
                    "IDX" => _idService.GenerateUniqueArticleID(CheckIDExists),
                    "STO" => _idService.GenerateUniqueStorageID(CheckIDExists),
                    _ => $"{prefix}{new Random().Next(1, 1000):D3}"
                };

                if (!CheckIDExists(suggestedID))
                    suggestions.Add(suggestedID);
            }

            return suggestions;
        }

        /// <summary>
        /// Prüft ob eine ID bereits verwendet wird
        /// </summary>
        public bool IsIDUnique(string id)
        {
            return !CheckIDExists(id);
        }

        /// <summary>
        /// Generiert die nächste verfügbare ID basierend auf einem Muster
        /// </summary>
        public string GenerateNextAvailableID(string pattern)
        {
            if (string.IsNullOrWhiteSpace(pattern))
                return _idService.GenerateUniqueArticleID(CheckIDExists);

            // Versuche verschiedene Varianten des Musters
            var attempts = new List<string>();

            // Originalmuster
            attempts.Add(pattern.ToUpper());

            // Mit Nummern
            for (int i = 1; i <= 99; i++)
            {
                attempts.Add($"{pattern.ToUpper()}{i:D2}");
                attempts.Add($"{pattern.ToUpper()}-{i:D2}");
            }

            // Finde die erste freie ID
            foreach (string attempt in attempts)
            {
                if (_idService.ValidateID(attempt) && !CheckIDExists(attempt))
                    return attempt;
            }

            // Fallback: Standard-ID generieren
            return _idService.GenerateUniqueArticleID(CheckIDExists);
        }

        /// <summary>
        /// Gibt Vorschläge für benutzerfreundliche IDs basierend auf dem Artikelnamen
        /// </summary>
        public List<string> GetSmartIDSuggestions(string itemName)
        {
            var suggestions = new List<string>();

            if (string.IsNullOrWhiteSpace(itemName))
                return GetSuggestedIDs();

            // Erstelle sinnvolle Abkürzungen vom Namen
            string cleanName = itemName.ToUpper()
                .Replace("Ä", "AE").Replace("Ö", "OE").Replace("Ü", "UE")
                .Replace("ß", "SS");

            var words = cleanName.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (words.Length >= 2)
            {
                // Erste Buchstaben der Wörter
                string initials = string.Join("", words.Select(w => w.First()));
                if (initials.Length >= 2)
                    suggestions.Add(GenerateNextAvailableID(initials));
            }

            if (words.Length >= 1)
            {
                // Ersten 4-6 Buchstaben des ersten Wortes
                string firstWord = words[0];
                if (firstWord.Length >= 4)
                {
                    suggestions.Add(GenerateNextAvailableID(firstWord.Substring(0, Math.Min(6, firstWord.Length))));
                }
            }

            // Spezielle Begriffe erkennen
            if (itemName.ToLower().Contains("werkzeug"))
                suggestions.Add(GenerateNextAvailableID("WERK"));
            if (itemName.ToLower().Contains("bohrer"))
                suggestions.Add(GenerateNextAvailableID("BOHR"));
            if (itemName.ToLower().Contains("schraube"))
                suggestions.Add(GenerateNextAvailableID("SCHR"));

            // Standard-Vorschläge hinzufügen
            suggestions.AddRange(GetSuggestedIDs().Take(2));

            return suggestions.Distinct().Take(5).ToList();
        }

        /// <summary>
        /// Hilfsmethode: Prüft ob ID bereits existiert
        /// </summary>
        private bool CheckIDExists(string id)
        {
            // Prüfe sowohl Artikel als auch Lager
            var articles = _dataService.GetAllArticles();
            var storages = _dataService.GetAllStorages();

            return articles.Any(a => string.Equals(a.IDCode, id, StringComparison.OrdinalIgnoreCase)) ||
                   storages.Any(s => string.Equals(s.IDCode, id, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Hilfsmethode: Kürzt Text für Etiketten
        /// </summary>
        private string TruncateText(string text, int maxLength)
        {
            if (string.IsNullOrEmpty(text))
                return "";

            if (text.Length <= maxLength)
                return text;

            return text.Substring(0, maxLength - 3) + "...";
        }
    }
}