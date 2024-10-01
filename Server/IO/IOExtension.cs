using Gopet.Server.IO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gopet.IO
{
    public static class IOExtension
    {
        public static sbyte toSbyte(this byte value)
        {
            if (value <= sbyte.MaxValue)
            {
                return (sbyte)value;
            }
            return (sbyte)(value & 255);
        }

        public static sbyte[] sbytes(this byte[] data)
        {
            if (data == null)
                throw new NullReferenceException("data");

            sbyte[] buffer = new sbyte[data.Length];
            for (int i = 0; i < data.Length; i++)
            {
                buffer[i] = data[i].toSbyte();
            }

            return buffer;
        }

        public static byte[] bytes(this sbyte[] data)
        {
            if (data == null)
                throw new NullReferenceException("data");

            byte[] buffer = new byte[data.Length];
            for (int i = 0; i < data.Length; i++)
            {
                buffer[i] = data[i].toByte();
            }

            return buffer;
        }

        public static byte toByte(this sbyte value)
        {
            if (value >= 0) return (byte)(value);

            return (byte)(value & 255);
        }

        public static void Write(this BinaryWriter writer, sbyte[] buffer)
        {
            if (writer == null)
                throw new ArgumentNullException();
            for (int i = 0; i < buffer.Length; i++)
            {
                writer.Write(buffer[i]);
            }
            writer.Flush();
        }

        public static void WriteInt(this BinaryWriter writer, int value)
        {
            if (writer == null)
                throw new ArgumentNullException();
            sbyte[] buffer = new sbyte[4];
            buffer[0] = (sbyte)((value >> 24) & 0xFF);
            buffer[1] = (sbyte)((value >> 16) & 0xFF);
            buffer[2] = (sbyte)((value >> 8) & 0xFF);
            buffer[3] = (sbyte)((value) & 0xFF);
            writer.Write(buffer);
        }

        public static int ReadJavaInt(this BinaryReader reader)
        {
            if (reader == null)
                throw new ArgumentNullException();
            int ch1 = reader.ReadByte();
            int ch2 = reader.ReadByte();
            int ch3 = reader.ReadByte();
            int ch4 = reader.ReadByte();
            if ((ch1 | ch2 | ch3 | ch4) < 0)
                throw new IOException("EOFException");
            return ((ch1 << 24) + (ch2 << 16) + (ch3 << 8) + (ch4 << 0));
        }

         

        public static short ReadJavaShort(this BinaryReader reader)
        {
            if (reader == null)
                throw new ArgumentNullException();
            int ch1 = reader.ReadByte();
            int ch2 = reader.ReadByte();
            if ((ch1 | ch2) < 0)
                throw new IOException("EOFException");
            return (short)((ch1 << 8) + (ch2 << 0));
        }
    }
}
