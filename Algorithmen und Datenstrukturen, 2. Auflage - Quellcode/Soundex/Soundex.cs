namespace Soundex
{
    public static class StringExtension
    {
        // Umsetzungstabelle für:   ABCDEFGHIJKLMNOPQRSTUVWXYZ
        private const string map = "01230120022455012623010202";

        public static string Soundex(this string text)
        {
            return Soundex(text, 4);
        }

        public static string Soundex(this string text, int maxLength)
        {
            if (string.IsNullOrEmpty(text))
                return text;

            text = text.ToUpper();

            string code = text[0].ToString();
            char last = char.MinValue;

            for(int i = 1; i < text.Length; i++)
            {
                var chr = text[i];

                int letter = chr - 'A';
                if (letter >= 0 && letter < map.Length)
                {
                    if (map[letter] != last)
                    {
                        code += map[letter];
                        last = map[letter];
                    }
                }
                else
                    last = char.MinValue;
            }

            code = code.Replace("0", "");       // Nicht zu berücksichtigende Zeichen entfernen
            
            if(code.Length > maxLength)
                code = code.Remove(maxLength);          // max. Länge
            else
                code = code.PadRight(maxLength, '0');   // auf max. Länge auffüllen

            return code;
        }
    }
}
