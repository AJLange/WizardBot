using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.Bot.Connector;

namespace CharacterCreationBot
{
    [Serializable]
    public class CommandDialog : IDialog<object>
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
            if(RaceDictionary.raceDictionary.Count < 1)
            {
                RaceDictionary.BuildRacesFromJSON();


            }
            // Retrieve Objects

            await context.PostAsync(message.Text);
            context.Wait(MessageReceivedAsync);
        }
    }
}