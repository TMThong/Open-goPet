 
public class ClanRequestJoin {

    public int user_id;
    public String name;
    public long timeRequest;
    public String avatarPath;

    public ClanRequestJoin(int user_id, String name, long timeRequest) {
        this.user_id = user_id;
        this.name = name;
        this.timeRequest = timeRequest;
    }

    public String getAvatar() {
        if (avatarPath == null) {
            return "npcs/gopet.png";
        }

        return avatarPath;
    }
}
