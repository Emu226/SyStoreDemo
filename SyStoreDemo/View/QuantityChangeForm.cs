using System;
using System.Drawing;
using System.Windows.Forms;
using SimplyStore.WinForms.Models;
using SimplyStore.WinForms.Services;

namespace SyStoreDemo.View.Forms
{
    public class QuantityChangeForm : Form
    {
        private readonly Article _article;
        private readonly DataService _dataService;
        private NumericUpDown numQuantity;
        private Label lblCurrentQuantity;
        private Label lblDifference;

        public QuantityChangeForm(Article article)
        {
            _article = article ?? throw new ArgumentNullException(nameof(article));
            _dataService = new DataService();
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            // Form Eigenschaften
            this.Text = "➕ Menge ändern";
            this.Size = new Size(400, 300);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Font = new Font("Segoe UI", 10F);
            this.BackColor = Color.FromArgb(245, 245, 245);

            // Header
            var lblHeader = new Label
            {
                Text = $"📊 Menge ändern für {_article.Name}",
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                ForeColor = Color.FromArgb(51, 51, 51),
                Location = new Point(20, 20),
                Size = new Size(360, 30)
            };
            this.Controls.Add(lblHeader);

            var lblIDCode = new Label
            {
                Text = $"ID-Code: {_article.IDCode}",
                Font = new Font("Segoe UI", 10F),
                ForeColor = Color.FromArgb(108, 117, 125),
                Location = new Point(20, 50),
                Size = new Size(360, 20)
            };
            this.Controls.Add(lblIDCode);

            // Aktuelle Menge
            var lblCurrentTitle = new Label
            {
                Text = "Aktuelle Menge:",
                Location = new Point(20, 90),
                Size = new Size(150, 25)
            };
            this.Controls.Add(lblCurrentTitle);

            lblCurrentQuantity = new Label
            {
                Text = $"{_article.Quantity} {GetQuantityUnit(_article.Category)}",
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                Location = new Point(170, 90),
                Size = new Size(200, 25)
            };
            this.Controls.Add(lblCurrentQuantity);

            // Neue Menge
            var lblNewQuantity = new Label
            {
                Text = "Neue Menge:",
                Location = new Point(20, 130),
                Size = new Size(150, 25)
            };
            this.Controls.Add(lblNewQuantity);

            numQuantity = new NumericUpDown
            {
                Location = new Point(170, 130),
                Size = new Size(120, 30),
                Minimum = 0,
                Maximum = 99999,
                Value = _article.Quantity,
                Font = new Font("Segoe UI", 12F),
                TextAlign = HorizontalAlignment.Right
            };
            numQuantity.ValueChanged += NumQuantity_ValueChanged;
            this.Controls.Add(numQuantity);

            var lblUnit = new Label
            {
                Text = GetQuantityUnit(_article.Category),
                Location = new Point(300, 130),
                Size = new Size(80, 25),
                TextAlign = ContentAlignment.MiddleLeft
            };
            this.Controls.Add(lblUnit);

            // Differenz
            lblDifference = new Label
            {
                Text = "Keine Änderung",
                Font = new Font("Segoe UI", 10F),
                ForeColor = Color.FromArgb(108, 117, 125),
                Location = new Point(170, 165),
                Size = new Size(200, 25)
            };
            this.Controls.Add(lblDifference);

            // Buttons
            var btnCancel = new Button
            {
                Text = "❌ Abbrechen",
                Location = new Point(20, 210),
                Size = new Size(150, 45),
                BackColor = Color.FromArgb(220, 53, 69),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.Click += (s, e) => this.DialogResult = DialogResult.Cancel;
            this.Controls.Add(btnCancel);

            var btnSave = new Button
            {
                Text = "💾 Speichern",
                Location = new Point(230, 210),
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

        private void NumQuantity_ValueChanged(object? sender, EventArgs e)
        {
            var difference = (int)numQuantity.Value - _article.Quantity;
            if (difference == 0)
            {
                lblDifference.Text = "Keine Änderung";
                lblDifference.ForeColor = Color.FromArgb(108, 117, 125);
            }
            else
            {
                lblDifference.Text = difference > 0 
                    ? $"+{difference} {GetQuantityUnit(_article.Category)}"
                    : $"{difference} {GetQuantityUnit(_article.Category)}";
                lblDifference.ForeColor = difference > 0 
                    ? Color.FromArgb(40, 167, 69)    // Grün
                    : Color.FromArgb(220, 53, 69);   // Rot
            }
        }

        private void BtnSave_Click(object? sender, EventArgs e)
        {
            try
            {
                var newQuantity = (int)numQuantity.Value;
                if (newQuantity == _article.Quantity)
                {
                    this.DialogResult = DialogResult.Cancel;
                    return;
                }

                var difference = newQuantity - _article.Quantity;
                var changeType = difference > 0 ? "Zugang" : "Abgang";
                
                _article.Quantity = newQuantity;
                _article.LastModified = DateTime.Now;
                
                _dataService.UpdateArticle(_article);
                _dataService.LogAction(_article.Id, "QuantityChange", 
                    $"{changeType}: {Math.Abs(difference)} {GetQuantityUnit(_article.Category)}");

                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fehler beim Speichern der neuen Menge:\n{ex.Message}",
                    "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
    }
}