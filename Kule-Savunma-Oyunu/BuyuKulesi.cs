using System.Linq;
using System.Drawing;
using System.Collections.Generic;

namespace Kule_savunma_oyunu
{
    public class BuyuKulesi : Kule, IAlanHasari

    {
        public BuyuKulesi(PointF konum) : base(konum)
        {
            Hasar = 20;
            AtisHizi = 50;
            Menzil = 100;
            Fiyat = 200;
        }
        public void AlanHasariVer(List<Dusman> dusmanlar)
        {
            AtesEt(dusmanlar);
        }

        protected override void AtesEt(List<Dusman> dusmanlar)
        {
            // 1️⃣ Menzildeki düşmanları al
            var hedefler = Menzildekiler(dusmanlar)
                // 2️⃣ Kuleye en yakın olanlar
                .OrderBy(d => Mesafe(d.Konum, Konum))
                // 3️⃣ En yakın 5 tanesi
                .Take(5)
                .ToList();

            if (hedefler.Count == 0)
                return;

            // 4️⃣ Hepsine hasar + ışın
            foreach (var d in hedefler)
            {
                d.HasarAl(Hasar);

                // 🔮 Her hedef için ışın
                BuyuIsiniTetikle(Konum, d.Konum);
            }
        }
    }
}
