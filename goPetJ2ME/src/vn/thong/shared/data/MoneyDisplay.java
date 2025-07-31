/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package vn.thong.shared.data;

import defpackage.Ulti;
import javax.microedition.lcdui.Image;

/**
 *
 * @author Admin
 */
public class MoneyDisplay {

    public Image image;
    public final String imgPath;
    public final long value;
    public String valueStr;

    public MoneyDisplay(String imgPath, long value) {
        this.imgPath = imgPath;
        this.value = value;
        this.valueStr = Ulti.formatNumber(value);
    }
}
