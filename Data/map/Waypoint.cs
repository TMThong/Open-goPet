using System.Diagnostics;

namespace Gopet.Data.Map
{
    [DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
    public class Waypoint
    {

        public int x;
        public int y;
        public string name;


        public void setX(int x)
        {
            this.x = x;
        }

        public void setY(int y)
        {
            this.y = y;
        }

        public void setName(string name)
        {
            this.name = name;
        }

        public int getX()
        {
            return x;
        }

        public int getY()
        {
            return y;
        }

        public string getName()
        {
            return name;
        }

        private string GetDebuggerDisplay()
        {
            return ToString();
        }
    }
}