using Gopet.Util;

namespace Gopet.Data.Dialog
{
    public class ExchangeItemInfo : MenuItemInfo
    {


        public ExchangeData exchangeData { get; }

        public ExchangeItemInfo(ExchangeData exchangeData)
        {
            this.exchangeData = exchangeData;
            setTitleMenu(Utilities.Format("Đổi %s (vang)", Utilities.FormatNumber(exchangeData.getGold())));
            setShowDialog(true);
            setCloseScreenAfterClick(true);
            setCanSelect(true);
            setDesc(Utilities.Format("Dùng %s vnđ để đổi %s (vang)", Utilities.FormatNumber(exchangeData.getAmount()), Utilities.FormatNumber(exchangeData.getGold())));
            setDialogText(Utilities.Format("Bạn có chắc muốn %s", getDesc()));
            setLeftCmdText(MenuController.CMD_CENTER_OK);
            setRightCmdText(MenuController.CMD_CENTER_OK);
            setImgPath("gameMisc/icons4.png");
        }

        public ExchangeData getExchangeData()
        {
            return exchangeData;
        }
    }
}