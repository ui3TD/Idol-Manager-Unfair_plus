using System.Collections.Generic;
using System;
using UnityEngine;

namespace Unfair_plus_v2_0
{
    internal class GameplayChanges
    {
        // Time passes 5x faster
        //
        // Postfix of mainScript.Time_SetState(mainScript._time_state state)
        // Postfix: Unfair_plus_v2_0.GameplayChanges.Time_SetStatePostfix();
        public static void Time_SetStatePostfix()
        {
            
            if (Unfair_plus_v2_1_1.BugFix.IsModEnabled("Unfair_plus"))
            {
                if (staticVars.timeState == mainScript._time_state.normal)
                {
                    staticVars.dateTimeAddMinutesPerSecond = 250.0;
                }
                if (staticVars.timeState == mainScript._time_state.fast)
                {
                    staticVars.dateTimeAddMinutesPerSecond = 1000.0;
                }
            }
        }

        // Training vocal/dance stamina reduced to 1/day
        //
        // Infix for float num in agency._room.DoGirlTraining()
        // Infix after float num = 3f / Mathf.Floor(1440f / (float)(staticVars.dateTimeAddMinutesPerSecond / (double)staticVars.dateTimeDivider));
        // Infix: num = Unfair_plus_v2_0.GameplayChanges.DoGirlTrainingInfix(num);
        public static float DoGirlTrainingInfix(float num)
        {
            float trainingIncrement = num;
            if (Unfair_plus_v2_1_1.BugFix.IsModEnabled("Unfair_plus"))
            {
                trainingIncrement /= 3;
            }
            return trainingIncrement;
        }

        // Removed minimum member limit for sister group creation
        //
        // Postfix of Groups.GetIdolsNeededForNewGroup()
        // Postfix: Unfair_plus_v2_0.GameplayChanges.GetIdolsNeededForNewGroupPostFix(Groups.CountActiveGroups() * 10)
        public static int GetIdolsNeededForNewGroupPostFix(int __return)
        {
            int output = __return;
            if (Unfair_plus_v2_1_1.BugFix.IsModEnabled("Unfair_plus"))
            {
                output = Groups.CountActiveGroups() + 1;
            }
            return output;
        }

        // Home row of keyboard (asdfg...) are hotkeys to open each sidebar tab.
        // 
        // Prefix of Controls.Update()
        // Prefix: if(!Unfair_plus_v2_0.GameplayChanges.UpdatePreFix()) return;
        public static bool UpdatePreFix()
        {
            if (Unfair_plus_v2_1_1.BugFix.IsModEnabled("Unfair_plus"))
            {
                if (mainScript.IsBlockingHotkeys())
                {
                    return false;
                }
                if (Input.GetKeyDown(KeyCode.A))
                {
                    Camera.main.GetComponent<mainScript>().Data.GetComponent<Tabs_Manager>().OpenTab(Tabs_Manager._tab._type.idols);
                }
                else if (Input.GetKeyDown(KeyCode.S))
                {
                    Camera.main.GetComponent<mainScript>().Data.GetComponent<Tabs_Manager>().OpenTab(Tabs_Manager._tab._type.staff);
                }
                else if (Input.GetKeyDown(KeyCode.D))
                {
                    Camera.main.GetComponent<mainScript>().Data.GetComponent<Tabs_Manager>().OpenTab(Tabs_Manager._tab._type.activities);
                }
                else if (Input.GetKeyDown(KeyCode.F))
                {
                    Camera.main.GetComponent<mainScript>().Data.GetComponent<Tabs_Manager>().OpenTab(Tabs_Manager._tab._type.singles);
                }
                else if (Input.GetKeyDown(KeyCode.G))
                {
                    Camera.main.GetComponent<mainScript>().Data.GetComponent<Tabs_Manager>().OpenTab(Tabs_Manager._tab._type.media);
                }
                else if (Input.GetKeyDown(KeyCode.H))
                {
                    Camera.main.GetComponent<mainScript>().Data.GetComponent<Tabs_Manager>().OpenTab(Tabs_Manager._tab._type.specialEvents);
                }
                else if (Input.GetKeyDown(KeyCode.J))
                {
                    Camera.main.GetComponent<mainScript>().Data.GetComponent<Tabs_Manager>().OpenTab(Tabs_Manager._tab._type.research);
                }
                else if (Input.GetKeyDown(KeyCode.K))
                {
                    Camera.main.GetComponent<mainScript>().Data.GetComponent<Tabs_Manager>().OpenTab(Tabs_Manager._tab._type.policies);
                }
                if (DEBUG.Debug_Enabled)
                {
                    if (Input.GetKeyDown(KeyCode.F))
                    {
                        resources.Add(resources.type.fans, 9900L);
                    }
                }
            }
            return true;
        }

