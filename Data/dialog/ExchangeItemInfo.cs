
using Gopet.Util;

public class ExchangeItemInfo : MenuItemInfo {

   
    public ExchangeData exchangeData { get; }

    public ExchangeItemInfo(ExchangeData exchangeData) {
        this.exchangeData = exchangeData;
        this.setTitleMenu(Utilities.Format("Đổi %s (vang)", Utilities.FormatNumber(exchangeData.getGold())));
        this.setShowDialog(true);
        this.setCloseScreenAfterClick(true);
        this.setCanSelect(true);
        this.setDesc(Utilities.Format("Dùng %s vnđ để đổi %s (vang)", Utilities.FormatNumber(exchangeData.getAmount()), Utilities.FormatNumber(exchangeData.getGold())));
        this.setDialogText(Utilities.Format("Bạn có chắc muốn %s", this.getDesc()));
        this.setLeftCmdText(MenuController.CMD_CENTER_OK);
        this.setRightCmdText(MenuController.CMD_CENTER_OK);
        this.setImgPath("gameMisc/icons4.png");
    }

    public ExchangeData getExchangeData()
    {
        return this.exchangeData;
    }
}
