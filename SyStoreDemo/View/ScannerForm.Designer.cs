using System;
using System.Drawing;
using System.Windows.Forms;

namespace SyStoreDemo.View
{
    partial class ScannerForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private TextBox txtIDCode;
        private Button btnSearch;
        private Button btnFileUpload;
        private ListBox lstResults;
        private Label lblStatus;
        private Button btnOpenContext;
        private Button btnCreateNew;
        private Button btnBack;

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
            this.Text = "📱 Simply Store - Scanner";
            this.Size = new Size(500, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            this.BackColor = Color.FromArgb(245, 245, 245);

            // Header Label
            var lblHeader = new Label
            {
                Name = "lblHeader",
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
                Name = "lblIDCode",
                Text = "ID-Code eingeben:",
                Font = new Font("Segoe UI", 12F, FontStyle.Regular),
                Location = new Point(20, 80),
                Size = new Size(200, 25),
                ForeColor = Color.FromArgb(51, 51, 51)
            };
            this.Controls.Add(lblIDCode);

            txtIDCode = new TextBox
            {
                Name = "txtIDCode",
                Font = new Font("Segoe UI", 14F, FontStyle.Regular),
                Location = new Point(20, 110),
                Size = new Size(460, 35),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };
            this.Controls.Add(txtIDCode);

            // Buttons für Aktionen
            btnSearch = new Button
            {
                Name = "btnSearch",
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
            this.Controls.Add(btnSearch);

            btnFileUpload = new Button
            {
                Name = "btnFileUpload",
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
            this.Controls.Add(btnFileUpload);

            // Status Label
            lblStatus = new Label
            {
                Name = "lblStatus",
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
                Name = "lblResults",
                Text = "Suchergebnisse:",
                Font = new Font("Segoe UI", 12F, FontStyle.Regular),
                Location = new Point(20, 250),
                Size = new Size(200, 25),
                ForeColor = Color.FromArgb(51, 51, 51)
            };
            this.Controls.Add(lblResults);

            lstResults = new ListBox
            {
                Name = "lstResults",
                Font = new Font("Segoe UI", 11F, FontStyle.Regular),
                Location = new Point(20, 280),
                Size = new Size(460, 200),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                DrawMode = DrawMode.OwnerDrawFixed,
                ItemHeight = 50
            };
            this.Controls.Add(lstResults);

            // Aktions-Buttons
            btnOpenContext = new Button
            {
                Name = "btnOpenContext",
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
            this.Controls.Add(btnOpenContext);

            btnCreateNew = new Button
            {
                Name = "btnCreateNew",
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
            this.Controls.Add(btnCreateNew);

            btnBack = new Button
            {
                Name = "btnBack",
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
            this.Controls.Add(btnBack);

            // Event-Handler zuweisen
            txtIDCode.KeyDown += TxtIDCode_KeyDown;
            txtIDCode.TextChanged += TxtIDCode_TextChanged;
            btnSearch.Click += BtnSearch_Click;
            btnFileUpload.Click += BtnFileUpload_Click;
            lstResults.DrawItem += LstResults_DrawItem;
            lstResults.SelectedIndexChanged += LstResults_SelectedIndexChanged;
            btnOpenContext.Click += BtnOpenContext_Click;
            btnCreateNew.Click += BtnCreateNew_Click;
            btnBack.Click += BtnBack_Click;

            this.ResumeLayout(false);
        }

        #endregion
    }
}