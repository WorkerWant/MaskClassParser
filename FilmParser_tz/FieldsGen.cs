using System;

namespace FilmParser_tz
{
    internal static class FieldsGen
    {
        public static int? GenInt(int num, int a = 1950, int b = 2022, int chance = 30)
        {
            if (new Random(num).Next(100) >= chance)
            {
                return new Random(num).Next(a, b);
            }
            return null;
        }
        public static string GenStr(int seed, int chance = 30, string alphabet = "abcdefghijklmnopqrstuvwxyz")
        {
            if (new Random(seed).Next(100) >= chance)
            {

                Random rand = new Random(seed);
                int strLength = rand.Next(5, alphabet.Length - 7);
                string ResponseString = alphabet[rand.Next(alphabet.Length)].ToString().ToUpper();

                for (int i = 0; i < strLength - 1; i++)
                {
                    ResponseString += alphabet[rand.Next(alphabet.Length)];
                }
                return ResponseString;
            }
            return null;
        }
    }
}
