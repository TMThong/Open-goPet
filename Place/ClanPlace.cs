/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Class.java to edit this template
 */
package place;

import data.map.GopetMap;
import server.Player;

/**
 *
 * @author MINH THONG
 */
public class ClanPlace extends GopetPlace {

    public ClanPlace(GopetMap m, int ID)   {
        super(m, ID);
        this.maxPlayer = Integer.MAX_VALUE;
    }

    @Override
    public void add(Player player)   {
        player.playerData.x = 326 + (24 * 3);
        player.playerData.y = 236;
        super.add(player); // Generated from nbfs://nbhost/SystemFileSystem/Templates/Classes/Code/OverriddenMethodBody
    }
}
