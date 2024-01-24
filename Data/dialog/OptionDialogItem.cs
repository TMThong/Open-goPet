namespace Gopet.Data.Dialog
{
    public class OptionDialogItem
    {


        public string name = "";
        public string iconImgPath;
        public bool enable = true;
        public string urlForDownload = "";


        public string desc = "";
        public string subIconImgPath = "";

        public virtual void doSelect(Player player)
        {

        }

    }
}