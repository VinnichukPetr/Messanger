using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessangerLibrary.Chat
{
    public class ParsingContent
    {
        static public string WriteParser(List<string> content)
        {
            string buf = "";

            foreach (var item in content)
            {
                buf += (item + '|');
            }

            return buf;
        }

        static public List<string> ReadParser(string content) => content.Split('|').ToList();
    }
}
