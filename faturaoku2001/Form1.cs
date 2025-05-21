using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using SharpCompress.Archives;
using SharpCompress.Archives.Rar;
using SharpCompress.Archives.Zip;
using SharpCompress.Common;

namespace faturaoku2001
{
    public partial class Form1 : Form
    {
        private string kaynakKlasor = "";
        private string cikisKlasor = "";
        private List<HtmlDosyaBilgi> onizlemeListesi = new List<HtmlDosyaBilgi>();
        private List<string> gecersizDosyalar = new List<string>();

        public Form1()
        {
            InitializeComponent();
            ApplyDarkTheme();
        }

        private void btnKlasorSec_Click(object sender, EventArgs e)
        {
            using (var dialog = new FolderBrowserDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    kaynakKlasor = dialog.SelectedPath;
                    lblKaynak.Text = "📂 Kaynak: " + kaynakKlasor;
                    TaraKlasor();
                }
            }
        }

        private void btnCikisSec_Click(object sender, EventArgs e)
        {
            using (var dialog = new FolderBrowserDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    cikisKlasor = dialog.SelectedPath;
                    lblCikis.Text = "📁 Çıkış: " + cikisKlasor;
                }
            }
        }

        private void btnYenidenTara_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(kaynakKlasor))
            {
                MessageBox.Show("Önce bir kaynak klasör seçin.");
                return;
            }

            TaraKlasor();
        }

        private void btnBaslat_Click(object sender, EventArgs e)
        {
            if (onizlemeListesi.Count == 0)
            {
                MessageBox.Show("İşlenecek dosya yok.");
                return;
            }

            if (string.IsNullOrWhiteSpace(cikisKlasor))
            {
                MessageBox.Show("Lütfen çıkış klasörünü seçin.");
                return;
            }

            int basarili = 0;
            foreach (var item in onizlemeListesi)
            {
                try
                {
                    string hedef = Path.Combine(cikisKlasor, item.YeniDosyaAdi);
                    File.Copy(item.TempPath, hedef, true);
                    lstLoglar.Items.Add($"✔️ {item.YeniDosyaAdi} kopyalandı.");
                    basarili++;
                }
                catch (Exception ex)
                {
                    lstLoglar.Items.Add($"❌ {item.YeniDosyaAdi}: {ex.Message}");
                }
                finally
                {
                    File.Delete(item.TempPath);
                }
            }

            lblDurum.Text = $"✅ {basarili} dosya başarıyla aktarıldı.";
        }

        private void btnCsvAktar_Click(object sender, EventArgs e)
        {
            if (onizlemeListesi.Count == 0)
            {
                MessageBox.Show("Aktarılacak veri yok.");
                return;
            }

            using (var dialog = new SaveFileDialog())
            {
                dialog.Filter = "CSV Dosyası (*.csv)|*.csv";
                dialog.FileName = "FaturaOnizleme.csv";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using (var sw = new StreamWriter(dialog.FileName))
                        {
                            sw.WriteLine("KaynakArsiv,HtmlDosyasi,YeniDosyaAdi");
                            foreach (var item in onizlemeListesi)
                            {
                                sw.WriteLine($"{item.KaynakArsiv},{item.HtmlDosyasi},{item.YeniDosyaAdi}");
                            }
                        }

                        MessageBox.Show("CSV dosyası başarıyla kaydedildi.");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("CSV yazma hatası: " + ex.Message);
                    }
                }
            }
        }

        private void dgvOnizleme_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvOnizleme.CurrentRow?.DataBoundItem is HtmlDosyaBilgi secili)
            {
                if (File.Exists(secili.TempPath))
                    rtbHtmlOnizleme.Text = File.ReadAllText(secili.TempPath);
                else
                    rtbHtmlOnizleme.Text = "(Dosya bulunamadı)";
            }
        }

        private void TaraKlasor()
        {
            dgvOnizleme.DataSource = null;
            rtbHtmlOnizleme.Clear();
            lstLoglar.Items.Clear();
            lstGecersizler.Items.Clear();
            onizlemeListesi.Clear();
            gecersizDosyalar.Clear();

            if (!Directory.Exists(kaynakKlasor)) return;

            string[] zipler = Directory.GetFiles(kaynakKlasor, "*.zip");
            string[] rarlar = Directory.GetFiles(kaynakKlasor, "*.rar");

            foreach (var zip in zipler) try { IslemZip(zip); } catch { }
            foreach (var rar in rarlar) try { IslemRar(rar); } catch { }

            dgvOnizleme.DataSource = onizlemeListesi;
            lstGecersizler.Items.AddRange(gecersizDosyalar.ToArray());

            lblDurum.Text = $"🔍 {onizlemeListesi.Count} geçerli, {gecersizDosyalar.Count} geçersiz dosya bulundu.";
        }

        private void IslemZip(string path)
        {
            var archive = ZipArchive.Open(path);
            using (archive)
            {
                foreach (var entry in archive.Entries)
                {
                    if (!entry.IsDirectory && entry.Key.EndsWith(".html", StringComparison.OrdinalIgnoreCase))
                    {
                        string temp = Path.Combine(Application.StartupPath, "temp_" + Path.GetFileName(entry.Key));
                        entry.WriteToFile(temp, new ExtractionOptions { Overwrite = true });

                        string isim = IsimUret(temp);
                        if (isim != null)
                        {
                            onizlemeListesi.Add(new HtmlDosyaBilgi
                            {
                                HtmlDosyasi = Path.GetFileName(entry.Key),
                                KaynakArsiv = Path.GetFileName(path),
                                YeniDosyaAdi = isim,
                                TempPath = temp
                            });
                        }
                        else
                        {
                            gecersizDosyalar.Add(Path.GetFileName(path) + " > " + Path.GetFileName(entry.Key));
                            File.Delete(temp);
                        }
                    }
                }
            }
        }

        private void IslemRar(string path)
        {
            var archive = RarArchive.Open(path);
            using (archive)
            {
                foreach (var entry in archive.Entries)
                {
                    if (!entry.IsDirectory && entry.Key.EndsWith(".html", StringComparison.OrdinalIgnoreCase))
                    {
                        string temp = Path.Combine(Application.StartupPath, "temp_" + Path.GetFileName(entry.Key));
                        entry.WriteToFile(temp, new ExtractionOptions { Overwrite = true });

                        string isim = IsimUret(temp);
                        if (isim != null)
                        {
                            onizlemeListesi.Add(new HtmlDosyaBilgi
                            {
                                HtmlDosyasi = Path.GetFileName(entry.Key),
                                KaynakArsiv = Path.GetFileName(path),
                                YeniDosyaAdi = isim,
                                TempPath = temp
                            });
                        }
                        else
                        {
                            gecersizDosyalar.Add(Path.GetFileName(path) + " > " + Path.GetFileName(entry.Key));
                            File.Delete(temp);
                        }
                    }
                }
            }
        }

        private string IsimUret(string htmlYolu)
        {
            string icerik = File.ReadAllText(htmlYolu);
            string ara = "SAYIN</span></td></tr><tr><td align=\"left\" style=\"width:469px; \">";

            int basla = icerik.IndexOf(ara);
            if (basla == -1) return null;

            basla += ara.Length;
            int bit = icerik.IndexOf("</td>", basla);
            if (bit == -1) return null;

            string adSoyad = icerik.Substring(basla, bit - basla).Trim();
            adSoyad = System.Net.WebUtility.HtmlDecode(adSoyad);

            var parcalar = adSoyad.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (parcalar.Length < 2) return null;

            string ilk = parcalar[0].Substring(0, 1).ToUpper();
            string kalan = string.Join(" ", parcalar.Skip(1));
            return $"{ilk}.{kalan}.html".Replace(" ", "");
        }

        public class HtmlDosyaBilgi
        {
            public string KaynakArsiv { get; set; }
            public string HtmlDosyasi { get; set; }
            public string YeniDosyaAdi { get; set; }
            public string TempPath { get; set; }
        }

        private void ApplyDarkTheme()
        {
            this.BackColor = Color.FromArgb(44, 62, 80);
            this.ForeColor = Color.White;

            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is Button btn)
                {
                    btn.BackColor = Color.FromArgb(52, 73, 94);
                    btn.ForeColor = Color.White;
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.FlatAppearance.BorderSize = 0;
                }
                else if (ctrl is Label lbl)
                {
                    lbl.ForeColor = Color.White;
                }
                else if (ctrl is DataGridView dgv)
                {
                    dgv.BackgroundColor = Color.FromArgb(44, 62, 80);
                    dgv.DefaultCellStyle.BackColor = Color.FromArgb(44, 62, 80);
                    dgv.DefaultCellStyle.ForeColor = Color.White;
                    dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(52, 152, 219);
                    dgv.DefaultCellStyle.SelectionForeColor = Color.Black;
                    dgv.EnableHeadersVisualStyles = false;
                    dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(52, 73, 94);
                    dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                }
                else if (ctrl is RichTextBox rtb)
                {
                    rtb.BackColor = Color.FromArgb(52, 73, 94);
                    rtb.ForeColor = Color.White;
                }
                else if (ctrl is ListBox lb)
                {
                    lb.BackColor = Color.FromArgb(52, 73, 94);
                    lb.ForeColor = Color.White;
                }
            }
        }
    }
}
