using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatPad
{
    internal struct ChatMessage
    {
        public string name;
        public string text;
        public string[] tags;
        public string id;

        public ChatMessage(string msg)
        {
            tags = msg.Split(new char[] { ':' }, 2)[0].Split(';');
            id = tags[8].Split('=')[1];
            string[] data = msg.Split(':');
            name = data[1].Split('!')[0];
            text = data[2].Trim();
            Console.WriteLine(name);
        }

        public ChatMessage(string msg, bool test)
        {
            if (test)
            {
                tags = new string[0];
                string[] data = msg.Split(new string[] { "PRIVMSG" }, 2, StringSplitOptions.None)[1].Split(new char[] { ':' }, 2);
                name = data[0].Trim();
                text = data[1].Trim();
                id = "";
            }
            else
            {
                tags = msg.Split(new char[] { ':' }, 2)[0].Split(';');
                id = tags[8].Split('=')[1];
                string[] data = msg.Split(new string[] { "PRIVMSG" }, 2, StringSplitOptions.None)[1].Split(new char[] { ':' }, 2);
                name = data[0].Trim();
                text = data[1].Trim();
            }
        }
    }
}
