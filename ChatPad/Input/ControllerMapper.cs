using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using ChatPad.Configuration;
using ChatPad.Configuration.JSONObjects;
using System.Threading;
using System.Windows.Forms;
using Newtonsoft.Json;
using static SDL2.SDL;
using vJoyInterfaceWrap;
using System.Diagnostics;

namespace ChatPad.Input
{
    internal class ControllerMapper : VirtualController
    {
        public List<string> ControllerList { get; private set; }
        public int SelectedIndex = -1;
        public IntPtr SelectedDevice { get; private set; }

        public ControllerMapper() 
        {
            ControllerList = new List<string>();

            UpdateList();
        }

        public void UpdateList()
        {
            ControllerList.Clear();
            bool foundSelect = false;
            for (int i = 0; i < 16; i++)
            {
                if (SDL_IsGameController(i) == SDL_bool.SDL_TRUE)
                {
                    string name = "[" + i + "] " + SDL_GameControllerNameForIndex(i);
                    ControllerList.Add(name);
                    if (SelectedIndex > -1 && SDL_GameControllerFromPlayerIndex(i) == SelectedDevice)
                    {
                        SelectedIndex = i;
                        foundSelect = true;
                    }
                }
                else break;
            }
            if (!foundSelect)
            {
                SelectedIndex = -1;
                SelectedDevice = IntPtr.Zero;
            }
        }

        public void SelectDevice(int index)
        {
            if (SelectedIndex != -1) SDL_GameControllerClose(SelectedDevice);
            SelectedIndex = index;
            SelectedDevice = SDL_GameControllerOpen(index);
        }

        public override void UpdateControllerState()
        {
            SDL_GameControllerUpdate();

            AButton = SDL_GameControllerGetButton(SelectedDevice, SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_A) > 0;
            BButton = SDL_GameControllerGetButton(SelectedDevice, SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_B) > 0;
            XButton = SDL_GameControllerGetButton(SelectedDevice, SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_X) > 0;
            YButton = SDL_GameControllerGetButton(SelectedDevice, SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_Y) > 0;
            UpButton = SDL_GameControllerGetButton(SelectedDevice, SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_DPAD_UP) > 0;
            DownButton = SDL_GameControllerGetButton(SelectedDevice, SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_DPAD_DOWN) > 0;
            LeftButton = SDL_GameControllerGetButton(SelectedDevice, SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_DPAD_LEFT) > 0;
            RightButton = SDL_GameControllerGetButton(SelectedDevice, SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_DPAD_RIGHT) > 0;
            LeftBumperButton = SDL_GameControllerGetButton(SelectedDevice, SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_LEFTSHOULDER) > 0;
            RightBumperButton = SDL_GameControllerGetButton(SelectedDevice, SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_RIGHTSHOULDER) > 0;

            LeftTriggerButton = SDL_GameControllerGetAxis(SelectedDevice, SDL_GameControllerAxis.SDL_CONTROLLER_AXIS_TRIGGERLEFT) > 0;
            RightTriggerButton = SDL_GameControllerGetAxis(SelectedDevice, SDL_GameControllerAxis.SDL_CONTROLLER_AXIS_TRIGGERRIGHT) > 0;

            LeftStickButton = SDL_GameControllerGetButton(SelectedDevice, SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_LEFTSTICK) > 0;
            RightStickButton = SDL_GameControllerGetButton(SelectedDevice, SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_RIGHTSTICK) > 0;
            PlusButton = SDL_GameControllerGetButton(SelectedDevice, SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_START) > 0;
            MinusButton = SDL_GameControllerGetButton(SelectedDevice, SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_BACK) > 0;

            LeftStickX = SDL_GameControllerGetAxis(SelectedDevice, SDL_GameControllerAxis.SDL_CONTROLLER_AXIS_LEFTX) / (double)short.MaxValue;
            LeftStickY = SDL_GameControllerGetAxis(SelectedDevice, SDL_GameControllerAxis.SDL_CONTROLLER_AXIS_LEFTY) / (double)short.MaxValue;
            RightStickX = SDL_GameControllerGetAxis(SelectedDevice, SDL_GameControllerAxis.SDL_CONTROLLER_AXIS_RIGHTX) / (double)short.MaxValue;
            RightStickY = SDL_GameControllerGetAxis(SelectedDevice, SDL_GameControllerAxis.SDL_CONTROLLER_AXIS_RIGHTY) / (double)short.MaxValue;
        }
    }
}
