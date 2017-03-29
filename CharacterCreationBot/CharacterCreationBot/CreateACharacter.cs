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
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using System.Threading.Tasks;
using Microsoft.Bot.Connector;
using CharacterCreationBot.Models;

namespace CharacterCreationBot
{
    [Serializable]
    public class CreateACharacter : IDialog<object>
    {
        public string usersClass;
        public string usersRace;
        public async Task StartAsync(IDialogContext context)
        {
            await this.ShowSelectClasses(context);
        }

        protected async Task ShowSelectClasses(IDialogContext context)
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
            //await context.PostAsync(endMessage);

            context.Wait(this.OnClassSelected);
        }

        // NEW
        private async Task OnClassSelected(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var message = await result;
            // need to check if they selected a correct value...
            if (ClassesDictionary.classDictionary.ContainsKey(message.Text))
            {
                StoredUserVals.PlayerCharacter.MyClass = message.Text;
                usersClass = message.Text;
                await context.PostAsync("You selected: " + message.Text);
                await this.ShowSelectRaces(context);
            }
            else
            {
                await this.ShowSelectClasses(context);
            }

        }


        protected async Task ShowSelectRaces(IDialogContext context)
        {
            await context.PostAsync("Now please select a Race ");

            if (RaceDictionary.raceDictionary.Count < 1)
            {
                RaceDictionary.BuildRacesFromJSON();
            }
            var resultMessage = context.MakeMessage();
            resultMessage.AttachmentLayout = AttachmentLayoutTypes.Carousel;
            resultMessage.Attachments = new List<Attachment>();
            List<HeroCard> information = RaceCards();
            foreach (var card in information)
            {
                resultMessage.Attachments.Add(card.ToAttachment());
            }

            await context.PostAsync(resultMessage);
            //await context.PostAsync(endMessage);

            context.Wait(this.OnRaceSelected);
        }

        private async Task OnRaceSelected(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var usermessage = await result;
            // need to check if they selected a correct value...
            if (RaceDictionary.raceDictionary.ContainsKey(usermessage.Text))
            {
                StoredUserVals.PlayerCharacter.MyRace = usermessage.Text;
                usersClass = usermessage.Text;
                await context.PostAsync("You selected: " + usermessage.Text + " for your race");
                await this.ShowAttributes(context);
            }
            else
            {
                await context.PostAsync("That wasn't a valid choice.");
                await this.ShowSelectRaces(context);
            }

        }

        protected async Task ShowAttributes(IDialogContext context)
        {

            await context.PostAsync("Next we need to select values for all the attributes: Strength, Dexterity, Constitution, Intelligence, Wisdom, Charisma");

            await context.PostAsync("Here are the numbers we created for you.");
            int[] attributes = setAttributes();
            string ints = "";
            for (int i = 0; i<attributes.Length; i++)
            {
                ints = ints + attributes[i].ToString() + ", ";
            }
            await context.PostAsync(ints);

            await this.ShowAttributeSelection(context);


        }

        private async Task ShowAttributeSelection(IDialogContext context)
        {
            if (AbilitiesDictionary.abilitiesDictionary.Count < 1)
            {
                AbilitiesDictionary.BuildAbilitiesFromJSON();
            }

            var resultMessage = context.MakeMessage();
            resultMessage.AttachmentLayout = AttachmentLayoutTypes.Carousel;
            resultMessage.Attachments = new List<Attachment>();
            List<HeroCard> information = AttributeCards();
            foreach (var card in information)
            {
                resultMessage.Attachments.Add(card.ToAttachment());
            }

            await context.PostAsync(resultMessage);
            //await context.PostAsync(endMessage);

            context.Wait(this.OnAttributeSelected);
            
        }

        private async Task OnAttributeSelected(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var usermessage = await result;
            // need to check if they selected a correct value...
            if (RaceDictionary.raceDictionary.ContainsKey(usermessage.Text))
            {
                StoredUserVals.PlayerCharacter.MyRace = usermessage.Text;
                usersClass = usermessage.Text;
                await context.PostAsync("You selected: " + usermessage.Text + " for your race");
                await this.ShowAttributes(context);
            }
            else
            {
                await context.PostAsync("That wasn't a valid choice.");
                await this.ShowSelectRaces(context);
            }

        }

