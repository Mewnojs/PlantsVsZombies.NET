using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LawnScript
{
    public class ContentPack
    {
        public record ContentPackInfo(string Name, string Author, Version Version, string Description);

        [JsonInclude]
        public ContentPackInfo Info;

        [JsonInclude]
        [JsonPropertyName("contents")]
        public List<ContentMetadata> ContentsMetadata;

        public List<Content> Contents = new();

        public void Enable() 
        {
            foreach (var item in Contents)
            {
                if (item.Technique == Content.PluginTechnique.ResourcePack)
                {
                    item.Enable();
                }
            }
            foreach (var item in Contents)
            {
                if (item.Technique == Content.PluginTechnique.JsBehavior)
                {
                    item.Enable();
                }
            }
            foreach (var item in Contents) 
            {
                if (item.Technique == Content.PluginTechnique.JsPlugin) 
                {
                    item.Enable();    
                }
            }
        }

        public void Disable() 
        {
            foreach (var item in Contents)
            {
                if (item.Technique == Content.PluginTechnique.JsPlugin)
                {
                    item.Disable();
                }
            }
            foreach (var item in Contents)
            {
                if (item.Technique == Content.PluginTechnique.JsBehavior)
                {
                    item.Disable();
                }
            }
            foreach (var item in Contents)
            {
                if (item.Technique == Content.PluginTechnique.ResourcePack)
                {
                    item.Disable();
                }
            }
        }
    }
}
