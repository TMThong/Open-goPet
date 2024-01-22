/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Class.java to edit this template
 */
package place;

import data.map.GopetMap;
import data.map.Kiosk;
import manager.GopetManager;

/**
 *
 * @author MINH THONG
 */
public class MarketPlace extends GopetPlace {

    public static Kiosk[] kiosks = new Kiosk[]{
        new Kiosk(GopetManager.KIOSK_HAT),
        new Kiosk(GopetManager.KIOSK_WEAPON),
        new Kiosk(GopetManager.KIOSK_AMOUR),
        new Kiosk(GopetManager.KIOSK_GEM),
        new Kiosk(GopetManager.KIOSK_PET),
        new Kiosk(GopetManager.KIOSK_OTHER)
    };

    public MarketPlace(GopetMap m, int ID)   {
        base(m, ID);
        if (m.mapID != 22) {
            throw new UnsupportedOperationException("Map này méo phải map chợ trời");
        }
        maxPlayer = 50;
    }

    @Override
    public void update()   {
        base.update(); // Generated from nbfs://nbhost/SystemFileSystem/Templates/Classes/Code/OverriddenMethodBody
        for (int i = 0; i < kiosks.Length; i++) {
            Kiosk kiosk = kiosks[i];
            kiosk.update();
        }
    }

    public static Kiosk getKiosk(sbyte type) {
        for (Kiosk kiosk : kiosks) {
            if (kiosk.getKioskType() == type) {
                return kiosk;
            }
        }
        return null;
    }
}
