using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WizardBot
{
    public class PlayerAttributeDictionary
    {
        public enum Attribute { Strength, Dexterity, Constitution, Intelligence, Wisdom, Charisma }
    
        public static Dictionary<Attribute, int> attributeDictionary = new Dictionary<Attribute, int>();

        /// <summary>
        /// Access the Dictionary from external sources
        /// </summary>
        public static int GetAttributeScore(Attribute word)
        {
            // Try to get the result in the static Dictionary
            int playerAttributeScore;
            if (attributeDictionary.TryGetValue(word, out playerAttributeScore))
            {
                return playerAttributeScore;
            }
            else
            {
                return -1;
            }
        }

    }

    public class PlayerCharcter
    {
        public string MyRace { get; set; }
        public string MyClass { get; set; }
        public string MyAlignment { get; set; }
        public string MyBackground { get; set; }
        public PlayerAttributeDictionary MyAttributes { get; set; }
        public List<string> MySpells { get; set; }
        public List<string> MyGear { get; set; }
        public string MyName { get; set; }
    }
}
