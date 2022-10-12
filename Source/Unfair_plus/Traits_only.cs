using System.Collections.Generic;
using UnityEngine;

namespace Unfair_plus_v2_0
{
    public class Traits_only
    {
        // Girls with Live Fast trait have double the rate of stat decreases after their peak age
        //
        // Infix for int num in data_girls.girls.AgeDeterioration()
        // Infix after int num = this.GetAge() - this.peakAge;
        // Infix: num = Unfair_plus_v2_0.Traits_only.AgeDeteriorationInfix(num,this);
        public static int AgeDeteriorationInfix(int num, data_girls.girls __this)
        {
            int output = num;
            if (Unfair_plus_v2_1_1.BugFix.IsModEnabled("Unfair_plus") || Unfair_plus_v2_1_1.BugFix.IsModEnabled("Traits_only"))
            {
                if (__this.trait == traits._trait._type.Live_fast)
                {
                    output *= 2;
                }
            }
            return output;
        }

        // Girls with Live Fast trait have double the rate of stat decreases after their peak age
        //
        // Infix for float num3 in Birthday_Popup.DoParam(data_girls._paramType Prm, float Delay)
        // Infix after num3 = num * (0.975f - 0.0025f * (float)num2);
        // Infix: num3 = Unfair_plus_v2_0.Traits_only.DoParamInfix(num3,Prm,this);
        public static float DoParamInfix(float num3, data_girls._paramType Prm, Birthday_Popup __this)
        {
            float output = num3;

            if (Unfair_plus_v2_1_1.BugFix.IsModEnabled("Unfair_plus") || Unfair_plus_v2_1_1.BugFix.IsModEnabled("Traits_only"))
            {
                if (__this.Girl.trait == traits._trait._type.Live_fast)
                {
                    float num = __this.Girl.getParam(Prm).val;
                    int age = __this.Girl.GetAge();
                    int num2 = age - __this.Girl.peakAge;
                    if (__this.Girl.trait == traits._trait._type.Late_bloomer && num2 >= 0)
                    {
                        num2 = -1;
                    }
                    output = num * (0.95f - 0.005f * (float)num2);
                }
            }

            return output;
        }

        // Girls with Trendy trait have 1.5x the appeal to non-adults and 0.5x the appeal to adults.
        //
        // Infix for float num in data_girls.girls.RecalcFanAppeal()
        // Infix before fanAppeal.SetRatio(num);
        // Infix: num = Unfair_plus_v2_0.Traits_only.RecalcFanAppealInfix(num,fanAppeal,this);
        public static float RecalcFanAppealInfix(float num, singles._fanAppeal __fanAppeal, data_girls.girls __this)
        {
            float output = num;
            if (Unfair_plus_v2_1_1.BugFix.IsModEnabled("Unfair_plus") || Unfair_plus_v2_1_1.BugFix.IsModEnabled("Traits_only"))
            {
                if (__this.trait == traits._trait._type.Trendy)
                {
                    if (__fanAppeal.type == resources.fanType.adult)
                    {
                        output *= 0.5f;
                    }
                    else
                    {
                        output *= 1.5f;
                    }
                }
            }
            return output;
        }

        // If there is an Indiscreet member, girls in dating relationships unknown to the player have a 2% chance
        // of having the relationship revealed each week
        //
        // Postfix for data_girls.girls.UpdateDatingStatus()
        // Postfix: Unfair_plus_v2_0.Traits_only.UpdateDatingStatusPostfix(this);
        public static void UpdateDatingStatusPostfix(data_girls.girls __this)
        {
            if (Unfair_plus_v2_1_1.BugFix.IsModEnabled("Unfair_plus") || Unfair_plus_v2_1_1.BugFix.IsModEnabled("Traits_only"))
            {
                if (mainScript.chance(2) && __this.DatingData.Is_Taken() && !__this.DatingData.Is_Partner_Status_Known)
                {
                    bool leaker = false;
                    foreach (data_girls.girls girls in data_girls.GetActiveGirls(null))
                    {
                        if (girls != __this && girls.trait == traits._trait._type.Indiscreet)
                        {
                            leaker = true;
                        }
                    }
                    if (leaker)
                    {
                        if (policies.GetSelectedPolicyValue(policies._type.dating).Value == policies._value.dating_forbidden)
                        {
                            NotificationManager.AddNotification(Language.Insert("IDOL__OUTSIDE_LEAK_SCANDAL", new string[]
                            {
                            __this.GetName(true)
                            }), mainScript.red32, NotificationManager._notification._type.idol_relationship_change);
                            __this.getParam(data_girls._paramType.scandalPoints).add(1f, false);
                        }
                        else
                        {
                            NotificationManager.AddNotification(Language.Insert("IDOL__OUTSIDE_LEAK", new string[]
                            {
                            __this.GetName(true)
                            }), mainScript.red32, NotificationManager._notification._type.idol_relationship_change);
                        }
                        __this.getParam(data_girls._paramType.mentalStamina).add(-30f, false);
                        __this.DatingData.Is_Partner_Status_Known = true;
                        __this.DatingData.Partner_Status_Known_To_Player = __this.DatingData.Partner_Status;
                    }
                }
            }
            return;
        }

