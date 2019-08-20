using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TMRC_CSP.ViewModel.GeneratePassword
{
    public class GeneratePassword
    {
        public static string AutomaticGeneratePassword(int lengthOfPassword, bool includeLowercase = true, bool includeUppercase = true, bool includeNumeric = true, bool includeSpecial = true, bool includeSpaces = false)
        {
            const int MAXIMUM_IDENTICAL_CONSECUTIVE_CHARS = 2;
            const string LOWERCASE_CHARACTERS = "abcdefghijklmnopqrstuvwxyz";
            const string UPPERCASE_CHARACTERS = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string NUMERIC_CHARACTERS = "0123456789";
            const string SPECIAL_CHARACTERS = @"!#$%&*@\";
            const string SPACE_CHARACTER = " ";
            const int PASSWORD_LENGTH_MIN = 8;
            const int PASSWORD_LENGTH_MAX = 50;

            if (lengthOfPassword < PASSWORD_LENGTH_MIN || lengthOfPassword > PASSWORD_LENGTH_MAX)
            {
                return "Password length must be between 8 and 50.";
            }

            string characterSet = "";

            if (includeLowercase)
            {
                characterSet += LOWERCASE_CHARACTERS;
            }

            if (includeUppercase)
            {
                characterSet += UPPERCASE_CHARACTERS;
            }

            if (includeNumeric)
            {
                characterSet += NUMERIC_CHARACTERS;
            }

            if (includeSpecial)
            {
                characterSet += SPECIAL_CHARACTERS;
            }

            if (includeSpaces)
            {
                characterSet += SPACE_CHARACTER;
            }

            char[] password = new char[lengthOfPassword];
            int characterSetLength = characterSet.Length;

            System.Random random = new System.Random();
            for (int characterPosition = 0; characterPosition < lengthOfPassword; characterPosition++)
            {
                password[characterPosition] = characterSet[random.Next(characterSetLength - 1)];

                bool moreThanTwoIdenticalInARow =
                    characterPosition > MAXIMUM_IDENTICAL_CONSECUTIVE_CHARS
                    && password[characterPosition] == password[characterPosition - 1]
                    && password[characterPosition - 1] == password[characterPosition - 2];

                if (moreThanTwoIdenticalInARow)
                {
                    characterPosition--;
                }
            }

            return string.Join(null, password);
        }
    }
}