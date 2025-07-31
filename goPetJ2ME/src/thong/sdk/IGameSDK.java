package thong.sdk;

import javax.microedition.lcdui.Graphics;

/* loaded from: GopetNative.jar:thong/sdk/IGameSDK.class */
public interface IGameSDK {
    void loop();

    void render();

    void setGraphics(Graphics graphics);
}