package vn.me.core;

import defpackage.Class29;
import vn.me.network.MobileClient;
import vn.me.screen.Screen;
import javax.microedition.lcdui.Canvas;
import javax.microedition.lcdui.Display;
import javax.microedition.lcdui.Graphics;
import javax.microedition.midlet.MIDlet;
import thong.sdk.IGameSDK;
import thong.sdk.IPlatformSDK;
import thong.sdk.ISoundManagerSDK;

public final class BaseCanvas extends Canvas implements Runnable {

    private long Field147;
    public static boolean isRunning;
    public static boolean isPause;
    public static int ticks;
    public static int w;
    public static int h;
    public static int Field157;
    public static int Field158;
    public static int Field159;
    public static int Field160;
    public static int Field161;
    public static int Field162;
    public static Screen currentScreen;
    public MIDlet midlet;
    public static BaseCanvas instance;
    private int vibrateTime;
    public static Graphics g;
    public Class29 Field169;
    public MobileClient Field170;
    public int initialPressX;
    public int initialPressY;
    private static int Field145 = 800;
    private static int Field146 = 10;
    private static boolean Field149 = false;
    private static int platform = 0;
    private int keyCode = -1982;
    private int[][] inputEvents = new int[10][3];
    private int inputEventsNum = 0;
    private int Field171 = 0;
    private int Field172 = 7;
    private int Field173 = 0;
    private int Field174 = 0;
    public static IPlatformSDK iPlatformSDK;
    public static final int SPEED_TIME = 50;
    public static int curSpeedGame = SPEED_TIME;
    public static ISoundManagerSDK soundManagerSDK = new J2MESoundManager();

    public static BaseCanvas create(MIDlet mIDlet) {
        if (instance == null) {
            instance = new BaseCanvas(mIDlet);
        }
        return instance;
    }

    private BaseCanvas(MIDlet mIDlet) {
        setFullScreenMode(true);
        w = getWidth();
        h = getHeight();
        layout();
        platform = initPlatform();
        this.midlet = mIDlet;
        if (iPlatformSDK != null) {
            iPlatformSDK.setGameSDK(new IGameSDK() {
                ///////@Override
                public void loop() {
                    BaseCanvas.this.gameLoop();
                }

                ///////@Override
                public void render() {
                    BaseCanvas.this.draw();
                }

                ///////@Override
                public void setGraphics(Graphics graphics) {
                    BaseCanvas.g = graphics;
                }
            });
        }
    }

    private int initPlatform() {
        if (getKeyCode(8) == -20) {
            return 1;
        }
        if (System.getProperty("microedition.platform").indexOf("SonyEricsson") != -1) {
            return 2;
        }
        try {
            Class.forName("com.samsung.util.Vibration");
            return 3;
        } catch (Exception unused) {
            try {
                Class.forName("com.siemens.mp.io.File");
                return 5;
            } catch (Exception unused2) {
                return 0;
            }
        }
    }

    protected final void sizeChanged(int i, int i2) {
        super.sizeChanged(i, i2);
        w = i;
        h = i2;
        layout();
    }

    private static void layout() {
        Field157 = w >> 1;
        Field158 = h >> 1;
        Field159 = w / 3;
        int i = h;
        Field160 = (2 * w) / 3;
        int i2 = h;
        Field161 = (3 * w) / 4;
        Field162 = (3 * h) / 4;
        int i3 = w;
        int i4 = h;
        int i5 = w;
        if (currentScreen != null) {
            currentScreen.Method130();
        }
    }

    public static void setCurrentScreen(Screen screen) {
        if (currentScreen == screen) {
            return;
        }
        if (currentScreen != null) {
            currentScreen.close();
            currentScreen = null;
        }
        if (screen == null) {
            currentScreen = null;
            return;
        }
        currentScreen = screen;
        screen.hide();
        System.out.println("vn.me.core.BaseCanvas.setCurrentScreen() " + screen.getClass().getName());
    }

    public static Screen getCurrentScreen() {
        return currentScreen;
    }

    public final void Method0() {
        new Thread(this).start();
    }

    private long timeUpdate = 0l;

    private void gameLoop() {
        if (!isPause) {

            if (iPlatformSDK != null) {
                if (timeUpdate > System.currentTimeMillis()) {
                    return;
                }
            }

            try {
                long currentTimeMillis = System.currentTimeMillis();
                ticks++;

                if (vibrateTime > 0) {
                    this.vibrateTime--;
                    if (this.vibrateTime == 0) {
                        Display.getDisplay(instance.midlet).vibrate(0);
                    }
                }
                if (this.inputEventsNum != 0) {
                    synchronized (this) {
                        for (int i = 0; i < this.inputEventsNum; i++) {
                            int[] iArr = this.inputEvents[i];
                            switch (iArr[0]) {
                                case 0:
                                case 1:
                                    currentScreen.checkKeys(iArr[0], iArr[1]);
                                    break;
                                case 2:
                                    currentScreen.pointerPressed(iArr[1], iArr[2]);
                                    break;
                                case 3:
                                    currentScreen.pointerDragged(iArr[1], iArr[2]);
                                    break;
                                case 4:
                                    currentScreen.pointerReleased(iArr[1], iArr[2]);
                                    break;
                            }
                        }
                        this.inputEventsNum = 0;
                    }
                }
                long currentTimeMillis2 = System.currentTimeMillis();
                if (this.keyCode != -1982 && this.Field147 <= currentTimeMillis2) {
                    synchronized (this) {
                        if (this.inputEventsNum < 10) {
                            this.inputEvents[this.inputEventsNum][0] = 0;
                            this.inputEvents[this.inputEventsNum][1] = this.keyCode;
                            this.inputEventsNum++;
                        }
                    }
                    this.Field147 = currentTimeMillis2 + Field146;
                }
                if (currentScreen != null) {
                    currentScreen.update();
                    currentScreen.Method296();
                    for (int i2 = 0; i2 < currentScreen.listScreen.size(); i2++) {
                        Screen Screen = (Screen) currentScreen.listScreen.elementAt(i2);
                        if (Screen.Field1171) {
                            Screen.update();
                        }
                    }
                }
                if (this.Field169 != null) {
                    this.Field169.Method0();
                }
                if (iPlatformSDK == null) {
                    repaint();
                    serviceRepaints();
                }
                if (this.Field170 != null && this.Field170.isConnected() && this.Field170.isSYNC) {
                    this.Field170.processMessages();
                }
                long thTime = curSpeedGame - (System.currentTimeMillis() - currentTimeMillis);
                timeUpdate = System.currentTimeMillis() + thTime;
                if (thTime > 0 && iPlatformSDK == null) {
                    Thread.sleep(thTime);
                }
            } catch (Exception e) {
                e.printStackTrace();
            }
        }
    }

