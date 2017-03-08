using CharacterCreationBot.Properties;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace CharacterCreationBot
{
    public static class RaceDictionary
    {
        public static Dictionary<string, Race> raceDictionary = new Dictionary<string, Race>();
        public static RaceList FullRaceList;


        public static void BuildRacesFromJSON()
        {
            FullRaceList = new RaceList();
            string jsonText = Resources.RacesJSON;
            var TEST = JsonConvert.DeserializeObject<RaceList>(jsonText);
            FullRaceList = JsonConvert.DeserializeObject<RaceList>(jsonText);
            AddAllRaces();
        }

        public static void AddAllRaces()
        {
            // For each Race in Race List add to Dictionary
            foreach (Race r in FullRaceList.fullRaceList)
            {
                raceDictionary.Add(r.Name, r);
            }

        }

        /// <summary>
        /// Access the Dictionary from external sources
        /// </summary>
        public static Race GetRace(string word)
        {
            // Try to get the result in the static Dictionary
            Race requestedRace;
            if (raceDictionary.TryGetValue(word, out requestedRace))
            {
                return requestedRace;
            }
            else
            {
                return null;
            }
        }
    }

    public class Race
    {
        [JsonProperty("Name")]
        public string Name { get; set; }
        [JsonProperty("Description")]
        public string Description { get; set; }
        [JsonProperty("PicLink")]
        public string PicLink { get; set; }
        [JsonProperty("MoreInfoLink")]
        public string MoreInfoLink { get; set; }
        [JsonProperty("Traits")]
        public Traits Traits { get; set; }
        [JsonProperty("Pros")]
        public string Pros { get; set; }
        [JsonProperty("Cons")]
        public string Cons { get; set; }
    }

    public class RaceList
    {
        [JsonProperty("Race")]
        public List<Race> fullRaceList { get; set; }
        //public int RaceCount { get; set; }
    }

    public class Traits
    {
        [JsonProperty("AbilityScoreIncrease")]
        public string AbilityScoreIncrease { get; set; }
        [JsonProperty("Alignment")]
        public string Alignment { get; set; }
        [JsonProperty("Size")]
        public string Size { get; set; }
        [JsonProperty("Speed")]
        public string Speed { get; set; }
        [JsonProperty("Languages")]
        public string Languages { get; set; }
    }
}