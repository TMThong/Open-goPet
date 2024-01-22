/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Class.java to edit this template
 */
package data.top;

import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.concurrent.CopyOnWriteArrayList;
import manager.MYSQLManager;

/**
 *
 * @author MINH THONG
 */
public class Top {

    public String name;
    public String desc;
    public CopyOnWriteArrayList<TopData> datas = new CopyOnWriteArrayList<>();
    public CopyOnWriteArrayList<TopData> lastDatas = new CopyOnWriteArrayList<>();
    public final String top_id;

    public Top(final String top_idString) {
        top_id = top_idString;
    }

    public void update() {

    }

    public void updateSQLBXH() throws SQLException {
        if (!lastDatas.isEmpty() || !datas.isEmpty()) {
            if (!lastDatas.isEmpty() && !datas.isEmpty()) {
                TopData topDataLast = lastDatas.get(0);
                TopData topDataNew = datas.get(0);
                if (topDataLast.id != topDataNew.id) {
                    MYSQLManager.updateSql(String.format(  "DELETE FROM `top_data` WHERE user_id != %s && timeReceiveTop < %s && topID = '%s'", topDataNew.id, System.currentTimeMillis(), top_id));
                    MYSQLManager.updateSql(String.format(  "INSERT INTO `top_data`(`user_id`, `topID`, `timeBeginTop`, `timeReceiveTop`) VALUES (%s, '%s', %s, %s)", topDataNew.id, top_id, System.currentTimeMillis(), 0));
                } else {
                    return;
                }
            } else if (lastDatas.isEmpty() && !datas.isEmpty()) {
                TopData topD = datas.get(0);
                ResultSet resultSet = MYSQLManager.jquery(String.format(  "SELECT * FROM `top_data` where user_id = %s && topID = '%s'", topD.id, top_id));
                if (!resultSet.next()) {
                    MYSQLManager.updateSql(String.format(  "INSERT INTO `top_data`(`user_id`, `topID`, `timeBeginTop`, `timeReceiveTop`) VALUES (%s, '%s', %s, %s)", topD.id, top_id, System.currentTimeMillis(), 0));
                }
                resultSet.close();
                MYSQLManager.updateSql(String.format(  "DELETE FROM `top_data` WHERE user_id != %s && timeReceiveTop < %s && topID = '%s'", topD.id, System.currentTimeMillis(), top_id));
            }
        }
    }

    public CopyOnWriteArrayList<TopData> getTOPData() {
        return datas;
    }
}
