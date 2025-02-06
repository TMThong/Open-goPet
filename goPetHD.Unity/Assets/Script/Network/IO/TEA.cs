namespace Gopet.IO
{

    public class TEA
    {
        private int[] S = new int[4];

        public TEA(sbyte[] key)
        {
            int off = 0;
            for (int i = 0; i < 4; ++i)
            {
                this.S[i] = key[off++] & 255 | (key[off++] & 255) << 8 | (key[off++] & 255) << 16 | (key[off++] & 255) << 24;
            }
        }
        public TEA(long longKey)
        {
            sbyte[] key = new sbyte[16];
            generateKey(longKey, key);
            int off = 0;

            for (int i = 0; i < 4; ++i)
            {
                this.S[i] = key[off++] & 255 | (key[off++] & 255) << 8 | (key[off++] & 255) << 16 | (key[off++] & 255) << 24;
            }

        }

        public static void generateKey(long value, sbyte[] array)
        {
            int offset = 0;
            array[offset] = (sbyte)((int)(255L & value >> 56));
            array[offset + 1] = (sbyte)((int)(255L & value >> 48));
            array[offset + 2] = (sbyte)((int)(255L & value >> 40));
            array[offset + 3] = (sbyte)((int)(255L & value >> 32));
            array[offset + 4] = (sbyte)((int)(255L & value >> 24));
            array[offset + 5] = (sbyte)((int)(255L & value >> 16));
            array[offset + 6] = (sbyte)((int)(255L & value >> 8));
            array[offset + 7] = (sbyte)((int)(255L & value));
            array[offset + 8] = (sbyte)((int)(255L & value >> 56));
            array[offset + 9] = (sbyte)((int)(255L & value >> 48));
            array[offset + 10] = (sbyte)((int)(255L & value >> 40));
            array[offset + 11] = (sbyte)((int)(255L & value >> 32));
            array[offset + 12] = (sbyte)((int)(255L & value >> 24));
            array[offset + 13] = (sbyte)((int)(255L & value >> 16));
            array[offset + 14] = (sbyte)((int)(255L & value >> 8));
            array[offset + 15] = (sbyte)((int)(255L & value));
        }

        public sbyte[] encrypt(sbyte[] clear)
        {
            int paddedSize = (clear.Length >> 3) + (clear.Length % 8 == 0 ? 0 : 1) << 1;
            int[] buffer = new int[paddedSize + 1];
            buffer[0] = clear.Length;
            this.pack(clear, buffer, 1);
            this.brew(buffer);
            return this.unpack(buffer, 0, buffer.Length << 2);
        }

        public sbyte[] decrypt(sbyte[] crypt)
        {
            if (crypt.Length % 4 == 0 && (crypt.Length >> 2) % 2 == 1)
            {
                int[] buffer = new int[crypt.Length >> 2];
                this.pack(crypt, buffer, 0);
                this.unbrew(buffer);
                return this.unpack(buffer, 1, buffer[0]);
            }
            else
            {
                return null;
            }
        }

        void brew(int[] buf)
        {
            if (buf.Length % 2 == 1)
            {
                for (int i = 1; i < buf.Length; i += 2)
                {
                    int n = 32;
                    int v0 = buf[i];
                    int v1 = buf[i + 1];

                    for (int sum = 0; n-- > 0; v1 += ((v0 << 4) + this.S[2] ^ v0) + (sum ^ v0 >>> 5) + this.S[3])
                    {
                        sum -= 1640531527;
                        v0 += ((v1 << 4) + this.S[0] ^ v1) + (sum ^ v1 >>> 5) + this.S[1];
                    }

                    buf[i] = v0;
                    buf[i + 1] = v1;
                }
            }

        }

        void unbrew(int[] buf)
        {
            if (buf.Length % 2 == 1)
            {
                for (int i = 1; i < buf.Length; i += 2)
                {
                    int n = 32;
                    int v0 = buf[i];
                    int v1 = buf[i + 1];

                    for (int sum = -957401312; n-- > 0; sum += 1640531527)
                    {
                        v1 -= ((v0 << 4) + this.S[2] ^ v0) + (sum ^ v0 >>> 5) + this.S[3];
                        v0 -= ((v1 << 4) + this.S[0] ^ v1) + (sum ^ v1 >>> 5) + this.S[1];
                    }

                    buf[i] = v0;
                    buf[i + 1] = v1;
                }
            }

        }

        void pack(sbyte[] src, int[] dest, int destOffset)
        {
            if (destOffset + (src.Length >> 2) <= dest.Length)
            {
                int i = 0;
                int shift = 24;
                int j = destOffset;

                for (dest[destOffset] = 0; i < src.Length; ++i)
                {
                    dest[j] |= (src[i] & 255) << shift;
                    if (shift == 0)
                    {
                        shift = 24;
                        ++j;
                        if (j < dest.Length)
                        {
                            dest[j] = 0;
                        }
                    }
                    else
                    {
                        shift -= 8;
                    }
                }
            }

        }

        sbyte[] unpack(int[] src, int srcOffset, int destLength)
        {
            if (destLength <= src.Length - srcOffset << 2)
            {
                sbyte[] dest = new sbyte[destLength];
                int i = srcOffset;
                int count = 0;

                for (int j = 0; j < destLength; ++j)
                {
                    dest[j] = (sbyte)(src[i] >> 24 - (count << 3) & 255);
                    ++count;
                    if (count == 4)
                    {
                        count = 0;
                        ++i;
                    }
                }

                return dest;
            }
            else
            {
                return null;
            }
        }
    }

}