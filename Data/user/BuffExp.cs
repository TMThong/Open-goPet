/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Class.java to edit this template
 */
package data.user;

import base.DataVersion;
import com.google.gson.annotations.SerializedName;
import lombok.Getter;
import lombok.Setter;
import manager.GopetManager;

/**
 *
 * @author MINH THONG
 */
@Getter
@Setter
public class BuffExp extends DataVersion {

    @SerializedName("buffExpTime")
    private long buffExpTime = -1;
    @SerializedName("itemTemplateIdBuff")
    private int itemTemplateIdBuff = -1;
    @SerializedName("buffPercent")
    private float _buffPercent = 0f;
    @SerializedName("currentTime")
    private long currentTime = System.currentTimeMillis();

    public BuffExp() {

    }

    public float getPercent() {
        if (buffExpTime > 0) {
            return _buffPercent;
        }
        return 0;
    }

    public void update() {
        if (buffExpTime >= 0 && System.currentTimeMillis() - currentTime > 2000L) {
            buffExpTime -= (System.currentTimeMillis() - currentTime);
            currentTime = System.currentTimeMillis();
        }
    }
    
    public void loadCurrentTime() {
        currentTime = System.currentTimeMillis();
    }

    public void addTime(long time) {
        currentTime = System.currentTimeMillis();
        if (buffExpTime + time > GopetManager.MAX_TIME_BUFF_EXP) {
            buffExpTime = GopetManager.MAX_TIME_BUFF_EXP;
        } else {
            if (buffExpTime <= 0) {
                buffExpTime = time;
            } else {
                buffExpTime += time;
            }
        }
    }
}
