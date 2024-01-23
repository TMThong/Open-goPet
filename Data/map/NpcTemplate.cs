
using System.Diagnostics;

[Serializable]
[DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
public class NpcTemplate {
    public int npcId;
    public sbyte type;
    public String name;
    public String[] optionName;
    public String[] chat;
    public int[] optionId;
    public String imgPath;
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

    public void setName(String name)
    {
        this.name = name;
    }

    public void setOptionName(String[] optionName)
    {
        this.optionName = optionName;
    }

    public void setChat(String[] chat)
    {
        this.chat = chat;
    }

    public void setOptionId(int[] optionId)
    {
        this.optionId = optionId;
    }

    public void setImgPath(String imgPath)
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
        return this.npcId;
    }

    public sbyte getType()
    {
        return this.type;
    }

    public String getName()
    {
        return this.name;
    }

    public String[] getOptionName()
    {
        return this.optionName;
    }

    public String[] getChat()
    {
        return this.chat;
    }

    public int[] getOptionId()
    {
        return this.optionId;
    }

    public String getImgPath()
    {
        return this.imgPath;
    }

    public int getX()
    {
        return this.x;
    }

    public int getY()
    {
        return this.y;
    }

    public int[] getBounds()
    {
        return this.bounds;
    }


    private string GetDebuggerDisplay()
    {
        return ToString();
    }
}
