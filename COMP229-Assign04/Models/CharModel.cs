using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace COMP229_Assign04.Models
{
    public class CharModel
    {
        [JsonProperty("name")]
        public string charName { get; set; }
        public string faction { get; set; }
        public int rank { get; set; }
        public int _base { get; set; }
        public int size { get; set; }
        public string deploymentZone { get; set; }
        public string[] traits { get; set; }
        public string[] types { get; set; }
        public string[] defenseChart { get; set; }
        public int mobility { get; set; }
        public int willpower { get; set; }
        public int resiliance { get; set; }
        public int wounds { get; set; }
        public List<Action> actions { get; set; }
        public List<SpecialAbility> specialAbilities { get; set; }
        public string imageUrl { get; set; }
    }
    public class Action
    {
        public string name { get; set; }
        public string type { get; set; }
        public int rating { get; set; }
        public string range { get; set; }
    }

    public class SpecialAbility
    {
        public string name { get; set; }
        public string description { get; set; }
    }
}