        public List<HeroCard> AttributeCards()
        {

            List<HeroCard> cards = new List<HeroCard>();
            for (int i = 0; i < AbilitiesDictionary.abilitiesDictionary.Count; i++)
            {
                Abilities curAttribute = AbilitiesDictionary.abilitiesDictionary.ElementAt(i).Value;
                string allSkills = "Skill Checks for this attribute \n\n * ";
                foreach(string skill in curAttribute.Skills)
                {
                    allSkills = allSkills + skill + " \n\n * ";
                }
                HeroCard raceHeroCard = new HeroCard()
                {
                    Title = curAttribute.Name,
                    Subtitle = curAttribute.Measures,
                    Images = new List<CardImage>()
                        {
                            new CardImage() { Url = "http://cdn3-www.dogtime.com/assets/uploads/gallery/30-impossibly-cute-puppies/impossibly-cute-puppy-21.jpg" }
                        },
                    Text = allSkills,
                    Buttons = new List<CardAction>()
                        {
                            new CardAction()
                            {
                                Title = "Select "+ curAttribute.Name,
                                Type = ActionTypes.ImBack,
                                Value = curAttribute.Name
                            }
                        }

                };
                cards.Add(raceHeroCard);
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
                                Title = "Select "+ curRace.Name,
                                Type = ActionTypes.ImBack,
                                Value = curRace.Name
                            }
                        }

                };
                cards.Add(raceHeroCard);
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
                                Title = "Select "+ curClass.Name,
                                Type = ActionTypes.ImBack,
                                Value = curClass.Name
                            }
                        }

                };
                cards.Add(classHeroCard);
            }
            return cards;
        }

        public async Task charName(IDialogContext context, LuisResult result)
        {

            string message = "What would you like to name your character?";

            await context.PostAsync(message);
            //context.Wait(MessageReceived);


        }

        //'echobot' code for assigning a name. Nothing actually fires this yet. 
        //To Do
        public async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> argument)
        {
            var message = await argument;
            await context.PostAsync("Your name is: " + message.Text);
            StoredUserVals.PlayerCharacter.MyName = message.Text;
            context.Wait(MessageReceivedAsync);
        }

        public int[] setAttributes()
        {
            //this is the code for default die rolling
            //assigns 6 values between 3 and 18 to an array

            int[] AttributesAll = new int[6];

            for (int i = 0; i < AttributesAll.Length; i++)
            {
                AttributesAll[i] = roll4D6();
                Console.WriteLine("Attribute is: " + AttributesAll[i]);
            }

            return AttributesAll;
            //this allows to arrange attributes in order for the user
            //setMyAttributeOrder();

            //this is the code that will add your information from your race modifier
            //raceModifiersAdd();
        }

        public void setMyAttributeOrder()
        {
            //TO DO
        }

        public int roll4D6()
        {

            //roll 4D6

            Random rnd = new Random();

            int[] allDice = new int[4];

            for(int i = 0; i < allDice.Length ; i++)
            { 
                allDice[i] = rnd.Next(6) + 1;
                Console.WriteLine("Rolled: " + allDice[i]);
            }


            //drop the lowest

            var allDiceListed = new List<int>();
            allDiceListed.AddRange(allDice);

            allDiceListed.Sort();

            int diceResult = allDiceListed[1] + allDiceListed[2] + allDiceListed[3];
            Console.WriteLine("Die Roll = " + diceResult);

            //return result should be between 3 and 18

            return diceResult;
            

         }

        void raceModifiersAdd()
        {
            //if human, you have to let the user pick. TO DO for now

            //other cases do addition as so:

            switch (StoredUserVals.PlayerCharacter.MyRace)
            {
                case "Elf":
                break;
                case "Dwarf":
                break;
                case "Half-Elf":
                break;
                case "Gnome":
                break;
                case "Tiefling":
                break;
                case "Dragonborn":
                break;
                case "Halfling":
                break;
                case "Half-Orc":
                break;     

            }

        }

    }
       

}


