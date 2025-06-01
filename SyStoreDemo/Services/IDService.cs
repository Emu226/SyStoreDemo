using System;
using System.Text.RegularExpressions;

namespace SimplyStore.WinForms.Services
{
    public class IDService
    {
        private static readonly Random _random = new Random();

        /// <summary>
        /// Generiert eine neue Artikel-ID im Format "IDX1234"
        /// </summary>
        public string GenerateArticleID()
        {
            // Generiere 4-stellige Zufallszahl zwischen 0001 und 9999
            int number = _random.Next(1, 10000);
            return $"IDX{number:D4}";
        }

        /// <summary>
        /// Generiert eine neue Lager-ID im Format "STO1234"
        /// </summary>
        public string GenerateStorageID()
        {
            // Generiere 4-stellige Zufallszahl zwischen 0001 und 9999
            int number = _random.Next(1, 10000);
            return $"STO{number:D4}";
        }

        /// <summary>
        /// Prüft ob eine ID gültig ist (Format oder benutzerdefiniert)
        /// </summary>
        public bool ValidateID(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return false;

            // Mindestlänge 3 Zeichen
            if (id.Length < 3)
                return false;

            // Nur Buchstaben, Zahlen und Bindestriche erlaubt
            return Regex.IsMatch(id, @"^[A-Za-z0-9\-]+$");
        }

        /// <summary>
        /// Analysiert eine ID und gibt Typ und bereinigten Wert zurück
        /// </summary>
        public (string Type, string ID) ParseID(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return ("Unknown", "");

            string cleanedID = input.Trim().ToUpper();

            // Erkenne Artikel-IDs
            if (cleanedID.StartsWith("IDX"))
                return ("Article", cleanedID);

            // Erkenne Lager-IDs
            if (cleanedID.StartsWith("STO"))
                return ("Storage", cleanedID);

            // Benutzerdefinierte IDs - versuche zu erraten basierend auf Inhalt
            if (cleanedID.Contains("WERK") || cleanedID.Contains("TOOL") ||
                cleanedID.Contains("BOHR") || cleanedID.Contains("SCHR"))
                return ("Article", cleanedID);

            if (cleanedID.Contains("LAGER") || cleanedID.Contains("REGAL") ||
                cleanedID.Contains("SHELF"))
                return ("Storage", cleanedID);

            // Wenn unklar, nehme Artikel an (häufigster Fall)
            return ("Article", cleanedID);
        }

        /// <summary>
        /// Formatiert eine ID für schöne Anzeige auf Etiketten
        /// </summary>
        public string FormatIDForLabel(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return "";

            string cleanedID = id.Trim().ToUpper();

            // Bei IDX/STO Format: Trenne mit Bindestrich für bessere Lesbarkeit
            if (Regex.IsMatch(cleanedID, @"^(IDX|STO)\d{4}$"))
            {
                return $"{cleanedID.Substring(0, 3)}-{cleanedID.Substring(3)}";
            }

            return cleanedID;
        }

        /// <summary>
        /// Generiert eine eindeutige ID, die noch nicht existiert
        /// </summary>
        public string GenerateUniqueArticleID(Func<string, bool> checkExists)
        {
            string newID;
            int attempts = 0;

            do
            {
                newID = GenerateArticleID();
                attempts++;

                // Sicherheitsventil: Nach 100 Versuchen längere ID verwenden
                if (attempts > 100)
                {
                    newID = $"IDX{_random.Next(10000, 99999)}";
                }
            }
            while (checkExists(newID) && attempts < 200);

            return newID;
        }

        /// <summary>
        /// Generiert eine eindeutige Lager-ID, die noch nicht existiert
        /// </summary>
        public string GenerateUniqueStorageID(Func<string, bool> checkExists)
        {
            string newID;
            int attempts = 0;

            do
            {
                newID = GenerateStorageID();
                attempts++;

                // Sicherheitsventil: Nach 100 Versuchen längere ID verwenden
                if (attempts > 100)
                {
                    newID = $"STO{_random.Next(10000, 99999)}";
                }
            }
            while (checkExists(newID) && attempts < 200);

            return newID;
        }
    }
}