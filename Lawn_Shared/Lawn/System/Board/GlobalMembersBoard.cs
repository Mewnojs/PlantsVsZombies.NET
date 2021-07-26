using System;
using Sexy;

namespace Lawn
{
	internal static class GlobalMembersBoard
	{
		public static void AddGameObjectRenderItem(RenderItem[] theRenderList, ref int theCurRenderItem, RenderObjectType theRenderObjectType, GameObject theGameObject)
		{
			Debug.ASSERT(theCurRenderItem < 2048);
			RenderItem renderItem = theRenderList[theCurRenderItem];
			renderItem.mRenderObjectType = theRenderObjectType;
			renderItem.mZPos = theGameObject.mRenderOrder;
			renderItem.mGameObject = theGameObject;
			renderItem.id = (long)theCurRenderItem;
			theCurRenderItem++;
		}

		public static void AddGameObjectRenderItemCursorPreview(RenderItem[] theRenderList, ref int theCurRenderItem, RenderObjectType theRenderObjectType, GameObject theGameObject)
		{
			Debug.ASSERT(theCurRenderItem < 2048);
			RenderItem renderItem = theRenderList[theCurRenderItem];
			renderItem.mRenderObjectType = theRenderObjectType;
			renderItem.mZPos = theGameObject.mRenderOrder;
			renderItem.mGameObject = theGameObject;
			renderItem.mCursorPreview = (CursorPreview)theGameObject;
			renderItem.id = (long)theCurRenderItem;
			theCurRenderItem++;
		}

		public static void AddGameObjectRenderItemPlant(RenderItem[] theRenderList, ref int theCurRenderItem, RenderObjectType theRenderObjectType, GameObject theGameObject)
		{
			Debug.ASSERT(theCurRenderItem < 2048);
			RenderItem renderItem = theRenderList[theCurRenderItem];
			renderItem.mRenderObjectType = theRenderObjectType;
			renderItem.mZPos = theGameObject.mRenderOrder;
			renderItem.mGameObject = theGameObject;
			renderItem.mPlant = (Plant)theGameObject;
			renderItem.id = (long)theCurRenderItem;
			theCurRenderItem++;
		}

		public static void AddGameObjectRenderItemZombie(RenderItem[] theRenderList, ref int theCurRenderItem, RenderObjectType theRenderObjectType, GameObject theGameObject)
		{
			Debug.ASSERT(theCurRenderItem < 2048);
			RenderItem renderItem = theRenderList[theCurRenderItem];
			renderItem.mRenderObjectType = theRenderObjectType;
			renderItem.mZPos = theGameObject.mRenderOrder;
			renderItem.mGameObject = theGameObject;
			renderItem.mZombie = (Zombie)theGameObject;
			renderItem.id = (long)theCurRenderItem;
			theCurRenderItem++;
		}

		public static void AddGameObjectRenderItemProjectile(RenderItem[] theRenderList, ref int theCurRenderItem, RenderObjectType theRenderObjectType, GameObject theGameObject)
		{
			Debug.ASSERT(theCurRenderItem < 2048);
			RenderItem renderItem = theRenderList[theCurRenderItem];
			renderItem.mRenderObjectType = theRenderObjectType;
			renderItem.mZPos = theGameObject.mRenderOrder;
			renderItem.mGameObject = theGameObject;
			renderItem.mProjectile = (Projectile)theGameObject;
			renderItem.id = (long)theCurRenderItem;
			theCurRenderItem++;
		}

		public static void AddGameObjectRenderItemCoin(RenderItem[] theRenderList, ref int theCurRenderItem, RenderObjectType theRenderObjectType, GameObject theGameObject)
		{
			Debug.ASSERT(theCurRenderItem < 2048);
			RenderItem renderItem = theRenderList[theCurRenderItem];
			renderItem.mRenderObjectType = theRenderObjectType;
			renderItem.mZPos = theGameObject.mRenderOrder;
			renderItem.mGameObject = theGameObject;
			renderItem.mCoin = (Coin)theGameObject;
			renderItem.id = (long)theCurRenderItem;
			theCurRenderItem++;
		}

		public static void AddUIRenderItem(RenderItem[] theRenderList, ref int theCurRenderItem, RenderObjectType theRenderObjectType, int thePosZ)
		{
			Debug.ASSERT(theCurRenderItem < 2048);
			RenderItem renderItem = theRenderList[theCurRenderItem];
			renderItem.mRenderObjectType = theRenderObjectType;
			renderItem.mZPos = thePosZ;
			renderItem.mGameObject = null;
			renderItem.id = (long)theCurRenderItem;
			theCurRenderItem++;
		}

		public static void TodCrash()
		{
			Debug.OutputDebug<string>("Crash !!!!");
			Debug.ASSERT(false);
		}
	}
}
