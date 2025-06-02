### WOCHE 1: FOUNDATION

#### Tag 1: WinForms Setup & Grundgerüst
**Ziele:**
- WinForms-Projekt aufsetzen
- Basis-Navigation implementieren
- Erstes lauffähiges Fenster

**Tasks:**
- [X] **Visual Studio Projekt** erstellen (.NET 8 WinForms)
- [X] **Projektstruktur** nach MVP-Pattern anlegen
- [X] **NuGet Packages:**
  - Entity Framework Core + SQLite
  - //
  - System.Drawing für Icons
- [X] **MainForm** mit Hauptmenü erstellen
- [ ] **Basic Navigation** zwischen Forms
- [X] **Git Repository** initialisieren

**Deliverable:** Hauptmenü öffnet verschiedene (leere) Forms

---

#### Tag 2: Datenmodell & Database
**Ziele:**
- SQLite-Datenbank konfigurieren
- Entity Models definieren
- CRUD-Operations implementieren

**Tasks:**
- [X] **Entity Models:**
  ```csharp
  public class Article
  {
      public int Id { get; set; }
      public string IDCode { get; set; }
      public string Name { get; set; }
      public string Description { get; set; }
      public decimal Price { get; set; }
      public int Quantity { get; set; }
      public string Category { get; set; }
      public int StorageId { get; set; }
      public Storage Storage { get; set; }
      public DateTime LastModified { get; set; }
  }
  
  public class Storage
  {
      public int Id { get; set; }
      public string IDCode { get; set; }
      public string Name { get; set; }
      public string Description { get; set; }
      public List<Article> Articles { get; set; }
  }
  
  public class ArticleAction
  {
      public int Id { get; set; }
      public int ArticleId { get; set; }
      public string ActionType { get; set; } // "Move", "Split", "Merge", "Edit"
      public string Details { get; set; }
      public DateTime Timestamp { get; set; }
  }
  ```
- [X] **SimplyStoreContext** mit DbSets
- [ ] **DataService** für CRUD-Operations
- [X] **Database Initialization** mit Demo-Daten

**Deliverable:** Datenbank funktioniert, Demo-Daten können geladen werden

---

#### Tag 3: ID-System & Handschriftliche Etiketten
**Ziele:**
- Einfaches ID-System implementieren
- Handschriftliche Etiketten-Workflow unterstützen
- ID zu Artikel-Mapping

**Tasks:**
- [X] **IDService:**
  ```csharp
  public class IDService
  {
      public string GenerateArticleID(); // z.B. "IDX1222"
      public string GenerateStorageID(); // z.B. "STO5589"
      public bool ValidateID(string id);
      public (string Type, string ID) ParseID(string input);
      public string FormatIDForLabel(string id); // Für Ausdruck formatieren
  }
  ```
- [X] **LabelService für Etiketten-Management:**
  ```csharp
  public class LabelService
  {
      public string CreatePrintableLabel(string id, string itemName);
      public List<string> GetSuggestedIDs(string prefix = "IDX");
      public bool IsIDUnique(string id);
      public string GenerateNextAvailableID(string pattern);
  }
  ```
- [ ] **ID-Format definieren:**
  - Artikel: "IDX" + 4 Ziffern (z.B. "IDX1222", "IDX0045")
  - Lager: "STO" + 4 Ziffern (z.B. "STO0001", "STO0234")
  - Flexibel: Mitarbeiter kann auch eigene IDs vergeben ("WERK01", "REGAL-A")
- [ ] **ID-Input UI** statt Scan-Simulation:
  ```
  ┌─────────────────────────────────┐
  │  📝 ARTIKEL/LAGER ID EINGEBEN   │
  │                                 │
  │  ID: [________________]         │
  │                                 │
  │  💡 Vorschlag: IDX1223          │
  │      [Vorschlag übernehmen]     │
  │                                 │
  │  ✏️ Oder eigene ID eingeben:    │
  │     z.B. WERK01, REGAL-A        │
  │                                 │
  │  [🔍 Suchen] [❌ Abbruch]       │
  └─────────────────────────────────┘
  ```

**Praxis-Workflow:**
1. **Neuer Artikel:** App schlägt ID vor (z.B. "IDX1223")
2. **Mitarbeiter:** Schreibt ID mit Stift auf Etikett/Artikel
3. **Später:** Mitarbeiter tippt ID in App ein (statt scannen)
4. **Alternative:** Mitarbeiter erfindet eigene ID ("BOHRER-KLEIN")

**Deliverable:** IDs können generiert, eingegeben und Artikeln zugeordnet werden

---

