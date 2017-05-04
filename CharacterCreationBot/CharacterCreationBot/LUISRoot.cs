using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis.Models;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Connector;
using System.Diagnostics;
using CharacterCreationBot.Models;
using Microsoft.Bot.Builder.FormFlow;

namespace CharacterCreationBot
{
    [Serializable]
    [LuisModel("05c494f4-d639-490d-b551-970fef333bcb", "f2b59c258e5042a3b265498b92acd8a8")]
    public class LUISRoot : LuisDialog<ContextPasser> //LuisDialog<LuisResult>
    {

        //private async Task StartAsync(IDialogContext context, string result)
        //{
        //    if (result == "Classes")
        //    {
        //        // call clases dialog
        //        await context.PostAsync("This is a classes Dialog...");
        //    }
        //    //context.Wait(this.ReturnList();
        //}


        [LuisIntent("None")]
        public async Task None(IDialogContext context, LuisResult result)
        {
            //None is the default response
            string message = "I didn't quite understand that request, I'm still learning. Do you want to learn more? Ask me about Races, Classes, Alignments, character Attributes, or character Backgrounds. If you're not sure where to start, say 'I'm new'.";

            await context.PostAsync(message);
            context.Wait(MessageReceived);
        }

        [LuisIntent("Back")]
        public async Task Back(IDialogContext context, LuisResult result)
        {
            //None is the default response
            string message = "Go back to beginning";
            //await context.PostAsync(message);
            ContextPasser cp = new ContextPasser(result.Intents[0].Intent, result.Entities.ToList(), message);
            context.Done(cp);
            //context.Done(result);
        }


        [LuisIntent("Test")]
        public async Task Test(IDialogContext context, LuisResult result)
        {
            string message = "Okay, let's start.";

            ContextPasser cp = new ContextPasser(result.Intents[0].Intent, result.Entities.ToList(), message);
            context.Done(cp);
        }

 

        [LuisIntent("Greeting")]
        public async Task Greeting(IDialogContext context, LuisResult result)
        {
            string message = "Hi! I'm a helpful bot to help you learn more about D&D Character Creation. Ask me about Races, Classes, Alignments, character Attributes, or character backgrounds. If you're not sure where to start, you can say 'I'm new'.";
            await context.PostAsync(message);
            context.Wait(MessageReceived);
        }



        /// <summary>
        /// New - Handles new person on where to start and has a button list about different things they can learn about
        /// </summary>
        /// <param name="context"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        [LuisIntent("NewPlayer")]
        public async Task NewPlayer(IDialogContext context, LuisResult result)
        {
            string message = "Okay, building a character is super fun but if you're new to D&D you might be a little overwhelmed. I'm here to help. Each Player Character has: \n\n"
                + "* Class - What you do \n\n"
                + "* Race - Who you are \n\n"
                + "* Background - Where you come from \n\n"
                + "* Alignment - How you behave \n\n"
                + "* Abilities - How well you do, what you do ";
            await context.PostAsync(message);
            //context.Wait(MessageReceived);

            var resultMessage = context.MakeMessage();
            resultMessage.AttachmentLayout = AttachmentLayoutTypes.Carousel;
            resultMessage.Attachments = new List<Attachment>();
            List<HeroCard> information = CategoryCards();
            foreach (var card in information)
            {
                resultMessage.Attachments.Add(card.ToAttachment());
            }

            await context.PostAsync(resultMessage);
            var endMessage = "Let me know which category you would like to know more about; or you can take a test to figure out what combination would work for you. If you are ready to build your character you can start now. ";
            await context.PostAsync(endMessage);
            context.Wait(MessageReceived);
        }

        [LuisIntent("BuildCharacter")]
        public async Task BuildCharacter(IDialogContext context, LuisResult result)
        {
            string message = "Okay so the first thing you have to choose for your character is your Class";

            ContextPasser cp = new ContextPasser(result.Intents[0].Intent, result.Entities.ToList(), message);
            context.Done(cp);
        }


    


