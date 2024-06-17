namespace Gopet.IO
{
    public class Message
    {
        public sbyte id;
        private DataOutputStream<MemoryStream> dos;
        private DataInputStream dis;
        public bool isEncrypted;
        public static bool isiWin = false;

        public Message(int command) : this(command, false)
        {

        }

        public Message(int command, bool isEncrypted)
        {
            this.isEncrypted = false;
            this.id = (sbyte)(command & 0xFF);
            this.isEncrypted = isEncrypted;
        }

        public Message(sbyte[] data)
        {
            this.isEncrypted = false;
            sbyte[] msgData = new sbyte[data.Length - 1];
            Buffer.BlockCopy(data, 1, msgData, 0, msgData.Length);
            this.id = data[0];
            this.dis = new DataInputStream(msgData);
        }

        public sbyte[] getBuffer()
        {
            if (this.dos == null)
            {
                return new sbyte[] { (sbyte)this.id };
            }
            else
            {
                sbyte[] data = this.dos.BaseStream.ToArray().sbytes();
                sbyte[] buffer;
                if (isiWin)
                {
                    buffer = new sbyte[data.Length + 3];
                    buffer[0] = 40;
                    buffer[1] = (sbyte)(this.id >>> 8 & 255);
                    buffer[2] = (sbyte)(this.id >>> 0 & 255);
                    Buffer.BlockCopy(data, 0, buffer, 3, data.Length);
                    return buffer;
                }
                else
                {
                    buffer = new sbyte[data.Length + 1];
                    buffer[0] = (sbyte)this.id;
                    Buffer.BlockCopy(data, 0, buffer, 1, data.Length);
                    return buffer;
                }
            }
        }

        public DataInputStream reader()
        {
            return this.dis;
        }

        public DataOutputStream<MemoryStream> writer()
        {
            if (this.dos == null)
            {
                this.dos = new DataOutputStream<MemoryStream>(new MemoryStream());
            }

            return this.dos;
        }

        public void putsbyte(int value)
        {

            this.writer().writeByte(value);

        }

        public void putString(string text)
        {

            this.writer().writeUTF(text);

        }

        public void putUTF(string text)
        {

            this.writer().writeUTF(text);

        }

        public void putInt(int value)
        {

            this.writer().writeInt(value);

        }

        public void putbool(bool value)
        {

            this.writer().writeBool(value);

        }

        public void putShort(int value)
        {

            this.writer().writeShort(value);

        }

        public void putlong(long value)
        {

            this.writer().writeLong(value);

        }

        public void cleanup()
        {
            try
            {
                if (this.dis != null)
                {
                    this.dis.Close();
                }

                if (this.dos != null)
                {
                    this.dos.Close();
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

        public String readUTF()
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