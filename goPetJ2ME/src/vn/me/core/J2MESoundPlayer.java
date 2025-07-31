/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package vn.me.core;

import javax.microedition.media.MediaException;
import javax.microedition.media.Player;
import thong.sdk.ISoundSDK;

/**
 *
 * @author Admin
 */
public class J2MESoundPlayer implements ISoundSDK {

    private Player player;

    public J2MESoundPlayer(Player player) {
        this.player = player;
    }

    public void setLoopCount(int count) {
        this.player.setLoopCount(count);
    }

    public void start() {
        try {
            this.player.start();
        } catch (MediaException ex) {
            ex.printStackTrace();
        }
    }

    public void stop() {
        try {
            this.player.stop();
        } catch (MediaException ex) {
            ex.printStackTrace();
        }
    }

    public void close() {
        this.player.close();
    }

    public Player getPlayer() {
        return this.player;
    }
}
