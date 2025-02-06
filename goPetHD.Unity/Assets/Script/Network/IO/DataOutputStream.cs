using Gopet.IO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gopet.IO
{
    public class DataOutputStream<T> where T : Stream
    {

        public T BaseStream { get; }

        public DataOutputStream(T baseStream)
        {
            BaseStream = baseStream;
        }

        public void writeSByte(sbyte value)
        {
            BaseStream.WriteByte(value.toByte());
        }

        public void writeSByte(int value)
        {
            BaseStream.WriteByte((byte)(value & 0xFF));
        }

        public void writeSByteUncheck(sbyte value)
        {
            BaseStream.WriteByte(value.toByte());
        }

        public void writeByte(sbyte value)
        {
            this.writeSByte(value);
        }

        public void writeByte(int value)
        {
            this.writeSByte((sbyte)value);
        }

        public void writeUnsignedByte(byte value)
        {
            this.writeSByte((sbyte)value);
        }

        public void writeUnsignedByte(byte[] value)
        {
            this.checkLenght(value.Length);
            for (int i = 0; i < value.Length; i++)
            {
                this.writeSByteUncheck((sbyte)value[i]);
            }
        }

        public void writeSByte(sbyte[] value)
        {
            this.checkLenght(value.Length);
            for (int i = 0; i < value.Length; i++)
            {
                this.writeSByteUncheck(value[i]);
            }
        }

        public void writeShort(short value)
        {
            this.checkLenght(2);
            for (int i = 1; i >= 0; i--)
            {
                this.writeSByteUncheck((sbyte)(value >> i * 8));
            }
        }

        public void writeShort(int value)
        {
            this.checkLenght(2);
            short num = (short)value;
            for (int i = 1; i >= 0; i--)
            {
                this.writeSByteUncheck((sbyte)(num >> i * 8));
            }
        }

        public void writeUnsignedShort(ushort value)
        {
            this.checkLenght(2);
            for (int i = 1; i >= 0; i--)
            {
                this.writeSByteUncheck((sbyte)(value >> i * 8));
            }
        }

        public void writeInt(int value)
        {
            this.checkLenght(4);
            for (int i = 3; i >= 0; i--)
            {
                this.writeSByteUncheck((sbyte)(value >> i * 8));
            }
        }

        public void writeLong(long value)
        {
            this.checkLenght(8);
            for (int i = 7; i >= 0; i--)
            {
                this.writeSByteUncheck((sbyte)(value >> i * 8));
            }
        }

        public void writeBoolean(bool value)
        {
            this.writeSByte((!value) ? (sbyte)0 : (sbyte)1);
        }

        public void writeBool(bool value)
        {
            this.writeSByte((!value) ? (sbyte)0 : (sbyte)1);
        }

        public void writeString(string value)
        {
            char[] array = value.ToCharArray();
            this.writeShort((short)array.Length);
            this.checkLenght(array.Length);
            for (int i = 0; i < array.Length; i++)
            {
                this.writeSByteUncheck((sbyte)array[i]);
            }
        }

        public void writeUTF(string value)
        {
            Encoding unicode = Encoding.Unicode;
            Encoding encoding = Encoding.GetEncoding(65001);
            byte[] bytes = unicode.GetBytes(value);
            byte[] array = Encoding.Convert(unicode, encoding, bytes);
            this.writeShort((short)array.Length);
            this.checkLenght(array.Length);
            foreach (sbyte value2 in array)
            {
                this.writeSByteUncheck(value2);
            }
        }

        public void write(sbyte value)
        {
            this.writeSByte(value);
        }

        public void write(sbyte[] value)
        {
            this.writeSByte(value);
        }


        public void checkLenght(int ltemp)
        {

        }





        public byte convertSbyteToByte(sbyte var)
        {
            return var.toByte();
        }

        public byte[] convertSbyteToByte(sbyte[] var)
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

        }

        public void close()
        {

        }

        internal void flush()
        {

        }

        private int posWrite;

    }
}
