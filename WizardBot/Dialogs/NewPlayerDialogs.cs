using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace WizardBot
{
    public class NewPlayerDialogs : IDialog<object>
    {
        private const string chooseClass = "Class";
        private const string chooseRace = "Race";
        private const string chooseAlign = "Alignment";
        private const string chooseName = "Name";

        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync("Welcome to the D&D character creator bot!");
            context.Wait(this.MessageReceivedAsync);
        }

        public virtual async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> argument)
        {
            var reply = context.MakeMessage();
            reply.Text = "When you first start making a character, the biggest choices you make are Race and Class. What Race would you like to choose?";
            reply.Attachments = new List<Attachment>();

            //this is how a card is generated. We'll want these for races and classes. This is a demo card.
            
            List<CardImage> HumanImages = new List<CardImage>();
            HumanImages.Add(new CardImage(url: "https://pbs.twimg.com/profile_images/760567451516018688/fVlIXTVf.jpg"));
            List<CardImage> ElfImages = new List<CardImage>();
            ElfImages.Add(new CardImage(url: "http://image.noelshack.com/fichiers/2013/23/1370466327-elf.jpg"));

            HeroCard humanCard = new HeroCard()
            {
                Title = "Human",
                Subtitle = "Information about humans goes here", //currentCharacterInfo.Quote,
                Images = HumanImages

            };
            HeroCard elfCard = new HeroCard()
            {
                Title = "Elf",
                Subtitle = "info about the elf race goes here", //currentCharacterInfo.Quote,
                Images = ElfImages

            };

            // here is how you display a card
            // 


            reply.Attachments.Add(elfCard.ToAttachment());

            await context.PostAsync(reply);
            context.Wait(MessageReceivedAsync);
            var message = await argument;

        }


    }
}
