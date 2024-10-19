using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LawnScript
{
    public abstract class Content
    {
        public enum PluginTechnique
        {
            Invalid = -1,
            JsPlugin, // JsPlugin
            DllPlugin, // DllPlugin
            ResourcePack, // ResourcePack
            JsBehavior, // JsBehavior
        }

        public record struct PluginDescription(string Name, Version Version, string Description);

        public string Path { get; private set; }

        public PluginDescription Info { get; private set; }

        public virtual PluginTechnique Technique { get => PluginTechnique.Invalid; }

        public ContentPack Pack { get; private set; }

        public ContentPackReader Reader { get; private set; }

        internal Content(ContentPackReader reader, ContentPack parent, string path)
        {
            Pack = parent;
            Path = path;
            Reader = reader;
        }

        public abstract void Enable();

        public abstract void Disable();
    }

    public record ContentMetadata(string Path, string Type, Content.PluginDescription? Info);
    // TODO: move to .NET 8, and retype "Type" as PluginTechnique.  .NET 6 problem. dotnet/runtime/issues/31619
}
