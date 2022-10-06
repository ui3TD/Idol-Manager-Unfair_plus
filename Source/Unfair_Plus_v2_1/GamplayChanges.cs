using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;

namespace Unfair_plus_v2_1
{
    internal class GameplayChanges
    {
        // Remove time speed patch from v2.0
        //
        // Postfix of mainScript.Time_SetState(mainScript._time_state state)
        // Postfix: Unfair_plus_v2_1.GameplayChanges.Time_SetStatePostfix();
        public static void Time_SetStatePostfix()
        {
            if (staticVars.Settings.IsModEnabled("Unfair_plus"))
            {

            }
        }

        // Speed up time if you click on the fast forward button twice
        //
        // Prefix of TimeControlButton.OnClick()
        // Prefix: if(!Unfair_plus_v2_1.GameplayChanges.OnClickPrefix(this)) return;
        public static bool OnClickPrefix(TimeControlButton __this)
        {
            bool output = true;
            if (staticVars.Settings.IsModEnabled("Unfair_plus"))
            {
                if(__this.Type == mainScript._time_state.fast && staticVars.timeState == mainScript._time_state.fast && staticVars.dateTimeAddMinutesPerSecond != 1000)
                {
                    staticVars.dateTimeAddMinutesPerSecond = 1000;
                    Camera.main.GetComponent<mainScript>().TimeControls_Fast.GetComponent<TextMeshProUGUI>().color = mainScript.gold32;

                    output = false;
                }
            }
            return output;
        }

        // Home row of keyboard (asdfg...) are hotkeys to open each sidebar tab.
        // 
        // Prefix of Controls.Update()
        // Prefix: if(!Unfair_plus_v2_1.GameplayChanges.UpdatePreFix()) return;
        public static bool UpdatePreFix()
        {
            if (staticVars.Settings.IsModEnabled("Unfair_plus"))
            {
                if (mainScript.IsBlockingHotkeys())
                {
                    return false;
                }
                if (Input.GetKeyDown(KeyCode.Alpha4))
                {
                    Camera.main.GetComponent<mainScript>().Time_SetState(mainScript._time_state.fast);
                    staticVars.dateTimeAddMinutesPerSecond = 1000;
                    Camera.main.GetComponent<mainScript>().TimeControls_Fast.GetComponent<TextMeshProUGUI>().color = mainScript.gold32;

                }
                else if (Input.GetKeyDown(KeyCode.A))
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

        // Each MC fame point gives a boost to new fans (fixed bug from v2.0)
        //
        // Infix for int num7 in Shows._show.SetSales()
        // Infix before if (this.episodeCount == 1 || this.episodeCount == 0)
        // Infix: num7 = Unfair_plus_v2_1.GameplayChanges.SetSalesInfix(num7,this);
        public static int SetSalesInfix(int num7, Shows._show __this)
        {
            int output = num7;
            if (staticVars.Settings.IsModEnabled("Unfair_plus"))
            {
                float mcCoeff = 1f;
                if (__this.mc != null)
                {
                    mcCoeff = 1f + (float)__this.mc.fame * (float)__this.mc.fame / 10f;
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
    }
}
