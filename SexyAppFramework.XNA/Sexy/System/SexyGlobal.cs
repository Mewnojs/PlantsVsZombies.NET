using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sexy
{
    public class SexyGlobal
    {
        public static Graphics g;

        public static SexyAppBase gSexyAppBase;

        internal static string CommaSeperate_(int theDispPoints)
        {
            if (theDispPoints == 0)
            {
                return "0";
            }
            return string.Format("{0:#,#}", theDispPoints);
        }
    }
}