        /// <summary>
        /// ReturnList - Returns a descriptive list for the specified Categories
        /// </summary>
        /// <param name="context"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        [LuisIntent("ReturnList")]
   
        public async Task ReturnList(IDialogContext context, LuisResult result)
        {
            var entitiesArray = result.Entities;
            var actions = result.Query;

            var reply = context.MakeMessage();

            if (entitiesArray.Count >= 1)
            {
                foreach (var entityItem in result.Entities)
                {
                    if (entityItem.Type == "Categories")
                    {
                        if(entityItem.Entity == "races" || entityItem.Entity == "race")
                        {
                            if (RaceDictionary.raceDictionary.Count < 1)
                            {
                                RaceDictionary.BuildRacesFromJSON();
                            }

                            var resultMessageRace = context.MakeMessage();
                            resultMessageRace.AttachmentLayout = AttachmentLayoutTypes.Carousel;
                            resultMessageRace.Attachments = new List<Attachment>();
                            List<HeroCard> information = RaceCards();
                            foreach (var card in information)
                            {
                                resultMessageRace.Attachments.Add(card.ToAttachment());
                            }

                            await context.PostAsync(resultMessageRace);                            
                        }

                        if(entityItem.Entity == "classes" || entityItem.Entity == "class")
                        {
                            if (ClassesDictionary.classDictionary.Count < 1)
                            {
                                ClassesDictionary.BuildClassesFromJSON();
                            }

                            var resultMessage = context.MakeMessage();
                            resultMessage.AttachmentLayout = AttachmentLayoutTypes.Carousel;
                            resultMessage.Attachments = new List<Attachment>();
                            List<HeroCard> information = ClassCards();
                            foreach (var card in information)
                            {
                                resultMessage.Attachments.Add(card.ToAttachment());
                            }

                            await context.PostAsync(resultMessage);
                        }

                        if (entityItem.Entity == "backgrounds" || entityItem.Entity == "background")
                        {
                            if (BackgroundsDictionary.backgroundsDictionary.Count < 1)
                            {
                                BackgroundsDictionary.BuildBackgroundsFromJSON();
                            }

                            var resultMessage = context.MakeMessage();
                            resultMessage.AttachmentLayout = AttachmentLayoutTypes.Carousel;
                            resultMessage.Attachments = new List<Attachment>();
                            List<HeroCard> information = BackgroundsCards();
                            foreach (var card in information)
                            {
                                resultMessage.Attachments.Add(card.ToAttachment());
                            }

                            await context.PostAsync(resultMessage);
                            await this.CategoryEnd(context);
                        }

                        if (entityItem.Entity == "attributes" || entityItem.Entity == "atribute")
                        {
                            if (AbilitiesDictionary.abilitiesDictionary.Count < 1)
                            {
                                AbilitiesDictionary.BuildAbilitiesFromJSON();
                            }

                            var resultMessage = context.MakeMessage();
                            resultMessage.AttachmentLayout = AttachmentLayoutTypes.Carousel;
                            resultMessage.Attachments = new List<Attachment>();
                            List<HeroCard> information = AbilitiesCards();
                            foreach (var card in information)
                            {
                                resultMessage.Attachments.Add(card.ToAttachment());
                            }

                            await context.PostAsync(resultMessage);
                            await this.CategoryEnd(context);
                        }
                    }
                }
            }
     
        }

        public async Task CategoryEnd(IDialogContext context)
        {
            var result = context.MakeMessage();

            //await context.PostAsync("What Category would you like to know about next? Or type what you would like to know about ");
            HeroCard card = new HeroCard()
            {
                Title = "",
                Subtitle = "",
                Text = "What Category would you like to know about next? Or type what you would like to know about.",
                Images = new List<CardImage>()
                        {
                            new CardImage() { Url = "" }
                        },

                Buttons = new List<CardAction>()
                        {
                            new CardAction()
                            {
                                Title = "Classes",
                                Type = ActionTypes.ImBack,
                                Value = "Classes"
                            },
                            new CardAction()
                            {
                                Title = "Races",
                                Type = ActionTypes.ImBack,
                                Value = "Races"
                            },
                            new CardAction()
                            {
                                Title = "Backgrounds",
                                Type = ActionTypes.ImBack,
                                Value = "Backgrounds"
                            },
                            new CardAction()
                            {
                                Title = "Attributes",
                                Type = ActionTypes.ImBack,
                                Value = "Attributes"
                            }
                        }

            };
            result.Attachments.Add(card.ToAttachment());
            await context.PostAsync(result);
            
        }

