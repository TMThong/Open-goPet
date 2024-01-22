
using Gopet.Data.Collections;

public class MenuScreen {

    protected int menuId;
    protected ArrayList<MenuItemInfo> menuItemInfos = new ArrayList<MenuItemInfo>();
    private String title;
    private byte type = 0;

    public MenuScreen(int menuId, String title) {
        this.menuId = menuId;
        this.title = title;
    }

    public int getMenuId() {
        return menuId;
    }

    public void setMenuId(int menuId) {
        this.menuId = menuId;
    }

    public ArrayList<MenuItemInfo> getMenuItemInfos() {
        return menuItemInfos;
    }

    public void setMenuItemInfos(ArrayList<MenuItemInfo> menuItemInfos) {
        this.menuItemInfos = menuItemInfos;
    }

    public String getTitle() {
        return title;
    }

    public void setTitle(String title) {
        this.title = title;
    }

    public byte getType() {
        return type;
    }

    public void setType(byte type) {
        this.type = type;
    }

    public void show(Player player)   {
        GameController gopetHandler = (GameController) player.controller;
        gopetHandler.showMenuItem(menuId, type, title, menuItemInfos);
    }

    public void select(Player player, int index)   {
        
    }
}
