using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawnScript
{
    public class JsPluginLoader : ContentLoader<Content>
    {
        public readonly Jint.Engine ScriptEngine;

        public JsPluginLoader(Jint.Engine engine) : base() { ScriptEngine = engine; }

        public Content Load(ContentPack pack, ContentPackReader reader, string path)
        {
            return new JsPlugin(reader, pack, path, ScriptEngine);
        }

        public void Unload(Content loaded)
        {
            throw new NotImplementedException();
        }
    }
}
