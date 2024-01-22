 
public class ExchangeItemInfo : MenuItemInfo {

   
    public ExchangeData exchangeData { get; }

    public ExchangeItemInfo(ExchangeData exchangeData) {
        this.exchangeData = exchangeData;
        this.setTitleMenu(String.format("Đổi %s (vang)", Utilities.formatNumber(exchangeData.getGold())));
        this.setShowDialog(true);
        this.setCloseScreenAfterClick(true);
        this.setCanSelect(true);
        this.setDesc(String.format("Dùng %s vnđ để đổi %s (vang)", Utilities.formatNumber(exchangeData.getAmount()), Utilities.formatNumber(exchangeData.getGold())));
        this.setDialogText(String.format("Bạn có chắc muốn %s", this.getDesc()));
        this.setLeftCmdText(MenuController.CMD_CENTER_OK);
        this.setRightCmdText(MenuController.CMD_CENTER_OK);
        this.setImgPath("gameMisc/icons4.png");
    }
}
