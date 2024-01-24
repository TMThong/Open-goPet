 
public class AdminItemInfo : MenuItemInfo {
    public AdminItemInfo(String titleMenu, String desc, String imgPath) : base(titleMenu, desc, imgPath){
       
        setCloseScreenAfterClick(true);
        setShowDialog(true);
        setDialogText("Chọn nó?");
        setLeftCmdText(MenuController.CMD_CENTER_OK);
        setCanSelect(true);
    }
}
