using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gopet.IO
{
    public class DataInputStream
    {
        public DataInputStream()
        {

        }

        public DataInputStream(sbyte[] data)
        {
            this.buffer = data;
        }

        public sbyte readSByte()
        {
            if (this.posRead < this.buffer.Length)
            {
                return this.buffer[this.posRead++];
            }
            this.posRead = this.buffer.Length;
            return 0;
        }

        public sbyte readsbyte()
        {
            return this.readSByte();
        }

        public sbyte readByte()
        {
            return this.readSByte();
        }

        public void mark(int readlimit)
        {
            this.posMark = this.posRead;
        }

        public void reset()
        {
            this.posRead = this.posMark;
        }

        public byte readUnsignedByte()
        {
            return convertSbyteToByte(this.readSByte());
        }

        public short readShort()
        {
            short num = 0;
            for (int i = 0; i < 2; i++)
            {
                num = (short)(num << 8);
                num |= (short)(255 & (int)this.buffer[this.posRead++]);
            }
            return num;
        }

        public ushort readUnsignedShort()
        {
            ushort num = 0;
            for (int i = 0; i < 2; i++)
            {
                num = (ushort)(num << 8);
                num |= (ushort)(255 & (int)this.buffer[this.posRead++]);
            }
            return num;
        }

        public int readInt()
        {
            int num = 0;
            for (int i = 0; i < 4; i++)
            {
                num <<= 8;
                num |= (255 & (int)this.buffer[this.posRead++]);
            }
            return num;
        }

        public long readlong()
        {
            long num = 0L;
            for (int i = 0; i < 8; i++)
            {
                num <<= 8;
                num |= (long)(255 & (int)this.buffer[this.posRead++]);
            }
            return num;
        }

        public bool readBool()
        {
            return (int)this.readSByte() > 0;
        }

        public bool readBoolean()
        {
            return (int)this.readSByte() > 0;
        }

        public string readString()
        {
            short num = this.readShort();
            byte[] array = new byte[(int)num];
            for (int i = 0; i < (int)num; i++)
            {
                array[i] = convertSbyteToByte(this.readSByte());
            }
            UTF8Encoding utf8Encoding = new UTF8Encoding();
            return utf8Encoding.GetString(array);
        }

        public string readStringUTF()
        {
            short num = this.readShort();
            byte[] array = new byte[(int)num];
            for (int i = 0; i < (int)num; i++)
            {
                array[i] = convertSbyteToByte(this.readSByte());
            }
            UTF8Encoding utf8Encoding = new UTF8Encoding();
            return utf8Encoding.GetString(array);
        }

        public string readUTF()
        {
            return this.readStringUTF();
        }

        public int read()
        {
            if (this.posRead < this.buffer.Length)
            {
                return (int)this.readSByte();
            }
            return -1;
        }

        public int read(ref sbyte[] data)
        {
            if (data == null)
            {
                return 0;
            }
            int num = 0;
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = this.readSByte();
                if (this.posRead > this.buffer.Length)
                {
                    return -1;
                }
                num++;
            }
            return num;
        }

        public int readz(sbyte[] data)
        {
            if (data == null)
            {
                return 0;
            }
            int num = 0;
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = this.readSByte();
                if (this.posRead > this.buffer.Length)
                {
                    return -1;
                }
                num++;
            }
            return num;
        }

        public void readFully(ref sbyte[] data)
        {
            if (data == null || data.Length + this.posRead > this.buffer.Length)
            {
                return;
            }
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = this.readSByte();
            }
        }

        public int available()
        {
            return this.buffer.Length - this.posRead;
        }

        public static byte convertSbyteToByte(sbyte var)
        {
            return var.toByte();
        }

        public static byte[] convertSbyteToByte(sbyte[] var)
        {
            byte[] array = new byte[var.Length];
            for (int i = 0; i < var.Length; i++)
            {
                array[i] = var[i].toByte();
            }
            return array;
        }

        public void Close()
        {
            this.buffer = null;
        }

        public void close()
        {
            this.buffer = null;
        }

        public bool readbool()
        {
            return readBoolean();
        }

        public sbyte[] buffer;

        private int posRead;

        private int posMark;
    }
}
