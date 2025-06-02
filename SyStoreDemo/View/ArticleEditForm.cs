using System;
using System.Drawing;
using System.Windows.Forms;
using SimplyStore.WinForms.Models;
using SimplyStore.WinForms.Services;

namespace SyStoreDemo.View.Forms
{
    public class ArticleEditForm : Form
    {
        private readonly Article _article;
        private readonly DataService _dataService;

        // UI Controls
        private TextBox txtName;
        private TextBox txtDescription;
        private TextBox txtPrice;
        private ComboBox cmbCategory;
        private ComboBox cmbStorage;
        private Button btnSave;
        private Button btnCancel;

        public ArticleEditForm(Article article)
        {
            _article = article ?? throw new ArgumentNullException(nameof(article));
            _dataService = new DataService();
            InitializeComponent();
            LoadData();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            // Form Eigenschaften
            this.Text = "📝 Artikel bearbeiten";
            this.Size = new Size(500, 500);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Font = new Font("Segoe UI", 10F);
            this.BackColor = Color.FromArgb(245, 245, 245);

            // Header
            var lblHeader = new Label
            {
                Text = $"Artikel bearbeiten: {_article.Name}",
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                ForeColor = Color.FromArgb(51, 51, 51),
                Location = new Point(20, 20),
                Size = new Size(460, 30)
            };
            this.Controls.Add(lblHeader);

            var lblIDCode = new Label
            {
                Text = $"ID-Code: {_article.IDCode}",
                Font = new Font("Segoe UI", 10F, FontStyle.Regular),
                ForeColor = Color.FromArgb(108, 117, 125),
                Location = new Point(20, 50),
                Size = new Size(460, 20)
            };
            this.Controls.Add(lblIDCode);

            // Eingabefelder
            int yPos = 90;
            int spacing = 60;

            // Name
            CreateLabel("Name:", new Point(20, yPos));
            txtName = CreateTextBox(_article.Name, new Point(20, yPos + 25), 460);
            yPos += spacing;

            // Beschreibung
            CreateLabel("Beschreibung:", new Point(20, yPos));
            txtDescription = CreateTextBox(_article.Description, new Point(20, yPos + 25), 460);
            txtDescription.Multiline = true;
            txtDescription.Height = 60;
            yPos += spacing + 30;

            // Preis
            CreateLabel("Preis (€):", new Point(20, yPos));
            txtPrice = CreateTextBox(_article.Price.ToString("F2"), new Point(20, yPos + 25), 200);
            txtPrice.TextAlign = HorizontalAlignment.Right;
            yPos += spacing;

            // Kategorie
            CreateLabel("Kategorie:", new Point(20, yPos));
            cmbCategory = new ComboBox
            {
                Location = new Point(20, yPos + 25),
                Size = new Size(200, 30),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbCategory.Items.AddRange(new[] { "Werkzeug", "Befestigung", "Elektro", "Sonstiges" });
            this.Controls.Add(cmbCategory);
            yPos += spacing;

            // Lager
            CreateLabel("Lager:", new Point(20, yPos));
            cmbStorage = new ComboBox
            {
                Location = new Point(20, yPos + 25),
                Size = new Size(460, 30),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            this.Controls.Add(cmbStorage);
            yPos += spacing + 20;

            // Buttons
            btnCancel = new Button
            {
                Text = "❌ Abbrechen",
                Location = new Point(20, yPos),
                Size = new Size(150, 45),
                BackColor = Color.FromArgb(220, 53, 69),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.Click += (s, e) => this.DialogResult = DialogResult.Cancel;
            this.Controls.Add(btnCancel);

            btnSave = new Button
            {
                Text = "💾 Speichern",
                Location = new Point(330, yPos),
                Size = new Size(150, 45),
                BackColor = Color.FromArgb(40, 167, 69),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.Click += BtnSave_Click;
            this.Controls.Add(btnSave);

            this.ResumeLayout(false);
        }

        private void CreateLabel(string text, Point location)
        {
            var label = new Label
            {
                Text = text,
                Location = location,
                Size = new Size(460, 20),
                Font = new Font("Segoe UI", 10F, FontStyle.Regular)
            };
            this.Controls.Add(label);
        }

        private TextBox CreateTextBox(string text, Point location, int width)
        {
            var textBox = new TextBox
            {
                Text = text,
                Location = location,
                Size = new Size(width, 30),
                BorderStyle = BorderStyle.FixedSingle,
                Font = new Font("Segoe UI", 11F)
            };
            this.Controls.Add(textBox);
            return textBox;
        }

        private void LoadData()
        {
            // Kategorien
            if (!string.IsNullOrEmpty(_article.Category))
            {
                cmbCategory.SelectedItem = _article.Category;
            }

            // Lager laden
            var storages = _dataService.GetAllStorages();
            cmbStorage.DisplayMember = "Name";
            cmbStorage.ValueMember = "Id";
            cmbStorage.DataSource = storages;

            if (_article.Storage != null)
            {
                cmbStorage.SelectedValue = _article.StorageId;
            }
        }

        private void BtnSave_Click(object? sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtName.Text))
                {
                    MessageBox.Show("Bitte geben Sie einen Namen ein.",
                        "Validierung", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!decimal.TryParse(txtPrice.Text, out decimal price))
                {
                    MessageBox.Show("Bitte geben Sie einen gültigen Preis ein.",
                        "Validierung", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Artikel aktualisieren
                _article.Name = txtName.Text.Trim();
                _article.Description = txtDescription.Text.Trim();
                _article.Price = price;
                _article.Category = cmbCategory.SelectedItem?.ToString() ?? "Sonstiges";
                _article.StorageId = (int)cmbStorage.SelectedValue;
                _article.LastModified = DateTime.Now;

                // Speichern
                _dataService.UpdateArticle(_article);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fehler beim Speichern: {ex.Message}",
                    "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}