        // If there is an Indiscreet member, girls in dating relationships unknown to the player have a 2% chance
        // of having the relationship revealed each week
        //
        // Postfix for Relationships._relationship.CheckDating()
        // Postfix: Unfair_plus_v2_0.Traits_only.CheckDatingPostfix(true,this)
        public static bool CheckDatingPostfix(bool __return, Relationships._relationship __this)
        {
            bool output = __return;

            if (Unfair_plus_v2_1_1.BugFix.IsModEnabled("Unfair_plus") || Unfair_plus_v2_1_1.BugFix.IsModEnabled("Traits_only"))
            {
                if (mainScript.chance(2) && __this.Dating && !__this.IsRelationshipKnown())
                {
                    bool leaker = false;
                    foreach (data_girls.girls girls in data_girls.GetActiveGirls(null))
                    {
                        if (girls != __this.Girls[0] && girls != __this.Girls[1] && girls.trait == traits._trait._type.Indiscreet)
                        {
                            leaker = true;
                        }
                    }
                    if (leaker)
                    {
                        if (policies.GetSelectedPolicyValue(policies._type.dating).Value == policies._value.dating_forbidden)
                        {
                            __this.Girls[0].getParam(data_girls._paramType.scandalPoints).add(1f, false);
                            __this.Girls[1].getParam(data_girls._paramType.scandalPoints).add(1f, false);
                            NotificationManager.AddNotification(Language.Insert("IDOL__INSIDE_LEAK_SCANDAL", new string[]
                            {
                            __this.Girls[0].GetName(true),
                            __this.Girls[1].GetName(true)
                            }), mainScript.red32, NotificationManager._notification._type.idol_relationship_change);
                        }
                        else
                        {
                            NotificationManager.AddNotification(Language.Insert("IDOL__INSIDE_LEAK", new string[]
                            {
                            __this.Girls[0].GetName(true),
                            __this.Girls[1].GetName(true)
                            }), mainScript.red32, NotificationManager._notification._type.idol_relationship_change);
                        }
                        __this.Girls[0].getParam(data_girls._paramType.mentalStamina).add(-30f, false);
                        __this.Girls[1].getParam(data_girls._paramType.mentalStamina).add(-30f, false);
                        __this.Girls[0].DatingData.Is_Partner_Status_Known = true;
                        __this.Girls[1].DatingData.Is_Partner_Status_Known = true;
                        __this.Girls[0].DatingData.Partner_Status_Known_To_Player = data_girls.girls._dating_data._partner_status.taken_idol;
                        __this.Girls[1].DatingData.Partner_Status_Known_To_Player = data_girls.girls._dating_data._partner_status.taken_idol;
                    }
                }
            }
            return output;
        }

        // Girls with Photogenic trait have +100% to photoshoots
        //
        // Postfix for business._proposal.GetGirlCoeff(data_girls.girls _girl)
        // Postfix: Unfair_plus_v2_0.Traits_only.GetGirlCoeffPostfix(_girl,num,this)
        public static float GetGirlCoeffPostfix(data_girls.girls _girl, float __return, business._proposal __this)
        {
            float output = __return;
            if (Unfair_plus_v2_1_1.BugFix.IsModEnabled("Unfair_plus") || Unfair_plus_v2_1_1.BugFix.IsModEnabled("Traits_only"))
            {
                if (__this.type == business._type.photoshoot && _girl.trait == traits._trait._type.Photogenic)
                {
                    output += 1f;
                    if (output > 20f)
                    {
                        output = 20f;
                    }
                }
            }
            return output;
        }

