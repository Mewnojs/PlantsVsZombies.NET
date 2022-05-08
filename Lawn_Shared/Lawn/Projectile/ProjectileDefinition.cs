using System;

namespace Lawn
{
    public/*internal*/ class ProjectileDefinition
    {
        public ProjectileDefinition(ProjectileType theType, int theRow, int theDamage)
        {
            mProjectileType = theType;
            mImageRow = theRow;
            mDamage = theDamage;
        }

        public ProjectileType mProjectileType;

        public int mImageRow;

        public int mDamage;
    }
}
