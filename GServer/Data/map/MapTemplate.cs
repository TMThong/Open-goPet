namespace Gopet.Data.Map
{
    public class MapTemplate
    {
        public int mapId { get; private set; }
        public string name { get; private set; }
        public int[] npc { get; private set; }
        public int[] boss {  get; private set; }
        public int[] numPetDie { get; private set; }

        public string getName(Player player)
        {
            return player.Language.MapLanguage[this.mapId];
        }
    }
}