        // Stat changes for businesses 
        //
        // Infix for float val in business._proposal.GetGirlCoeff(data_girls.girls _girl)
        // Infix after float val = _girl.getParam(this.skill).val;
        // Infix: val = Unfair_plus_v2_0.Traits_only.GetGirlCoeffInfix(val,_girl,this);
        public static float GetGirlCoeffInfix(float val, data_girls.girls _girl, business._proposal __this)
        {
            float output = val;

            if (Unfair_plus_v2_1_1.BugFix.IsModEnabled("Unfair_plus") || Unfair_plus_v2_1_1.BugFix.IsModEnabled("Traits_only"))
            {
                output = _girl.getParam(__this.skill).val + GetTraitModifier(_girl, __this.skill);
                if (output < 0f)
                {
                    output = 0f;
                }
            }

            return output;
        }

        // Stat changes for shows
        //
        // Infix for float num in data_girls.GetAverageParam(data_girls._paramType Type, List<data_girls.girls> Girls)
        // Infix after num += girls.getParam(Type).val;
        // Infix: num = Unfair_plus_v2_0.Traits_only.GetAverageParamInfix(num,Type,Girls,girls);
        public static float GetAverageParamInfix(float num, data_girls._paramType Type, List<data_girls.girls> Girls, data_girls.girls __girls)
        {
            float output = num;
            if (Unfair_plus_v2_1_1.BugFix.IsModEnabled("Unfair_plus") || Unfair_plus_v2_1_1.BugFix.IsModEnabled("Traits_only"))
            {
                output += GetTraitModifier(__girls, Type, Girls);
                if (output < 0)
                {
                    output = 0;
                }
            }
            return output;
        }

        // Stat changes for singles
        //
        // Infix for float num5 in singles._single.SenbatsuCalcParam(List<data_girls.girls> _girls, data_girls._paramType type, Groups._group Group = null)
        // Infix after num5 += girls.getParam(type).val;
        // Infix: num5 = Unfair_plus_v2_0.Traits_only.SenbatsuCalcParamInfix(num5,type,girls);
        public static float SenbatsuCalcParamInfix(float num5, data_girls._paramType type, data_girls.girls girls)
        {
            float output = num5;
            if (Unfair_plus_v2_1_1.BugFix.IsModEnabled("Unfair_plus") || Unfair_plus_v2_1_1.BugFix.IsModEnabled("Traits_only"))
            {
                output += GetTraitModifier(girls, type);
                if (output < 0)
                {
                    output = 0;
                }
            }
            return output;
        }

        // Stat changes for concert songs
        //
        // Postfix for SEvent_Concerts._concert._song.GetSkillValue()
        // Postfix: Unfair_plus_v2_0.Traits_only.SongGetSkillValuePostfix(num,this)
        public static int SongGetSkillValuePostfix(int __return, SEvent_Concerts._concert._song __this)
        {
            int output = __return;

            if (Unfair_plus_v2_1_1.BugFix.IsModEnabled("Unfair_plus") || Unfair_plus_v2_1_1.BugFix.IsModEnabled("Traits_only"))
            {
                if (__this.Center != null)
                {
                    output += Mathf.FloorToInt((GetTraitModifier(__this.Center, data_girls._paramType.dance) + GetTraitModifier(__this.Center, data_girls._paramType.vocal)) / 2f);
                    if (__this.Center == __this.Single.GetCenter() && output < 80)
                    {
                        output = 80;
                    }
                    if (output < 0)
                    {
                        output = 0;
                    }
                }

            }

            return output;
        }

        // Stat changes for concert MCs
        //
        // Postfix for SEvent_Concerts._concert._mc.GetSkillValue()
        // Postfix: Unfair_plus_v2_0.Traits_only.McGetSkillValuePostfix(Mathf.FloorToInt(num),this)
        public static int McGetSkillValuePostfix(int __return, SEvent_Concerts._concert._mc __this)
        {
            int output = __return;

            if (Unfair_plus_v2_1_1.BugFix.IsModEnabled("Unfair_plus") || Unfair_plus_v2_1_1.BugFix.IsModEnabled("Traits_only"))
            {
                foreach (data_girls.girls girls in __this.Girls)
                {
                    if (girls != null)
                    {
                        output += Mathf.FloorToInt((GetTraitModifier(girls, data_girls._paramType.smart) + GetTraitModifier(girls, data_girls._paramType.funny)) / 2f);
                    }
                }
                if (output < 0)
                {
                    output = 0;
                }

            }

            return output;
        }

