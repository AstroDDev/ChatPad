using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatPad.Configuration;

namespace ChatPad.Twitch
{
    internal class TwitchVote
    {
        public bool[] ButtonPress;
        public bool[] ButtonHold;
        public bool[] ButtonRelease;
        public float[] Axis;
        public bool[] AxisVote;
        public bool[] Macro;

        public double Weight;

        public TwitchVote(string msg)
        {
            Update(msg);
        }

        public void Degrade()
        {
            Weight -= (1d / Config.Settings.UpdatesPerSecond);
        }

        public void Update(string msg)
        {
            Weight = Config.Settings.VoteLifespan;

            string[] cmds = msg.Split(' ');

            Config.Commands.ButtonOutput(cmds, ref ButtonPress, ref ButtonHold, ref ButtonRelease);
            Config.Commands.AxisOutput(cmds, ref Axis, ref AxisVote);
        }
    }
}