        // Staff cannot be fired using scandal points within the first month
        //
        // Postfix of staff._staff.CanFire()
        // Postfix: Unfair_plus_v2_0.GameplayChanges.CanFirePostfix(this,this.UniqueType == staff._staff._unique_type.NONE && this.type != staff._type.player && this.type != staff._type.player_female && (long)this.PointsToFire() <= resources.Get(resources.type.scandalPoints, true) && (staticVars.dateTime - staff.LastFiredDate).TotalDays > (double)staff.FiringCooldown)
        public static bool CanFirePostfix(staff._staff __this, bool __return)
        {
            bool output = __return;
            if (Unfair_plus_v2_1_1.BugFix.IsModEnabled("Unfair_plus"))
            {
                if ((staticVars.dateTime - __this.HireDate).Days < 30)
                {
                    output = false;
                }
            }
            return output;
        }

        // Theater show sales and subscriptions start to decay after 30 days
        //
        // Postfix for Theaters._theater.GetPriceCoeff(int Price)
        // Postfix: Unfair_plus_v2_0.GameplayChanges.GetPriceCoeffPostfix(Price, this, function.GetY((float)Price))
        public static float GetPriceCoeffPostfix(int Price, Theaters._theater __this, float __return)
        {
            float output = __return;
            if (Unfair_plus_v2_1_1.BugFix.IsModEnabled("Unfair_plus"))
            {
                if (Price <= 30000)
                {
                    LinearFunction._function linear = new LinearFunction._function();
                    int daysSinceSingle = (staticVars.dateTime - singles.GetLatestReleasedSingle(false, __this.GetGroup()).ReleaseData.ReleaseDate).Days;
                    if (daysSinceSingle > 30)
                    {
                        linear.Init(30f, 1f, 366f, 0.1f);
                        if (staticVars.IsHard())
                        {
                            linear.Init(30f, 1f, 183f, 0.1f);
                        }
                        output *= linear.GetY((float)daysSinceSingle);
                    }
                    if (output < 0.001f)
                    {
                        output = 0.001f;
                    }
                }
            }
            return output;
        }

        // Theater subscription revenue decreased by 90%
        //
        // Postfix for Theaters._theater.GetSubRevenue()
        // Postfix: Unfair_plus_v2_0.GameplayChanges.GetSubRevenuePostfix(this.GetSubscribers() * (long)this.Subscription_Price)
        public static long GetSubRevenuePostfix(long __return)
        {
            long output = __return;
            if (Unfair_plus_v2_1_1.BugFix.IsModEnabled("Unfair_plus"))
            {
                output = (long)Mathf.Round(output * 0.1f);
            }
            return output;
        }

