namespace Gopet.Data.Dialog
{
    public class AdminItemInfo : MenuItemInfo
    {
        public AdminItemInfo(string titleMenu, string desc, string imgPath) : base(titleMenu, desc, imgPath)
        {

            setCloseScreenAfterClick(true);
            setShowDialog(true);
            setDialogText("Chọn nó?");
            setLeftCmdText(MenuController.CMD_CENTER_OK);
            setCanSelect(true);
        }
    }
}