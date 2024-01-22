/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Class.java to edit this template
 */
package data.top;

import server.Player;
import java.util.ArrayList;
import java.util.Comparator;
import java.util.HashMap;
import java.util.Map;
import java.util.concurrent.ConcurrentHashMap;

/**
 *
 * @author MINH THONG
 */
public class TopFightMob {

    private final ConcurrentHashMap<Player, Integer> damageHashMap = new ConcurrentHashMap<>();
    private int maxTop = 10;

    public int getMaxTop() {
        return maxTop;
    }

    public void setMaxTop(int maxTop) {
        this.maxTop = maxTop;
    }

    /**
     *
     * @param player
     * @param damage
     */
    public void addDamage(Player player, int damage) {

        if (!damageHashMap.containsKey(player)) {
            damageHashMap.put(player, damage);
            return;
        }
        damageHashMap.put(player, damageHashMap.get(player) + damage);

    }

    /**
     *
     * @return
     */
    public ArrayList<HashMap.Entry<Player, Integer>> getTop() {
        ArrayList<HashMap.Entry<Player, Integer>> sortList = new ArrayList<>(damageHashMap.entrySet());
        sortList.sort(new Comparator<Map.Entry<Player, Integer>>() {
            @Override
            public int compare(Map.Entry<Player, Integer> object1, Map.Entry<Player, Integer> object2) {
                return object2.getValue() - object1.getValue();
            }
        });
        if (sortList.size() > maxTop) {
            int listsize = sortList.size();
            for (int i = 0; i < listsize - maxTop; i++) {
                sortList.remove(listsize - (i + 1));
            }
        }
        return sortList;
    }
}
