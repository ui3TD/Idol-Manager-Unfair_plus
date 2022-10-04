using System;
using UnityEngine;

namespace Unfair_plus_v2_0
{
    public class Unfair_plus
    {

        //In hard mode, penalty for low salary satisfaction increased 10x
        //
        // Postfix of data_girls.girls.Graduation_Date_Update()
        // Postfix: Unfair_plus_v2_0.Unfair_plus.Graduation_Date_UpdatePostFix(this,true)
        public static bool Graduation_Date_UpdatePostFix(data_girls.girls __this, bool __return)
        {
            if (staticVars.Settings.IsModEnabled("Unfair_plus"))
            {
                int salarySatisfaction_Percentage = __this.GetSalarySatisfaction_Percentage();
                if (__this.status == data_girls._status.announced_graduation)
                {
                    return __return;
                }
                float num = 0;
                if (salarySatisfaction_Percentage < 20)
                {
                    if (staticVars.IsHard() && policies.GetSelectedPolicyValue(policies._type.salary).Value == policies._value.salary_manual)
                    {
                        num -= 27f;
                    }
                }
                else if (salarySatisfaction_Percentage < 50)
                {
                    if (staticVars.IsHard() && policies.GetSelectedPolicyValue(policies._type.salary).Value == policies._value.salary_manual)
                    {
                        num -= 9f;
                    }
                }
                __this.Graduation_Date.AddDays((double)Mathf.RoundToInt(num));
            }
            return __return;
        }

        // In hard mode, sister group new fans are reduced 5x
        //
        // Postfix of Groups.GetNewFansPerSingle()
        // Postfix: Unfair_plus_v2_0.Unfair_plus.GetNewFansPerSinglePostFix(Mathf.RoundToInt((float)num9 * num10))
        public static int GetNewFansPerSinglePostFix(int __return)
        {
            int output = __return;
            if (staticVars.Settings.IsModEnabled("Unfair_plus"))
            {
                if (staticVars.IsHard())
                {
                    output = Mathf.RoundToInt((float)output / 5);
                }
            }
            return output;
        }

        // In hard mode, player relationships with girls decay each week
        //
        // Prefix of data_girls.girls.UpdateRelationshipBasedOnSalary()
        // Prefix: if(!Unfair_plus_v2_0.Unfair_plus.UpdateRelationshipBasedOnSalaryPrefix(this)) return;
        public static bool UpdateRelationshipBasedOnSalaryPrefix(data_girls.girls __this)
        {
            if (staticVars.Settings.IsModEnabled("Unfair_plus"))
            {
                if (staticVars.IsHard())
                {
                    int salarySatisfaction_Percentage = __this.GetSalarySatisfaction_Percentage();
                    if (salarySatisfaction_Percentage >= 150)
                    {
                        __this.Rel_Influence_Points++;
                    }
                    else if (salarySatisfaction_Percentage <= 50)
                    {
                        __this.Rel_Influence_Points -= 2;
                    }
                    else if (salarySatisfaction_Percentage <= 100 && __this.Rel_Romance_Points >= 1)
                    {
                        __this.Rel_Influence_Points--;
                    }
                    if (__this.Rel_Romance_Points >= 1)
                    {
                        __this.Rel_Romance_Points--;
                    }
                    if (__this.Rel_Friendship_Points >= 2)
                    {
                        __this.Rel_Friendship_Points -= 2;
                    }
                }
            }
            return true;
        }

        // In hard mode, there is significant customer churn rate
        // 
        // Prefix for resources.OnNewDay()
        // Prefix: 
        public static bool OnNewDayPrefix(resources __this)
        {
            if (staticVars.Settings.IsModEnabled("Unfair_plus"))
            {
                __this.AddMoney((long)__this.Money_DailyProfit());
                resources.Add(resources.type.buzz, (long)(__this.Buzz_Daily() - __this.GetDailyBuzzReduction()));
                resources.Add(resources.type.fame, (long)__this.Fame_Daily());
                DailyFanChurn();
                return false;
            }
            return true;
        }

        // In hard mode, there is significant customer churn rate
        //
        // Postfix for tooltip_fans.RenderFanChange()
        // Postfix: Unfair_plus_v2_0.Unfair_plus.RenderFanChangePostfix(this);
        public static void RenderFanChangePostfix(tooltip_fans __this)
        {
            if (staticVars.Settings.IsModEnabled("Unfair_plus"))
            {
                if (staticVars.IsHard())
                {
                    double fans = (double)resources.GetFansTotal(null);
                    long churn = resources.FansChange;
                    if ((long)fans + churn <= 0L || churn > 0L)
                    {
                        churn = -(long)fans;
                    }
                    ExtensionMethods.SetText(__this.fan_change, string.Concat(new string[]
                    {
                    Language.Data["CHURN"],
                    ": ",
                    ExtensionMethods.color(ExtensionMethods.formatNumber(churn, false, false), mainScript.red),
                    ExtensionMethods.color(" /d", mainScript.red)
                    }));
                }
            }
            return;
        }

