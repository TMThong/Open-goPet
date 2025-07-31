package defpackage;

import java.io.DataInputStream;
import java.io.IOException;

/* renamed from: Class188  reason: default package */
/* loaded from: gopet_repackage.jar:Class188.class */
public final class SkillAnimation {
    public Class186[] Field1263;
    public Class189[] Field1264;
    public Class190[] Field1265;
    private int Field1266;

    public final void read(DataInputStream dataInputStream) {
        try {
            this.Field1266 = dataInputStream.readByte();
            int readByte = dataInputStream.readByte();
            this.Field1265 = new Class190[readByte];
            for (int i = 0; i < readByte; i++) {
                this.Field1265[i] = new Class190();
                this.Field1265[i].Field1272 = dataInputStream.readInt();
                this.Field1265[i].Field1273 = dataInputStream.readInt();
                this.Field1265[i].Field1274 = dataInputStream.readInt();
                this.Field1265[i].Field1275 = dataInputStream.readInt();
            }
            int readByte2 = dataInputStream.readByte();
            this.Field1264 = new Class189[readByte2];
            for (int i2 = 0; i2 < readByte2; i2++) {
                this.Field1264[i2] = new Class189();
                Class189 class189 = this.Field1264[i2];
                dataInputStream.readByte();
                int readByte3 = dataInputStream.readByte();
                class189.Field1267 = new byte[readByte3];
                class189.Field1268 = new int[readByte3];
                class189.Field1269 = new int[readByte3];
                class189.Field1270 = new byte[readByte3];
                for (int i3 = 0; i3 < readByte3; i3++) {
                    class189.Field1267[i3] = dataInputStream.readByte();
                    class189.Field1268[i3] = dataInputStream.readInt();
                    class189.Field1269[i3] = dataInputStream.readInt();
                    class189.Field1270[i3] = dataInputStream.readByte();
                }
                class189.Field1271 = new int[this.Field1266][3];
                int readByte4 = dataInputStream.readByte();
                for (int i4 = 0; i4 < readByte4; i4++) {
                    byte readByte5 = dataInputStream.readByte();
                    class189.Field1271[readByte5][0] = 1;
                    class189.Field1271[readByte5][1] = dataInputStream.readInt();
                    class189.Field1271[readByte5][2] = dataInputStream.readInt();
                }
            }
            int readByte6 = dataInputStream.readByte();
            this.Field1263 = new Class186[readByte6];
            for (int i5 = 0; i5 < readByte6; i5++) {
                this.Field1263[i5] = new Class186();
                this.Field1263[i5].skillAniData = this;
                Class186 class186 = this.Field1263[i5];
                dataInputStream.readByte();
                class186.Field1252 = dataInputStream.readInt();
                int readByte7 = dataInputStream.readByte();
                class186.Field1253 = readByte7;
                class186.Field1254 = new byte[readByte7];
                class186.Field1255 = new int[readByte7];
                for (int i6 = 0; i6 < readByte7; i6++) {
                    class186.Field1254[i6] = dataInputStream.readByte();
                    class186.Field1255[i6] = dataInputStream.readInt();
                    if (class186.Field1255[i6] == -1) {
                        class186.Field1255[i6] = class186.Field1252;
                    }
                }
            }
            dataInputStream.close();
        } catch (IOException e) {
            e.printStackTrace();
        }
    }
}
