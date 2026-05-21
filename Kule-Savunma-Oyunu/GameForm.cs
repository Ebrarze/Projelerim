using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Kule_savunma_oyunu
{
    public partial class GameForm : Form
    {
        Timer gameTimer;

        Yol yol;
        List<Dusman> dusmanlar = new List<Dusman>();
        List<Kule> kuleler = new List<Kule>();
        List<Explosion> patlamalar = new List<Explosion>();
        List<TopMermisi> topMermileri = new List<TopMermisi>();
        List<OkMermisi> okMermileri = new List<OkMermisi>();
        List<BuyuIsini> buyuIsinlari = new List<BuyuIsini>();


        int can = 15;
        int altin = 0;
        int dalga = 1;
        bool dalgaBasladi = false;
        int maxDalga = 5;
        int skor;

        Kule seciliKuleObjesi = null;
        // =========================
        //  EKONOMİ + YERLEŞTİRME
        // =========================
        int baslangicAltin = 300;

        // Paint ile birebir aynı olmalı
        float scale = 1.3f;
        float yolCizgiKalinligi_EkranPx = 25f; // Paint'teki Pen kalınlığı

        enum KuleTipi
        {
            Ok,
            Top,
            Buyu
        }

        KuleTipi seciliKule = KuleTipi.Ok;

        IYukseltilebilir okKulesi;
        IYukseltilebilir topKulesi;
        IYukseltilebilir buyuKulesi;
        public GameForm()
        {
            InitializeComponent();

            // 🔘 Tüm yükselt butonlarını TEK event'e bağla
            btnYukseltOk.Click += btnYukselt_Click;
            btnYukseltTop.Click += btnYukselt_Click;
            btnYukseltBuyu.Click += btnYukselt_Click;


            panelGame.Width = 1000;
            panelGame.Height = 500;
            panelGame.BackgroundImage = Properties.Resources.stone_tile;
            panelGame.BackgroundImageLayout = ImageLayout.Tile;

            EnableDoubleBuffer(panelGame);
            this.DoubleBuffered = true;

            gameTimer = new Timer();
            gameTimer.Interval = 30;
            gameTimer.Tick += GameTimer_Tick;

            yol = new Yol();

            // BAŞLANGIÇ ALTINI
            altin = baslangicAltin;

            // TIKLAMA İLE KULE EKLEME (BUNU MUTLAKA BURADA BAĞLA)
            panelGame.MouseClick += PanelGame_MouseClick;

            // Patlama çizimi
            Kule.PatlamaOlustu += (konum, yaricap) =>
            {
                patlamalar.Add(new Explosion(konum, yaricap, 8));
            };

            Kule.TopMermisiOlustu += (bas, hedef) =>
            {
                topMermileri.Add(new TopMermisi(bas, hedef));
            };
            Kule.OkMermisiOlustu += (bas, hedef) =>
            {
                okMermileri.Add(new OkMermisi(bas, hedef));
            };
            Kule.BuyuIsiniOlustu += (bas, bitis) =>
            {
                buyuIsinlari.Add(new BuyuIsini(bas, bitis));
            };


            DalgaBaslat();
            gameTimer.Start();

            panelGame.Paint += PanelGame_Paint;
            BilgileriGuncelle();
            SecimiGoster();

        }
        private void btnYukselt_Click(object sender, EventArgs e)
        {
            if (seciliKuleObjesi == null)
            {
                MessageBox.Show("Önce bir kule seçmelisin!");
                return;
            }

            if (altin < seciliKuleObjesi.YukseltmeBedeli)
            {
                MessageBox.Show("Yeterli altın yok!");
                return;
            }

            altin -= seciliKuleObjesi.YukseltmeBedeli;
            seciliKuleObjesi.Yukselt();

            BilgileriGuncelle();
            panelGame.Invalidate();
        }

        private void DalgaBaslat()
        {
            dalgaBasladi = true;
            int dusmanSayisi = 5 + dalga * dalga;
            float aralik = 60 + dalga * 10;
            PointF bas = yol.Noktalar[0];

            for (int i = 0; i < dusmanSayisi; i++)
            {
                dusmanlar.Add(new Dusman(new PointF(bas.X - i * aralik, bas.Y), dalga));
            }
        }
        private int DalgaOdulAltini()
        {
            switch (dalga)
            {
                case 1: return 100;
                case 2: return 150;
                case 3: return 200;
                case 4: return 250;
                case 5: return 300;
                default: return 300; // 5'ten sonrası için
            }
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            for (int i = okMermileri.Count - 1; i >= 0; i--)
            {
                var ok = okMermileri[i];
                ok.Guncelle();

                if (ok.Vurdu)
                {
                    if (ok.Hedef != null && !ok.Hedef.Oldu)
                        ok.Hedef.HasarAl(15); // ok hasarı

                    okMermileri.RemoveAt(i);
                }
            }

            float patlamaYaricapi = 80f;
            int patlamaHasari = 40;

            for (int i = topMermileri.Count - 1; i >= 0; i--)
            {
                var mermi = topMermileri[i];
                mermi.Guncelle();

                if (mermi.Vurdu)
                {
                    // 🔥 ALAN HASARI
                    foreach (var d in dusmanlar)
                    {
                        if (d.Oldu) continue;

                        float dx = d.Konum.X - mermi.Konum.X;
                        float dy = d.Konum.Y - mermi.Konum.Y;
                        float mesafe = (float)Math.Sqrt(dx * dx + dy * dy);

                        if (mesafe <= patlamaYaricapi)
                        {
                            d.HasarAl(patlamaHasari);
                        }
                    }

                    // 💥 PATLAMA EFEKTİ
                    Kule.PatlamaTetikle(mermi.Konum, patlamaYaricapi);

                    topMermileri.RemoveAt(i);
                }
            }
            for (int i = buyuIsinlari.Count - 1; i >= 0; i--)
            {
                buyuIsinlari[i].KalanSure--;
                if (buyuIsinlari[i].KalanSure <= 0)
                    buyuIsinlari.RemoveAt(i);
            }

            foreach (var d in dusmanlar)
                d.Ilerle(yol.Noktalar);

            foreach (var kule in kuleler)
                kule.Guncelle(dusmanlar);


            // Sona ulaşan düşmanlar -> can azalt
            bool canAzaldi = false;
            for (int i = dusmanlar.Count - 1; i >= 0; i--)
            {
                if (dusmanlar[i].HedefIndex >= yol.Noktalar.Count)
                {
                    if (!canAzaldi && can > 0)
                    {
                        can--;
                        canAzaldi = true;
                    }
                    dusmanlar.RemoveAt(i);
                }
            }
            // Patlamalar süre
            for (int i = patlamalar.Count - 1; i >= 0; i--)
            {
                patlamalar[i].KalanSure--;
                if (patlamalar[i].KalanSure <= 0)
                    patlamalar.RemoveAt(i);
            }

            // ÖLEN DÜŞMANLAR -> DALGAYA GÖRE ALTIN KAZAN
            for (int i = dusmanlar.Count - 1; i >= 0; i--)
            {
                if (dusmanlar[i].Oldu)
                {
                    if (dusmanlar[i] is IParaKazandirir para)
                    {
                        altin += para.AltinVer();
                    }

                    skor += 100 * dalga; // dalgaya göre skor

                    dusmanlar.RemoveAt(i);
                }
            }
            BilgileriGuncelle();

            if (dusmanlar.Count == 0 && dalgaBasladi)
            {
                dalgaBasladi = false;
                altin += DalgaOdulAltini();
                dalga++;

                if (dalga > maxDalga)
                {
                    gameTimer.Stop();
                    MessageBox.Show(
                        "Tebrikler! Oyunu kazandın 🎉",
                        "Oyun Bitti",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    return;
                }
                if (can <= 0)
                {
                    gameTimer.Stop();
                    MessageBox.Show(
                        "Kaybettin! Oyun bitti.",
                        "Oyun Bitti",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                    return;
                }

                DalgaBaslat();
            }

            panelGame.Invalidate();
        }
        private void YukseltButonlariniGuncelle()
        {
            // Varsayılan: hepsi pasif
            btnYukseltOk.Enabled = false;
            btnYukseltTop.Enabled = false;
            btnYukseltBuyu.Enabled = false;

            if (seciliKuleObjesi == null)
                return;

            // Seçili kule türüne göre SADECE ilgili buton aktif
            if (seciliKuleObjesi is OkKulesi)
                btnYukseltOk.Enabled = true;
            else if (seciliKuleObjesi is TopKulesi)
                btnYukseltTop.Enabled = true;
            else if (seciliKuleObjesi is BuyuKulesi)
                btnYukseltBuyu.Enabled = true;
        }

        // =========================
        //  TIKLAMA İLE KULE EKLEME
        // =========================
        private void PanelGame_MouseClick(object sender, MouseEventArgs e)
        {
            PointF worldPos = ScreenToWorld(e.Location);

            if (YolUzerindeMi(worldPos))
                return;

            seciliKuleObjesi = null;
            YukseltButonlariniGuncelle();

            // 🟡 ÖNCE KULE SEÇMEYİ DENE
            foreach (var kule in kuleler)
            {
                float dx = kule.Konum.X - worldPos.X;
                float dy = kule.Konum.Y - worldPos.Y;

                if (Math.Sqrt(dx * dx + dy * dy) <= 15)
                {
                    seciliKuleObjesi = kule;
                    YukseltButonlariniGuncelle();
                    panelGame.Invalidate();
                    return; // ⛔ yeni kule ekleme
                }
            }

            Kule yeniKule = null;

            switch (seciliKule)
            {
                case KuleTipi.Ok:
                    yeniKule = new OkKulesi(worldPos);
                    break;

                case KuleTipi.Top:
                    yeniKule = new TopKulesi(worldPos);
                    break;

                case KuleTipi.Buyu:
                    yeniKule = new BuyuKulesi(worldPos);
                    break;
            }

            // ⛔ KULE SAYISI SINIRI (EN ÖNCE!)
            if (kuleler.Count >= MaksimumKuleSayisi())
            {
                MessageBox.Show(
                    $"Bu dalgada en fazla {MaksimumKuleSayisi()} kule kurabilirsin!",
                    "Kule Limiti",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            // 💰 SATIN ALMA KONTROLÜ
            if (yeniKule != null)
            {
                if (altin < yeniKule.Fiyat)
                {
                    MessageBox.Show(
                        "Yeterli altının yok!",
                        "Uyarı",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }

                // ✅ TÜM KURALLAR SAĞLANDI → EKLE
                altin -= yeniKule.Fiyat;
                kuleler.Add(yeniKule);
            }
            // 🟡 KULE SEÇME
            foreach (var kule in kuleler)
            {
                float dx = kule.Konum.X - worldPos.X;
                float dy = kule.Konum.Y - worldPos.Y;

                if (Math.Sqrt(dx * dx + dy * dy) < 15)
                {
                    seciliKuleObjesi = kule;
                    YukseltButonlariniGuncelle();
                    panelGame.Invalidate();
                    return;
                }
            }


            BilgileriGuncelle();
            panelGame.Invalidate();
        }

        // =========================
        //  KOORDİNAT DÖNÜŞÜMLERİ
        // =========================
        private PointF ScreenToWorld(Point p)
        {
            float minX = yol.Noktalar.Min(pt => pt.X);
            float maxX = yol.Noktalar.Max(pt => pt.X);
            float minY = yol.Noktalar.Min(pt => pt.Y);
            float maxY = yol.Noktalar.Max(pt => pt.Y);

            float yolGenislik = (maxX - minX) * scale;
            float yolYukseklik = (maxY - minY) * scale;

            float offsetX = (panelGame.Width - yolGenislik) / 2f;
            float offsetY = (panelGame.Height - yolYukseklik) / 2f;

            return new PointF(
                (p.X - offsetX) / scale + minX,
                (p.Y - offsetY) / scale + minY
            );
        }

        private bool YolUzerindeMi(PointF worldP)
        {
            // Paint'te çizgi kalınlığı ekran px. World'de karşılığı: (px / scale)
            float worldThreshold = (yolCizgiKalinligi_EkranPx / 2f) / scale;

            for (int i = 0; i < yol.Noktalar.Count - 1; i++)
            {
                float dist = PointLineDistance(yol.Noktalar[i], yol.Noktalar[i + 1], worldP);
                if (dist <= worldThreshold)
                    return true;
            }
            return false;
        }

        private float PointLineDistance(PointF a, PointF b, PointF p)
        {
            float C = b.X - a.X;
            float D = b.Y - a.Y;
            float lenSq = C * C + D * D;

            // Aynı nokta ise (0 uzunluk) direkt mesafe
            if (lenSq <= 0.000001f)
            {
                float dx0 = p.X - a.X;
                float dy0 = p.Y - a.Y;
                return (float)Math.Sqrt(dx0 * dx0 + dy0 * dy0);
            }

            float A = p.X - a.X;
            float B = p.Y - a.Y;
            float dot = A * C + B * D;
            float param = dot / lenSq;

            float xx, yy;
            if (param < 0) { xx = a.X; yy = a.Y; }
            else if (param > 1) { xx = b.X; yy = b.Y; }
            else { xx = a.X + param * C; yy = a.Y + param * D; }

            float dx = p.X - xx;
            float dy = p.Y - yy;
            return (float)Math.Sqrt(dx * dx + dy * dy);
        }
        private int MaksimumKuleSayisi()
        {
            if (dalga <= 2)
                return 3;
            else if (dalga <= 4)
                return 5;
            else
                return 7;
        }

        private void PanelGame_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            // Yol sınırları (orijinal)
            float minX = yol.Noktalar.Min(p => p.X);
            float maxX = yol.Noktalar.Max(p => p.X);
            float minY = yol.Noktalar.Min(p => p.Y);
            float maxY = yol.Noktalar.Max(p => p.Y);

            // Ölçeklenmiş yol boyutu
            float yolGenislik = (maxX - minX) * scale;
            float yolYukseklik = (maxY - minY) * scale;

            // PANEL MERKEZİNE GÖRE OFFSET
            float offsetX = (panelGame.Width - yolGenislik) / 2f;
            float offsetY = (panelGame.Height - yolYukseklik) / 2f;

            PointF ToScreen(PointF p)
            {
                return new PointF(
                    (p.X - minX) * scale + offsetX,
                    (p.Y - minY) * scale + offsetY
                );
            }

            // Yol çizimi
            using (Pen yolPen = new Pen(Color.DarkOliveGreen, yolCizgiKalinligi_EkranPx))
            {
                yolPen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
                yolPen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
                yolPen.LineJoin = System.Drawing.Drawing2D.LineJoin.Round;

                for (int i = 0; i < yol.Noktalar.Count - 1; i++)
                {
                    PointF p1 = ToScreen(yol.Noktalar[i]);
                    PointF p2 = ToScreen(yol.Noktalar[i + 1]);
                    g.DrawLine(yolPen, p1, p2);
                }
            }
            // Kule çizimi (TÜRE GÖRE)
            foreach (var kule in kuleler)
            {
                PointF kuleEkran = new PointF(
                    (kule.Konum.X - minX) * scale + offsetX,
                    (kule.Konum.Y - minY) * scale + offsetY
                );

                if (kule is OkKulesi)
                {
                    // ÜÇGEN – OK KULESİ
                    PointF[] ucgen =
                    {
            new PointF(kuleEkran.X, kuleEkran.Y - 14),
            new PointF(kuleEkran.X - 12, kuleEkran.Y + 10),
            new PointF(kuleEkran.X + 12, kuleEkran.Y + 10)
        };

                    g.FillPolygon(Brushes.Green, ucgen);
                    g.DrawPolygon(Pens.Black, ucgen);
                }
                else if (kule is TopKulesi)
                {
                    // KARE – TOP KULESİ
                    g.FillRectangle(
                        Brushes.DarkRed,
                        kuleEkran.X - 14,
                        kuleEkran.Y - 14,
                        28,
                        28
                    );
                    g.DrawRectangle(
                        Pens.Black,
                        kuleEkran.X - 14,
                        kuleEkran.Y - 14,
                        28,
                        28);
                    g.DrawEllipse(
                        Pens.Black,
                        kuleEkran.X - 14,
                        kuleEkran.Y - 14,
                        28,
                        28
                    );

                    g.DrawEllipse(
                        Pens.White,
                        kuleEkran.X - 6,
                        kuleEkran.Y - 6,
                        12,
                        12
                    );
                }
                else if (kule is BuyuKulesi)
                {
                    // DAİRE + İÇ HALKA – BÜYÜ KULESİ
                    g.FillEllipse(
                        Brushes.MediumPurple,
                        kuleEkran.X - 14,
                        kuleEkran.Y - 14,
                        28,
                        28
                    );

                    g.DrawEllipse(
                        Pens.Black,
                        kuleEkran.X - 14,
                        kuleEkran.Y - 14,
                        28,
                        28
                    );

                    // iç halka (büyü efekti)
                    g.DrawEllipse(
                        Pens.White,
                        kuleEkran.X - 6,
                        kuleEkran.Y - 6,
                        12,
                        12
                    );
                }
                // MENZİL (ORTAK)
                using (Pen menzilPen = new Pen(Color.FromArgb(70, Color.CornflowerBlue)))
                {
                    g.DrawEllipse(
                        menzilPen,
                        kuleEkran.X - kule.Menzil * scale,
                        kuleEkran.Y - kule.Menzil * scale,
                        kule.Menzil * 2 * scale,
                        kule.Menzil * 2 * scale
                    );
                }

                // ✅ SEÇİLİ KULE VURGUSU (DOĞRU YER)
                if (kule == seciliKuleObjesi)
                {
                    using (Pen seciliPen = new Pen(Color.Gold, 3))
                    {
                        seciliPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;

                        g.DrawEllipse(
                            seciliPen,
                            kuleEkran.X - (kule.Menzil * scale + 6),
                            kuleEkran.Y - (kule.Menzil * scale + 6),
                            (kule.Menzil * 2 * scale + 12),
                            (kule.Menzil * 2 * scale + 12)
                        );
                    }
                }
            }

            // TOP MERMİLERİ ÇİZİMİ
            foreach (var m in topMermileri)
            {
                PointF p = ToScreen(m.Konum);

                g.FillEllipse(
                    Brushes.OrangeRed,
                    p.X - 4,
                    p.Y - 4,
                    8,
                    8
                );
            }


            foreach (var ok in okMermileri)
            {
                PointF p = ToScreen(ok.Konum);

                using (Pen okPen = new Pen(Color.SaddleBrown, 3))
                {
                    g.DrawLine(
                        okPen,
                        p.X - 10, p.Y,
                        p.X + 10, p.Y
                    );
                }

                // Ok ucu
                g.FillEllipse(Brushes.Black, p.X + 8, p.Y - 3, 6, 6);
            }
            foreach (var isin in buyuIsinlari)
            {
                PointF isinBas = ToScreen(isin.Baslangic);
                PointF isinBit = ToScreen(isin.Bitis);


                using (Pen pen = new Pen(Color.MediumPurple, 4))
                {
                    pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
                    pen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
                    g.DrawLine(pen, isinBas, isinBit);
                }
            }


            // START/END
            PointF bas = ToScreen(yol.Baslangic);
            PointF bit = ToScreen(yol.Bitis);

            g.FillEllipse(Brushes.LimeGreen, bas.X - 12, bas.Y - 12, 24, 24);
            g.DrawString("START", this.Font, Brushes.White, bas.X - 22, bas.Y - 30);

            g.FillEllipse(Brushes.Red, bit.X - 12, bit.Y - 12, 24, 24);
            g.DrawString("END", this.Font, Brushes.White, bit.X - 15, bit.Y - 30);

            // Düşman çizimi
            foreach (var d in dusmanlar)
            {
                PointF k = ToScreen(d.Konum);

                PointF[] elmas =
                {
                    new PointF(k.X,      k.Y - 12),
                    new PointF(k.X + 12, k.Y),
                    new PointF(k.X,      k.Y + 12),
                    new PointF(k.X - 12, k.Y)
                };

                g.FillPolygon(Brushes.DeepSkyBlue, elmas);
                g.DrawPolygon(Pens.White, elmas);

                // HP BAR
                float barW = 28f, barH = 5f;
                float barX = k.X - barW / 2f;
                float barY = k.Y - 22f;

                g.FillRectangle(Brushes.DarkRed, barX, barY, barW, barH);

                float oran = 1f;
                if (d.MaxCan > 0) oran = (float)d.Can / d.MaxCan;
                if (oran < 0) oran = 0;

                g.FillRectangle(Brushes.LimeGreen, barX, barY, barW * oran, barH);
                g.DrawRectangle(Pens.Black, barX, barY, barW, barH);

                // Gözler
                g.FillEllipse(Brushes.White, k.X - 7, k.Y - 4, 6, 6);
                g.FillEllipse(Brushes.Black, k.X - 5, k.Y - 2, 2, 2);

                g.FillEllipse(Brushes.White, k.X + 1, k.Y - 4, 6, 6);
                g.FillEllipse(Brushes.Black, k.X + 3, k.Y - 2, 2, 2);
            }

            // Patlamalar
            foreach (var p in patlamalar)
            {
                PointF ekran = ToScreen(p.Konum);
                using (Pen pen = new Pen(Color.FromArgb(120, Color.OrangeRed), 4))
                {
                    g.DrawEllipse(
                        pen,
                        ekran.X - p.YariCap * scale,
                        ekran.Y - p.YariCap * scale,
                        p.YariCap * 2 * scale,
                        p.YariCap * 2 * scale
                    );
                }
            }
        }

        private void EnableDoubleBuffer(Control control)
        {
            typeof(Control)
                .GetProperty("DoubleBuffered",
                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                ?.SetValue(control, true, null);
        }
        private void SecimiGoster()
        {
            btnOk.BackColor = seciliKule == KuleTipi.Ok ? Color.LightGreen : SystemColors.Control;
            btnTop.BackColor = seciliKule == KuleTipi.Top ? Color.LightCoral : SystemColors.Control;
            btnBuyu.BackColor = seciliKule == KuleTipi.Buyu ? Color.Plum : SystemColors.Control;
        }

        private void BilgileriGuncelle()
        {
            lblCan.Text = "Can: " + can;
            lblAltin.Text = "Altın: " + altin;
            lblDalga.Text = "Dalga: " + dalga;
            lblSkor.Text = "Skor: " + skor;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            seciliKule = KuleTipi.Ok;
            SecimiGoster();
        }

        private void btnTop_Click(object sender, EventArgs e)
        {
            seciliKule = KuleTipi.Top;
            SecimiGoster();
        }

        private void btnBuyu_Click(object sender, EventArgs e)
        {
            seciliKule = KuleTipi.Buyu;
            SecimiGoster();
        }

        private void panelHUD_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}

