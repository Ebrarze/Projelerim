using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Kule_savunma_oyunu
{
    public class OkKulesi : Kule, ISaldiriYapabilir

    {
        public OkKulesi(PointF konum) : base(konum)
        {
            Hasar = 15;
            AtisHizi = 40;
            Menzil = 80;
            Fiyat = 100;
        }
        public void Saldir(List<Dusman> dusmanlar)
        {
            AtesEt(dusmanlar);
        }

        protected override void AtesEt(List<Dusman> dusmanlar)
        {
            var hedefler = Menzildekiler(dusmanlar);
            if (hedefler.Count == 0) return;

            var hedef = hedefler[0];

            OkMermisiTetikle(Konum, hedef);
        }

    }
}
