using System;
using System.Runtime.ConstrainedExecution;
using UnityEngine;
using UnityEngine.UI;

namespace Unfair_plus_v2_1
{
    public class Unfair_plus
    {

        // In hard mode, level 3 requires 6 internet episodes, level 4 requires 6000 internet show viewers and 6 radio episodes, level 6 requires 6 tv episodes, level 9 Promotion requires 3 idols with 9 fame instead of 5
        //
        // Postfix of Activities.GetMaxLevel_Promotion(Activities._activity act)
        // Postfix: Unfair_plus_v2_1.Unfair_plus.GetMaxLevel_PromotionPostfix(num,act)
        public static int GetMaxLevel_PromotionPostfix(int __return, Activities._activity act)
        {
            int output = __return;
            if (staticVars.Settings.IsModEnabled("Unfair_plus"))
            {
                if (staticVars.IsHard())
                {
                    if (act.lvl < 3 && output >= 3 && CountEpisodesOfShowsWithType(Shows._param._media_type.internet) < 6)
                    {
                        output = 2;
                    }
                    else if (act.lvl < 4 && output >= 4 &&
                        (Shows.GetPeakAudience(new Shows._param._media_type?(Shows._param._media_type.internet)) < 6000 || CountEpisodesOfShowsWithType(Shows._param._media_type.radio) < 6)
                        )
                    {
                        output = 3;
                    }
                    else if (act.lvl < 6 && output >= 6 && CountEpisodesOfShowsWithType(Shows._param._media_type.tv) < 6)
                    {
                        output = 5;
                    }
                    else if (act.lvl < 9 && output >= 9 && data_girls.GetNumberOfIdolsWithFame(9) < 3)
                    {
                        output = 8;
                    }
                }
            }
            return output;
        }