### Warum dieser Ansatz besser ist:
- **🖊️ Einfach:** Jeder kann mit Stift eine ID schreiben
- **💰 Kostengünstig:** Keine QR-Code-Drucker oder Scanner nötig  
- **🔧 Flexibel:** Mitarbeiter können sinnvolle eigene IDs vergeben
- **📱 Zukunftssicher:** Später kann OCR hinzugefügt werden für automatisches "Lesen"
- **🏭 Praxisnah:** So arbeiten viele Werkstätten und kleine Lager bereits

---

#### Tag 4: Hauptfunktion - Scan Form
**Ziele:**
- Scan-Interface implementieren
- Artikel-Erkennung
- Erste Artikel-Kontext Verbindung

**Tasks:**
- [ ] **ScanForm UI:**
  ```
  ┌─────────────────────────────────┐
  │  📱 ARTIKEL SCANNEN             │
  │                                 │
  │  [📁 ID-Code auswählen]    │
  │                                 │
  │  ┌─ Scan-Ergebnis ─────────────┐ │
  │  │                             │ │
  │  │  [Wird nach Scan angezeigt] │ │
  │  │                             │ │
  │  └─────────────────────────────┘ │
  │                                 │
  │  [🔍 Artikel öffnen] [❌ Abbruch] │
  └─────────────────────────────────┘
  ```
- [ ] **ScanPresenter Logic:**
  - File-Dialog öffnen
  - ID-Code aus Dateiname/Mock extrahieren
  - Artikel in Datenbank suchen
  - Artikel-Kontext öffnen oder "Nicht gefunden"
- [ ] **Error Handling:** ID-Code nicht erkannt, Artikel nicht gefunden

**Deliverable:** Scan-Prozess funktioniert Ende-zu-Ende

---

#### Tag 5: Artikel-Kontext Form (Kernstück)
**Ziele:**
- Hauptfeature implementieren
- Artikel-Details anzeigen
- Aktions-Buttons vorbereiten

**Tasks:**
- [ ] **ArticleContextForm UI Design:**
  ```csharp
  // Hauptelement: Artikel-Info-Panel
  private Panel articleInfoPanel;
  private Label articleNameLabel;
  private Label storageLocationLabel;
  private Label quantityLabel;
  private Label priceLabel;
  
  // Aktions-Buttons
  private Button editDetailsButton;
  private Button relocateButton;
  private Button changeQuantityButton;
  private Button splitArticleButton;
  private Button mergeArticleButton;
  private Button showHistoryButton;
  ```
- [ ] **ArticleContextPresenter:**
  ```csharp
  public class ArticleContextPresenter
  {
      private Article currentArticle;
      private ArticleContextForm view;
      private IDataService dataService;
      
      public void LoadArticle(int articleId);
      public void RefreshArticleDisplay();
      public void HandleActionButton(string actionType);
  }
  ```
- [ ] **Dynamic Context Updates:** Artikel-Info aktualisiert sich nach Aktionen

**Deliverable:** Artikel-Kontext zeigt alle relevanten Infos an

---

#### Tag 6: Erste Kontext-Aktionen
**Ziele:**
- "Details bearbeiten" implementieren
- "Menge ändern" implementieren
- Aktions-Historie beginnen

**Tasks:**
- [ ] **Edit Details Dialog:**
  - Popup-Form mit Artikel-Feldern
  - Validierung und Speicherung
  - Zurück zu Artikel-Kontext mit Updates
- [ ] **Change Quantity Dialog:**
  - Eingabefeld für neue Menge
  - Plus/Minus Buttons für schnelle Änderungen
  - Bestandshistorie aktualisieren
- [ ] **Action History Service:**
  ```csharp
  public void LogAction(int articleId, string actionType, string details)
  {
      var action = new ArticleAction
      {
          ArticleId = articleId,
          ActionType = actionType,
          Details = details,
          Timestamp = DateTime.Now
      };
      // Save to database
  }
  ```

**Deliverable:** Zwei Basis-Aktionen funktionieren vollständig

---

#### Tag 7: Navigation & Polish
**Ziele:**
- Zwischen Forms navigieren
- Breadcrumb-Navigation
- UI/UX Verbesserungen

**Tasks:**
- [ ] **Navigation Service:**
  ```csharp
  public class NavigationService
  {
      public void OpenArticleContext(int articleId);
      public void OpenScanForm();
      public void ReturnToMainMenu();
      public void NavigateBack();
  }
  ```
- [ ] **Breadcrumb System:**
  - "Hauptmenü > Scan > Artikel: Werkzeugkoffer"
  - Back-Button funktionalität
- [ ] **UI-Verbesserungen:**
  - Konsistente Button-Größen
  - Icons für alle Aktionen
  - Loading-Indikatoren
- [ ] **Keyboard Shortcuts:** ESC = Zurück, Enter = Hauptaktion

