using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawnScript
{
    internal static class PathFinder
    {
        public static string GetZipPath(string sourcePath, string path) 
        {
            return Path.GetRelativePath(".", Path.Combine(sourcePath, path));
        }
    }
}
