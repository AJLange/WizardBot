using CharacterCreationBot.Properties;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CharacterCreationBot.Models
{
    
        public static class AbilitiesDictionary
        {
            public static Dictionary<string, Abilities> abilitiesDictionary = new Dictionary<string, Abilities>();
            public static AbilitiesList FullAbilitiesList;


            public static void BuildAbilitiesFromJSON()
            {
                FullAbilitiesList = new AbilitiesList();
                string jsonText = Resources.AbilitiesJSON;
                FullAbilitiesList = JsonConvert.DeserializeObject<AbilitiesList>(jsonText);
                AddAllAbilitiess();
            }

            public static void AddAllAbilitiess()
            {
                // For each Abilities in Abilities List add to Dictionary
                foreach (Abilities c in FullAbilitiesList.fullAbilitiesList)
                {
                    abilitiesDictionary.Add(c.Name, c);
                }

            }

            /// <summary>
            /// Access the Dictionary from external sources
            /// </summary>
            public static Abilities GetAbilities(string word)
            {
                // Try to get the result in the static Dictionary
                Abilities requestedAbilities;
                if (abilitiesDictionary.TryGetValue(word, out requestedAbilities))
                {
                    return requestedAbilities;
                }
                else
                {
                    return null;
                }
            }
        }

        public class Abilities
        {

            [JsonProperty("Name")]
            public string Name { get; set; }
            [JsonProperty("Description")]
            public string Description { get; set; }
            [JsonProperty("Measures")]
            public string Measures { get; set; }
            [JsonProperty("Important for")]
            public string ImportantFor { get; set; }   
            [JsonProperty("Racial Increase")]
            public List<string> RacialIncrease { get; set; }
            [JsonProperty("Skills")]
            public List<string> Skills { get; set; }

        }

        public class AbilitiesList
        {
            [JsonProperty("Abilities")]
            public List<Abilities> fullAbilitiesList { get; set; }
            //public int AbilitiesCount { get; set; }
        }

    
}