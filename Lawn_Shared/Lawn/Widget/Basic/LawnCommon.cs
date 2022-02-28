using System;
using Sexy;

namespace Lawn
{
    internal static class LawnCommon
    {
        public static EditWidget CreateEditWidget(int theId, EditListener theListener, Dialog theDialog, string title, string description)
        {
            EditWidget editWidget = new LawnEditWidget(theId, theListener, theDialog, title, description);
            editWidget.SetFont(Resources.FONT_BRIANNETOD16);
            editWidget.SetFont(Resources.FONT_BRIANNETOD16);
            editWidget.SetColors(gLawnEditWidgetColors, 5);
            editWidget.mBlinkDelay = 14;
            return editWidget;
        }

        public static void DrawEditBox(Graphics g, EditWidget theWidget)
        {
            Rect theDest = new Rect(theWidget.mX - (int)Constants.InvertAndScale(5f), theWidget.mY - (int)Constants.InvertAndScale(6f), theWidget.mWidth + (int)Constants.InvertAndScale(10f), theWidget.mHeight + (int)Constants.InvertAndScale(12f));
            g.DrawImageBox(theDest, AtlasResources.IMAGE_EDITBOX);
        }

        public static void DrawCheckboxText(Graphics g, string theText, Widget theCheckbox)
        {
            LawnCommon.DrawCheckboxText(g, theText, theCheckbox, null);
        }

        public static void DrawCheckboxText(Graphics g, string theText, Widget theCheckbox, string theText2)
        {
            if (theText2 != null)
            {
                g.DrawString(theText, theCheckbox.mX - g.mTransX + 43, theCheckbox.mY - g.mTransY + 15);
                g.DrawString(theText2, theCheckbox.mX - g.mTransX + 43, theCheckbox.mY - g.mTransY + 30);
                return;
            }
            g.DrawString(theText, theCheckbox.mX - g.mTransX + 43, theCheckbox.mY - g.mTransY + 24);
        }

        public static string GetSavedGameName(GameMode theGameMode, int theProfileId)
        {
            return GlobalStaticVars.GetDocumentsDir() + Common.StrFormat_("userdata/game{0}_{1}.dat", theProfileId, (int)theGameMode);
        }

        public static Checkbox MakeNewCheckbox(int theId, CheckboxListener theListener, bool theDefault)
        {
            return new Checkbox(AtlasResources.IMAGE_OPTIONS_CHECKBOX0, AtlasResources.IMAGE_OPTIONS_CHECKBOX1, theId, theListener)
            {
                mChecked = theDefault,
                mHasAlpha = true,
                mHasTransparencies = true
            };
        }

        public static int GetCurrentDaysSince2000()
        {
            DateTime today = DateTime.Today;
            DateTime dateTime = new DateTime(2010, 1, 1);
            return (int)today.Subtract(dateTime).TotalDays;
        }

        public static void DrawImageBox(Graphics g, Rect theDest, Image theComponentImage)
        {
            LawnCommon.DrawImageBox(g, theDest, theComponentImage, true);
        }

        public static void DrawImageBox(Graphics g, Rect theDest, Image theComponentImage, bool theDrawCenter)
        {
            Rect Rect = new Rect(0, 0, theComponentImage.mWidth, theComponentImage.mHeight);
            int num = Rect.mWidth / 3;
            int num2 = Rect.mHeight / 3;
            int x = Rect.mX;
            int y = Rect.mY;
            int num3 = Rect.mWidth - num * 2;
            int num4 = Rect.mHeight - num2 * 2;
            g.DrawImage(theComponentImage, theDest.mX, theDest.mY, new Rect(x, y, num, num2));
            g.DrawImage(theComponentImage, theDest.mX + theDest.mWidth - num, theDest.mY, new Rect(x + num + num3, y, num, num2));
            g.DrawImage(theComponentImage, theDest.mX, theDest.mY + theDest.mHeight - num2, new Rect(x, y + num2 + num4, num, num2));
            g.DrawImage(theComponentImage, theDest.mX + theDest.mWidth - num, theDest.mY + theDest.mHeight - num2, new Rect(x + num + num3, y + num2 + num4, num, num2));
            g.DrawImage(theComponentImage, new Rect(theDest.mX, theDest.mY + num2, num, theDest.mHeight - num2 * 2), new Rect(x, y + num2, num, num4));
            g.DrawImage(theComponentImage, new Rect(theDest.mX + theDest.mWidth - num, theDest.mY + num2, num, theDest.mHeight - num2 * 2), new Rect(x + num + num3, y + num2, num, num4));
            g.DrawImage(theComponentImage, new Rect(theDest.mX + num, theDest.mY, theDest.mWidth - num * 2, num2), new Rect(x + num, y, num3, num2));
            g.DrawImage(theComponentImage, new Rect(theDest.mX + num, theDest.mY + theDest.mHeight - num2, theDest.mWidth - num * 2, num2), new Rect(x + num, y + num2 + num4, num3, num2));
            if (theDrawCenter)
            {
                g.DrawImage(theComponentImage, new Rect(theDest.mX + num, theDest.mY + num2, theDest.mWidth - 2 * num, theDest.mHeight - 2 * num2), new Rect(x + num, y + num2, num3, num4));
            }
        }

