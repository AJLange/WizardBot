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

         
            [Prompt("Do you feel like your character would be in tune with nature, in tune with arcane forces, or rely on a deity or outsider for their power? (Or None of the above?) {||}")]
            public Question2options? Choice2;

           
            [Prompt("Would you like a character that’s more self-reliant, or one that shines best in a team situation? {||}")]
            public Question3options? Choice3;

           
            [Prompt("Do you want a character that can heal other party members? {||} ")]
            public bool? HealerYesNo;
   

            [Prompt("Do you want a character who might fight alongside a pet? {||}")]
            public bool? PetYesNo;


            [Prompt("Do you see your character more as having intuitive powers, or more as having a skill that they have trained with discipline? Or does the power come from outside of them? {||}")]
            public Question6options? Choice6;


  
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
                              .Field(nameof(Choice1), validate: ValidateQuestion1)

                              .Field(nameof(Choice2), validate: ValidateQuestion2)

                              .Field(nameof(Choice3), validate: ValidateQuestion3)

                              .Field(nameof(HealerYesNo), validate: ValidateHealing)

                              .Field(nameof(PetYesNo), validate: ValidatePet)

                              .Field(nameof(Choice6), validate: ValidateQuestion6)


                           .AddRemainingFields()
                           .Message("Thanks for taking the quiz. Processing your result.")
                           
                          .OnCompletion(processQuiz)
                          
                          .Build();
                

            }

            private static async Task<ValidateResult> ValidateQuestion1(QuizForm state, object value)
            {
                var ChoiceMade = (Question1options)value;
                switch (ChoiceMade)
                {
                    case Question1options.weapons: BarbarianValue++; FighterValue++; MonkValue++; PaladinValue++; RogueValue++; RangerValue++; BarbarianValue++; FighterValue++; MonkValue++; PaladinValue++; RogueValue++; RangerValue++; break;
                    case Question1options.magic: BardValue++; ClericValue++; DruidValue++; SorcererValue++; WizardValue++; WarlockValue++; BardValue++; ClericValue++; DruidValue++; SorcererValue++; WizardValue++; WarlockValue++; break;
                    case Question1options.both: BardValue++; PaladinValue++; ClericValue++; DruidValue++; WarlockValue++; RogueValue++; RangerValue++; break;
                }

                return new ValidateResult { IsValid = true, Value = value };
            }


            private static async Task<ValidateResult> ValidateQuestion2(QuizForm state, object value)
            {
                var ChoiceMade = (Question2options)value;
                switch (ChoiceMade)
                {
                    case Question2options.nature: MonkValue++; DruidValue++; RangerValue++; MonkValue++; DruidValue++; RangerValue++; break;
                    case Question2options.arcane: BardValue++; SorcererValue++; WizardValue++; BardValue++; SorcererValue++; WizardValue++; break;
                    case Question2options.diety: PaladinValue++; ClericValue++; WarlockValue++; PaladinValue++; ClericValue++; WarlockValue++; break;
                    case Question2options.none: BarbarianValue++; FighterValue++; RogueValue++; BarbarianValue++; FighterValue++; RogueValue++; break;
                }

                return new ValidateResult { IsValid = true, Value = value };
            }

            private static async Task<ValidateResult> ValidateQuestion3(QuizForm state, object value)
            {
                var ChoiceMade = (Question3options)value;
                switch (ChoiceMade)
                {
                    case Question3options.self_reliant: BarbarianValue++; FighterValue++; DruidValue++; PaladinValue++; RogueValue++; SorcererValue++; RangerValue++; MonkValue++; break;
                    case Question3options.team: BardValue++; WarlockValue++; WizardValue++; ClericValue++; break;
                }

                return new ValidateResult { IsValid = true, Value = value };
            }

            private static async Task<ValidateResult> ValidateHealing(QuizForm state, object value)
            {
                var TrueOrFalse = (bool)value;
                switch (TrueOrFalse)
                {
                    case true: ClericValue++; BardValue++; DruidValue++; PaladinValue++; RangerValue++; ClericValue++; BardValue++; break;
                    case false: FighterValue++; BarbarianValue++; PaladinValue++; MonkValue++; WarlockValue++; RogueValue++; WizardValue++; break;
                }

                return new ValidateResult { IsValid = true, Value = value };
            }

            private static async Task<ValidateResult> ValidatePet(QuizForm state, object value)
            {
                var TrueOrFalse = (bool)value;
                switch (TrueOrFalse)
                {
                    case true: DruidValue++; SorcererValue++; WizardValue++; WarlockValue++; RangerValue++; RangerValue++; BardValue++; DruidValue++; SorcererValue++; WizardValue++; WarlockValue++; break;
                    case false: BarbarianValue++; FighterValue++; MonkValue++; PaladinValue++; RogueValue++; BardValue++; ClericValue++;  break;
                }

                return new ValidateResult { IsValid = true, Value = value };
            }

            private static async Task<ValidateResult> ValidateQuestion6(QuizForm state, object value)
            {
                var ChoiceMade = (Question6options)value;
                switch (ChoiceMade)
                {
                    case Question6options.inuitive: BarbarianValue++; FighterValue++; MonkValue++; PaladinValue++; RogueValue++; RangerValue++; break;
                    case Question6options.skill: BardValue++; ClericValue++; DruidValue++; SorcererValue++; WizardValue++; WarlockValue++; break;
                    case Question6options.outside: break;
                }

                return new ValidateResult { IsValid = true, Value = value };
            }

            public static string TestResults() {

                BuildForm();

            int[] Classesnums = { BarbarianValue, BardValue, ClericValue, DruidValue, FighterValue, MonkValue, RangerValue, RogueValue, PaladinValue, SorcererValue, WarlockValue, WizardValue };
                var ClassChoiceList = new List<int>();
                ClassChoiceList.AddRange(Classesnums);

                ClassChoiceList.Sort();
                Debug.Write(Classesnums);

                //make that the finalClass
                string SortedClass = "No Valid Class";

                if (ClassChoiceList[11] == BarbarianValue) { SortedClass = "Barbarian";  }
                if (ClassChoiceList[11] == BardValue) { SortedClass = "Bard";  }
                if (ClassChoiceList[11] == ClericValue) { SortedClass = "Cleric"; }
                if (ClassChoiceList[11] == DruidValue) { SortedClass = "Druid";  }
                if (ClassChoiceList[11] == FighterValue) { SortedClass = "Fighter"; }
                if (ClassChoiceList[11] == MonkValue) { SortedClass = "Monk";  }        
                if (ClassChoiceList[11] == RangerValue) { SortedClass = "Ranger";  }
                if (ClassChoiceList[11] == RogueValue) { SortedClass = "Rogue"; }
                if (ClassChoiceList[11] == PaladinValue) { SortedClass = "Paladin"; }
                if (ClassChoiceList[11] == SorcererValue) { SortedClass = "Sorcerer";  }
                if (ClassChoiceList[11] == WizardValue) { SortedClass = "Wizard";  }
                if (ClassChoiceList[11] == WarlockValue) { SortedClass = "Warlock"; }

                Debug.Write(ClassChoiceList);

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

            }

        }

    }


}