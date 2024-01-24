namespace Gopet.IO
{
    public class Message
    {

        public sbyte id;

        private DataOutputStream dos;
        private DataInputStream dis;
        public bool isEncrypted;
        public static bool isiWin = false;

        public Message(int command) : this(command, false)
        {

        }

        public Message(int command, bool isEncrypted)
        {
            this.isEncrypted = false;
            id = (sbyte)(command & 0xFF);
            this.isEncrypted = isEncrypted;
        }

        public Message(sbyte[] data)
        {
            isEncrypted = false;
            sbyte[] msgData = new sbyte[data.Length - 1];
            Buffer.BlockCopy(data, 1, msgData, 0, msgData.Length);
            id = data[0];
            dis = new DataInputStream(msgData);
        }

        public sbyte[] getBuffer()
        {
            if (dos == null)
            {
                return new sbyte[] { id };
            }
            else
            {
                sbyte[] data = dos.getData();
                sbyte[] buffer;
                if (isiWin)
                {
                    buffer = new sbyte[data.Length + 3];
                    buffer[0] = 40;
                    buffer[1] = (sbyte)(id >>> 8 & 255);
                    buffer[2] = (sbyte)(id >>> 0 & 255);
                    Buffer.BlockCopy(data, 0, buffer, 3, data.Length);
                    return buffer;
                }
                else
                {
                    buffer = new sbyte[data.Length + 1];
                    buffer[0] = id;
                    Buffer.BlockCopy(data, 0, buffer, 1, data.Length);
                    return buffer;
                }
            }
        }

        public DataInputStream reader()
        {
            return dis;
        }

        public DataOutputStream writer()
        {
            if (dos == null)
            {
                dos = new DataOutputStream();
            }

            return dos;
        }

        public void putsbyte(int value)
        {

            writer().writeByte(value);

        }

        public void putString(string text)
        {

            writer().writeUTF(text);

        }

        public void putUTF(string text)
        {

            writer().writeUTF(text);

        }

        public void putInt(int value)
        {

            writer().writeInt(value);

        }

        public void putbool(bool value)
        {

            writer().writeBool(value);

        }

        public void putShort(int value)
        {

            writer().writeShort(value);

        }

        public void putlong(long value)
        {

            writer().writeLong(value);

        }

        public void cleanup()
        {
            try
            {
                if (dis != null)
                {
                    dis.Close();
                }

                if (dos != null)
                {
                    dos.Close();
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
            return reader().readUnsignedByte();
        }

        public int readUnsignedShort()
        {
            return reader().readUnsignedShort();
        }

        public string readUTF()
        {
            return reader().readUTF();
        }

        public long readlong()
        {
            return reader().readlong();
        }





        public int readInt()
        {
            return reader().readInt();
        }

        public void Close()
        {
            cleanup();
        }
    }
}