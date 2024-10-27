namespace Gopet.Data.GopetClan
{
    public class ClanChat
    {
        private string who;
        private string text;

        public ClanChat(string who, string text)
        {
            this.who = who;
            this.text = text;
        }
        public string getWho()
        {
            return who;
        }

        public string getText()
        {
            return text;
        }

    }
}