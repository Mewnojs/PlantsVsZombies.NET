using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICSharpCode.SharpZipLib.Zip;

namespace LawnScript
{
    public sealed class ContentPackZipReader : ContentPackReader, IDisposable
    {
        public override Stream LoadFile(string path, string sourcePath = ".") 
        {
            ZipEntry entry = zipFile.GetEntry(PathFinder.GetZipPath(sourcePath, path));
            return zipFile.GetInputStream(entry);
        }

        public ContentPackZipReader(Stream stream) 
        {
            zipFile = new ZipFile(stream);
            this.stream = stream;
        }

        private ZipFile zipFile;

        private Stream stream;

        public void Dispose() {
            stream.Dispose();
        }
    }
}
