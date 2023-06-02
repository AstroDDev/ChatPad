using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatPad.Configuration.JSONObjects
{
    internal abstract class VCommandInput
    {
        [JsonProperty("enabled")] public bool Enabled;
        [JsonProperty("passthrough")] public bool Passthrough;
        [JsonProperty("threshold")] public double Threshold;
    }
}
