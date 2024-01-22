 
public class ItemGem : DataVersion  {

    private int element;
    private String name;
    private int[] option;
    private int[] optionValue;
    private int lvl = 0;
    private int itemTemplateId;
    private long timeUnequip = -1;

    public String getElementIcon() {
        switch (element) {
            case GopetManager.FIRE_ELEMENT:
                return "(fire)";
            case GopetManager.WATER_ELEMENT:
                return "(water)";
            case GopetManager.ROCK_ELEMENT:
                return "(rock)";
            case GopetManager.THUNDER_ELEMENT:
                return "(thunder)";
            case GopetManager.TREE_ELEMENT:
                return "(tree)";
            case GopetManager.LIGHT_ELEMENT:
                return "(light)";
            case GopetManager.DARK_ELEMENT:
                return "(dark)";
        }
        return "";
    }
}
