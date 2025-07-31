package defpackage;

import vn.me.ui.geom.Rectangle;
import vn.me.ui.common.ResourceManager;
import vn.me.core.BaseCanvas;
import java.io.ByteArrayInputStream;
import java.io.DataInputStream;
import java.io.IOException;
import javax.microedition.lcdui.Image;

/* renamed from: Class137  reason: default package */
 /* loaded from: gopet_repackage.jar:Class137.class */
public final class mMap {

    public int mGOMapId;
    public int mapWidth;
    private int mapHeight;
    public int mapWidthPixel;
    public int mapHeightPixel;
    private static byte[][][] Field928 = {
        new byte[][]{
            new byte[]{0, 0},
            new byte[]{0, 1}},
        new byte[][]{
            new byte[]{0, 0},
            new byte[]{1, 0}},
        new byte[][]{
            new byte[]{0, 0},
            new byte[]{1, 1}},
        new byte[][]{
            new byte[]{0, 1},
            new byte[]{0, 0}},
        new byte[][]{
            new byte[]{0, 1},
            new byte[]{0, 1}},
        new byte[][]{
            new byte[]{0, 1},
            new byte[]{1, 0}},
        new byte[][]{
            new byte[]{0, 1},
            new byte[]{1, 1}},
        new byte[][]{
            new byte[]{1, 0},
            new byte[]{0, 0}},
        new byte[][]{
            new byte[]{1, 0},
            new byte[]{0, 1}},
        new byte[][]{new byte[]{1, 0},
        new byte[]{1, 0}},
        new byte[][]{
            new byte[]{1, 0},
            new byte[]{1, 1}},
        new byte[][]{
            new byte[]{1, 1},
            new byte[]{0, 0}},
        new byte[][]{
            new byte[]{1, 1},
            new byte[]{0, 1}},
        new byte[][]{
            new byte[]{1, 1},
            new byte[]{1, 0}}
    };
    private int Field929;
    private byte[][][] Field930;
    private byte[][] Field931;
    private Image[] Field932;
    private int[] Field933;
    private byte[] Field934;
    public Class132[] Field935;
    public short[] xObject;
    public short[] yObject;
    public byte[] Field938;
    public byte[] Field939;
    public byte[] Field940;
    public Class131[] Field941;
    public mMapObject[] Field942;
    private Rectangle Field943 = new Rectangle(0, 0, BaseCanvas.w, BaseCanvas.h);

    public mMap() {
    }

    public mMap(int i, SceneManage class140, int i2, int i3) {
        this.mGOMapId = i;
        DataInputStream dataInputStream = null;
        if (!(getMapVersion(i) == Method184(i) && Method177(i)) && (i < 2 || i > 5)) {
            byte[] Method488 = ActorFactory.loadBuffer(new StringBuffer("mapDynamicData_").append(i).toString(), 1);
            if (Method488 != null) {
                dataInputStream = new DataInputStream(new ByteArrayInputStream(Method488));
            }
        } else {
            dataInputStream = new DataInputStream(ResourceManager.getResource(new StringBuffer("/maps/").append(i).append(".dat").toString()));
        }
        readMap(dataInputStream, class140);
    }

    public static int getMapVersion(int i) {
        Integer Method493 = ActorFactory.loadInt(new StringBuffer("mapVersion_").append(i).toString());
        if (Method493 == null) {
            if (Method177(i)) {
                return Method184(i);
            }
            return -1;
        }
        return Method493.intValue();
    }

    private static boolean Method177(int i) {
        try {
            new DataInputStream(ResourceManager.getResource(new StringBuffer("/maps/").append(i).append(".dat").toString())).readByte();
            return true;
        } catch (Exception unused) {
            return false;
        }
    }

    public static void Method178(int i, int i2) {
        String stringBuffer = new StringBuffer("mapVersion_").append(i).toString();
        ActorFactory.deleteRecord(stringBuffer);
        ActorFactory.saveInt(stringBuffer, i2);
    }

    public static void Method179(byte[] bArr, int i) {
        try {
            ActorFactory.deleteRecord(new StringBuffer("mapDynamicData_").append(i).toString());
            Method180(i);
            ActorFactory.saveBuffer(new StringBuffer("mapDynamicData_").append(i).toString(), bArr);
        } catch (Exception unused) {
        }
    }

