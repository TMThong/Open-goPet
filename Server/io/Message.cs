
public   class Message {

    public int id;
    private ByteArrayOutputStream baos;
    private DataOutputStream dos;
    private DataInputStream dis;
    public bool isEncrypted;
    public static bool isiWin = false;

    public Message(int command) {
        this(command, true);
    }

    public Message(int command, bool isEncrypted) {
        this.isEncrypted = false;
        this.id = command;
        this.isEncrypted = isEncrypted;
    }

    public Message(byte[] data)   {
        this.isEncrypted = false;
        byte[] msgData = new byte[data.length - 1];
        System.arraycopy(data, 1, msgData, 0, msgData.length);
        this.id = data[0];
        this.dis = new DataInputStream(new ByteArrayInputStream(msgData));
    }

    public byte[] getBuffer() {
        if (this.baos == null) {
            return new byte[]{(byte) this.id};
        } else {
            byte[] data = this.baos.toByteArray();
            byte[] buffer;
            if (isiWin) {
                buffer = new byte[data.length + 3];
                buffer[0] = 40;
                buffer[1] = (byte) (this.id >>> 8 & 255);
                buffer[2] = (byte) (this.id >>> 0 & 255);
                System.arraycopy(data, 0, buffer, 3, data.length);
                return buffer;
            } else {
                buffer = new byte[data.length + 1];
                buffer[0] = (byte) this.id;
                System.arraycopy(data, 0, buffer, 1, data.length);
                return buffer;
            }
        }
    }

    public DataInputStream reader() {
        return this.dis;
    }

    public DataOutputStream writer() {
        if (this.baos == null) {
            this.baos = new ByteArrayOutputStream();
            this.dos = new DataOutputStream(this.baos);
        }

        return this.dos;
    }

    public void putByte(int value)   {

        this.writer().writeByte(value);

    }

    public void putString(String text)   {

        this.writer().writeUTF(text);

    }

    public void putUTF(String text)   {

        this.writer().writeUTF(text);

    }

    public void putInt(int value)   {

        this.writer().writeInt(value);

    }

    public void putbool(bool value)   {

        this.writer().writebool(value);

    }

    public void putShort(int value)   {

        this.writer().writeShort(value);

    }

    public void putLong(long value)   {

        this.writer().writeLong(value);

    }

    public void cleanup() {
        try {
            if (this.dis != null) {
                this.dis.close();
            }

            if (this.dos != null) {
                this.dos.close();
            }
        } catch (IOException var2) {
        }
    }

    public byte readByte()   {
        return reader().readByte();
    }

    public short readShort()   {
        return reader().readShort();
    }

    public int readUnsignedByte()   {
        return reader().readUnsignedByte();
    }

    public int readUnsignedShort()   {
        return reader().readUnsignedShort();
    }

    public String readUTF()   {
        return reader().readUTF();
    }

    public long readLong()   {
        return reader().readLong();
    }

    public float readFloat()   {
        return reader().readFloat();
    }

    public double readDouble()   {
        return reader().readDouble();
    }

    public int readInt()   {
        return reader().readInt();
    }

    public void close() {
        cleanup();
        baos = null;
    }
}
