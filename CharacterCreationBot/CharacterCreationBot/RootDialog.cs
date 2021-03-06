﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis.Models;
using System.Threading.Tasks;
using Microsoft.Bot.Connector;
using CharacterCreationBot.Properties;
using static CharacterCreationBot.TakeTheTest;
using Microsoft.Bot.Builder.FormFlow;
using CharacterCreationBot.Models;
using System.Diagnostics;

namespace CharacterCreationBot
{
    [Serializable]
    public class RootDialog : IDialog<object> 
    {

        TakeTheTest.QuizForm ourtest;
        private ResumptionCookie resumptionCookie;

        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync(Resources.RootDialog_Welcome_Message);

            context.Wait(this.MessageReceivedAsync);
        }


        protected async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var message = await result;

            //await context.PostAsync("You said: " + message.Text + " . I'm in a cool dialog watch this... ");

            if (this.resumptionCookie == null)
            {
                this.resumptionCookie = new ResumptionCookie(message);
            }

            await this.WelcomeMessageAsync(context);
        }

        private async Task WelcomeMessageAsync(IDialogContext context)
        {
            // Build button selection...
            var resultMessage = context.MakeMessage();
            resultMessage.Attachments = new List<Attachment>();
            HeroCard heroCard = CreateWelcomeHeroCard();
            resultMessage.Attachments.Add(heroCard.ToAttachment());


            await context.PostAsync(resultMessage);


            context.Wait(this.OnWelcomeOptionSelected);
        }

        public HeroCard CreateWelcomeHeroCard()
        {
            HeroCard whc = new HeroCard()
            {
                Title = "Welcome",
                Subtitle = "Please select one of the following to get started",
                Images = new List<CardImage>()
                        {
                            new CardImage() { Url = "" }
                        },
                Buttons = new List<CardAction>()
                        {
                            new CardAction()
                            {
                                Title = "Learn more about Creating Characters",
                                Type = ActionTypes.ImBack,
                                Value = "LearnMore"
                            },
                            new CardAction()
                            {
                                Title = "Take a character Test",
                                Type = ActionTypes.ImBack,
                                Value = "Test"
                            },
                            new CardAction()
                            {
                                Title = "Start building your Character",
                                Type = ActionTypes.ImBack,
                                Value = "Build"
                            }
                        }
            };
            return whc;
        }


        private async Task OnWelcomeOptionSelected(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var message = await result;
            string response = "";
            switch (message.Text)
            {
                case "LearnMore":
                    response = "Okay, building a character is super fun but if you're new to D&D you might be a little overwhelmed. I'm here to help. Each Player Character has: \n"
                    + "* Class - What you do \n"
                    + "* Race - Who you are \n"
                    + "* Background - Where you come from \n"
                    + "* Alignment - How you behave \n"
                    + "* Abilities - How well you do, what you do ";
                    await context.PostAsync(response);


                    await this.LearnMoreMessageAsync(context);
                    break;
                case "Test":
                    response = "Let's take a quick test to find out your Class and Race!";
                    await context.PostAsync(response);
                    await this.TestMessageAsync(context);
                    break;
                case "Build":
                    response = "Building a character...";
                    await context.PostAsync(response);
                    await this.BuildCharacter(context);
                    break;
                default:
                    await this.StartOverAsync(context, message.Text);
                    break;
            }

        }

        private async Task TestMessageAsync(IDialogContext context)
        {

            IFormDialog<TakeTheTest.QuizForm> dialog = MakeRootDialog();
            context.Call(dialog, GetUserResponse);    
       }
        internal static IFormDialog<TakeTheTest.QuizForm> MakeRootDialog()
        {
            return FormDialog.FromForm(TakeTheTest.QuizForm.BuildForm, options: FormOptions.PromptInStart);
        }

        private async Task BuildCharacter(IDialogContext context)
        {


            await context.PostAsync("Thanks, character creation is not here yet but will be available soon at https://www.dndbeyond.com/");


            //For now I'm taking away the 'create a character' because it's skeletal. won't use it in first demo. - AJ
            /*
            var dialog = new CreateACharacter();
            context.Call(dialog, GetUserResponse); */

        }

        private async Task LearnMoreMessageAsync(IDialogContext context)
        {
            //If we put the new carolsel here it will not register the users input for the ListDialog and will need the user to select which option they would like to know more about twice...
            var resultMessage = context.MakeMessage();
            resultMessage.AttachmentLayout = AttachmentLayoutTypes.Carousel;
            resultMessage.Attachments = new List<Attachment>();
            List<HeroCard> information = CategoryCards();
            foreach (var card in information)
            {
                resultMessage.Attachments.Add(card.ToAttachment());
            }

            await context.PostAsync(resultMessage);
            ////await context.PostAsync(endMessage);

            //context.Wait(this.OnOptionSelected);

            //var dialog = new ListsDialog();
            var dialog = new LUISRoot();
            context.Call<ContextPasser>(dialog, this.BackFromLuis);

        }


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
                                Title = "Go to site",
                                Type = ActionTypes.OpenUrl,
                                Value = "http://dnd.wizards.com/dungeons-and-dragons/what-is-dd/races"
                            },
                            new CardAction()
                            {
                                Title = "List Races",
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
                                Title = "Go to site",
                                Type = ActionTypes.OpenUrl,
                                Value = "http://dnd.wizards.com/dungeons-and-dragons/what-is-dd/classes"
                            },
                            new CardAction()
                            {
                                Title = "List the Classes",
                                Type = ActionTypes.ImBack,
                                Value = "Classes"
                            }
                        }
            };
            cards.Add(classHeroCard);

            return cards;
        }

        private async Task OnOptionSelected(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var message = await result;

            if (message.Text == "Classes")
            {
                var dialog = new ListsDialog();
                context.Call(dialog, this.GetUserResponse);
                

            }
            else if (message.Text == "Races")
            {
                await context.PostAsync("You chose: " + message.Text);

                var dialog = new LUISRoot();
                context.Call(dialog, this.GetUserResponse);
            }
            else
            {
                await this.StartOverAsync(context, "Please select one of the options below.");
            }
        }


        private async Task GetUserResponse(IDialogContext context, IAwaitable<object> result)
        {
            string message = "";
            await this.ProcessMessageReceived(context, message);
        }

        public async Task ProcessMessageReceived(IDialogContext context, string response)
        {
            var themessage = response;

            switch (themessage)
            {
                case "Back":
                    await this.StartAsync(context);
                    break;
                default:
                    await this.StartAsync(context);
                    break;
            }
        }

        private async Task BackFromLuis(IDialogContext context, IAwaitable<ContextPasser> result)
        {
            // Returning from LUIS
            ContextPasser cp = null;
            try
            {
                cp = await result;
                
            }
            catch (OperationCanceledException)
            {
                await context.PostAsync("You canceled the form!");
                return;
            }

            Debug.WriteLine(result);
            switch (cp.Intent)
            {
                case "Back":                   
                    await StartOverAsync(context);
                    break;
                case "Test":               
                    await this.TestMessageAsync(context);
                    break;
                case "Build":              
                    await this.BuildCharacter(context);
                    break;
                default:
                    await StartOverAsync(context);
                    break;
            }

        }

        private async Task StartOverAsync(IDialogContext context)
        {
            await this.WelcomeMessageAsync(context);

        }

        private async Task StartOverAsync(IDialogContext context, string text)
        {
            var message = context.MakeMessage();
            message.Text = text;
            await this.StartOverAsync(context, message);
        }

        private async Task StartOverAsync(IDialogContext context, IMessageActivity message)
        {
            //await context.PostAsync(message);
            //this.order = new Models.Order();
            await this.WelcomeMessageAsync(context);
        }

    }
}