    private static void Method180(int i) {
        try {
            byte[] Method488 = ActorFactory.loadBuffer(new StringBuffer("mapDynamicData_").append(i).toString(), 1);
            if (Method488 != null) {
                DataInputStream dataInputStream = new DataInputStream(new ByteArrayInputStream(Method488));
                int readByte = dataInputStream.readByte() + dataInputStream.readByte();
                for (int i2 = 0; i2 < readByte; i2++) {
                    short readShort = dataInputStream.readShort();
                    if (dataInputStream.readByte() == 1) {
                        ActorFactory.deleteRecord(new StringBuffer("ani11_").append((int) readShort).toString());
                    } else {
                        ActorFactory.deleteRecord(new StringBuffer("image10_").append((int) readShort).toString());
                    }
                }
            }
        } catch (Exception unused) {
        }
    }

    private void readMap(DataInputStream reader, SceneManage class140) {
        try {
            int readByte = reader.readByte();
            this.Field932 = new Image[readByte];
            int readByte2 = readByte + reader.readByte();
            this.Field933 = new int[readByte2];
            this.Field934 = new byte[readByte2];
            for (int i = 0; i < readByte2; i++) {
                this.Field933[i] = reader.readShort();
                this.Field934[i] = reader.readByte();
            }
            for (int i2 = 0; i2 < readByte; i2++) {
                this.Field932[i2] = Method185(new StringBuffer().append(this.Field933[i2]).toString());
            }
            this.mapWidth = reader.readByte();
            this.mapHeight = reader.readByte();
            this.mapWidthPixel = this.mapWidth * 24;
            this.mapHeightPixel = this.mapHeight * 24;
            this.Field929 = reader.readByte();
            this.Field930 = new byte[this.Field929][this.mapHeight][this.mapWidth];
            for (int i3 = 0; i3 < this.Field929; i3++) {
                for (int i4 = 0; i4 < this.mapHeight; i4++) {
                    reader.read(this.Field930[i3][i4]);
                }
            }
            this.Field931 = new byte[this.mapHeight][this.mapWidth];
            for (int i5 = 0; i5 < this.mapHeight; i5++) {
                reader.read(this.Field931[i5]);
            }
            int readInt = reader.readInt();
            this.xObject = new short[readInt];
            this.yObject = new short[readInt];
            this.Field938 = new byte[readInt];
            this.Field939 = new byte[readInt];
            this.Field940 = new byte[readInt];
            this.Field941 = new Class131[readByte2];
            for (int i6 = 0; i6 < readInt; i6++) {
                byte readByte3 = reader.readByte();
                this.Field938[i6] = readByte3;
                boolean z = false;
                if (this.Field941[readByte3] == null) {
                    this.Field941[readByte3] = new Class131(this.Field933[readByte3], this.Field934[readByte3]);
                    z = true;
                }
                Class131 class131 = this.Field941[readByte3];
                this.xObject[i6] = reader.readShort();
                this.yObject[i6] = reader.readShort();
                this.Field940[i6] = reader.readByte();
                if (this.Field934[readByte3] == 1) {
                    if (z) {
                        class131.setCollisionRec(new Rectangle(reader.readByte(), reader.readByte(), reader.readByte(), reader.readByte()));
                        class131.Field859 = reader.readByte();
                        this.Field939[i6] = class131.Field859;
                        SkillAnimation Method186 = Method186(new StringBuffer().append(class131.Field858).toString());
                        if (Method186 != null) {
                            class131.Field860 = new SkillEffect[Method186.Field1263.length];
                            for (int i7 = 0; i7 < Method186.Field1263.length; i7++) {
                                class131.Field860[i7] = new SkillEffect(Method186.Field1263[i7]);
                            }
                        }
                    } else {
                        for (int i8 = 0; i8 < 5; i8++) {
                            this.Field939[i6] = reader.readByte();
                        }
                    }
                } else if (z && class131.img == null) {
                    class131.setCollisionRec(new Rectangle(-64, -64, 127, 127));
                    class131.img = Method185(new StringBuffer().append(class131.Field858).toString());
                }
            }
            int readInt2 = reader.readInt();
            this.Field942 = new mMapObject[readInt2];
            int i9 = 0;
            for (int i10 = 0; i10 < readInt2; i10++) {
                int i11 = i9;
                i9++;
                this.Field942[i11] = mMapObject.Method383(class140, reader);
            }
            int readByte4 = reader.readByte();
            this.Field935 = new Class132[readByte4];
            for (int i12 = 0; i12 < readByte4; i12++) {
                this.Field935[i12] = new Class132(reader.readByte(), reader.readShort(), reader.readShort());
            }
            reader.close();
        } catch (Exception e) {
            Method178(this.mGOMapId, Method184(this.mGOMapId));
        }
    }

