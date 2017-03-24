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


    public class CreateACharacter : LuisDialog<object>
    {
        [LuisIntent("Name")]
        public async Task charName(IDialogContext context, LuisResult result)
        {

            string message = "What would you like to name your character?";

            await context.PostAsync(message);
            context.Wait(MessageReceived);


        }

        public async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> argument)
        {
            var message = await argument;
            await context.PostAsync("You said: " + message.Text);
            StoredUserVals.PlayerCharacter.MyName = message.Text;
            context.Wait(MessageReceivedAsync);
        }

        public void setAttributes()
        {
            //this is the code for default die rolling
            //assigns 6 values between 3 and 18 to an array

            int[] AttributesAll = new int[6];

            foreach (int num in AttributesAll)
            {
                AttributesAll[num] = roll4D6();
                Console.WriteLine("Attribute is: " + AttributesAll[num]);
            }


            //this allows to arrange attributes in order for the user
            setMyAttributeOrder();

            //this is the code that will add your information from your race modifier

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

            foreach (int die in allDice)
            {
                allDice[die] = rnd.Next(6) + 1;
                Console.WriteLine("Rolled: " + allDice[die]);
            }


            //drop the lowest

            var allDiceListed = new List<int>();
            allDiceListed.AddRange(allDice);

            allDiceListed.Sort();

            int diceResult = allDice[0] + allDice[1] + allDice[2];
            Console.WriteLine("Die Roll = " + diceResult);

            //return result should be between 3 and 18

            return diceResult;
            

         }
}
       

    }


