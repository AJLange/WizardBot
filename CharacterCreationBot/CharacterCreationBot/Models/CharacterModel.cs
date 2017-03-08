using CharacterCreationBot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CharacterCreationBot
{

    public class CharacterModel
    {
        public string CharacterName { get; set; }
        public Dictionary<string,string> Atributes { get; set; }
        public Race Race { get; set; }
        public Class Class { get; set; }
        public string Alignment { get; set; }
        public string Background { get; set; }

    }
}