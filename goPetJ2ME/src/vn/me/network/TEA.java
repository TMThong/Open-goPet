package vn.me.network;

/* renamed from: Class164  reason: default package */
 /* loaded from: gopet_repackage.jar:Class164.class */
public final class TEA {

    private static final int SUGAR = -1640531527;
    private static final int CUPS = 32;
    private static final int UNSUGAR = -957401312;
    private int[] S = new int[4];

    public TEA(byte[] key) {
        int off = 0;

        for (int i = 0; i < 4; ++i) {
            this.S[i] = key[off++] & 255 | (key[off++] & 255) << 8 | (key[off++] & 255) << 16 | (key[off++] & 255) << 24;
        }

    }

    public TEA(long longKey) {
        byte[] key = new byte[16];
        generateKey(longKey, key);
        int off = 0;

        for (int i = 0; i < 4; ++i) {
            this.S[i] = key[off++] & 255 | (key[off++] & 255) << 8 | (key[off++] & 255) << 16 | (key[off++] & 255) << 24;
        }

    }

    public static void generateKey(long value, byte[] array) {
        int offset = 0;
        array[offset] = (byte) ((int) (255L & value >> 56));
        array[offset + 1] = (byte) ((int) (255L & value >> 48));
        array[offset + 2] = (byte) ((int) (255L & value >> 40));
        array[offset + 3] = (byte) ((int) (255L & value >> 32));
        array[offset + 4] = (byte) ((int) (255L & value >> 24));
        array[offset + 5] = (byte) ((int) (255L & value >> 16));
        array[offset + 6] = (byte) ((int) (255L & value >> 8));
        array[offset + 7] = (byte) ((int) (255L & value));
        array[offset + 8] = (byte) ((int) (255L & value >> 56));
        array[offset + 9] = (byte) ((int) (255L & value >> 48));
        array[offset + 10] = (byte) ((int) (255L & value >> 40));
        array[offset + 11] = (byte) ((int) (255L & value >> 32));
        array[offset + 12] = (byte) ((int) (255L & value >> 24));
        array[offset + 13] = (byte) ((int) (255L & value >> 16));
        array[offset + 14] = (byte) ((int) (255L & value >> 8));
        array[offset + 15] = (byte) ((int) (255L & value));
    }

    public byte[] encrypt(byte[] clear) {
        int paddedSize = (clear.length >> 3) + (clear.length % 8 == 0 ? 0 : 1) << 1;
        int[] buffer = new int[paddedSize + 1];
        buffer[0] = clear.length;
        this.pack(clear, buffer, 1);
        this.brew(buffer);
        return this.unpack(buffer, 0, buffer.length << 2);
    }

    public byte[] decrypt(byte[] crypt) {
        if (crypt.length % 4 == 0 && (crypt.length >> 2) % 2 == 1) {
            int[] buffer = new int[crypt.length >> 2];
            this.pack(crypt, buffer, 0);
            this.unbrew(buffer);
            return this.unpack(buffer, 1, buffer[0]);
        } else {
            return null;
        }
    }

    void brew(int[] buf) {
        if (buf.length % 2 == 1) {
            for (int i = 1; i < buf.length; i += 2) {
                int n = 32;
                int v0 = buf[i];
                int v1 = buf[i + 1];

                for (int sum = 0; n-- > 0; v1 += ((v0 << 4) + this.S[2] ^ v0) + (sum ^ v0 >>> 5) + this.S[3]) {
                    sum -= 1640531527;
                    v0 += ((v1 << 4) + this.S[0] ^ v1) + (sum ^ v1 >>> 5) + this.S[1];
                }

                buf[i] = v0;
                buf[i + 1] = v1;
            }
        }

    }

    void unbrew(int[] buf) {
        if (buf.length % 2 == 1) {
            for (int i = 1; i < buf.length; i += 2) {
                int n = 32;
                int v0 = buf[i];
                int v1 = buf[i + 1];

                for (int sum = -957401312; n-- > 0; sum += 1640531527) {
                    v1 -= ((v0 << 4) + this.S[2] ^ v0) + (sum ^ v0 >>> 5) + this.S[3];
                    v0 -= ((v1 << 4) + this.S[0] ^ v1) + (sum ^ v1 >>> 5) + this.S[1];
                }

                buf[i] = v0;
                buf[i + 1] = v1;
            }
        }

    }

    void pack(byte[] src, int[] dest, int destOffset) {
        if (destOffset + (src.length >> 2) <= dest.length) {
            int i = 0;
            int shift = 24;
            int j = destOffset;

            for (dest[destOffset] = 0; i < src.length; ++i) {
                dest[j] |= (src[i] & 255) << shift;
                if (shift == 0) {
                    shift = 24;
                    ++j;
                    if (j < dest.length) {
                        dest[j] = 0;
                    }
                } else {
                    shift -= 8;
                }
            }
        }

    }

    byte[] unpack(int[] src, int srcOffset, int destLength) {
        if (destLength <= src.length - srcOffset << 2) {
            byte[] dest = new byte[destLength];
            int i = srcOffset;
            int count = 0;

            for (int j = 0; j < destLength; ++j) {
                dest[j] = (byte) (src[i] >> 24 - (count << 3) & 255);
                ++count;
                if (count == 4) {
                    count = 0;
                    ++i;
                }
            }

            return dest;
        } else {
            return null;
        }
    }
}
