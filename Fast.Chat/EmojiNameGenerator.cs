using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using EmojiSharp;

namespace Fast.Chat
{
    public class EmojiNameGenerator
    {
        static Random rnd;
        static KeyValuePair<string, Emoji>[] emojis;
        static EmojiNameGenerator()
        {
            rnd = new Random();

            emojis = Emoji.All.ToArray();
        }
        private static string GetName()
        {

            Emoji randomVariable = emojis[rnd.Next(emojis.Length)].Value;

            string hexString = randomVariable.Unified;
            int num = Int32.Parse(hexString, System.Globalization.NumberStyles.HexNumber);

            return "&#"+num+";";
        }

        public static string GetNEmojis(int n)
        {
            string name = "";
            for(int i=0; i<n; i++)
            {
                name += GetName();
            }
            return name;
        }

        
    }
}
