using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AkkaNetExample
{
    public static class LetterPhonetizer
    {
        private static Dictionary<char, string> phoneticLetterMapping = new Dictionary<char, string>();

        static LetterPhonetizer()
        {
            phoneticLetterMapping.Add('A', "Alpha");
            phoneticLetterMapping.Add('B', "Bravo");
            phoneticLetterMapping.Add('C', "Charlie");
            phoneticLetterMapping.Add('D', "Delta");
            phoneticLetterMapping.Add('E', "Echo");
            phoneticLetterMapping.Add('F', "Foxtrot");
            phoneticLetterMapping.Add('G', "Golf");
            phoneticLetterMapping.Add('H', "Hotel");
            phoneticLetterMapping.Add('I', "India");
            phoneticLetterMapping.Add('J', "Juliet");
            phoneticLetterMapping.Add('K', "Kilo");
            phoneticLetterMapping.Add('L', "Lima");
            phoneticLetterMapping.Add('M', "Mike");
            phoneticLetterMapping.Add('N', "November");
            phoneticLetterMapping.Add('O', "Oscar");
            phoneticLetterMapping.Add('P', "Papa");
            phoneticLetterMapping.Add('Q', "Quebec");
            phoneticLetterMapping.Add('R', "Romeo");
            phoneticLetterMapping.Add('S', "Sierra");
            phoneticLetterMapping.Add('T', "Tango");
            phoneticLetterMapping.Add('U', "Uniform");
            phoneticLetterMapping.Add('V', "Victor");
            phoneticLetterMapping.Add('W', "Whiskey");
            phoneticLetterMapping.Add('X', "X-ray");
            phoneticLetterMapping.Add('Y', "Yankee");
            phoneticLetterMapping.Add('Z', "Zulu");
        }

        public static string getPhoneticLetter(char c)
        {
            string returnValue;
            phoneticLetterMapping.TryGetValue(char.ToUpper(c), out returnValue);
            return returnValue;
        }

        public static string getPhoneticLetter(string s)
        {
            StringBuilder sb = new StringBuilder();
            foreach(char c in s.ToCharArray())
            {
                string temp = string.Empty;
                phoneticLetterMapping.TryGetValue(char.ToUpper(c), out temp);
                sb.Append(temp);
                sb.Append(" ");
            }
            return sb.ToString();
        }
    }

}
