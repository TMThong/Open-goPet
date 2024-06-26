
using Gopet.Util;
using System.Diagnostics;

public class PetTemplate
{
    public int agi { get; private set; }
    public sbyte element { get; private set; }
    public sbyte type { get; private set; }
    public sbyte nclass { get; private set; }
    public int petId { get; private set; }
    public int str { get; private set; }
    public int _int { get; private set; }

    public string frameImg { get; private set; }
    public string name { get; private set; }
    public string icon { get; private set; }

    public int gymUpLevel { get; private set; }
    public sbyte frameNum { get; private set; }
    public short vY { get; private set; }


    public String getDesc()
    {
        return Utilities.Format("(str) %s (int) %s (agi) %s", str, _int, agi);
    }

    public int getHp()
    {
        return 1 * 3 + str * 4 + 20;
    }

    public int getMp()
    {
        return 1 * 2 + agi * 5 + 20;
    }
}
