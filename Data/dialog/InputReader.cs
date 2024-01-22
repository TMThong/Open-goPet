 
public class InputReader {

    public const byte FIELD_STRING = 0;
    public const byte FIELD_BYTE = 1;
    public const byte FIELD_INT = 2;
    public const byte FIELD_FLOAT = 3;
    public const byte FIELD_DOBUBLE = 4;
    public const byte FIELD_LONG = 5;
    public const byte FIELD_BIGINT = 6;

    private byte[] inputType;
    private HashMap<@NonNull Integer, @NonNull Object> listField = new HashMap<>();

    public InputReader(byte[] inputType, String[] str) {
        this.inputType = inputType;
        load(str);
    }

    private void load(String[] strings) {
        if (strings.length != inputType.length) {
            throw new ArrayIndexOutOfBoundsException();
        }

        for (int i = 0; i < inputType.length; i++) {
            switch (inputType[i]) {
                case FIELD_BYTE:
                    listField.put(i, Byte.parseByte(strings[i]));
                    break;
                case FIELD_STRING:
                    listField.put(i, strings[i]);
                    break;
                case FIELD_INT:
                    listField.put(i, Integer.parseInt(strings[i]));
                    break;
                case FIELD_FLOAT:
                    listField.put(i, Float.parseFloat(strings[i]));
                    break;
                case FIELD_DOBUBLE:
                    listField.put(i, Double.parseDouble(strings[i]));
                    break;
                case FIELD_LONG:
                    listField.put(i, Long.parseLong(strings[i]));
                    break;
                case FIELD_BIGINT:
                    listField.put(i, new BigDecimal(strings[i]));
                    break;
            }
        }
    }

    public Object readObject(int index)   {
        if (listField.containsKey(index)) {
            return listField.get(index);
        }
        throw new NullPointerException("List filed not have field " + index);
    }

    public int readInt(int index)   {
        return (int) readObject(index);
    }
    
    public byte readByte(int index)   {
        return (byte) readObject(index);
    }

    public long readLong(int index)   {
        return (long) readObject(index);
    }

    public float readFloat(int index)   {
        return (float) readObject(index);
    }

    public double readDouble(int index)   {
        return (double) readObject(index);
    }

    public String readString(int index)   {
        return (String) readObject(index);
    }

    public BigDecimal readBigInt(int index)   {
        return (BigDecimal) readObject(index);
    }
}
