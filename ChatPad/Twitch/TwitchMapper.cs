using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatPad.Configuration;
using ChatPad.Input;

namespace ChatPad.Twitch
{
    internal class TwitchMapper : VirtualController
    {
        private static TwitchMapper instance;

        private Dictionary<string, TwitchVote> Votes = new Dictionary<string, TwitchVote>();

        private double[] ButtonWeight;
        private double[] ButtonPress;
        private double[] ButtonHold;
        private double[] ButtonRelease;
        private double[] AxisValue;
        private double ButtonScalar;
        private double[] AxisScalar;

        public TwitchMapper() 
        { 
            instance = this;
            ButtonWeight = new double[TwitchCommandList.BUTTON_LENGTH];
            ButtonPress = new double[TwitchCommandList.BUTTON_LENGTH];
            ButtonHold = new double[TwitchCommandList.BUTTON_LENGTH];
            ButtonRelease = new double[TwitchCommandList.BUTTON_LENGTH];
            AxisValue = new double[TwitchCommandList.AXIS_LENGTH];
            AxisScalar = new double[TwitchCommandList.AXIS_LENGTH];
        }

        public override void UpdateControllerState()
        {
            while (TwitchManager.Messages.Count > 0)
            {
                try
                {
                    ChatMessage chatMessage = TwitchManager.Messages.Pop();
                    TwitchVote vote;
                    if (Votes.TryGetValue(chatMessage.name, out vote))
                    {
                        vote.Update(chatMessage.text);
                    }
                    else
                    {
                        vote = new TwitchVote(chatMessage.text);
                        Votes.Add(chatMessage.name, vote);
                    }
                }
                catch
                {
                    continue;
                }
            }

            UpdateControls();

            foreach (KeyValuePair<string, TwitchVote> vote in Votes.ToList())
            {
                vote.Value.Degrade();
                if (vote.Value.Weight <= 0)
                {
                    Votes.Remove(vote.Key);
                }
            }
        }

        public static void UpdateControls()
        {
            instance.UpdateWeights();

            instance.CalculateControls();
        }

        private void UpdateWeights()
        {
            instance.ButtonScalar = 0;
            for (int i = 0; i < TwitchCommandList.BUTTON_LENGTH; i++)
            {
                instance.ButtonWeight[i] = 0;
                instance.ButtonPress[i] = 0;
                instance.ButtonHold[i] = 0;
                instance.ButtonRelease[i] = 0;
            }
            for (int i = 0; i < TwitchCommandList.AXIS_LENGTH; i++) 
            { 
                instance.AxisValue[i] = 0; 
                instance.AxisScalar[i] = 0; 
            }

            foreach (var vote in Votes.Values)
            {
                for (int i = 0; i < TwitchCommandList.BUTTON_LENGTH; i++)
                {
                    double adder = (vote.ButtonPress[i] || vote.ButtonHold[i] || vote.ButtonRelease[i]) ? vote.Weight : 0;

                    instance.ButtonPress[i] += vote.ButtonPress[i] ? vote.Weight : 0;
                    instance.ButtonHold[i] += vote.ButtonHold[i] ? vote.Weight : 0;
                    instance.ButtonRelease[i] += vote.ButtonRelease[i] ? vote.Weight : 0;

                    instance.ButtonWeight[i] += adder;
                    instance.ButtonScalar += adder;
                }

                for (int i = 0; i < TwitchCommandList.AXIS_LENGTH; i++)
                {
                    instance.AxisValue[i] += vote.AxisVote[i] ? vote.Axis[i] * vote.Weight : 0;
                    instance.AxisScalar[i] += vote.AxisVote[i] ? vote.Weight : 0;
                }
            }
            double LeftStickScalar = Math.Max(instance.AxisScalar[0], instance.AxisScalar[1]);
            instance.AxisScalar[0] = LeftStickScalar;
            instance.AxisScalar[1] = LeftStickScalar;
            double RightStickScalar = Math.Max(instance.AxisScalar[2], instance.AxisScalar[3]);
            instance.AxisScalar[2] = RightStickScalar;
            instance.AxisScalar[3] = RightStickScalar;
            for (int i = 0; i < TwitchCommandList.BUTTON_LENGTH; i++) instance.ButtonWeight[i] /= instance.ButtonScalar;
            for (int i = 0; i < TwitchCommandList.AXIS_LENGTH; i++)
            {
                instance.AxisValue[i] *= Config.Commands.AxisMap[i].Threshold / instance.AxisScalar[i];
            }
        }

        private void CalculateControls()
        {
            for (int i = 0; i < TwitchCommandList.BUTTON_LENGTH; i++)
            {
                bool active = ButtonWeight[i] / ButtonScalar >= ConvertThreshold(Config.Commands.ButtonMap[i].Threshold);
                ButtonMap[i] = false;

                if (Config.Commands.ButtonMap[i].HoldTime > 0)
                {//Hold the press
                    ButtonMap[i] = true;
                    Config.Commands.ButtonMap[i].HoldTime--;

                    if (Config.Commands.ButtonMap[i].HoldTime <= 0)
                    {//Ran out
                        DisabledPress(i);
                        ButtonMap[i] = false;
                    }
                }
                else if (ButtonWeight[i] / ButtonScalar >= ConvertThreshold(Config.Commands.ButtonMap[i].Threshold))
                {
                    ButtonMap[i] = false;
                    if (ButtonPress[i] > ButtonRelease[i] && ButtonPress[i] > ButtonHold[i])
                    {//Press
                        ButtonMap[i] = true;
                        Config.Commands.ButtonMap[i].HoldTime = Config.Settings.ButtonPressLength;
                    }
                    else if (ButtonHold[i] > ButtonRelease[i])
                    {//Hold
                        ButtonMap[i] = true;
                    }
                }
            }

            //Axies might need to be hard coded!
            for (int i = 0; i < TwitchCommandList.AXIS_LENGTH; i++)
            {
                AxisMap[i] = AxisValue[i];
            }
        }

        private void DisabledPress(int index)
        {
            foreach (var vote in Votes.Values)
            {
                vote.ButtonPress[index] = false;
            }
        }

        private double ConvertThreshold(double threshold)
        {
            // -1 >> 0 : 0% >> AverageButtonInput
            // 0 >> 1 : AverageButtonInput >> 100%
            double avg = 1d / (TwitchCommandList.BUTTON_LENGTH + TwitchCommandList.MACRO_LENGTH);
            return threshold > 0 ? Lerp(avg, 1, Clamp01(threshold)) : Lerp(0, avg, Clamp01(threshold + 1));
        }

        private double Clamp01(double value)
        {
            return Math.Max(0, Math.Min(value, 1));
        }

        private double Lerp(double a, double b, double t)
        {
            return (b - a) * t + a;
        }
    }
}
