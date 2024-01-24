using System.Diagnostics;

namespace Gopet.Data.Map
{
    [Serializable]
    [DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
    public class NpcTemplate
    {
        public int npcId;
        public sbyte type;
        public string name;
        public string[] optionName;
        public string[] chat;
        public int[] optionId;
        public string imgPath;
        public int x;
        public int y;
        public int[] bounds;

        public void setNpcId(int npcId)
        {
            this.npcId = npcId;
        }

        public void setType(sbyte type)
        {
            this.type = type;
        }

        public void setName(string name)
        {
            this.name = name;
        }

        public void setOptionName(string[] optionName)
        {
            this.optionName = optionName;
        }

        public void setChat(string[] chat)
        {
            this.chat = chat;
        }

        public void setOptionId(int[] optionId)
        {
            this.optionId = optionId;
        }

        public void setImgPath(string imgPath)
        {
            this.imgPath = imgPath;
        }

        public void setX(int x)
        {
            this.x = x;
        }

        public void setY(int y)
        {
            this.y = y;
        }

        public void setBounds(int[] bounds)
        {
            this.bounds = bounds;
        }

        public int getNpcId()
        {
            return npcId;
        }

        public sbyte getType()
        {
            return type;
        }

        public string getName()
        {
            return name;
        }

        public string[] getOptionName()
        {
            return optionName;
        }

        public string[] getChat()
        {
            return chat;
        }

        public int[] getOptionId()
        {
            return optionId;
        }

        public string getImgPath()
        {
            return imgPath;
        }

        public int getX()
        {
            return x;
        }

        public int getY()
        {
            return y;
        }

        public int[] getBounds()
        {
            return bounds;
        }


        private string GetDebuggerDisplay()
        {
            return ToString();
        }
    }
}