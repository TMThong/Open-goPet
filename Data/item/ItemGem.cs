/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Class.java to edit this template
 */
package data.item;

import base.DataVersion;
import lombok.Getter;
import lombok.Setter;
import manager.GopetManager;

/**
 *
 * @author MINH THONG
 */
@Getter
@Setter
public class ItemGem extends DataVersion  {

    private int element;
    private String name;
    private int[] option;
    private int[] optionValue;
    private int lvl = 0;
    private int itemTemplateId;
    private long timeUnequip = -1;

    public final String getElementIcon() {
        switch (element) {
            case GopetManager.FIRE_ELEMENT:
                return "(fire)";
            case GopetManager.WATER_ELEMENT:
                return "(water)";
            case GopetManager.ROCK_ELEMENT:
                return "(rock)";
            case GopetManager.THUNDER_ELEMENT:
                return "(thunder)";
            case GopetManager.TREE_ELEMENT:
                return "(tree)";
            case GopetManager.LIGHT_ELEMENT:
                return "(light)";
            case GopetManager.DARK_ELEMENT:
                return "(dark)";
        }
        return "";
    }
}