        // In hard mode, audience of TV and Radio shows decrease with fatigue
        //
        // Infix for float num2 in Shows._show.SetSales()
        // Infix after num2 = (num2 + (float)this.GetAllNewFans()) / 12f;
        // Infix: num2 = Unfair_plus_v2_0.Unfair_plus.SetSalesInfix(num2, this);
        public static float SetSalesInfix(float num2, Shows._show __this)
        {
            float output = num2;
            if (staticVars.Settings.IsModEnabled("Unfair_plus"))
            {
                float num = (float)(resources.GetFameLevel() + __this.fame[__this.fame.Count - 1]);
                if (num < 1f)
                {
                    num = 1f;
                }
                output = num * 0.5f * 0.1f * (float)GetBaseAudience(__this);
                if (staticVars.IsHard() && __this.medium.media_type != Shows._param._media_type.internet)
                {
                    output = (output + (float)GetAllNewFans(__this) * (1f - __this.GetFatigue(null) * __this.GetFatigue(null) / 10000f)) / 12f;
                }
                else
                {
                    output = (output + (float)GetAllNewFans(__this)) / 12f;
                }
            }
            return output;
        }

        // In hard mode, idols at 10 fame will expect at least 10% of their earnings as salary
        //
        // Postfix for data_girls.girls.GetExpectedSalary_Total()
        // Postfix: Unfair_plus_v2_0.Unfair_plus.GetExpectedSalary_TotalPostfix(num * 2L,this)
        public static long GetExpectedSalary_TotalPostfix(long __return, data_girls.girls __this)
        {
            long output = __return;
            if (staticVars.Settings.IsModEnabled("Unfair_plus"))
            {
                if (staticVars.IsHard())
                {
                    int num = __this.GetExpectedSalary();
                    float num2 = __this.GetAverageEarnings();
                    if (__this.GetFameLevel() == 10 && num2 / 10 > num)
                    {
                        output = (long)Mathf.Round(num2 / 10);
                    }
                }
            }
            return output;
        }


        // Coliseum level concert venues in hard mode increased to 50,000 capacity
        //
        // Prefix for SEvent_Concerts.GetVenueCapacity(SEvent_Concerts._venue val)
        // Prefix: if (!Unfair_plus_v2_0.Unfair_plus.GetVenueCapacityPrefix(val, ref _return)) return _return;
        public static bool GetVenueCapacityPrefix(SEvent_Concerts._venue val, ref int __return)
        {
            if (staticVars.Settings.IsModEnabled("Unfair_plus"))
            {
                if (staticVars.IsHard() && val == SEvent_Concerts._venue.tokyoColiseum)
                {
                    __return = 50000;
                    return false;
                }
            }

            return true;
        }

        // Coliseum level concert venues in hard mode have a base cost increased to 200,000,000
        //
        // Prefix for SEvent_Concerts.GetVenueBaseCost(SEvent_Concerts._venue val)
        // Prefix: if (!Unfair_plus_v2_0.Unfair_plus.GetVenueBaseCostPrefix(val, ref _return)) return _return;
        public static bool GetVenueBaseCostPrefix(SEvent_Concerts._venue val, ref int __return)
        {
            if (staticVars.Settings.IsModEnabled("Unfair_plus"))
            {
                if (staticVars.IsHard() && val == SEvent_Concerts._venue.tokyoColiseum)
                {
                    __return = 200000000;
                    return false;
                }
            }

            return true;
        }

        // Removes fans daily if on hard mode
        private static void DailyFanChurn()
        {
            if (staticVars.IsHard())
            {
                resources.Add(resources.type.fans, resources.FansChange);
                long fansTotal = resources.GetFansTotal(null);
                float churn = (float)Math.Pow((double)fansTotal, 0.83) * 0.012f + 2f;
                if (churn > (float)fansTotal)
                {
                    churn = (float)fansTotal;
                }
                if (churn > 0f)
                {
                    resources.FansChange = -(long)Mathf.CeilToInt(churn);
                }
                else
                {
                    resources.FansChange = 0L;
                }
            }
        }


        // Copy of private method Shows._show.GetBaseAudience()
        private static int GetBaseAudience(Shows._show __this)
        {
            Shows._param._media_type? media_type = __this.medium.media_type;
            if (media_type != null)
            {
                Shows._param._media_type valueOrDefault = media_type.GetValueOrDefault();
                if (valueOrDefault <= Shows._param._media_type.radio)
                {
                    return 100000;
                }
                if (valueOrDefault == Shows._param._media_type.tv)
                {
                    return 500000;
                }
            }
            return 1;
        }

        // Copy of private method Shows._show.GetAllNewFans()
        private static int GetAllNewFans(Shows._show __this)
        {
            int num = 0;
            foreach (int num2 in __this.fans)
            {
                num += num2;
            }
            return num;
        }



    }
}