using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatPad.Configuration;

namespace ChatPad.Input
{
    internal class VirtualControllerManager : VirtualController
    {
        public static VirtualControllerManager instance;

        public VirtualController Passthrough;
        public VirtualController Twitch;

        public VirtualControllerManager(VirtualController passthrough, VirtualController twitch)
        {
            instance = this;

            Passthrough = passthrough;
            Twitch = twitch;
        }

        public override void UpdateControllerState()
        {
            Passthrough.UpdateControllerState();

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
                MotionX = Passthrough.MotionX;
                MotionY = Passthrough.MotionY;
                MotionZ = Passthrough.MotionZ;
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
                MotionX = Config.Commands.MotionXAxis.Enabled ? Config.Commands.MotionXAxis.Passthrough ? Passthrough.MotionX : Twitch.MotionX : 0;
                MotionY = Config.Commands.MotionYAxis.Enabled ? Config.Commands.MotionYAxis.Passthrough ? Passthrough.MotionY : Twitch.MotionY : 0;
                MotionZ = Config.Commands.MotionZAxis.Enabled ? Config.Commands.MotionZAxis.Passthrough ? Passthrough.MotionZ : Twitch.MotionZ : 0;
            }

            Form1.Instance.controllerPanel.Invalidate();
        }
    }
}
