/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Class.java to edit this template
 */
package data.top;

import data.pet.PetTemplate;
import java.sql.Connection;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.logging.Level;
import java.util.logging.Logger;
import manager.GopetManager;
import manager.MYSQLManager;
import Utilities;

/**
 *
 * @author MINH THONG
 */
public class TopPet extends Top {

    public const TopPet instance = new TopPet();

    public TopPet() {
        base("top_pet");
        base.name = "TOP Pet";
        base.desc = "Chỉ những thú cưng mạnh mẽ";
    }

     
    public void update() {
        Connection connection = null;
        try {
            lastDatas.Clear();
            lastDatas.AddRange(datas);
            datas.Clear();
            connection = MYSQLManager.create();
            MYSQLManager.updateSql("DELETE FROM `top_pet`;", connection);
            MYSQLManager.updateSql("INSERT INTO `top_pet` (`ownerId`, `ownerName`,`petTemplateId`, `star`, `lvl`, `exp`, `maxHp`, `maxMp`, `def`, `atk`) SELECT `player`.`user_id` as `ownerId`, `player`.`name` as` ownerName`, JSON_VALUE(`player`.`pets`, '$[*].petIdTemplate') AS `petIdTemplate`, JSON_VALUE(`player`.`pets`, '$[*].star') AS `star`, JSON_VALUE(`player`.`pets`, '$[*].lvl') AS `lvl`, JSON_VALUE(`player`.`pets`, '$[*].exp') AS `exp`, JSON_VALUE(`player`.`pets`, '$[*].maxHp') AS `maxHp`, JSON_VALUE(`player`.`pets`, '$[*].maxMp') AS `maxMp`, JSON_VALUE(`player`.`pets`, '$[*].def') AS def, JSON_VALUE(player.pets, '$[*].atk') AS `atk` FROM `player` WHERE `player`.`pets` IS NOT NULL && JSON_VALUE(`player`.`pets`, '$[*].exp') IS NOT NULL;", connection);
            MYSQLManager.updateSql("INSERT INTO `top_pet` (`ownerId`, `ownerName`,`petTemplateId`, `star`, `lvl`, `exp`, `maxHp`, `maxMp`, `def`, `atk`) SELECT `player`.`user_id` as `ownerId`, `player`.`name` as `ownerName`, JSON_VALUE(`player`.`petSelected`, '$.petIdTemplate') AS `petIdTemplate`, JSON_VALUE(`player`.`petSelected`, '$.star') AS `star`, JSON_VALUE(player.petSelected, '$.lvl') AS `lvl`, JSON_VALUE(player.petSelected, '$.exp') AS `exp`, JSON_VALUE(`player`.`petSelected`, '$.maxHp') AS `maxHp`, JSON_VALUE(`player`.`petSelected`, '$.maxMp') AS `maxMp`, JSON_VALUE(`player`.`petSelected`, '$.def') AS `def`, JSON_VALUE(`player`.`petSelected`, '$.atk') AS `atk` FROM `player` WHERE `player`.`petSelected` IS NOT NULL && NOT `player`.`petSelected` = 'null';", connection);
            try (ResultSet resultSet = MYSQLManager.jquery("SELECT * FROM `top_pet` ORDER BY lvl DESC, exp DESC LIMIT 30", connection)) {
                int index = 1;
                while (resultSet.next()) {
                    PetTemplate petTemplate = GopetManager.PETTEMPLATE_HASH_MAP.get(resultSet.getInt("petTemplateId"));
                    TopData topData = new TopData();
                    topData.id = resultSet.getInt("ownerId");
                    topData.name = resultSet.getString("ownerName");
                    topData.imgPath = petTemplate.getIcon();
                    topData.title = getNameWithStar(resultSet.getInt("star"), petTemplate) + " của " + topData.name;
                    topData.desc = Utilities.Format("Hạng %s : Cấp %s  hiện có %s kinh nghiệm (hp) %s (mp) %s (def) %s (atk) %s",
                            index,
                            resultSet.getInt("lvl"),
                            Utilities.formatNumber(resultSet.getBigDecimal("exp").longValue()),
                            resultSet.getInt("maxHp"),
                            resultSet.getInt("maxMp"),
                            resultSet.getInt("def"),
                            resultSet.getInt("atk"));
                    datas.add(topData);
                    index++;
                }
            }
            updateSQLBXH();
            connection.close();
        } catch (Exception e) {
            e.printStackTrace();
            if (connection != null) {
                try {
                    connection.close();
                } catch (SQLException ex) {
                    Logger.getLogger(TopPet.class.getName()).log(Level.SEVERE, null, ex);
                }
            }
        }
    }

    public String getNameWithStar(int star, PetTemplate petTemplate) {
        String name = petTemplate.getName() + " ";
        for (int i = 0; i < star; i++) {
            name += "(sao)";
        }

        for (int i = 0; i < 5 - star; i++) {
            name += "(saoden)";
        }
        return name;
    }
}
