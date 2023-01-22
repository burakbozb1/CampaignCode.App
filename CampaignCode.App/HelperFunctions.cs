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
    public static class HelperFunctions
    {

        public static long PossibilityCalculator(List<string> patterns, List<CharModel> myCharacters)
        {
            int consonants = myCharacters.Where(x => x.Property == Convert.ToByte(CharProperties.Consonant)).Count();
            int vovels = myCharacters.Where(x => x.Property == Convert.ToByte(CharProperties.Vovel)).Count();
            int numbers = myCharacters.Where(x => x.Property == Convert.ToByte(CharProperties.Number)).Count();
            int specialCharacters = myCharacters.Where(x => x.Property == Convert.ToByte(CharProperties.SpecialCharacter)).Count();

            long result = 1;
            foreach (var item in patterns)
            {
                foreach (var c in item)
                {
                    if (Convert.ToByte(c.ToString()) == Convert.ToByte(CharProperties.Consonant))
                    {
                        result *= consonants;
                    }
                    else if (Convert.ToByte(c.ToString()) == Convert.ToByte(CharProperties.Vovel))
                    {
                        result *= vovels;
                    }
                    else if (Convert.ToByte(c.ToString()) == Convert.ToByte(CharProperties.Number))
                    {
                        result *= numbers;
                    }
                    else if (Convert.ToByte(c.ToString()) == Convert.ToByte(CharProperties.SpecialCharacter))
                    {
                        result *= specialCharacters;
                    }
                }
            }
            return result;
        }

        public static void PrintPatterns(List<string> patterns)
        {
            Console.WriteLine($"{patterns.Count} adet patterniniz mevcut. Patternleriniz: ");
            foreach (var item in patterns)
            {
                foreach (var c in item)
                {
                    if (Convert.ToByte(c.ToString()) == Convert.ToByte(CharProperties.Consonant))
                    {
                        Console.Write(" SessizHarf ");
                    }
                    else if (Convert.ToByte(c.ToString()) == Convert.ToByte(CharProperties.Vovel))
                    {
                        Console.Write(" SesliHarf ");
                    }
                    else if (Convert.ToByte(c.ToString()) == Convert.ToByte(CharProperties.Number))
                    {
                        Console.Write(" Sayı ");
                    }
                    else if (Convert.ToByte(c.ToString()) == Convert.ToByte(CharProperties.SpecialCharacter))
                    {
                        Console.Write(" ÖzelKarakter ");
                    }
                }
                Console.WriteLine();
            }
        }

        public static void PrintProducedCodes(List<string> codes)
        {
            foreach (var item in codes)
            {
                Console.WriteLine(item);
            }
        }
    }
}
