
using Gopet.Data.Map;

public class ClanPlace : GopetPlace {

    public ClanPlace(GopetMap m, int ID) : base(m, ID)
    {
        
        this.maxPlayer = int.MaxValue;
    }

     
    public override void add(Player player)   {
        player.playerData.x = 326 + (24 * 3);
        player.playerData.y = 236;
        base.add(player); // Generated from nbfs://nbhost/SystemFileSystem/Templates/Classes/Code/OverriddenMethodBody
    }
}
