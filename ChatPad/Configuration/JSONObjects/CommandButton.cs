using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatPad.Configuration.JSONObjects
{
    internal class CommandButton : VCommandInput
    {
        [JsonProperty("press")] public string[] Press = new string[0];
        [JsonProperty("hold")] public string[] Hold = new string[0];
        [JsonProperty("release")] public string[] Release = new string[0];

        [JsonIgnore] private int holdtime = 0;
        [JsonIgnore] public int HoldTime { get { return holdtime; } set { holdtime = Math.Max(0, value); } }

        public CommandButton()
        {
            Enabled = false;
            Passthrough = false;
            Threshold = 0;

            Press = new string[0];
            Hold = new string[0];
            Release = new string[0];
        }

        public void GetValue(string[] cmd, out bool press, out bool hold, out bool release)
        {
            press = false;
            hold = false;
            release = false;
            for (int i = 0; i < cmd.Length; i++) 
            {
                press = Press.Contains(cmd[i]);
                hold = Hold.Contains(cmd[i]);
                release = Release.Contains(cmd[i]);
                if (press || hold || release) return;
            }
        }
    }
}
