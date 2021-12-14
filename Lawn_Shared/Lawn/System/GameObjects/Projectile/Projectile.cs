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
			if (mProjectileType == ProjectileType.PROJECTILE_FIREBALL)
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
			mMotionType = ProjectileMotion.MOTION_STRAIGHT;
			mProjectileType = ProjectileType.PROJECTILE_PEA;
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
			mPosX = (float)theX;
			mPosY = (float)theY;
			mPosZ = 0f;
			mVelX = 0f;
			mVelY = 0f;
			mVelZ = 0f;
			mAccZ = 0f;
			mShadowY = (float)mBoard.GridToPixelY(num, theRow) + 67f;
			mMotionType = ProjectileMotion.MOTION_STRAIGHT;
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
			if (mBoard.mGridSquareType[num, mRow] == GridSquareType.GRIDSQUARE_HIGH_GROUND)
			{
				mOnHighGround = true;
			}
			else
			{
				mOnHighGround = false;
			}
			if (mBoard.StageHasRoof() && (float)theX < 480f)
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
			if (mProjectileType == ProjectileType.PROJECTILE_CABBAGE || mProjectileType == ProjectileType.PROJECTILE_BUTTER)
			{
				mRotation = -0.87964594f;
				mRotationSpeed = TodCommon.RandRangeFloat(-0.08f, -0.02f);
			}
			else if (mProjectileType == ProjectileType.PROJECTILE_MELON || mProjectileType == ProjectileType.PROJECTILE_WINTERMELON)
			{
				mRotation = -1.2566371f;
				mRotationSpeed = TodCommon.RandRangeFloat(-0.08f, -0.02f);
			}
			else if (mProjectileType == ProjectileType.PROJECTILE_KERNEL)
			{
				mRotation = 0f;
				mRotationSpeed = TodCommon.RandRangeFloat(-0.2f, -0.08f);
			}
			else if (mProjectileType == ProjectileType.PROJECTILE_SNOWPEA)
			{
				float num2 = 8f;
				float num3 = 13f;
				TodParticleSystem theParticleSystem = mApp.AddTodParticle(mPosX + num2, mPosY + num3, 400000, ParticleEffect.PARTICLE_SNOWPEA_TRAIL);
				GlobalMembersAttachment.AttachParticle(ref mAttachmentID, theParticleSystem, num2, num3);
			}
			else if (mProjectileType == ProjectileType.PROJECTILE_FIREBALL)
			{
				Debug.ASSERT(false);
			}
			else if (mProjectileType == ProjectileType.PROJECTILE_COBBIG)
			{
				mWidth = (int)((float)AtlasResources.IMAGE_REANIM_COBCANNON_COB.GetWidth() * Constants.IS);
				mHeight = (int)((float)AtlasResources.IMAGE_REANIM_COBCANNON_COB.GetHeight() * Constants.IS);
				mRotation = 1.5707964f;
			}
			else if (mProjectileType == ProjectileType.PROJECTILE_PUFF)
			{
				float num4 = 13f;
				float num5 = 13f;
				TodParticleSystem theParticleSystem2 = mApp.AddTodParticle(mPosX + num4, mPosY + num5, 400000, ParticleEffect.PARTICLE_PUFFSHROOM_TRAIL);
				GlobalMembersAttachment.AttachParticle(ref mAttachmentID, theParticleSystem2, num4, num5);
			}
			else if (mProjectileType != ProjectileType.PROJECTILE_BUTTER)
			{
				if (mProjectileType == ProjectileType.PROJECTILE_BASKETBALL)
				{
					mRotation = TodCommon.RandRangeFloat(0f, 6.2831855f);
					mRotationSpeed = TodCommon.RandRangeFloat(0.05f, 0.1f);
				}
				else if (mProjectileType == ProjectileType.PROJECTILE_STAR)
				{
					mShadowY += 15f;
					mRotationSpeed = TodCommon.RandRangeFloat(0.05f, 0.1f);
					if (RandomNumbers.NextNumber(2) == 0)
					{
						mRotationSpeed = -mRotationSpeed;
					}
				}
				else if (mProjectileType == ProjectileType.PROJECTILE_ZOMBIE_PEA_MIND_CONTROL)
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
			if (mApp.mGameScene != GameScenes.SCENE_PLAYING && !mBoard.mCutScene.ShouldRunUpsellBoard())
			{
				return;
			}
			int num = 20;
			if (mProjectileType == ProjectileType.PROJECTILE_PEA || mProjectileType == ProjectileType.PROJECTILE_SNOWPEA || mProjectileType == ProjectileType.PROJECTILE_CABBAGE || mProjectileType == ProjectileType.PROJECTILE_MELON || mProjectileType == ProjectileType.PROJECTILE_WINTERMELON || mProjectileType == ProjectileType.PROJECTILE_KERNEL || mProjectileType == ProjectileType.PROJECTILE_BUTTER || mProjectileType == ProjectileType.PROJECTILE_COBBIG || mProjectileType == ProjectileType.PROJECTILE_ZOMBIE_PEA || mProjectileType == ProjectileType.PROJECTILE_SPIKE || mProjectileType == ProjectileType.PROJECTILE_ZOMBIE_PEA_MIND_CONTROL)
			{
				num = 0;
			}
			if (mProjectileAge > num)
			{
				mRenderOrder = Board.MakeRenderOrder(RenderLayer.RENDER_LAYER_PROJECTILE, mRow, 0);
			}
			if (mApp.IsFinalBossLevel())
			{
				mRenderOrder = Board.MakeRenderOrder(RenderLayer.RENDER_LAYER_PROJECTILE, 5, 0);
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
			if (mProjectileType == ProjectileType.PROJECTILE_COBBIG)
			{
				image = AtlasResources.IMAGE_REANIM_COBCANNON_COB;
				num = 0.9f;
			}
			else if (mProjectileType == ProjectileType.PROJECTILE_PEA || mProjectileType == ProjectileType.PROJECTILE_ZOMBIE_PEA || mProjectileType == ProjectileType.PROJECTILE_ZOMBIE_PEA_MIND_CONTROL)
			{
				image = AtlasResources.IMAGE_PROJECTILEPEA;
				if (mProjectileType == ProjectileType.PROJECTILE_ZOMBIE_PEA_MIND_CONTROL)
				{
					g.SetColorizeImages(true);
					g.SetColor(GameConstants.ZOMBIE_MINDCONTROLLED_COLOR);
				}
			}
			else if (mProjectileType == ProjectileType.PROJECTILE_SNOWPEA)
			{
				image = AtlasResources.IMAGE_PROJECTILESNOWPEA;
			}
			else if (mProjectileType == ProjectileType.PROJECTILE_FIREBALL)
			{
				image = null;
			}
			else if (mProjectileType == ProjectileType.PROJECTILE_SPIKE)
			{
				image = AtlasResources.IMAGE_PROJECTILECACTUS;
			}
			else if (mProjectileType == ProjectileType.PROJECTILE_STAR)
			{
				image = AtlasResources.IMAGE_PROJECTILE_STAR;
			}
			else if (mProjectileType == ProjectileType.PROJECTILE_PUFF)
			{
				image = AtlasResources.IMAGE_PUFFSHROOM_PUFF1;
				num = TodCommon.TodAnimateCurveFloat(0, 30, mProjectileAge, 0.3f, 1f, TodCurves.CURVE_LINEAR);
			}
			else if (mProjectileType == ProjectileType.PROJECTILE_BASKETBALL)
			{
				image = AtlasResources.IMAGE_REANIM_ZOMBIE_CATAPULT_BASKETBALL;
				num = 1.1f;
			}
			else if (mProjectileType == ProjectileType.PROJECTILE_CABBAGE)
			{
				image = AtlasResources.IMAGE_REANIM_CABBAGEPULT_CABBAGE;
				num = 1f;
			}
			else if (mProjectileType == ProjectileType.PROJECTILE_KERNEL)
			{
				image = AtlasResources.IMAGE_REANIM_CORNPULT_KERNAL;
				num = 0.95f;
			}
			else if (mProjectileType == ProjectileType.PROJECTILE_BUTTER)
			{
				image = AtlasResources.IMAGE_REANIM_CORNPULT_BUTTER;
				num = 0.8f;
			}
			else if (mProjectileType == ProjectileType.PROJECTILE_MELON)
			{
				image = AtlasResources.IMAGE_REANIM_MELONPULT_MELON;
				num = 1f;
			}
			else if (mProjectileType == ProjectileType.PROJECTILE_WINTERMELON)
			{
				image = AtlasResources.IMAGE_REANIM_WINTERMELON_PROJECTILE;
				num = 1f;
			}
			else
			{
				Debug.ASSERT(false);
			}
			bool mirror = false;
			if (mMotionType == ProjectileMotion.MOTION_BEE_BACKWARDS)
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
					float num2 = mPosX + (float)celWidth * 0.5f;
					float num3 = mPosZ + mPosY + (float)celHeight * 0.5f;
					SexyTransform2D sexyTransform2D = default(SexyTransform2D);
					TodCommon.TodScaleRotateTransformMatrix(ref sexyTransform2D.mMatrix, num2 * Constants.S + (float)mBoard.mX, num3 * Constants.S + (float)mBoard.mY, mRotation, num, num);
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
			if (mProjectileType == ProjectileType.PROJECTILE_ZOMBIE_PEA_MIND_CONTROL)
			{
				g.SetColorizeImages(false);
			}
		}

		public void DrawShadow(Graphics g)
		{
			int theCelCol = 0;
			float num = 1f;
			float num2 = 1f;
			float num3 = mPosX - (float)mX;
			float num4 = mPosY - (float)mY;
			int num5 = mBoard.PixelToGridXKeepOnBoard(mX, mY);
			bool flag = false;
			if (mBoard.mGridSquareType[num5, mRow] == GridSquareType.GRIDSQUARE_HIGH_GROUND)
			{
				flag = true;
			}
			if (mOnHighGround && !flag)
			{
				num4 += (float)Constants.HIGH_GROUND_HEIGHT;
			}
			else if (!mOnHighGround && flag)
			{
				num4 += (float)(-(float)Constants.HIGH_GROUND_HEIGHT);
			}
			if (mBoard.StageIsNight())
			{
				theCelCol = 1;
			}
			if (mProjectileType == ProjectileType.PROJECTILE_PEA || mProjectileType == ProjectileType.PROJECTILE_ZOMBIE_PEA || mProjectileType == ProjectileType.PROJECTILE_ZOMBIE_PEA_MIND_CONTROL)
			{
				num3 += 3f;
			}
			else if (mProjectileType == ProjectileType.PROJECTILE_SNOWPEA)
			{
				num3 += -1f;
				num = 1.3f;
			}
			else if (mProjectileType == ProjectileType.PROJECTILE_STAR)
			{
				num3 += 7f;
			}
			else if (mProjectileType == ProjectileType.PROJECTILE_CABBAGE || mProjectileType == ProjectileType.PROJECTILE_KERNEL || mProjectileType == ProjectileType.PROJECTILE_BUTTER || mProjectileType == ProjectileType.PROJECTILE_MELON || mProjectileType == ProjectileType.PROJECTILE_WINTERMELON)
			{
				num3 += 3f;
				num4 += 10f;
				num = 1.6f;
			}
			else
			{
				if (mProjectileType == ProjectileType.PROJECTILE_PUFF)
				{
					return;
				}
				if (mProjectileType == ProjectileType.PROJECTILE_COBBIG)
				{
					num = 1f;
					num2 = 3f;
					num3 += 57f;
				}
				else if (mProjectileType == ProjectileType.PROJECTILE_FIREBALL)
				{
					num = 1.4f;
				}
			}
			if (mMotionType == ProjectileMotion.MOTION_LOBBED)
			{
				float num6 = 200f / (TodCommon.ClampFloat(-mPosZ, 0f, 200f) + 200f);
				num *= num6;
			}
			TodCommon.TodDrawImageCelScaledF(g, AtlasResources.IMAGE_PEA_SHADOWS, num3 * Constants.S, (mShadowY - mPosY + num4) * Constants.S, theCelCol, 0, num * num2, num);
		}

		public void Die()
		{
			mDead = true;
			if (mProjectileType == ProjectileType.PROJECTILE_PUFF || mProjectileType == ProjectileType.PROJECTILE_SNOWPEA)
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
				if (mProjectileType == ProjectileType.PROJECTILE_FIREBALL && theZombie != null)
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
			ParticleEffect particleEffect = ParticleEffect.PARTICLE_NONE;
			float num3 = mPosX + 12f;
			float num4 = mPosY + 12f;
			if (mProjectileType == ProjectileType.PROJECTILE_MELON)
			{
				mApp.AddTodParticle(num + 30f, num2 + 30f, aRenderOrder, ParticleEffect.PARTICLE_MELONSPLASH);
			}
			else if (mProjectileType == ProjectileType.PROJECTILE_WINTERMELON)
			{
				mApp.AddTodParticle(num + 30f, num2 + 30f, aRenderOrder, ParticleEffect.PARTICLE_WINTERMELON);
			}
			else if (mProjectileType == ProjectileType.PROJECTILE_COBBIG)
			{
				int aRenderOrder2 = Board.MakeRenderOrder(RenderLayer.RENDER_LAYER_GROUND, mCobTargetRow, 2);
				mApp.AddTodParticle(mPosX + 80f, mPosY + 40f, aRenderOrder2, ParticleEffect.PARTICLE_BLASTMARK);
				mApp.AddTodParticle(mPosX + 80f, mPosY + 40f, aRenderOrder, ParticleEffect.PARTICLE_POPCORNSPLASH);
				mApp.PlaySample(Resources.SOUND_DOOMSHROOM);
				mBoard.ShakeBoard(3, -4);
				mApp.Vibrate();
			}
			else if (mProjectileType == ProjectileType.PROJECTILE_PEA || mProjectileType == ProjectileType.PROJECTILE_ZOMBIE_PEA_MIND_CONTROL)
			{
				num3 -= 15f;
				particleEffect = ParticleEffect.PARTICLE_PEA_SPLAT;
			}
			else if (mProjectileType == ProjectileType.PROJECTILE_SNOWPEA)
			{
				num3 -= 15f;
				particleEffect = ParticleEffect.PARTICLE_SNOWPEA_SPLAT;
			}
			else if (mProjectileType == ProjectileType.PROJECTILE_FIREBALL)
			{
				if (IsSplashDamage(theZombie))
				{
					Reanimation reanimation = mApp.AddReanimation(mPosX + 38f, mPosY - 20f, aRenderOrder, ReanimationType.REANIM_JALAPENO_FIRE);
					reanimation.mAnimTime = 0.25f;
					reanimation.mAnimRate = 24f;
					reanimation.OverrideScale(0.7f, 0.4f);
				}
			}
			else if (mProjectileType == ProjectileType.PROJECTILE_STAR)
			{
				particleEffect = ParticleEffect.PARTICLE_STAR_SPLAT;
			}
			else if (mProjectileType == ProjectileType.PROJECTILE_PUFF)
			{
				num3 -= 20f;
				particleEffect = ParticleEffect.PARTICLE_PUFF_SPLAT;
			}
			else if (mProjectileType == ProjectileType.PROJECTILE_CABBAGE)
			{
				num3 = num - 38f;
				num4 = num2 + 23f;
				particleEffect = ParticleEffect.PARTICLE_CABBAGE_SPLAT;
			}
			else if (mProjectileType == ProjectileType.PROJECTILE_BUTTER)
			{
				num3 = num - 20f;
				num4 = num2 + 63f;
				particleEffect = ParticleEffect.PARTICLE_BUTTER_SPLAT;
				if (theZombie != null)
				{
					theZombie.ApplyButter();
				}
			}
			if (particleEffect != ParticleEffect.PARTICLE_NONE)
			{
				if (theZombie != null)
				{
					float num5 = num3 + 52f - (float)theZombie.mX;
					float num6 = num4 - (float)theZombie.mY;
					if (theZombie.mZombiePhase == ZombiePhase.PHASE_SNORKEL_WALKING_IN_POOL || theZombie.mZombiePhase == ZombiePhase.PHASE_DOLPHIN_WALKING_IN_POOL)
					{
						num6 += 60f;
					}
					if (mMotionType == ProjectileMotion.MOTION_BACKWARDS)
					{
						num5 -= 80f;
					}
					else if (mPosX > (float)(theZombie.mX + 40) && mMotionType != ProjectileMotion.MOTION_LOBBED)
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
			if (mMotionType == ProjectileMotion.MOTION_LOBBED)
			{
				UpdateLobMotion();
			}
			else
			{
				UpdateNormalMotion();
			}
			float posYBasedOnRow2 = mBoard.GetPosYBasedOnRow(mPosX, row);
			float num = posYBasedOnRow2 - posYBasedOnRow;
			if (mProjectileType == ProjectileType.PROJECTILE_COBBIG)
			{
				num = 0f;
			}
			if (mMotionType == ProjectileMotion.MOTION_FLOAT_OVER)
			{
				mPosY += num;
			}
			if (mMotionType == ProjectileMotion.MOTION_LOBBED)
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
			if (mMotionType == ProjectileMotion.MOTION_PUFF && mProjectileAge >= 75)
			{
				Die();
				return;
			}
			if (mPosX > (float)(Constants.WIDE_BOARD_WIDTH + 40) || mPosX + (float)mWidth < 0f)
			{
				Die();
				return;
			}
			Zombie zombie;
			if (mMotionType == ProjectileMotion.MOTION_HOMING)
			{
				zombie = mBoard.ZombieTryToGet(mTargetZombieID);
				if (zombie != null && zombie.EffectedByDamage((uint)mDamageRangeFlags))
				{
					TRect projectileRect = GetProjectileRect();
					TRect zombieRect = zombie.GetZombieRect();
					int rectOverlap = GameConstants.GetRectOverlap(projectileRect, zombieRect);
					if (rectOverlap >= 0 && mPosY > (float)zombieRect.mY && mPosY < (float)(zombieRect.mY + zombieRect.mHeight))
					{
						DoImpact(zombie);
					}
				}
				return;
			}
			if (mProjectileType == ProjectileType.PROJECTILE_STAR && (mPosY > 600f || mPosY < 0f))
			{
				Die();
				return;
			}
			if ((mProjectileType == ProjectileType.PROJECTILE_PEA || mProjectileType == ProjectileType.PROJECTILE_STAR) && mShadowY - mPosY > 90f)
			{
				return;
			}
			if (mMotionType == ProjectileMotion.MOTION_FLOAT_OVER)
			{
				return;
			}
			if (mProjectileType == ProjectileType.PROJECTILE_ZOMBIE_PEA)
			{
				Plant plant = FindCollisionTargetPlant();
				if (plant != null)
				{
					ProjectileDefinition projectileDef = GetProjectileDef();
					plant.mPlantHealth -= projectileDef.mDamage;
					plant.mEatenFlashCountdown = Math.Max(plant.mEatenFlashCountdown, 25);
					mApp.PlayFoley(FoleyType.FOLEY_SPLAT);
					mApp.AddTodParticle(mPosX - 3f, mPosY + 17f, mRenderOrder + 1, ParticleEffect.PARTICLE_PEA_SPLAT);
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
				if (!zombie2.mDead && (zombie2.mZombiePhase != ZombiePhase.PHASE_SNORKEL_WALKING_IN_POOL || mPosZ > 45f) && (mProjectileType != ProjectileType.PROJECTILE_STAR || mProjectileAge >= 25 || mVelX < 0f || zombie2.mZombieType != ZombieType.ZOMBIE_DIGGER) && zombie2.EffectedByDamage((uint)mDamageRangeFlags))
				{
					int num2 = zombie2.mRow - mRow;
					if (zombie2.mZombieType == ZombieType.ZOMBIE_BOSS)
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
			if (mProjectileType == ProjectileType.PROJECTILE_COBBIG && mPosZ < -700f)
			{
				mVelZ = 8f;
				mPosX = mCobTargetX;
				mRow = mCobTargetRow;
				int theGridX = mBoard.PixelToGridXKeepOnBoard((int)mCobTargetX, 0);
				mPosY = (float)mBoard.GridToPixelY(theGridX, mCobTargetRow);
				mShadowY = mPosY + 67f;
				mRotation = -1.5707964f;
			}
			mVelZ += 3f * mAccZ;
			if (mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_HIGH_GRAVITY)
			{
				mVelZ += 3f * mAccZ;
			}
			mPosX += 3f * mVelX;
			mPosY += 3f * mVelY;
			mPosZ += 3f * mVelZ;
			bool flag = mVelZ < 0f;
			if (flag && (mProjectileType == ProjectileType.PROJECTILE_BASKETBALL || mProjectileType == ProjectileType.PROJECTILE_COBBIG))
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
				if (mProjectileType == ProjectileType.PROJECTILE_BUTTER)
				{
					num = -32f;
				}
				else if (mProjectileType == ProjectileType.PROJECTILE_BASKETBALL)
				{
					num = 60f;
				}
				else if (mProjectileType == ProjectileType.PROJECTILE_MELON || mProjectileType == ProjectileType.PROJECTILE_WINTERMELON)
				{
					num = -35f;
				}
				else if (mProjectileType == ProjectileType.PROJECTILE_CABBAGE || mProjectileType == ProjectileType.PROJECTILE_KERNEL)
				{
					num = -30f;
				}
				else if (mProjectileType == ProjectileType.PROJECTILE_COBBIG)
				{
					num = -60f;
				}
				if (mBoard.mPlantRow[mRow] == PlantRowType.PLANTROW_POOL)
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
			if (mProjectileType == ProjectileType.PROJECTILE_BASKETBALL || mProjectileType == ProjectileType.PROJECTILE_ZOMBIE_PEA)
			{
				plant = FindCollisionTargetPlant();
			}
			else
			{
				zombie = FindCollisionTarget();
			}
			float num2 = 80f;
			if (mProjectileType == ProjectileType.PROJECTILE_COBBIG)
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
					if (plant2.mState == PlantState.STATE_UMBRELLA_REFLECTING)
					{
						mApp.PlayFoley(FoleyType.FOLEY_SPLAT);
						mApp.AddTodParticle(mPosX + 20f, mPosY + 20f, 400001, ParticleEffect.PARTICLE_UMBRELLA_REFLECT);
					}
					else
					{
						if (plant2.mState == PlantState.STATE_UMBRELLA_TRIGGERED)
						{
							return;
						}
						mApp.PlayFoley(FoleyType.FOLEY_UMBRELLA);
						plant2.DoSpecial();
						return;
					}
				}
				else
				{
					ProjectileDefinition projectileDef = GetProjectileDef();
					plant.mPlantHealth -= projectileDef.mDamage;
					plant.mEatenFlashCountdown = Math.Max(plant.mEatenFlashCountdown, 25);
					mApp.PlayFoley(FoleyType.FOLEY_SPLAT);
				}
				Die();
				return;
			}
			if (mProjectileType == ProjectileType.PROJECTILE_COBBIG)
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
			if ((mProjectileType == ProjectileType.PROJECTILE_PEA || mProjectileType == ProjectileType.PROJECTILE_SNOWPEA || mProjectileType == ProjectileType.PROJECTILE_FIREBALL || mProjectileType == ProjectileType.PROJECTILE_SPIKE || mProjectileType == ProjectileType.PROJECTILE_COBBIG) && num < 28f)
			{
				DoImpact(null);
				return;
			}
			if (mProjectileType == ProjectileType.PROJECTILE_PUFF && num < 0f)
			{
				DoImpact(null);
				return;
			}
			if (mProjectileType == ProjectileType.PROJECTILE_STAR && num < 23f)
			{
				DoImpact(null);
				return;
			}
			if (!CantHitHighGround())
			{
				return;
			}
			int num2 = mBoard.PixelToGridXKeepOnBoard((int)mPosX + 30, (int)mPosY);
			if (mBoard.mGridSquareType[num2, mRow] == GridSquareType.GRIDSQUARE_HIGH_GROUND)
			{
				DoImpact(null);
			}
		}

		public bool CantHitHighGround()
		{
			return mMotionType != ProjectileMotion.MOTION_BACKWARDS && mMotionType != ProjectileMotion.MOTION_HOMING && (mProjectileType == ProjectileType.PROJECTILE_PEA || mProjectileType == ProjectileType.PROJECTILE_SNOWPEA || mProjectileType == ProjectileType.PROJECTILE_STAR || mProjectileType == ProjectileType.PROJECTILE_PUFF || mProjectileType == ProjectileType.PROJECTILE_FIREBALL) && !mOnHighGround;
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
			if (mProjectileType == ProjectileType.PROJECTILE_FIREBALL)
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
			else if (mMotionType == ProjectileMotion.MOTION_LOBBED || mMotionType == ProjectileMotion.MOTION_BACKWARDS)
			{
				TodCommon.SetBit(ref result, 0, 1);
			}
			else if (mMotionType == ProjectileMotion.MOTION_STAR && mVelX < 0f)
			{
				TodCommon.SetBit(ref result, 0, 1);
			}
			if (mProjectileType == ProjectileType.PROJECTILE_SNOWPEA || mProjectileType == ProjectileType.PROJECTILE_WINTERMELON)
			{
				TodCommon.SetBit(ref result, 2, 1);
			}
			return result;
		}

		public TRect GetProjectileRect()
		{
			if (mProjectileType == ProjectileType.PROJECTILE_PEA || mProjectileType == ProjectileType.PROJECTILE_SNOWPEA || mProjectileType == ProjectileType.PROJECTILE_ZOMBIE_PEA || mProjectileType == ProjectileType.PROJECTILE_ZOMBIE_PEA_MIND_CONTROL)
			{
				return new TRect(mX - 15, mY, mWidth + 15, mHeight);
			}
			if (mProjectileType == ProjectileType.PROJECTILE_COBBIG)
			{
				int num = 115;
				return new TRect(mX + mWidth / 2 - num, mY + mHeight / 2 - num, num * 2, num * 2);
			}
			if (mProjectileType == ProjectileType.PROJECTILE_MELON || mProjectileType == ProjectileType.PROJECTILE_WINTERMELON)
			{
				return new TRect(mX + 20, mY, 60, mHeight);
			}
			if (mProjectileType == ProjectileType.PROJECTILE_FIREBALL)
			{
				return new TRect(mX, mY, mWidth - 10, mHeight);
			}
			if (mProjectileType == ProjectileType.PROJECTILE_SPIKE)
			{
				return new TRect(mX - 25, mY, mWidth + 25, mHeight);
			}
			return new TRect(mX, mY, mWidth, mHeight);
		}

		public void UpdateNormalMotion()
		{
			if (mMotionType == ProjectileMotion.MOTION_BACKWARDS)
			{
				mPosX -= 9.99f;
			}
			else if (mMotionType == ProjectileMotion.MOTION_HOMING)
			{
				Zombie zombie = mBoard.ZombieTryToGet(mTargetZombieID);
				if (zombie != null && zombie.EffectedByDamage((uint)mDamageRangeFlags))
				{
					TRect zombieRect = zombie.GetZombieRect();
					SexyVector2 lhs = new SexyVector2(zombie.ZombieTargetLeadX(0f), (float)(zombieRect.mY + zombieRect.mHeight / 2));
					SexyVector2 rhs = new SexyVector2(mPosX + (float)(mWidth / 2), mPosY + (float)(mHeight / 2));
					SexyVector2 sexyVector = (lhs - rhs).Normalize();
					SexyVector2 sexyVector2 = new SexyVector2(mVelX, mVelY);
					sexyVector2.mVector += sexyVector.mVector * (0.001f * (float)mProjectileAge);
					sexyVector2 = sexyVector2.Normalize();
					sexyVector2.mVector *= 2f;
					mVelX = sexyVector2.x;
					mVelY = sexyVector2.y;
					mRotation = (float)(-(float)Math.Atan2((double)mVelY, (double)mVelX));
				}
				mPosY += 3f * mVelY;
				mPosX += 3f * mVelX;
				mShadowY += 3f * mVelY;
				mRow = mBoard.PixelToGridYKeepOnBoard((int)mPosX, (int)mPosY);
			}
			else if (mMotionType == ProjectileMotion.MOTION_STAR)
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
			else if (mMotionType == ProjectileMotion.MOTION_BEE)
			{
				if (mProjectileAge < 60)
				{
					mPosY -= 1.5f;
				}
				mPosX += 9.99f;
			}
			else if (mMotionType == ProjectileMotion.MOTION_FLOAT_OVER)
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
			else if (mMotionType == ProjectileMotion.MOTION_BEE_BACKWARDS)
			{
				if (mProjectileAge < 60)
				{
					mPosY -= 1.5f;
				}
				mPosX -= 9.99f;
			}
			else if (mMotionType == ProjectileMotion.MOTION_THREEPEATER)
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
			if (mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_HIGH_GRAVITY)
			{
				if (mMotionType == ProjectileMotion.MOTION_FLOAT_OVER)
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
				if (!plant.mDead && mRow == plant.mRow && (mProjectileType != ProjectileType.PROJECTILE_ZOMBIE_PEA || (plant.mSeedType != SeedType.SEED_PUFFSHROOM && plant.mSeedType != SeedType.SEED_SUNSHROOM && plant.mSeedType != SeedType.SEED_POTATOMINE && plant.mSeedType != SeedType.SEED_SPIKEWEED && plant.mSeedType != SeedType.SEED_SPIKEROCK && plant.mSeedType != SeedType.SEED_LILYPAD)))
				{
					TRect plantRect = plant.GetPlantRect();
					int rectOverlap = GameConstants.GetRectOverlap(projectileRect, plantRect);
					if (rectOverlap > 8)
					{
						if (mProjectileType == ProjectileType.PROJECTILE_ZOMBIE_PEA)
						{
							return mBoard.GetTopPlantAt(plant.mPlantCol, plant.mRow, PlantPriority.TOPPLANT_EATING_ORDER);
						}
						return mBoard.GetTopPlantAt(plant.mPlantCol, plant.mRow, PlantPriority.TOPPLANT_CATAPULT_ORDER);
					}
				}
			}
			return null;
		}

		public void ConvertToFireball()
		{
			mProjectileType = ProjectileType.PROJECTILE_FIREBALL;
			mApp.PlayFoley(FoleyType.FOLEY_FIREPEA);
			float num = -25f;
			float num2 = -25f;
			Reanimation reanimation = mApp.AddReanimation(0f, 0f, 0, ReanimationType.REANIM_FIRE_PEA);
			if (mMotionType == ProjectileMotion.MOTION_BACKWARDS)
			{
				reanimation.OverrideScale(-1f, 1f);
				num += 80f;
			}
			reanimation.SetPosition((mPosX + num) * Constants.S, (mPosY + num2) * Constants.S);
			reanimation.mLoopType = ReanimLoopType.REANIM_LOOP;
			reanimation.mAnimRate = TodCommon.RandRangeFloat(50f, 80f);
			GlobalMembersAttachment.AttachReanim(ref mAttachmentID, reanimation, num, num2);
		}

		public void ConvertToFireball(int aGridX)
		{
			if (mHitTorchwoodGridX == aGridX)
			{
				return;
			}
			mProjectileType = ProjectileType.PROJECTILE_FIREBALL;
			mHitTorchwoodGridX = aGridX;
			mApp.PlayFoley(FoleyType.FOLEY_FIREPEA);
			float num = -25f;
			float num2 = -25f;
			Reanimation reanimation = mApp.AddReanimation(0f, 0f, 0, ReanimationType.REANIM_FIRE_PEA);
			if (mMotionType == ProjectileMotion.MOTION_BACKWARDS)
			{
				reanimation.OverrideScale(-1f, 1f);
				num += 80f;
			}
			reanimation.SetPosition((mPosX + num) * Constants.S, (mPosY + num2) * Constants.S);
			reanimation.mLoopType = ReanimLoopType.REANIM_LOOP;
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
			mProjectileType = ProjectileType.PROJECTILE_PEA;
			mHitTorchwoodGridX = aGridX;
			mApp.PlayFoley(FoleyType.FOLEY_THROW);
		}

		public bool IsSplashDamage(Zombie theZombie)
		{
			return (mProjectileType != ProjectileType.PROJECTILE_FIREBALL || theZombie == null || !theZombie.IsFireResistant()) && (mProjectileType == ProjectileType.PROJECTILE_MELON || mProjectileType == ProjectileType.PROJECTILE_WINTERMELON || mProjectileType == ProjectileType.PROJECTILE_FIREBALL);
		}

		public void PlayImpactSound(Zombie theZombie)
		{
			bool flag = true;
			bool flag2 = true;
			if (mProjectileType == ProjectileType.PROJECTILE_KERNEL)
			{
				mApp.PlayFoley(FoleyType.FOLEY_KERNEL_SPLAT);
				flag = false;
				flag2 = false;
			}
			else if (mProjectileType == ProjectileType.PROJECTILE_BUTTER)
			{
				mApp.PlayFoley(FoleyType.FOLEY_BUTTER);
				flag2 = false;
			}
			else if (mProjectileType == ProjectileType.PROJECTILE_FIREBALL && IsSplashDamage(theZombie))
			{
				mApp.PlayFoley(FoleyType.FOLEY_IGNITE);
				flag = false;
				flag2 = false;
			}
			else if (mProjectileType == ProjectileType.PROJECTILE_MELON || mProjectileType == ProjectileType.PROJECTILE_WINTERMELON)
			{
				mApp.PlayFoley(FoleyType.FOLEY_MELONIMPACT);
				flag2 = false;
			}
			if (flag)
			{
				if (theZombie != null && theZombie.mHelmType == HelmType.HELMTYPE_PAIL)
				{
					mApp.PlayFoley(FoleyType.FOLEY_SHIELD_HIT);
					flag2 = false;
				}
				else if (theZombie != null && theZombie.mHelmType == HelmType.HELMTYPE_TRAFFIC_CONE)
				{
					mApp.PlayFoley(FoleyType.FOLEY_PLASTIC_HIT);
				}
				else if (theZombie != null && theZombie.mHelmType == HelmType.HELMTYPE_DIGGER)
				{
					mApp.PlayFoley(FoleyType.FOLEY_PLASTIC_HIT);
				}
				else if (theZombie != null && theZombie.mHelmType == HelmType.HELMTYPE_FOOTBALL)
				{
					mApp.PlayFoley(FoleyType.FOLEY_PLASTIC_HIT);
				}
			}
			if (flag2)
			{
				mApp.PlayFoley(FoleyType.FOLEY_SPLAT);
			}
		}

		public bool IsZombieHitBySplash(Zombie theZombie)
		{
			TRect projectileRect = GetProjectileRect();
			if (mProjectileType == ProjectileType.PROJECTILE_FIREBALL)
			{
				projectileRect.mWidth = 100;
			}
			int num = theZombie.mRow - mRow;
			TRect zombieRect = theZombie.GetZombieRect();
			if (theZombie.IsFireResistant() && mProjectileType == ProjectileType.PROJECTILE_FIREBALL)
			{
				return false;
			}
			if (theZombie.mZombieType == ZombieType.ZOMBIE_BOSS)
			{
				num = 0;
			}
			if (mProjectileType == ProjectileType.PROJECTILE_FIREBALL)
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
			if (mMotionType != ProjectileMotion.MOTION_STRAIGHT)
			{
				return false;
			}
			if (mProjectileType != ProjectileType.PROJECTILE_PEA && mProjectileType != ProjectileType.PROJECTILE_SNOWPEA && mProjectileType != ProjectileType.PROJECTILE_ZOMBIE_PEA_MIND_CONTROL)
			{
				return false;
			}
			int count = mBoard.mPlants.Count;
			for (int i = 0; i < count; i++)
			{
				Plant plant = mBoard.mPlants[i];
				if (!plant.mDead && plant.mSeedType == SeedType.SEED_TORCHWOOD && plant.mRow == mRow && !plant.NotOnGround() && mHitTorchwoodGridX != plant.mPlantCol)
				{
					TRect plantAttackRect = plant.GetPlantAttackRect(PlantWeapon.WEAPON_PRIMARY);
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
