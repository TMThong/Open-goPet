namespace Gopet.Data.GopetItem
{
    public class ItemGem 
    {
        public int element;
        public string name;
        public int[] option;
        public int[] optionValue;
        public int lvl = 0;
        public int itemTemplateId;
        public long timeUnequip = -1;

        public string getElementIcon()
        {
            switch (element)
            {
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
}