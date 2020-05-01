
using System.Linq;

namespace Praticis.Framework.Layers.Domain.Abstractions.Helpers
{
    public class TextHelper
    {
        public static string RemoveSpecialCharacters(string text)
        {
            if (text == null) return string.Empty;

            const string specialCharacters = "ÄÅÁÂÀÃäáâàãÉÊËÈéêëèÍÎÏÌíîïìÖÓÔÒÕöóôòõÜÚÛüúûùÇç";
            const string normalCharacters = "AAAAAAaaaaaEEEEeeeeIIIIiiiiOOOOOoooooUUUuuuuCc";

            for (var i = 0; i < specialCharacters.Length; i++)
                text = text.Replace(specialCharacters[i].ToString(), normalCharacters[i].ToString());

            return text;
        }

        public static int ObtainOnlyNumbers(string text)
        {
            string value = "";

            text.Where(c => char.IsDigit(c))
                .ToList()
                .ForEach(c => value += c);

            if (string.IsNullOrEmpty(value))
                return 0;
            else
                return int.Parse(value);
        }

        public static string UpperInitials(string text)
        {
            string str = "";

            text.Trim()
                .Split(' ')
                .ToList()
                .ForEach(x =>
                {
                    if (x.Length > 1)
                        str += " " + x.First().ToString().ToUpper() + x.Substring(1).ToLower();
                    else
                        str += " " + x.ToUpper();
                });

            str = str.Trim();

            return str;
        }
    }
}