 
public class MenuItemInfo {

    private String titleMenu, desc, imgPath, dialogText, leftCmdText, rightCmdText;
    private bool canSelect = false;
    private bool showDialog = false;
    private bool closeScreenAfterClick = false;
    private sbyte saleStatus = 0;
    private bool hasId = false;
    private int itemId;
    private PaymentOption[] paymentOptions = new PaymentOption[0];

    public MenuItemInfo(String titleMenu, String desc, String imgPath) {
        this.titleMenu = titleMenu;
        this.desc = desc;
        this.imgPath = imgPath;
        this.rightCmdText = string.Empty;
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

    public record PaymentOption(int paymentOptionsId, String moneyText, sbyte isPaymentEnable);
}