        // Maternal and Precocious default relationship is positive based on age criteria
        //
        // Postfix for Relationships._relationship.Initialize()
        // Postfix: Unfair_plus_v2_0.Traits_only.InitializePostfix(this);
        public static void InitializePostfix(Relationships._relationship __this)
        {
            if (Unfair_plus_v2_1_1.BugFix.IsModEnabled("Unfair_plus") || Unfair_plus_v2_1_1.BugFix.IsModEnabled("Traits_only"))
            {
                int age = __this.Girls[0].GetAge();
                int age2 = __this.Girls[1].GetAge();
                if (__this.Girls[0].trait == traits._trait._type.Maternal && age > age2)
                {
                    __this.Dynamic = Relationships._relationship._dynamic.positive;
                }
                else if (__this.Girls[1].trait == traits._trait._type.Maternal && age2 > age)
                {
                    __this.Dynamic = Relationships._relationship._dynamic.positive;
                }
                else if (__this.Girls[0].trait == traits._trait._type.Precocious && age < age2)
                {
                    __this.Dynamic = Relationships._relationship._dynamic.positive;
                }
                else if (__this.Girls[1].trait == traits._trait._type.Precocious && age2 < age)
                {
                    __this.Dynamic = Relationships._relationship._dynamic.positive;
                }
            }
            return;
        }

        // Maternal, Precocious and Arrogant relationship evolution
        //
        // Postfix for Relationships.Do_Dynamic()
        // Postfix: Unfair_plus_v2_0.Traits_only.Do_DynamicPostfix();
        public static void Do_DynamicPostfix()
        {
            if (Unfair_plus_v2_1_1.BugFix.IsModEnabled("Unfair_plus") || Unfair_plus_v2_1_1.BugFix.IsModEnabled("Traits_only"))
            {
                foreach (Relationships._relationship relationship in Relationships.RelationshipsData)
                {
                    if (relationship.Dynamic == Relationships._relationship._dynamic.positive)
                    {
                        if (relationship.Girls[0].trait == traits._trait._type.Maternal && relationship.Girls[0].GetAge() > relationship.Girls[1].GetAge())
                        {
                            relationship.Add(0.3f);
                        }
                        else if (relationship.Girls[1].trait == traits._trait._type.Maternal && relationship.Girls[1].GetAge() > relationship.Girls[0].GetAge())
                        {
                            relationship.Add(0.3f);
                        }
                        if (relationship.Girls[0].trait == traits._trait._type.Precocious && relationship.Girls[0].GetAge() < relationship.Girls[1].GetAge())
                        {
                            relationship.Add(0.3f);
                        }
                        else if (relationship.Girls[1].trait == traits._trait._type.Precocious && relationship.Girls[1].GetAge() < relationship.Girls[0].GetAge())
                        {
                            relationship.Add(0.3f);
                        }
                    }
                    singles._single single = singles.GetLatestReleasedSingle(false, relationship.Girls[0].GetGroup());
                    singles._single single2 = singles.GetLatestReleasedSingle(false, relationship.Girls[1].GetGroup());
                    singles._single single3 = singles.GetLatestReleasedSingle(false, Groups.GetMainGroup());
                    if (single == null)
                    {
                        single = single3;
                    }
                    if (single2 == null)
                    {
                        single2 = single3;
                    }
                    if (single != null)
                    {
                        if ((single.GetCenter() == relationship.Girls[0] || single3.GetCenter() == relationship.Girls[0]) && relationship.Girls[0].trait == traits._trait._type.Arrogant)
                        {
                            relationship.Add(-0.5f);
                        }
                        else if ((single2.GetCenter() == relationship.Girls[1] || single3.GetCenter() == relationship.Girls[1]) && relationship.Girls[1].trait == traits._trait._type.Arrogant)
                        {
                            relationship.Add(-0.5f);
                        }
                    }
                }
            }
            return;
        }

