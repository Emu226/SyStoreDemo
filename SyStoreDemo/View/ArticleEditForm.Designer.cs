using SimplyStore.WinForms.Models;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace SyStoreDemo.View.Forms
{
    public partial class ArticleEditForm : Form
    {
        private TextBox txtName;
        private TextBox txtDescription;
        private TextBox txtPrice;
        private ComboBox cmbCategory;
        private ComboBox cmbStorage;
        private Button btnSave;
        private Button btnCancel;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.SuspendLayout();

            this.Text = "📝 Artikel bearbeiten";
            this.Size = new Size(500, 500);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Font = new Font("Segoe UI", 10F);
            this.BackColor = Color.FromArgb(245, 245, 245);

            var lblHeader = new Label
            {
                Text = "Artikel bearbeiten",  // Platzhalter statt {_article.Name}
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                ForeColor = Color.FromArgb(51, 51, 51),
                Location = new Point(20, 20),
                Size = new Size(460, 30),
                Name = "lblHeader"  // Name hinzugefügt für spätere Referenz
            };
            this.Controls.Add(lblHeader);

            var lblIDCode = new Label
            {
                Text = "ID-Code: ",  // Platzhalter statt {_article.IDCode}
                Font = new Font("Segoe UI", 10F),
                ForeColor = Color.FromArgb(108, 117, 125),
                Location = new Point(20, 50),
                Size = new Size(460, 20),
                Name = "lblIDCode"  // Name hinzugefügt für spätere Referenz
            };
            this.Controls.Add(lblIDCode);

            int yPos = 90;
            const int spacing = 60;

            CreateLabel("Name:", new Point(20, 90));
            txtName = CreateTextBox("", new Point(20, 115), 460);  // Leerer Text statt _article.Name

            CreateLabel("Beschreibung:", new Point(20, 150));
            txtDescription = CreateTextBox("", new Point(20, 175), 460);  // Leerer Text statt _article.Description
            txtDescription.Multiline = true;
            txtDescription.Height = 60;

            CreateLabel("Preis (€):", new Point(20, 240));
            txtPrice = CreateTextBox("0,00", new Point(20, 265), 200);  // Standardwert statt _article.Price
            txtPrice.TextAlign = HorizontalAlignment.Right;

            CreateLabel("Kategorie:", new Point(20, 300));
            cmbCategory = new ComboBox
            {
                Name = "cmbCategory",
                Location = new Point(20, 325),
                Size = new Size(200, 30),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            
            // Nur eine Methode zur Initialisierung der Items verwenden
            cmbCategory.Items.Add("Werkzeug");
            cmbCategory.Items.Add("Befestigung");
            cmbCategory.Items.Add("Elektro"); 
            cmbCategory.Items.Add("Sonstiges");
            
            // ComboBox nur einmal zu den Controls hinzufügen
            this.Controls.Add(cmbCategory);

            CreateLabel("Lager:", new Point(20, 360));
            cmbStorage = new ComboBox
            {
                Location = new Point(20, 385),
                Size = new Size(460, 30),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            this.Controls.Add(cmbStorage);

            btnCancel = new Button
            {
                Name = "btnCancel",
                Text = "❌ Abbrechen",
                Location = new Point(20, 440),
                Size = new Size(150, 45),
                BackColor = Color.FromArgb(220, 53, 69),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.Click += BtnCancel_Click;  // Statt Lambda-Ausdruck
            this.Controls.Add(btnCancel);

            btnSave = new Button
            {
                Name = "btnSave",
                Text = "💾 Speichern",
                Location = new Point(330, 440),
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

        #endregion

        private void CreateLabel(string text, Point location)
        {
            var label = new Label
            {
                Text = text,
                Location = location,
                Size = new Size(460, 20),
                Font = new Font("Segoe UI", 10F)
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
    }
}