﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CharacterCreationBot.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("CharacterCreationBot.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {
        ///  &quot;Abilities&quot; : [
        ///    {
        ///    &quot;Name&quot;: &quot;Strength&quot;,
        ///    &quot;Description&quot;: &quot;&quot;,
        ///    &quot;Measures&quot;:&quot;Natural athleticism, bodily power&quot;,
        ///    &quot;Important for&quot;:&quot;Barbarian, fighter, paladin&quot;,
        ///    &quot;Racial Increase&quot;:[
        ///      &quot;Mountain dwarf (+2)&quot;,
        ///      &quot;Half-orc (+2)&quot;,
        ///      &quot;Dragonborn (+2)&quot;,
        ///      &quot;Human (+1)&quot;
        ///      ],
        ///    &quot;Skills&quot;:[
        ///      &quot;Athletics&quot;
        ///    ]
        ///  },
        ///  {
        ///    &quot;Name&quot;: &quot;Dexterity&quot;,
        ///    &quot;Description&quot;: &quot;&quot;,
        ///    &quot;Measures&quot;:&quot;Physical agility, reflexes, balance, poise&quot;,
        ///    &quot;Important for&quot;:&quot;Monk, ra [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string AbilitiesJSON {
            get {
                return ResourceManager.GetString("AbilitiesJSON", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {
        ///  &quot;Background&quot;: [
        ///    {
        ///      &quot;Name&quot;: &quot;Acolyte&quot;,
        ///      &quot;Description&quot;: &quot;&quot;,
        ///      &quot;Skill Proficiencies&quot;: &quot;Insight, Religion&quot;,
        ///      &quot;Languages&quot;: &quot;Two of your choice&quot;,
        ///      &quot;Extra&quot;: &quot;&quot;,
        ///      &quot;Equipment&quot;: [
        ///        &quot;A holy symbol&quot;,
        ///        &quot;A prayer book or Prayer wheel&quot;,
        ///        &quot;5 sticks of incense&quot;,
        ///        &quot;Vestments&quot;,
        ///        &quot;A set of common clothes&quot;,
        ///        &quot;15gp&quot;
        ///      ]
        ///    },
        ///    {
        ///      &quot;Name&quot;: &quot;Charlatan&quot;,
        ///      &quot;Description&quot;: &quot;&quot;,
        ///      &quot;Skill Proficiencies&quot;: &quot;Natural athlet [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string BackgroundsJSON {
            get {
                return ResourceManager.GetString("BackgroundsJSON", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {
        ///  &quot;Class&quot;:[
        ///    {
        ///    &quot;Name&quot;:&quot;Barbarian&quot;,
        ///    &quot;Description&quot;:&quot;A fierce warrior of primitive background who can enter a battle rage. &quot;,
        ///    &quot;PicLink&quot;:&quot;&quot;,
        ///    &quot;MoreInfoLink&quot;:&quot;&quot;,
        ///    &quot;Proficiencies&quot;:{
        ///      &quot;Armor&quot;:&quot;Light armor, medium armor, shields&quot;,
        ///      &quot;Weapons&quot;:&quot;Simple weapons, martial weapons&quot;,
        ///      &quot;Tools&quot;:&quot;None&quot;,
        ///      &quot;Saving Throws&quot;:&quot;Strength, Constitution&quot;,
        ///      &quot;Skills&quot;:&quot;Choose two from Animal Handling, Athletics, Intimidation, Nature, Perception, and Survival&quot;
        ///    },
        ///    &quot;Pros&quot;: [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string ClassesJSON {
            get {
                return ResourceManager.GetString("ClassesJSON", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {
        ///&quot;Race&quot;: [
        ///{
        ///  &quot;Name&quot;: &quot;Human&quot;,
        ///  &quot;Description&quot;: &quot;Humans are the most adaptable and ambitious people among the common races. They have widely varying tastes, morals, and customs in the many different lands where they have settled. When they settle, though, they stay: they build cities to last for the ages, and great kingdoms that can persist for long centuries. An individual human might have a relatively short life span, but a human nation or culture preserves traditions with origins far beyond the rea [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string RacesJSON {
            get {
                return ResourceManager.GetString("RacesJSON", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Hi! I&apos;m a helpful bot to help you learn more about D&amp;D Character Creation. Would you like to learn more about D&amp;D character creation, take a test to find out which Race and Class would be best for you, or you can start building your character now..
        /// </summary>
        internal static string RootDialog_Welcome_Message {
            get {
                return ResourceManager.GetString("RootDialog_Welcome_Message", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Option1.
        /// </summary>
        internal static string RootDialog_Welcome_Option1 {
            get {
                return ResourceManager.GetString("RootDialog_Welcome_Option1", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Option2.
        /// </summary>
        internal static string RootDialog_Welcome_Option2 {
            get {
                return ResourceManager.GetString("RootDialog_Welcome_Option2", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Subtile also welcoming.
        /// </summary>
        internal static string RootDialog_Welcome_Subtitle {
            get {
                return ResourceManager.GetString("RootDialog_Welcome_Subtitle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Welcome Title.
        /// </summary>
        internal static string RootDialog_Welcome_Title {
            get {
                return ResourceManager.GetString("RootDialog_Welcome_Title", resourceCulture);
            }
        }
    }
}