**Deliverable:** Flüssige Navigation zwischen allen Forms

---

### WOCHE 2: ERWEITERTE FEATURES

#### Tag 8: Umlagern-Funktion
**Ziele:**
- Artikel zwischen Lagern verschieben
- Lager-Auswahl implementieren
- Kontext-Update nach Aktion

**Tasks:**
- [ ] **Relocate Dialog:**
  ```
  ┌─────────────────────────────────┐
  │  📍 ARTIKEL UMLAGERN            │
  │                                 │
  │  Von: Werkstatt-Regal 3         │
  │                                 │
  │  Nach: [Dropdown: Lager wählen] │
  │        [📱 Neues Lager scannen] │
  │                                 │
  │  [✅ Umlagern] [❌ Abbrechen]    │
  └─────────────────────────────────┘
  ```
- [ ] **Storage Selection Service:**
  - Alle verfügbaren Lager laden
  - Neues Lager via ID-Scan erstellen
- [ ] **Relocation Logic:**
  - Artikel.StorageId aktualisieren
  - Action loggen
  - Artikel-Kontext refreshen

**Deliverable:** Artikel können zwischen Lagern verschoben werden

---

#### Tag 9: Artikel teilen & zusammenführen
**Ziele:**
- Split-Funktionalität (1 → 2 Einheiten)
- Merge-Funktionalität (2 → 1 Einheit)
- ID-Code Management

**Tasks:**
- [ ] **Split Article Dialog:**
  ```
  ┌─────────────────────────────────┐
  │  ✂️ ARTIKEL TEILEN              │
  │                                 │
  │  Aktuell: 10 Stück              │
  │                                 │
  │  Aufteilen in:                  │
  │  Teil 1: [4] Stück              │
  │  Teil 2: [6] Stück              │
  │                                 │
  │  [✅ Teilen] [❌ Abbrechen]      │
  └─────────────────────────────────┘
  ```
- [ ] **Split Logic:**
  - Neuen Artikel erstellen mit Teil der Menge
  - Originalartikel-Menge reduzieren
  - Neue ID-Codes generieren
  - Beide Artikel gleiche Properties (außer Menge + ID)
- [ ] **Merge Logic:**
  - Anderen Artikel des gleichen Typs finden/scannen
  - Mengen addieren
  - Einen Artikel löschen, anderen aktualisieren

**Deliverable:** Artikel können geteilt und zusammengeführt werden

---

#### Tag 10: Dashboard & Übersichten
**Ziele:**
- Artikel-Übersicht Form
- Lager-Übersicht Form
- Dashboard mit Statistiken

**Tasks:**
- [ ] **ArticleListForm:**
  - DataGridView mit allen Artikeln
  - Such- und Filterfunktionen
  - Doppelklick → Artikel-Kontext öffnen
- [ ] **StorageListForm:**
  - Liste aller Lager mit Artikel-Anzahl
  - Lager-Details anzeigen
- [ ] **DashboardForm:**
  ```
  ┌─────────────────────────────────┐
  │  📊 DASHBOARD                   │
  │                                 │
  │  📦 Artikel gesamt: 23          │
  │  🏪 Lager gesamt: 5             │
  │  💰 Gesamtwert: €1,234.56       │
  │                                 │
  │  📈 Letzte Aktionen:            │
  │  • Werkzeugkoffer umgelagert    │
  │  • Schrauben M6 geteilt         │
  │  • Bohrer-Set bearbeitet        │
  │                                 │
  └─────────────────────────────────┘
  ```

**Deliverable:** Vollständige Übersicht über alle Daten

---

#### Tag 11: Demo-Daten & Scenarios
**Ziele:**
- Realistische Demo-Daten erstellen
- Demo-Workflow optimieren
- Reset-Funktionalität

**Tasks:**
- [ ] **DemoDataService erweitern:**
  ```csharp
  public class DemoDataService
  {
      public void CreateWorkshopScenario()
      {
          // Werkstatt mit typischen Artikeln
          // 3-4 Lager: Hauptlager, Werkbank, Fahrzeug
          // 15-20 Artikel: Werkzeuge, Schrauben, Materialien
      }
      
      public void CreateWarehouseScenario()
      {
          // Kleines Warenlager
          // Verschiedene Kategorien
      }
  }
  ```
- [ ] **Demo-Optimierungen:**
  - ID-Codes mit erkennbaren Namen (Einfache IDs wie "WERK01", "BOHR15", "SCHR-M6")
  - Interessante Artikel für Split/Merge-Demos
  - Verschiedene Lager für Umlagern-Demo
- [ ] **Demo-Steuerung:**
  - "Demo laden" Button im Hauptmenü
  - "Alles zurücksetzen" Funktion

