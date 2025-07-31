/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package thong.auto;

import defpackage.GlobalService;
import vn.me.network.Message;

/**
 *
 * @author TMThong
 */
public class AutoDectectSpeed implements IAutoBase {

    public static final AutoDectectSpeed instance = new AutoDectectSpeed();

    public static boolean isTest = false;
    public static long TimeSend = 0L;

    public void update() {
        if (System.currentTimeMillis() > TimeSend) {
            Message message = new Message(81);
            message.putByte(103);
            message.cleanup();
            GlobalService.session.sendMessage(message);
            TimeSend = 0;
            isTest = false;
        }
    }

    public boolean isEnable() {
        return isTest;
    }
}
