
public class Message
{

    public int id;
    private sbyteArrayOutputStream baos;
    private DataOutputStream dos;
    private DataInputStream dis;
    public bool isEncrypted;
    public static bool isiWin = false;

    public Message(int command)
    {
        this(command, true);
    }

    public Message(int command, bool isEncrypted)
    {
        this.isEncrypted = false;
        this.id = command;
        this.isEncrypted = isEncrypted;
    }

    public Message(sbyte[] data)
    {
        this.isEncrypted = false;
        sbyte[] msgData = new sbyte[data.Length - 1];
        System.arraycopy(data, 1, msgData, 0, msgData.Length);
        this.id = data[0];
        this.dis = new DataInputStream(new sbyteArrayInputStream(msgData));
    }

    public sbyte[] getBuffer()
    {
        if (this.baos == null)
        {
            return new sbyte[] { (sbyte)this.id };
        }
        else
        {
            sbyte[] data = this.baos.tosbyteArray();
            sbyte[] buffer;
            if (isiWin)
            {
                buffer = new sbyte[data.Length + 3];
                buffer[0] = 40;
                buffer[1] = (sbyte)(this.id >>> 8 & 255);
                buffer[2] = (sbyte)(this.id >>> 0 & 255);
                System.arraycopy(data, 0, buffer, 3, data.Length);
                return buffer;
            }
            else
            {
                buffer = new sbyte[data.Length + 1];
                buffer[0] = (sbyte)this.id;
                System.arraycopy(data, 0, buffer, 1, data.Length);
                return buffer;
            }
        }
    }

    public DataInputStream reader()
    {
        return this.dis;
    }

    public DataOutputStream writer()
    {
        if (this.baos == null)
        {
            this.baos = new sbyteArrayOutputStream();
            this.dos = new DataOutputStream(this.baos);
        }

        return this.dos;
    }

    public void putsbyte(int value)
    {

        this.writer().writesbyte(value);

    }

    public void putString(String text)
    {

        this.writer().writeUTF(text);

    }

    public void putUTF(String text)
    {

        this.writer().writeUTF(text);

    }

    public void putInt(int value)
    {

        this.writer().writeInt(value);

    }

    public void putbool(bool value)
    {

        this.writer().writebool(value);

    }

    public void putShort(int value)
    {

        this.writer().writeShort(value);

    }

    public void putLong(long value)
    {

        this.writer().writeLong(value);

    }

    public void cleanup()
    {
        try
        {
            if (this.dis != null)
            {
                this.dis.close();
            }

            if (this.dos != null)
            {
                this.dos.close();
            }
        }
        catch (IOException var2)
        {
        }
    }

    public sbyte readsbyte()
    {
        return reader().readsbyte();
    }

    public short readShort()
    {
        return reader().readShort();
    }

    public int readUnsignedsbyte()
    {
        return reader().readUnsignedsbyte();
    }

    public int readUnsignedShort()
    {
        return reader().readUnsignedShort();
    }

    public String readUTF()
    {
        return reader().readUTF();
    }

    public long readLong()
    {
        return reader().readLong();
    }

    public float readFloat()
    {
        return reader().readFloat();
    }

    public double readDouble()
    {
        return reader().readDouble();
    }

    public int readInt()
    {
        return reader().readInt();
    }

    public void close()
    {
        cleanup();
        baos = null;
    }
}
