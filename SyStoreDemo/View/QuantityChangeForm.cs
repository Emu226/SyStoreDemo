using System;
using System.Drawing;
using System.Windows.Forms;
using SimplyStore.WinForms.Models;
using SimplyStore.WinForms.Services;

namespace SyStoreDemo.View.Forms
{
    public partial class QuantityChangeForm : Form
    {
        private readonly Article _article;
        private readonly DataService _dataService;

        public QuantityChangeForm(Article article)
        {
            _article = article ?? throw new ArgumentNullException(nameof(article));
            _dataService = new DataService();
            InitializeComponent();
            InitializeData();
        }

        private void InitializeData()
        {
            var lblHeader = Controls.Find("lblHeader", true).FirstOrDefault() as Label;
            if (lblHeader != null)
                lblHeader.Text = $"📊 Menge ändern für {_article.Name}";

            var lblIDCode = Controls.Find("lblIDCode", true).FirstOrDefault() as Label;
            if (lblIDCode != null)
                lblIDCode.Text = $"ID-Code: {_article.IDCode}";

            lblCurrentQuantity.Text = $"{_article.Quantity} {GetQuantityUnit(_article.Category)}";
            numQuantity.Value = _article.Quantity;

            var lblUnit = Controls.Find("lblUnit", true).FirstOrDefault() as Label;
            if (lblUnit != null)
                lblUnit.Text = GetQuantityUnit(_article.Category);
        }

        private void NumQuantity_ValueChanged(object sender, EventArgs e)
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

        private void BtnSave_Click(object sender, EventArgs e)
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

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
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