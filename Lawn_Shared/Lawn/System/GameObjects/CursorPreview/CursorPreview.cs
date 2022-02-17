using System;
using Sexy;

namespace Lawn
{
    public/*internal*/ class CursorPreview : GameObject
    {
        public CursorPreview()
        {
            mX = 0;
            mY = 0;
            mWidth = 80;
            mHeight = 80;
            mGridX = 0;
            mGridY = 0;
            mVisible = false;
        }

        public void Update()//3Update
        {
            if (mApp.mGameScene == GameScenes.Playing && mApp.mWidgetManager.mMouseIn)
            {
                var v2 = mBoard.GetSeedTypeInCursor();
                var lastMouseX = mBoard.mLastToolX;//mApp.mWidgetManager.mLastMouseX;
                var lastMouseY = mBoard.mLastToolY;//mApp.mWidgetManager.mLastMouseY;
                mGridX = mBoard.PlantingPixelToGridX((int)(lastMouseX * Constants.IS), (int)(lastMouseY * Constants.IS), v2);
                mGridY = mBoard.PlantingPixelToGridY((int)(lastMouseX * Constants.IS), (int)(lastMouseY * Constants.IS), v2);
                if (mGridX >= 0 && mGridX < Constants.GRIDSIZEX && mGridY >= 0 && mGridY <= Constants.MAX_GRIDSIZEY)
                {
                    if (mBoard.IsPlantInCursor()
                       || mBoard.mCursorObject.mCursorType == CursorType.Wheeelbarrow
                       && mApp.mZenGarden.GetPottedPlantInWheelbarrow() != null)
                    {
                        if (mBoard.CanPlantAt(mGridX, mGridY, v2) == PlantingReason.Ok)
                        {
                            mX = mBoard.GridToPixelX(mGridX, mGridY);
                            mY = mBoard.GridToPixelY(mGridX, mGridY);
                            mVisible = true;
                            return;
                        }
                    }
                }
            }
            mVisible = false;
        }

        public override bool LoadFromFile(Sexy.Buffer b)
        {
            return false;
        }

        public override bool SaveToFile(Sexy.Buffer b)
        {
            return false;
        }

        public void Draw(Graphics g)
        {
            SeedType seedTypeInCursor = mBoard.GetSeedTypeInCursor();
            if (seedTypeInCursor == SeedType.None)
            {
                return;
            }
            g.SetColorizeImages(true);
            g.SetColor(new SexyColor(0xFF, 0xFF, 0xFF, 0x64 + 0x20)); // Extra 0x20 opacity is added for better clearness in this version
            var cursorType = mBoard.mCursorObject.mCursorType;
            PottedPlant v9 = null;
            if (cursorType == CursorType.Wheeelbarrow || cursorType == CursorType.PlantFromWheelBarrow)
            {
                v9 = mApp.mZenGarden.GetPottedPlantInWheelbarrow();
            }
            else if (cursorType == CursorType.PlantFromGlove)
            {
                v9 = mApp.GetPottedPlantByIndex(mBoard.mCursorObject.mGlovePlantID.mPottedPlantIndex);
            }
            if (v9 != null)
            {
                mApp.mZenGarden.DrawPottedPlant(g, 0, 0, v9, 1, mBoard.mBackground != BackgroundType.MushroomGarden
                     && mBoard.mBackground != BackgroundType.Zombiquarium);
            }
            else
            {
                float y;
                float x;
                if (mApp.IsIZombieLevel())
                {
                    float aY = Plant.PlantDrawHeightOffset(mBoard, null, seedTypeInCursor, mGridX, mGridY);
                    if (seedTypeInCursor == SeedType.ZombieGargantuar)
                        aY -= 30.0f;
                    y = aY - 78.0f;
                    x = -49.0f;
                }
                else
                {
                    y = Plant.PlantDrawHeightOffset(mBoard, null, seedTypeInCursor, mGridX, mGridY);
                    x = 0.0f;
                }
                Plant.DrawSeedType(
                  g,
                  mBoard.mCursorObject.mType,
                  mBoard.mCursorObject.mImitaterType,
                  DrawVariation.Normal,
                  x,
                  y);
            }
            if (mApp.mGameMode == GameMode.ChallengeColumn) 
            {
                for (int i = 0; i < Constants.MAX_GRIDSIZEY; i++) 
                {
                    if (i != mGridY && mBoard.CanPlantAt(mGridX, i, seedTypeInCursor) == PlantingReason.Ok) 
                    { 
                        Plant.DrawSeedType(
                          g,
                          mBoard.mCursorObject.mType,
                          mBoard.mCursorObject.mImitaterType,
                          DrawVariation.Normal,
                          0,
                          (i - mGridY) * Constants.S * 85.0f + Plant.PlantDrawHeightOffset(mBoard, null, seedTypeInCursor, mGridX, i));
                    }
                }
            }
            g.SetColorizeImages(false);
        }

        public int mGridX;

        public int mGridY;
    }
}
