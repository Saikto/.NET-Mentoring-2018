using System;

namespace StringToIntLibrary
{
    public static class StringParser
    {
        public static int ParseToInt(this string inputString)
        {
            if (string.IsNullOrWhiteSpace(inputString))
            {
                throw new ArgumentException($"String '{inputString}' is null or empty or contains only of whitespaces.");
            }

            try
            {
                VerifyStringSymbols(inputString);
            }
            catch (FormatException ex)
            {
                throw new FormatException(ex.Message);
            }

            int startPos;
            int signMultiplier = GetNumberSign(inputString, out startPos);
            int result = 0;
            try
            {
                checked
                {
                    for (int i = startPos; i < inputString.Length; i++)
                    {
                        result = result * 10 + (inputString[i]-'0');
                    }
                    result = result * signMultiplier;
                }
            }
            catch (OverflowException)
            {
                throw new OverflowException($"String '{inputString}' represents too big number for int type.");
            }

            return result;
        }

        private static int GetNumberSign(string inputString, out int startPos)
        {
            char firstChar = inputString[0];

            if (firstChar == '-')
            {
                startPos = 1;
                return -1;
            }
            if (firstChar == '+')
            {
                startPos = 1;
                return 1;
            }
            startPos = 0;
            return 1;
        }

        private static void VerifyStringSymbols(string inputString)
        {
            char[] charArray = inputString.ToCharArray();

            for (int i = 0; i < charArray.Length; i++)
            {
                if (i == 0 && (charArray[i] == '-' || charArray[i] == '+'))
                {
                    continue;
                }

                if (!char.IsDigit(charArray[i]))
                {
                    throw new FormatException($"Symbol on position {i} in string '{inputString}' is not numeric.");
                }
            }
        }
    }
}
