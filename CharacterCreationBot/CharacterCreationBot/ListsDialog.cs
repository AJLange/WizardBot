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
            context.Wait(this.MessageReceivedAsync);
        }


        protected async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var message = await result;
            if(message.Text == "Classes")
            {
                await this.ReturnClasses(context);
            }

            await context.PostAsync(message);
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
    }
}