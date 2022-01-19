using System;
using System.Collections.Generic;
using Sexy;
using Sexy.TodLib;

namespace Lawn
{
    public/*internal*/ class Projectile : GameObject
    {
        public static void PreallocateMemory()
        {
            for (int i = 0; i < 200; i++)
            {
                new Projectile().PrepareForReuse();
            }
        }

        internal static Projectile GetNewProjectile()
        {
            if (Projectile.unusedObjects.Count > 0)
            {
                Projectile projectile = Projectile.unusedObjects.Pop();
                projectile.Reset();
                return projectile;
            }
            return new Projectile();
        }

        private Projectile()
        {
        }

        public override void PrepareForReuse()
        {
            Projectile.unusedObjects.Push(this);
        }

        public override bool SaveToFile(Sexy.Buffer b)
        {
            base.SaveToFile(b);
            b.WriteFloat(mAccZ);
            b.WriteLong(mAnimCounter);
            b.WriteLong(mAnimTicksPerFrame);
            b.WriteLong(mClickBackoffCounter);
            b.WriteLong(mCobTargetRow);
            b.WriteFloat(mCobTargetX);
            b.WriteLong(mDamageRangeFlags);
            b.WriteBoolean(mDead);
            b.WriteLong(mFrame);
            b.WriteLong(mHitTorchwoodGridX);
            b.WriteLong(mLastPortalX);
            b.WriteLong((int)mMotionType);
            b.WriteLong(mNumFrames);
            b.WriteBoolean(mOnHighGround);
            b.WriteFloat(mPosX);
            b.WriteFloat(mPosY);
            b.WriteFloat(mPosZ);
            b.WriteLong(mProjectileAge);
            b.WriteLong((int)mProjectileType);
            b.WriteFloat(mRotation);
            b.WriteFloat(mRotationSpeed);
            b.WriteFloat(mShadowY);
            b.WriteFloat(mVelX);
            b.WriteFloat(mVelY);
            b.WriteFloat(mVelZ);
            GameObject.SaveId(mTargetZombieID, b);
            return true;
        }

        public override bool LoadFromFile(Sexy.Buffer b)
        {
            base.LoadFromFile(b);
            mAccZ = b.ReadFloat();
            mAnimCounter = b.ReadLong();
            mAnimTicksPerFrame = b.ReadLong();
            mClickBackoffCounter = b.ReadLong();
            mCobTargetRow = b.ReadLong();
            mCobTargetX = b.ReadFloat();
            mDamageRangeFlags = b.ReadLong();
            mDead = b.ReadBoolean();
            mFrame = b.ReadLong();
            mHitTorchwoodGridX = b.ReadLong();
            mLastPortalX = b.ReadLong();
            mMotionType = (ProjectileMotion)b.ReadLong();
            mNumFrames = b.ReadLong();
            mOnHighGround = b.ReadBoolean();
            mPosX = b.ReadFloat();
            mPosY = b.ReadFloat();
            mPosZ = b.ReadFloat();
            mProjectileAge = b.ReadLong();
            mProjectileType = (ProjectileType)b.ReadLong();
            mRotation = b.ReadFloat();
            mRotationSpeed = b.ReadFloat();
            mShadowY = b.ReadFloat();
            mVelX = b.ReadFloat();
            mVelY = b.ReadFloat();
            mVelZ = b.ReadFloat();
            mTargetZombieIDSaved = GameObject.LoadId(b);
            return true;
        }

        public override void LoadingComplete()
        {
            base.LoadingComplete();
            mTargetZombieID = (GameObject.GetObjectById(mTargetZombieIDSaved) as Zombie);
            Projectile newProjectile = Projectile.GetNewProjectile();
            newProjectile.ProjectileInitialize(mX, mY, mRenderOrder, mRow, mProjectileType);
            if (mProjectileType == ProjectileType.Fireball)
            {
                ConvertToFireball();
            }
        }

        protected override void Reset()
        {
            base.Reset();
            mFrame = 0;
            mNumFrames = 0;
            mAnimCounter = 0;
            mPosX = 0f;
            mPosY = 0f;
            mPosZ = 0f;
            mVelX = 0f;
            mVelY = 0f;
            mVelZ = 0f;
            mAccZ = 0f;
            mShadowY = 0f;
            mDead = false;
            mAnimTicksPerFrame = 0;
            mMotionType = ProjectileMotion.Straight;
            mProjectileType = ProjectileType.Pea;
            mProjectileAge = 0;
            mClickBackoffCounter = 0;
            mRotation = 0f;
            mRotationSpeed = 0f;
            mOnHighGround = false;
            mDamageRangeFlags = 0;
            mHitTorchwoodGridX = 0;
            mAttachmentID = null;
            mCobTargetX = 0f;
            mCobTargetRow = 0;
            mTargetZombieID = null;
            mLastPortalX = 0;
        }

        public void ProjectileInitialize(int theX, int theY, int theRenderOrder, int theRow, ProjectileType theProjectileType)
        {
            int num = mBoard.PixelToGridXKeepOnBoard(theX, theY);
            mProjectileType = theProjectileType;
            mPosX = theX;
            mPosY = theY;
            mPosZ = 0f;
            mVelX = 0f;
            mVelY = 0f;
            mVelZ = 0f;
            mAccZ = 0f;
            mShadowY = mBoard.GridToPixelY(num, theRow) + 67f;
            mMotionType = ProjectileMotion.Straight;
            mFrame = 0;
            mNumFrames = 1;
            mRow = theRow;
            mDamageRangeFlags = 0;
            mHitTorchwoodGridX = -1;
            mDead = false;
            mAttachmentID = null;
            mCobTargetX = 0f;
            mCobTargetRow = 0;
            mTargetZombieID = null;
            mLastPortalX = -1;
            if (mBoard.mGridSquareType[num, mRow] == GridSquareType.HighGround)
            {
                mOnHighGround = true;
            }
            else
            {
                mOnHighGround = false;
            }
            if (mBoard.StageHasRoof() && theX < 480f)
            {
                mShadowY -= 12f;
            }
            mRenderOrder = theRenderOrder;
            mProjectileAge = 0;
            mClickBackoffCounter = 0;
            mRotation = 0f;
            mRotationSpeed = 0f;
            mWidth = 40;
            mHeight = 40;
            mAnimTicksPerFrame = 0;
            bool flag = false;
            if (mProjectileType == ProjectileType.Cabbage || mProjectileType == ProjectileType.Butter)
            {
                mRotation = -0.87964594f;
                mRotationSpeed = TodCommon.RandRangeFloat(-0.08f, -0.02f);
            }
            else if (mProjectileType == ProjectileType.Melon || mProjectileType == ProjectileType.Wintermelon)
            {
                mRotation = -1.2566371f;
                mRotationSpeed = TodCommon.RandRangeFloat(-0.08f, -0.02f);
            }
            else if (mProjectileType == ProjectileType.Kernel)
            {
                mRotation = 0f;
                mRotationSpeed = TodCommon.RandRangeFloat(-0.2f, -0.08f);
            }
            else if (mProjectileType == ProjectileType.Snowpea)
            {
                float num2 = 8f;
                float num3 = 13f;
                TodParticleSystem theParticleSystem = mApp.AddTodParticle(mPosX + num2, mPosY + num3, 400000, ParticleEffect.SnowpeaTrail);
                GlobalMembersAttachment.AttachParticle(ref mAttachmentID, theParticleSystem, num2, num3);
            }
            else if (mProjectileType == ProjectileType.Fireball)
            {
                Debug.ASSERT(false);
            }
            else if (mProjectileType == ProjectileType.Cobbig)
            {
                mWidth = (int)(AtlasResources.IMAGE_REANIM_COBCANNON_COB.GetWidth() * Constants.IS);
                mHeight = (int)(AtlasResources.IMAGE_REANIM_COBCANNON_COB.GetHeight() * Constants.IS);
                mRotation = 1.5707964f;
            }
            else if (mProjectileType == ProjectileType.Puff)
            {
                float num4 = 13f;
                float num5 = 13f;
                TodParticleSystem theParticleSystem2 = mApp.AddTodParticle(mPosX + num4, mPosY + num5, 400000, ParticleEffect.PuffshroomTrail);
                GlobalMembersAttachment.AttachParticle(ref mAttachmentID, theParticleSystem2, num4, num5);
            }
            else if (mProjectileType != ProjectileType.Butter)
            {
                if (mProjectileType == ProjectileType.Basketball)
                {
                    mRotation = TodCommon.RandRangeFloat(0f, 6.2831855f);
                    mRotationSpeed = TodCommon.RandRangeFloat(0.05f, 0.1f);
                }
                else if (mProjectileType == ProjectileType.Star)
                {
                    mShadowY += 15f;
                    mRotationSpeed = TodCommon.RandRangeFloat(0.05f, 0.1f);
                    if (RandomNumbers.NextNumber(2) == 0)
                    {
                        mRotationSpeed = -mRotationSpeed;
                    }
                }
                else if (mProjectileType == ProjectileType.ZombiePeaMindControl)
                {
                    mDamageRangeFlags = 1;
                }
            }
            if (flag)
            {
                mAnimCounter = TodCommon.RandRangeInt(0, mNumFrames * mAnimTicksPerFrame);
            }
            else
            {
                mAnimCounter = 0;
            }
            mX = (int)mPosX;
            mY = (int)mPosY;
        }

        public void Dispose()
        {
            GlobalMembersAttachment.AttachmentDie(ref mAttachmentID);
        }

        public void Update()
        {
            mProjectileAge += 3;
            if (mApp.mGameScene != GameScenes.Playing && !mBoard.mCutScene.ShouldRunUpsellBoard())
            {
                return;
            }
            int num = 20;
            if (mProjectileType == ProjectileType.Pea || mProjectileType == ProjectileType.Snowpea || mProjectileType == ProjectileType.Cabbage || mProjectileType == ProjectileType.Melon || mProjectileType == ProjectileType.Wintermelon || mProjectileType == ProjectileType.Kernel || mProjectileType == ProjectileType.Butter || mProjectileType == ProjectileType.Cobbig || mProjectileType == ProjectileType.ZombiePea || mProjectileType == ProjectileType.Spike || mProjectileType == ProjectileType.ZombiePeaMindControl)
            {
                num = 0;
            }
            if (mProjectileAge > num)
            {
                mRenderOrder = Board.MakeRenderOrder(RenderLayer.Projectile, mRow, 0);
            }
            if (mApp.IsFinalBossLevel())
            {
                mRenderOrder = Board.MakeRenderOrder(RenderLayer.Projectile, 5, 0);
            }
            if (mClickBackoffCounter > 0)
            {
                mClickBackoffCounter -= 3;
            }
            mRotation += mRotationSpeed * 3f;
            UpdateMotion();
            GlobalMembersAttachment.AttachmentUpdateAndMove(ref mAttachmentID, mPosX, mPosY + mPosZ);
        }

        public void Draw(Graphics g)
        {
            ProjectileDefinition projectileDef = GetProjectileDef();
            g.SetDrawMode(Graphics.DrawMode.DRAWMODE_NORMAL);
            Image image = null;
            float num = 1f;
            if (mProjectileType == ProjectileType.Cobbig)
            {
                image = AtlasResources.IMAGE_REANIM_COBCANNON_COB;
                num = 0.9f;
            }
            else if (mProjectileType == ProjectileType.Pea || mProjectileType == ProjectileType.ZombiePea || mProjectileType == ProjectileType.ZombiePeaMindControl)
            {
                image = AtlasResources.IMAGE_PROJECTILEPEA;
                if (mProjectileType == ProjectileType.ZombiePeaMindControl)
                {
                    g.SetColorizeImages(true);
                    g.SetColor(GameConstants.ZOMBIE_MINDCONTROLLED_COLOR);
                }
            }
            else if (mProjectileType == ProjectileType.Snowpea)
            {
                image = AtlasResources.IMAGE_PROJECTILESNOWPEA;
            }
            else if (mProjectileType == ProjectileType.Fireball)
            {
                image = null;
            }
            else if (mProjectileType == ProjectileType.Spike)
            {
                image = AtlasResources.IMAGE_PROJECTILECACTUS;
            }
            else if (mProjectileType == ProjectileType.Star)
            {
                image = AtlasResources.IMAGE_PROJECTILE_STAR;
            }
            else if (mProjectileType == ProjectileType.Puff)
            {
                image = AtlasResources.IMAGE_PUFFSHROOM_PUFF1;
                num = TodCommon.TodAnimateCurveFloat(0, 30, mProjectileAge, 0.3f, 1f, TodCurves.Linear);
            }
            else if (mProjectileType == ProjectileType.Basketball)
            {
                image = AtlasResources.IMAGE_REANIM_ZOMBIE_CATAPULT_BASKETBALL;
                num = 1.1f;
            }
            else if (mProjectileType == ProjectileType.Cabbage)
            {
                image = AtlasResources.IMAGE_REANIM_CABBAGEPULT_CABBAGE;
                num = 1f;
            }
            else if (mProjectileType == ProjectileType.Kernel)
            {
                image = AtlasResources.IMAGE_REANIM_CORNPULT_KERNAL;
                num = 0.95f;
            }
            else if (mProjectileType == ProjectileType.Butter)
            {
                image = AtlasResources.IMAGE_REANIM_CORNPULT_BUTTER;
                num = 0.8f;
            }
            else if (mProjectileType == ProjectileType.Melon)
            {
                image = AtlasResources.IMAGE_REANIM_MELONPULT_MELON;
                num = 1f;
            }
            else if (mProjectileType == ProjectileType.Wintermelon)
            {
                image = AtlasResources.IMAGE_REANIM_WINTERMELON_PROJECTILE;
                num = 1f;
            }
            else
            {
                Debug.ASSERT(false);
            }
            bool mirror = false;
            if (mMotionType == ProjectileMotion.BeeBackwards)
            {
                mirror = true;
            }
            if (image != null)
            {
                Debug.ASSERT(projectileDef.mImageRow < image.mNumRows);
                Debug.ASSERT(mFrame < image.mNumCols);
                int celWidth = image.GetCelWidth();
                int celHeight = image.GetCelHeight();
                TRect theSrcRect = new TRect(celWidth * mFrame, celHeight * projectileDef.mImageRow, celWidth, celHeight);
                if (TodCommon.FloatApproxEqual(mRotation, 0f) && TodCommon.FloatApproxEqual(num, 1f))
                {
                    g.DrawImageMirror(image, 0, 0, theSrcRect, mirror);
                }
                else
                {
                    float num2 = mPosX + celWidth * 0.5f;
                    float num3 = mPosZ + mPosY + celHeight * 0.5f;
                    SexyTransform2D sexyTransform2D = default(SexyTransform2D);
                    TodCommon.TodScaleRotateTransformMatrix(ref sexyTransform2D.mMatrix, num2 * Constants.S + mBoard.mX, num3 * Constants.S + mBoard.mY, mRotation, num, num);
                    TodCommon.TodBltMatrix(g, image, sexyTransform2D.mMatrix, ref g.mClipRect, SexyColor.White, g.mDrawMode, theSrcRect);
                }
            }
            if (mAttachmentID != null)
            {
                Graphics @new = Graphics.GetNew(g);
                base.MakeParentGraphicsFrame(@new);
                GlobalMembersAttachment.AttachmentDraw(mAttachmentID, @new, false, true);
                @new.PrepareForReuse();
            }
            if (mProjectileType == ProjectileType.ZombiePeaMindControl)
            {
                g.SetColorizeImages(false);
            }
        }

        public void DrawShadow(Graphics g)
        {
            int theCelCol = 0;
            float num = 1f;
            float num2 = 1f;
            float num3 = mPosX - mX;
            float num4 = mPosY - mY;
            int num5 = mBoard.PixelToGridXKeepOnBoard(mX, mY);
            bool flag = false;
            if (mBoard.mGridSquareType[num5, mRow] == GridSquareType.HighGround)
            {
                flag = true;
            }
            if (mOnHighGround && !flag)
            {
                num4 += Constants.HIGH_GROUND_HEIGHT;
            }
            else if (!mOnHighGround && flag)
            {
                num4 += (float)(-Constants.HIGH_GROUND_HEIGHT);
            }
            if (mBoard.StageIsNight())
            {
                theCelCol = 1;
            }
            if (mProjectileType == ProjectileType.Pea || mProjectileType == ProjectileType.ZombiePea || mProjectileType == ProjectileType.ZombiePeaMindControl)
            {
                num3 += 3f;
            }
            else if (mProjectileType == ProjectileType.Snowpea)
            {
                num3 += -1f;
                num = 1.3f;
            }
            else if (mProjectileType == ProjectileType.Star)
            {
                num3 += 7f;
            }
            else if (mProjectileType == ProjectileType.Cabbage || mProjectileType == ProjectileType.Kernel || mProjectileType == ProjectileType.Butter || mProjectileType == ProjectileType.Melon || mProjectileType == ProjectileType.Wintermelon)
            {
                num3 += 3f;
                num4 += 10f;
                num = 1.6f;
            }
            else
            {
                if (mProjectileType == ProjectileType.Puff)
                {
                    return;
                }
                if (mProjectileType == ProjectileType.Cobbig)
                {
                    num = 1f;
                    num2 = 3f;
                    num3 += 57f;
                }
                else if (mProjectileType == ProjectileType.Fireball)
                {
                    num = 1.4f;
                }
            }
            if (mMotionType == ProjectileMotion.Lobbed)
            {
                float num6 = 200f / (TodCommon.ClampFloat(-mPosZ, 0f, 200f) + 200f);
                num *= num6;
            }
            TodCommon.TodDrawImageCelScaledF(g, AtlasResources.IMAGE_PEA_SHADOWS, num3 * Constants.S, (mShadowY - mPosY + num4) * Constants.S, theCelCol, 0, num * num2, num);
        }

        public void Die()
        {
            mDead = true;
            if (mProjectileType == ProjectileType.Puff || mProjectileType == ProjectileType.Snowpea)
            {
                GlobalMembersAttachment.AttachmentCrossFade(mAttachmentID, "FadeOut");
                GlobalMembersAttachment.AttachmentDetach(ref mAttachmentID);
                return;
            }
            GlobalMembersAttachment.AttachmentDie(ref mAttachmentID);
        }

        public void DoImpact(Zombie theZombie)
        {
            PlayImpactSound(theZombie);
            if (IsSplashDamage(theZombie))
            {
                if (mProjectileType == ProjectileType.Fireball && theZombie != null)
                {
                    theZombie.RemoveColdEffects();
                }
                DoSplashDamage(theZombie);
            }
            else if (theZombie != null)
            {
                ProjectileDefinition projectileDef = GetProjectileDef();
                uint damageFlags = GetDamageFlags(theZombie);
                int aDamage = projectileDef.mDamage;
                ProjectileType projectileType = mProjectileType;
                theZombie.TakeDamage(projectileDef.mDamage, damageFlags);
            }
            int aRenderOrder = mRenderOrder + 1;
            float num = mPosX - mVelX;
            float num2 = mPosY + mPosZ - mVelY - mVelZ;
            ParticleEffect particleEffect = ParticleEffect.None;
            float num3 = mPosX + 12f;
            float num4 = mPosY + 12f;
            if (mProjectileType == ProjectileType.Melon)
            {
                mApp.AddTodParticle(num + 30f, num2 + 30f, aRenderOrder, ParticleEffect.Melonsplash);
            }
            else if (mProjectileType == ProjectileType.Wintermelon)
            {
                mApp.AddTodParticle(num + 30f, num2 + 30f, aRenderOrder, ParticleEffect.Wintermelon);
            }
            else if (mProjectileType == ProjectileType.Cobbig)
            {
                int aRenderOrder2 = Board.MakeRenderOrder(RenderLayer.Ground, mCobTargetRow, 2);
                mApp.AddTodParticle(mPosX + 80f, mPosY + 40f, aRenderOrder2, ParticleEffect.Blastmark);
                mApp.AddTodParticle(mPosX + 80f, mPosY + 40f, aRenderOrder, ParticleEffect.Popcornsplash);
                mApp.PlaySample(Resources.SOUND_DOOMSHROOM);
                mBoard.ShakeBoard(3, -4);
                mApp.Vibrate();
            }
            else if (mProjectileType == ProjectileType.Pea || mProjectileType == ProjectileType.ZombiePeaMindControl)
            {
                num3 -= 15f;
                particleEffect = ParticleEffect.PeaSplat;
            }
            else if (mProjectileType == ProjectileType.Snowpea)
            {
                num3 -= 15f;
                particleEffect = ParticleEffect.SnowpeaSplat;
            }
            else if (mProjectileType == ProjectileType.Fireball)
            {
                if (IsSplashDamage(theZombie))
                {
                    Reanimation reanimation = mApp.AddReanimation(mPosX + 38f, mPosY - 20f, aRenderOrder, ReanimationType.JalapenoFire);
                    reanimation.mAnimTime = 0.25f;
                    reanimation.mAnimRate = 24f;
                    reanimation.OverrideScale(0.7f, 0.4f);
                }
            }
            else if (mProjectileType == ProjectileType.Star)
            {
                particleEffect = ParticleEffect.StarSplat;
            }
            else if (mProjectileType == ProjectileType.Puff)
            {
                num3 -= 20f;
                particleEffect = ParticleEffect.PuffSplat;
            }
            else if (mProjectileType == ProjectileType.Cabbage)
            {
                num3 = num - 38f;
                num4 = num2 + 23f;
                particleEffect = ParticleEffect.CabbageSplat;
            }
            else if (mProjectileType == ProjectileType.Butter)
            {
                num3 = num - 20f;
                num4 = num2 + 63f;
                particleEffect = ParticleEffect.ButterSplat;
                if (theZombie != null)
                {
                    theZombie.ApplyButter();
                }
            }
            if (particleEffect != ParticleEffect.None)
            {
                if (theZombie != null)
                {
                    float num5 = num3 + 52f - theZombie.mX;
                    float num6 = num4 - theZombie.mY;
                    if (theZombie.mZombiePhase == ZombiePhase.SnorkelWalkingInPool || theZombie.mZombiePhase == ZombiePhase.DolphinWalkingInPool)
                    {
                        num6 += 60f;
                    }
                    if (mMotionType == ProjectileMotion.Backwards)
                    {
                        num5 -= 80f;
                    }
                    else if (mPosX > theZombie.mX + 40 && mMotionType != ProjectileMotion.Lobbed)
                    {
                        num5 -= 60f;
                    }
                    num6 = TodCommon.ClampFloat(num6, 20f, 100f);
                    theZombie.AddAttachedParticle((int)num5, (int)num6, particleEffect);
                }
                else
                {
                    mApp.AddTodParticle(num3, num4, aRenderOrder, particleEffect);
                }
            }
            Die();
        }

        public void UpdateMotion()
        {
            if (mAnimTicksPerFrame > 0)
            {
                mAnimCounter = (mAnimCounter + 1) % (mNumFrames * mAnimTicksPerFrame);
                mFrame = mAnimCounter / mAnimTicksPerFrame;
            }
            int row = mRow;
            float posYBasedOnRow = mBoard.GetPosYBasedOnRow(mPosX, mRow);
            if (mMotionType == ProjectileMotion.Lobbed)
            {
                UpdateLobMotion();
            }
            else
            {
                UpdateNormalMotion();
            }
            float posYBasedOnRow2 = mBoard.GetPosYBasedOnRow(mPosX, row);
            float num = posYBasedOnRow2 - posYBasedOnRow;
            if (mProjectileType == ProjectileType.Cobbig)
            {
                num = 0f;
            }
            if (mMotionType == ProjectileMotion.FloatOver)
            {
                mPosY += num;
            }
            if (mMotionType == ProjectileMotion.Lobbed)
            {
                mPosY += num;
                mPosZ -= num;
            }
            mShadowY += num;
            mX = (int)mPosX;
            mY = (int)(mPosY + mPosZ);
        }

        private Zombie FindCollisionMindControlledTarget()
        {
            TRect projectileRect = GetProjectileRect();
            Zombie zombie = null;
            int num = 0;
            for (int i = 0; i < mBoard.mZombies.Count; i++)
            {
                Zombie zombie2 = mBoard.mZombies[i];
                if (!zombie2.mDead && zombie2.mRow - mRow == 0 && zombie2.mMindControlled)
                {
                    TRect zombieRect = zombie2.GetZombieRect();
                    int rectOverlap = GameConstants.GetRectOverlap(projectileRect, zombieRect);
                    if (rectOverlap >= 0 && (zombie != null || zombie2.mX > num))
                    {
                        zombie = zombie2;
                        num = zombie2.mX;
                    }
                }
            }
            return zombie;
        }

        public void CheckForCollision()
        {
            if (mMotionType == ProjectileMotion.Puff && mProjectileAge >= 75)
            {
                Die();
                return;
            }
            if (mPosX > Constants.WIDE_BOARD_WIDTH + 40 || mPosX + mWidth < 0f)
            {
                Die();
                return;
            }
            Zombie zombie;
            if (mMotionType == ProjectileMotion.Homing)
            {
                zombie = mBoard.ZombieTryToGet(mTargetZombieID);
                if (zombie != null && zombie.EffectedByDamage((uint)mDamageRangeFlags))
                {
                    TRect projectileRect = GetProjectileRect();
                    TRect zombieRect = zombie.GetZombieRect();
                    int rectOverlap = GameConstants.GetRectOverlap(projectileRect, zombieRect);
                    if (rectOverlap >= 0 && mPosY > zombieRect.mY && mPosY < zombieRect.mY + zombieRect.mHeight)
                    {
                        DoImpact(zombie);
                    }
                }
                return;
            }
            if (mProjectileType == ProjectileType.Star && (mPosY > 600f || mPosY < 0f))
            {
                Die();
                return;
            }
            if ((mProjectileType == ProjectileType.Pea || mProjectileType == ProjectileType.Star) && mShadowY - mPosY > 90f)
            {
                return;
            }
            if (mMotionType == ProjectileMotion.FloatOver)
            {
                return;
            }
            if (mProjectileType == ProjectileType.ZombiePea)
            {
                Plant plant = FindCollisionTargetPlant();
                if (plant != null)
                {
                    ProjectileDefinition projectileDef = GetProjectileDef();
                    plant.mPlantHealth -= projectileDef.mDamage;
                    plant.mEatenFlashCountdown = Math.Max(plant.mEatenFlashCountdown, 25);
                    mApp.PlayFoley(FoleyType.Splat);
                    mApp.AddTodParticle(mPosX - 3f, mPosY + 17f, mRenderOrder + 1, ParticleEffect.PeaSplat);
                    Die();
                }
                Zombie zombie2 = FindCollisionMindControlledTarget();
                if (zombie2 != null)
                {
                    if (zombie2.mOnHighGround && CantHitHighGround())
                    {
                        return;
                    }
                    DoImpact(zombie2);
                }
                return;
            }
            zombie = FindCollisionTarget();
            if (zombie != null)
            {
                if (zombie.mOnHighGround && CantHitHighGround())
                {
                    return;
                }
                DoImpact(zombie);
            }
        }

        public Zombie FindCollisionTarget()
        {
            if (PeaAboutToHitTorchwood())
            {
                return null;
            }
            TRect projectileRect = GetProjectileRect();
            Zombie zombie = null;
            int num = 0;
            int count = mBoard.mZombies.Count;
            for (int i = 0; i < count; i++)
            {
                Zombie zombie2 = mBoard.mZombies[i];
                if (!zombie2.mDead && (zombie2.mZombiePhase != ZombiePhase.SnorkelWalkingInPool || mPosZ > 45f) && (mProjectileType != ProjectileType.Star || mProjectileAge >= 25 || mVelX < 0f || zombie2.mZombieType != ZombieType.Digger) && zombie2.EffectedByDamage((uint)mDamageRangeFlags))
                {
                    int num2 = zombie2.mRow - mRow;
                    if (zombie2.mZombieType == ZombieType.Boss)
                    {
                        num2 = 0;
                    }
                    if (num2 == 0)
                    {
                        TRect zombieRect = zombie2.GetZombieRect();
                        int rectOverlap = GameConstants.GetRectOverlap(projectileRect, zombieRect);
                        if (rectOverlap >= 0 && (zombie == null || zombie2.mX < num))
                        {
                            zombie = zombie2;
                            num = zombie2.mX;
                        }
                    }
                }
            }
            return zombie;
        }

        public void UpdateLobMotion()
        {
            if (mProjectileType == ProjectileType.Cobbig && mPosZ < -700f)
            {
                mVelZ = 8f;
                mPosX = mCobTargetX;
                mRow = mCobTargetRow;
                int theGridX = mBoard.PixelToGridXKeepOnBoard((int)mCobTargetX, 0);
                mPosY = mBoard.GridToPixelY(theGridX, mCobTargetRow);
                mShadowY = mPosY + 67f;
                mRotation = -1.5707964f;
            }
            mVelZ += 3f * mAccZ;
            if (mApp.mGameMode == GameMode.ChallengeHighGravity)
            {
                mVelZ += 3f * mAccZ;
            }
            mPosX += 3f * mVelX;
            mPosY += 3f * mVelY;
            mPosZ += 3f * mVelZ;
            bool flag = mVelZ < 0f;
            if (flag && (mProjectileType == ProjectileType.Basketball || mProjectileType == ProjectileType.Cobbig))
            {
                return;
            }
            if (mProjectileAge > 20)
            {
                if (flag)
                {
                    return;
                }
                float num = 0f;
                if (mProjectileType == ProjectileType.Butter)
                {
                    num = -32f;
                }
                else if (mProjectileType == ProjectileType.Basketball)
                {
                    num = 60f;
                }
                else if (mProjectileType == ProjectileType.Melon || mProjectileType == ProjectileType.Wintermelon)
                {
                    num = -35f;
                }
                else if (mProjectileType == ProjectileType.Cabbage || mProjectileType == ProjectileType.Kernel)
                {
                    num = -30f;
                }
                else if (mProjectileType == ProjectileType.Cobbig)
                {
                    num = -60f;
                }
                if (mBoard.mPlantRow[mRow] == PlantRowType.Pool)
                {
                    num += 40f;
                }
                if (mPosZ <= num)
                {
                    return;
                }
            }
            Plant plant = null;
            Zombie zombie = null;
            if (mProjectileType == ProjectileType.Basketball || mProjectileType == ProjectileType.ZombiePea)
            {
                plant = FindCollisionTargetPlant();
            }
            else
            {
                zombie = FindCollisionTarget();
            }
            float num2 = 80f;
            if (mProjectileType == ProjectileType.Cobbig)
            {
                num2 = -40f;
            }
            bool flag2 = mPosZ > num2;
            if (zombie == null && plant == null && !flag2)
            {
                return;
            }
            if (plant != null)
            {
                Plant plant2 = mBoard.FindUmbrellaPlant(plant.mPlantCol, plant.mRow);
                if (plant2 != null)
                {
                    if (plant2.mState == PlantState.UmbrellaReflecting)
                    {
                        mApp.PlayFoley(FoleyType.Splat);
                        mApp.AddTodParticle(mPosX + 20f, mPosY + 20f, 400001, ParticleEffect.UmbrellaReflect);
                    }
                    else
                    {
                        if (plant2.mState == PlantState.UmbrellaTriggered)
                        {
                            return;
                        }
                        mApp.PlayFoley(FoleyType.Umbrella);
                        plant2.DoSpecial();
                        return;
                    }
                }
                else
                {
                    ProjectileDefinition projectileDef = GetProjectileDef();
                    plant.mPlantHealth -= projectileDef.mDamage;
                    plant.mEatenFlashCountdown = Math.Max(plant.mEatenFlashCountdown, 25);
                    mApp.PlayFoley(FoleyType.Splat);
                }
                Die();
                return;
            }
            if (mProjectileType == ProjectileType.Cobbig)
            {
                int liveGargantuarCount = mBoard.GetLiveGargantuarCount();
                mBoard.KillAllZombiesInRadius(mRow, (int)mPosX + 80, (int)mPosY + 40, 115, 1, true, mDamageRangeFlags);
                int liveGargantuarCount2 = mBoard.GetLiveGargantuarCount();
                mBoard.mGargantuarsKillsByCornCob += liveGargantuarCount - liveGargantuarCount2;
                if (mBoard.mGargantuarsKillsByCornCob >= 2)
                {
                    mBoard.GrantAchievement(AchievementId.ACHIEVEMENT_POPCORN_PARTY, true);
                }
                DoImpact(null);
                return;
            }
            DoImpact(zombie);
        }

        public void CheckForHighGround()
        {
            float num = mShadowY - mPosY;
            if ((mProjectileType == ProjectileType.Pea || mProjectileType == ProjectileType.Snowpea || mProjectileType == ProjectileType.Fireball || mProjectileType == ProjectileType.Spike || mProjectileType == ProjectileType.Cobbig) && num < 28f)
            {
                DoImpact(null);
                return;
            }
            if (mProjectileType == ProjectileType.Puff && num < 0f)
            {
                DoImpact(null);
                return;
            }
            if (mProjectileType == ProjectileType.Star && num < 23f)
            {
                DoImpact(null);
                return;
            }
            if (!CantHitHighGround())
            {
                return;
            }
            int num2 = mBoard.PixelToGridXKeepOnBoard((int)mPosX + 30, (int)mPosY);
            if (mBoard.mGridSquareType[num2, mRow] == GridSquareType.HighGround)
            {
                DoImpact(null);
            }
        }

        public bool CantHitHighGround()
        {
            return mMotionType != ProjectileMotion.Backwards && mMotionType != ProjectileMotion.Homing && (mProjectileType == ProjectileType.Pea || mProjectileType == ProjectileType.Snowpea || mProjectileType == ProjectileType.Star || mProjectileType == ProjectileType.Puff || mProjectileType == ProjectileType.Fireball) && !mOnHighGround;
        }

        public void DoSplashDamage(Zombie theZombie)
        {
            ProjectileDefinition projectileDef = GetProjectileDef();
            int num = 0;
            int count = mBoard.mZombies.Count;
            for (int i = 0; i < count; i++)
            {
                Zombie zombie = mBoard.mZombies[i];
                if (!zombie.mDead && theZombie != zombie && IsZombieHitBySplash(zombie))
                {
                    num++;
                }
            }
            int damage = projectileDef.mDamage;
            int num2 = 3;
            int num3 = projectileDef.mDamage / num2;
            int num4 = damage * 7;
            if (mProjectileType == ProjectileType.Fireball)
            {
                num4 = damage;
            }
            int num5 = num3 * num;
            if (num5 > num4)
            {
                num3 = Math.Max(1, projectileDef.mDamage * num4 / (num5 * num2));
            }
            for (int j = 0; j < count; j++)
            {
                Zombie zombie = mBoard.mZombies[j];
                if (!zombie.mDead && IsZombieHitBySplash(zombie))
                {
                    uint damageFlags = GetDamageFlags(zombie);
                    if (theZombie == zombie)
                    {
                        zombie.TakeDamage(damage, damageFlags);
                    }
                    else
                    {
                        zombie.TakeDamage(num3, damageFlags);
                    }
                }
            }
        }

        public ProjectileDefinition GetProjectileDef()
        {
            ProjectileDefinition projectileDefinition = GameConstants.gProjectileDefinition[(int)mProjectileType];
            Debug.ASSERT(projectileDefinition.mProjectileType == mProjectileType);
            return projectileDefinition;
        }

        public uint GetDamageFlags(Zombie theZombie)
        {
            uint result = 0U;
            if (IsSplashDamage(theZombie))
            {
                TodCommon.SetBit(ref result, 1, 1);
            }
            else if (mMotionType == ProjectileMotion.Lobbed || mMotionType == ProjectileMotion.Backwards)
            {
                TodCommon.SetBit(ref result, 0, 1);
            }
            else if (mMotionType == ProjectileMotion.Star && mVelX < 0f)
            {
                TodCommon.SetBit(ref result, 0, 1);
            }
            if (mProjectileType == ProjectileType.Snowpea || mProjectileType == ProjectileType.Wintermelon)
            {
                TodCommon.SetBit(ref result, 2, 1);
            }
            return result;
        }

        public TRect GetProjectileRect()
        {
            if (mProjectileType == ProjectileType.Pea || mProjectileType == ProjectileType.Snowpea || mProjectileType == ProjectileType.ZombiePea || mProjectileType == ProjectileType.ZombiePeaMindControl)
            {
                return new TRect(mX - 15, mY, mWidth + 15, mHeight);
            }
            if (mProjectileType == ProjectileType.Cobbig)
            {
                int num = 115;
                return new TRect(mX + mWidth / 2 - num, mY + mHeight / 2 - num, num * 2, num * 2);
            }
            if (mProjectileType == ProjectileType.Melon || mProjectileType == ProjectileType.Wintermelon)
            {
                return new TRect(mX + 20, mY, 60, mHeight);
            }
            if (mProjectileType == ProjectileType.Fireball)
            {
                return new TRect(mX, mY, mWidth - 10, mHeight);
            }
            if (mProjectileType == ProjectileType.Spike)
            {
                return new TRect(mX - 25, mY, mWidth + 25, mHeight);
            }
            return new TRect(mX, mY, mWidth, mHeight);
        }

        public void UpdateNormalMotion()
        {
            if (mMotionType == ProjectileMotion.Backwards)
            {
                mPosX -= 9.99f;
            }
            else if (mMotionType == ProjectileMotion.Homing)
            {
                Zombie zombie = mBoard.ZombieTryToGet(mTargetZombieID);
                if (zombie != null && zombie.EffectedByDamage((uint)mDamageRangeFlags))
                {
                    TRect zombieRect = zombie.GetZombieRect();
                    SexyVector2 lhs = new SexyVector2(zombie.ZombieTargetLeadX(0f), zombieRect.mY + zombieRect.mHeight / 2);
                    SexyVector2 rhs = new SexyVector2(mPosX + mWidth / 2, mPosY + mHeight / 2);
                    SexyVector2 sexyVector = (lhs - rhs).Normalize();
                    SexyVector2 sexyVector2 = new SexyVector2(mVelX, mVelY);
                    sexyVector2.mVector += sexyVector.mVector * (0.001f * mProjectileAge);
                    sexyVector2 = sexyVector2.Normalize();
                    sexyVector2.mVector *= 2f;
                    mVelX = sexyVector2.x;
                    mVelY = sexyVector2.y;
                    mRotation = -(float)Math.Atan2(mVelY, mVelX);
                }
                mPosY += 3f * mVelY;
                mPosX += 3f * mVelX;
                mShadowY += 3f * mVelY;
                mRow = mBoard.PixelToGridYKeepOnBoard((int)mPosX, (int)mPosY);
            }
            else if (mMotionType == ProjectileMotion.Star)
            {
                mPosY += 3f * mVelY;
                mPosX += 3f * mVelX;
                mShadowY += 3f * mVelY;
                if (mVelY != 0f)
                {
                    int row = mBoard.PixelToGridYKeepOnBoard((int)mPosX, (int)mPosY);
                    mRow = row;
                }
            }
            else if (mMotionType == ProjectileMotion.Bee)
            {
                if (mProjectileAge < 60)
                {
                    mPosY -= 1.5f;
                }
                mPosX += 9.99f;
            }
            else if (mMotionType == ProjectileMotion.FloatOver)
            {
                if (mVelZ < 0f)
                {
                    mVelZ += 0.006f;
                    mVelZ = Math.Min(mVelZ, 0f);
                    mPosY += 3f * mVelZ;
                    mRotation = 0.3f + -0.70000005f * mVelZ * 3.1415927f * 0.25f;
                }
                mPosX += 1.2f;
            }
            else if (mMotionType == ProjectileMotion.BeeBackwards)
            {
                if (mProjectileAge < 60)
                {
                    mPosY -= 1.5f;
                }
                mPosX -= 9.99f;
            }
            else if (mMotionType == ProjectileMotion.Threepeater)
            {
                mPosX += 9.99f;
                mPosY += 3f * mVelY;
                mVelY *= 0.97f;
                mVelY *= 0.97f;
                mVelY *= 0.97f;
                mShadowY += 3f * mVelY;
            }
            else
            {
                mPosX += 9.99f;
            }
            if (mApp.mGameMode == GameMode.ChallengeHighGravity)
            {
                if (mMotionType == ProjectileMotion.FloatOver)
                {
                    mVelZ += 0.012f;
                }
                else
                {
                    mVelZ += 0.6f;
                }
                mPosY += 3f * mVelZ;
            }
            CheckForCollision();
            CheckForHighGround();
        }

        public Plant FindCollisionTargetPlant()
        {
            TRect projectileRect = GetProjectileRect();
            int count = mBoard.mPlants.Count;
            for (int i = 0; i < count; i++)
            {
                Plant plant = mBoard.mPlants[i];
                if (!plant.mDead && mRow == plant.mRow && (mProjectileType != ProjectileType.ZombiePea || (plant.mSeedType != SeedType.Puffshroom && plant.mSeedType != SeedType.Sunshroom && plant.mSeedType != SeedType.Potatomine && plant.mSeedType != SeedType.Spikeweed && plant.mSeedType != SeedType.Spikerock && plant.mSeedType != SeedType.Lilypad)))
                {
                    TRect plantRect = plant.GetPlantRect();
                    int rectOverlap = GameConstants.GetRectOverlap(projectileRect, plantRect);
                    if (rectOverlap > 8)
                    {
                        if (mProjectileType == ProjectileType.ZombiePea)
                        {
                            return mBoard.GetTopPlantAt(plant.mPlantCol, plant.mRow, TopPlant.EatingOrder);
                        }
                        return mBoard.GetTopPlantAt(plant.mPlantCol, plant.mRow, TopPlant.CatapultOrder);
                    }
                }
            }
            return null;
        }

        public void ConvertToFireball()
        {
            mProjectileType = ProjectileType.Fireball;
            mApp.PlayFoley(FoleyType.Firepea);
            float num = -25f;
            float num2 = -25f;
            Reanimation reanimation = mApp.AddReanimation(0f, 0f, 0, ReanimationType.FirePea);
            if (mMotionType == ProjectileMotion.Backwards)
            {
                reanimation.OverrideScale(-1f, 1f);
                num += 80f;
            }
            reanimation.SetPosition((mPosX + num) * Constants.S, (mPosY + num2) * Constants.S);
            reanimation.mLoopType = ReanimLoopType.Loop;
            reanimation.mAnimRate = TodCommon.RandRangeFloat(50f, 80f);
            GlobalMembersAttachment.AttachReanim(ref mAttachmentID, reanimation, num, num2);
        }

        public void ConvertToFireball(int aGridX)
        {
            if (mHitTorchwoodGridX == aGridX)
            {
                return;
            }
            mProjectileType = ProjectileType.Fireball;
            mHitTorchwoodGridX = aGridX;
            mApp.PlayFoley(FoleyType.Firepea);
            float num = -25f;
            float num2 = -25f;
            Reanimation reanimation = mApp.AddReanimation(0f, 0f, 0, ReanimationType.FirePea);
            if (mMotionType == ProjectileMotion.Backwards)
            {
                reanimation.OverrideScale(-1f, 1f);
                num += 80f;
            }
            reanimation.SetPosition((mPosX + num) * Constants.S, (mPosY + num2) * Constants.S);
            reanimation.mLoopType = ReanimLoopType.Loop;
            reanimation.mAnimRate = TodCommon.RandRangeFloat(50f, 80f);
            GlobalMembersAttachment.AttachReanim(ref mAttachmentID, reanimation, num, num2);
        }

        public void ConvertToPea(int aGridX)
        {
            if (mHitTorchwoodGridX == aGridX)
            {
                return;
            }
            GlobalMembersAttachment.AttachmentDie(ref mAttachmentID);
            mProjectileType = ProjectileType.Pea;
            mHitTorchwoodGridX = aGridX;
            mApp.PlayFoley(FoleyType.Throw);
        }

        public bool IsSplashDamage(Zombie theZombie)
        {
            return (mProjectileType != ProjectileType.Fireball || theZombie == null || !theZombie.IsFireResistant()) && (mProjectileType == ProjectileType.Melon || mProjectileType == ProjectileType.Wintermelon || mProjectileType == ProjectileType.Fireball);
        }

        public void PlayImpactSound(Zombie theZombie)
        {
            bool flag = true;
            bool flag2 = true;
            if (mProjectileType == ProjectileType.Kernel)
            {
                mApp.PlayFoley(FoleyType.KernelSplat);
                flag = false;
                flag2 = false;
            }
            else if (mProjectileType == ProjectileType.Butter)
            {
                mApp.PlayFoley(FoleyType.Butter);
                flag2 = false;
            }
            else if (mProjectileType == ProjectileType.Fireball && IsSplashDamage(theZombie))
            {
                mApp.PlayFoley(FoleyType.Ignite);
                flag = false;
                flag2 = false;
            }
            else if (mProjectileType == ProjectileType.Melon || mProjectileType == ProjectileType.Wintermelon)
            {
                mApp.PlayFoley(FoleyType.Melonimpact);
                flag2 = false;
            }
            if (flag)
            {
                if (theZombie != null && theZombie.mHelmType == HelmType.Pail)
                {
                    mApp.PlayFoley(FoleyType.ShieldHit);
                    flag2 = false;
                }
                else if (theZombie != null && theZombie.mHelmType == HelmType.TrafficCone)
                {
                    mApp.PlayFoley(FoleyType.PlasticHit);
                }
                else if (theZombie != null && theZombie.mHelmType == HelmType.Digger)
                {
                    mApp.PlayFoley(FoleyType.PlasticHit);
                }
                else if (theZombie != null && theZombie.mHelmType == HelmType.Football)
                {
                    mApp.PlayFoley(FoleyType.PlasticHit);
                }
            }
            if (flag2)
            {
                mApp.PlayFoley(FoleyType.Splat);
            }
        }

        public bool IsZombieHitBySplash(Zombie theZombie)
        {
            TRect projectileRect = GetProjectileRect();
            if (mProjectileType == ProjectileType.Fireball)
            {
                projectileRect.mWidth = 100;
            }
            int num = theZombie.mRow - mRow;
            TRect zombieRect = theZombie.GetZombieRect();
            if (theZombie.IsFireResistant() && mProjectileType == ProjectileType.Fireball)
            {
                return false;
            }
            if (theZombie.mZombieType == ZombieType.Boss)
            {
                num = 0;
            }
            if (mProjectileType == ProjectileType.Fireball)
            {
                if (num != 0)
                {
                    return false;
                }
            }
            else if (num > 1 || num < -1)
            {
                return false;
            }
            if (!theZombie.EffectedByDamage((uint)mDamageRangeFlags))
            {
                return false;
            }
            int rectOverlap = GameConstants.GetRectOverlap(projectileRect, zombieRect);
            return rectOverlap >= 0;
        }

        public bool PeaAboutToHitTorchwood()
        {
            if (mMotionType != ProjectileMotion.Straight)
            {
                return false;
            }
            if (mProjectileType != ProjectileType.Pea && mProjectileType != ProjectileType.Snowpea && mProjectileType != ProjectileType.ZombiePeaMindControl)
            {
                return false;
            }
            int count = mBoard.mPlants.Count;
            for (int i = 0; i < count; i++)
            {
                Plant plant = mBoard.mPlants[i];
                if (!plant.mDead && plant.mSeedType == SeedType.Torchwood && plant.mRow == mRow && !plant.NotOnGround() && mHitTorchwoodGridX != plant.mPlantCol)
                {
                    TRect plantAttackRect = plant.GetPlantAttackRect(PlantWeapon.Primary);
                    TRect projectileRect = GetProjectileRect();
                    projectileRect.mX += 40;
                    int rectOverlap = GameConstants.GetRectOverlap(plantAttackRect, projectileRect);
                    if (rectOverlap > 10)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public int mFrame;

        public int mNumFrames;

        public int mAnimCounter;

        public float mPosX;

        public float mPosY;

        public float mPosZ;

        public float mVelX;

        public float mVelY;

        public float mVelZ;

        public float mAccZ;

        public float mShadowY;

        public bool mDead;

        public int mAnimTicksPerFrame;

        public ProjectileMotion mMotionType;

        public ProjectileType mProjectileType;

        public int mProjectileAge;

        public int mClickBackoffCounter;

        public float mRotation;

        public float mRotationSpeed;

        public bool mOnHighGround;

        public int mDamageRangeFlags;

        public int mHitTorchwoodGridX;

        public Attachment mAttachmentID;

        public float mCobTargetX;

        public int mCobTargetRow;

        public Zombie mTargetZombieID;

        private int mTargetZombieIDSaved;

        public int mLastPortalX;

        private static Stack<Projectile> unusedObjects = new Stack<Projectile>(200);
    }
}
