using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Kule_savunma_oyunu
{
    public class TopKulesi : Kule, ISaldiriYapabilir, IMermiAtar

    {
        public TopKulesi(PointF konum) : base(konum)
        {
            Hasar = 50;
            AtisHizi = 70;   // yavaş
            Menzil = 90;
            Fiyat = 250;
        }
        public void Saldir(List<Dusman> dusmanlar)
        {
            AtesEt(dusmanlar);
        }
        public void MermiUret(List<Dusman> dusmanlar)
        {
            AtesEt(dusmanlar);
        }

        protected override void AtesEt(List<Dusman> dusmanlar)
        {
            var hedefler = Menzildekiler(dusmanlar);
            if (hedefler.Count == 0)
                return;

            // TEK HEDEF
            var hedef = hedefler[0];

            // MERMİ OLUŞTUR
            TopMermisiTetikle(Konum, hedef);
        }
    }
}

