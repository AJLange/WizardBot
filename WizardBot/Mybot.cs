using Microsoft.Bot.Builder.FormFlow;
using System;
using System.Collections.Generic;
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
    { Random, Standard_Array, Point_buy }

    [Serializable]
    public class DnDBot
    {
        [Prompt("What race do you want your character to be? {||}")]
        public Racetype? MyRace;
        //AddPlayerValue("MyRace", { MyRace});
        [Prompt("Great choice, {MyRace}. Select a class for your {&} {||}")]
        public Classtype? MyClass;
        [Prompt("Choose a Background {&} {||}")]
        public Backgroundtype? MyBackground;
        [Prompt("Do you want to roll attributes randomly (1), choose from the standard array (2), or choose a custom point buy (3)?")]
        public AttrtypeSelection? MyAttrtype;
        //if(MyAttrtype == 1 ) {

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
        public double? Charisma;
        public Alignmenttype? MyAlignment;

        public static IForm<DnDBot> BuildForm()
        {
            return new FormBuilder<DnDBot>()
                    .Message("Welcome to the DnD Bot.")
                    .Build();
        }

        public static void AddPlayerValue(string key, string value)
        {
            if (key == "MyRace")
                PlayerCharcter.MyRace = value;
        }
    };
}