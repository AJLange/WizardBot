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
    public class LUISRoot : LuisDialog<object>
    {

        //Generate Method for every Intent in LUIS model
        [LuisIntent("None")]
        public async Task None(IDialogContext context, LuisResult result)
        {
            //None is the default response
            string message = "I'm a helpful bot to help you learn more about D&D Character Creation, so you can create your own characters. Ask me about Races, Classes, Alignments, character Attributes, or character backgrounds. If you're not sure where to start, say 'I'm new'.";
            //Can also respond with the following if you don't have a set default message- $"Sorry I did not understand: " + string.Join(", ", result.Intents.Select(i => i.Intent));

            await context.PostAsync(message);
            context.Wait(MessageReceived);
        }

        //Generate Method for every Intent in LUIS model
        [LuisIntent("Test")]
        public async Task Test(IDialogContext context, LuisResult result)
        {
            //None is the default response
            //this intent now means take the test!

            string message = "Let's get started taking the test.";
      
                await context.PostAsync(message);

                //Run Take the Test
               var feedbackForm = new FormDialog<TakeTheTest.QuizForm>(new TakeTheTest.QuizForm(), TakeTheTest.QuizForm.BuildForm, FormOptions.PromptInStart);

                string TestMe = TakeTheTest.QuizForm.TestResults();
               // context.Call(feedbackForm, ChooseClass);


            // Figure out what FinalClass is from the test
            StoredUserVals.PlayerCharacter.MyClass = TestMe;
   
            context.Wait(MessageReceived);
            await context.PostAsync("You Chose " + StoredUserVals.PlayerCharacter.MyClass);

        }

 

        [LuisIntent("Greeting")]
        public async Task Greeting(IDialogContext context, LuisResult result)
        {
            //Random random = new Random();
            //int x = random.Next(0, 5);
            //if(x == 0)
            //{
            //    // yes or no prompt
            //}
            //string message = Greetings[x];
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
            await context.PostAsync(message);
            //context.Wait(MessageReceived);

            // GO TO DIFFERENT DIALOG! 
            // Button to choose Race and Class

            // ABILITIES

            // 
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
                        }
                    }
                }
            }
            
            //Chain.PostToChain()
            //    .ContinueWith(async (ctxt, res) =>
            //    {
            //        var msg = await res;
            //        await context.PostAsync($"you said {msg.Text}");
            //        return Chain.From(() => new PromptDialog.PromptChoice<string>(new[] { "a", "b", "c" }, "Which one you want to select?", string.Empty, 1, PromptStyle.None));
            //    })
            //    .ContinueWith(async (ctxt, res) =>
            //    {
            //        var selection = await res;
            //        ctxt.ConversationData.SetValue("selected", selection);
            //        return (IDialog<bool>)new PromptDialog.PromptConfirm($"do you want {selection}?", string.Empty, 1, PromptStyle.None);
            //    })
            //    .Then(async (context1, res) =>
            //    {

            //        var selection = context.ConversationData.Get<string>("selected");
            //        if (await res)
            //        {
            //            return $"{selection} is selected!";
            //        }
            //        else
            //        {
            //            return "selection canceled!";
            //        }
            //    })
            //    .PostToUser();


            //new PromptDialog.PromptChoice<string>(new[] { "a", "b", "c" }, "Which one you want to select?", string.Empty, 1, PromptStyle.None);

            //PromptDialog.Confirm(
            //    context,
            //    JoinConfirmation,
            //    "Do you want to take the key?",
            //    "I do not understand your response.",
            //    promptStyle: PromptStyle.None);


            //string message = "Would you like to know more about Classes, Abilites and Modifiers, or Backgrounds now? You can also start building your character and say Start Building.";
            //await context.PostAsync(message);
            //context.Wait(MessageReceived);
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