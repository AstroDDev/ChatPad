using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatPad.Configuration.JSONObjects
{
    public enum ControllerType { Switch, Xbox, PlayStation }
    internal class TwitchPlaysOptions
    {
        [JsonProperty("channel")] public string Channel;
        [JsonProperty("prefix")] public string Prefix;
        [JsonProperty("vote_lifespan")] public float VoteLifespan;
        [JsonProperty("updates_per_second")] public float UpdatesPerSecond;
        [JsonIgnore] public float MillisecondsPerUpdate { get { return 1000 / UpdatesPerSecond; } }
        [JsonProperty("controller_interface")] public ControllerType ControllerInterface;
        [JsonProperty("button_press_length")] public int ButtonPressLength;

        public TwitchPlaysOptions()
        {
            Channel = "";
            Prefix = "!";
            VoteLifespan = 1;
            UpdatesPerSecond = 60;
            ControllerInterface = ControllerType.Switch;
            ButtonPressLength = 1;
        }
    }
}
