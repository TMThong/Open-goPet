
using System.Diagnostics;

[DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
public class Waypoint
{

    public int x;
    public int y;
    public String name;


    public void setX(int x)
    {
        this.x = x;
    }

    public void setY(int y)
    {
        this.y = y;
    }

    public void setName(String name)
    {
        this.name = name;
    }

    public int getX()
    {
        return this.x;
    }

    public int getY()
    {
        return this.y;
    }

    public String getName()
    {
        return this.name;
    }

    private string GetDebuggerDisplay()
    {
        return ToString();
    }
}
