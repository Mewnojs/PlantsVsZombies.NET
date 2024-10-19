using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawnScript
{
    public class JsPlugin : Content
    {
        public override PluginTechnique Technique => PluginTechnique.JsPlugin;

        public readonly Jint.Engine ScriptEngine;

        public JsPlugin(ContentPackReader reader, ContentPack pack, string path, Jint.Engine engine) : base(reader, pack, path) { 
            ScriptEngine = engine; 
            LoadCode();
        }

        private Dictionary<string, string>? subModules;

        private Jint.Native.Object.ObjectInstance module;

        private string code = "";

        private void LoadCode() 
        {
            using var sr = new StreamReader(Reader.LoadFile(Path, "."));
            code = sr.ReadToEnd();
            ScriptEngine.Modules.Add($"{Pack.Info.Name}${System.IO.Path.GetFileNameWithoutExtension(Path)}", code);
            module = ScriptEngine.Modules.Import($"{Pack.Info.Name}${System.IO.Path.GetFileNameWithoutExtension(Path)}");
            if (module.Get("enable").Type != Jint.Runtime.Types.Object || module.Get("disable").Type != Jint.Runtime.Types.Object)
                throw new InvalidDataException("Script must have (enable) and (disable) exports");
        }

        public override void Enable()
        {
            ScriptEngine.SetValue("plugin", Jint.Runtime.Interop.TypeReference.FromObject(ScriptEngine, this));
            ScriptEngine.Call(module.Get("enable"));
            ScriptEngine.SetValue("plugin", Jint.Native.JsUndefined.Undefined);
        }

        public override void Disable() 
        {
            ScriptEngine.SetValue("plugin", Jint.Runtime.Interop.TypeReference.FromObject(ScriptEngine, this));
            ScriptEngine.Call(module.Get("disable"));
            ScriptEngine.SetValue("plugin", Jint.Native.JsUndefined.Undefined);
        }

    }
}
