/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Class.java to edit this template
 */
package data.item;

import lombok.Getter;
import lombok.Setter;

/**
 *
 * @author MINH THONG
 */
@Getter
@Setter
public class ItemAttributeTemplate {

    private int[] listItemId;
    private String name;
    private int[][] buff;
    private int attrId;

    public int findValueById(int id) {
        for (int i = 0; i < buff.length; i++) {
            int[] buffIn4 = buff[i];
            if (buffIn4[0] == id) {
                return buffIn4[1];
            }
        }
        return 0;
    }
}
