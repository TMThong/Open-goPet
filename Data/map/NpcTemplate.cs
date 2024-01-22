
using System.Diagnostics;

[Serializable]
[DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
public class NpcTemplate {
    public int npcId;
    public byte type;
    public String name;
    public String[] optionName;
    public String[] chat;
    public int[] optionId;
    public String imgPath;
    public int x;
    public int y;
    public int[] bounds;

    private string GetDebuggerDisplay()
    {
        return ToString();
    }
}