        // Theater attendance increased for 'everyone', 'casual' and age groups
        //
        // Infix for float num in Theaters._theater.GetNumberofVisitors() 
        // Infix before num *= this.GetPriceCoeff(this.Ticket_Price);
        // Infix: num = Unfair_plus_v2_0.GameplayChanges.GetNumberofVisitorsPostFix(num,this);
        public static float GetNumberofVisitorsPostFix(float num, Theaters._theater __this)
        {
            float output = num;
            if (Unfair_plus_v2_1_1.BugFix.IsModEnabled("Unfair_plus"))
            {
                Theaters._theater._schedule schedule = __this.GetSchedule();
                if (schedule.FanType_Everyone && __this.Doing_Now == Theaters._theater._schedule._type.manzai)
                {
                    output = 0.2f;
                }
                else if (schedule.FanType_Everyone)
                {
                    output = 0.3f;
                }
                else if (schedule.FanType == resources.fanType.casual)
                {
                    output = 0.65f;
                }
                else if (schedule.FanType == resources.fanType.adult || schedule.FanType == resources.fanType.youngAdult)
                {
                    output = 0.95f;
                }
                else if (schedule.FanType == resources.fanType.teen)
                {
                    output = 0.9f;
                }
                else if (schedule.FanType != resources.fanType.hardcore)
                {
                    output = 0.75f;
                }
                if (!schedule.FanType_Everyone && __this.Doing_Now == Theaters._theater._schedule._type.manzai && schedule.FanType != resources.fanType.hardcore)
                {
                    output *= 0.75f;
                }
                if (__this.Doing_Now == Theaters._theater._schedule._type.manzai)
                {
                    output /= 2f;
                }
            }
            return output;
        }


        // Each MC fame point gives a boost to new fans
        //
        // Infix for int num7 in Shows._show.SetSales()
        // Infix before if (this.episodeCount == 1 || this.episodeCount == 0)
        // Infix: num7 = Unfair_plus_v2_0.GameplayChanges.SetSalesInfix(num7,this);
        public static int SetSalesInfix(int num7, Shows._show __this)
        {
            int output = num7;
            if (Unfair_plus_v2_1_1.BugFix.IsModEnabled("Unfair_plus"))
            {
                float mcCoeff = 1f;
                if (__this.mc != null)
                {
                    mcCoeff = 1f + (float)__this.mc.fame * (float)__this.mc.fame / 100f;
                    if (mcCoeff < 1f)
                    {
                        mcCoeff = 1f;
                    }
                }
                output = Mathf.RoundToInt(output * mcCoeff);
                if (output < 1)
                {
                    output = 1;
                }
            }
            return output;
        }

        // 20000 yen/wk is the expected starting salary for 100% satisfaction
        //
        // Postfix for data_girls.girls.GetExpectedSalary()
        // Postfix: Unfair_plus_v2_0.GameplayChanges.GetExpectedSalaryPostfix(Mathf.FloorToInt((531435.2f + -531429.7f / (1f + Mathf.Pow(num / 43.12236f, 6.423545f))) * 10000f),this)
        public static int GetExpectedSalaryPostfix(int __return, data_girls.girls __this)
        {
            int output = __return;
            if (Unfair_plus_v2_1_1.BugFix.IsModEnabled("Unfair_plus"))
            {
                float num = (float)__this.GetFameLevel();
                if (num < 1f)
                {
                    output = 40000;
                }
            }
            return output;
        }

        // Probabilty of being lesbian reduced to 6% and probability for bisexuality reduced to 5% if the player is male
        //
        // Postfix for data_girls.GenerateGirl(bool genTextures = true, Auditions.data._girl._type Type = Auditions.data._girl._type.normal, data_girls_textures._textureAsset BodyAsset = null)
        // Postfix: Unfair_plus_v2_0.GameplayChanges.GenerateGirlPostfix(girls)
        public static data_girls.girls GenerateGirlPostfix(data_girls.girls __return)
        {
            data_girls.girls output = __return;
            if (Unfair_plus_v2_1_1.BugFix.IsModEnabled("Unfair_plus"))
            {
                output.salary = 20000;

                if (!staticVars.PlayerData.IsFemale())
                {
                    data_girls.girls._sexuality sexuality = data_girls.girls._sexuality.straight;
                    if (mainScript.chance(6))
                    {
                        sexuality = data_girls.girls._sexuality.lesbian;
                    }
                    else if (mainScript.chance(5))
                    {
                        sexuality = data_girls.girls._sexuality.bi;
                    }
                    output.sexuality = sexuality;
                }
            }
            return output;
        }

