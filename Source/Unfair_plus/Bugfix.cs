using System;
using UnityEngine;

namespace Unfair_plus_v2_0
{
    public class Bugfix
    {
        // Fixed Theater so that it uses stamina.
        //
        // Postfix for Theaters.GetStaminaCost(Theaters._theater._schedule._type Type)
        // Postfix: Unfair_plus_v2_0.Bugfix.GetStaminaCostPostfix(Type, 0f)
        public static float GetStaminaCostPostfix(Theaters._theater._schedule._type Type, float __return)
        {
            float output = __return;
            if (Unfair_plus_v2_1_1.BugFix.IsModEnabled("Unfair_plus"))
            {
                float num = 0f;
                if (Type == Theaters._theater._schedule._type.performance)
                {
                    num = 5f;
                }
                else if (Type == Theaters._theater._schedule._type.manzai)
                {
                    num = 2f;
                }
                if (staticVars.IsHard())
                {
                    num *= 2f;
                }
                output = num;
            }
            return output;
        }

        // Fixed Theater so that revenue stats are not offset by one day
        //
        // Prefix for Theaters.CompleteDay()
        // Prefix: if(!Unfair_plus_v2_0.Bugfix.CompleteDayPrefix()) return;
        public static bool CompleteDayPrefix()
        {
            if (Unfair_plus_v2_1_1.BugFix.IsModEnabled("Unfair_plus"))
            {
                foreach (Theaters._theater theater in Theaters.Theaters_)
                {
                    // Fix so that auto schedules contribute revenue on the day of
                    theater.Doing_Now = theater.GetSchedule().Type;
                }
            }
            return true;
        }

        // Fixed Theater so that average stats ignore days off, and so that girls earnings are increased by revenue
        //
        // Postfix for Theaters.CompleteDay()
        // Postfix: Unfair_plus_v2_0.Bugfix.CompleteDayPostfix();
        public static void CompleteDayPostfix()
        {
            if (Unfair_plus_v2_1_1.BugFix.IsModEnabled("Unfair_plus"))
            {
                foreach (Theaters._theater theater in Theaters.Theaters_)
                {
                    // Fix so that auto schedules contribute revenue on the day of
                    if (theater.GetSchedule().Type == Theaters._theater._schedule._type.auto &&
                        (theater.Doing_Now == Theaters._theater._schedule._type.manzai || theater.Doing_Now == Theaters._theater._schedule._type.performance))
                    {
                        long rev = theater.GetTicketSales();
                        if (rev > 0) resources.Add(resources.type.money, rev);
                        if (staticVars.dateTime.Day != 1)
                        {
                            theater.GetRoom().addFloat(Floats.type.icon_money, "", true, null, 0f, 1f, 0f, null);
                        }
                    }

                    // Fix so that days off have no revenue
                    if (theater.Doing_Now == Theaters._theater._schedule._type.day_off)
                    {
                        theater.Stats[theater.Stats.Count - 1].Revenue = 0;
                    }

                    if (theater.Doing_Now == Theaters._theater._schedule._type.performance || theater.Doing_Now == Theaters._theater._schedule._type.manzai)
                    {
                        int num2 = theater.GetGroup().GetGirls(true, false, null).Count;
                        long num = theater.GetTicketSales();
                        if (theater.AreSubsUnlocked() && staticVars.dateTime.Day == 1)
                        {
                            num += theater.GetSubRevenue();
                        }
                        foreach (data_girls.girls girls2 in theater.GetGroup().GetGirls(true, false, null))
                        {
                            if (num > 0L && num2 > 0)
                            {
                                girls2.Earn(num / (long)num2);
                            }
                        }
                    }
                }
            }
        }

        // Fixed Theater so that average stats ignore days off
        //
        // Postfix for Theaters._theater.GetAvgAttendance()
        // Postfix: Unfair_plus_v2_0.Bugfix.GetAvgAttendancePostfix(Mathf.RoundToInt(num),this)
        public static int GetAvgAttendancePostfix(int __return, Theaters._theater __this)
        {
            int output = __return;
            if (Unfair_plus_v2_1_1.BugFix.IsModEnabled("Unfair_plus"))
            {
                float num = 0f;
                float num2 = 7f;
                if (__this.Stats.Count == 0)
                {
                    return 0;
                }
                if (__this.Stats.Count < 7)
                {
                    num2 = (float)__this.Stats.Count;
                }
                int num3 = __this.Stats.Count - 1;
                int num4 = 0;
                while ((float)num3 >= (float)__this.Stats.Count - num2)
                {
                    if (__this.Stats[num3].Schedule.Type != Theaters._theater._schedule._type.day_off)
                    {
                        num += (float)__this.Stats[num3].Attendance;
                        num4++;
                    }
                    num3--;
                }
                if (num4 != 0)
                {
                    num /= (float)num4;
                }
                output = Mathf.RoundToInt(num);
            }
            return output;
        }

        // Fixed Theater so that average stats ignore days off
        //
        // Postfix for Theaters._theater.GetAvgRevenue()
        // Postfix: Unfair_plus_v2_0.Bugfix.GetAvgRevenuePostfix(Mathf.RoundToInt(num),this)
        public static int GetAvgRevenuePostfix(int __return, Theaters._theater __this)
        {
            int output = __return;
            if (Unfair_plus_v2_1_1.BugFix.IsModEnabled("Unfair_plus"))
            {
                float num = 0f;
                float num2 = 7f;
                if (__this.Stats.Count == 0)
                {
                    return 0;
                }
                if (__this.Stats.Count < 7)
                {
                    num2 = (float)__this.Stats.Count;
                }
                int num3 = __this.Stats.Count - 1;
                int num4 = 0;
                while ((float)num3 >= (float)__this.Stats.Count - num2)
                {
                    if (__this.Stats[num3].Schedule.Type != Theaters._theater._schedule._type.day_off)
                    {
                        num += (float)__this.Stats[num3].Revenue;
                        num4++;
                    }
                    num3--;
                }
                if (num4 != 0)
                {
                    num /= (float)num4;
                }
                output = Mathf.RoundToInt(num);
            }
            return output;
        }