**Deliverable:** Demo kann sofort gestartet werden

---

#### Tag 12: Action History & Advanced Features
**Ziele:**
- Aktions-Historie anzeigen
- Undo-Funktionalität (basic)
- Artikel-Details erweitern

**Tasks:**
- [ ] **History Viewer:**
  ```
  ┌─────────────────────────────────┐
  │  📋 ARTIKEL-HISTORIE            │
  │                                 │
  │  🕐 Heute 14:23                 │
  │      Umgelagert: Regal 2→3      │
  │                                 │
  │  🕐 Heute 11:15                 │
  │      Menge geändert: 5→3        │
  │                                 │
  │  🕐 Gestern 16:45               │
  │      Details bearbeitet         │
  │                                 │
  └─────────────────────────────────┘
  ```
- [ ] **Extended Article Properties:**
  - Kategorie-System
  - Tags/Labels
  - Notizen-Feld
- [ ] **Quick Actions im Artikel-Kontext:**
  - "Zuletzt verwendet" Lager für schnelles Umlagern
  - Häufige Mengen-Änderungen als Shortcuts

**Deliverable:** Artikel-Historie ist vollständig nachvollziehbar

---

#### Tag 13: UI/UX Polish & Testing
**Ziele:**
- Interface-Verbesserungen
- User Experience optimieren
- Vollständige Tests aller Workflows

**Tasks:**
- [ ] **UI-Verbesserungen:**
  - Konsistente Icons und Farben
  - Bessere Layouts und Spacing
  - Tastatur-Navigation optimieren
- [ ] **UX-Optimierungen:**
  - Confirmation-Dialoge für kritische Aktionen
  - Progress-Indikatoren
  - Bessere Error-Messages
- [ ] **Vollständige Workflow-Tests:**
  - Artikel scannen → Kontext → alle Aktionen → zurück zu Hauptmenü
  - Demo-Scenario komplett durchspielen
  - Edge Cases testen (ID-Code nicht gefunden, etc.)

**Deliverable:** Demo ist benutzerfreundlich und stabil

---

#### Tag 14: Final Polish & Presentation Prep
**Ziele:**
- Letzte Bug-Fixes
- Dokumentation finalisieren
- Präsentation vorbereiten

**Tasks:**
- [ ] **Final Testing:**
  - Alle Demo-Scenarios mehrfach durchlaufen
  - Performance-Test mit vielen Artikeln
  - Error-Handling überprüfen
- [ ] **Documentation:**
  - README mit Screenshots
  - Code-Kommentare vervollständigen
  - Architecture-Übersicht
- [ ] **Presentation Prep:**
  - Demo-Script (5-10 Minuten)
  - Backup-Screenshots
  - Talking Points über Architektur

**Final Deliverable:** Präsentationsreife WinForms-Demo

---

## DEMO-PRESENTATION SCRIPT

### 5-Minuten Demo-Ablauf
1. **Problem erklären (30s):** "KMUs brauchen einfache Lagerverwaltung"
2. **Lösungsansatz (30s):** "Artikel-zentrierte statt Aktions-zentrierte Bedienung"
3. **Live Demo (3min):**
   - Hauptmenü zeigen
   - Artikel "scannen" (File auswählen)
   - Artikel-Kontext öffnen - **"Das ist der Kern!"**
   - 2-3 Aktionen demonstrieren (umlagern, teilen)
   - Dashboard-Übersicht
4. **Technologie (1min):** WinForms, MVP, SQLite, Architektur
5. **Ausblick (30s):** Mobile Version, Cloud-Backend, weitere Features

### Key Messages
- **Innovation:** "Mitarbeiter denkt mit dem Artikel, nicht gegen das System"
- **Pragmatismus:** "Funktioniert auch ohne IT-Abteilung"
- **Skalierbarkeit:** "Von Demo zu Produkt ist der Weg klar"

---

## SUCCESS METRICS

### Technische Demo-Ziele
- [ ] WinForms App startet und läuft stabil
- [ ] Artikel-Kontext ist das Herzstück und funktioniert perfekt
- [ ] Mindestens 4 Kontext-Aktionen sind implementiert
- [ ] Demo-Daten machen realistic Scenario möglich
- [ ] Code ist clean und review-ready

### Bewerbungs-Ziele
- [ ] Artikel-Kontext Konzept ist einzigartig und einprägsam
- [ ] Technische Kompetenz in C# und WinForms demonstriert
- [ ] MVP-Architektur ist sauber implementiert
- [ ] Praxisbezug zur Zielbranche ist erkennbar
- [ ] 5-Minuten Demo läuft ohne Probleme

**Remember:** Der Artikel-Kontext ist dein Alleinstellungsmerkmal - das muss perfekt funktionieren!