        public static void TileImageHorizontally(Graphics g, Image theImage, int x, int y, int theWidth)
        {
            while (theWidth > 0)
            {
                int theWidth2 = Math.Min(theWidth, theImage.mWidth);
                g.DrawImage(theImage, x, y, new Rect(0, 0, theWidth2, theImage.mHeight));
                x += theImage.mWidth;
                theWidth -= theImage.mWidth;
            }
        }

        public static TimeSpan time_day()
        {
            return DateTime.UtcNow.TimeOfDay;
        }

        public static void DrawHorzTile(Graphics g, Image theImage, int x, int y, int width)
        {
            for (int i = 0; i < width; i += theImage.mWidth)
            {
                int num = Math.Min(width - i, theImage.mWidth);
                g.DrawImage(theImage, x, y, new Rect(0, 0, num, theImage.mHeight));
                x += num;
            }
        }

        public static void DrawVertTile(Graphics g, Image theImage, int x, int y, int height)
        {
            for (int i = 0; i < height; i += theImage.mHeight)
            {
                int num = Math.Min(height - i, theImage.mHeight);
                g.DrawImage(theImage, x, y, new Rect(0, 0, theImage.mWidth, num));
                y += num;
            }
        }

        public static void DrawStringWithOutline(Graphics g, string theStr, int x, int y, Font theOutlineFont, SexyColor theOutlineColor)
        {
            Font font = g.GetFont();
            SexyColor aColor = g.GetColor();
            g.SetFont(theOutlineFont);
            g.SetColor(theOutlineColor);
            g.DrawString(theStr, x, y);
            g.SetFont(font);
            g.SetColor(aColor);
            g.DrawString(theStr, x, y);
        }

        public static string GetTimeString(int theNumSeconds)
        {
            int num = theNumSeconds / 60;
            int num2 = num / 60;
            if (num2 <= 0)
            {
                return Common.StrFormat_("%d:%02d", num, theNumSeconds % 60);
            }
            return Common.StrFormat_("%2d:%02d:%02d", num2, num % 60, theNumSeconds % 60);
        }

        public static string GetNthStr(int theNumber)
        {
            int num = theNumber / 10 % 10;
            int num2 = theNumber % 10;
            if (num == 1)
            {
                return "th";
            }
            switch (num2)
            {
            case 1:
                return "st";
            case 2:
                return "nd";
            case 3:
                return "rd";
            default:
                return "th";
            }
        }

        public static string GetNthBossStr(int theNumber)
        {
            if (theNumber <= 1)
            {
                return "FINAL BOSS";
            }
            LawnCommon.aStr = Common.StrFormat_("%d%s FINAL BOSS", theNumber, LawnCommon.GetNthStr(theNumber));
            return LawnCommon.aStr;
        }

        public static int[,] gUserListWidgetColors = new int[,]
        {
            {
                23,
                24,
                35
            },
            {
                0,
                0,
                0
            },
            {
                235,
                225,
                180
            },
            {
                255,
                255,
                255
            },
            {
                20,
                180,
                15
            }
        };

        public static int[,] gLawnEditWidgetColors = new int[,]
        {
            {
                0,
                0,
                0,
                0
            },
            {
                0,
                0,
                0,
                0
            },
            {
                240,
                240,
                255,
                255
            },
            {
                255,
                255,
                255,
                255
            },
            {
                0,
                0,
                0,
                255
            }
        };

        public static int gDebugDayInc = 0;

        private static string aStr;
    }
}
