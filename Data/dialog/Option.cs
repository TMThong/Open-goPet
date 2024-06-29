namespace Gopet.Data.Dialog
{
    public class Option
    {

        public const sbyte CAN_SELECT = 1;
        public const sbyte CANT_SELECT = 0;
        private int optionId;
        private string optionText;
        private sbyte optionStatus;

        public Option()
        {
        }

        public Option(int optionId, string optionText, int optionStatus)
        {
            this.optionId = optionId;
            this.optionText = optionText;
            this.optionStatus = (sbyte)optionStatus;
        }

        public Option(int optionId, string optionText, bool optionStatus)
        {
            this.optionId = optionId;
            this.optionText = optionText;
            this.optionStatus = optionStatus ? CAN_SELECT : CANT_SELECT;
        }

        public Option(int optionId, string optionText)
        {
            this.optionId = optionId;
            this.optionText = optionText;
            optionStatus = CAN_SELECT;
        }

        public void setOptionId(int optionId)
        {
            this.optionId = optionId;
        }

        public void setOptionText(string optionText)
        {
            this.optionText = optionText;
        }

        public void setOptionStatus(sbyte optionStatus)
        {
            this.optionStatus = optionStatus;
        }

        public int getOptionId()
        {
            return optionId;
        }

        public string getOptionText()
        {
            return optionText;
        }

        public sbyte getOptionStatus()
        {
            return optionStatus;
        }

    }
}