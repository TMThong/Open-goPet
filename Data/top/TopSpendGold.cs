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
public class TopSpendGold extends Top {

    public const TopSpendGold instance = new TopSpendGold();

    public TopSpendGold() {
        super("top_spendgold");
        super.name = "Top Đại gia xuống núi";
        super.desc = "Chỉ người nạp số tiền cao nhất";
    }

    public TopData find(int user_id) {
        for (TopData data : datas) {
            if (data.id == user_id) {
                return data;
            }
        }
        return null;
    }

    @Override
    public void update() {
        try {
            lastDatas.clear();
            lastDatas.addAll(datas);
            datas.clear();
            try (ResultSet resultSet = MYSQLManager.jquery("SELECT * FROM `player`  WHERE isAdmin = 0 ORDER BY  `spendGold` DESC LIMIT 300;")) {
                int index = 1;
                while (resultSet.next()) {
                    TopData topData = new TopData();
                    topData.id = resultSet.getInt("user_id");
                    topData.name = resultSet.getString("name");
                    topData.imgPath = resultSet.getString("avatarPath");
                    topData.title = topData.name;
                    topData.desc = String.format("Hạng %s: Đã tiêu %s (vang)", index, Utilities.formatNumber(resultSet.getBigDecimal("spendGold").longValue()));
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
