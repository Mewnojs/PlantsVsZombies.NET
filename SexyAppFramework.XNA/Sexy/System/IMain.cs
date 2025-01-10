using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.GamerServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sexy
{
    public abstract class IMain : Game
    {
        public static bool DO_LOW_MEMORY_OPTIONS { get; protected set; }

        public abstract void CompensateForSlowUpdate();

        public abstract string FetchApplicationStoragePath();

        public static bool IsInTrialMode
        {
            get
            {
                return IMain.trialModeCachedValue;
            }
        }

        public static SignedInGamer GetGamer()
        {
            if (Gamer.SignedInGamers.Count == 0)
            {
                return null;
            }
            return Gamer.SignedInGamers[PlayerIndex.One];
        }

        public void NeedToSetUpOrientationMatrix(UI_ORIENTATION orientation)
        {
            orientationUsed = orientation;
            newOrientation = true;
        }

        protected void SetupOrientationMatrix(UI_ORIENTATION orientation)
        {
            newOrientation = false;
        }

        protected SexyTransform2D orientationTransform;

        protected UI_ORIENTATION orientationUsed;

        protected bool newOrientation;

        public static GamerServicesComponent GamerServicesComp;

        public static bool trialModeChecked = false;

        public static bool trialModeCachedValue = true;
    }
}
