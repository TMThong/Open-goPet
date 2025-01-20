using Gopet.IO;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gopet.Server.IO
{
    internal class ListWriterMessage
    {
        public Message[] Messages { get; set; }
        public Message this[int index]
        {
            get
            {
                return Messages[index];
            }
        }

        public ListWriterMessage(int count, int command)
        {
            if (count <= 0)
                throw new IndexOutOfRangeException("count");
            Messages = new Message[count];
            for (int i = 0; i < Messages.Length; i++)
            {
                Messages[i] = new Message(command);
            }
        }

        public void putsbyte(int value)
        {
            foreach (var item in Messages)
            {
                item.putsbyte(value);
            }
        }

        public void putString(string text)
        {
            foreach (var item in Messages)
            {
                item.putString(text);
            }
        }

        public void putUTF(string text)
        {
            foreach (var item in Messages)
            {
                item.putUTF(text);
            }
        }

        public void putInt(int value)
        {
            foreach (var item in Messages)
            {
                item.putInt(value);
            }
        }

        public void putbool(bool value)
        {
            foreach (var item in Messages)
            {
                item.putbool(value);
            }
        }

        public void putShort(int value)
        {
            foreach (var item in Messages)
            {
                item.putShort(value);
            }

        }

        public void putlong(long value)
        {
            foreach (var item in Messages)
            {
                item.putlong(value);
            }
        }

        public void cleanup()
        {
            foreach (var item in Messages)
            {
                item.cleanup();
            }
        }
    }
}
