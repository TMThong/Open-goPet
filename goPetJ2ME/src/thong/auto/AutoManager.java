/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package thong.auto;

import java.util.Vector;

/**
 *
 * @author TMThong
 */
public class AutoManager {

    private static Vector autos = new Vector();

    public static boolean isAutoAttack = false;
    public static final int MAX_SKILL_COOLDOWN = 3;

    public static void update() {
        for (int i = 0; i < autos.size(); i++) {
            IAutoBase get = (IAutoBase) autos.elementAt(i);
            if (get.isEnable()) {
                get.update();
            }
        }
    }

    public static void init() {
        autos.removeAllElements();
        autos.addElement(AutoAttack.instance);
        autos.addElement(AutoDectectSpeed.instance);
    }
}
