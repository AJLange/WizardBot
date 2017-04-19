using Microsoft.Bot.Builder.Luis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CharacterCreationBot.Models
{
    public class ContextPasser
    {
        public string Intent;        // Main Intent
        public List<EntityRecommendation> Entities;       // Entities list
        public string Message;       // Optional Message

        public ContextPasser(string intent, List<EntityRecommendation> entities, string message)
        {
            Intent = intent;
            Entities = entities;
            Message = message;
        }

    }
}