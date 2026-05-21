using System.Drawing;
using System.Linq;
using System.Collections.Generic;

namespace Kule_savunma_oyunu
{
    public class BasitKule : Kule
    {
        public BasitKule(PointF konum) : base(konum)
        {
            Menzil = 120;
            Hasar = 1;
            AtisHizi = 30; // kaç tick'te bir
        }

        // ✅ Kule.cs'teki abstract metodu uyguluyoruz
        protected override void AtesEt(List<Dusman> dusmanlar)
        {
            // menzildekilerden en yakını seç
            var hedef = Menzildekiler(dusmanlar)
                .OrderBy(d => Mesafe(d.Konum, Konum))
                .FirstOrDefault();

            if (hedef != null)
                hedef.HasarAl(Hasar);
        }
    }
}
