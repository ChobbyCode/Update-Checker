using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Update_Checker
{
    internal class Parser
    {
        internal string[] parseVersion(string version)
        {
            String[]? versionParsed = { null, null, null };

            string versionParsable = version + ".";

            string letter = "";
            string word = "";
            int charLet = 1;
            while (letter != ".")
            {
                word = word + letter;

                letter = versionParsable[charLet].ToString();
                charLet += 1;
            }
            versionParsed[0] = word;

            word = "";
            letter = "";
            while (letter != ".")
            {
                word = word + letter;

                letter = versionParsable[charLet].ToString();
                charLet += 1;
            }
            versionParsed[1] = word;

            word = "";
            letter = "";
            while (letter != ".")
            {
                word = word + letter;

                letter = versionParsable[charLet].ToString();
                charLet += 1;
            }
            versionParsed[2] = word;

            return versionParsed;
        }
    }
}
