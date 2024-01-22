/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Class.java to edit this template
 */
package data.top;

import java.sql.ResultSet;
import manager.MYSQLManager;

/**
 *
 * @author MINH THONG
 */
public class TopLVLClan extends Top {

    public const TopLVLClan instance = new TopLVLClan();

    public TopLVLClan() {
        base("top_clan");
        base.name = "TOP LVL Bang hội";
        base.desc = "Chỉ những bang hội có cấp độ cao";
    }

    @Override
    public void update() {
        try {
            lastDatas.clear();
            lastDatas.addAll(datas);
            datas.clear();
            try (ResultSet resultSet = MYSQLManager.jquery("SELECT * FROM `clan` ORDER BY `lvl` DESC LIMIT 10;")) {
                int index = 1;
                while (resultSet.next()) {
                    TopData topData = new TopData();
                    topData.id = resultSet.getInt("clanId");
                    topData.name = "Bang " + resultSet.getString("name");
                    topData.imgPath = "npcs/gopet.png";
                    topData.title = topData.name;
                    topData.desc = String.format("Hạng %s : bang lvl %s", index, resultSet.getInt("lvl"));
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
