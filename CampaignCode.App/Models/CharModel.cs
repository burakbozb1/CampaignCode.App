using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampaignCode.App.Models
{
    public class CharModel
    {
        public char Char { get; set; }
        public byte Property { get; set; }

        public CharModel(char @char, byte property)
        {
            Char = @char;
            Property = property;
        }
    }
}
