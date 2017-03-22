using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis.Models;

namespace CharacterCreationBot
{
    public class RootDialog : LuisDialog<object>
    {
        //public LuisServiceResult BestResultFrom(IEnumerable<LuisResult> results)
        //{
        //    var winners = from result in results
        //                  let resultWinner = BestIntentFrom(result)
        //                  where resultWinner != null
        //                  select new LuisServiceResult(result, resultWinner);
        //    var nonNoneWinner = winners.Where(i => i.BestIntent.Intent != "None").MaxBy(i => i.BestIntent.Score ?? 0d);
        //    return nonNoneWinner ?? winners.MaxBy(i => i.BestIntent.Score ?? 0d);
        //}
    }
}