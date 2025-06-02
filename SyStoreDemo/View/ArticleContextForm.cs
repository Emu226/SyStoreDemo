using System;
using System.Drawing;
using System.Windows.Forms;
using SimplyStore.WinForms.Models;
using SimplyStore.WinForms.Services;
using SyStoreDemo.View.Forms;
using System.ComponentModel;

namespace SyStoreDemo.View
{
    public partial class ArticleContextForm : Form
    {
        private readonly Article _article;
        private readonly DataService _dataService;

        public ArticleContextForm(Article article)
        {
            _article = article ?? throw new ArgumentNullException(nameof(article));
            _dataService = new DataService();
            InitializeComponent();
            //SetupForm();
            LoadArticleData();
        }

        //private void SetupForm()
        //{
        //    // Header Bereich
        //    var pnlHeader = new Panel
        //    {
        //        Location = new Point(0, 0),
        //        Size = new Size(600, 120),
        //        BackColor = Color.FromArgb(0, 123, 255),
        //        Dock = DockStyle.Top
        //    };
        //    this.Controls.Add(pnlHeader);

        //    lblHeader = new Label
        //    {
        //        Text = "📦 ARTIKEL-KONTEXT",
        //        Font = new Font("Segoe UI", 18F, FontStyle.Bold),
        //        ForeColor = Color.White,
        //        Location = new Point(20, 10),
        //        Size = new Size(560, 35),
        //        TextAlign = ContentAlignment.MiddleLeft
        //    };
        //    pnlHeader.Controls.Add(lblHeader);

        //    // Artikel-Info Bereich
        //    var pnlInfo = new Panel
        //    {
        //        Location = new Point(20, 140),
        //        Size = new Size(560, 180),
        //        BackColor = Color.White,
        //        BorderStyle = BorderStyle.FixedSingle
        //    };
        //    this.Controls.Add(pnlInfo);

        //    // Info Labels
        //    lblIDCode = CreateInfoLabel("🏷️ ID-Code:", "", new Point(20, 20), pnlInfo);
        //    lblName = CreateInfoLabel("📦 Name:", "", new Point(20, 50), pnlInfo);
        //    lblStorage = CreateInfoLabel("📍 Lager:", "", new Point(20, 80), pnlInfo);
        //    lblQuantity = CreateInfoLabel("📊 Menge:", "", new Point(20, 110), pnlInfo);
        //    lblPrice = CreateInfoLabel("💰 Preis:", "", new Point(20, 140), pnlInfo);

        //    // Aktionen Bereich
        //    var lblActions = new Label
        //    {
        //        Text = "AKTIONEN",
        //        Font = new Font("Segoe UI", 14F, FontStyle.Bold),
        //        ForeColor = Color.FromArgb(51, 51, 51),
        //        Location = new Point(20, 340),
        //        Size = new Size(200, 30)
        //    };
        //    this.Controls.Add(lblActions);

        //    pnlActions = new Panel
        //    {
        //        Location = new Point(20, 380),
        //        Size = new Size(560, 200),
        //        BackColor = Color.White,
        //        BorderStyle = BorderStyle.FixedSingle,
        //        AutoScroll = true
        //    };
        //    this.Controls.Add(pnlActions);

        //    CreateActionButtons();

        //    // Bottom Buttons
        //    var btnClose = new Button
        //    {
        //        Text = "❌ Kontext verlassen",
        //        Font = new Font("Segoe UI", 11F, FontStyle.Regular),
        //        Location = new Point(20, 600),
        //        Size = new Size(150, 45),
        //        BackColor = Color.FromArgb(220, 53, 69),
        //        ForeColor = Color.White,
        //        FlatStyle = FlatStyle.Flat,
        //        Cursor = Cursors.Hand
        //    };
        //    btnClose.FlatAppearance.BorderSize = 0;
        //    btnClose.Click += (s, e) => this.Close();
        //    this.Controls.Add(btnClose);

        //    var btnHome = new Button
        //    {
        //        Text = "🏠 Hauptmenü",
        //        Font = new Font("Segoe UI", 11F, FontStyle.Regular),
        //        Location = new Point(430, 600),
        //        Size = new Size(150, 45),
        //        BackColor = Color.FromArgb(108, 117, 125),
        //        ForeColor = Color.White,
        //        FlatStyle = FlatStyle.Flat,
        //        Cursor = Cursors.Hand
        //    };
        //    btnHome.FlatAppearance.BorderSize = 0;
        //    btnHome.Click += (s, e) => {
        //        this.DialogResult = DialogResult.OK;
        //        this.Close();
        //    };
        //    this.Controls.Add(btnHome);
        //}

