 
public class Option {

    public const sbyte CAN_SELECT = 1;
    public const sbyte CANT_SELECT = 0;
    private int optionId;
    private String optionText;
    private sbyte optionStatus;

    public Option() {
    }

    public Option(int optionId, String optionText, int optionStatus) {
        this.optionId = optionId;
        this.optionText = optionText;
        this.optionStatus = (sbyte) optionStatus;
    }

    public Option(int optionId, String optionText) {
        this.optionId = optionId;
        this.optionText = optionText;
        this.optionStatus = CAN_SELECT;
    }
}
