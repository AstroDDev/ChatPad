using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ChatPad.Configuration.JSONConverters;
using ChatPad.Configuration.JSONObjects;

namespace ChatPad.Configuration
{
    internal class Config
    {
        [JsonIgnore] private static Config instance;
        [JsonIgnore] public static string CONFIG_PATH = "./config.json";

        [JsonIgnore] public static TwitchPlaysOptions Settings { get { return instance.settings; } }
        [JsonIgnore] public static TwitchCommandList Commands { get { return instance.commands; } }

        [JsonProperty("oauth")] public static string oauth;
        [JsonProperty("username")] public static string username;
        [JsonProperty("settings")] private TwitchPlaysOptions settings;
        [JsonProperty("commands")] private TwitchCommandList commands;

        public static void Load(string path)
        {
            if (File.Exists(path))
            {
                instance = JsonConvert.DeserializeObject<Config>(File.ReadAllText(path));
            }
            else
            {
                Generate(path);
            }
        }

        public static void Load()
        {
            Load(CONFIG_PATH);
        }

        public static void Generate(string path)
        {
            instance = new Config();
            instance.settings = new TwitchPlaysOptions();
            instance.commands = new TwitchCommandList();

            Save(path);
        }

        public static void Save(string path)
        {
            using (StringWriter sw = new StringWriter())
            {
                using (JsonWriter jw = new JSONFormattedTextWriter(sw))
                {
                    jw.Formatting = Formatting.Indented;
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Serialize(jw, instance);
                    File.WriteAllText(path, sw.ToString());
                }
            }
        }

        public static void Save()
        {
            Save(CONFIG_PATH);
        }

        public static void Reset()
        {
            instance = new Config();
            instance.settings = new TwitchPlaysOptions();
            instance.commands = new TwitchCommandList();
        }
    }
}
