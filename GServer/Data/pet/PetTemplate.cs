
using Gopet.Util;
using Newtonsoft.Json;
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

    public int FusionScore { get; private set; } = 0;

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

    public string getName(Player player)
    {
        return player.Language.PetNameLanguage[this.petId];
    }
    /// <summary>
    /// Là pet thiên đình
    /// </summary>
    [JsonIgnore]
    public bool IsSky
    {
        get
        {
            switch (nclass)
            {
                case GopetManager.Angel:
                case GopetManager.Demon:
                case GopetManager.Archer:
                    return true;
                default:
                    return false;
            }
        }
    }
}
