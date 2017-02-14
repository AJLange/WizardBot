using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace WizardBot.Dialogs
{
    [Serializable]
    public class ChoicesDialog
    {

        public class NewPlayerDialogs : IDialog<object>
        {
            public int count = 0;
            public async Task StartAsync(IDialogContext context)
            {
                await context.PostAsync("Test of second tier dialog");
                await Respond(context);
                //call context.Wait and set the callback method
                context.Wait(MessageReceivedAsync);

            }

            //in this draft, it doesn't validate for a real race or class.
            private static async Task Respond(IDialogContext context)
            {
                //Varible to hold user Race
                var userRace = String.Empty;
                //check to see if we already have userRace stored
                context.UserData.TryGetValue<string>("Race", out userRace);
                //If not, we will ask for it. 
                if (string.IsNullOrEmpty(userRace))
                {
                    //We ask here but dont capture it here, we do that in the MessageRecieved Async
                    await context.PostAsync("What Race do you choose?");
                    //We set a value telling us that we need to get the Race out of userdata
                    context.UserData.SetValue<bool>("GetRace", true);
                }
                else
                {
                    //If Race was already stored we will say hi to the user.
                    await context.PostAsync(String.Format("Hi {0}.  How can I help you today?", userRace));
                }
            }


public async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> argument)
{
    //variable to hold message coming in
    var message = await argument;
    //variable for userRace
    var userRace = String.Empty;
    //variable to hold whether or not we need to get Race
    var getRace = false;
    //see if Race exists
    context.UserData.TryGetValue<string>("Race", out userRace);
    //if GetRace exists we assign it to the getRace variable and replace false
    context.UserData.TryGetValue<bool>("GetRace", out getRace);
    //If we need to get Race, we go in here.
    if (getRace)
    {
        //we get the userRace we stored above. and set getRace to false
        userRace = message.Text;
        context.UserData.SetValue<string>("Race", userRace);
        context.UserData.SetValue<bool>("GetRace", false);
    }

    //we call respond again, this time it will print out the name and greeting
    await Respond(context);
    //call context.done to exit this dialog and go back to the root dialog
    context.Done(message);
}


  
        }
    }

}