        // Dating status is visible for underage members
        //
        // Postfix for data_girls.girls.GetPartnerString()
        // Postfix: Unfair_plus_v2_0.GameplayChanges.GetPartnerStringPostfix(text,this);
        public static string GetPartnerStringPostfix(string __return, data_girls.girls __this)
        {
            string output = __return;
            if (Unfair_plus_v2_1_1.BugFix.IsModEnabled("Unfair_plus"))
            {
                if (!__this.Is_AOC())
                {
                    string text = "";
                    if (!__this.DatingData.Is_Partner_Status_Known)
                    {
                        text += Language.Data["PROFILE__DATING_UNKNOWN"];
                    }
                    else if (__this.DatingData.Partner_Status_Known_To_Player == data_girls.girls._dating_data._partner_status.free)
                    {
                        text += Language.Data["PROFILE__DATING_NOT_DATING"];
                    }
                    else if (__this.DatingData.Partner_Status_Known_To_Player == data_girls.girls._dating_data._partner_status.taken_idol)
                    {
                        data_girls.girls girlfriend = __this.GetGirlfriend();
                        if (girlfriend != null)
                        {
                            text += Language.Insert("PROFILE__DATING_IDOL", new string[]
                            {
                        girlfriend.GetName(true)
                            });
                        }
                        else
                        {
                            text += Language.Data["PROFILE__DATING_IDOL_UNKNOWN"];
                        }
                    }
                    else if (__this.DatingData.Partner_Status_Known_To_Player == data_girls.girls._dating_data._partner_status.taken_outside_bf)
                    {
                        text += Language.Data["PROFILE__DATING_HAS_BF"];
                    }
                    else if (__this.DatingData.Partner_Status_Known_To_Player == data_girls.girls._dating_data._partner_status.taken_outside_gf)
                    {
                        text += Language.Data["PROFILE__DATING_HAS_GF"];
                    }
                    else if (__this.DatingData.Partner_Status_Known_To_Player == data_girls.girls._dating_data._partner_status.taken_player)
                    {
                        text += Language.Data["PROFILE__DATING_YOU"];
                    }
                    text += "\n";
                    if (__this.DatingData.Is_Sexuality_Known)
                    {
                        if (__this.sexuality == data_girls.girls._sexuality.straight)
                        {
                            text += Language.Data["PROFILE__DATING_STRAIGHT"];
                        }
                        else if (__this.sexuality == data_girls.girls._sexuality.lesbian)
                        {
                            text += Language.Data["PROFILE__DATING_LESBIAN"];
                        }
                        else
                        {
                            text += Language.Data["PROFILE__DATING_BI"];
                        }
                    }
                    else
                    {
                        text += Language.Data["PROFILE__DATING_PREF_UNKNOWN"];
                    }
                    if (__this.DatingData.Previous_Attempt != Date_Flirt._flirt._category.NONE && __this.DatingData.Partner_Status_Known_To_Player != data_girls.girls._dating_data._partner_status.taken_player)
                    {
                        text += "\n";
                        if (__this.DatingData.Is_Uninterested || (__this.DatingData.Is_Sexuality_Known && !Date_Flirt.IsCompatibleSexuality(__this)))
                        {
                            text += Language.Data["PROFILE__DATING_NOT_INTERESTED"];
                        }
                        else
                        {
                            text += Language.Data["PROFILE__DATING_INTERESTED"];
                        }
                    }
                    output = text;
                }
            }
            return output;
        }


