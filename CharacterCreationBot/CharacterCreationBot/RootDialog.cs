using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis.Models;
using System.Threading.Tasks;
using Microsoft.Bot.Connector;

namespace CharacterCreationBot
{
    public class RootDialog : IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);
        }

        // Test building Dictionary
        public virtual async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var message = await result;

            // Build dictionary
            if (RaceDictionary.raceDictionary.Count < 1)
            {
                RaceDictionary.BuildRacesFromJSON();


            }
            // Retrieve Objects

            await context.PostAsync(message.Text);
            context.Wait(MessageReceivedAsync);
        }
    }
}
