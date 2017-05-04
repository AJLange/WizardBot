using CharacterCreationBot.Properties;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CharacterCreationBot.Models
{
    public static class BackgroundsDictionary
    {
        public static Dictionary<string, Backgrounds> backgroundsDictionary = new Dictionary<string, Backgrounds>();
        public static BackgroundsList FullBackgroundsList;


        public static void BuildBackgroundsFromJSON()
        {
            FullBackgroundsList = new BackgroundsList();
            string jsonText = Resources.BackgroundsJSON;
            FullBackgroundsList = JsonConvert.DeserializeObject<BackgroundsList>(jsonText);
            AddAllBackgroundss();
        }

        public static void AddAllBackgroundss()
        {
            // For each Backgrounds in Backgrounds List add to Dictionary
            foreach (Backgrounds c in FullBackgroundsList.fullBackgroundsList)
            {
                backgroundsDictionary.Add(c.Name, c);
            }

        }

        /// <summary>
        /// Access the Dictionary from external sources
        /// </summary>
        public static Backgrounds GetBackgrounds(string word)
        {
            // Try to get the result in the static Dictionary
            Backgrounds requestedBackgrounds;
            if (backgroundsDictionary.TryGetValue(word, out requestedBackgrounds))
            {
                return requestedBackgrounds;
            }
            else
            {
                return null;
            }
        }
    }

    public class Backgrounds
    {
      
        [JsonProperty("Name")]
        public string Name { get; set; }
        [JsonProperty("Description")]
        public string Description { get; set; }
        [JsonProperty("PicLink")]
        public string PicLink { get; set; }

        [JsonProperty("Skill Proficiencies")]
        public string SkillProficiencies { get; set; }
        [JsonProperty("Tool Proficiencies")]
        public string ToolProficiencies { get; set; }
        [JsonProperty("Languages")]
        public string Languages { get; set; }
        [JsonProperty("Extra")]
        public Proficiencies Extra { get; set; }
        [JsonProperty("Equipment")]
        public List<string> Equipment { get; set; }
    }

    public class BackgroundsList
    {
        [JsonProperty("Background")]
        public List<Backgrounds> fullBackgroundsList { get; set; }
        //public int BackgroundsCount { get; set; }
    }

    public class Equipment
    {
        [JsonProperty("Armor")]
        public string Armor { get; set; }
        [JsonProperty("Weapons")]
        public string Weapons { get; set; }
        [JsonProperty("Tools")]
        public string Tools { get; set; }
        [JsonProperty("Saving Throws")]
        public string SavingThrows { get; set; }
        [JsonProperty("Skills")]
        public string Skills { get; set; }
    }
    
}