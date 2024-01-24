namespace Gopet.Data.GopetClan
{
    public class ClanRequestJoin
    {

        public int user_id;
        public string name;
        public long timeRequest;
        public string avatarPath;

        public ClanRequestJoin(int user_id, string name, long timeRequest)
        {
            this.user_id = user_id;
            this.name = name;
            this.timeRequest = timeRequest;
        }

        public string getAvatar()
        {
            if (avatarPath == null)
            {
                return "npcs/gopet.png";
            }

            return avatarPath;
        }
    }
}