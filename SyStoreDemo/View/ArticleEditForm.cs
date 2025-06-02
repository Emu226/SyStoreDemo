using System;
using System.Windows.Forms;
using SimplyStore.WinForms.Models;
using SimplyStore.WinForms.Services;

namespace SyStoreDemo.View.Forms
{
    public partial class ArticleEditForm : Form
    {
        private readonly Article _article;
        private readonly DataService _dataService;

        public ArticleEditForm(Article article)
        {
            _article = article ?? throw new ArgumentNullException(nameof(article));
            _dataService = new DataService();
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            // Setze die dynamischen Texte
            var lblHeader = Controls.Find("lblHeader", true).FirstOrDefault() as Label;
            if (lblHeader != null)
                lblHeader.Text = $"Artikel bearbeiten: {_article.Name}";

            var lblIDCode = Controls.Find("lblIDCode", true).FirstOrDefault() as Label;
            if (lblIDCode != null)
                lblIDCode.Text = $"ID-Code: {_article.IDCode}";

            // Setze die Textbox-Werte
            txtName.Text = _article.Name;
            txtDescription.Text = _article.Description;
            txtPrice.Text = _article.Price.ToString("F2");

            // Rest der LoadData-Methode...
            if (!string.IsNullOrEmpty(_article.Category))
            {
                cmbCategory.SelectedItem = _article.Category;
            }

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

                _article.Name = txtName.Text.Trim();
                _article.Description = txtDescription.Text.Trim();
                _article.Price = price;
                _article.Category = cmbCategory.SelectedItem?.ToString() ?? "Sonstiges";
                _article.StorageId = (int)cmbStorage.SelectedValue;
                _article.LastModified = DateTime.Now;

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

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