        private Label CreateInfoLabel(string caption, string value, Point location, Control parent)
        {
            var lblCaption = new Label
            {
                Text = caption,
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = Color.FromArgb(51, 51, 51),
                Location = location,
                Size = new Size(120, 25)
            };
            parent.Controls.Add(lblCaption);

            var lblValue = new Label
            {
                Text = value,
                Font = new Font("Segoe UI", 11F, FontStyle.Regular),
                ForeColor = Color.FromArgb(51, 51, 51),
                Location = new Point(location.X + 130, location.Y),
                Size = new Size(400, 25)
            };
            parent.Controls.Add(lblValue);

            return lblValue;
        }

        private void CreateActionButtons()
        {
            var buttons = new[]
            {
                new { Text = "📝 Details bearbeiten", Color = Color.FromArgb(0, 123, 255), Action = (EventHandler)BtnEditDetails_Click },
                new { Text = "📍 Umlagern", Color = Color.FromArgb(255, 193, 7), Action = (EventHandler)BtnRelocate_Click },
                new { Text = "➕ Menge ändern", Color = Color.FromArgb(40, 167, 69), Action = (EventHandler)BtnChangeQuantity_Click },
                new { Text = "✂️ Artikel teilen", Color = Color.FromArgb(255, 87, 34), Action = (EventHandler)BtnSplitArticle_Click },
                new { Text = "🔄 Artikel zusammenführen", Color = Color.FromArgb(156, 39, 176), Action = (EventHandler)BtnMergeArticle_Click },
                new { Text = "📋 Historie anzeigen", Color = Color.FromArgb(108, 117, 125), Action = (EventHandler)BtnShowHistory_Click },
                new { Text = "🖨️ Neues Label drucken", Color = Color.FromArgb(13, 202, 240), Action = (EventHandler)BtnPrintLabel_Click }
            };

            int yPos = 15;
            int buttonWidth = 250;
            int buttonHeight = 45;
            int margin = 15;

            for (int i = 0; i < buttons.Length; i++)
            {
                var btn = new Button
                {
                    Text = buttons[i].Text,
                    Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                    Size = new Size(buttonWidth, buttonHeight),
                    BackColor = buttons[i].Color,
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat,
                    Cursor = Cursors.Hand
                };
                btn.FlatAppearance.BorderSize = 0;
                btn.Click += buttons[i].Action;

                // Zwei Spalten Layout
                if (i % 2 == 0)
                {
                    btn.Location = new Point(15, yPos);
                }
                else
                {
                    btn.Location = new Point(15 + buttonWidth + margin, yPos);
                    yPos += buttonHeight + margin;
                }

                pnlActions.Controls.Add(btn);
            }
        }

        private void LoadArticleData()
        {
            var currentArticle = _dataService.GetArticleById(_article.Id);
            if (currentArticle != null)
            {
                lblIDCode.Text = currentArticle.IDCode;
                lblName.Text = currentArticle.Name;
                lblStorage.Text = currentArticle.Storage?.Name ?? "Kein Lager zugewiesen";
                lblQuantity.Text = $"{currentArticle.Quantity} {GetQuantityUnit(currentArticle.Category)}";
                lblPrice.Text = $"€{currentArticle.Price:F2}";
                lblHeader.Text = $"📦 {currentArticle.Name} #{currentArticle.IDCode}";
            }
        }

        private string GetQuantityUnit(string category)
        {
            return category?.ToLower() switch
            {
                "werkzeug" => "Stück",
                "befestigung" => "Stück",
                "elektro" => "Meter",
                _ => "Einheit(en)"
            };
        }

        #region Action Button Event Handlers

        private void BtnEditDetails_Click(object? sender, EventArgs e)
        {
            using (var editForm = new ArticleEditForm(_article))
            {
                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    LoadArticleData();
                    _dataService.LogAction(_article.Id, "Edit", "Artikel-Details bearbeitet");
                }
            }
        }