        // Fixed Theater so that money tooltip includes 7 days instead of 6, and include sub revenue
        //
        // Postfix for Theaters.GetLastWeekEarning()
        // Postfix: Unfair_plus_v2_0.Bugfix.TheatersGetLastWeekEarningPostfix(num)
        public static long TheatersGetLastWeekEarningPostfix(long __return)
        {
            long output = __return;
            if (Unfair_plus_v2_1_1.BugFix.IsModEnabled("Unfair_plus"))
            {
                foreach (Theaters._theater theater in Theaters.Theaters_)
                {
                    if (theater.Stats.Count >= 7)
                    {
                        output += theater.Stats[theater.Stats.Count - 7].Revenue;
                    }
                    if (theater.AreSubsUnlocked())
                    {
                        output += theater.GetSubRevenue();
                    }
                }
            }
            return output;
        }

        // Fixed Cafe so that money tooltip includes 7 days instead of 6
        //
        // Postfix for Cafes.GetLastWeekEarning()
        // Postfix: Unfair_plus_v2_0.Bugfix.CafesGetLastWeekEarningPostfix(num)
        public static int CafesGetLastWeekEarningPostfix(int __return)
        {
            int output = __return;
            if (Unfair_plus_v2_1_1.BugFix.IsModEnabled("Unfair_plus"))
            {
                foreach (Cafes._cafe cafe in Cafes.Cafes_)
                {
                    if (cafe.Stats.Count >= 7)
                    {
                        output += cafe.Stats[cafe.Stats.Count - 7].Profit;
                    }
                }
            }
            return output;
        }

        // Fixed so that when girls dating within the group break up, their relationship status is no longer known
        //
        // Postfix for Relationships._relationship.Breakup()
        // Postfix: Unfair_plus_v2_0.Bugfix.BreakupPostfix(this);
        public static void BreakupPostfix(Relationships._relationship __this)
        {
            if (Unfair_plus_v2_1_1.BugFix.IsModEnabled("Unfair_plus"))
            {
                if (__this.Dating)
                {
                    __this.Girls[0].DatingData.Is_Partner_Status_Known = false;
                    __this.Girls[1].DatingData.Is_Partner_Status_Known = false;
                }
            }

            return;
        }

        // Fixed Concert revenue formula so that it shows accurate estimated values
        //
        // Postfix for SEvent_Concerts._concert._projectedValues.GetRevenue()
        // Postfix: Unfair_plus_v2_0.Bugfix.GetRevenuePostfix((long)Mathf.Round((float)this.GetNumberOfSoldTickets() * (float)this.TicketPrice * this.GetHype()),this)
        public static long GetRevenuePostfix(long __return, SEvent_Concerts._concert._projectedValues __this)
        {
            long output = __return;

            if (Unfair_plus_v2_1_1.BugFix.IsModEnabled("Unfair_plus"))
            {
                float hype = __this.GetHype() * 100f;
                if (hype > 100f)
                {
                    float num;
                    float num2 = hype - 100f;
                    LinearFunction._function function = new LinearFunction._function();
                    function.Init(0f, 50f, 100f, 25f);
                    float num3 = function.GetY(num2) / 100f;
                    num = num2 * num3 + 100f;
                    output = (long)Mathf.Round(output / hype * num);
                }
            }

            return output;
        }

        // Fixed Concert revenue formula so that it shows accurate estimated values
        //
        // Infix for int num in SEvents_Concerts._concert._projectedValues.GetString(float _val)
        // Infix after int num = ExtensionMethods.toPercent(_val);
        // Infix: num = Unfair_plus_v2_0.Bugfix.GetStringInfix(num,_val);
        public static int GetStringInfix(int num, float _val)
        {
            int output = num;

            if (Unfair_plus_v2_1_1.BugFix.IsModEnabled("Unfair_plus"))
            {
                output = Mathf.FloorToInt(_val * 100f);
            }

            return output;
        }


        // Fixed senbatsu stats calculation so it doesn't punish you if you don't have enough idols to fill all rows
        //
        // Infix for float num3 in singles._single.SenbatsuCalcParam(List<data_girls.girls> _girls, data_girls._paramType type, Groups._group Group = null)
        // Infix after float num3 = 100f / (float)num2;
        // Infix: num3 = Unfair_plus_v2_0.Bugfix.SenbatsuCalcParamInfix(num3,Group,this);
        public static float SenbatsuCalcParamInfix(float num3, Groups._group Group, singles._single __this)
        {
            float output = num3;
            if (Unfair_plus_v2_1_1.BugFix.IsModEnabled("Unfair_plus"))
            {
                if (Group == null)
                {
                    Group = __this.GetGroup();
                }
                int num = Group.GetNumberOfNonGraduatedGirls();
                float num2 = 5;
                if (num == 0)
                {
                    num2 = 0;
                }
                else if (num == 1)
                {
                    num2 = 1;
                }
                else if (num <= 3)
                {
                    num2 = 1 + (num - 1) / 2;
                }
                else if (num <= 6)
                {
                    num2 = 2 + (num - 3) / 3;
                }
                else if (num <= 10)
                {
                    num2 = 3 + (num - 6) / 4;
                }
                else if (num <= 15)
                {
                    num2 = 3 + (num - 10) / 5;
                }
                output = 100f / num2;
            }
            return output;
        }

    }
}