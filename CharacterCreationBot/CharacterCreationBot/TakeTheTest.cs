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
    public class TakeTheTest
    {
        
        //This quiz only chooses from PHB1 classes. Another variable is added for every weighted class.
        //this works but is probably not the most efficient way to do this long term.
        public enum Classtype { Barbarian, Bard, Cleric, Druid, Fighter, Monk, Paladin, Ranger, Rogue, Sorcerer, Warlock, Wizard };
        public enum Question1options { weapons, magic, both };
        public enum Question2options { nature, arcane, diety, none };
        public enum Question3options { team, self_reliant };
        public enum Question6options { inuitive, skill, outside };


        public static int BarbarianValue = 0;
        public static int BardValue = 0;
        public static int ClericValue = 0;
        public static int DruidValue = 0;
        public static int FighterValue = 0;
        public static int RangerValue = 0;
        public static int RogueValue = 0;
        public static int MonkValue = 0;
        public static int PaladinValue = 0;
        public static int SorcererValue = 0;
        public static int WarlockValue = 0;
        public static int WizardValue = 0;
        public static string finalClass;

        [Serializable]

        public class QuizForm
        {
            [Prompt("Do you imagine your character fighting primarily with weapons or physical prowess, mostly casting magic, or a combination of both? {||}")]

            public Question1options? Choice1;

            //if(Choice1 = 1){ BarbarianValue++; FighterValue++; MonkValue++; PaladinValue++; RogueValue++; RangerValue++; }
            //magic classes: { BardValue++; ClericValue++; DruidValue++; SorcererValue++; WizardValue++; WarlockValue++; }
            // combo classes: { BardValue++; PaladinValue++; ClericValue++; DruidValue++; WarlockValue++; RogueValue++; RangerValue++; }

            [Prompt("Do you feel like your character would be in tune with nature, in tune with arcane forces, or rely on a deity or outsider for their power? (Or None of the above?) {||}")]
            public Question2options? Choice2;

            //Natural: { MonkValue++; DruidValue++; RangerValue++; }
            //    Arcane { BardValue++; SorcererValue++; WizardValue++; }
            //   diety classes: { PaladinValue++; ClericValue++; WarlockValue++; }
            //   nada { BarbarianValue++; FighterValue++; RogueValue++; }

            [Prompt("Would you like a character that’s more self-reliant, or one that shines best in a team situation? {||}")]
            public Question3options? Choice3;

            //Self { BarbarianValue++; FighterValue++; DruidValue++; PaladinValue++; RogueValue++; SorcererValue++; RangerValue++; MonkValue++; }
            //    Teamwork { BardValue++; WarlockValue++; WizardValue++; ClericValue++; }



            [Prompt("Do you want a character that can heal other party members? {||} ")]
            public bool? HealerYesNo;
            //Canheal { ClericValue++; BardValue++; DruidValue++; PaladinValue++; RangerValue++; }
            // noheal { FighterValue++; BarbarianValue++; PaladinValue++; MonkValue++; WarlockValue++; RogueValue++; WizardValue++; SorcererValue++; }


            [Prompt("Do you want a character who might fight alongside a pet? {||}")]
            public bool? PetYesNo;

            // Canpet { DruidValue++; WizardValue++; RangerValue++; WarlockValue++; }
            //        nopet { ClericValue++; BardValue++; FighterValue++; BarbarianValue++; PaladinValue++; MonkValue++; RogueValue++; SorcererValue++; }



            [Prompt("Do you see your character more as having intuitive powers, or more as having a skill that they have trained with discipline? Or does the power come from outside of them? {||}")]
            public Question6options? Choice6;

            // Intuitive type classes: { BarbarianValue++; FighterValue++; SorcererValue++; }
            //            Disciplined type classes: { BardValue++; MonkValue++; WizardValue++; RogueValue++; RangerValue++; }
            //          Otherworldly type classes: { ClericValue++; DruidValue++; PaladinValue++; WarlockValue++; }


  
            public static IForm<QuizForm> BuildForm()
            {
                OnCompletionAsyncDelegate<QuizForm> processQuiz = async (context, state) =>
                     {
                      
                         await context.PostAsync("OK, let's see what we come up with.");
                         finalClass = TestResults();
                         await context.PostAsync("You Chose: " + finalClass);

                     };

                return new FormBuilder<QuizForm>()
                             .Message("OK, let's get started.")
                              .Field(nameof(Choice1),
                              validate: async (state, response) =>
                              {
                                      var result = new ValidateResult { IsValid = true, Value = response };
                                      switch (state.Choice1)
                                      {
                                          case Question1options.weapons: BarbarianValue++; FighterValue++; MonkValue++; PaladinValue++; RogueValue++; RangerValue++; BarbarianValue++; FighterValue++; MonkValue++; PaladinValue++; RogueValue++; RangerValue++; break;
                                          case Question1options.magic: BardValue++; ClericValue++; DruidValue++; SorcererValue++; WizardValue++; WarlockValue++; BardValue++; ClericValue++; DruidValue++; SorcererValue++; WizardValue++; WarlockValue++;  break;
                                          case Question1options.both: BardValue++; PaladinValue++; ClericValue++; DruidValue++; WarlockValue++; RogueValue++; RangerValue++; break;
                                      }
                                      return result;
                                  })

                              .Field(nameof(Choice2),
                               validate: async (state, response) =>
                           
                            {
                                var result = new ValidateResult { IsValid = true, Value = response };
                                switch (state.Choice2)
                                {
                                    case Question2options.nature: MonkValue++; DruidValue++; RangerValue++; MonkValue++; DruidValue++; RangerValue++; break;
                                    case Question2options.arcane: BardValue++; SorcererValue++; WizardValue++; BardValue++; SorcererValue++; WizardValue++; break;
                                    case Question2options.diety: PaladinValue++; ClericValue++; WarlockValue++; PaladinValue++; ClericValue++; WarlockValue++; break;
                                    case Question2options.none: BarbarianValue++; FighterValue++; RogueValue++; BarbarianValue++; FighterValue++; RogueValue++; break;
                                }
                                return result;
                            })

                              .Field(nameof(Choice3),
                             validate: async (state, response) =>
                           {
                                var result = new ValidateResult { IsValid = true, Value = response };
                                switch (state.Choice3)
                                {
                                    case Question3options.self_reliant: BarbarianValue++; FighterValue++; DruidValue++; PaladinValue++; RogueValue++; SorcererValue++; RangerValue++; MonkValue++; break;
                                    case Question3options.team: BardValue++; WarlockValue++; WizardValue++; ClericValue++; break;

                                }
                                return result;
                            })

                              .Field(nameof(HealerYesNo),
                            validate: async (state, response) =>
                            {
                                var result = new ValidateResult { IsValid = true, Value = response };
                                switch (state.HealerYesNo)
                                {
                                    case true: ClericValue++; BardValue++; DruidValue++; PaladinValue++; RangerValue++; ClericValue++; BardValue++; DruidValue++; PaladinValue++; RangerValue++; break;
                                    case false: FighterValue++; BarbarianValue++; PaladinValue++; MonkValue++; WarlockValue++; RogueValue++; WizardValue++; SorcererValue++; break;

                                }
                                return result;
                            })
                              .Field(nameof(PetYesNo),
                               validate: async (state, response) =>
                               {
                                   var result = new ValidateResult { IsValid = true, Value = response };
                                   switch (state.PetYesNo)
                                {
                                    case false: BarbarianValue++; FighterValue++; MonkValue++; PaladinValue++; RogueValue++; BardValue++; break;
                                    case true:  ClericValue++; DruidValue++; SorcererValue++; WizardValue++; WarlockValue++; RangerValue++; RangerValue++;  BardValue++; ClericValue++; DruidValue++; SorcererValue++; WizardValue++; WarlockValue++; break;
                                }
                                   return result;
                               })
                              .Field(nameof(Choice6), 
                            validate: async (state, response) =>
                            {
                                var result = new ValidateResult { IsValid = true, Value = response };
                                switch (state.Choice6)
                                {
                                    case Question6options.inuitive: BarbarianValue++; FighterValue++; MonkValue++; PaladinValue++; RogueValue++; RangerValue++; break;
                                    case Question6options.skill: BardValue++; ClericValue++; DruidValue++; SorcererValue++; WizardValue++; WarlockValue++; break;
                                    case Question6options.outside: break;
                                }
                                return result;
                            })

                           .AddRemainingFields()
                           .Message("Thanks for taking the quiz. Let's see what you came up with.")
                           
                          .OnCompletion(processQuiz)
                          
                          .Build();
                

            }



            public static string TestResults() {

                BuildForm();

            int[] Classesnums = { BarbarianValue, BardValue, ClericValue, DruidValue, FighterValue, MonkValue, RangerValue, RogueValue, PaladinValue, SorcererValue, WarlockValue, WizardValue };
                var ClassChoiceList = new List<int>();
                ClassChoiceList.AddRange(Classesnums);

                ClassChoiceList.Sort();

                //make that the finalClass
                string SortedClass = "No Valid Class";

                if (ClassChoiceList[0] == BarbarianValue) { SortedClass = "Barbarian";  }
                if (ClassChoiceList[0] == BardValue) { SortedClass = "Bard";  }
                if (ClassChoiceList[0] == ClericValue) { SortedClass = "Cleric"; }
                if (ClassChoiceList[0] == DruidValue) { SortedClass = "Druid";  }
                if (ClassChoiceList[0] == FighterValue) { SortedClass = "Fighter"; }
                if (ClassChoiceList[0] == MonkValue) { SortedClass = "Monk";  }
                if (ClassChoiceList[0] == RangerValue) { SortedClass = "Ranger";  }
                if (ClassChoiceList[0] == RogueValue) { SortedClass = "Rogue"; }
                if (ClassChoiceList[0] == PaladinValue) { SortedClass = "Paladin"; }
                if (ClassChoiceList[0] == SorcererValue) { SortedClass = "Sorcerer";  }
                if (ClassChoiceList[0] == WarlockValue) { SortedClass = "Warlock";  }
                if (ClassChoiceList[0] == WizardValue) { SortedClass = "Wizard";  }

                //TO DO
                //handle ties
                //if finalClass isn't loved, finalClass redefined as the second element in the list
                //finally return the value
                return SortedClass;

            }


            private async Task ChooseClass(IDialogContext context, IAwaitable<TakeTheTest.QuizForm> result)
            {

                StoredUserVals.PlayerCharacter.MyClass = result.ToString();
                var message = ("You Chose " + StoredUserVals.PlayerCharacter.MyClass);

                await context.PostAsync(message);
                //context.Wait(MessageReceived);


            }

        }

    }


}