    ///////@Override
    public void run() {
        isRunning = true;
        while (isRunning && iPlatformSDK == null) {
            gameLoop();
        }
    }

    public final void keyPressed(int i) {
        int Method343 = Method343(i);
        if (Method343 != -6 && Method343 != -7) {
            this.keyCode = Method343;
            this.Field147 = System.currentTimeMillis() + Field145;
        }
        if (currentScreen.Method272(Method343)) {
            return;
        }
        synchronized (this) {
            if (this.inputEventsNum < 10) {
                this.inputEvents[this.inputEventsNum][0] = 0;
                this.inputEvents[this.inputEventsNum][1] = Method343;
                this.inputEventsNum++;
            }
        }
    }

    public final void keyReleased(int i) {
        this.keyCode = -1982;
        int Method343 = Method343(i);
        Screen Screen = currentScreen;
        Screen.Method126();
        synchronized (this) {
            if (this.inputEventsNum < 10) {
                this.inputEvents[this.inputEventsNum][0] = 1;
                this.inputEvents[this.inputEventsNum][1] = Method343;
                this.inputEventsNum++;
            }
        }
    }

    private static int Method343(int i) {
        if (platform == 1) {
            switch (i) {
                case -6:
                    return -2;
                case -5:
                    return -4;
                case -2:
                    return -3;
            }
        } else if (platform == 5) {
            switch (i) {
                case -62:
                    return -4;
                case -61:
                    return -3;
                case -60:
                    return -2;
                case -59:
                    return -1;
                case -26:
                    return -5;
                case -4:
                    return -7;
                case -1:
                    return -6;
            }
        }
        switch (i) {
            case -204:
            case -8:
            case 8:
                return -8;
            case -39:
            case -2:
                return -2;
            case -38:
            case -1:
                return -1;
            case -22:
            case -7:
                return -7;
            case -21:
            case -6:
            case 4098:
                return -6;
            case -20:
            case -5:
            case 10:
                return -5;
            case -4:
                return -4;
            case -3:
                return -3;
            default:
                return i;
        }
    }

    protected final void keyRepeated(int i) {
    }

    private boolean Method1(int i, int i2) {
        if (this.Field171 == 0) {
            this.Field173 = i;
            this.Field174 = i2;
            this.Field171++;
            return false;
        }
        this.Field171++;
        if (this.Field171 > this.Field172) {
            return true;
        }
        if ((3 * w) / 100 <= Math.abs(this.Field173 - i)) {
            this.Field171 = this.Field172 + 1;
            return true;
        } else if ((3 * h) / 100 <= Math.abs(this.Field174 - i2)) {
            this.Field171 = this.Field172 + 1;
            return true;
        } else {
            return false;
        }
    }

    protected final void pointerDragged(int i, int i2) {
        synchronized (this) {
            if (Method1(i, i2) && this.inputEventsNum < 10) {
                this.inputEvents[this.inputEventsNum][0] = 3;
                this.inputEvents[this.inputEventsNum][1] = i;
                this.inputEvents[this.inputEventsNum][2] = i2;
                this.inputEventsNum++;
            }
        }
    }

    protected final void pointerPressed(int i, int i2) {
        this.initialPressX = i;
        this.initialPressY = i2;
        synchronized (this) {
            if (this.inputEventsNum < 10) {
                this.inputEvents[this.inputEventsNum][0] = 2;
                this.inputEvents[this.inputEventsNum][1] = i;
                this.inputEvents[this.inputEventsNum][2] = i2;
                this.inputEventsNum++;
            }
        }
    }

    protected final void pointerReleased(int i, int i2) {
        if (this.Field171 == 0 && i != this.initialPressX && i2 != this.initialPressY) {
            Method1(this.initialPressX, this.initialPressY);
            if (Method1(i, i2)) {
                pointerDragged(this.initialPressX, this.initialPressY);
                pointerDragged(i, i2);
            }
        }
        this.Field171 = 0;
        synchronized (this) {
            if (this.inputEventsNum < 10) {
                this.inputEvents[this.inputEventsNum][0] = 4;
                this.inputEvents[this.inputEventsNum][1] = i;
                this.inputEvents[this.inputEventsNum][2] = i2;
                this.inputEventsNum++;
            }
        }
    }

    private void draw() {
        if (currentScreen != null) {
            currentScreen.paint();
            return;
        }
        g.setColor(0);
        g.fillRect(0, 0, w, h);
    }

    protected final void paint(Graphics graphics) {
        if (g != graphics) {
            g = graphics;
        }
        draw();
    }

    public final void resetScreen() {
        Display.getDisplay(this.midlet).setCurrent(this);
        setFullScreenMode(true);
    }
}