        // Single PVs have success chances increased by 33%
        // 
        // Postfix for singles._param.GetSuccessChance(Single_Marketing_Roll._result Result, int Level, singles._single Single = null)
        // Postfix: Unfair_plus_v2_0.GameplayChanges.GetSuccessChancePostfix(Traits_only.GetSuccessChancePostfix(0f, this, Result, Single),this,Result,Single);
        public static float GetSuccessChancePostfix(float __return, singles._param __this, Single_Marketing_Roll._result Result, singles._single Single = null)
        {
            float output = __return;
            if (Unfair_plus_v2_1_1.BugFix.IsModEnabled("Unfair_plus"))
            {
                if (Single != null)
                {
                    float f = 0f;
                    if (__this.Special_Type == singles._param._special_type.lewd_pv)
                    {
                        float num = (Single.GetSenbatsuParamValue(data_girls._paramType.sexy) + Single.GetSenbatsuParamValue(data_girls._paramType.cute)) / 2f;
                        if (Result == Single_Marketing_Roll._result.success)
                        {
                            f = num * 9f / 10f;
                        }
                        else if (Result == Single_Marketing_Roll._result.success_crit)
                        {
                            f = num / 10f;
                        }
                        else if (Result == Single_Marketing_Roll._result.fail_crit)
                        {
                            f = (100f - num) / 10f;
                        }
                        else if (Result == Single_Marketing_Roll._result.fail)
                        {
                            f = 90f - num * 9f / 10f;
                        }
                    }
                    else if (__this.Special_Type == singles._param._special_type.edgy_pv)
                    {
                        float num2 = (Single.GetSenbatsuParamValue(data_girls._paramType.cool) + Single.GetSenbatsuParamValue(data_girls._paramType.funny)) / 2f;
                        if (Result == Single_Marketing_Roll._result.success)
                        {
                            f = num2 * 9f / 10f;
                        }
                        else if (Result == Single_Marketing_Roll._result.success_crit)
                        {
                            f = num2 / 10f;
                        }
                        else if (Result == Single_Marketing_Roll._result.fail_crit)
                        {
                            f = (100f - num2) / 10f;
                        }
                        else if (Result == Single_Marketing_Roll._result.fail)
                        {
                            f = 90f - num2 * 9f / 10f;
                        }
                    }
                    else if (__this.Special_Type == singles._param._special_type.artsy_pv)
                    {
                        float num3 = (Single.GetSenbatsuParamValue(data_girls._paramType.pretty) + Single.GetSenbatsuParamValue(data_girls._paramType.smart)) / 2f;
                        if (Result == Single_Marketing_Roll._result.success)
                        {
                            f = num3 * 9f / 10f;
                        }
                        else if (Result == Single_Marketing_Roll._result.success_crit)
                        {
                            f = num3 / 10f;
                        }
                        else if (Result == Single_Marketing_Roll._result.fail_crit)
                        {
                            f = (100f - num3) / 10f;
                        }
                        else if (Result == Single_Marketing_Roll._result.fail)
                        {
                            f = 90f - num3 * 9f / 10f;
                        }
                    }
                    output = f;
                }
            }
            return output;
        }

        // Fake Scandal crit success bonus increased significantly. Single PV crit success bonus decreased.
        //
        // Prefix for singles._param.GetSuccessModifier(Single_Marketing_Roll._result Result, bool Secondary, int Level)
        // Prefix: if(!Unfair_plus_v2_0.GameplayChanges.GetSuccessModifierPrefix(ref _return,this,Result,Secondary,Level)) return _return;
        public static bool GetSuccessModifierPrefix(ref float __return, singles._param __this, Single_Marketing_Roll._result Result, bool Secondary, int Level)
        {
            bool output = true;
            if (Unfair_plus_v2_1_1.BugFix.IsModEnabled("Unfair_plus"))
            {
                if (__this.Special_Type == singles._param._special_type.fake_scandal)
                {
                    if (!Secondary & Result == Single_Marketing_Roll._result.success_crit)
                    {
                        __return = 100f + (float)Level * 290f;
                    }
                }
                else if (__this.Special_Type == singles._param._special_type.edgy_pv || __this.Special_Type == singles._param._special_type.lewd_pv || __this.Special_Type == singles._param._special_type.artsy_pv)
                {
                    if (!Secondary)
                    {
                        if (Result == Single_Marketing_Roll._result.success_crit)
                        {
                            __return = 100f + (float)Level * 80f;
                        }
                    }
                    else
                    {
                        if (Result == Single_Marketing_Roll._result.success_crit)
                        {
                            __return = 50f + (float)Level * 30f;
                        }
                    }
                }
                output = false;
            }
            return output;
        }


