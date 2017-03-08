using CharacterCreationBot.Properties;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CharacterCreationBot.Models
{
    public static class ClassesDictionary
    {
        public static Dictionary<string, Class> classDictionary = new Dictionary<string, Class>();
        public static ClassList FullClassList;


        public static void BuildClassesFromJSON()
        {
            FullClassList = new ClassList();
            string jsonText = Resources.ClassesJSON;
            var TEST = JsonConvert.DeserializeObject<ClassList>(jsonText);
            FullClassList = JsonConvert.DeserializeObject<ClassList>(jsonText);
            AddAllClasss();
        }

        public static void AddAllClasss()
        {
            // For each Class in Class List add to Dictionary
            foreach (Class c in FullClassList.fullClassList)
            {
                classDictionary.Add(c.Name, c);
            }

        }

        /// <summary>
        /// Access the Dictionary from external sources
        /// </summary>
        public static Class GetClass(string word)
        {
            // Try to get the result in the static Dictionary
            Class requestedClass;
            if (classDictionary.TryGetValue(word, out requestedClass))
            {
                return requestedClass;
            }
            else
            {
                return null;
            }
        }
    }

    public class Class
    {
        [JsonProperty("Name")]
        public string Name { get; set; }
        [JsonProperty("Description")]
        public string Description { get; set; }
        [JsonProperty("PicLink")]
        public string PicLink { get; set; }
        [JsonProperty("MoreInfoLink")]
        public string MoreInfoLink { get; set; }
        [JsonProperty("Proficiencies")]
        public Proficiencies Proficiencies { get; set; }
        [JsonProperty("Pros")]
        public string Pros { get; set; }
        [JsonProperty("Cons")]
        public string Cons { get; set; }
    }

    public class ClassList
    {
        [JsonProperty("Class")]
        public List<Class> fullClassList { get; set; }
        //public int ClassCount { get; set; }
    }

    public class Proficiencies
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