        // Girls with Forgiving trait will never dislike or hate any other girls
        //
        // Postfix Relationships._relationship.Recalc()
        // Postfix: Unfair_plus_v2_0.Traits_only.RecalcPostfix(this);
        public static void RecalcPostfix(Relationships._relationship __this)
        {
            if (Unfair_plus_v2_1_1.BugFix.IsModEnabled("Unfair_plus") || Unfair_plus_v2_1_1.BugFix.IsModEnabled("Traits_only"))
            {
                if (__this.Ratio < 0.5f)
                {
                    if ((__this.Girls[0].trait == traits._trait._type.Forgiving || __this.Girls[1].trait == traits._trait._type.Forgiving))
                    {
                        __this.Ratio = 0.5f;
                    }
                }
            }
            return;
        }

        // Girls with Meme Queen trait get +10 to all stats for internet shows
        // 
        // Infix for float param.val in Shows._show.AddCastParam()
        // Infix after param.val = data_girls.GetAverageParam(type, girlList);
        // Infix: param.val = Unfair_plus_v2_0.Traits_only.Shows_showAddCastParamInfix(param.val,girlList,this);
        public static float Shows_showAddCastParamInfix(float val, List<data_girls.girls> girlList, Shows._show __this)
        {
            float output = val;
            if (Unfair_plus_v2_1_1.BugFix.IsModEnabled("Unfair_plus") || Unfair_plus_v2_1_1.BugFix.IsModEnabled("Traits_only"))
            {
                float counter = 0;
                float sum = 0f;
                foreach (data_girls.girls girls in girlList)
                {
                    if (girls != null)
                    {
                        counter++;
                        if (girls.trait == traits._trait._type.Meme_queen)
                        {
                            if (__this.medium.media_type == Shows._param._media_type.internet)
                            {
                                sum += 10f;
                            }
                        }
                    }
                }
                if (counter > 0)
                {
                    output += sum / counter;
                }
            }
            return output;
        }

        // Girls with Meme Queen trait get +10 to all stats for internet shows
        // 
        // Infix for float param.val in Show_Popup.AddCastParam()
        // Infix after param.val = data_girls.GetAverageParam(type, girlList);
        // Infix: param.val = Unfair_plus_v2_0.Traits_only.Show_PopupAddCastParamInfix(param.val, girlList, this.medium.media_type);
        public static float Show_PopupAddCastParamInfix(float val, List<data_girls.girls> girlList, Shows._param._media_type? medium)
        {
            float output = val;
            if (Unfair_plus_v2_1_1.BugFix.IsModEnabled("Unfair_plus") || Unfair_plus_v2_1_1.BugFix.IsModEnabled("Traits_only"))
            {
                float counter = 0;
                float sum = 0f;
                foreach (data_girls.girls girls in girlList)
                {
                    if (girls != null)
                    {
                        counter++;
                        if (girls.trait == traits._trait._type.Meme_queen)
                        {
                            if (medium == Shows._param._media_type.internet)
                            {
                                sum += 10f;
                            }
                        }
                    }
                }
                if(counter > 0)
                {
                    output += sum / counter;
                }
            }
            return output;
        }

        // Update shows on medium selection just in case of meme queen
        //
        // Postfix for Show_Popup.SetParam(Shows._param __param, Show_Popup_Param_Button._type type)
        // Postfix: Unfair_plus_v2_0.Traits_only.SetParamPostfix(this,this.castType);
        public static void SetParamPostfix(Show_Popup __this, Shows._show._castType? castType)
        {
            if (Unfair_plus_v2_1_1.BugFix.IsModEnabled("Unfair_plus") || Unfair_plus_v2_1_1.BugFix.IsModEnabled("Traits_only"))
            {
                if (castType != null)
                {
                    __this.SetCastType(castType.Value);
                }
            }
            return;
        }

