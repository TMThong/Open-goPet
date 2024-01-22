/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Class.java to edit this template
 */
package data.user;

import com.google.gson.reflect.TypeToken;
import java.lang.reflect.Type;
import java.sql.Date;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.ArrayList;
import lombok.Getter;
import lombok.Setter;
import manager.JsonManager;
/**
 *
 * @author MINH THONG
 */
@Getter
@Setter
public class GiftCodeData {

    private int id;
    private String code;

    private int curUser;
    private int maxUser;

    private int[][] gift_data;

    private Date expire;

    private ArrayList<Integer> usersOfUseThis = new ArrayList<>();

    public GiftCodeData(ResultSet resultSet) throws SQLException {
        this.id = resultSet.getInt("id");
        this.code = resultSet.getString("code");
        this.curUser = resultSet.getInt("currentUser");
        this.maxUser = resultSet.getInt("maxUser");
        this.gift_data = (int[][]) JsonManager.LoadFromJson(resultSet.getString("gift_data"), int[][].class);
        this.expire = resultSet.getDate("expire");
        Type arrayType = new TypeToken<ArrayList<Integer>>() {
            }.getType();
        this.usersOfUseThis = (ArrayList<Integer>) JsonManager.LoadFromJson(resultSet.getString("usersOfUseThis"), arrayType);
    }
}
