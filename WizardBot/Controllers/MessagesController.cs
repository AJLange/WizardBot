﻿using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using System.Net.Http;
using System.Web.Http.Description;
using System.Diagnostics;



namespace Microsoft.Bot.MyBot
{
    [BotAuthentication]

    public class MessagesController : ApiController
    {

        internal static IDialog<DnDBot> MakeRootDialog()
        {

            return Chain.From(() => FormDialog.FromForm(DnDBot.BuildForm));

        }



        /// <summary>
        /// POST: api/Messages
        /// receive a message from a user and send replies
        /// </summary>

        /// <param name="activity"></param>

        [ResponseType(typeof(void))]

        public virtual async Task<HttpResponseMessage> Post([FromBody] Activity activity)
        {

            if (activity != null)
            {

                // one of these will have an interface and process it

                switch (activity.GetActivityType())
                {

                    case ActivityTypes.Message:
                        await Conversation.SendAsync(activity, MakeRootDialog);
                        break;

                    case ActivityTypes.ConversationUpdate:
                    case ActivityTypes.ContactRelationUpdate:
                    case ActivityTypes.Typing:
                    case ActivityTypes.DeleteUserData:
                    default:
                        Trace.TraceError($"Unknown activity type ignored: {activity.GetActivityType()}");
                        break;
                }
            }

            return new HttpResponseMessage(System.Net.HttpStatusCode.Accepted);
        }
    }
}