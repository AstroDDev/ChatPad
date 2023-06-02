using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatPad.Configuration.JSONObjects
{
    internal class ControllerOptions
    {
        [JsonProperty("id")] public string ID;
        [JsonProperty("button_bindings")] public uint[] ButtonBindings = new uint[16];
        [JsonProperty("axis_bindings")] public uint[] AxisBindings = new uint[7];

        public ControllerOptions() 
        {
            ButtonBindings = new uint[TwitchCommandList.BUTTON_LENGTH];
            AxisBindings = new uint[TwitchCommandList.AXIS_LENGTH];
        }
    }
}
