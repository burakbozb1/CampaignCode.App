using CampaignCode.App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingProject;
using TestingProject.Enums;

namespace CampaignCode.App
{
    public static class CodeService
    {
        public static List<CharModel> SetDefaultCharacters()
        {
            var consonants = new List<CharModel>();
            foreach (var item in AllChars.consonants)
            {
                consonants.Add(new CharModel(item, Convert.ToByte(CharProperties.Consonant)));
            }

            var vovels = new List<CharModel>();
            foreach (var item in AllChars.vovels)
            {
                vovels.Add(new CharModel(item, Convert.ToByte(CharProperties.Vovel)));
            }

            var number = new List<CharModel>();
            foreach (var item in AllChars.numbers)
            {
                number.Add(new CharModel(item, Convert.ToByte(CharProperties.Number)));
            }

            var specialCharacters = new List<CharModel>();
            foreach (var item in AllChars.specialCharacters)
            {
                specialCharacters.Add(new CharModel(item, Convert.ToByte(CharProperties.SpecialCharacter)));
            }

            var result = consonants.Concat(vovels).Concat(number).Concat(specialCharacters).ToList();
            return result;
        }

        public static List<CharModel> SetMyCharacters(string myCharacters, List<CharModel> charModels)
        {
            List<CharModel> result = new List<CharModel>();
            foreach (var c in myCharacters)
            {
                var tmpCharModel = charModels.Where(x => x.Char == c).SingleOrDefault();
                if (tmpCharModel != null)
                {
                    result.Add(tmpCharModel);
                }
            }
            return result;
        }

        /// <summary>
        /// Patterne uygun olarak kod üretir.
        /// </summary>
        /// <param name="pattern"></param>
        /// <param name="myCharModel"></param>
        /// <param name="lengthOfKey"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public static string GenerateKey(string pattern, List<CharModel> myCharModel, int lengthOfKey)
        {
            List<char> consonants = myCharModel.Where(x => x.Property == Convert.ToByte(CharProperties.Consonant)).Select(x => x.Char).ToList();
            List<char> vovels = myCharModel.Where(x => x.Property == Convert.ToByte(CharProperties.Vovel)).Select(x => x.Char).ToList();
            List<char> numbers = myCharModel.Where(x => x.Property == Convert.ToByte(CharProperties.Number)).Select(x => x.Char).ToList();
            List<char> specialCharacters = myCharModel.Where(x => x.Property == Convert.ToByte(CharProperties.SpecialCharacter)).Select(x => x.Char).ToList();

            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < lengthOfKey; i++)
            {
                Random rnd = new Random();
                if (Convert.ToByte(pattern[i].ToString()) == Convert.ToByte(CharProperties.Consonant))
                {
                    int index = rnd.Next(consonants.Count);
                    stringBuilder.Append(consonants[index]);
                }
                else if (Convert.ToByte(pattern[i].ToString()) == Convert.ToByte(CharProperties.Vovel))
                {
                    int index = rnd.Next(vovels.Count);
                    stringBuilder.Append(vovels[index]);
                }

                else if (Convert.ToByte(pattern[i].ToString()) == Convert.ToByte(CharProperties.Number))
                {
                    int index = rnd.Next(numbers.Count);
                    stringBuilder.Append(numbers[index]);
                }
                else if (Convert.ToByte(pattern[i].ToString()) == Convert.ToByte(CharProperties.SpecialCharacter))
                {
                    int index = rnd.Next(specialCharacters.Count);
                    stringBuilder.Append(specialCharacters[index]);
                }
            }
            return stringBuilder.ToString();
        }

        /// <summary>
        /// Patternlere uygun olarak istenen adette kod üretir.
        /// </summary>
        /// <param name="patterns"></param>
        /// <param name="myCharacters"></param>
        /// <param name="codeLength"></param>
        /// <param name="list"></param>
        /// <param name="numberOfKeys"></param>
        /// <returns></returns>
        public static List<string> GenerateKeys(List<string> patterns, List<CharModel> myCharacters, int codeLength, int numberOfKeys)
        {
            List<string> codes = new List<string>();
            int codeNumberCounter = 0;
            while (codeNumberCounter < numberOfKeys)
            {
                var rndPattern = new Random().Next(patterns.Count);
                var tmpCode = GenerateKey(patterns[rndPattern], myCharacters, codeLength);
                var checkCode = codes.Where(x => x == tmpCode).Any();
                if (!checkCode)
                {
                    codes.Add(tmpCode);
                    codeNumberCounter++;
                }
            }
            return codes;
        }

        /// <summary>
        /// Sorgulanacak kodun patternlere uygun olup olmadığını kontrol eder.
        /// </summary>
        /// <param name="patterns">Kodun uyup uymadığının kontrol edileceği pattern listesi</param>
        /// <param name="key">Key</param>
        /// <param name="charModels">Default olarak gelen ve tüm karakterleri içeren model</param>
        /// <returns></returns>
        public static bool CheckKey(List<string> patterns, string key, List<CharModel> charModels)
        {
            if (key.Length != patterns[0].Length)
            {
                return false;
            }
            bool control = false;
            int counter = 0;
            while (!control && counter < patterns.Count)
            {
                int patternCounter = 0;
                for (int i = 0; i < patterns[counter].Length; i++)
                {
                    if (Convert.ToByte(patterns[counter][i].ToString()) == GetTypeOfChar(key[i], charModels))
                    {
                        patternCounter++;
                    }
                    else
                    {
                        break;
                    }
                }
                if (patternCounter == patterns[counter].Length)
                {
                    control = true;
                }
                counter++;
            }
            return control;
        }

        /// <summary>
        /// Girilen karakterin tipini döndürür.
        /// </summary>
        /// <param name="c"></param>
        /// <param name="charModels"></param>
        /// <returns></returns>
        public static byte GetTypeOfChar(char c, List<CharModel> charModels)
        {
            var type = charModels.Where(x => x.Char == c).Select(x => x.Property).Single();
            return type;
        }
    }
}