    public final boolean Method182(int i, int i2) {
        int i3 = i / 24;
        int i4 = i2 / 24;
        if (i3 < 0 || i4 < 0 || i3 >= this.mapWidth || i4 >= this.mapHeight) {
            return false;
        }
        byte b = this.Field931[i4][i3];
        if (b == 0) {
            return true;
        }
        if (b == 15) {
            return false;
        }
        return Field928[b - 1][(i2 % 24) / 12][(i % 24) / 12] == 0;
    }

    public final void Method183(int i, int i2, boolean z) {
        BaseCanvas.g.setColor(16777215);
        BaseCanvas.g.fillRect(0, 0, BaseCanvas.w, BaseCanvas.h);
        int i3 = i / 24;
        int i4 = i2 / 24;
        if (i3 < 0) {
            i3 = 0;
        }
        if (i4 < 0) {
            i4 = 0;
        }
        int i5 = i4 + (BaseCanvas.h / 24) + 2;
        int i6 = i3 + (BaseCanvas.w / 24) + 2;
        if (i5 > this.mapHeight) {
            i5 = this.mapHeight;
        }
        if (i6 > this.mapWidth) {
            i6 = this.mapWidth;
        }
        BaseCanvas.g.translate(-i, -i2);
        for (int i7 = 0; i7 < this.Field929; i7++) {
            for (int i8 = i4; i8 < i5; i8++) {
                for (int i9 = i3; i9 < i6; i9++) {
                    if (this.Field932[this.Field930[i7][i8][i9] >> 4] != null) {
                        BaseCanvas.g.drawRegion(this.Field932[this.Field930[i7][i8][i9] >> 4], ((this.Field930[i7][i8][i9] & 15) - 1) * 24, 0, 24, 24, 0, i9 * 24, i8 * 24, 0);
                    } else {
                        this.Field932[this.Field930[i7][i8][i9] >> 4] = Method185(new StringBuffer().append(this.Field933[this.Field930[i7][i8][i9] >> 4]).toString());
                    }
                }
            }
        }
        if (z) {
            this.Field943.x = i;
            this.Field943.y = i2;
            for (int i10 = 0; i10 < this.xObject.length; i10++) {
                Class131 class131 = this.Field941[this.Field938[i10]];
                Rectangle Method207 = class131.getPosition();
                Method207.x += this.xObject[i10];
                Method207.y += this.yObject[i10] - this.Field940[i10];
                if (this.Field943.intersect(Method207)) {
                    class131.Field859 = this.Field939[i10];
                    class131.draw(this.xObject[i10], this.yObject[i10] - this.Field940[i10]);
                }
            }
        }
        BaseCanvas.g.translate(i, i2);
    }

    private static int Method184(int i) {
        switch (i) {
            case 1:
            case 6:
            case 7:
            case 10:
                return 3;
            case 2:
            case 3:
            case 4:
            case 5:
            case 9:
            default:
                return 1;
            case 8:
                return 2;
        }
    }

    private static Image Method185(String str) {
        try {
            return Image.createImage(new StringBuffer("/newMapData/").append(str).append(".png").toString());
        } catch (IOException unused) {
            return null;
        }
    }

    private static SkillAnimation Method186(String str) {
        try {
            Image createImage = Image.createImage(new StringBuffer("/newMapData/").append(str).append("_a.png").toString());
            DataInputStream dataInputStream = new DataInputStream(ResourceManager.getResource(new StringBuffer("/newMapData/").append(str).append("_b").toString()));
            SkillAnimation class188 = new SkillAnimation();
            class188.read(dataInputStream);
            for (int i = 0; i < class188.Field1263.length; i++) {
                class188.Field1263[i].imgSkill = createImage;
            }
            return class188;
        } catch (Exception unused) {
            return null;
        }
    }
}