        public async Task JoinConfirmation(IDialogContext context, IAwaitable<bool> argument)
        {
            var confirm = await argument;
            if (confirm)
            {
                await context.PostAsync("Welcome to the City of Light!");
            }
            else
            {
                await context.PostAsync("You should reconsider. The City of Light will take away all pain.");
            }
            context.Wait(MessageReceived);
        }

        private List<HeroCard> AbilitiesCards()
        {
            List<HeroCard> cards = new List<HeroCard>();
            for (int i = 0; i < AbilitiesDictionary.abilitiesDictionary.Count; i++)
            {
                Abilities curAbilities = AbilitiesDictionary.abilitiesDictionary.ElementAt(i).Value;
                var racialIncreaseString = "";
                for (int j = 0; j < curAbilities.RacialIncrease.Count; j++)
                {
                    racialIncreaseString = racialIncreaseString + "* " + curAbilities.RacialIncrease[j] + " \n ";
                }
                HeroCard backgroundHeroCard = new HeroCard()
                {
                    Title = curAbilities.Name,
                    Subtitle = "Measures: " + curAbilities.Measures + " | Important for: " + curAbilities.ImportantFor,
                    Text = "Racial Increase: \n " + racialIncreaseString,
                    Images = new List<CardImage>()
                        {
                            new CardImage() { Url = "http://cdn3-www.dogtime.com/assets/uploads/gallery/30-impossibly-cute-puppies/impossibly-cute-puppy-21.jpg" }
                        },

                    Buttons = new List<CardAction>()
                        {
                            new CardAction()
                            {
                                Title = "Go to site",
                                Type = ActionTypes.OpenUrl,
                                Value = "http://dnd.wizards.com/dungeons-and-dragons/what-is-dd/classes"
                            }
                        }

                };
                cards.Add(backgroundHeroCard);
            }
            return cards;
        }

        private List<HeroCard> BackgroundsCards()
        {
            List<HeroCard> cards = new List<HeroCard>();
            for (int i = 0; i < BackgroundsDictionary.backgroundsDictionary.Count; i++)
            {
                Backgrounds curBG = BackgroundsDictionary.backgroundsDictionary.ElementAt(i).Value;
                var equipmentString = "";
                for (int j = 0; j < curBG.Equipment.Count; j++)
                {
                    equipmentString = equipmentString.ToString() + "* " + curBG.Equipment[j] + " \n\n ";
                }
                var profString = curBG.Description + " \n\n Equipment: \n\n " + equipmentString;
                HeroCard backgroundHeroCard = new HeroCard()
                {
                    Title = curBG.Name,
                    Subtitle = profString.ToString(),
                    Text = "Hello",
                    Images = new List<CardImage>()
                        {
                            new CardImage() { Url = "http://cdn3-www.dogtime.com/assets/uploads/gallery/30-impossibly-cute-puppies/impossibly-cute-puppy-21.jpg" }
                        },

                    Buttons = new List<CardAction>()
                        {
                            new CardAction()
                            {
                                Title = "Go to site",
                                Type = ActionTypes.OpenUrl,
                                Value = "http://dnd.wizards.com/dungeons-and-dragons/what-is-dd/classes"
                            }
                        }

                };
                cards.Add(backgroundHeroCard);
            }
            return cards;
        }


