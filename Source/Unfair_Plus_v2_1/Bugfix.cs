using System;
using System.Runtime.ConstrainedExecution;
using UnityEngine;
using UnityEngine.UI;

namespace Unfair_plus_v2_1
{
    public class Bugfix
    {
        // Fixed fan pie charts so that it is correct
        //
        // Postfix for Profile_Fans_Pies.Render_Pies()
        // Postfix: Unfair_plus_v2_1.Bugfix.Render_PiesPostfix(this, this.Girl);
        public static void Render_PiesPostfix(Profile_Fans_Pies __this, data_girls.girls Girl)
        {
            if (staticVars.Settings.IsModEnabled("Unfair_plus"))
            {
                if (Girl.GetFans_Total(null) != 0L)
                {
                    __this.Fans_Pie_Adult.GetComponent<Image>().fillAmount += __this.Fans_Pie_YA.GetComponent<Image>().fillAmount -
                        __this.Fans_Pie_Teen.GetComponent<Image>().fillAmount;
                }
            }
            return;
        }

        // Fixed tour revenue text color to consider savings
        //
        // Postfix for Tour_New_Popup.Render()
        // Postfix: Unfair_plus_v2_1.Bugfix.RenderPostfix(this);
        public static void RenderPostfix(Tour_New_Popup __this)
        {
            if (staticVars.Settings.IsModEnabled("Unfair_plus"))
            {
                if (__this.Tour.ExpectedRevenue > __this.Tour.ProductionCost - __this.Tour.Saving)
                {
                    ExtensionMethods.SetColor(__this.ExpectedRevenue, mainScript.green32);
                }
            }
            return;

        }
    }
}