        // Girls with Meme Queen trait get +10% success rate and +5% crit success rate when participating in viral marketing campaigns
        //
        // Postfix for singles._param.GetSuccessChance(Single_Marketing_Roll._result Result, int Level, singles._single Single = null)
        // Postfix: _return = Unfair_plus_v2_0.Traits_only.GetSuccessChancePostfix(_return,this,Result,Single);
        public static float GetSuccessChancePostfix(float __return, singles._param __this, Single_Marketing_Roll._result Result, singles._single Single)
        {
            float output = __return;
            if (Unfair_plus_v2_1_1.BugFix.IsModEnabled("Unfair_plus") || Unfair_plus_v2_1_1.BugFix.IsModEnabled("Traits_only"))
            {
                if (Single != null && __this.Special_Type == singles._param._special_type.viral_campaign)
                {
                    bool flag = false;
                    foreach (data_girls.girls girls in Single.girls)
                    {
                        if (girls != null && girls.trait == traits._trait._type.Meme_queen)
                        {
                            flag = true;
                        }
                    }
                    if (flag)
                    {
                        if (Result == Single_Marketing_Roll._result.success)
                        {
                            output += 10;
                        }
                        else if (Result == Single_Marketing_Roll._result.success_crit)
                        {
                            output += 5;
                        }
                        else if (Result == Single_Marketing_Roll._result.fail)
                        {
                            output -= 15;
                        }
                    }
                }
            }
            return output;
        }

        // Girls with Annoying trait cause other members to spend 1.2x physical stamina in shows
        //
        // Postfix for Shows._show.SetStamina()
        // Postfix: Unfair_plus_v2_0.Traits_only.SetStaminaPostfix(this);
        public static void SetStaminaPostfix(Shows._show __this)
        {
            if (Unfair_plus_v2_1_1.BugFix.IsModEnabled("Unfair_plus") || Unfair_plus_v2_1_1.BugFix.IsModEnabled("Traits_only"))
            {
                List<data_girls.girls> cast = __this.GetCast();
                float staminaCost = __this.GetStaminaCost();
                int annoyCount = 0;
                foreach (data_girls.girls girls in cast)
                {
                    if (girls != null && girls.trait == traits._trait._type.Annoying && girls.IsActive())
                    {
                        annoyCount++;
                    }
                }
                foreach (data_girls.girls girls2 in cast)
                {
                    if (girls2.IsActive())
                    {
                        if (annoyCount == 1)
                        {
                            if (girls2.trait != traits._trait._type.Annoying)
                            {
                                girls2.addParam(data_girls._paramType.physicalStamina, -staminaCost * 0.2f, false);
                            }
                        }
                        else if (annoyCount > 1)
                        {
                            girls2.addParam(data_girls._paramType.physicalStamina, -staminaCost * 0.2f, false);
                        }
                    }
                }
            }
            return;
        }

        // Girls with Misandry trait have a 20% chance of receiving bad opinions from Male fans when participating in a single with handshakes
        //
        // Postfix for singles.ReleaseSingle(singles._single single)
        // Postfix: Unfair_plus_v2_0.Traits_only.ReleaseSinglePostfix(single);
        public static void ReleaseSinglePostfix(singles._single single)
        {
            if (Unfair_plus_v2_1_1.BugFix.IsModEnabled("Unfair_plus") || Unfair_plus_v2_1_1.BugFix.IsModEnabled("Traits_only"))
            {
                foreach (data_girls.girls girls in single.girls)
                {
                    if (girls != null)
                    {
                        if (girls.trait == traits._trait._type.Misandry && single.IsGroupHS() && mainScript.chance(20))
                        {
                            girls.AddAppeal(resources.fanType.male, -1f);
                        }
                        else if (girls.trait == traits._trait._type.Misandry && single.IsIndividualHS() && mainScript.chance(20))
                        {
                            girls.AddAppeal(resources.fanType.male, -1f);
                        }
                    }
                }
            }
            return;
        }

        // Girls with Perfectionist trait get -20 to mental stamina when world tours end with less than 80% average attendance
        //
        // Postfix for SEvent_Tour.FinishTour()
        // Postfix: Unfair_plus_v2_0.Traits_only.FinishTourPostfix(this);
        public static void FinishTourPostfix(SEvent_Tour __this)
        {
            if (Unfair_plus_v2_1_1.BugFix.IsModEnabled("Unfair_plus") || Unfair_plus_v2_1_1.BugFix.IsModEnabled("Traits_only"))
            {
                List<data_girls.girls> activeGirls = data_girls.GetActiveGirls(null);
                foreach (data_girls.girls girls in activeGirls)
                {
                    if (girls.trait == traits._trait._type.Perfectionist && __this.Tour.GetAverageAttendance() < 80)
                    {
                        girls.getParam(data_girls._paramType.mentalStamina).add(-20f, false);
                    }
                }
            }
            return;
        }