        private List<HeroCard> ClassCards()
        {
            List<HeroCard> cards = new List<HeroCard>();
            for (int i = 0; i < ClassesDictionary.classDictionary.Count; i++)
            {
                Class curClass = ClassesDictionary.classDictionary.ElementAt(i).Value;
                var profString = curClass.Description + " \n\n";
                   /* + "* "+ curClass.Proficiencies.Armor + " \n\n"
                    + "* " + curClass.Proficiencies.Weapons + " \n\n"
                    + "* " + curClass.Proficiencies.Tools + " \n\n" 
                    + "* " + curClass.Proficiencies.SavingThrows + " \n\n" 
                    + "* " + curClass.Proficiencies.Skills;*/
                HeroCard classHeroCard = new HeroCard()
                {
                    Title = curClass.Name,
                    Subtitle = profString.ToString(),
                    Images = new List<CardImage>()
                        {
                             new CardImage() { Url = curClass.PicLink }
                    },

                    Buttons = new List<CardAction>()
                        {
                            new CardAction()
                            {
                                Title = "Go to site",
                                Type = ActionTypes.OpenUrl,
                                Value = "http://dnd.wizards.com/dungeons-and-dragons/what-is-dd/classes"
                            }
                        }

                };
                cards.Add(classHeroCard);
            }
            return cards;
        }

        public List<HeroCard> RaceCards()
        {

            List<HeroCard> cards = new List<HeroCard>();
            for (int i = 0; i < RaceDictionary.raceDictionary.Count; i++)
            {
                Race curRace = RaceDictionary.raceDictionary.ElementAt(i).Value;
                HeroCard raceHeroCard = new HeroCard()
                {
                    Title = curRace.Name,
                    Subtitle = curRace.Description,
                    Images = new List<CardImage>()
                        {
                            new CardImage() { Url = curRace.PicLink }
                        },

                    Buttons = new List<CardAction>()
                        {
                            new CardAction()
                            {
                                Title = "Go to site",
                                Type = ActionTypes.OpenUrl,
                                Value = "http://dnd.wizards.com/dungeons-and-dragons/what-is-dd/races"
                            }
                        }
                    
                };
                cards.Add(raceHeroCard);
            }

            return cards;
        }

        public List<HeroCard> CategoryCards()
        {
            List<HeroCard> cards = new List<HeroCard>();
            HeroCard raceHeroCard = new HeroCard()
            {
                Title = "Race",
                Subtitle = "Your choice of character race provides you with a basic set of advantages and special abilities. If you’re a fighter, are you a stubborn dwarf monster-slayer, a graceful elf blademaster, or a fierce dragonborn gladiator? If you’re a wizard, are you a brave human spell-for-hire or a devious tiefling conjurer? Your character race not only affects your ability scores and powers but also provides the first cues for building your character’s story.",
                Images = new List<CardImage>()
                        {
                            new CardImage() { Url = "http://media.wizards.com/images/dnd/newtodnd/NEW_TO_DD_Races_Elf.png" }
                        },
                Buttons = new List<CardAction>()
                        {
                            new CardAction()
                            {
                                Title = "More details",
                                Type = ActionTypes.OpenUrl,
                                Value = "http://dnd.wizards.com/dungeons-and-dragons/what-is-dd/races"
                            }
                        }
            };
            cards.Add(raceHeroCard);
            HeroCard classHeroCard = new HeroCard()
            {
                Title = "Class",
                Subtitle = "Before you start, you might find it helpful to think about the basic kind of character you want to play. You might be a courageous knight, a skulking rogue, a fervent cleric, or a flamboyant wizard. Or you might be more interested in an unconventional character, such as a brawny rogue who likes to mix it up in hand-to‐hand combat, or a sharpshooter who picks off enemies from afar.",
                Images = new List<CardImage>()
                        {
                            new CardImage() { Url = "http://dnd.wizards.com/sites/default/files/media/styles/mosaic_thumbnail/public/images/mosaic/NEW_TO_DD_Classes_Rogue_T_140626.jpg?itok=1FiJJxV1" }
                        },
                Buttons = new List<CardAction>()
                        {
                            new CardAction()
                            {
                                Title = "More details",
                                Type = ActionTypes.OpenUrl,
                                Value = "http://dnd.wizards.com/dungeons-and-dragons/what-is-dd/classes"
                            }
                        }
            };
            cards.Add(classHeroCard);

            return cards;
        }
    }
}