using CharacterCreationBot.Properties;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CharacterCreationBot.Models
{
    public class AlignmentDictionary
    {
            public static Dictionary<string, Alignments> alignmentsDictionary = new Dictionary<string, Alignments>();
            public static AlignmentsList FullAlignmentsList;


            public static void BuildAlignmentsFromJSON()
            {
                FullAlignmentsList = new AlignmentsList();
                string jsonText = Resources.AlignmentsJSON;
                FullAlignmentsList = JsonConvert.DeserializeObject<AlignmentsList>(jsonText);
                AddAllAlignments();
            }

            public static void AddAllAlignments()
            {
                // For each Alignments in the list add to dictionary

                foreach (Alignments c in FullAlignmentsList.fullAlignmentsList)
                {
                    alignmentsDictionary.Add(c.Name, c);
                }

            }

            /// <summary>
            /// Access the Dictionary from external sources
            /// </summary>
            public static Alignments GetAlignments(string word)
            {
                // Try to get the result in the static Dictionary
                Alignments requestedAlignments;
                if (alignmentsDictionary.TryGetValue(word, out requestedAlignments))
                {
                    return requestedAlignments;
                }
                else
                {
                    return null;
                }
            }
        }

        public class Alignments
        {

            [JsonProperty("Name")]
            public string Name { get; set; }
            [JsonProperty("Description")]
            public string Description { get; set; }
            [JsonProperty("PicLink")]
            public string PicLink { get; set; }

            [JsonProperty("WhoIs")]
            public string WhoIs { get; set; }
        }

        public class AlignmentsList
        {
            [JsonProperty("Alignment")]
            public List<Alignments> fullAlignmentsList { get; set; }

        }

    }
