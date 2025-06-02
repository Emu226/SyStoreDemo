using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using SimplyStore.WinForms.Models;
using SimplyStore.WinForms.Services;

namespace SyStoreDemo.View
{
    public partial class ScannerForm : Form
    {
        private readonly DataService _dataService;
        private TextBox txtIDCode;
        private Button btnSearch;
        private Button btnFileUpload;
        private ListBox lstResults;
        private Label lblStatus;
        private Button btnOpenContext;
        private Button btnCreateNew;
        private Button btnBack;

        // Aktuell ausgewähltes Item
        private object _selectedItem;
        private string _selectedType;

        public ScannerForm()
        {
            _dataService = new DataService();
            InitializeComponent();
            SetupForm();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            // Form Eigenschaften
            this.Text = "📱 Simply Store - Scanner";
            this.Size = new Size(500, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            this.BackColor = Color.FromArgb(245, 245, 245);

            this.ResumeLayout(false);
        }

        private void SetupForm()
        {
            // Header Label
            var lblHeader = new Label
            {
                Text = "📱 ARTIKEL/LAGER SCANNEN",
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                ForeColor = Color.FromArgb(51, 51, 51),
                AutoSize = false,
                Size = new Size(460, 40),
                Location = new Point(20, 20),
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.Transparent
            };
            this.Controls.Add(lblHeader);

            // ID-Code Eingabe Bereich
            var lblIDCode = new Label
            {
                Text = "ID-Code eingeben:",
                Font = new Font("Segoe UI", 12F, FontStyle.Regular),
                Location = new Point(20, 80),
                Size = new Size(200, 25),
                ForeColor = Color.FromArgb(51, 51, 51)
            };
            this.Controls.Add(lblIDCode);

            txtIDCode = new TextBox
            {
                Font = new Font("Segoe UI", 14F, FontStyle.Regular),
                Location = new Point(20, 110),
                Size = new Size(460, 35),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };
            txtIDCode.KeyDown += TxtIDCode_KeyDown;
            txtIDCode.TextChanged += TxtIDCode_TextChanged;
            this.Controls.Add(txtIDCode);

            // Buttons für Aktionen
            btnSearch = new Button
            {
                Text = "🔍 Suchen",
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                Location = new Point(20, 160),
                Size = new Size(150, 45),
                BackColor = Color.FromArgb(0, 123, 255),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnSearch.FlatAppearance.BorderSize = 0;
            btnSearch.Click += BtnSearch_Click;
            this.Controls.Add(btnSearch);

            btnFileUpload = new Button
            {
                Text = "📁 Datei hochladen",
                Font = new Font("Segoe UI", 11F, FontStyle.Regular),
                Location = new Point(190, 160),
                Size = new Size(150, 45),
                BackColor = Color.FromArgb(108, 117, 125),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnFileUpload.FlatAppearance.BorderSize = 0;
            btnFileUpload.Click += BtnFileUpload_Click;
            this.Controls.Add(btnFileUpload);

            // Status Label
            lblStatus = new Label
            {
                Text = "Geben Sie einen ID-Code ein oder laden Sie eine Datei hoch",
                Font = new Font("Segoe UI", 10F, FontStyle.Italic),
                Location = new Point(20, 220),
                Size = new Size(460, 25),
                ForeColor = Color.FromArgb(108, 117, 125),
                TextAlign = ContentAlignment.MiddleLeft
            };
            this.Controls.Add(lblStatus);

            // Ergebnisse Liste
            var lblResults = new Label
            {
                Text = "Suchergebnisse:",
                Font = new Font("Segoe UI", 12F, FontStyle.Regular),
                Location = new Point(20, 250),
                Size = new Size(200, 25),
                ForeColor = Color.FromArgb(51, 51, 51)
            };
            this.Controls.Add(lblResults);

            lstResults = new ListBox
            {
                Font = new Font("Segoe UI", 11F, FontStyle.Regular),
                Location = new Point(20, 280),
                Size = new Size(460, 200),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                DrawMode = DrawMode.OwnerDrawFixed,
                ItemHeight = 50
            };
            lstResults.DrawItem += LstResults_DrawItem;
            lstResults.SelectedIndexChanged += LstResults_SelectedIndexChanged;
            this.Controls.Add(lstResults);

            // Aktions-Buttons
            btnOpenContext = new Button
            {
                Text = "▶️ Kontext öffnen",
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                Location = new Point(20, 500),
                Size = new Size(150, 45),
                BackColor = Color.FromArgb(40, 167, 69),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Enabled = false
            };
            btnOpenContext.FlatAppearance.BorderSize = 0;
            btnOpenContext.Click += BtnOpenContext_Click;
            this.Controls.Add(btnOpenContext);

            btnCreateNew = new Button
            {
                Text = "➕ Neu erstellen",
                Font = new Font("Segoe UI", 11F, FontStyle.Regular),
                Location = new Point(190, 500),
                Size = new Size(130, 45),
                BackColor = Color.FromArgb(255, 193, 7),
                ForeColor = Color.FromArgb(51, 51, 51),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnCreateNew.FlatAppearance.BorderSize = 0;
            btnCreateNew.Click += BtnCreateNew_Click;
            this.Controls.Add(btnCreateNew);

            btnBack = new Button
            {
                Text = "🏠 Hauptmenü",
                Font = new Font("Segoe UI", 11F, FontStyle.Regular),
                Location = new Point(340, 500),
                Size = new Size(140, 45),
                BackColor = Color.FromArgb(108, 117, 125),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnBack.FlatAppearance.BorderSize = 0;
            btnBack.Click += BtnBack_Click;
            this.Controls.Add(btnBack);

            // Focus auf Eingabefeld setzen
            txtIDCode.Focus();
        }

        #region Event Handler

        private void TxtIDCode_KeyDown(object sender, KeyEventArgs e)
        {
            // Enter-Taste löst Suche aus
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                BtnSearch_Click(sender, e);
            }
        }

        private void TxtIDCode_TextChanged(object sender, EventArgs e)
        {
            // Automatische Suche bei Texteingabe (nach 2+ Zeichen)
            if (txtIDCode.Text.Trim().Length >= 2)
            {
                PerformSearch(txtIDCode.Text.Trim());
            }
            else if (txtIDCode.Text.Trim().Length == 0)
            {
                lstResults.Items.Clear();
                lblStatus.Text = "Geben Sie einen ID-Code ein oder laden Sie eine Datei hoch";
                btnOpenContext.Enabled = false;
            }
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIDCode.Text))
            {
                MessageBox.Show("Bitte geben Sie einen ID-Code ein.", "Eingabe erforderlich",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtIDCode.Focus();
                return;
            }

            PerformSearch(txtIDCode.Text.Trim());
        }

        private void BtnFileUpload_Click(object sender, EventArgs e)
        {
            using (var openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "ID-Code Datei auswählen (OCR Simulation)";
                openFileDialog.Filter = "Textdateien (*.txt)|*.txt|Alle Dateien (*.*)|*.*";
                openFileDialog.FilterIndex = 1;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string fileContent = File.ReadAllText(openFileDialog.FileName);

                        // Simuliere OCR-Erkennung (erste Zeile als ID-Code verwenden)
                        string recognizedID = fileContent.Split('\n')[0].Trim();

                        if (!string.IsNullOrEmpty(recognizedID))
                        {
                            txtIDCode.Text = recognizedID;
                            lblStatus.Text = $"📄 ID aus Datei erkannt: {recognizedID}";
                            PerformSearch(recognizedID);
                        }
                        else
                        {
                            lblStatus.Text = "❌ Keine ID in der Datei gefunden";
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Fehler beim Lesen der Datei:\n{ex.Message}", "Dateifehler",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void LstResults_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnOpenContext.Enabled = lstResults.SelectedIndex >= 0;

            if (lstResults.SelectedIndex >= 0)
            {
                var resultItem = (SearchResultItem)lstResults.SelectedItem;
                _selectedItem = resultItem.Item;
                _selectedType = resultItem.Type;
            }
        }

        private void BtnOpenContext_Click(object sender, EventArgs e)
        {
            if (_selectedItem == null) return;

            if (_selectedType == "Article")
            {
                var article = (Article)_selectedItem;
                // Öffne Artikel-Kontext Form
                var articleContextForm = new ArticleContextForm(article);
                this.Hide();
                articleContextForm.ShowDialog();
                this.Show();
            }
            else if (_selectedType == "Storage")
            {
                var storage = (Storage)_selectedItem;
                // Öffne Lager-Kontext Form (später implementieren)
                MessageBox.Show($"Lager-Kontext für '{storage.Name}' wird geöffnet.\n(Noch nicht implementiert)",
                    "Lager-Kontext", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BtnCreateNew_Click(object sender, EventArgs e)
        {
            string idCode = txtIDCode.Text.Trim();

            if (string.IsNullOrEmpty(idCode))
            {
                MessageBox.Show("Geben Sie zuerst einen ID-Code ein.", "ID-Code erforderlich",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var result = MessageBox.Show(
                $"Möchten Sie einen neuen Artikel mit ID '{idCode}' erstellen?",
                "Neuen Artikel erstellen",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // Öffne Artikel-Erstellungs-Dialog (später implementieren)
                MessageBox.Show($"Artikel-Erstellung für ID '{idCode}' wird geöffnet.\n(Noch nicht implementiert)",
                    "Artikel erstellen", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LstResults_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;

            e.DrawBackground();

            var item = (SearchResultItem)lstResults.Items[e.Index];
            var isSelected = (e.State & DrawItemState.Selected) == DrawItemState.Selected;

            // Hintergrundfarbe
            var bgColor = isSelected ? Color.FromArgb(0, 123, 255) : Color.White;
            var textColor = isSelected ? Color.White : Color.FromArgb(51, 51, 51);

            using (var bgBrush = new SolidBrush(bgColor))
            {
                e.Graphics.FillRectangle(bgBrush, e.Bounds);
            }

            // Icon und Text
            string icon = item.Type == "Article" ? "📦" : "🏪";
            string mainText = $"{icon} {item.DisplayText}";
            string subText = item.SubText;

            using (var textBrush = new SolidBrush(textColor))
            {
                var mainFont = new Font("Segoe UI", 11F, FontStyle.Bold);
                var subFont = new Font("Segoe UI", 9F, FontStyle.Regular);

                e.Graphics.DrawString(mainText, mainFont, textBrush,
                    new PointF(e.Bounds.X + 10, e.Bounds.Y + 5));
                e.Graphics.DrawString(subText, subFont, textBrush,
                    new PointF(e.Bounds.X + 10, e.Bounds.Y + 25));
            }

            e.DrawFocusRectangle();
        }

        #endregion

        #region Private Methods

        private void PerformSearch(string searchTerm)
        {
            try
            {
                lblStatus.Text = "🔍 Suche läuft...";
                lstResults.Items.Clear();
                btnOpenContext.Enabled = false;

                // Exakte Suche
                var (exactType, exactItem) = _dataService.FindByIDCode(searchTerm);

                if (exactItem != null)
                {
                    var exactResult = new SearchResultItem
                    {
                        Type = exactType,
                        Item = exactItem,
                        DisplayText = exactType == "Article" ?
                            $"{((Article)exactItem).IDCode} - {((Article)exactItem).Name}" :
                            $"{((Storage)exactItem).IDCode} - {((Storage)exactItem).Name}",
                        SubText = exactType == "Article" ?
                            $"📍 {((Article)exactItem).Storage?.Name ?? "Kein Lager"} | Menge: {((Article)exactItem).Quantity}" :
                            $"📦 {((Storage)exactItem).Articles?.Count ?? 0} Artikel"
                    };

                    lstResults.Items.Add(exactResult);
                    lblStatus.Text = $"✅ Exakte Übereinstimmung gefunden: {exactResult.DisplayText}";
                    lstResults.SelectedIndex = 0;
                    return;
                }

                // Ähnliche Artikel suchen
                var similarArticles = _dataService.FindSimilarArticles(searchTerm);

                foreach (var article in similarArticles)
                {
                    var result = new SearchResultItem
                    {
                        Type = "Article",
                        Item = article,
                        DisplayText = $"{article.IDCode} - {article.Name}",
                        SubText = $"📍 {article.Storage?.Name ?? "Kein Lager"} | Menge: {article.Quantity} | €{article.Price:F2}"
                    };

                    lstResults.Items.Add(result);
                }

                if (lstResults.Items.Count > 0)
                {
                    lblStatus.Text = $"📋 {lstResults.Items.Count} ähnliche Ergebnisse gefunden";
                }
                else
                {
                    lblStatus.Text = $"❌ Keine Ergebnisse für '{searchTerm}' gefunden";
                }
            }
            catch (Exception ex)
            {
                lblStatus.Text = "❌ Fehler bei der Suche";
                MessageBox.Show($"Fehler bei der Suche:\n{ex.Message}", "Suchfehler",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Helper Classes

        private class SearchResultItem
        {
            public string Type { get; set; }  // "Article" oder "Storage"
            public object Item { get; set; }
            public string DisplayText { get; set; }
            public string SubText { get; set; }

            public override string ToString()
            {
                return DisplayText;
            }
        }

        #endregion
    }
}