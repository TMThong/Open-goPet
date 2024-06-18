namespace Gopet.Data.Dialog
{
    public class OptionDialogItem
    {


        public string name { get; set; } = "";
        public string iconImgPath { get; set; }
        public bool enable { get; set; } = true;
        public string urlForDownload { get; set; } = "";


        public string desc { get; set; } = "";
        public string subIconImgPath { get; set; } = "";

        public virtual void doSelect(Player player)
        {

        }

    }
}