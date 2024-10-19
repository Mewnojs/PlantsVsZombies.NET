using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawnScript
{
    public abstract class ContentPackReader
    {
        public abstract Stream LoadFile(string path, string sourcePath);
    }
}
