using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingProject.Enums;

namespace TestingProject
{
    public static class AllChars
    {
        public static string consonants = "BCÇDFGĞHJKLMNPRSŞTVXYZ";
        public static string vovels = "AEIİOÖUÜ";
        public static string numbers = "1234567890";
        public static string specialCharacters = @".:,;/*-+!'^%&/()=?_-\}][{½$#£><@|";
        public static string allCharacters = consonants + vovels + numbers + specialCharacters;

    }
}
