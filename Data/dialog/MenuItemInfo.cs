 
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

    public void setTitleMenu(String titleMenu)
    {
        this.titleMenu = titleMenu;
    }

    public void setDesc(String desc)
    {
        this.desc = desc;
    }

    public void setImgPath(String imgPath)
    {
        this.imgPath = imgPath;
    }

    public void setDialogText(String dialogText)
    {
        this.dialogText = dialogText;
    }

    public void setLeftCmdText(String leftCmdText)
    {
        this.leftCmdText = leftCmdText;
    }

    public void setRightCmdText(String rightCmdText)
    {
        this.rightCmdText = rightCmdText;
    }

    public void setCanSelect(bool canSelect)
    {
        this.canSelect = canSelect;
    }

    public void setShowDialog(bool showDialog)
    {
        this.showDialog = showDialog;
    }

    public void setCloseScreenAfterClick(bool closeScreenAfterClick)
    {
        this.closeScreenAfterClick = closeScreenAfterClick;
    }

    public void setSaleStatus(sbyte saleStatus)
    {
        this.saleStatus = saleStatus;
    }

    public void setHasId(bool hasId)
    {
        this.hasId = hasId;
    }

    public void setItemId(int itemId)
    {
        this.itemId = itemId;
    }

    public void setPaymentOptions(PaymentOption[] paymentOptions)
    {
        this.paymentOptions = paymentOptions;
    }

    public String getTitleMenu()
    {
        return this.titleMenu;
    }

    public String getDesc()
    {
        return this.desc;
    }

    public String getImgPath()
    {
        return this.imgPath;
    }

    public String getDialogText()
    {
        return this.dialogText;
    }

    public String getLeftCmdText()
    {
        return this.leftCmdText;
    }

    public String getRightCmdText()
    {
        return this.rightCmdText;
    }

    public bool isCanSelect()
    {
        return this.canSelect;
    }

    public bool isShowDialog()
    {
        return this.showDialog;
    }

    public bool isCloseScreenAfterClick()
    {
        return this.closeScreenAfterClick;
    }

    public sbyte getSaleStatus()
    {
        return this.saleStatus;
    }

    public bool isHasId()
    {
        return this.hasId;
    }

    public int getItemId()
    {
        return this.itemId;
    }

    public PaymentOption[] getPaymentOptions()
    {
        return this.paymentOptions;
    }


    public class PaymentOption 
    {
        int paymentOptionsId; 
        String moneyText; 
        sbyte isPaymentEnable;

        public PaymentOption(int paymentOptionsId, string moneyText, sbyte isPaymentEnable)
        {
            this.paymentOptionsId = paymentOptionsId;
            this.moneyText = moneyText;
            this.isPaymentEnable = isPaymentEnable;
        }

        public void setPaymentOptionsId(int paymentOptionsId)
        {
            this.paymentOptionsId = paymentOptionsId;
        }

        public void setMoneyText(String moneyText)
        {
            this.moneyText = moneyText;
        }

        public void setIsPaymentEnable(sbyte isPaymentEnable)
        {
            this.isPaymentEnable = isPaymentEnable;
        }

        public int getPaymentOptionsId()
        {
            return this.paymentOptionsId;
        }

        public String getMoneyText()
        {
            return this.moneyText;
        }

        public sbyte getIsPaymentEnable()
        {
            return this.isPaymentEnable;
        }

    }
}