        // Girls with Perfectionist trait get -20 to mental stamina when they participate in concerts with less than 100% hype.
        //
        // Postfix for SEvent_Concerts._concert.Finish()
        // Postfix: Unfair_plus_v2_0.Traits_only.FinishPostfix(this);
        public static void FinishPostfix(SEvent_Concerts._concert __this)
        {
            if (Unfair_plus_v2_1_1.BugFix.IsModEnabled("Unfair_plus") || Unfair_plus_v2_1_1.BugFix.IsModEnabled("Traits_only"))
            {
                foreach (data_girls.girls girls in __this.GetGirls(true))
                {
                    if (girls.trait == traits._trait._type.Perfectionist && __this.Hype < 100f)
                    {
                        girls.getParam(data_girls._paramType.mentalStamina).add(-20f, false);
                    }
                }
            }
            return;
        }



        // This method calculates the modifier to girl parameters based on their trait.
        private static int GetTraitModifier(data_girls.girls girls, data_girls._paramType type, List<data_girls.girls> allGirls = null)
        {
            float num = 0;
            if (girls != null && data_girls.IsStatParam(type))
            {
                int ind = 0;
                foreach (SEvent_Tour.tour tour in SEvent_Tour.Tours)
                {
                    if (tour.Status == SEvent_Tour.tour._status.normal)
                    {
                        ind++;
                    }
                }
                foreach (SEvent_SSK._SSK sSK in SEvent_SSK.Elections)
                {
                    if (sSK.Status == SEvent_Tour.tour._status.normal)
                    {
                        ind++;
                    }
                }
                foreach (SEvent_Concerts._concert concert in SEvent_Concerts.Concerts)
                {
                    if (concert.Status == SEvent_Tour.tour._status.normal)
                    {
                        ind++;
                    }
                }
                if (girls.trait == traits._trait._type.Anxiety && ind > 0)
                {
                    num -= 10f;
                }
                if (girls.trait == traits._trait._type.Clumsy && type == data_girls._paramType.dance)
                {
                    num -= 30f;
                }
                if (girls.trait == traits._trait._type.Clumsy && type == data_girls._paramType.funny)
                {
                    num += 30f;
                }
                if (girls.trait == traits._trait._type.Worrier && resources.GetScandalPointsTotal() > 0L)
                {
                    num -= 20f;
                }
                singles._single single = singles.GetLatestReleasedSingle(false, girls.GetGroup());
                singles._single single2 = singles.GetLatestReleasedSingle(false, Groups.GetMainGroup());
                singles._single single3 = null;
                if (single == null)
                {
                    single = single2;
                }
                if (single != null)
                {
                    if (single.ReleaseData.ReleaseDate > single2.ReleaseData.ReleaseDate)
                    {
                        single3 = single;
                    }
                    else if (single.ReleaseData.ReleaseDate < single2.ReleaseData.ReleaseDate)
                    {
                        single3 = single2;
                    }
                    else if (single.ReleaseData.Sales > single2.ReleaseData.Sales)
                    {
                        single3 = single;
                    }
                    else
                    {
                        single3 = single2;
                    }
                }
                if (single != null && girls.trait == traits._trait._type.Complacent && (girls == single.GetCenter() || girls == single2.GetCenter()) && (type == data_girls._paramType.vocal || type == data_girls._paramType.dance))
                {
                    num -= 20f;
                }
                if (single3 != null && (staticVars.dateTime - single3.ReleaseData.ReleaseDate).Days >= staticVars.dateTime.Day && single3.ReleaseData.Chart_Position != 1)
                {
                    if (girls.trait == traits._trait._type.Defeatist)
                    {
                        num -= 20f;
                    }
                    else if (girls.trait == traits._trait._type.Underdog)
                    {
                        num += 20f;
                    }
                }
                if (allGirls != null && girls.trait == traits._trait._type.Lone_Wolf)
                {
                    int count = 0;
                    foreach (data_girls.girls girls1 in allGirls)
                    {
                        if (girls1 != null) count++;
                    }
                    if (count == 1)
                    {
                        num += 40f;
                    }
                }
            }
            return (int)num;
        }

    }
}