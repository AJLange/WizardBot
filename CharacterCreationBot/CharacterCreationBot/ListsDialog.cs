using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using System.Threading.Tasks;
using Microsoft.Bot.Connector;
using CharacterCreationBot.Models;

namespace CharacterCreationBot
{
    [Serializable]
    public class ListsDialog : IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {
            // context.Wait(this.MessageReceivedAsync);

            // Needs to Put Cards here so we have something! 
            await this.ShowCategoryOptions(context);
        }

        // New
        protected async Task ShowCategoryOptions(IDialogContext context)
        {
            var resultMessage = context.MakeMessage();
            resultMessage.AttachmentLayout = AttachmentLayoutTypes.Carousel;
            resultMessage.Attachments = new List<Attachment>();
            List<HeroCard> information = CategoryCards();
            foreach (var card in information)
            {
                resultMessage.Attachments.Add(card.ToAttachment());
            }

            await context.PostAsync(resultMessage);
            //await context.PostAsync(endMessage);

            context.Wait(this.OnOptionSelected);
        }

        // NEW
        private async Task OnOptionSelected(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var message = await result;

            if (message.Text == "Classes")
            {
                //await context.PostAsync("You chose: " + message.Text);
                this.ReturnClasses(context);


            }
            else if (message.Text == "Races")
            {
                await context.PostAsync("You chose: " + message.Text);


            }
            else
            {
                await context.PostAsync("Need to restart " + message.Text);

            }
        }


        public async Task ReturnClasses(IDialogContext context)
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
            context.Wait(this.OnOptionSelected);
        }

        protected async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var message = await result;

            if(message.Text == "Classes")
            {
                await this.ReturnClasses(context);
                context.Wait(this.OnOptionSelected);
            }
            // TODO: validation
            if (message.Text.Equals("Back", StringComparison.InvariantCultureIgnoreCase))
            {
                await this.StartAsync(context);
            }
            else
            {
                await this.ProcessMessageReceived(context, message.Text);
            }
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

        public async Task ProcessMessageReceived(IDialogContext context, string response)
        {
            var themessage = response;

            if (themessage == "Classes") // if flower is a category we are done
            {
                //context.Done(themessage);
                //context.Wait(ReturnClasses);
            }
            else
            {
                await context.PostAsync("Invalid so reshowing you options");
                await this.ShowCategoryOptions(context);
                context.Wait(this.MessageReceivedAsync);
            }
        }


        // New
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
                                Title = "Learn more about Races",
                                Type = ActionTypes.ImBack,
                                Value = "Races"
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
                                Title = "Learn more about Classes",
                                Type = ActionTypes.ImBack,
                                Value = "Classes"
                            }
                        }
            };
            cards.Add(classHeroCard);

            return cards;
        }
    }
}