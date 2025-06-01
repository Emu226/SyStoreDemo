namespace SyStoreDemo.View
{
    partial class MainForm
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

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            button4 = new Button();
            sqliteCommand1 = new Microsoft.Data.Sqlite.SqliteCommand();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(51, 43);
            button1.Name = "button1";
            button1.Size = new Size(160, 160);
            button1.TabIndex = 0;
            button1.Text = "Artikel scannen";
            button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.Location = new Point(217, 43);
            button2.Name = "button2";
            button2.Size = new Size(160, 160);
            button2.TabIndex = 1;
            button2.Text = "Lager anzeigen";
            button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            button3.Location = new Point(383, 43);
            button3.Name = "button3";
            button3.Size = new Size(160, 160);
            button3.TabIndex = 2;
            button3.Text = "Artikel anzeigen";
            button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            button4.Location = new Point(549, 43);
            button4.Name = "button4";
            button4.Size = new Size(160, 160);
            button4.TabIndex = 3;
            button4.Text = "Dashboard";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // sqliteCommand1
            // 
            sqliteCommand1.CommandTimeout = 30;
            sqliteCommand1.Connection = null;
            sqliteCommand1.Transaction = null;
            sqliteCommand1.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Name = "MainForm";
            Text = "MainForm";
            Load += MainForm_Load;
            ResumeLayout(false);
        }

        #endregion

        private Button button1;
        private Button button2;
        private Button button3;
        private Button button4;
        private Microsoft.Data.Sqlite.SqliteCommand sqliteCommand1;
    }
}