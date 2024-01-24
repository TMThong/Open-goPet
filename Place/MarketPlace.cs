
using Gopet.Data.Map;

public class MarketPlace : GopetPlace {

    public static readonly Kiosk[] kiosks = new Kiosk[]{
        new Kiosk(GopetManager.KIOSK_HAT),
        new Kiosk(GopetManager.KIOSK_WEAPON),
        new Kiosk(GopetManager.KIOSK_AMOUR),
        new Kiosk(GopetManager.KIOSK_GEM),
        new Kiosk(GopetManager.KIOSK_PET),
        new Kiosk(GopetManager.KIOSK_OTHER)
    };

    public MarketPlace(GopetMap m, int ID)  : base(m, ID)
    {
        
        if (m.mapID != 22) {
            throw new UnsupportedOperationException("Map này méo phải map chợ trời");
        }
        maxPlayer = 50;
    }

     
    public override void update()   {
        base.update(); // Generated from nbfs://nbhost/SystemFileSystem/Templates/Classes/Code/OverriddenMethodBody
        for (int i = 0; i < kiosks.Length; i++) {
            Kiosk kiosk = kiosks[i];
            kiosk.update();
        }
    }

    public static Kiosk getKiosk(sbyte type) {
        foreach (Kiosk kiosk in kiosks) {
            if (kiosk.kioskType == type) {
                return kiosk;
            }
        }
        return null;
    }
}
