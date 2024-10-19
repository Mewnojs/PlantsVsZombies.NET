using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawnScript
{
    public interface ContentLoader<T> where T : Content
    {
        public abstract T Load(ContentPack pack, ContentPackReader reader, string path);

        public abstract void Unload(T loaded);
    }
}
