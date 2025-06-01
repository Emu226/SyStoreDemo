## DEMO-KONZEPT

### Kern-Innovation: "Artikel-Kontext" Workflow
**Grundidee:** Mitarbeiter "loggt sich" in einen Artikel/eine Palette ein und führt alle Aktionen aus diesem Kontext heraus aus.

```
Traditioneller Workflow:
Artikel auswählen → Aktion wählen → Parameter eingeben

Simply Store Workflow:
Artikel scannen → "Einloggen" → Alle Aktionen verfügbar
```

### Zentrale UI-Philosophie
**"Context-First" statt "Action-First"**
- Mitarbeiter denkt: "Ich arbeite MIT diesem Artikel"
- Nicht: "Ich führe eine Aktion auf einem Artikel aus"

---

## TECHNISCHE ARCHITEKTUR (WinForms)

### Framework & Tools
- **Platform:** .NET 8 Windows Forms
- **Database:** SQLite + Entity Framework Core
- **Architecture:** MVP Pattern (Model-View-Presenter)
- **Additional:** QR-Code Generation, File I/O für "Scanning"

### Projekt-Struktur
```
SimplyStore.WinForms/
├── Models/
│   ├── Article.cs
│   ├── Storage.cs
│   └── ArticleAction.cs
├── Views/
│   ├── MainForm.cs
│   ├── ScanForm.cs
│   ├── ArticleContextForm.cs
│   └── DashboardForm.cs
├── Presenters/
│   ├── MainPresenter.cs
│   ├── ScanPresenter.cs
│   └── ArticleContextPresenter.cs
├── Services/
│   ├── DataService.cs
│   ├── QRCodeService.cs
│   └── FileService.cs
└── Database/
    └── SimplyStoreContext.cs
```

---

## KERN-WORKFLOW: ARTIKEL-KONTEXT

### 1. Scan-Prozess
```
[Hauptmenü] 
     ↓
[QR-Code Scannen] (File Upload Simulation)
     ↓
[Artikel/ Lager gefunden?] 
     ├─ JA → [Artikel/Lager-Kontext öffnen]
     └─ NEIN → [Neuen Artikel/ Lager
## WINFORMS ROADMAP (2 WOCHEN) erstellen]
```

### 2. Artikel-Kontext Form
**Das Herzstück der Anwendung:**

```
╔══════════════════════════════════════════╗
║  📦 ARTIKEL-KONTEXT                      ║
║  ──────────────────────────────────────  ║
║  🏷️  Werkzeugkoffer #A001               ║
║  📍  Lager: Werkstatt-Regal 3           ║
║  📊  Menge: 1 Stück                     ║
║  💰  Wert: €45.00                       ║
║                                          ║
║  ┌─ AKTIONEN ─────────────────────────┐  ║
║  │                                   │  ║
║  │  [📦 Details bearbeiten]          │  ║
║  │  [📍 Umlagern]                    │  ║
║  │  [➕ Menge ändern]                │  ║
║  │  [✂️  Einheit teilen]             │  ║
║  │  [🔄 Artikel zusammenführen]       │  ║
║  │  [📋 Historie anzeigen]           │  ║
║  │  [🖨️  Neues Label drucken]        │  ║
║  │                                   │  ║
║  └───────────────────────────────────┘  ║
║                                          ║
║  [❌ Kontext verlassen]  [🏠 Hauptmenü] ║
╚══════════════════════════════════════════╝
```

### 3. Kontext-basierte Aktionen

#### 3.1 Artikel Details bearbeiten
- **Kontext:** Aktueller Artikel ist "aktiv"
- **Aktion:** Eigenschaften ändern (Name, Beschreibung, Preis, etc.)
- **Besonderheit:** Änderungen werden sofort im Kontext-Header sichtbar

#### 3.2 Umlagern
```
[Artikel-Kontext: Werkzeugkoffer]
         ↓
[Neues Lager scannen/auswählen]
         ↓
[Umlagerung bestätigen]
         ↓
[Kontext-Header aktualisiert: "Neuer Standort"]
```

#### 3.3 Einheit teilen
```
[Artikel-Kontext: 10x Schrauben M6]
         ↓
[Teilung definieren: 4 + 6]
         ↓
[Neuen QR-Code für geteilte Einheit generieren]
         ↓
[Auswahl: In welcher Einheit weitermachen?]
```

#### 3.4 Einheiten zusammenführen
```
[Artikel-Kontext: 5x Schrauben M6]
         ↓
[Anderen Artikel gleichen Typs scannen]
         ↓
[Zusammenführung: 5 + 3 = 8 Stück]
         ↓
[Einen QR-Code wird ungültig]
```

---

## BENUTZERFÜHRUNG: "MOBILE-FIRST" MENTALITÄT

### Design-Prinzipien für Desktop
**Auch wenn WinForms Desktop ist, denken wir "Mobile":**
- **Große Buttons:** Auch mit Maus gut klickbar
- **Klare Hierarchie:** Eine Hauptaktion pro Screen
- **Wenig Text:** Symbole + kurze Labels
- **Schnelle Navigation:** Shortcuts und Breadcrumbs

### Hauptmenü-Struktur
```
╔═══════════════════════════════════════╗
║     📱 SIMPLY STORE DEMO              ║
║                                       ║
║  ┌───────────────────────────────────┐ ║
║  │         [📱 ARTIKEL SCANNEN]      │ ║
║  │                                   │ ║
║  │        Der Haupteinstieg          │ ║
║  └───────────────────────────────────┘ ║
║                                       ║
║  [📋 Alle Artikel]  [🏪 Lager]       ║
║                                       ║
║  [📊 Dashboard]     [⚙️ Einstellungen] ║
║                                       ║
║  [🎮 Demo laden]    [🔄 Reset]        ║
╚═══════════════════════════════════════╝
```

---

## ERWEITERTE FEATURES

### 1. Intelligente Artikel-Erkennung
**Problem:** Was passiert wenn QR-Code nicht lesbar?
**Lösung:** Fallback-Strategien
```
QR-Code Scan fehlgeschlagen
         ↓
[Manuelle Eingabe von Artikel-ID]
         ↓
[Suche nach ähnlichen Artikeln]
         ↓
[Neuen Artikel erstellen]
```

### 2. Kontext-Persistenz
**Feature:** Letzter Artikel-Kontext wird gespeichert
```
App-Start → "Zuletzt bearbeitet: Werkzeugkoffer #A001"
          → [Kontext fortsetzen] oder [Neu scannen]
```

### 3. Batch-Operationen im Kontext
**Scenario:** Mitarbeiter arbeitet an einer Palette mit vielen gleichen Artikeln
```
[Palette-Kontext: 20x Schrauben M8]
         ↓
[Batch-Modus aktivieren]
         ↓
[Aktion wählen: "5 Stück entnehmen"]
         ↓
[Aktion auf alle/ausgewählte anwenden]
```

### 4. Schnelle Lager-Navigation
**Im Artikel-Kontext:**
```
"Werkzeugkoffer ist in: Werkstatt-Regal 3"
         ↓
[📍 Andere Artikel in diesem Lager]
         ↓
[Lager-Übersicht mit allen Artikeln]
         ↓
[Direkter Artikel-Wechsel ohne Hauptmenü]
```

---
