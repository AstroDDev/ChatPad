using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ChatPad.Configuration;

namespace ChatPad.Input
{
    internal class VirtualControllerManager : VirtualController
    {
        public static VirtualControllerManager instance;

        public VirtualController Passthrough;
        public VirtualController Twitch;

        private Thread updateTwitchThread;

        public VirtualControllerManager(VirtualController passthrough, VirtualController twitch)
        {
            instance = this;

            Passthrough = passthrough;
            Twitch = twitch;

            updateTwitchThread = new Thread(UpdateTwitchController);
            updateTwitchThread.Start();

            Form1.Instance.FormClosing += (s, evt) => Stop();
        }

        public override void UpdateControllerState()
        {
            Passthrough.UpdateControllerState();
            //Form1.Instance.controllerPanel.Invalidate();
        }

        private void Stop()
        {
            updateTwitchThread = null;
        }

        private void UpdateTwitchController()
        {
            while (updateTwitchThread != null && updateTwitchThread.IsAlive) {
                Stopwatch sw = Stopwatch.StartNew();

                if (RuntimeOptions.Paused || RuntimeOptions.FullPassthrough)
                {
                    for (int i = 0; i < TwitchCommandList.BUTTON_LENGTH; i++)
                    {
                        ButtonMap[i] = Passthrough.ButtonMap[i];
                    }

                    LeftStickX = Passthrough.LeftStickX;
                    LeftStickY = Passthrough.LeftStickY;
                    RightStickX = Passthrough.RightStickX;
                    RightStickY = Passthrough.RightStickY;
                }
                else
                {
                    Twitch.UpdateControllerState();

                    for (int i = 0; i < TwitchCommandList.BUTTON_LENGTH; i++)
                    {
                        ButtonMap[i] = Config.Commands.ButtonMap[i].Enabled && (Config.Commands.ButtonMap[i].Passthrough ? Passthrough.ButtonMap[i] : Twitch.ButtonMap[i]);
                    }

                    LeftStickX = Config.Commands.LeftStickXAxis.Enabled ? Config.Commands.LeftStickXAxis.Passthrough ? Passthrough.LeftStickX : Twitch.LeftStickX : 0;
                    LeftStickY = Config.Commands.LeftStickYAxis.Enabled ? Config.Commands.LeftStickYAxis.Passthrough ? Passthrough.LeftStickY : Twitch.LeftStickY : 0;
                    RightStickX = Config.Commands.RightStickXAxis.Enabled ? Config.Commands.RightStickXAxis.Passthrough ? Passthrough.RightStickX : Twitch.RightStickX : 0;
                    RightStickY = Config.Commands.RightStickYAxis.Enabled ? Config.Commands.RightStickYAxis.Passthrough ? Passthrough.RightStickY : Twitch.RightStickY : 0;
                }

                sw.Stop();

                Thread.Sleep(new TimeSpan(Math.Max(0, (long)(Config.Settings.MillisecondsPerUpdate * Program.TICKS_PER_MILLISECOND) - sw.ElapsedTicks)));
            }
        }
    }
}
