using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LawnScript
{
    public class ContentPackLoader
    {
        public readonly string MANIFEST_PATH = "./manifest.json";

        public ContentPack? LoadFromZip(Stream stream, Dictionary<Content.PluginTechnique, ContentLoader<Content>> loaders)
        {
            using var reader = new ContentPackZipReader(stream);
            var result = LoadManifest(reader.LoadFile(MANIFEST_PATH));
            if (result is not null)
                LoadContents(result, reader, loaders);
            return result;
        }

        private void LoadContents(ContentPack contentPack, ContentPackReader reader, Dictionary<Content.PluginTechnique, ContentLoader<Content>> loaders) 
        {
            var jsContentLoader = loaders[Content.PluginTechnique.JsPlugin];
            foreach (ContentMetadata metadata in contentPack.ContentsMetadata) 
            {
                switch (metadata.Type) 
                {
                case "js_plugin":
                    contentPack.Contents.Add(jsContentLoader.Load(contentPack, reader, metadata.Path));
                    break;
                default:
                    throw new NotSupportedException($"Content of type {metadata.Type} not supported.");
                }
            }
        }

        private ContentPack? LoadManifest(Stream stream) 
        {
            return LoadUtf8JsonStream<ContentPack>(stream);
        }

        public T? LoadUtf8JsonStream<T>(Stream stream) 
        {
            return JsonSerializer.Deserialize<T>(stream, new JsonSerializerOptions()
            {
                PropertyNamingPolicy = namingPolicy,
                Converters = {
                    new JsonStringEnumConverter(namingPolicy)
                }
            });
        }

        private JsonNamingPolicy namingPolicy = new PascalToSnakeCaseNamingPolicy();
    }

    public class PascalToSnakeCaseNamingPolicy : JsonNamingPolicy 
    {
        public override string ConvertName(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            StringBuilder result = GetStringBuilder();

            for (int i = 0; i < input.Length; i++)
            {
                char currentChar = input[i];

                // Check if the character is uppercase
                if (char.IsUpper(currentChar))
                {
                    // If it's not the first character, add an underscore before it
                    if (i > 0)
                    {
                        result.Append('_');
                    }
                    // Convert to lowercase and append
                    result.Append(char.ToLower(currentChar));
                }
                else
                {
                    // If it's lowercase, just append it
                    result.Append(currentChar);
                }
            }

            var resultString = result.ToString();

            ReturnStringBuilder(result);

            return resultString;
        }

        private static readonly ConcurrentBag<StringBuilder> StringBuilderPool = new ConcurrentBag<StringBuilder>();

        private static StringBuilder GetStringBuilder()
        {
            if (StringBuilderPool.TryTake(out StringBuilder? sb) && sb is not null)
            {
                sb.Clear();
                return sb;
            }
            return new StringBuilder();
        }

        private static void ReturnStringBuilder(StringBuilder sb)
        {
            StringBuilderPool.Add(sb);
        }
    }
}
