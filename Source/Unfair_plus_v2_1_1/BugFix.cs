using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unfair_plus_v2_1_1
{
    public class BugFix
    {
        // New method to use to check if mod is enabled that uses ModTitle instead of ModName
        public static bool IsModEnabled(string ModTitle)
        {
            bool output = false;
            foreach (Mods._mod mod in Mods._Mods)
            {
                if (mod.Title.IndexOf(ModTitle) > -1 && mod.Enabled)
                {
                    output = true;
                    break;
                }
            }
            return output;
        }
    }
}
