using System;

namespace Lawn
{
	public/*internal*/ class ProjectileDefinition
	{
		public ProjectileDefinition(ProjectileType theType, int theRow, int theDamage)
		{
			this.mProjectileType = theType;
			this.mImageRow = theRow;
			this.mDamage = theDamage;
		}

		public ProjectileType mProjectileType;

		public int mImageRow;

		public int mDamage;
	}
}
