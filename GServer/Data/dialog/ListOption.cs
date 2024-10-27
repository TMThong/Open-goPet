namespace Gopet.Data.Dialog
{
    public class ListOption
    {

        protected OptionDialogItem[] options = new OptionDialogItem[0];
        public int listId;
        public bool isModal = false;
        public bool isFullScreen = false;
        public string title = "";

        public ListOption(string Title, int Id)
        {
            title = Title;
            listId = Id;
        }

        public void doSelect(int optionId, Player player)
        {
            OptionDialogItem[] optionDialogItems_ = getOptionDialogItems(player);
            if (optionId >= 0 && optionDialogItems_.Length > optionId)
            {
                optionDialogItems_[optionId].doSelect(player);
            }
        }

        public OptionDialogItem[] getOptionDialogItems(Player player)
        {
            return options;
        }

        public void show(Player player)
        {

        }
    }
}