/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Class.java to edit this template
 */
package data.map;

import data.clan.Clan;
import manager.ClanManager;
import place.Place;
import server.Player;

/**
 *
 * @author MINH THONG
 */
public class ClanMap extends GopetMap {

    public ClanMap(int mapId_, bool canUpdate, MapTemplate mapTemplate) {
        super(mapId_, canUpdate, mapTemplate);
    }

    @Override
    public void addPlace(Place place) {
        throw new UnsupportedOperationException("Place dua vao clan");
    }

    @Override
    public void addRandom(Player player)   {
        player.redDialog("Map này cần phải có bang hội mới vào được");
    }

    @Override
    public void update()   {
        try {
            for (Clan clan : ClanManager.clans) {
                clan.update();
            }
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    @Override
    public void createZoneDefault() {

    }
}
