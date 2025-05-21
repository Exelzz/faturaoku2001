namespace faturaoku2001
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.btnKlasorSec = new System.Windows.Forms.Button();
            this.btnCikisSec = new System.Windows.Forms.Button();
            this.btnBaslat = new System.Windows.Forms.Button();
            this.btnYenidenTara = new System.Windows.Forms.Button();
            this.btnCsvAktar = new System.Windows.Forms.Button();
            this.lblKaynak = new System.Windows.Forms.Label();
            this.lblCikis = new System.Windows.Forms.Label();
            this.dgvOnizleme = new System.Windows.Forms.DataGridView();
            this.lblDurum = new System.Windows.Forms.Label();
            this.rtbHtmlOnizleme = new System.Windows.Forms.RichTextBox();
            this.lstLoglar = new System.Windows.Forms.ListBox();
            this.lstGecersizler = new System.Windows.Forms.ListBox();

            ((System.ComponentModel.ISupportInitialize)(this.dgvOnizleme)).BeginInit();
            this.SuspendLayout();

            // btnKlasorSec
            this.btnKlasorSec.Location = new System.Drawing.Point(20, 20);
            this.btnKlasorSec.Size = new System.Drawing.Size(180, 35);
            this.btnKlasorSec.Text = "📂 Kaynak Klasörü";
            this.btnKlasorSec.Click += new System.EventHandler(this.btnKlasorSec_Click);

            // btnCikisSec
            this.btnCikisSec.Location = new System.Drawing.Point(210, 20);
            this.btnCikisSec.Size = new System.Drawing.Size(180, 35);
            this.btnCikisSec.Text = "📁 Çıkış Klasörü";
            this.btnCikisSec.Click += new System.EventHandler(this.btnCikisSec_Click);

            // btnYenidenTara
            this.btnYenidenTara.Location = new System.Drawing.Point(400, 20);
            this.btnYenidenTara.Size = new System.Drawing.Size(140, 35);
            this.btnYenidenTara.Text = "🔄 Yeniden Tara";
            this.btnYenidenTara.Click += new System.EventHandler(this.btnYenidenTara_Click);

            // btnCsvAktar
            this.btnCsvAktar.Location = new System.Drawing.Point(550, 20);
            this.btnCsvAktar.Size = new System.Drawing.Size(130, 35);
            this.btnCsvAktar.Text = "⬇️ CSV'ye Aktar";
            this.btnCsvAktar.Click += new System.EventHandler(this.btnCsvAktar_Click);

            // btnBaslat
            this.btnBaslat.Location = new System.Drawing.Point(690, 20);
            this.btnBaslat.Size = new System.Drawing.Size(100, 35);
            this.btnBaslat.Text = "▶️ Aktar";
            this.btnBaslat.Click += new System.EventHandler(this.btnBaslat_Click);

            // lblKaynak
            this.lblKaynak.Location = new System.Drawing.Point(20, 60);
            this.lblKaynak.Size = new System.Drawing.Size(600, 20);
            this.lblKaynak.Text = "📂 Kaynak: [Seçilmedi]";

            // lblCikis
            this.lblCikis.Location = new System.Drawing.Point(20, 80);
            this.lblCikis.Size = new System.Drawing.Size(600, 20);
            this.lblCikis.Text = "📁 Çıkış: [Seçilmedi]";

            // dgvOnizleme
            this.dgvOnizleme.Location = new System.Drawing.Point(20, 110);
            this.dgvOnizleme.Size = new System.Drawing.Size(500, 250);
            this.dgvOnizleme.SelectionChanged += new System.EventHandler(this.dgvOnizleme_SelectionChanged);

            // rtbHtmlOnizleme
            this.rtbHtmlOnizleme.Location = new System.Drawing.Point(530, 110);
            this.rtbHtmlOnizleme.Size = new System.Drawing.Size(380, 250);
            this.rtbHtmlOnizleme.ReadOnly = true;

            // lstLoglar
            this.lstLoglar.Location = new System.Drawing.Point(20, 370);
            this.lstLoglar.Size = new System.Drawing.Size(500, 100);

            // lstGecersizler
            this.lstGecersizler.Location = new System.Drawing.Point(530, 370);
            this.lstGecersizler.Size = new System.Drawing.Size(380, 100);

            // lblDurum
            this.lblDurum.Location = new System.Drawing.Point(20, 480);
            this.lblDurum.Size = new System.Drawing.Size(800, 30);
            this.lblDurum.Text = "";

            // Form1
            this.ClientSize = new System.Drawing.Size(940, 520);
            this.Controls.Add(this.btnKlasorSec);
            this.Controls.Add(this.btnCikisSec);
            this.Controls.Add(this.btnYenidenTara);
            this.Controls.Add(this.btnCsvAktar);
            this.Controls.Add(this.btnBaslat);
            this.Controls.Add(this.lblKaynak);
            this.Controls.Add(this.lblCikis);
            this.Controls.Add(this.dgvOnizleme);
            this.Controls.Add(this.rtbHtmlOnizleme);
            this.Controls.Add(this.lstLoglar);
            this.Controls.Add(this.lstGecersizler);
            this.Controls.Add(this.lblDurum);
            this.Text = "FaturaOkur 2001 - Gelişmiş Sürüm";

            ((System.ComponentModel.ISupportInitialize)(this.dgvOnizleme)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Button btnKlasorSec;
        private System.Windows.Forms.Button btnCikisSec;
        private System.Windows.Forms.Button btnBaslat;
        private System.Windows.Forms.Button btnYenidenTara;
        private System.Windows.Forms.Button btnCsvAktar;
        private System.Windows.Forms.Label lblKaynak;
        private System.Windows.Forms.Label lblCikis;
        private System.Windows.Forms.Label lblDurum;
        private System.Windows.Forms.DataGridView dgvOnizleme;
        private System.Windows.Forms.RichTextBox rtbHtmlOnizleme;
        private System.Windows.Forms.ListBox lstLoglar;
        private System.Windows.Forms.ListBox lstGecersizler;
    }
}
