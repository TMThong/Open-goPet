package thong.sdk;

import defpackage.ActorFactory;
import java.io.IOException;
import java.io.InputStream;
import vn.me.core.BaseCanvas;

/* loaded from: GopetNative.jar:thong/sdk/ISoundManagerSDK.class */
public abstract class ISoundManagerSDK {

    public abstract ISoundSDK load(InputStream inputStream) throws IOException;

    public abstract ISoundSDK load(String path) throws IOException;

    public void setVolume(int value) {
        this.currentVolume = value;
    }

    public static ISoundSDK currentSoundBg = null;

    public static boolean hasPermissionPlayBgSound = true;
    public static boolean hasPermissionPlayEffSound = true;
    public static final String hasPermissionPlayBgSoundRMS = "permissonBGSound";
    public static final String hasPermissionPlayEffSoundRMS = "permissonEffSound";

    
    
    public static void loadMusicState() {
        hasPermissionPlayBgSound = ActorFactory.loadBoolean(hasPermissionPlayBgSoundRMS, true);
        hasPermissionPlayEffSound = ActorFactory.loadBoolean(hasPermissionPlayEffSoundRMS, true);
        if (!hasPermissionPlayBgSound) {
            if (currentSoundBg != null) {
                currentSoundBg.stop();
            }
        }
    }

    public static void saveMusicState() {
        ActorFactory.saveBool(hasPermissionPlayBgSoundRMS, hasPermissionPlayBgSound);
        ActorFactory.saveBool(hasPermissionPlayEffSoundRMS, hasPermissionPlayEffSound);
    }

    public synchronized static void setCurrentBgSound(ISoundSDK soundSDK) {
        if (currentSoundBg != null) {
            currentSoundBg.stop();
            currentSoundBg.close();
        }

        currentSoundBg = soundSDK;

        if (hasPermissionPlayBgSound && currentSoundBg != null) {
            currentSoundBg.setLoopCount(-1);
            currentSoundBg.start();
        }
    }

    protected int currentVolume = 100;

    public static void playSoundEffect(ISoundSDK iSoundSDK) {
        if (iSoundSDK == null) {
            throw new NullPointerException();
        }
        if (hasPermissionPlayEffSound) {
            iSoundSDK.start();
        }
    }

    public static void playBgSound(String soundName) {
        if (!hasPermissionPlayBgSound) {
            return;
        }
        try {
            setCurrentBgSound(BaseCanvas.soundManagerSDK.load("/sound/" + soundName + ".wav"));
        } catch (IOException ex) {
            ex.printStackTrace();
        }
    }

    public static void playSoundEffect(String soundName) {
        if (!hasPermissionPlayEffSound) {
            return;
        }
        try {
            playSoundEffect(BaseCanvas.soundManagerSDK.load("/sound/" + soundName + ".wav"));
        } catch (IOException ex) {
            ex.printStackTrace();
        }
    }
}