        // When releasing a single, the penalty for a decrease in fame and appeal now only considers past singles of the same group
        // 
        // Replace singles.AddOpinion(singles._single single)
        // Replace: Unfair_plus_v2_0.GameplayChanges.AddOpinionReplace(single);
        public static void AddOpinionReplace(singles._single single)
        {
            if (Unfair_plus_v2_1_1.BugFix.IsModEnabled("Unfair_plus"))
            {
                single.ReleaseData.FanAppeal = single.GetAppealForOpinion();
                single.ReleaseData.FanAppeal.Sort(new Comparison<singles._fanAppeal>(CompareAppeal));
                List<singles._single> latestReleasedSingles = GetLatestReleasedSingles(3, single.GetGroup());
                if (latestReleasedSingles.Count == 0)
                {
                    foreach (data_girls.girls girls in single.girls)
                    {
                        if (girls != null)
                        {
                            girls.AddAppeal(single.FanAppeal[0].type, 1f);
                            girls.AddAppeal(single.FanAppeal[1].type, 1f);
                            girls.AddAppeal(single.FanAppeal[single.FanAppeal.Count - 1].type, -1f);
                            girls.AddAppeal(single.FanAppeal[single.FanAppeal.Count - 2].type, -1f);
                        }
                    }
                    return;
                }
                foreach (singles._fanAppeal fanAppeal in single.ReleaseData.FanAppeal)
                {
                    float avgOpinion = GetAvgOpinion(latestReleasedSingles, fanAppeal.type);
                    float val = 1f;
                    if (fanAppeal.ratio < avgOpinion)
                    {
                        val = -1f;
                    }
                    foreach (data_girls.girls girls2 in single.girls)
                    {
                        if (girls2 != null)
                        {
                            girls2.AddAppeal(fanAppeal.type, val);
                        }
                    }
                }
                return;
            }
            else
            {
                single.ReleaseData.FanAppeal = single.GetAppealForOpinion();
                single.ReleaseData.FanAppeal.Sort(new Comparison<singles._fanAppeal>(CompareAppeal));
                List<singles._single> latestReleasedSingles = singles.GetLatestReleasedSingles(3);
                if (latestReleasedSingles.Count == 0)
                {
                    foreach (data_girls.girls girls in single.girls)
                    {
                        if (girls != null)
                        {
                            girls.AddAppeal(single.FanAppeal[0].type, 1f);
                            girls.AddAppeal(single.FanAppeal[1].type, 1f);
                            girls.AddAppeal(single.FanAppeal[single.FanAppeal.Count - 1].type, -1f);
                            girls.AddAppeal(single.FanAppeal[single.FanAppeal.Count - 2].type, -1f);
                        }
                    }
                    return;
                }
                foreach (singles._fanAppeal fanAppeal in single.ReleaseData.FanAppeal)
                {
                    float avgOpinion = GetAvgOpinion(latestReleasedSingles, fanAppeal.type);
                    float val = 1f;
                    if (fanAppeal.ratio < avgOpinion)
                    {
                        val = -1f;
                    }
                    foreach (data_girls.girls girls2 in single.girls)
                    {
                        if (girls2 != null)
                        {
                            girls2.AddAppeal(fanAppeal.type, val);
                        }
                    }
                }
            }
            return;
        }

        // When releasing a single, the penalty for a decrease in fame and appeal now only considers past singles of the same group
        //
        // Infix for List<singles._single> latestReleasedSingles in singles.GenerateSales(singles._single single)
        // Infix after List<singles._single> latestReleasedSingles = singles.GetLatestReleasedSingles(3);
        // Infix: latestReleasedSingles = Unfair_plus_v2_0.GameplayChanges.GenerateSalesInfix(latestReleasedSingles,single);
        public static List<singles._single> GenerateSalesInfix(List<singles._single> latestReleasedSingles, singles._single single)
        {
            List<singles._single> output = latestReleasedSingles;

            if (Unfair_plus_v2_1_1.BugFix.IsModEnabled("Unfair_plus"))
            {
                output = GetLatestReleasedSingles(3, single.GetGroup());
            }

            return output;
        }

        // World tours give 3.5x more fans
        //
        // Postfix for SEvent_Tour.tour.GetNewFansByAttendance(int attendance)
        // Postfix: Unfair_plus_v2_0.GameplayChanges.GetNewFansByAttendancePostfix(Mathf.RoundToInt((float)attendance * num))
        public static int GetNewFansByAttendancePostfix(int __return)
        {
            int output = __return;
            if (Unfair_plus_v2_1_1.BugFix.IsModEnabled("Unfair_plus"))
            {
                output = Mathf.RoundToInt(output * 3.5f);
            }
            return output;
        }


