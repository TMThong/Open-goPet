package vn.me.network;

import java.io.ByteArrayInputStream;
import java.io.ByteArrayOutputStream;
import java.io.DataInputStream;
import java.io.DataOutputStream;
import java.io.IOException;

/* renamed from: Class159  reason: default package */
 /* loaded from: gopet_repackage.jar:message.class */
public final class Message {

    public int id;
    private ByteArrayOutputStream baos;
    private DataOutputStream dos;
    private DataInputStream dis;
    public boolean isEncrypted;
    public static boolean isiWin = false;

    public Message(int i) {
        this(i, true);
    }

    public Message(int i, boolean z) {
        this.isEncrypted = true;
        this.id = i;
        this.isEncrypted = true;
    }

    public Message(byte[] bArr) {
        this.isEncrypted = true;
        byte[] bArr2 = new byte[bArr.length - 1];
        System.arraycopy(bArr, 1, bArr2, 0, bArr2.length);
        this.id = bArr[0];
        this.dis = new DataInputStream(new ByteArrayInputStream(bArr2));
    }

    public final byte[] getBuffer() {
        if (this.baos == null) {
            return new byte[]{(byte) this.id};
        }
        byte[] byteArray = this.baos.toByteArray();
        byte[] bArr = new byte[byteArray.length + 1];
        bArr[0] = (byte) this.id;
        System.arraycopy(byteArray, 0, bArr, 1, byteArray.length);
        return bArr;
    }

    public final DataInputStream reader() {
        return this.dis;
    }

    private DataOutputStream writer() {
        if (this.baos == null) {
            this.baos = new ByteArrayOutputStream();
            this.dos = new DataOutputStream(this.baos);
        }
        return this.dos;
    }

    public final void putByte(int i) {
        try {
            writer().writeByte(i);
        } catch (IOException unused) {
        }
    }

    public final void putString(String str) {
        try {
            writer().writeUTF(str);
        } catch (IOException unused) {
        }
    }

    public final void putInt(int i) {
        try {
            writer().writeInt(i);
        } catch (IOException unused) {
        }
    }

    public final void putBoolean(boolean z) {
        try {
            writer().writeBoolean(z);
        } catch (IOException unused) {
        }
    }

    public void putShort(int value) {
        try {
            this.writer().writeShort(value);
        } catch (IOException var3) {
        }

    }

    public final void cleanup() {
        try {
            if (this.dis != null) {
                this.dis.close();
            }
            if (this.dos != null) {
                this.dos.close();
            }
        } catch (IOException unused) {
        }
    }
}