        private void BtnRelocate_Click(object? sender, EventArgs e)
        {
            MessageBox.Show(
                $"Umlagerung für Artikel '{_article.Name}' wird geöffnet.\n\n" +
                "Hier würden Sie:\n" +
                "• Neues Lager scannen/auswählen\n" +
                "• Umlagerung bestätigen\n" +
                "• Lager-Info wird aktualisiert",
                "Umlagern (Demo)",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void BtnChangeQuantity_Click(object? sender, EventArgs e)
        {
            using (var quantityForm = new QuantityChangeForm(_article))
            {
                if (quantityForm.ShowDialog() == DialogResult.OK)
                {
                    LoadArticleData();
                    _dataService.LogAction(_article.Id, "QuantityChange",
                        $"Menge geändert auf {_article.Quantity}");
                }
            }
        }

        private void BtnSplitArticle_Click(object? sender, EventArgs e)
        {
            if (_article.Quantity <= 1)
            {
                MessageBox.Show("Artikel kann nicht geteilt werden.\nMenge muss größer als 1 sein.",
                    "Teilung nicht möglich", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            MessageBox.Show(
                $"Artikel-Teilung für '{_article.Name}' (Menge: {_article.Quantity}) wird geöffnet.\n\n" +
                "Hier würden Sie:\n" +
                "• Neue Teilmenge eingeben\n" +
                "• Neuen ID-Code generieren\n" +
                "• Teilartikel erstellen\n" +
                "• Original-Menge reduzieren",
                "Artikel teilen (Demo)",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void BtnMergeArticle_Click(object? sender, EventArgs e)
        {
            var result = MessageBox.Show(
                $"Artikel zusammenführen für '{_article.Name}':\n\n" +
                "Möchten Sie einen anderen Artikel mit diesem zusammenführen?\n\n" +
                "• Anderen Artikel scannen\n" +
                "• Kompatibilität prüfen\n" +
                "• Mengen addieren\n" +
                "• Einen Artikel entfernen",
                "Artikel zusammenführen (Demo)",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                MessageBox.Show(
                    "Öffne Scanner für zweiten Artikel...\n(Funktion noch nicht implementiert)",
                    "Scanner öffnen",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }

        private void BtnShowHistory_Click(object? sender, EventArgs e)
        {
            try
            {
                var actions = _dataService.GetArticleHistory(_article.Id);
                string historyText = $"📋 Historie für Artikel '{_article.Name}' (#{_article.IDCode})\n\n";

                if (actions.Count == 0)
                {
                    historyText += "Keine Aktionen bisher aufgezeichnet.";
                }
                else
                {
                    foreach (var action in actions)
                    {
                        historyText += $"🕒 {action.Timestamp:dd.MM.yyyy HH:mm}\n";
                        historyText += $"   {action.ActionType}: {action.Details}\n\n";
                    }
                }

                var historyForm = new Form
                {
                    Text = $"Historie - {_article.Name}",
                    Size = new Size(500, 400),
                    StartPosition = FormStartPosition.CenterParent,
                    FormBorderStyle = FormBorderStyle.FixedDialog,
                    MaximizeBox = false,
                    MinimizeBox = false
                };

                var txtHistory = new TextBox
                {
                    Text = historyText,
                    Multiline = true,
                    ReadOnly = true,
                    ScrollBars = ScrollBars.Vertical,
                    Font = new Font("Consolas", 9F),
                    Dock = DockStyle.Fill,
                    BackColor = Color.White
                };

                historyForm.Controls.Add(txtHistory);
                historyForm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fehler beim Laden der Historie:\n{ex.Message}",
                    "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnPrintLabel_Click(object? sender, EventArgs e)
        {
            try
            {
                var idService = new IDService();
                var labelService = new LabelService(idService, _dataService);

                var result = MessageBox.Show(
                    $"Neues Label für Artikel '{_article.Name}' drucken?\n\n" +
                    $"ID-Code: {_article.IDCode}\n" +
                    $"Name: {_article.Name}\n" +
                    $"Lager: {_article.Storage?.Name ?? "Kein Lager"}\n" +
                    $"Menge: {_article.Quantity}",
                    "Label drucken",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    var labelContent = labelService.CreatePrintableLabel(_article.IDCode, _article.Name);

                    MessageBox.Show(
                        $"✅ Label wurde erfolgreich erstellt!\n\n" +
                        $"Label-Inhalt:\n{labelContent}\n\n" +
                        $"Druckdatum: {DateTime.Now:dd.MM.yyyy HH:mm}",
                        "Label erstellt",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    _dataService.LogAction(_article.Id, "PrintLabel",
                        $"Neues Label erstellt am {DateTime.Now:dd.MM.yyyy HH:mm}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fehler beim Erstellen des Labels:\n{ex.Message}",
                    "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            try
            {
                _dataService.LogAction(_article.Id, "CloseContext",
                    $"Artikel-Kontext geschlossen um {DateTime.Now:HH:mm}");
            }
            catch
            {
                // Fehler beim Protokollieren ignorieren
            }

            base.OnFormClosing(e);
        }
    }
}