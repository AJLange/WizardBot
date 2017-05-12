# Wizard of the Coast Bot

### Authors
* Amanda Lange | Microsoft Technical Evangelist | Twitter: @second_truth
* Katherine "Kat" Harris | Microsoft Technical Evangelist (TE) | Twitter: @KatVHarris


## Summary
This Bot was created to aid in Character creation for DnD based on 5th Edition. 

WizardBot was created using the Microsoft Bot Framework. It uses the Bot Connector (C#) for integration to the Web, Facebook and Slack. Each class has a different sample of how to implement messaging for your Bot. The Natural Language processing is done through Microsoft's LUIS Serivce: https://www.luis.ai/

The updated version of the Bot is in the Character Creation Bot folder.

To run the Bot, open up the charactercreationbot.sln in Visual Studio, and run this locally. The bot will run in a browser. 

You will need the Bot Framework Emulator to interact with this draft of the bot. See https://docs.microsoft.com/en-us/bot-framework/debug-bots-emulator

## Interactions

Try saying "I'm new"

Or ask about Races, Classes, Alignment, Backgrounds, and Attributes for more information.

For now, all these things are hardcoded into the bot with data entry from local JSON.

If you want to see a Quiz that will allow you to choose a character class that suits you best, use the "Take a test" interaction for a formflow.




