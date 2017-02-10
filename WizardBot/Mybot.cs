using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Builder.FormFlow.Advanced;
using Newtonsoft.Json.Linq;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using WizardBot;
#pragma warning disable 649


namespace Microsoft.Bot.MyBot
{
    //variables for initial selections

    public enum Racetype
    { Human, Dwarf, Elf, Half_Elf, Halfling, Dragonborn, Tiefling, Half_Orc, Gnome };
    public enum Classtype { Barbarian, Bard, Cleric, Druid, Fighter, Monk, Paladin, Ranger, Rogue, Sorcerer, Warlock, Wizard };

    public enum Backgroundtype
    { Acolyte, Charlatan, Criminal, Entertainer, Folk_Hero, Guild_Artisan, Hermit, Noble, Outlander, Sage, Sailor, Soldier, Urchin };

    public enum Alignmenttype
    { Lawful_Good, Neutral_Good, Chaotic_Good, Lawful_Neutral, True_Neutral, Chaotic_Neutral, Lawful_Evil, Neutral_Evil, Chaotic_Evil };

    public enum AttrtypeSelection
    { Random, Standard_Array, Point_Buy }


    public enum SpellSelection

    {

        // This starts at 1 because 0 is the "no value" value

        //[Terms("except", "but", "not", "no", "all", "everything")]

        //Everything = 1,

        //this needs to be adjusted. You only get 3 spells. Or however many your stat allows?

        Alarm, Burning_Hands, Charm_Person, Color_Spray, Comprehend_Languages, Detect_Magic, Disguise_Self, Expeditious_Retreat,
False_Life, Feather_Fall, Find_Familiar, Floating_Disk, Fog_Cloud, Grease, Hideous_Laughter, Identify,
Illusory_Script, Jump, Longstrider, Mage_Armor, Magic_Missile, Protection_from_Evil_and_Good, Shield, Silent_Image, Sleep, Thunderwave, Unseen_Servant

    };

    public enum GearSelection

    {
        // This is just some trash as a placeholder for a proper gear selection menu.

      Weapon, Armor, Torch, Lockpicking_Kit, Disguise_Kit, War_Horse

    };



    [Serializable]
    public class DnDBot
    {
        [Prompt("What race do you want your character to be? {||}")]
        public Racetype? MyRace;

        [Prompt("Great choice, {MyRace}. Now, select a class for your character. {||}")]
        public Classtype? MyClass;
        [Prompt("Next, choose a background. {||}")]
        public Backgroundtype? MyBackground;
        [Prompt("Do you want to roll attributes randomly (1), choose from the standard array (2), or choose a custom point buy (3)? {||}")]
        public AttrtypeSelection? MyAttrtype;
        //I need to do the switch case here.
        //screw this until I actually need it - it's tedious to test
        /*
        [Numeric(3, 18)]
        public double? Strength;
        [Numeric(3, 18)]
        public double? Dexterity;
        [Numeric(3, 18)]
        public double? Constitution;
        [Numeric(3, 18)]
        public double? Intelligence;
        [Numeric(3, 18)]
        public double? Wisdom;
        [Numeric(3, 18)]
        public double? Charisma; */

        public Alignmenttype? MyAlignment;

        [Optional]
        [Prompt("Members of the {MyClass} class can choose from the following spells. Please choose three. Put commas between your choices. {||}")]
         public List<SpellSelection> MySpells { get; set; }

        [Prompt("Please select your starting gear. {||}")]
        public List<GearSelection> MyGear { get; set; }

        public static IForm<DnDBot> BuildForm()
        {

            OnCompletionAsyncDelegate<DnDBot> processOrder = async (context, state) =>

            {

                await context.PostAsync("Goodbye!");

            };

            return new FormBuilder<DnDBot>()
                    .Message("Welcome to the DnD Bot. This bot will walk you through the process of building a DnD character.")


                                     //Advanced bot stuff. It's a bit slow.

                                     .Field(nameof(MyRace))
                                     .Field(nameof(MyClass))
                                     .Field(nameof(MyBackground))
                                     .Field(nameof(MyAttrtype))
                                     .Field(nameof(MyAlignment))
                                     .Field(nameof(MySpells))

                                     /*  .Field(new FieldReflector<DnDBot>(nameof(MySpells))
                         .SetType(null)
                         .SetActive((state) => state.MyClass == Classtype.Wizard)
                         .SetDefine(async (state, field) =>
                         {
                             field
                                 .AddDescription("Magic Missile", "Magic Missile")
                                 .AddTerms("Magic missile", "Magic Missile")
                                 .AddDescription("Floating Disc", "Floating Disc")
                                 .AddTerms("Disc", "floating", "floating disc");
                             return true;
                         }))

                  .SetActive((state) => state.MyClass == Classtype.Druid)
                         .SetDefine(async (state, field) =>
                         {
                             field
                                 .AddDescription("Animal Friendship", "Animal Friendship")
                                 .AddTerms("Animal Friendship", "Animal Friendship")
                                 .AddDescription("Cure Wounds", "Cure Wounds")
                                 .AddTerms("cure", "wounds", "cure wounds");
                             return true;
                         })) */


                                   
                                     .Field(nameof(MyGear))

                        .Confirm("Your character is a {MyRace} {MyClass} who was once a {MyBackground} and is {MyAlignment}. You have the following spells: {MySpells} and the following gear: {MyGear}. Sound good?")
                        .AddRemainingFields()
                        .Message("Thanks for making a D&D character!")
                        .OnCompletion(processOrder)



                    .Build();
        } 
/*
        public static void AddPlayerValue(string key, string value)
        {
            if (key == "MyRace")
                PlayerCharcter.MyRace = value;
        } */
    };
}