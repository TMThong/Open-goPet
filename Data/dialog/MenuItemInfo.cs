namespace Gopet.Data.Dialog
{
    public class MenuItemInfo
    {
        private string titleMenu, desc, imgPath, dialogText, leftCmdText, rightCmdText;
        private bool canSelect = false;
        private bool showDialog = false;
        private bool closeScreenAfterClick = false;
        private sbyte saleStatus = 0;
        private bool hasId = false;
        private int itemId;
        private PaymentOption[] paymentOptions = new PaymentOption[0];

        public MenuItemInfo(string titleMenu, string desc, string imgPath)
        {
            this.titleMenu = titleMenu;
            this.desc = desc;
            this.imgPath = imgPath;
            rightCmdText = string.Empty;
        }

        public MenuItemInfo()
        {
            setTitleMenu("");
            setDesc("");
            setImgPath("");
            setRightCmdText("");
            setLeftCmdText("");
        }

        public MenuItemInfo(string titleMenu, string desc, string imgPath, bool canSelect)
        {
            this.titleMenu = titleMenu;
            this.desc = desc;
            this.imgPath = imgPath;
            this.canSelect = canSelect;
            setRightCmdText("");
        }

        public MenuItemInfo(string titleMenu, string desc)
        {
            this.titleMenu = titleMenu;
            this.desc = desc;
            setRightCmdText("");
        }

        public void setTitleMenu(string titleMenu)
        {
            this.titleMenu = titleMenu;
        }

        public void setDesc(string desc)
        {
            this.desc = desc;
        }

        public void setImgPath(string imgPath)
        {
            this.imgPath = imgPath;
        }

        public void setDialogText(string dialogText)
        {
            this.dialogText = dialogText;
        }

        public void setLeftCmdText(string leftCmdText)
        {
            this.leftCmdText = leftCmdText;
        }

        public void setRightCmdText(string rightCmdText)
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

        public string getTitleMenu()
        {
            return titleMenu;
        }

        public string getDesc()
        {
            return desc;
        }

        public string getImgPath()
        {
            return imgPath;
        }

        public string getDialogText()
        {
            return dialogText;
        }

        public string getLeftCmdText()
        {
            return leftCmdText;
        }

        public string getRightCmdText()
        {
            return rightCmdText;
        }

        public bool isCanSelect()
        {
            return canSelect;
        }

        public bool isShowDialog()
        {
            return showDialog;
        }

        public bool isCloseScreenAfterClick()
        {
            return closeScreenAfterClick;
        }

        public sbyte getSaleStatus()
        {
            return saleStatus;
        }

        public bool isHasId()
        {
            return hasId;
        }

        public int getItemId()
        {
            return itemId;
        }

        public PaymentOption[] getPaymentOptions()
        {
            return paymentOptions;
        }

        public class PaymentOption
        {
            public int paymentOptionsId;
            public string moneyText;
            public sbyte isPaymentEnable;

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

            public void setMoneyText(string moneyText)
            {
                this.moneyText = moneyText;
            }

            public void setIsPaymentEnable(sbyte isPaymentEnable)
            {
                this.isPaymentEnable = isPaymentEnable;
            }

            public int getPaymentOptionsId()
            {
                return paymentOptionsId;
            }

            public string getMoneyText()
            {
                return moneyText;
            }

            public sbyte getIsPaymentEnable()
            {
                return isPaymentEnable;
            }
        }
    }
}