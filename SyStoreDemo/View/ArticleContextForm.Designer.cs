namespace SyStoreDemo.View
{
    partial class ArticleContextForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        // UI Controls
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Label lblIDCode;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblStorage;
        private System.Windows.Forms.Label lblQuantity;
        private System.Windows.Forms.Label lblPrice;
        private System.Windows.Forms.Panel pnlActions;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Panel pnlInfo;
        private System.Windows.Forms.Label lblActions;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnHome;
        private System.Windows.Forms.Button btnEditDetails;
        private System.Windows.Forms.Button btnRelocate;
        private System.Windows.Forms.Button btnChangeQuantity;
        private System.Windows.Forms.Button btnSplitArticle;
        private System.Windows.Forms.Button btnMergeArticle;
        private System.Windows.Forms.Button btnShowHistory;
        private System.Windows.Forms.Button btnPrintLabel;

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

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            pnlHeader = new Panel();
            lblHeader = new Label();
            pnlInfo = new Panel();
            lblIDCode = new Label();
            lblName = new Label();
            lblStorage = new Label();
            lblQuantity = new Label();
            lblPrice = new Label();
            lblActions = new Label();
            pnlActions = new Panel();
            btnEditDetails = new Button();
            btnRelocate = new Button();
            btnChangeQuantity = new Button();
            btnSplitArticle = new Button();
            btnMergeArticle = new Button();
            btnShowHistory = new Button();
            btnPrintLabel = new Button();
            btnClose = new Button();
            btnHome = new Button();
            pnlHeader.SuspendLayout();
            pnlInfo.SuspendLayout();
            pnlActions.SuspendLayout();
            SuspendLayout();
            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = Color.FromArgb(0, 123, 255);
            pnlHeader.Controls.Add(lblHeader);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new Size(600, 120);
            pnlHeader.TabIndex = 0;
            // 
            // lblHeader
            // 
            lblHeader.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblHeader.ForeColor = Color.White;
            lblHeader.Location = new Point(20, 10);
            lblHeader.Name = "lblHeader";
            lblHeader.Size = new Size(560, 35);
            lblHeader.TabIndex = 0;
            lblHeader.Text = "📦 ARTIKEL-KONTEXT";
            lblHeader.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // pnlInfo
            // 
            pnlInfo.BackColor = Color.White;
            pnlInfo.BorderStyle = BorderStyle.FixedSingle;
            pnlInfo.Controls.Add(lblIDCode);
            pnlInfo.Controls.Add(lblName);
            pnlInfo.Controls.Add(lblStorage);
            pnlInfo.Controls.Add(lblQuantity);
            pnlInfo.Controls.Add(lblPrice);
            pnlInfo.Location = new Point(20, 140);
            pnlInfo.Name = "pnlInfo";
            pnlInfo.Size = new Size(560, 180);
            pnlInfo.TabIndex = 1;
            // 
            // lblActions
            // 
            lblActions.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblActions.ForeColor = Color.FromArgb(51, 51, 51);
            lblActions.Location = new Point(20, 340);
            lblActions.Name = "lblActions";
            lblActions.Size = new Size(200, 30);
            lblActions.TabIndex = 2;
            lblActions.Text = "AKTIONEN";
            // 
            // pnlActions
            // 
            pnlActions.AutoScroll = true;
            pnlActions.BackColor = Color.White;
            pnlActions.BorderStyle = BorderStyle.FixedSingle;
            pnlActions.Controls.Add(btnEditDetails);
            pnlActions.Controls.Add(btnRelocate);
            pnlActions.Controls.Add(btnChangeQuantity);
            pnlActions.Controls.Add(btnSplitArticle);
            pnlActions.Controls.Add(btnMergeArticle);
            pnlActions.Controls.Add(btnShowHistory);
            pnlActions.Controls.Add(btnPrintLabel);
            pnlActions.Location = new Point(20, 380);
            pnlActions.Name = "pnlActions";
            pnlActions.Size = new Size(560, 200);
            pnlActions.TabIndex = 3;
            // 
            // btnEditDetails
            // 
            btnEditDetails.BackColor = Color.FromArgb(0, 123, 255);
            btnEditDetails.FlatStyle = FlatStyle.Flat;
            btnEditDetails.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnEditDetails.ForeColor = Color.White;
            btnEditDetails.Location = new Point(15, 15);
            btnEditDetails.Name = "btnEditDetails";
            btnEditDetails.Size = new Size(250, 45);
            btnEditDetails.TabIndex = 0;
            btnEditDetails.Text = "📝 Details bearbeiten";
            btnEditDetails.UseVisualStyleBackColor = false;
            btnEditDetails.Click += BtnEditDetails_Click;
            // 
            // btnClose
            // 
            btnClose.BackColor = Color.FromArgb(220, 53, 69);
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.Font = new Font("Segoe UI", 11F);
            btnClose.ForeColor = Color.White;
            btnClose.Location = new Point(20, 600);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(150, 45);
            btnClose.TabIndex = 4;
            btnClose.Text = "❌ Kontext verlassen";
            btnClose.UseVisualStyleBackColor = false;
            // 
            // btnHome
            // 
            btnHome.BackColor = Color.FromArgb(108, 117, 125);
            btnHome.FlatStyle = FlatStyle.Flat;
            btnHome.Font = new Font("Segoe UI", 11F);
            btnHome.ForeColor = Color.White;
            btnHome.Location = new Point(430, 600);
            btnHome.Name = "btnHome";
            btnHome.Size = new Size(150, 45);
            btnHome.TabIndex = 5;
            btnHome.Text = "🏠 Hauptmenü";
            btnHome.UseVisualStyleBackColor = false;
            // 
            // ArticleContextForm
            // 
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 245, 245);
            ClientSize = new Size(600, 700);
            Controls.Add(pnlHeader);
            Controls.Add(pnlInfo);
            Controls.Add(lblActions);
            Controls.Add(pnlActions);
            Controls.Add(btnClose);
            Controls.Add(btnHome);
            Font = new Font("Segoe UI", 10F);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "ArticleContextForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "📦 Artikel-Kontext";
            pnlHeader.ResumeLayout(false);
            pnlInfo.ResumeLayout(false);
            pnlActions.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
    }
}