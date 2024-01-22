 
public class GiftCodeData {

    private int id;
    private String code;

    private int curUser;
    private int maxUser;

    private int[][] gift_data;

    private Date expire;

    private ArrayList<int> usersOfUseThis = new();

    public GiftCodeData(ResultSet resultSet)   {
        this.id = resultSet.getInt("id");
        this.code = resultSet.getString("code");
        this.curUser = resultSet.getInt("currentUser");
        this.maxUser = resultSet.getInt("maxUser");
        this.gift_data = (int[][]) JsonManager.LoadFromJson(resultSet.getString("gift_data"), int[][].class);
        this.expire = resultSet.getDate("expire");
        Type arrayType = new TypeToken<ArrayList<int>>() {
            }.getType();
        this.usersOfUseThis = (ArrayList<int>) JsonManager.LoadFromJson(resultSet.getString("usersOfUseThis"), arrayType);
    }
}
