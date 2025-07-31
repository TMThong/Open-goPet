package thong.sdk;

/* loaded from: GopetNative.jar:thong/sdk/IPlatformSDK.class */
public interface IPlatformSDK {
    IAsssetSDK getAssetSDK();

    ISoundManagerSDK getSoundManagerSDK();

    void setGameSDK(IGameSDK iGameSDK);
}