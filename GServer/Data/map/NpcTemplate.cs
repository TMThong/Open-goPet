using System.Diagnostics;

namespace Gopet.Data.Map
{
    [Serializable]
    [DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
    public class NpcTemplate
    {
        #region CONST
        public const int TRẦN_CHÂN = -1;
        #endregion

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

        public string getName(Player player)
        {
            return player.Language.NpcNameLanguage[this.npcId];
        }

        public string[] getOptionName(Player player)
        {
            string[] strings = new string[optionId.Length];
            for (int i = 0; i < strings.Length; i++)
            {
                strings[i] = player.Language.NpcOptionLanguage[optionId[i]];
            }
            return strings;
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