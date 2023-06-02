using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatPad.Configuration.JSONObjects
{
    internal class CommandAxis : VCommandInput
    {
        [JsonProperty("max")] public string[] Max = new string[0];
        [JsonProperty("min")] public string[] Min = new string[0];
        [JsonProperty("zero")] public string[] Zero = new string[0];

        public CommandAxis() 
        {
            Enabled = false;
            Passthrough = false;
            Threshold = 0;

            Max = new string[0];
            Min = new string[0];
            Zero = new string[0];
        }

        public double GetValue(string[] cmds, out bool voted)
        {
            bool max = false;
            bool min = false;
            bool zero = false;
            for (int i = 0; i < cmds.Length; i++)
            {
                max = max || Max.Contains(cmds[i]);
                min = min || Min.Contains(cmds[i]);
                zero = zero || Zero.Contains(cmds[i]);
            }
            voted = max || min || zero;
            return (max ? 1 : 0) + (min ? -1 : 0);
        }
    }
}
