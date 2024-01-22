/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Class.java to edit this template
 */
package data.top;

import java.sql.ResultSet;
import manager.MYSQLManager;
import util.Utilities;

/**
 *
 * @author MINH THONG
 */
public class TopGold extends Top {

    private final String[] topNameStrings = new String[]{"Phú hộ", "Bá hộ", "Chủ nông"};
    public static TopGold instance = new TopGold();

    public TopGold() {
        super("top_gold");
        super.name = "TOP Đại gia";
        super.desc = "Chỉ những người chơi giàu có";
    }

    @Override
    public void update() {
        try {

            lastDatas.clear();
            lastDatas.addAll(datas);
            datas.clear();
            try (ResultSet resultSet = MYSQLManager.jquery("SELECT * FROM `player` WHERE gold > 0 && isAdmin = 0 ORDER BY `player`.`gold` DESC LIMIT 10")) {
                int index = 1;
                while (resultSet.next()) {
                    TopData topData = new TopData();
                    topData.id = resultSet.getInt("user_id");
                    topData.name = resultSet.getString("name");
                    topData.imgPath = resultSet.getString("avatarPath");
                    topData.title = topData.name;
                    topData.desc = String.format("Hạng %s : đang có %s (vang)", index, Utilities.formatNumber(resultSet.getBigDecimal("gold").longValue()));
                    datas.add(topData);
                    index++;
                }
            }
            updateSQLBXH();
        } catch (Exception e) {
            e.printStackTrace();
        }
    }
}
