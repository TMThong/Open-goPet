using Gopet.Data.Collections;

namespace Gopet.Data.Dialog
{
    public class MenuScreen
    {

        protected int menuId;
        protected JArrayList<MenuItemInfo> menuItemInfos = new JArrayList<MenuItemInfo>();
        private string title;
        private sbyte type = 0;

        public MenuScreen(int menuId, string title)
        {
            this.menuId = menuId;
            this.title = title;
        }

        public int getMenuId()
        {
            return menuId;
        }

        public void setMenuId(int menuId)
        {
            this.menuId = menuId;
        }

        public JArrayList<MenuItemInfo> getMenuItemInfos()
        {
            return menuItemInfos;
        }

        public void setMenuItemInfos(JArrayList<MenuItemInfo> menuItemInfos)
        {
            this.menuItemInfos = menuItemInfos;
        }

        public string getTitle()
        {
            return title;
        }

        public void setTitle(string title)
        {
            this.title = title;
        }

        public sbyte getType()
        {
            return type;
        }

        public void setType(sbyte type)
        {
            this.type = type;
        }

        public void show(Player player)
        {
            GameController gopetHandler = player.controller;
            gopetHandler.showMenuItem(menuId, type, title, menuItemInfos);
        }

        public void select(Player player, int index)
        {

        }
    }
}