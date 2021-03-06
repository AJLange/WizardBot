﻿using System;
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

    public static class PlayerCharcter
    {
        public static string MyRace { get; set; }
        public static string MyClass { get; set; }
        public static string MyAlignment { get; set; }
        public static string MyBackground { get; set; }
        public static PlayerAttributeDictionary MyAttributes { get; set; }
        public static List<string> MySpells { get; set; }
        public static List<string> MyGear { get; set; }
        public static string MyName { get; set; }
    }
}
