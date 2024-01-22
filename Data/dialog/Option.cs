 
public class Option {

    public const byte CAN_SELECT = 1;
    public const byte CANT_SELECT = 0;
    private int optionId;
    private String optionText;
    private byte optionStatus;

    public Option() {
    }

    public Option(int optionId, String optionText, int optionStatus) {
        this.optionId = optionId;
        this.optionText = optionText;
        this.optionStatus = (byte) optionStatus;
    }

    public Option(int optionId, String optionText) {
        this.optionId = optionId;
        this.optionText = optionText;
        this.optionStatus = CAN_SELECT;
    }
}
