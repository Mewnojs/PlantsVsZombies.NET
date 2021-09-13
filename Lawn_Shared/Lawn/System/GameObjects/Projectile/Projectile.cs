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
			b.WriteFloat(this.mAccZ);
			b.WriteLong(this.mAnimCounter);
			b.WriteLong(this.mAnimTicksPerFrame);
			b.WriteLong(this.mClickBackoffCounter);
			b.WriteLong(this.mCobTargetRow);
			b.WriteFloat(this.mCobTargetX);
			b.WriteLong(this.mDamageRangeFlags);
			b.WriteBoolean(this.mDead);
			b.WriteLong(this.mFrame);
			b.WriteLong(this.mHitTorchwoodGridX);
			b.WriteLong(this.mLastPortalX);
			b.WriteLong((int)this.mMotionType);
			b.WriteLong(this.mNumFrames);
			b.WriteBoolean(this.mOnHighGround);
			b.WriteFloat(this.mPosX);
			b.WriteFloat(this.mPosY);
			b.WriteFloat(this.mPosZ);
			b.WriteLong(this.mProjectileAge);
			b.WriteLong((int)this.mProjectileType);
			b.WriteFloat(this.mRotation);
			b.WriteFloat(this.mRotationSpeed);
			b.WriteFloat(this.mShadowY);
			b.WriteFloat(this.mVelX);
			b.WriteFloat(this.mVelY);
			b.WriteFloat(this.mVelZ);
			GameObject.SaveId(this.mTargetZombieID, b);
			return true;
		}

		public override bool LoadFromFile(Sexy.Buffer b)
		{
			base.LoadFromFile(b);
			this.mAccZ = b.ReadFloat();
			this.mAnimCounter = b.ReadLong();
			this.mAnimTicksPerFrame = b.ReadLong();
			this.mClickBackoffCounter = b.ReadLong();
			this.mCobTargetRow = b.ReadLong();
			this.mCobTargetX = b.ReadFloat();
			this.mDamageRangeFlags = b.ReadLong();
			this.mDead = b.ReadBoolean();
			this.mFrame = b.ReadLong();
			this.mHitTorchwoodGridX = b.ReadLong();
			this.mLastPortalX = b.ReadLong();
			this.mMotionType = (ProjectileMotion)b.ReadLong();
			this.mNumFrames = b.ReadLong();
			this.mOnHighGround = b.ReadBoolean();
			this.mPosX = b.ReadFloat();
			this.mPosY = b.ReadFloat();
			this.mPosZ = b.ReadFloat();
			this.mProjectileAge = b.ReadLong();
			this.mProjectileType = (ProjectileType)b.ReadLong();
			this.mRotation = b.ReadFloat();
			this.mRotationSpeed = b.ReadFloat();
			this.mShadowY = b.ReadFloat();
			this.mVelX = b.ReadFloat();
			this.mVelY = b.ReadFloat();
			this.mVelZ = b.ReadFloat();
			this.mTargetZombieIDSaved = GameObject.LoadId(b);
			return true;
		}

		public override void LoadingComplete()
		{
			base.LoadingComplete();
			this.mTargetZombieID = (GameObject.GetObjectById(this.mTargetZombieIDSaved) as Zombie);
			Projectile newProjectile = Projectile.GetNewProjectile();
			newProjectile.ProjectileInitialize(this.mX, this.mY, this.mRenderOrder, this.mRow, this.mProjectileType);
			if (this.mProjectileType == ProjectileType.PROJECTILE_FIREBALL)
			{
				this.ConvertToFireball();
			}
		}

		protected override void Reset()
		{
			base.Reset();
			this.mFrame = 0;
			this.mNumFrames = 0;
			this.mAnimCounter = 0;
			this.mPosX = 0f;
			this.mPosY = 0f;
			this.mPosZ = 0f;
			this.mVelX = 0f;
			this.mVelY = 0f;
			this.mVelZ = 0f;
			this.mAccZ = 0f;
			this.mShadowY = 0f;
			this.mDead = false;
			this.mAnimTicksPerFrame = 0;
			this.mMotionType = ProjectileMotion.MOTION_STRAIGHT;
			this.mProjectileType = ProjectileType.PROJECTILE_PEA;
			this.mProjectileAge = 0;
			this.mClickBackoffCounter = 0;
			this.mRotation = 0f;
			this.mRotationSpeed = 0f;
			this.mOnHighGround = false;
			this.mDamageRangeFlags = 0;
			this.mHitTorchwoodGridX = 0;
			this.mAttachmentID = null;
			this.mCobTargetX = 0f;
			this.mCobTargetRow = 0;
			this.mTargetZombieID = null;
			this.mLastPortalX = 0;
		}

		public void ProjectileInitialize(int theX, int theY, int theRenderOrder, int theRow, ProjectileType theProjectileType)
		{
			int num = this.mBoard.PixelToGridXKeepOnBoard(theX, theY);
			this.mProjectileType = theProjectileType;
			this.mPosX = (float)theX;
			this.mPosY = (float)theY;
			this.mPosZ = 0f;
			this.mVelX = 0f;
			this.mVelY = 0f;
			this.mVelZ = 0f;
			this.mAccZ = 0f;
			this.mShadowY = (float)this.mBoard.GridToPixelY(num, theRow) + 67f;
			this.mMotionType = ProjectileMotion.MOTION_STRAIGHT;
			this.mFrame = 0;
			this.mNumFrames = 1;
			this.mRow = theRow;
			this.mDamageRangeFlags = 0;
			this.mHitTorchwoodGridX = -1;
			this.mDead = false;
			this.mAttachmentID = null;
			this.mCobTargetX = 0f;
			this.mCobTargetRow = 0;
			this.mTargetZombieID = null;
			this.mLastPortalX = -1;
			if (this.mBoard.mGridSquareType[num, this.mRow] == GridSquareType.GRIDSQUARE_HIGH_GROUND)
			{
				this.mOnHighGround = true;
			}
			else
			{
				this.mOnHighGround = false;
			}
			if (this.mBoard.StageHasRoof() && (float)theX < 480f)
			{
				this.mShadowY -= 12f;
			}
			this.mRenderOrder = theRenderOrder;
			this.mProjectileAge = 0;
			this.mClickBackoffCounter = 0;
			this.mRotation = 0f;
			this.mRotationSpeed = 0f;
			this.mWidth = 40;
			this.mHeight = 40;
			this.mAnimTicksPerFrame = 0;
			bool flag = false;
			if (this.mProjectileType == ProjectileType.PROJECTILE_CABBAGE || this.mProjectileType == ProjectileType.PROJECTILE_BUTTER)
			{
				this.mRotation = -0.87964594f;
				this.mRotationSpeed = TodCommon.RandRangeFloat(-0.08f, -0.02f);
			}
			else if (this.mProjectileType == ProjectileType.PROJECTILE_MELON || this.mProjectileType == ProjectileType.PROJECTILE_WINTERMELON)
			{
				this.mRotation = -1.2566371f;
				this.mRotationSpeed = TodCommon.RandRangeFloat(-0.08f, -0.02f);
			}
			else if (this.mProjectileType == ProjectileType.PROJECTILE_KERNEL)
			{
				this.mRotation = 0f;
				this.mRotationSpeed = TodCommon.RandRangeFloat(-0.2f, -0.08f);
			}
			else if (this.mProjectileType == ProjectileType.PROJECTILE_SNOWPEA)
			{
				float num2 = 8f;
				float num3 = 13f;
				TodParticleSystem theParticleSystem = this.mApp.AddTodParticle(this.mPosX + num2, this.mPosY + num3, 400000, ParticleEffect.PARTICLE_SNOWPEA_TRAIL);
				GlobalMembersAttachment.AttachParticle(ref this.mAttachmentID, theParticleSystem, num2, num3);
			}
			else if (this.mProjectileType == ProjectileType.PROJECTILE_FIREBALL)
			{
				Debug.ASSERT(false);
			}
			else if (this.mProjectileType == ProjectileType.PROJECTILE_COBBIG)
			{
				this.mWidth = (int)((float)AtlasResources.IMAGE_REANIM_COBCANNON_COB.GetWidth() * Constants.IS);
				this.mHeight = (int)((float)AtlasResources.IMAGE_REANIM_COBCANNON_COB.GetHeight() * Constants.IS);
				this.mRotation = 1.5707964f;
			}
			else if (this.mProjectileType == ProjectileType.PROJECTILE_PUFF)
			{
				float num4 = 13f;
				float num5 = 13f;
				TodParticleSystem theParticleSystem2 = this.mApp.AddTodParticle(this.mPosX + num4, this.mPosY + num5, 400000, ParticleEffect.PARTICLE_PUFFSHROOM_TRAIL);
				GlobalMembersAttachment.AttachParticle(ref this.mAttachmentID, theParticleSystem2, num4, num5);
			}
			else if (this.mProjectileType != ProjectileType.PROJECTILE_BUTTER)
			{
				if (this.mProjectileType == ProjectileType.PROJECTILE_BASKETBALL)
				{
					this.mRotation = TodCommon.RandRangeFloat(0f, 6.2831855f);
					this.mRotationSpeed = TodCommon.RandRangeFloat(0.05f, 0.1f);
				}
				else if (this.mProjectileType == ProjectileType.PROJECTILE_STAR)
				{
					this.mShadowY += 15f;
					this.mRotationSpeed = TodCommon.RandRangeFloat(0.05f, 0.1f);
					if (RandomNumbers.NextNumber(2) == 0)
					{
						this.mRotationSpeed = -this.mRotationSpeed;
					}
				}
				else if (this.mProjectileType == ProjectileType.PROJECTILE_ZOMBIE_PEA_MIND_CONTROL)
				{
					this.mDamageRangeFlags = 1;
				}
			}
			if (flag)
			{
				this.mAnimCounter = TodCommon.RandRangeInt(0, this.mNumFrames * this.mAnimTicksPerFrame);
			}
			else
			{
				this.mAnimCounter = 0;
			}
			this.mX = (int)this.mPosX;
			this.mY = (int)this.mPosY;
		}

		public void Dispose()
		{
			GlobalMembersAttachment.AttachmentDie(ref this.mAttachmentID);
		}

		public void Update()
		{
			this.mProjectileAge += 3;
			if (this.mApp.mGameScene != GameScenes.SCENE_PLAYING && !this.mBoard.mCutScene.ShouldRunUpsellBoard())
			{
				return;
			}
			int num = 20;
			if (this.mProjectileType == ProjectileType.PROJECTILE_PEA || this.mProjectileType == ProjectileType.PROJECTILE_SNOWPEA || this.mProjectileType == ProjectileType.PROJECTILE_CABBAGE || this.mProjectileType == ProjectileType.PROJECTILE_MELON || this.mProjectileType == ProjectileType.PROJECTILE_WINTERMELON || this.mProjectileType == ProjectileType.PROJECTILE_KERNEL || this.mProjectileType == ProjectileType.PROJECTILE_BUTTER || this.mProjectileType == ProjectileType.PROJECTILE_COBBIG || this.mProjectileType == ProjectileType.PROJECTILE_ZOMBIE_PEA || this.mProjectileType == ProjectileType.PROJECTILE_SPIKE || this.mProjectileType == ProjectileType.PROJECTILE_ZOMBIE_PEA_MIND_CONTROL)
			{
				num = 0;
			}
			if (this.mProjectileAge > num)
			{
				this.mRenderOrder = Board.MakeRenderOrder(RenderLayer.RENDER_LAYER_PROJECTILE, this.mRow, 0);
			}
			if (this.mApp.IsFinalBossLevel())
			{
				this.mRenderOrder = Board.MakeRenderOrder(RenderLayer.RENDER_LAYER_PROJECTILE, 5, 0);
			}
			if (this.mClickBackoffCounter > 0)
			{
				this.mClickBackoffCounter -= 3;
			}
			this.mRotation += this.mRotationSpeed * 3f;
			this.UpdateMotion();
			GlobalMembersAttachment.AttachmentUpdateAndMove(ref this.mAttachmentID, this.mPosX, this.mPosY + this.mPosZ);
		}

		public void Draw(Graphics g)
		{
			ProjectileDefinition projectileDef = this.GetProjectileDef();
			g.SetDrawMode(Graphics.DrawMode.DRAWMODE_NORMAL);
			Image image = null;
			float num = 1f;
			if (this.mProjectileType == ProjectileType.PROJECTILE_COBBIG)
			{
				image = AtlasResources.IMAGE_REANIM_COBCANNON_COB;
				num = 0.9f;
			}
			else if (this.mProjectileType == ProjectileType.PROJECTILE_PEA || this.mProjectileType == ProjectileType.PROJECTILE_ZOMBIE_PEA || this.mProjectileType == ProjectileType.PROJECTILE_ZOMBIE_PEA_MIND_CONTROL)
			{
				image = AtlasResources.IMAGE_PROJECTILEPEA;
				if (this.mProjectileType == ProjectileType.PROJECTILE_ZOMBIE_PEA_MIND_CONTROL)
				{
					g.SetColorizeImages(true);
					g.SetColor(GameConstants.ZOMBIE_MINDCONTROLLED_COLOR);
				}
			}
			else if (this.mProjectileType == ProjectileType.PROJECTILE_SNOWPEA)
			{
				image = AtlasResources.IMAGE_PROJECTILESNOWPEA;
			}
			else if (this.mProjectileType == ProjectileType.PROJECTILE_FIREBALL)
			{
				image = null;
			}
			else if (this.mProjectileType == ProjectileType.PROJECTILE_SPIKE)
			{
				image = AtlasResources.IMAGE_PROJECTILECACTUS;
			}
			else if (this.mProjectileType == ProjectileType.PROJECTILE_STAR)
			{
				image = AtlasResources.IMAGE_PROJECTILE_STAR;
			}
			else if (this.mProjectileType == ProjectileType.PROJECTILE_PUFF)
			{
				image = AtlasResources.IMAGE_PUFFSHROOM_PUFF1;
				num = TodCommon.TodAnimateCurveFloat(0, 30, this.mProjectileAge, 0.3f, 1f, TodCurves.CURVE_LINEAR);
			}
			else if (this.mProjectileType == ProjectileType.PROJECTILE_BASKETBALL)
			{
				image = AtlasResources.IMAGE_REANIM_ZOMBIE_CATAPULT_BASKETBALL;
				num = 1.1f;
			}
			else if (this.mProjectileType == ProjectileType.PROJECTILE_CABBAGE)
			{
				image = AtlasResources.IMAGE_REANIM_CABBAGEPULT_CABBAGE;
				num = 1f;
			}
			else if (this.mProjectileType == ProjectileType.PROJECTILE_KERNEL)
			{
				image = AtlasResources.IMAGE_REANIM_CORNPULT_KERNAL;
				num = 0.95f;
			}
			else if (this.mProjectileType == ProjectileType.PROJECTILE_BUTTER)
			{
				image = AtlasResources.IMAGE_REANIM_CORNPULT_BUTTER;
				num = 0.8f;
			}
			else if (this.mProjectileType == ProjectileType.PROJECTILE_MELON)
			{
				image = AtlasResources.IMAGE_REANIM_MELONPULT_MELON;
				num = 1f;
			}
			else if (this.mProjectileType == ProjectileType.PROJECTILE_WINTERMELON)
			{
				image = AtlasResources.IMAGE_REANIM_WINTERMELON_PROJECTILE;
				num = 1f;
			}
			else
			{
				Debug.ASSERT(false);
			}
			bool mirror = false;
			if (this.mMotionType == ProjectileMotion.MOTION_BEE_BACKWARDS)
			{
				mirror = true;
			}
			if (image != null)
			{
				Debug.ASSERT(projectileDef.mImageRow < image.mNumRows);
				Debug.ASSERT(this.mFrame < image.mNumCols);
				int celWidth = image.GetCelWidth();
				int celHeight = image.GetCelHeight();
				TRect theSrcRect = new TRect(celWidth * this.mFrame, celHeight * projectileDef.mImageRow, celWidth, celHeight);
				if (TodCommon.FloatApproxEqual(this.mRotation, 0f) && TodCommon.FloatApproxEqual(num, 1f))
				{
					g.DrawImageMirror(image, 0, 0, theSrcRect, mirror);
				}
				else
				{
					float num2 = this.mPosX + (float)celWidth * 0.5f;
					float num3 = this.mPosZ + this.mPosY + (float)celHeight * 0.5f;
					SexyTransform2D sexyTransform2D = default(SexyTransform2D);
					TodCommon.TodScaleRotateTransformMatrix(ref sexyTransform2D.mMatrix, num2 * Constants.S + (float)this.mBoard.mX, num3 * Constants.S + (float)this.mBoard.mY, this.mRotation, num, num);
					TodCommon.TodBltMatrix(g, image, sexyTransform2D.mMatrix, ref g.mClipRect, SexyColor.White, g.mDrawMode, theSrcRect);
				}
			}
			if (this.mAttachmentID != null)
			{
				Graphics @new = Graphics.GetNew(g);
				base.MakeParentGraphicsFrame(@new);
				GlobalMembersAttachment.AttachmentDraw(this.mAttachmentID, @new, false, true);
				@new.PrepareForReuse();
			}
			if (this.mProjectileType == ProjectileType.PROJECTILE_ZOMBIE_PEA_MIND_CONTROL)
			{
				g.SetColorizeImages(false);
			}
		}

		public void DrawShadow(Graphics g)
		{
			int theCelCol = 0;
			float num = 1f;
			float num2 = 1f;
			float num3 = this.mPosX - (float)this.mX;
			float num4 = this.mPosY - (float)this.mY;
			int num5 = this.mBoard.PixelToGridXKeepOnBoard(this.mX, this.mY);
			bool flag = false;
			if (this.mBoard.mGridSquareType[num5, this.mRow] == GridSquareType.GRIDSQUARE_HIGH_GROUND)
			{
				flag = true;
			}
			if (this.mOnHighGround && !flag)
			{
				num4 += (float)Constants.HIGH_GROUND_HEIGHT;
			}
			else if (!this.mOnHighGround && flag)
			{
				num4 += (float)(-(float)Constants.HIGH_GROUND_HEIGHT);
			}
			if (this.mBoard.StageIsNight())
			{
				theCelCol = 1;
			}
			if (this.mProjectileType == ProjectileType.PROJECTILE_PEA || this.mProjectileType == ProjectileType.PROJECTILE_ZOMBIE_PEA || this.mProjectileType == ProjectileType.PROJECTILE_ZOMBIE_PEA_MIND_CONTROL)
			{
				num3 += 3f;
			}
			else if (this.mProjectileType == ProjectileType.PROJECTILE_SNOWPEA)
			{
				num3 += -1f;
				num = 1.3f;
			}
			else if (this.mProjectileType == ProjectileType.PROJECTILE_STAR)
			{
				num3 += 7f;
			}
			else if (this.mProjectileType == ProjectileType.PROJECTILE_CABBAGE || this.mProjectileType == ProjectileType.PROJECTILE_KERNEL || this.mProjectileType == ProjectileType.PROJECTILE_BUTTER || this.mProjectileType == ProjectileType.PROJECTILE_MELON || this.mProjectileType == ProjectileType.PROJECTILE_WINTERMELON)
			{
				num3 += 3f;
				num4 += 10f;
				num = 1.6f;
			}
			else
			{
				if (this.mProjectileType == ProjectileType.PROJECTILE_PUFF)
				{
					return;
				}
				if (this.mProjectileType == ProjectileType.PROJECTILE_COBBIG)
				{
					num = 1f;
					num2 = 3f;
					num3 += 57f;
				}
				else if (this.mProjectileType == ProjectileType.PROJECTILE_FIREBALL)
				{
					num = 1.4f;
				}
			}
			if (this.mMotionType == ProjectileMotion.MOTION_LOBBED)
			{
				float num6 = 200f / (TodCommon.ClampFloat(-this.mPosZ, 0f, 200f) + 200f);
				num *= num6;
			}
			TodCommon.TodDrawImageCelScaledF(g, AtlasResources.IMAGE_PEA_SHADOWS, num3 * Constants.S, (this.mShadowY - this.mPosY + num4) * Constants.S, theCelCol, 0, num * num2, num);
		}

		public void Die()
		{
			this.mDead = true;
			if (this.mProjectileType == ProjectileType.PROJECTILE_PUFF || this.mProjectileType == ProjectileType.PROJECTILE_SNOWPEA)
			{
				GlobalMembersAttachment.AttachmentCrossFade(this.mAttachmentID, "FadeOut");
				GlobalMembersAttachment.AttachmentDetach(ref this.mAttachmentID);
				return;
			}
			GlobalMembersAttachment.AttachmentDie(ref this.mAttachmentID);
		}

		public void DoImpact(Zombie theZombie)
		{
			this.PlayImpactSound(theZombie);
			if (this.IsSplashDamage(theZombie))
			{
				if (this.mProjectileType == ProjectileType.PROJECTILE_FIREBALL && theZombie != null)
				{
					theZombie.RemoveColdEffects();
				}
				this.DoSplashDamage(theZombie);
			}
			else if (theZombie != null)
			{
				ProjectileDefinition projectileDef = this.GetProjectileDef();
				uint damageFlags = this.GetDamageFlags(theZombie);
				int mDamage = projectileDef.mDamage;
				ProjectileType projectileType = this.mProjectileType;
				theZombie.TakeDamage(projectileDef.mDamage, damageFlags);
			}
			int aRenderOrder = this.mRenderOrder + 1;
			float num = this.mPosX - this.mVelX;
			float num2 = this.mPosY + this.mPosZ - this.mVelY - this.mVelZ;
			ParticleEffect particleEffect = ParticleEffect.PARTICLE_NONE;
			float num3 = this.mPosX + 12f;
			float num4 = this.mPosY + 12f;
			if (this.mProjectileType == ProjectileType.PROJECTILE_MELON)
			{
				this.mApp.AddTodParticle(num + 30f, num2 + 30f, aRenderOrder, ParticleEffect.PARTICLE_MELONSPLASH);
			}
			else if (this.mProjectileType == ProjectileType.PROJECTILE_WINTERMELON)
			{
				this.mApp.AddTodParticle(num + 30f, num2 + 30f, aRenderOrder, ParticleEffect.PARTICLE_WINTERMELON);
			}
			else if (this.mProjectileType == ProjectileType.PROJECTILE_COBBIG)
			{
				int aRenderOrder2 = Board.MakeRenderOrder(RenderLayer.RENDER_LAYER_GROUND, this.mCobTargetRow, 2);
				this.mApp.AddTodParticle(this.mPosX + 80f, this.mPosY + 40f, aRenderOrder2, ParticleEffect.PARTICLE_BLASTMARK);
				this.mApp.AddTodParticle(this.mPosX + 80f, this.mPosY + 40f, aRenderOrder, ParticleEffect.PARTICLE_POPCORNSPLASH);
				this.mApp.PlaySample(Resources.SOUND_DOOMSHROOM);
				this.mBoard.ShakeBoard(3, -4);
				this.mApp.Vibrate();
			}
			else if (this.mProjectileType == ProjectileType.PROJECTILE_PEA || this.mProjectileType == ProjectileType.PROJECTILE_ZOMBIE_PEA_MIND_CONTROL)
			{
				num3 -= 15f;
				particleEffect = ParticleEffect.PARTICLE_PEA_SPLAT;
			}
			else if (this.mProjectileType == ProjectileType.PROJECTILE_SNOWPEA)
			{
				num3 -= 15f;
				particleEffect = ParticleEffect.PARTICLE_SNOWPEA_SPLAT;
			}
			else if (this.mProjectileType == ProjectileType.PROJECTILE_FIREBALL)
			{
				if (this.IsSplashDamage(theZombie))
				{
					Reanimation reanimation = this.mApp.AddReanimation(this.mPosX + 38f, this.mPosY - 20f, aRenderOrder, ReanimationType.REANIM_JALAPENO_FIRE);
					reanimation.mAnimTime = 0.25f;
					reanimation.mAnimRate = 24f;
					reanimation.OverrideScale(0.7f, 0.4f);
				}
			}
			else if (this.mProjectileType == ProjectileType.PROJECTILE_STAR)
			{
				particleEffect = ParticleEffect.PARTICLE_STAR_SPLAT;
			}
			else if (this.mProjectileType == ProjectileType.PROJECTILE_PUFF)
			{
				num3 -= 20f;
				particleEffect = ParticleEffect.PARTICLE_PUFF_SPLAT;
			}
			else if (this.mProjectileType == ProjectileType.PROJECTILE_CABBAGE)
			{
				num3 = num - 38f;
				num4 = num2 + 23f;
				particleEffect = ParticleEffect.PARTICLE_CABBAGE_SPLAT;
			}
			else if (this.mProjectileType == ProjectileType.PROJECTILE_BUTTER)
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
					if (this.mMotionType == ProjectileMotion.MOTION_BACKWARDS)
					{
						num5 -= 80f;
					}
					else if (this.mPosX > (float)(theZombie.mX + 40) && this.mMotionType != ProjectileMotion.MOTION_LOBBED)
					{
						num5 -= 60f;
					}
					num6 = TodCommon.ClampFloat(num6, 20f, 100f);
					theZombie.AddAttachedParticle((int)num5, (int)num6, particleEffect);
				}
				else
				{
					this.mApp.AddTodParticle(num3, num4, aRenderOrder, particleEffect);
				}
			}
			this.Die();
		}

		public void UpdateMotion()
		{
			if (this.mAnimTicksPerFrame > 0)
			{
				this.mAnimCounter = (this.mAnimCounter + 1) % (this.mNumFrames * this.mAnimTicksPerFrame);
				this.mFrame = this.mAnimCounter / this.mAnimTicksPerFrame;
			}
			int mRow = this.mRow;
			float posYBasedOnRow = this.mBoard.GetPosYBasedOnRow(this.mPosX, this.mRow);
			if (this.mMotionType == ProjectileMotion.MOTION_LOBBED)
			{
				this.UpdateLobMotion();
			}
			else
			{
				this.UpdateNormalMotion();
			}
			float posYBasedOnRow2 = this.mBoard.GetPosYBasedOnRow(this.mPosX, mRow);
			float num = posYBasedOnRow2 - posYBasedOnRow;
			if (this.mProjectileType == ProjectileType.PROJECTILE_COBBIG)
			{
				num = 0f;
			}
			if (this.mMotionType == ProjectileMotion.MOTION_FLOAT_OVER)
			{
				this.mPosY += num;
			}
			if (this.mMotionType == ProjectileMotion.MOTION_LOBBED)
			{
				this.mPosY += num;
				this.mPosZ -= num;
			}
			this.mShadowY += num;
			this.mX = (int)this.mPosX;
			this.mY = (int)(this.mPosY + this.mPosZ);
		}

		private Zombie FindCollisionMindControlledTarget()
		{
			TRect projectileRect = this.GetProjectileRect();
			Zombie zombie = null;
			int num = 0;
			for (int i = 0; i < this.mBoard.mZombies.Count; i++)
			{
				Zombie zombie2 = this.mBoard.mZombies[i];
				if (!zombie2.mDead && zombie2.mRow - this.mRow == 0 && zombie2.mMindControlled)
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
			if (this.mMotionType == ProjectileMotion.MOTION_PUFF && this.mProjectileAge >= 75)
			{
				this.Die();
				return;
			}
			if (this.mPosX > (float)(Constants.WIDE_BOARD_WIDTH + 40) || this.mPosX + (float)this.mWidth < 0f)
			{
				this.Die();
				return;
			}
			Zombie zombie;
			if (this.mMotionType == ProjectileMotion.MOTION_HOMING)
			{
				zombie = this.mBoard.ZombieTryToGet(this.mTargetZombieID);
				if (zombie != null && zombie.EffectedByDamage((uint)this.mDamageRangeFlags))
				{
					TRect projectileRect = this.GetProjectileRect();
					TRect zombieRect = zombie.GetZombieRect();
					int rectOverlap = GameConstants.GetRectOverlap(projectileRect, zombieRect);
					if (rectOverlap >= 0 && this.mPosY > (float)zombieRect.mY && this.mPosY < (float)(zombieRect.mY + zombieRect.mHeight))
					{
						this.DoImpact(zombie);
					}
				}
				return;
			}
			if (this.mProjectileType == ProjectileType.PROJECTILE_STAR && (this.mPosY > 600f || this.mPosY < 0f))
			{
				this.Die();
				return;
			}
			if ((this.mProjectileType == ProjectileType.PROJECTILE_PEA || this.mProjectileType == ProjectileType.PROJECTILE_STAR) && this.mShadowY - this.mPosY > 90f)
			{
				return;
			}
			if (this.mMotionType == ProjectileMotion.MOTION_FLOAT_OVER)
			{
				return;
			}
			if (this.mProjectileType == ProjectileType.PROJECTILE_ZOMBIE_PEA)
			{
				Plant plant = this.FindCollisionTargetPlant();
				if (plant != null)
				{
					ProjectileDefinition projectileDef = this.GetProjectileDef();
					plant.mPlantHealth -= projectileDef.mDamage;
					plant.mEatenFlashCountdown = Math.Max(plant.mEatenFlashCountdown, 25);
					this.mApp.PlayFoley(FoleyType.FOLEY_SPLAT);
					this.mApp.AddTodParticle(this.mPosX - 3f, this.mPosY + 17f, this.mRenderOrder + 1, ParticleEffect.PARTICLE_PEA_SPLAT);
					this.Die();
				}
				Zombie zombie2 = this.FindCollisionMindControlledTarget();
				if (zombie2 != null)
				{
					if (zombie2.mOnHighGround && this.CantHitHighGround())
					{
						return;
					}
					this.DoImpact(zombie2);
				}
				return;
			}
			zombie = this.FindCollisionTarget();
			if (zombie != null)
			{
				if (zombie.mOnHighGround && this.CantHitHighGround())
				{
					return;
				}
				this.DoImpact(zombie);
			}
		}

		public Zombie FindCollisionTarget()
		{
			if (this.PeaAboutToHitTorchwood())
			{
				return null;
			}
			TRect projectileRect = this.GetProjectileRect();
			Zombie zombie = null;
			int num = 0;
			int count = this.mBoard.mZombies.Count;
			for (int i = 0; i < count; i++)
			{
				Zombie zombie2 = this.mBoard.mZombies[i];
				if (!zombie2.mDead && (zombie2.mZombiePhase != ZombiePhase.PHASE_SNORKEL_WALKING_IN_POOL || this.mPosZ > 45f) && (this.mProjectileType != ProjectileType.PROJECTILE_STAR || this.mProjectileAge >= 25 || this.mVelX < 0f || zombie2.mZombieType != ZombieType.ZOMBIE_DIGGER) && zombie2.EffectedByDamage((uint)this.mDamageRangeFlags))
				{
					int num2 = zombie2.mRow - this.mRow;
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
			if (this.mProjectileType == ProjectileType.PROJECTILE_COBBIG && this.mPosZ < -700f)
			{
				this.mVelZ = 8f;
				this.mPosX = this.mCobTargetX;
				this.mRow = this.mCobTargetRow;
				int theGridX = this.mBoard.PixelToGridXKeepOnBoard((int)this.mCobTargetX, 0);
				this.mPosY = (float)this.mBoard.GridToPixelY(theGridX, this.mCobTargetRow);
				this.mShadowY = this.mPosY + 67f;
				this.mRotation = -1.5707964f;
			}
			this.mVelZ += 3f * this.mAccZ;
			if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_HIGH_GRAVITY)
			{
				this.mVelZ += 3f * this.mAccZ;
			}
			this.mPosX += 3f * this.mVelX;
			this.mPosY += 3f * this.mVelY;
			this.mPosZ += 3f * this.mVelZ;
			bool flag = this.mVelZ < 0f;
			if (flag && (this.mProjectileType == ProjectileType.PROJECTILE_BASKETBALL || this.mProjectileType == ProjectileType.PROJECTILE_COBBIG))
			{
				return;
			}
			if (this.mProjectileAge > 20)
			{
				if (flag)
				{
					return;
				}
				float num = 0f;
				if (this.mProjectileType == ProjectileType.PROJECTILE_BUTTER)
				{
					num = -32f;
				}
				else if (this.mProjectileType == ProjectileType.PROJECTILE_BASKETBALL)
				{
					num = 60f;
				}
				else if (this.mProjectileType == ProjectileType.PROJECTILE_MELON || this.mProjectileType == ProjectileType.PROJECTILE_WINTERMELON)
				{
					num = -35f;
				}
				else if (this.mProjectileType == ProjectileType.PROJECTILE_CABBAGE || this.mProjectileType == ProjectileType.PROJECTILE_KERNEL)
				{
					num = -30f;
				}
				else if (this.mProjectileType == ProjectileType.PROJECTILE_COBBIG)
				{
					num = -60f;
				}
				if (this.mBoard.mPlantRow[this.mRow] == PlantRowType.PLANTROW_POOL)
				{
					num += 40f;
				}
				if (this.mPosZ <= num)
				{
					return;
				}
			}
			Plant plant = null;
			Zombie zombie = null;
			if (this.mProjectileType == ProjectileType.PROJECTILE_BASKETBALL || this.mProjectileType == ProjectileType.PROJECTILE_ZOMBIE_PEA)
			{
				plant = this.FindCollisionTargetPlant();
			}
			else
			{
				zombie = this.FindCollisionTarget();
			}
			float num2 = 80f;
			if (this.mProjectileType == ProjectileType.PROJECTILE_COBBIG)
			{
				num2 = -40f;
			}
			bool flag2 = this.mPosZ > num2;
			if (zombie == null && plant == null && !flag2)
			{
				return;
			}
			if (plant != null)
			{
				Plant plant2 = this.mBoard.FindUmbrellaPlant(plant.mPlantCol, plant.mRow);
				if (plant2 != null)
				{
					if (plant2.mState == PlantState.STATE_UMBRELLA_REFLECTING)
					{
						this.mApp.PlayFoley(FoleyType.FOLEY_SPLAT);
						this.mApp.AddTodParticle(this.mPosX + 20f, this.mPosY + 20f, 400001, ParticleEffect.PARTICLE_UMBRELLA_REFLECT);
					}
					else
					{
						if (plant2.mState == PlantState.STATE_UMBRELLA_TRIGGERED)
						{
							return;
						}
						this.mApp.PlayFoley(FoleyType.FOLEY_UMBRELLA);
						plant2.DoSpecial();
						return;
					}
				}
				else
				{
					ProjectileDefinition projectileDef = this.GetProjectileDef();
					plant.mPlantHealth -= projectileDef.mDamage;
					plant.mEatenFlashCountdown = Math.Max(plant.mEatenFlashCountdown, 25);
					this.mApp.PlayFoley(FoleyType.FOLEY_SPLAT);
				}
				this.Die();
				return;
			}
			if (this.mProjectileType == ProjectileType.PROJECTILE_COBBIG)
			{
				int liveGargantuarCount = this.mBoard.GetLiveGargantuarCount();
				this.mBoard.KillAllZombiesInRadius(this.mRow, (int)this.mPosX + 80, (int)this.mPosY + 40, 115, 1, true, this.mDamageRangeFlags);
				int liveGargantuarCount2 = this.mBoard.GetLiveGargantuarCount();
				this.mBoard.mGargantuarsKillsByCornCob += liveGargantuarCount - liveGargantuarCount2;
				if (this.mBoard.mGargantuarsKillsByCornCob >= 2)
				{
					this.mBoard.GrantAchievement(AchievementId.ACHIEVEMENT_POPCORN_PARTY, true);
				}
				this.DoImpact(null);
				return;
			}
			this.DoImpact(zombie);
		}

		public void CheckForHighGround()
		{
			float num = this.mShadowY - this.mPosY;
			if ((this.mProjectileType == ProjectileType.PROJECTILE_PEA || this.mProjectileType == ProjectileType.PROJECTILE_SNOWPEA || this.mProjectileType == ProjectileType.PROJECTILE_FIREBALL || this.mProjectileType == ProjectileType.PROJECTILE_SPIKE || this.mProjectileType == ProjectileType.PROJECTILE_COBBIG) && num < 28f)
			{
				this.DoImpact(null);
				return;
			}
			if (this.mProjectileType == ProjectileType.PROJECTILE_PUFF && num < 0f)
			{
				this.DoImpact(null);
				return;
			}
			if (this.mProjectileType == ProjectileType.PROJECTILE_STAR && num < 23f)
			{
				this.DoImpact(null);
				return;
			}
			if (!this.CantHitHighGround())
			{
				return;
			}
			int num2 = this.mBoard.PixelToGridXKeepOnBoard((int)this.mPosX + 30, (int)this.mPosY);
			if (this.mBoard.mGridSquareType[num2, this.mRow] == GridSquareType.GRIDSQUARE_HIGH_GROUND)
			{
				this.DoImpact(null);
			}
		}

		public bool CantHitHighGround()
		{
			return this.mMotionType != ProjectileMotion.MOTION_BACKWARDS && this.mMotionType != ProjectileMotion.MOTION_HOMING && (this.mProjectileType == ProjectileType.PROJECTILE_PEA || this.mProjectileType == ProjectileType.PROJECTILE_SNOWPEA || this.mProjectileType == ProjectileType.PROJECTILE_STAR || this.mProjectileType == ProjectileType.PROJECTILE_PUFF || this.mProjectileType == ProjectileType.PROJECTILE_FIREBALL) && !this.mOnHighGround;
		}

		public void DoSplashDamage(Zombie theZombie)
		{
			ProjectileDefinition projectileDef = this.GetProjectileDef();
			int num = 0;
			int count = this.mBoard.mZombies.Count;
			for (int i = 0; i < count; i++)
			{
				Zombie zombie = this.mBoard.mZombies[i];
				if (!zombie.mDead && theZombie != zombie && this.IsZombieHitBySplash(zombie))
				{
					num++;
				}
			}
			int mDamage = projectileDef.mDamage;
			int num2 = 3;
			int num3 = projectileDef.mDamage / num2;
			int num4 = mDamage * 7;
			if (this.mProjectileType == ProjectileType.PROJECTILE_FIREBALL)
			{
				num4 = mDamage;
			}
			int num5 = num3 * num;
			if (num5 > num4)
			{
				num3 = Math.Max(1, projectileDef.mDamage * num4 / (num5 * num2));
			}
			for (int j = 0; j < count; j++)
			{
				Zombie zombie = this.mBoard.mZombies[j];
				if (!zombie.mDead && this.IsZombieHitBySplash(zombie))
				{
					uint damageFlags = this.GetDamageFlags(zombie);
					if (theZombie == zombie)
					{
						zombie.TakeDamage(mDamage, damageFlags);
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
			ProjectileDefinition projectileDefinition = GameConstants.gProjectileDefinition[(int)this.mProjectileType];
			Debug.ASSERT(projectileDefinition.mProjectileType == this.mProjectileType);
			return projectileDefinition;
		}

		public uint GetDamageFlags(Zombie theZombie)
		{
			uint result = 0U;
			if (this.IsSplashDamage(theZombie))
			{
				TodCommon.SetBit(ref result, 1, 1);
			}
			else if (this.mMotionType == ProjectileMotion.MOTION_LOBBED || this.mMotionType == ProjectileMotion.MOTION_BACKWARDS)
			{
				TodCommon.SetBit(ref result, 0, 1);
			}
			else if (this.mMotionType == ProjectileMotion.MOTION_STAR && this.mVelX < 0f)
			{
				TodCommon.SetBit(ref result, 0, 1);
			}
			if (this.mProjectileType == ProjectileType.PROJECTILE_SNOWPEA || this.mProjectileType == ProjectileType.PROJECTILE_WINTERMELON)
			{
				TodCommon.SetBit(ref result, 2, 1);
			}
			return result;
		}

		public TRect GetProjectileRect()
		{
			if (this.mProjectileType == ProjectileType.PROJECTILE_PEA || this.mProjectileType == ProjectileType.PROJECTILE_SNOWPEA || this.mProjectileType == ProjectileType.PROJECTILE_ZOMBIE_PEA || this.mProjectileType == ProjectileType.PROJECTILE_ZOMBIE_PEA_MIND_CONTROL)
			{
				return new TRect(this.mX - 15, this.mY, this.mWidth + 15, this.mHeight);
			}
			if (this.mProjectileType == ProjectileType.PROJECTILE_COBBIG)
			{
				int num = 115;
				return new TRect(this.mX + this.mWidth / 2 - num, this.mY + this.mHeight / 2 - num, num * 2, num * 2);
			}
			if (this.mProjectileType == ProjectileType.PROJECTILE_MELON || this.mProjectileType == ProjectileType.PROJECTILE_WINTERMELON)
			{
				return new TRect(this.mX + 20, this.mY, 60, this.mHeight);
			}
			if (this.mProjectileType == ProjectileType.PROJECTILE_FIREBALL)
			{
				return new TRect(this.mX, this.mY, this.mWidth - 10, this.mHeight);
			}
			if (this.mProjectileType == ProjectileType.PROJECTILE_SPIKE)
			{
				return new TRect(this.mX - 25, this.mY, this.mWidth + 25, this.mHeight);
			}
			return new TRect(this.mX, this.mY, this.mWidth, this.mHeight);
		}

		public void UpdateNormalMotion()
		{
			if (this.mMotionType == ProjectileMotion.MOTION_BACKWARDS)
			{
				this.mPosX -= 9.99f;
			}
			else if (this.mMotionType == ProjectileMotion.MOTION_HOMING)
			{
				Zombie zombie = this.mBoard.ZombieTryToGet(this.mTargetZombieID);
				if (zombie != null && zombie.EffectedByDamage((uint)this.mDamageRangeFlags))
				{
					TRect zombieRect = zombie.GetZombieRect();
					SexyVector2 lhs = new SexyVector2(zombie.ZombieTargetLeadX(0f), (float)(zombieRect.mY + zombieRect.mHeight / 2));
					SexyVector2 rhs = new SexyVector2(this.mPosX + (float)(this.mWidth / 2), this.mPosY + (float)(this.mHeight / 2));
					SexyVector2 sexyVector = (lhs - rhs).Normalize();
					SexyVector2 sexyVector2 = new SexyVector2(this.mVelX, this.mVelY);
					sexyVector2.mVector += sexyVector.mVector * (0.001f * (float)this.mProjectileAge);
					sexyVector2 = sexyVector2.Normalize();
					sexyVector2.mVector *= 2f;
					this.mVelX = sexyVector2.x;
					this.mVelY = sexyVector2.y;
					this.mRotation = (float)(-(float)Math.Atan2((double)this.mVelY, (double)this.mVelX));
				}
				this.mPosY += 3f * this.mVelY;
				this.mPosX += 3f * this.mVelX;
				this.mShadowY += 3f * this.mVelY;
				this.mRow = this.mBoard.PixelToGridYKeepOnBoard((int)this.mPosX, (int)this.mPosY);
			}
			else if (this.mMotionType == ProjectileMotion.MOTION_STAR)
			{
				this.mPosY += 3f * this.mVelY;
				this.mPosX += 3f * this.mVelX;
				this.mShadowY += 3f * this.mVelY;
				if (this.mVelY != 0f)
				{
					int mRow = this.mBoard.PixelToGridYKeepOnBoard((int)this.mPosX, (int)this.mPosY);
					this.mRow = mRow;
				}
			}
			else if (this.mMotionType == ProjectileMotion.MOTION_BEE)
			{
				if (this.mProjectileAge < 60)
				{
					this.mPosY -= 1.5f;
				}
				this.mPosX += 9.99f;
			}
			else if (this.mMotionType == ProjectileMotion.MOTION_FLOAT_OVER)
			{
				if (this.mVelZ < 0f)
				{
					this.mVelZ += 0.006f;
					this.mVelZ = Math.Min(this.mVelZ, 0f);
					this.mPosY += 3f * this.mVelZ;
					this.mRotation = 0.3f + -0.70000005f * this.mVelZ * 3.1415927f * 0.25f;
				}
				this.mPosX += 1.2f;
			}
			else if (this.mMotionType == ProjectileMotion.MOTION_BEE_BACKWARDS)
			{
				if (this.mProjectileAge < 60)
				{
					this.mPosY -= 1.5f;
				}
				this.mPosX -= 9.99f;
			}
			else if (this.mMotionType == ProjectileMotion.MOTION_THREEPEATER)
			{
				this.mPosX += 9.99f;
				this.mPosY += 3f * this.mVelY;
				this.mVelY *= 0.97f;
				this.mVelY *= 0.97f;
				this.mVelY *= 0.97f;
				this.mShadowY += 3f * this.mVelY;
			}
			else
			{
				this.mPosX += 9.99f;
			}
			if (this.mApp.mGameMode == GameMode.GAMEMODE_CHALLENGE_HIGH_GRAVITY)
			{
				if (this.mMotionType == ProjectileMotion.MOTION_FLOAT_OVER)
				{
					this.mVelZ += 0.012f;
				}
				else
				{
					this.mVelZ += 0.6f;
				}
				this.mPosY += 3f * this.mVelZ;
			}
			this.CheckForCollision();
			this.CheckForHighGround();
		}

		public Plant FindCollisionTargetPlant()
		{
			TRect projectileRect = this.GetProjectileRect();
			int count = this.mBoard.mPlants.Count;
			for (int i = 0; i < count; i++)
			{
				Plant plant = this.mBoard.mPlants[i];
				if (!plant.mDead && this.mRow == plant.mRow && (this.mProjectileType != ProjectileType.PROJECTILE_ZOMBIE_PEA || (plant.mSeedType != SeedType.SEED_PUFFSHROOM && plant.mSeedType != SeedType.SEED_SUNSHROOM && plant.mSeedType != SeedType.SEED_POTATOMINE && plant.mSeedType != SeedType.SEED_SPIKEWEED && plant.mSeedType != SeedType.SEED_SPIKEROCK && plant.mSeedType != SeedType.SEED_LILYPAD)))
				{
					TRect plantRect = plant.GetPlantRect();
					int rectOverlap = GameConstants.GetRectOverlap(projectileRect, plantRect);
					if (rectOverlap > 8)
					{
						if (this.mProjectileType == ProjectileType.PROJECTILE_ZOMBIE_PEA)
						{
							return this.mBoard.GetTopPlantAt(plant.mPlantCol, plant.mRow, PlantPriority.TOPPLANT_EATING_ORDER);
						}
						return this.mBoard.GetTopPlantAt(plant.mPlantCol, plant.mRow, PlantPriority.TOPPLANT_CATAPULT_ORDER);
					}
				}
			}
			return null;
		}

		public void ConvertToFireball()
		{
			this.mProjectileType = ProjectileType.PROJECTILE_FIREBALL;
			this.mApp.PlayFoley(FoleyType.FOLEY_FIREPEA);
			float num = -25f;
			float num2 = -25f;
			Reanimation reanimation = this.mApp.AddReanimation(0f, 0f, 0, ReanimationType.REANIM_FIRE_PEA);
			if (this.mMotionType == ProjectileMotion.MOTION_BACKWARDS)
			{
				reanimation.OverrideScale(-1f, 1f);
				num += 80f;
			}
			reanimation.SetPosition((this.mPosX + num) * Constants.S, (this.mPosY + num2) * Constants.S);
			reanimation.mLoopType = ReanimLoopType.REANIM_LOOP;
			reanimation.mAnimRate = TodCommon.RandRangeFloat(50f, 80f);
			GlobalMembersAttachment.AttachReanim(ref this.mAttachmentID, reanimation, num, num2);
		}

		public void ConvertToFireball(int aGridX)
		{
			if (this.mHitTorchwoodGridX == aGridX)
			{
				return;
			}
			this.mProjectileType = ProjectileType.PROJECTILE_FIREBALL;
			this.mHitTorchwoodGridX = aGridX;
			this.mApp.PlayFoley(FoleyType.FOLEY_FIREPEA);
			float num = -25f;
			float num2 = -25f;
			Reanimation reanimation = this.mApp.AddReanimation(0f, 0f, 0, ReanimationType.REANIM_FIRE_PEA);
			if (this.mMotionType == ProjectileMotion.MOTION_BACKWARDS)
			{
				reanimation.OverrideScale(-1f, 1f);
				num += 80f;
			}
			reanimation.SetPosition((this.mPosX + num) * Constants.S, (this.mPosY + num2) * Constants.S);
			reanimation.mLoopType = ReanimLoopType.REANIM_LOOP;
			reanimation.mAnimRate = TodCommon.RandRangeFloat(50f, 80f);
			GlobalMembersAttachment.AttachReanim(ref this.mAttachmentID, reanimation, num, num2);
		}

		public void ConvertToPea(int aGridX)
		{
			if (this.mHitTorchwoodGridX == aGridX)
			{
				return;
			}
			GlobalMembersAttachment.AttachmentDie(ref this.mAttachmentID);
			this.mProjectileType = ProjectileType.PROJECTILE_PEA;
			this.mHitTorchwoodGridX = aGridX;
			this.mApp.PlayFoley(FoleyType.FOLEY_THROW);
		}

		public bool IsSplashDamage(Zombie theZombie)
		{
			return (this.mProjectileType != ProjectileType.PROJECTILE_FIREBALL || theZombie == null || !theZombie.IsFireResistant()) && (this.mProjectileType == ProjectileType.PROJECTILE_MELON || this.mProjectileType == ProjectileType.PROJECTILE_WINTERMELON || this.mProjectileType == ProjectileType.PROJECTILE_FIREBALL);
		}

		public void PlayImpactSound(Zombie theZombie)
		{
			bool flag = true;
			bool flag2 = true;
			if (this.mProjectileType == ProjectileType.PROJECTILE_KERNEL)
			{
				this.mApp.PlayFoley(FoleyType.FOLEY_KERNEL_SPLAT);
				flag = false;
				flag2 = false;
			}
			else if (this.mProjectileType == ProjectileType.PROJECTILE_BUTTER)
			{
				this.mApp.PlayFoley(FoleyType.FOLEY_BUTTER);
				flag2 = false;
			}
			else if (this.mProjectileType == ProjectileType.PROJECTILE_FIREBALL && this.IsSplashDamage(theZombie))
			{
				this.mApp.PlayFoley(FoleyType.FOLEY_IGNITE);
				flag = false;
				flag2 = false;
			}
			else if (this.mProjectileType == ProjectileType.PROJECTILE_MELON || this.mProjectileType == ProjectileType.PROJECTILE_WINTERMELON)
			{
				this.mApp.PlayFoley(FoleyType.FOLEY_MELONIMPACT);
				flag2 = false;
			}
			if (flag)
			{
				if (theZombie != null && theZombie.mHelmType == HelmType.HELMTYPE_PAIL)
				{
					this.mApp.PlayFoley(FoleyType.FOLEY_SHIELD_HIT);
					flag2 = false;
				}
				else if (theZombie != null && theZombie.mHelmType == HelmType.HELMTYPE_TRAFFIC_CONE)
				{
					this.mApp.PlayFoley(FoleyType.FOLEY_PLASTIC_HIT);
				}
				else if (theZombie != null && theZombie.mHelmType == HelmType.HELMTYPE_DIGGER)
				{
					this.mApp.PlayFoley(FoleyType.FOLEY_PLASTIC_HIT);
				}
				else if (theZombie != null && theZombie.mHelmType == HelmType.HELMTYPE_FOOTBALL)
				{
					this.mApp.PlayFoley(FoleyType.FOLEY_PLASTIC_HIT);
				}
			}
			if (flag2)
			{
				this.mApp.PlayFoley(FoleyType.FOLEY_SPLAT);
			}
		}

		public bool IsZombieHitBySplash(Zombie theZombie)
		{
			TRect projectileRect = this.GetProjectileRect();
			if (this.mProjectileType == ProjectileType.PROJECTILE_FIREBALL)
			{
				projectileRect.mWidth = 100;
			}
			int num = theZombie.mRow - this.mRow;
			TRect zombieRect = theZombie.GetZombieRect();
			if (theZombie.IsFireResistant() && this.mProjectileType == ProjectileType.PROJECTILE_FIREBALL)
			{
				return false;
			}
			if (theZombie.mZombieType == ZombieType.ZOMBIE_BOSS)
			{
				num = 0;
			}
			if (this.mProjectileType == ProjectileType.PROJECTILE_FIREBALL)
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
			if (!theZombie.EffectedByDamage((uint)this.mDamageRangeFlags))
			{
				return false;
			}
			int rectOverlap = GameConstants.GetRectOverlap(projectileRect, zombieRect);
			return rectOverlap >= 0;
		}

		public bool PeaAboutToHitTorchwood()
		{
			if (this.mMotionType != ProjectileMotion.MOTION_STRAIGHT)
			{
				return false;
			}
			if (this.mProjectileType != ProjectileType.PROJECTILE_PEA && this.mProjectileType != ProjectileType.PROJECTILE_SNOWPEA && this.mProjectileType != ProjectileType.PROJECTILE_ZOMBIE_PEA_MIND_CONTROL)
			{
				return false;
			}
			int count = this.mBoard.mPlants.Count;
			for (int i = 0; i < count; i++)
			{
				Plant plant = this.mBoard.mPlants[i];
				if (!plant.mDead && plant.mSeedType == SeedType.SEED_TORCHWOOD && plant.mRow == this.mRow && !plant.NotOnGround() && this.mHitTorchwoodGridX != plant.mPlantCol)
				{
					TRect plantAttackRect = plant.GetPlantAttackRect(PlantWeapon.WEAPON_PRIMARY);
					TRect projectileRect = this.GetProjectileRect();
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
