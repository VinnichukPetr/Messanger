using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessangerLibrary
{
    class Parsing
    {
        string WriteParser(List<string> content)
        {
            string buf = "";

            foreach (var item in content)
            {
                buf += (item + '|');
            }

            return buf;
        }

        List<string> ReadParser(string content) => content.Split('|').ToList();
    }
}
