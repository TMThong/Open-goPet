using Gopet.Data.Collections;
using System.Numerics;

namespace Gopet.Data.Dialog
{
    public class InputReader
    {

        public const sbyte FIELD_STRING = 0;
        public const sbyte FIELD_SBYTE = 1;
        public const sbyte FIELD_INT = 2;
        public const sbyte FIELD_FLOAT = 3;
        public const sbyte FIELD_DOBUBLE = 4;
        public const sbyte FIELD_LONG = 5;
        public const sbyte FIELD_BIGINT = 6;

        private sbyte[] inputType;
        private HashMap<int, object> listField = new();

        public InputReader(sbyte[] inputType, string[] str)
        {
            this.inputType = inputType;
            load(str);
        }

        private void load(string[] strings)
        {
            if (strings.Length != inputType.Length)
            {
                throw new IndexOutOfRangeException();
            }

            for (int i = 0; i < inputType.Length; i++)
            {
                switch (inputType[i])
                {
                    case FIELD_SBYTE:
                        listField.put(i, sbyte.Parse(strings[i]));
                        break;
                    case FIELD_STRING:
                        listField.put(i, strings[i]);
                        break;
                    case FIELD_INT:
                        listField.put(i, int.Parse(strings[i]));
                        break;
                    case FIELD_FLOAT:
                        listField.put(i, float.Parse(strings[i]));
                        break;
                    case FIELD_DOBUBLE:
                        listField.put(i, double.Parse(strings[i]));
                        break;
                    case FIELD_LONG:
                        listField.put(i, long.Parse(strings[i]));
                        break;
                    case FIELD_BIGINT:
                        listField.put(i, BigInteger.Parse(strings[i]));
                        break;
                }
            }
        }

        public object readObject(int index)
        {
            if (listField.ContainsKey(index))
            {
                return listField.get(index);
            }
            throw new NullReferenceException("List filed not have field " + index);
        }

        public int readInt(int index)
        {
            return (int)readObject(index);
        }

        public sbyte readsbyte(int index)
        {
            return (sbyte)readObject(index);
        }

        public long readlong(int index)
        {
            return (long)readObject(index);
        }

        public float readFloat(int index)
        {
            return (float)readObject(index);
        }

        public double readDouble(int index)
        {
            return (double)readObject(index);
        }

        public string readString(int index)
        {
            return (string)readObject(index);
        }

        public BigInteger readBigInt(int index)
        {
            return (BigInteger)readObject(index);
        }
    }
}