using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Kule_savunma_oyunu
{
    public abstract class Kule : IAlinabilir, IYukseltilebilir

    {
        public static event Action<PointF, float> PatlamaOlustu;
        public static void PatlamaTetikle(PointF konum, float yariCap)
        {
            PatlamaOlustu?.Invoke(konum, yariCap);
        }
        public PointF Konum { get; protected set; }
        public float Menzil { get; protected set; }
        public int Hasar { get; protected set; }
        public int AtisHizi { get; protected set; } // kaç tick'te bir

        private int sayac = 0;
        public int Fiyat { get; protected set; }
        public int Seviye { get; protected set; } = 1;

        public static event Action<PointF, PointF, Color> AtesOlustu;

        public virtual int YukseltmeBedeli => Seviye * 50;
        protected static void AtesTetikle(PointF bas, PointF bit, Color renk)
        {
            AtesOlustu?.Invoke(bas, bit, renk);
        }

        public static event Action<PointF, Dusman> TopMermisiOlustu;

        protected static void TopMermisiTetikle(PointF baslangic, Dusman hedef)
        {
            TopMermisiOlustu?.Invoke(baslangic, hedef);
        }
        public static event Action<PointF, Dusman> OkMermisiOlustu;

        protected static void OkMermisiTetikle(PointF bas, Dusman hedef)
        {
            OkMermisiOlustu?.Invoke(bas, hedef);
        }
        public static event Action<PointF, PointF> BuyuIsiniOlustu;

        protected static void BuyuIsiniTetikle(PointF bas, PointF bitis)
        {
            BuyuIsiniOlustu?.Invoke(bas, bitis);
        }

        protected Kule(PointF konum)
        {
            Konum = konum;
        }

        // Her tick çağrılır
        public void Guncelle(List<Dusman> dusmanlar)
        {
            sayac++;
            if (sayac < AtisHizi)
                return;

            // Ateş etme kararını kule türü verir
            AtesEt(dusmanlar);

            sayac = 0;
        }

        // Her kule türü kendi saldırı tipini belirler
        protected abstract void AtesEt(List<Dusman> dusmanlar);

        // Menzilde olan düşmanları listeler
        protected List<Dusman> Menzildekiler(List<Dusman> dusmanlar)
        {
            return dusmanlar
                .Where(d => !d.Oldu && Mesafe(d.Konum, Konum) <= Menzil)
                .ToList();
        }
        public virtual void Yukselt()
        {
            Seviye++;

            // Temel artışlar (tüm kuleler için ortak)
            Hasar = (int)(Hasar * 1.25f);
            Menzil *= 1.1f;
        }

        // İki nokta arası mesafe
        protected float Mesafe(PointF a, PointF b)
        {
            float dx = a.X - b.X;
            float dy = a.Y - b.Y;
            return (float)Math.Sqrt(dx * dx + dy * dy);
        }
    }
}
