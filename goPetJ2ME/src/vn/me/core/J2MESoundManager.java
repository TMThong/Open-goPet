/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package vn.me.core;

import java.io.IOException;
import java.io.InputStream;
import javax.microedition.media.Manager;
import javax.microedition.media.MediaException;
import javax.microedition.media.control.VolumeControl;
import thong.sdk.ISoundManagerSDK;
import thong.sdk.ISoundSDK;
import vn.me.ui.common.ResourceManager;

/**
 *
 * @author Admin
 */
public class J2MESoundManager extends ISoundManagerSDK {

    public ISoundSDK load(InputStream inputStream) throws IOException {
        try {
            J2MESoundPlayer eSoundPlayer = new J2MESoundPlayer(Manager.createPlayer(inputStream, "audio/x-wav"));
            return eSoundPlayer;
        } catch (MediaException ex) {
            ex.printStackTrace();
            return null;
        }
    }

    public ISoundSDK load(String path) throws IOException {
        return load(ResourceManager.getResource(path));
    }
}
