using System;

namespace Sexy
{
    public interface IReusable
    {
        void Reset();

        void PrepareForReuse();
    }
}
