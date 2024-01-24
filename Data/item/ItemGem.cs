namespace Gopet.Data.GopetItem
{
    public class ItemGem : DataVersion
    {

        private int element;
        private string name;
        private int[] option;
        private int[] optionValue;
        private int lvl = 0;
        private int itemTemplateId;
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
        public void setElement(int element)
        {
            this.element = element;
        }

        public void setName(string name)
        {
            this.name = name;
        }

        public void setOption(int[] option)
        {
            this.option = option;
        }

        public void setOptionValue(int[] optionValue)
        {
            this.optionValue = optionValue;
        }

        public void setLvl(int lvl)
        {
            this.lvl = lvl;
        }

        public void setItemTemplateId(int itemTemplateId)
        {
            this.itemTemplateId = itemTemplateId;
        }

        public void setTimeUnequip(long timeUnequip)
        {
            this.timeUnequip = timeUnequip;
        }

        public int getElement()
        {
            return element;
        }

        public string getName()
        {
            return name;
        }

        public int[] getOption()
        {
            return option;
        }

        public int[] getOptionValue()
        {
            return optionValue;
        }

        public int getLvl()
        {
            return lvl;
        }

        public int getItemTemplateId()
        {
            return itemTemplateId;
        }

        public long getTimeUnequip()
        {
            return timeUnequip;
        }



    }
}