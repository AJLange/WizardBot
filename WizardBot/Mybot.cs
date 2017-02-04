using Microsoft.Bot.Builder.FormFlow;
using System;
using System.Collections.Generic;
#pragma warning disable 649


namespace Microsoft.Bot.MyBot
{
    //variables for initial selections

    public enum Racetype
    { Human, Dwarf, Elf, Half_Elf, Halfling, Dragonborn, Tiefling, Half_Orc, Gnome };
    public enum Classtype { Barbarian, Bard, Cleric, Druid, Fighter, Monk, Paladin, Ranger, Rogue, Sorcerer, Warlock, Wizard };

    public enum Backgroundtype
    { Acolyte, Charlatan, Criminal, Entertainer, Folk_Hero, Guild_Artisan, Hermit, Noble, Outlander, Sage, Sailor, Soldier, Urchin };

    [Serializable]
    public class DnDBot
    {
        public Racetype? MyRace;
        public Classtype? MyClass;
        public Backgroundtype? MyBackground;

        public static IForm<DnDBot> BuildForm()
        {
            return new FormBuilder<DnDBot>()
                    .Message("Welcome to the DnD Bot.")
                    .Build();
        }
    };
}