        // In hard mode, level 3 requires 6 internet episodes, level 4 requires 6000 internet show viewers and 6 radio episodes, level 6 requires 6 tv episodes, level 9 Promotion requires 3 idols with 9 fame instead of 5
        //
        // Postfix of Activities.GetPromotionDescription(int lvl, int actualLvl = 0)
        // Postfix: Unfair_plus_v2_1.Unfair_plus.GetPromotionDescriptionPostfix(text,lvl,actualLvl)
        public static string GetPromotionDescriptionPostfix(string __return, int lvl, int actualLvl)
        {
            string output = __return;
            if (Unfair_plus_v2_1_1.BugFix.IsModEnabled("Unfair_plus"))
            {
                if (staticVars.IsHard())
                {
                    string text;
                    string clr = mainScript.blue;
                    if (actualLvl + 1 < lvl)
                    {
                        clr = mainScript.red;
                    }
                    if (lvl == 3)
                    {
                        text = Language.Data["ACTIVITIES__MAGAZINES"] + ": ";
                        long num;
                        if (actualLvl >= lvl || Stats.data.business.photoshoot_counter >= 3)
                        {
                            num = 3;
                        }
                        else
                        {
                            num = (long)Stats.data.business.photoshoot_counter;
                        }
                        text = string.Concat(new object[]
                        {
                            text,
                            num,
                            " / ",
                            3
                        });
                        if (num == (long)3)
                        {
                            text = ExtensionMethods.color(text, mainScript.green);
                        }
                        else
                        {
                            text = ExtensionMethods.color(text, clr);
                        }
                        string text3 = Language.Data["UNFAIR_PLUS__ACTIVITIES__INTERNETSHOW"] + ": ";
                        long num2;
                        if (actualLvl >= lvl || CountEpisodesOfShowsWithType(Shows._param._media_type.internet) >= 6)
                        {
                            num2 = 6;
                        }
                        else
                        {
                            num2 = (long)CountEpisodesOfShowsWithType(Shows._param._media_type.internet);
                        }
                        text3 = string.Concat(new object[]
                        {
                            text3,
                            num2,
                            " / 6"
                        });
                        if (actualLvl >= lvl || CountEpisodesOfShowsWithType(Shows._param._media_type.internet) >= 6)
                        {
                            text3 = ExtensionMethods.color(text3, mainScript.green);
                        }
                        else
                        {
                            text3 = ExtensionMethods.color(text3, clr);
                        }
                        output = text + "\n" + text3;
                    }
                    if (lvl == 4)
                    {
                        string text2 = Language.Data["ACTIVITIES__INTERNETPEAK"] + ": ";
                        long num;
                        if (actualLvl < lvl)
                        {
                            num = Shows.GetPeakAudience(new Shows._param._media_type?(Shows._param._media_type.internet));
                            if (num > (long)6000)
                            {
                                num = (long)6000;
                            }
                        }
                        else
                        {
                            num = (long)6000;
                        }
                        text2 = text2 + ExtensionMethods.formatNumber(num, false, false) + " / " + ExtensionMethods.formatNumber(6000, false, false);
                        if (actualLvl >= lvl || num == (long)6000)
                        {
                            text2 = ExtensionMethods.color(text2, mainScript.green);
                        }
                        else
                        {
                            text2 = ExtensionMethods.color(text2, clr);
                        }
                        string text3 = Language.Data["UNFAIR_PLUS__ACTIVITIES__RADIOSHOW"] + ": ";
                        long num2;
                        if (actualLvl >= lvl || CountEpisodesOfShowsWithType(Shows._param._media_type.radio) >= 6)
                        {
                            num2 = 6;
                        }
                        else
                        {
                            num2 = (long)CountEpisodesOfShowsWithType(Shows._param._media_type.radio);
                        }
                        text3 = string.Concat(new object[]
                        {
                            text3,
                            num2,
                            " / 6"
                        });
                        if (actualLvl >= lvl || CountEpisodesOfShowsWithType(Shows._param._media_type.radio) >= 6)
                        {
                            text3 = ExtensionMethods.color(text3, mainScript.green);
                        }
                        else
                        {
                            text3 = ExtensionMethods.color(text3, clr);
                        }
                        output = text2 + "\n" + text3;
                    }
                    if (lvl == 6)
                    {
                        string text2 = Language.Data["ACTIVITIES__MAGAZINES"] + ": ";
                        long num;
                        if (actualLvl >= lvl || Stats.data.business.photoshoot_counter >= 24)
                        {
                            num = (long)24;
                        }
                        else
                        {
                            num = (long)Stats.data.business.photoshoot_counter;
                        }
                        text2 = string.Concat(new object[]
                        {
                            text2,
                            num,
                            " / ",
                            24
                        });
                        if (num == (long)24)
                        {
                            text2 = ExtensionMethods.color(text2, mainScript.green);
                        }
                        else
                        {
                            text2 = ExtensionMethods.color(text2, clr);
                        }
                        string text3 = Language.Data["UNFAIR_PLUS__ACTIVITIES__TVSHOW"] + ": ";
                        long num2;
                        if (actualLvl >= lvl || CountEpisodesOfShowsWithType(Shows._param._media_type.tv) >= 6)
                        {
                            num2 = 6;
                        }
                        else
                        {
                            num2 = (long)CountEpisodesOfShowsWithType(Shows._param._media_type.tv);
                        }
                        text3 = string.Concat(new object[]
                        {
                            text3,
                            num2,
                            " / 6"
                        });
                        if (actualLvl >= lvl || CountEpisodesOfShowsWithType(Shows._param._media_type.tv) >= 6)
                        {
                            text3 = ExtensionMethods.color(text3, mainScript.green);
                        }
                        else
                        {
                            text3 = ExtensionMethods.color(text3, clr);
                        }
                        output = text2 + "\n" + text3;
                    }
                    if (lvl == 9)
                    {
                        text = string.Concat(new object[]
                        {
                            Language.Data["ACTIVITIES__IDOLSLEVEL"],
                            " 9 ",
                            Language.Data["FAME"].ToLower(),
                            ": "
                        });
                        long num = (long)data_girls.GetNumberOfIdolsWithFame(9);
                        if (actualLvl >= lvl || num >= 3)
                        {
                            text = ExtensionMethods.color(text + "3 / 3", mainScript.green);
                        }
                        else
                        {
                            text = ExtensionMethods.color(text + ExtensionMethods.formatNumber(num, false, false) + " / 3", clr);
                        }
                        output = text;
                    }
                }
            }
            return output;
        }

        public static int CountEpisodesOfShowsWithType(Shows._param._media_type type)
        {
            int maxEpisodes = 0;
            foreach (Shows._show show in Shows.shows)
            {
                if (show.status != Shows._show._status.working)
                {
                    Shows._param._media_type? media_type = show.medium.media_type;
                    if (media_type.GetValueOrDefault() == type & media_type != null)
                    {
                        if(show.episodeCount >= maxEpisodes)
                        {
                            maxEpisodes = show.episodeCount;
                        }
                    }
                }
            }
            return maxEpisodes;
        }

    }
}