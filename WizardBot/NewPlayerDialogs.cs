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
        public int count = 0; 
        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);
        }

        public virtual async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> argument)
        {
            var reply = context.MakeMessage();
            reply.Text = "Heloo";
            reply.Attachments = new List<Attachment>();
            List<CardImage> cardImages = new List<CardImage>();
            cardImages.Add(new CardImage(url: "https://pbs.twimg.com/profile_images/760567451516018688/fVlIXTVf.jpg"));
            HeroCard plCard = new HeroCard()
            {
                Title = "Name: " + "Kat",
                Subtitle = "Subtitle", //currentCharacterInfo.Quote,
                Images = cardImages

            };
            reply.Attachments.Add(plCard.ToAttachment());
            await context.PostAsync(reply);
            context.Wait(MessageReceivedAsync);
            //var message = await argument;
            //if (message.Text == "reset")
            //{
            //    PromptDialog.Confirm(
            //        context,
            //        AfterResetAsync,
            //        "Are you sure you want to reset the count?",
            //        "Didn't get that!",
            //        promptStyle: PromptStyle.None);
            //}
            //else
            //{
            //    await context.PostAsync($"{this.count++}: You said {message.Text}");
            //    context.Wait(MessageReceivedAsync);
            //}
        }

        public async Task AfterResetAsync(IDialogContext context, IAwaitable<bool> argument)
        {
            var confirm = await argument;
            if (confirm)
            {
                this.count = 1;
                await context.PostAsync("Reset count.");
            }
            else
            {
                await context.PostAsync("Did not reset count.");
            }
            context.Wait(MessageReceivedAsync);
        }
    }
}
