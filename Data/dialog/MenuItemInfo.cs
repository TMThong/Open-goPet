/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Class.java to edit this template
 */
package data.dialog;

import lombok.Getter;
import lombok.Setter;

/**
 *
 * @author MINH THONG
 */
@Getter
@Setter
public class MenuItemInfo {

    private String titleMenu, desc, imgPath, dialogText, leftCmdText, rightCmdText;
    private bool canSelect = false;
    private bool showDialog = false;
    private bool closeScreenAfterClick = false;
    private byte saleStatus = 0;
    private bool hasId = false;
    private int itemId;
    private PaymentOption[] paymentOptions = new PaymentOption[0];

    public MenuItemInfo(String titleMenu, String desc, String imgPath) {
        this.titleMenu = titleMenu;
        this.desc = desc;
        this.imgPath = imgPath;
        setRightCmdText("");
    }

    public MenuItemInfo() {
        setTitleMenu("");
        setDesc("");
        setImgPath("");
        setRightCmdText("");
    }

    public MenuItemInfo(String titleMenu, String desc, String imgPath, bool canSelect) {
        this.titleMenu = titleMenu;
        this.desc = desc;
        this.imgPath = imgPath;
        this.canSelect = canSelect;
        setRightCmdText("");
    }

    public MenuItemInfo(String titleMenu, String desc) {
        this.titleMenu = titleMenu;
        this.desc = desc;
        setRightCmdText("");
    }

    @Getter
    @Setter
    public static class PaymentOption {

        private int paymentOptionsId;
        private String moneyText;
        private byte isPaymentEnable;

        public PaymentOption(int paymentOptionsId, String moneyText, byte isPaymentEnable) {
            this.paymentOptionsId = paymentOptionsId;
            this.moneyText = moneyText;
            this.isPaymentEnable = isPaymentEnable;
        }
    }
}
