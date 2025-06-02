using System;
using System.Drawing;
using System.Windows.Forms;

namespace SyStoreDemo.View.Forms
{
    partial class QuantityChangeForm
    {
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
                Name = "lblHeader",
                Text = "Menge ändern",
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                ForeColor = Color.FromArgb(51, 51, 51),
                Location = new Point(20, 20),
                Size = new Size(360, 30)
            };
            this.Controls.Add(lblHeader);

            var lblIDCode = new Label
            {
                Name = "lblIDCode",
                Text = "ID-Code: ",
                Font = new Font("Segoe UI", 10F),
                ForeColor = Color.FromArgb(108, 117, 125),
                Location = new Point(20, 50),
                Size = new Size(360, 20)
            };
            this.Controls.Add(lblIDCode);

            // Aktuelle Menge
            var lblCurrentTitle = new Label
            {
                Name = "lblCurrentTitle",
                Text = "Aktuelle Menge:",
                Location = new Point(20, 90),
                Size = new Size(150, 25)
            };
            this.Controls.Add(lblCurrentTitle);

            lblCurrentQuantity = new Label
            {
                Name = "lblCurrentQuantity",
                Text = "0",
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                Location = new Point(170, 90),
                Size = new Size(200, 25)
            };
            this.Controls.Add(lblCurrentQuantity);

            // Neue Menge
            var lblNewQuantity = new Label
            {
                Name = "lblNewQuantity",
                Text = "Neue Menge:",
                Location = new Point(20, 130),
                Size = new Size(150, 25)
            };
            this.Controls.Add(lblNewQuantity);

            numQuantity = new NumericUpDown
            {
                Name = "numQuantity",
                Location = new Point(170, 130),
                Size = new Size(120, 30),
                Minimum = 0,
                Maximum = 99999,
                Value = 0,
                Font = new Font("Segoe UI", 12F),
                TextAlign = HorizontalAlignment.Right
            };
            this.Controls.Add(numQuantity);

            var lblUnit = new Label
            {
                Name = "lblUnit",
                Text = "Einheit(en)",
                Location = new Point(300, 130),
                Size = new Size(80, 25),
                TextAlign = ContentAlignment.MiddleLeft
            };
            this.Controls.Add(lblUnit);

            // Differenz
            lblDifference = new Label
            {
                Name = "lblDifference",
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
                Name = "btnCancel",
                Text = "❌ Abbrechen",
                Location = new Point(20, 210),
                Size = new Size(150, 45),
                BackColor = Color.FromArgb(220, 53, 69),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.Click += BtnCancel_Click;
            this.Controls.Add(btnCancel);

            var btnSave = new Button
            {
                Name = "btnSave",
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

            numQuantity.ValueChanged += NumQuantity_ValueChanged;

            this.ResumeLayout(false);
        }

        #endregion

        private NumericUpDown numQuantity;
        private Label lblCurrentQuantity;
        private Label lblDifference;
    }
}