        // World tours are limited to 100 stamina
        //
        // Prefix for SEvent_Tour.tour.SelectCountry(SEvent_Tour.country Country, int Level)
        // Prefix: if(!Unfair_plus_v2_0.GameplayChanges.SelectCountryPrefix(Country,this)) return;
        public static bool SelectCountryPrefix(SEvent_Tour.country Country, SEvent_Tour.tour __this)
        {
            if (Unfair_plus_v2_1_1.BugFix.IsModEnabled("Unfair_plus"))
            {
                SEvent_Tour.tour.selectedCountry country = __this.GetCountry(Country);
                if (country == null)
                {
                    int staminaCost = __this.Stamina + Country.GetStaminaCost();
                    if (staminaCost > 100)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        // World tours are limited to 100 stamina
        //
        // Infix for string text3 in Tour_Star.SetTooltip()
        // Infix before base.GetComponent<ButtonDefault>().SetTooltip(text);
        // Infix: text3 = Unfair_plus_v2_0.GameplayChanges.SetTooltipInfix(text3,this.TourCountry);
        public static string SetTooltipInfix(string text3, Tour_Country TourCountry)
        {
            string output = text3;
            if (Unfair_plus_v2_1_1.BugFix.IsModEnabled("Unfair_plus"))
            {
                SEvent_Tour.country country = TourCountry.Country;
                SEvent_Tour.tour tour = TourCountry.TourPopup.Tour;
                if (country.GetStaminaCost() + tour.Stamina > 100 && tour.GetCountry(country) == null)
                {
                    output = string.Concat(new object[]
                    {
                    "<color=",
                    mainScript.red,
                    ">",
                    Language.Data["TOUR__STAMINACAP"],
                    "</color>"
                    });
                }
            }
            return output;
        }

        // World tours are limited to 100 stamina
        //
        // Postfix for Tour_Country.OnClick()
        // Postfix: Unfair_plus_v2_0.GameplayChanges.OnClickPostfix(this);
        public static void OnClickPostfix(Tour_Country __this)
        {
            if (Unfair_plus_v2_1_1.BugFix.IsModEnabled("Unfair_plus"))
            {
                Tour_Country[] componentsInChildren = __this.TourPopup.CountriesContainer.transform.GetComponentsInChildren<Tour_Country>();
                for (int i = 0; i < componentsInChildren.Length; i++)
                {
                    componentsInChildren[i].UpdateData();
                }
            }
            return;
        }

        // Concert revenue formula rebalanced
        //
        // Postfix for SEvent_Concerts._concert._projectedValues.GetAttendanceOfDemo()
        // Postfix: Unfair_plus_v2_0.GameplayChanges.GetAttendanceOfDemoPostfix(num3 / 100f,this)
        public static float GetAttendanceOfDemoPostfix(float __return, SEvent_Concerts._concert._projectedValues __this)
        {
            float output = __return;

            if (Unfair_plus_v2_1_1.BugFix.IsModEnabled("Unfair_plus"))
            {
                int num = __this.TicketPrice;
                int num2 = 10000;
                float num3 = 4.99f;
                if (staticVars.IsHard())
                {
                    num *= 3;
                    num2 = 6000;
                    num3 = 7.85f;
                }
                float num4;
                if (num >= 3000 && num > num2)
                {
                    num4 = num3 * Mathf.Pow(1.0001f, -(float)num + (float)num2);
                    output = num4 / 100f;
                }
            }

            return output;
        }

        // Concert hype multiplier reduced for Club venue
        //
        // Postfix for SEvent_Concerts._concert.RecalcProjectedValues()
        // Postfix: Unfair_plus_v2_0.GameplayChanges.RecalcProjectedValuesPostfix(this);
        public static void RecalcProjectedValuesPostfix(SEvent_Concerts._concert __this)
        {
            if (Unfair_plus_v2_1_1.BugFix.IsModEnabled("Unfair_plus"))
            {
                if (__this.Venue == SEvent_Concerts._venue.club && __this.Hype > 100f)
                {
                    float num;
                    float num2 = __this.Hype - 100f;
                    LinearFunction._function function = new LinearFunction._function();
                    function.Init(0f, 50f, 100f, 25f);
                    float num3 = function.GetY(num2) / 100f;
                    num = num2 * num3 / 100f + 1f;
                    float num4 = 1f;
                    if (variables.Get("FUJI_3_TICKETS") == "true")
                    {
                        num4 = 1.05f;
                    }
                    __this.ProjectedValues.Actual_Revenue = (long)Mathf.Round((float)(__this.ProjectedValues.Actual_Audience * (long)__this.ProjectedValues.TicketPrice) * num * num4);
                }
            }

            return;
        }

        // You can only unlock the next concert venue if you've sold out the previous venue with a profit
        //
        // Prefix for SEvent_Concerts.StartConcert()
        // Prefix: if(!Unfair_plus_v2_0.GameplayChanges.StartConcertPrefix(this,this.popupManager)) return;
        public static bool StartConcertPrefix(SEvent_Concerts __this, PopupManager popupManager)
        {
            if (Unfair_plus_v2_1_1.BugFix.IsModEnabled("Unfair_plus"))
            {
                SpecialEvents_Manager.GetEvent(SpecialEvents_Manager._type.Concert).SetCooldown();
                SpecialEvents_Manager.GetEvent(SpecialEvents_Manager._type.Concert).Status = SpecialEvents_Manager._specialEvent._status.normal;
                __this.Concert.GenerateCards();
                Camera.main.GetComponent<mainScript>().Data.GetComponent<resources>().AddMoney(-__this.Concert.ProjectedValues.GetProductionCost());
                popupManager.Open(PopupManager._type.sevent_concert, true);
                __this.ConcertPopup.Reset();
                return false;
            }

            return true;
        }

        // You can only unlock the next concert venue if you've sold out the previous venue with a profit
        //
        // Postfix for SEvent_Concerts._concert.Finish()
        // Postfix: Unfair_plus_v2_0.GameplayChanges.FinishPostfix(this);
        public static void FinishPostfix(SEvent_Concerts._concert __this)
        {
            if (Unfair_plus_v2_1_1.BugFix.IsModEnabled("Unfair_plus"))
            {
                if (__this.ProjectedValues.Actual_Attendance >= 1f && __this.ProjectedValues.GetActualProfit() >= 0L)
                {
                    SEvent_Concerts.UpdateVenueUnlocked(__this);
                }
            }

            return;
        }



        // This method gets latest released singles of a given group. It is based on singles.GetReleasedSingles(int count)
        // which does not take group as a parameter.
        private static List<singles._single> GetLatestReleasedSingles(int count, Groups._group group = null)
        {
            if (group == null)
            {
                return singles.GetLatestReleasedSingles(count);
            }
            List<singles._single> latestReleasedSingles = new List<singles._single>();
            for (int i = singles.Singles.Count - 1; i >= 0; i--)
            {
                if (singles.Singles[i].status == singles._single._status.released && singles.Singles[i].GetGroup() == group)
                {
                    latestReleasedSingles.Add(singles.Singles[i]);
                    if (latestReleasedSingles.Count >= count)
                    {
                        break;
                    }
                }
            }
            return latestReleasedSingles;
        }

        // This method compares fan appeals in order to sort them. This is a duplicate of an internal method
        // in the class singles so that it can be referenced publicly.
        private static int CompareAppeal(singles._fanAppeal x, singles._fanAppeal y)
        {
            return y.ratio.CompareTo(x.ratio);
        }

        // private class singles.GetAvgOpinion(List<singles._single> __Singles, resources.fanType __Type)
        private static float GetAvgOpinion(List<singles._single> __Singles, resources.fanType __Type)
        {
            float num = 0f;
            foreach (singles._single single in __Singles)
            {
                num += single.ReleaseData.GetFanAppeal(__Type).ratio;
            }
            if (num != 0f)
            {
                num /= (float)__Singles.Count;
            }
            return num;
        }
    }
}
