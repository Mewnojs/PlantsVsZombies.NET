using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Sexy;
using Sexy.TodLib;

namespace Lawn
{
	public/*internal*/ class ZombiePileWidget : Widget, ButtonListener
	{
		private static int SortGems(ZombiePileObject a, ZombiePileObject b)
		{
			return (int)(100f * (Math.Abs(a.gemSpeedX) - Math.Abs(b.gemSpeedX)));
		}

		public ZombiePileWidget(LawnApp theApp)
		{
			this.mApp = theApp;
			this.mWidth = Constants.BOARD_WIDTH;
			this.mPileHeight = (int)(this.mApp.mPlayerInfo.mZombiesKilled / (long)ZombiePileWidget.mZombieScale);
			this.mPileHeight = Math.Min(this.mPileHeight, ZombiePileWidget.maxHeight);
			this.mHeight = Constants.BOARD_HEIGHT + this.CalculatePileHeight(this.mPileHeight) + 300;
			this.mY = -this.mHeight;
			this.mScreenTop = this.mHeight - Constants.BOARD_HEIGHT;
			List<ZombiePileObject> list = new List<ZombiePileObject>();
			list.Add(new ZombiePileObject(5, ZombiePileObjectType.OBJECT_YELLOW_CLOUD));
			list.Add(new ZombiePileObject(15, ZombiePileObjectType.OBJECT_YELLOW_CLOUD));
			list.Add(new ZombiePileObject(30, ZombiePileObjectType.OBJECT_YELLOW_CLOUD));
			list.Add(new ZombiePileObject(45, ZombiePileObjectType.OBJECT_AIRPLANE));
			list.Add(new ZombiePileObject(50, ZombiePileObjectType.OBJECT_YELLOW_CLOUD));
			list.Add(new ZombiePileObject(60, ZombiePileObjectType.OBJECT_YELLOW_CLOUD));
			list.Add(new ZombiePileObject(85, ZombiePileObjectType.OBJECT_SATELLITE));
			list.Add(new ZombiePileObject(110, ZombiePileObjectType.OBJECT_MOON));
			list.Add(new ZombiePileObject(140, ZombiePileObjectType.OBJECT_ASTRONAUT));
			list.Add(new ZombiePileObject(160, ZombiePileObjectType.OBJECT_PEGGLE_URSAMAJOR));
			List<ZombiePileObject> list2 = new List<ZombiePileObject>();
			for (int i = 0; i < 100; i++)
			{
				list2.Add(new ZombiePileObject(180 + this.rand.Next(4) - 2, ZombiePileObjectType.OBJECT_GEM0 + this.rand.Next(7)));
			}
			list2.Sort(new Comparison<ZombiePileObject>(ZombiePileWidget.SortGems));
			list.AddRange(list2);
			list.Add(new ZombiePileObject(200, ZombiePileObjectType.OBJECT_BLACKHOLE));
			if (!theApp.mPlayerInfo.mSeenLeaderboardArrow && this.mPileHeight >= 1)
			{
				list.Add(new ZombiePileObject(0, ZombiePileObjectType.OBJECT_ARROW));
				theApp.mPlayerInfo.mSeenLeaderboardArrow = true;
			}
			this.gPileObjects = list.ToArray();
			this.SetObjectYVals();
			this.spaceColor = Color.Black;
			this.skyColor = new Color(134, 169, 197);
			int num = Constants.BOARD_HEIGHT + this.CalculatePileHeight(ZombiePileWidget.maxHeight);
			int num2 = Constants.BOARD_HEIGHT + this.CalculatePileHeight(ZombiePileWidget.skyEndHeight);
			int num3 = Constants.BOARD_HEIGHT + this.CalculatePileHeight(ZombiePileWidget.spaceStartHeight);
			this.skyBottomLeft = new TriVertex(-1f, (float)this.mScreenTop);
			this.skyBottomRight = new TriVertex((float)Constants.BOARD_WIDTH, (float)this.mScreenTop);
			this.transferBottomLeft = new TriVertex(-1f, (float)(this.mHeight - num2));
			this.transferBottomRight = new TriVertex((float)Constants.BOARD_WIDTH, (float)(this.mHeight - num2));
			this.spaceBottomLeft = new TriVertex(-1f, (float)(this.mHeight - num3));
			this.spaceBottomRight = new TriVertex((float)Constants.BOARD_WIDTH, (float)(this.mHeight - num3));
			this.spaceTopLeft = new TriVertex(-1f, (float)(this.mHeight - num));
			this.spaceTopRight = new TriVertex((float)Constants.BOARD_WIDTH, (float)(this.mHeight - num));
			this.screenBottomLeft = new TriVertex(-1f, (float)this.mHeight);
			this.screenBottomRight = new TriVertex((float)Constants.BOARD_WIDTH, (float)this.mHeight);
			this.mVasebreakerButton = GameButton.MakeNewButton(3, this, "", null, AtlasResources.IMAGE_REANIM_SELECTORSCREEN_LEADERBOARD_VASEBREAKER, null, AtlasResources.IMAGE_REANIM_SELECTORSCREEN_LEADERBOARD_VASEBREAKER_HIGHLIGHT);
			this.mVasebreakerButton.Resize(Constants.LeaderboardScreen_Vasebreaker_Button_X, this.mScreenTop + Constants.LeaderboardScreen_Vasebreaker_Button_Y, AtlasResources.IMAGE_REANIM_SELECTORSCREEN_LEADERBOARD_VASEBREAKER_HIGHLIGHT.mWidth, AtlasResources.IMAGE_REANIM_SELECTORSCREEN_LEADERBOARD_VASEBREAKER_HIGHLIGHT.mHeight);
			this.mVasebreakerButton.mTranslateX = (this.mVasebreakerButton.mTranslateY = 0);
			this.mIZombieButton = GameButton.MakeNewButton(4, this, "", null, AtlasResources.IMAGE_REANIM_SELECTORSCREEN_LEADERBOARD_IZOMBIE, null, AtlasResources.IMAGE_REANIM_SELECTORSCREEN_LEADERBOARD_IZOMBIE_HIGHLIGHT);
			this.mIZombieButton.Resize(Constants.LeaderboardScreen_IZombie_Button_X, this.mScreenTop + Constants.LeaderboardScreen_IZombie_Button_Y, AtlasResources.IMAGE_REANIM_SELECTORSCREEN_LEADERBOARD_IZOMBIE_HIGHLIGHT.mWidth, AtlasResources.IMAGE_REANIM_SELECTORSCREEN_LEADERBOARD_IZOMBIE_HIGHLIGHT.mHeight);
			this.mIZombieButton.mTranslateX = (this.mIZombieButton.mTranslateY = 0);
			this.mZombiesKilledButton = GameButton.MakeNewButton(5, this, "", null, AtlasResources.IMAGE_REANIM_SELECTORSCREEN_LEADERBOARD_KILLED, null, AtlasResources.IMAGE_REANIM_SELECTORSCREEN_LEADERBOARD_KILLED_HIGHLIGHT);
			this.mZombiesKilledButton.Resize(Constants.LeaderboardScreen_Killed_Button_X, this.mScreenTop + Constants.LeaderboardScreen_Killed_Button_Y, AtlasResources.IMAGE_REANIM_SELECTORSCREEN_LEADERBOARD_KILLED_HIGHLIGHT.mWidth, AtlasResources.IMAGE_REANIM_SELECTORSCREEN_LEADERBOARD_KILLED_HIGHLIGHT.mHeight);
			this.mZombiesKilledButton.mTranslateX = (this.mZombiesKilledButton.mTranslateY = 0);
			this.mBackButton = GameButton.MakeNewButton(0, this, "", null, AtlasResources.IMAGE_PILE_LEADERBOARD_BACK_BUTTON, AtlasResources.IMAGE_PILE_LEADERBOARD_BACK_BUTTON, AtlasResources.IMAGE_PILE_LEADERBOARD_BACK_BUTTON_HIGHLIGHT);
			this.mBackButton.Resize(0, this.mScreenTop + Constants.BOARD_HEIGHT - AtlasResources.IMAGE_PILE_LEADERBOARD_BACK_BUTTON.mHeight, AtlasResources.IMAGE_PILE_LEADERBOARD_BACK_BUTTON.mWidth, AtlasResources.IMAGE_PILE_LEADERBOARD_BACK_BUTTON.mHeight);
			this.mBackButton.mTranslateX = (this.mBackButton.mTranslateY = 0);
			this.AddWidget(this.mVasebreakerButton);
			this.AddWidget(this.mIZombieButton);
			this.AddWidget(this.mZombiesKilledButton);
			this.AddWidget(this.mBackButton);
			LeaderBoardComm.LoadResults(LeaderboardGameMode.Adventure);
			this.friendMarkers = new ZombiePileMarker[10];
			int num4 = (int)((double)num3 * 1.75 * 0.20000000298023224);
			this.stars = new ZombiePileWidget.Star[this.mPileHeight * 10];
			for (int j = 0; j < this.stars.Length; j++)
			{
				this.stars[j] = new ZombiePileWidget.Star(new Point(this.rand.Next(this.mWidth), this.rand.Next(num4)), (float)this.rand.NextDouble() / 2f, this.rand.Next(3));
			}
		}

		public override bool DoScroll(_Touch touch)
		{
			CGPoint absPos = this.GetAbsPos();
			int theX = (int)(touch.location.x - absPos.x);
			int theY = (int)(touch.location.y - absPos.y);
			return !this.mBackButton.Contains(theX, theY) && !this.mIZombieButton.Contains(theX, theY) && !this.mZombiesKilledButton.Contains(theX, theY) && !this.mVasebreakerButton.Contains(theX, theY);
		}

		public void SetGray(bool aGrayed)
		{
			this.mGrayed = aGrayed;
		}

		public int CalculatePileHeight(int height)
		{
			int num = 0;
			int num2 = 0;
			for (int i = 0; i < height; i++)
			{
				num++;
				switch (num)
				{
				case 1:
					num2 += AtlasResources.IMAGE_PILE_ZOMBIE_PILE_1A.mHeight;
					break;
				case 2:
					num2 += AtlasResources.IMAGE_PILE_ZOMBIE_PILE_2A.mHeight;
					break;
				case 3:
					num2 += AtlasResources.IMAGE_PILE_ZOMBIE_PILE_1B.mHeight;
					break;
				case 4:
					num2 += AtlasResources.IMAGE_PILE_ZOMBIE_PILE_2B.mHeight;
					break;
				}
				if (num >= 4)
				{
					num = 0;
				}
				num2 -= ZombiePileWidget.mZombieSpace;
			}
			return num2;
		}

		public override void Update()
		{
			base.Update();
			this.UpdatePileObjects();
			if (!this.mLoadedFriends)
			{
				int num = LeaderBoardComm.LoadResults(LeaderboardGameMode.Adventure);
				if (num >= 0)
				{
					int signedInGamerIndex = LeaderBoardComm.GetSignedInGamerIndex(LeaderboardState.Adventure);
					int num2 = LeaderBoardComm.LoadResults(LeaderboardGameMode.Adventure);
					long leaderboardScore = LeaderBoardComm.GetLeaderboardScore(signedInGamerIndex, LeaderboardState.Adventure);
					if (leaderboardScore > this.mApp.mPlayerInfo.mZombiesKilled)
					{
						this.mApp.mPlayerInfo.mZombiesKilled = leaderboardScore;
					}
					bool flag = false;
					int num3 = 1;
					int num4 = 0;
					while (!flag)
					{
						if (signedInGamerIndex + num3 < num2)
						{
							Gamer leaderboardGamer = LeaderBoardComm.GetLeaderboardGamer(signedInGamerIndex + num3, LeaderboardState.Adventure);
							int num5 = (int)(LeaderBoardComm.GetLeaderboardScore(signedInGamerIndex + num3, LeaderboardState.Adventure) / (long)ZombiePileWidget.mZombieScale);
							num5 = Math.Min(num5, ZombiePileWidget.maxHeight);
							ZombiePileMarker zombiePileMarker = new ZombiePileMarker();
							zombiePileMarker.mGamer = leaderboardGamer;
							zombiePileMarker.mHeight = num5;
							this.friendMarkers[num4] = zombiePileMarker;
							num4++;
						}
						if (num4 >= 5)
						{
							break;
						}
						if (signedInGamerIndex - num3 >= 0)
						{
							Gamer leaderboardGamer2 = LeaderBoardComm.GetLeaderboardGamer(signedInGamerIndex - num3, LeaderboardState.Adventure);
							int num6 = (int)(LeaderBoardComm.GetLeaderboardScore(signedInGamerIndex - num3, LeaderboardState.Adventure) / (long)ZombiePileWidget.mZombieScale);
							num6 = Math.Min(num6, ZombiePileWidget.maxHeight);
							ZombiePileMarker zombiePileMarker2 = new ZombiePileMarker();
							zombiePileMarker2.mGamer = leaderboardGamer2;
							zombiePileMarker2.mHeight = num6;
							this.friendMarkers[num4] = zombiePileMarker2;
							num4++;
						}
						if (num4 >= 5 || (signedInGamerIndex - num3 <= 0 && signedInGamerIndex + num3 > num2))
						{
							flag = true;
						}
						num3++;
					}
					this.mLoadedFriends = true;
				}
			}
		}

		public override void DrawOverlay(Graphics g)
		{
		}

		public override void Draw(Graphics g)
		{
			g.DrawImage(Resources.IMAGE_LEADERBOARDSCREEN_BACKGROUND, 0, this.mScreenTop);
			g.DrawTriangle(this.skyBottomLeft, this.skyColor, this.skyBottomRight, this.skyColor, this.transferBottomLeft, this.skyColor, Graphics.DrawMode.DRAWMODE_NORMAL);
			g.DrawTriangle(this.skyBottomRight, this.skyColor, this.transferBottomRight, this.skyColor, this.transferBottomLeft, this.skyColor, Graphics.DrawMode.DRAWMODE_NORMAL);
			g.DrawTriangle(this.transferBottomLeft, this.skyColor, this.transferBottomRight, this.skyColor, this.spaceBottomLeft, this.skyColor, Graphics.DrawMode.DRAWMODE_NORMAL);
			g.DrawTriangle(this.transferBottomRight, this.skyColor, this.spaceBottomRight, this.skyColor, this.spaceBottomLeft, this.skyColor, Graphics.DrawMode.DRAWMODE_NORMAL);
			g.DrawTriangle(this.spaceBottomLeft, this.spaceColor, this.spaceBottomRight, this.spaceColor, this.spaceTopLeft, this.spaceColor, Graphics.DrawMode.DRAWMODE_NORMAL);
			g.DrawTriangle(this.spaceBottomRight, this.spaceColor, this.spaceTopRight, this.spaceColor, this.spaceTopLeft, this.spaceColor, Graphics.DrawMode.DRAWMODE_NORMAL);
			g.DrawImage(Resources.IMAGE_EDGE_OF_SPACE, 0, (int)this.spaceBottomLeft.y - Resources.IMAGE_EDGE_OF_SPACE.mHeight / 2, Constants.BOARD_WIDTH, Resources.IMAGE_EDGE_OF_SPACE.mHeight);
			float y = this.spaceBottomRight.y;
			int board_HEIGHT = Constants.BOARD_HEIGHT;
			int a = 255;
			g.SetColorizeImages(true);
			Color color = new Color(255, 255, 255, a);
			g.SetColor(color);
			int mTransY = g.mTransY;
			g.mTransY = (int)((float)mTransY * 0.2f);
			for (int i = 0; i < this.stars.Length; i++)
			{
				g.SetScale(this.stars[i].size);
				color.A = (byte)(200 + this.rand.Next(55));
				g.SetColor(color);
				g.DrawImageCel(AtlasResources.IMAGE_ICE_SPARKLES, this.stars[i].pos.X, this.stars[i].pos.Y, this.stars[i].cel, 0);
			}
			g.SetScale(1f);
			g.SetColor(Color.White);
			g.mTransY = mTransY;
			this.DrawPileObjects(g);
			int num = this.mScreenTop - 100;
			int num2 = 0;
			int num3 = 0;
			for (int j = 0; j < this.mPileHeight; j++)
			{
				num2++;
				switch (num2)
				{
				case 1:
					g.DrawImage(AtlasResources.IMAGE_PILE_ZOMBIE_PILE_1A, Constants.Leaderboard_Pile_1_X, num);
					num3 = AtlasResources.IMAGE_PILE_ZOMBIE_PILE_1A.mHeight;
					break;
				case 2:
					g.DrawImage(AtlasResources.IMAGE_PILE_ZOMBIE_PILE_2A, Constants.Leaderboard_Pile_1_X, num);
					num3 = AtlasResources.IMAGE_PILE_ZOMBIE_PILE_2A.mHeight;
					break;
				case 3:
					g.DrawImage(AtlasResources.IMAGE_PILE_ZOMBIE_PILE_1B, Constants.Leaderboard_Pile_1_X, num);
					num3 = AtlasResources.IMAGE_PILE_ZOMBIE_PILE_1B.mHeight;
					break;
				case 4:
					g.DrawImage(AtlasResources.IMAGE_PILE_ZOMBIE_PILE_2B, Constants.Leaderboard_Pile_1_X, num);
					num3 = AtlasResources.IMAGE_PILE_ZOMBIE_PILE_2B.mHeight;
					break;
				}
				if (num2 >= 4)
				{
					num2 = 0;
				}
				num -= num3 - ZombiePileWidget.mZombieSpace;
			}
			if (this.mPileHeight == ZombiePileWidget.maxHeight)
			{
				g.DrawImage(AtlasResources.IMAGE_PILE_ZOMBIE_PILE_TOP, Constants.Leaderboard_Pile_1_X + 35, num - 20);
			}
			g.DrawImage(AtlasResources.IMAGE_PILE_ZOMBIE_PILE_2B, Constants.Leaderboard_Pile_1_X, this.mScreenTop - 80);
			g.DrawImage(AtlasResources.IMAGE_PILE_ZOMBIE_PILE_BASE, Constants.LeaderboardScreen_PileBase_X, this.mScreenTop + Constants.LeaderboardScreen_PileBase_Y);
			g.DrawImage(AtlasResources.IMAGE_PILE_CLOUD_RING, 0, (int)this.spaceBottomLeft.y - Resources.IMAGE_EDGE_OF_SPACE.mHeight / 2 + Constants.LeaderboardScreen_EdgeOfSpace_Overlay_Offset, Constants.BOARD_WIDTH, Resources.IMAGE_EDGE_OF_SPACE.mHeight);
			if (this.mLoadedFriends)
			{
				this.DrawPileMarkers(g);
			}
			base.DeferOverlay();
		}

		public void DrawPileMarkers(Graphics g)
		{
			for (int i = 0; i < this.friendMarkers.Length; i++)
			{
				if (this.friendMarkers[i] != null && this.friendMarkers[i].mGamer != null)
				{
					int num = this.mScreenTop - this.CalculatePileHeight(this.friendMarkers[i].mHeight) - AtlasResources.IMAGE_PILE_SIGN_OVERLAY.mHeight / 2;
					g.DrawImage(AtlasResources.IMAGE_PILE_BALLOON, 108, num - 152);
					g.DrawImage(AtlasResources.IMAGE_PILE_SIGN_OVERLAY, 0, num);
					Image gamerImage = LeaderBoardComm.GetGamerImage(this.friendMarkers[i].mGamer);
					if (gamerImage != null)
					{
						g.DrawImage(gamerImage, 51, num + 61);
					}
					TodCommon.TodDrawString(g, this.friendMarkers[i].mGamer.Gamertag, 126, num + 79, Resources.FONT_DWARVENTODCRAFT15, SexyColor.White, 211, DrawStringJustification.DS_ALIGN_LEFT);
				}
			}
		}

		public void SetObjectYVals()
		{
			for (int i = 0; i < this.gPileObjects.Length; i++)
			{
				this.gPileObjects[i].mY = (int)((float)this.mScreenTop * 0.4f - (float)this.CalculatePileHeight(this.gPileObjects[i].mHeight) * 0.4f);
				if (this.gPileObjects[i].mType == ZombiePileObjectType.OBJECT_ARROW)
				{
					this.gPileObjects[i].mY = this.mScreenTop - this.CalculatePileHeight(this.gPileObjects[i].mHeight);
				}
			}
		}

		public void DrawPileObjects(Graphics g)
		{
			int mTransY = g.mTransY;
			g.mTransY = (int)((float)mTransY * 0.4f);
			for (int i = 0; i < this.gPileObjects.Length; i++)
			{
				ZombiePileObject zombiePileObject = this.gPileObjects[i];
				if (zombiePileObject.mHeight <= this.mPileHeight)
				{
					switch (zombiePileObject.mType)
					{
					case ZombiePileObjectType.OBJECT_BALLOON:
						g.DrawImage(AtlasResources.IMAGE_PILE_BALLOON, 40f, (float)zombiePileObject.mY + zombiePileObject.mOffsetY);
						break;
					case ZombiePileObjectType.OBJECT_YELLOW_CLOUD:
						g.DrawImage(AtlasResources.IMAGE_PILE_YELLOW_CLOUD, (int)zombiePileObject.mOffsetX, zombiePileObject.mY + (int)zombiePileObject.mOffsetY, AtlasResources.IMAGE_PILE_YELLOW_CLOUD.mWidth, AtlasResources.IMAGE_PILE_YELLOW_CLOUD.mHeight);
						break;
					case ZombiePileObjectType.OBJECT_AIRPLANE:
						g.DrawImage(AtlasResources.IMAGE_PILE_AIRPLANE, zombiePileObject.mOffsetX, (float)zombiePileObject.mY + zombiePileObject.mOffsetY);
						break;
					case ZombiePileObjectType.OBJECT_MOON:
						g.DrawImage(AtlasResources.IMAGE_PILE_MOON, 40, zombiePileObject.mY);
						break;
					case ZombiePileObjectType.OBJECT_SATELLITE:
						g.DrawImage(AtlasResources.IMAGE_PILE_SATELLITE, zombiePileObject.mOffsetX, (float)zombiePileObject.mY + zombiePileObject.mOffsetY);
						break;
					case ZombiePileObjectType.OBJECT_PEGGLE_URSAMAJOR:
						g.DrawImage(AtlasResources.IMAGE_PILE_PEGGLE_URSAMAJOR, 30, zombiePileObject.mY);
						break;
					case ZombiePileObjectType.OBJECT_ASTRONAUT:
					{
						Graphics @new = Graphics.GetNew(g);
						@new.mTransX += (int)zombiePileObject.mOffsetX;
						@new.mTransY += zombiePileObject.mY + (int)zombiePileObject.mOffsetY;
						zombiePileObject.mReanim.Draw(@new);
						@new.PrepareForReuse();
						break;
					}
					case ZombiePileObjectType.OBJECT_BLACKHOLE:
					{
						Matrix world = Matrix.Identity;
						new TRect(0, 0, Resources.IMAGE_BLACKHOLE.mWidth, Resources.IMAGE_BLACKHOLE.mHeight);
						int num = (int)((float)g.mTransY / 0.4f * 1.6f);
						this.blackHoleView = Matrix.CreateLookAt(new Vector3(0f, (float)(-(float)num), -1000f), Vector3.Zero, Vector3.UnitY);
						this.blackHoleVerts[0].Position.Y = (float)(-(float)Resources.IMAGE_BLACKHOLE.mHeight) / 4f - (float)num;
						this.blackHoleVerts[1].Position.Y = (float)(-(float)Resources.IMAGE_BLACKHOLE.mHeight) / 4f - (float)num;
						this.blackHoleVerts[2].Position.Y = (float)Resources.IMAGE_BLACKHOLE.mHeight / 4f - (float)num;
						this.blackHoleVerts[3].Position.Y = (float)Resources.IMAGE_BLACKHOLE.mHeight / 4f - (float)num;
						Vector3 vector = new Vector3(0f, (float)num, 0f);
						Vector3 position = new Vector3(-60f, 80f, 0f);
						world = Matrix.CreateTranslation(vector) * Matrix.CreateScale(4f) * Matrix.CreateRotationZ(zombiePileObject.mCounter) * Matrix.CreateRotationX(-0.85f) * Matrix.CreateTranslation(-vector) * Matrix.CreateTranslation(position);
						g.DrawImageWithBasicEffect(Resources.IMAGE_BLACKHOLE, this.blackHoleVerts, this.blackHoleIndices, world, this.blackHoleView, this.blackHoleProjection);
						break;
					}
					case ZombiePileObjectType.OBJECT_ARROW:
						g.DrawImageRotatedScaled(AtlasResources.IMAGE_DOWNARROW, 160f, (float)(zombiePileObject.mY + (int)zombiePileObject.mOffsetY + mTransY + 50), 3.141592653589793, (float)(AtlasResources.IMAGE_DOWNARROW.mWidth / 2), (float)(AtlasResources.IMAGE_DOWNARROW.mHeight / 2), new TRect?(new TRect(0, 0, AtlasResources.IMAGE_DOWNARROW.mWidth, AtlasResources.IMAGE_DOWNARROW.mHeight)), 10, 10);
						break;
					case ZombiePileObjectType.OBJECT_GEM0:
					case ZombiePileObjectType.OBJECT_GEM1:
					case ZombiePileObjectType.OBJECT_GEM2:
					case ZombiePileObjectType.OBJECT_GEM3:
					case ZombiePileObjectType.OBJECT_GEM4:
					case ZombiePileObjectType.OBJECT_GEM5:
					case ZombiePileObjectType.OBJECT_GEM6:
					{
						float num2 = Math.Abs(zombiePileObject.gemSpeedX / 5f);
						num2 = 1f / num2;
						g.DrawImageRotatedScaled(zombiePileObject.gemImage, (float)((int)zombiePileObject.mOffsetX + g.mTransX), (float)(zombiePileObject.mY + (int)zombiePileObject.mOffsetY + g.mTransY), (double)zombiePileObject.mCounter, (float)((int)((float)zombiePileObject.gemImage.mWidth / num2)), (float)((int)((float)zombiePileObject.gemImage.mHeight / num2)), new TRect?(new TRect(0, 0, zombiePileObject.gemImage.mWidth, zombiePileObject.gemImage.mHeight)), (int)(num2 * (float)zombiePileObject.gemImage.mWidth), (int)(num2 * (float)zombiePileObject.gemImage.mHeight));
						break;
					}
					}
				}
			}
			g.mTransY = mTransY;
		}

		public void UpdatePileObjects()
		{
			for (int i = 0; i < this.gPileObjects.Length; i++)
			{
				ZombiePileObject zombiePileObject = this.gPileObjects[i];
				if (zombiePileObject.mHeight <= this.mPileHeight)
				{
					switch (zombiePileObject.mType)
					{
					case ZombiePileObjectType.OBJECT_BALLOON:
						zombiePileObject.mCounter += 0.1f;
						if ((double)zombiePileObject.mCounter > 6.283185307179586)
						{
							zombiePileObject.mCounter -= 6.2831855f;
						}
						zombiePileObject.mOffsetY = (float)((int)(Math.Sin((double)zombiePileObject.mCounter) * 5.0));
						break;
					case ZombiePileObjectType.OBJECT_YELLOW_CLOUD:
						zombiePileObject.mCounter -= 1f;
						zombiePileObject.mOffsetX = (float)((int)zombiePileObject.mCounter);
						if (zombiePileObject.mOffsetX < (float)(-(float)AtlasResources.IMAGE_PILE_YELLOW_CLOUD.mWidth))
						{
							zombiePileObject.mCounter = (float)Constants.BOARD_WIDTH;
							zombiePileObject.mOffsetX = (float)((int)zombiePileObject.mCounter);
						}
						break;
					case ZombiePileObjectType.OBJECT_AIRPLANE:
						zombiePileObject.mOffsetX += 6f;
						zombiePileObject.mOffsetY += zombiePileObject.mCounter;
						zombiePileObject.mCounter -= 0.01f * zombiePileObject.mCounter;
						if (zombiePileObject.mOffsetX > (float)Constants.BOARD_WIDTH)
						{
							zombiePileObject.mOffsetX = (float)(-2 * AtlasResources.IMAGE_PILE_AIRPLANE.mWidth);
							zombiePileObject.mOffsetY = -100f;
							zombiePileObject.mCounter = 3f;
						}
						break;
					case ZombiePileObjectType.OBJECT_SATELLITE:
						zombiePileObject.mOffsetX -= 9f;
						zombiePileObject.mOffsetY += 1f;
						if (zombiePileObject.mOffsetX < (float)(-(float)AtlasResources.IMAGE_PILE_SATELLITE.mWidth))
						{
							zombiePileObject.mOffsetX = (float)(Constants.BOARD_WIDTH + AtlasResources.IMAGE_PILE_SATELLITE.mWidth);
							zombiePileObject.mOffsetY = -50f;
						}
						break;
					case ZombiePileObjectType.OBJECT_ASTRONAUT:
						zombiePileObject.mReanim.Update();
						zombiePileObject.mCounter += 0.025f;
						if ((double)zombiePileObject.mCounter > 6.283185307179586)
						{
							zombiePileObject.mCounter -= 6.2831855f;
						}
						zombiePileObject.mOffsetY = (float)(-(float)((int)(Math.Sin((double)zombiePileObject.mCounter) * 80.0)));
						zombiePileObject.mOffsetX -= 6f;
						if (zombiePileObject.mReanim.mLastFrameTime > 0.95f)
						{
							zombiePileObject.mOffsetX = 600f;
							zombiePileObject.mCounter = 0f;
						}
						break;
					case ZombiePileObjectType.OBJECT_BLACKHOLE:
						zombiePileObject.mCounter += 0.05f;
						if ((double)zombiePileObject.mCounter > 6.283185307179586)
						{
							zombiePileObject.mCounter -= 6.2831855f;
						}
						break;
					case ZombiePileObjectType.OBJECT_ARROW:
						zombiePileObject.mCounter += 0.1f;
						if ((double)zombiePileObject.mCounter > 6.283185307179586)
						{
							zombiePileObject.mCounter -= 6.2831855f;
						}
						zombiePileObject.mOffsetY = (float)((int)(Math.Sin((double)zombiePileObject.mCounter) * 10.0));
						break;
					case ZombiePileObjectType.OBJECT_GEM0:
					case ZombiePileObjectType.OBJECT_GEM1:
					case ZombiePileObjectType.OBJECT_GEM2:
					case ZombiePileObjectType.OBJECT_GEM3:
					case ZombiePileObjectType.OBJECT_GEM4:
					case ZombiePileObjectType.OBJECT_GEM5:
					case ZombiePileObjectType.OBJECT_GEM6:
						zombiePileObject.mOffsetX += zombiePileObject.gemSpeedX;
						zombiePileObject.mCounter += zombiePileObject.gemRotationSpeed;
						zombiePileObject.mOffsetY += zombiePileObject.gemSpeedY;
						if (zombiePileObject.gemSpeedX > 0f)
						{
							if (zombiePileObject.mOffsetX > (float)(Constants.BOARD_WIDTH + 200))
							{
								zombiePileObject.mOffsetX = (float)(-(float)zombiePileObject.gemImage.mWidth - 100);
								zombiePileObject.mOffsetY = 50f;
								zombiePileObject.mCounter = 0f;
							}
						}
						else if (zombiePileObject.mOffsetX < (float)(-(float)zombiePileObject.gemImage.mWidth))
						{
							zombiePileObject.mOffsetX = (float)(Constants.BOARD_WIDTH + 200);
							zombiePileObject.mOffsetY = 50f;
							zombiePileObject.mCounter = 0f;
						}
						break;
					}
					this.gPileObjects[i] = zombiePileObject;
				}
			}
		}

		public void ButtonPress(int theId)
		{
			this.mApp.PlaySample(Resources.SOUND_GRAVEBUTTON);
		}

		public void ButtonDepress(int theId)
		{
			if (theId == 0)
			{
				this.mApp.KillLeaderboardScreen();
				this.mApp.DoBackToMain(false);
				return;
			}
			if (theId == 4)
			{
				this.ShowLeaderboard(LeaderBoardType.LEADERBOARD_TYPE_IZOMBIE);
				return;
			}
			if (theId == 3)
			{
				this.ShowLeaderboard(LeaderBoardType.LEADERBOARD_TYPE_VASEBREAKER);
				return;
			}
			if (theId == 5)
			{
				this.ShowLeaderboard(LeaderBoardType.LEADERBOARD_TYPE_KILLED);
			}
		}

		public void ShowLeaderboard(LeaderBoardType aType)
		{
			this.mApp.ShowLeaderboardDialog(aType);
		}

		public override bool BackButtonPress()
		{
			this.mApp.KillLeaderboardScreen();
			this.mApp.DoBackToMain(false);
			return true;
		}

		public override void MouseUp(int x, int y, int theClickCount)
		{
			ScrollWidget scrollWidget = (ScrollWidget)this.mParent;
			scrollWidget.ScrollToBottom(true);
		}

		public void UpdateToolTip()
		{
		}

		public void ButtonMouseMove(int id, int x, int y)
		{
		}

		public void ButtonMouseLeave(int id)
		{
		}

		public void ButtonMouseEnter(int id)
		{
			this.mApp.PlayFoley(FoleyType.FOLEY_BLEEP);
		}

		public void ButtonMouseTick(int id)
		{
		}

		public void ButtonPress(int id, int id2)
		{
			if (id != 0)
			{
				this.mApp.PlaySample(Resources.SOUND_GRAVEBUTTON);
			}
			else
			{
				this.mApp.PlaySample(Resources.SOUND_TAP);
			}
			this.mApp.PlayFoley(FoleyType.FOLEY_BLEEP);
		}

		public void ButtonDownTick(int id)
		{
		}

		public const float starParallax = 0.2f;

		public const float pileObjectParallax = 0.4f;

		public LawnApp mApp;

		public DialogButton mVasebreakerButton;

		public DialogButton mIZombieButton;

		public DialogButton mZombiesKilledButton;

		public NewLawnButton mBackButton;

		public TriVertex skyBottomLeft;

		public TriVertex skyBottomRight;

		public TriVertex transferBottomLeft;

		public TriVertex transferBottomRight;

		public TriVertex spaceBottomLeft;

		public TriVertex spaceBottomRight;

		public TriVertex spaceTopLeft;

		public TriVertex spaceTopRight;

		public TriVertex screenBottomRight;

		public TriVertex screenBottomLeft;

		public Color spaceColor;

		public Color skyColor;

		public static int maxHeight = 200;

		public static int skyEndHeight = 30;

		public static int spaceStartHeight = 75;

		public static int mZombieSpace = 140;

		public static int mZombieScale = 125;

		public int mScreenTop;

		public int mPileHeight;

		private bool mGrayed;

		private static Color mGray = new Color(0, 0, 0, 150);

		private VertexPositionTexture[] blackHoleVerts = new VertexPositionTexture[]
		{
			new VertexPositionTexture(new Vector3((float)(-(float)Resources.IMAGE_BLACKHOLE.mWidth) / 4f, (float)(-(float)Resources.IMAGE_BLACKHOLE.mHeight) / 4f, 0f), new Vector2(0f, 1f)),
			new VertexPositionTexture(new Vector3((float)Resources.IMAGE_BLACKHOLE.mWidth / 4f, (float)(-(float)Resources.IMAGE_BLACKHOLE.mHeight) / 4f, 0f), new Vector2(1f, 1f)),
			new VertexPositionTexture(new Vector3((float)(-(float)Resources.IMAGE_BLACKHOLE.mWidth) / 4f, (float)Resources.IMAGE_BLACKHOLE.mHeight / 4f, 0f), new Vector2(0f, 0f)),
			new VertexPositionTexture(new Vector3((float)Resources.IMAGE_BLACKHOLE.mWidth / 4f, (float)Resources.IMAGE_BLACKHOLE.mHeight / 4f, 0f), new Vector2(1f, 0f))
		};

		private short[] blackHoleIndices = new short[]
		{
			0,
			1,
			2,
			1,
			3,
			2
		};

		private Matrix blackHoleView = Matrix.CreateLookAt(new Vector3(0f, 0f, -1000f), Vector3.Zero, Vector3.UnitY);

		private Matrix blackHoleProjection = Matrix.CreatePerspectiveFieldOfView(0.7853982f, 1f, 1f, 5000f);

		public ZombiePileMarker[] friendMarkers;

		public bool mLoadedFriends;

		private ZombiePileWidget.Star[] stars;

		private Random rand = new Random();

		public ZombiePileObject[] gPileObjects;

		private struct Star
		{
			public Star(Point pos, float size, int cel)
			{
				this.pos = pos;
				this.size = size;
				this.cel = cel;
			}

			public Point pos;

			public float size;

			public int cel;
		}
	}
}
