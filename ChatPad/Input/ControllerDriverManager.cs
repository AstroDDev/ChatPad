//using Nefarius.ViGEm.Client;
//using Nefarius.ViGEm.Client.Targets;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ChatPad.Configuration;
using vJoyInterfaceWrap;
using System.Windows.Forms;

namespace ChatPad.Input
{
    internal class ControllerDriverManager
    {
        public VirtualController Controller;
        private vJoy VJoystick;
        //private ViGEmClient ViGEmClient;
        //private IXbox360Controller XController;
        //private IDualShock4Controller PSController;

        private Thread updateThread;

        public ControllerDriverManager(VirtualController controller)
        {
            Controller = controller;
            //ViGEmClient = new ViGEmClient();
        }

        public void Output()
        {
            Controller.UpdateControllerState();

            switch (Config.Settings.ControllerInterface)
            {
                case Configuration.JSONObjects.ControllerType.Switch:
                    OutputSwitch();
                    break;
                case Configuration.JSONObjects.ControllerType.Xbox:
                    OutputXbox();
                    break;
                case Configuration.JSONObjects.ControllerType.PlayStation:
                    OutputPlayStation();
                    break;
                default:
                    break;
            }
        }

        public void Stop()
        {
            updateThread = null;
        }

        public void Update()
        {
            while (updateThread != null)
            {
                RuntimeOptions.Frame++;

                if (RuntimeOptions.Paused) Thread.Sleep((int)Config.Settings.MillisecondsPerUpdate);

                Stopwatch sw = Stopwatch.StartNew();

                Output();

                sw.Stop();

                Thread.Sleep(new TimeSpan(Math.Max(0, (long)(Config.Settings.MillisecondsPerUpdate * Program.TICKS_PER_MILLISECOND) - sw.ElapsedTicks)));
            }
        }

        public void Init()
        {
            updateThread = new Thread(new ThreadStart(Update));

            switch (Config.Settings.ControllerInterface)
            {
                case Configuration.JSONObjects.ControllerType.Switch:
                    InitSwitch();
                    break;
                case Configuration.JSONObjects.ControllerType.Xbox:
                    InitXbox();
                    break;
                case Configuration.JSONObjects.ControllerType.PlayStation:
                    InitPlayStation();
                    break;
                default:
                    break;
            }

            updateThread.Start();
        }

        private void InitSwitch()
        {
            /*if (XController != null)
            {
                XController.Disconnect();
                XController = null;
            }
            if (PSController != null)
            {
                PSController.Disconnect();
                PSController = null;
            }*/

            uint index = 1;
            VJoystick = new vJoy();

            if (!VJoystick.vJoyEnabled())
            {
                System.Media.SystemSounds.Exclamation.Play();
                MessageBox.Show("vJoy not enabled or installed! Enabled vJoy in the \"Configure vJoy\" application.", "vJoy Error!");
                throw new ArgumentException("Illegal joystick device id!");
            }

            uint apiVersion = 0;
            uint driverVersion = 0;
            bool match = VJoystick.DriverMatch(ref apiVersion, ref driverVersion);
            if (!match)
                Console.WriteLine("vJoy version of Driver ({0:X}) does NOT match DLL Version ({1:X})", driverVersion, apiVersion);

            var status = VJoystick.GetVJDStatus(index);

            string error = null;
            switch (status)
            {
                case VjdStat.VJD_STAT_BUSY:
                    error = "vJoy Device {0} is already owned by another feeder";
                    break;
                case VjdStat.VJD_STAT_MISS:
                    error = "vJoy Device {0} is not installed or disabled";
                    break;
                case VjdStat.VJD_STAT_UNKN:
                    error = ("vJoy Device {0} general error");
                    break;
            }

            if (error == null && !VJoystick.AcquireVJD(index))
            {
                error = "Failed to acquire vJoy device number {0}";
            }
            if (error != null)
            {
                System.Media.SystemSounds.Exclamation.Play();
                MessageBox.Show(string.Format(error, index), "vJoy Error!");
                throw new Exception(string.Format(error, index));
            }
        }

        private void OutputSwitch()
        {
            if (VJoystick == null) InitSwitch();

            for (uint i = 0; i < TwitchCommandList.BUTTON_LENGTH; i++)
            {
                VJoystick.SetBtn(Controller.ButtonMap[i], 1, i + 1);
            }

            VJoystick.SetAxis(ConvertToVJOYAxis(Controller.LeftStickX), 1, HID_USAGES.HID_USAGE_X);
            VJoystick.SetAxis(ConvertToVJOYAxis(Controller.LeftStickY), 1, HID_USAGES.HID_USAGE_Y);
            VJoystick.SetAxis(ConvertToVJOYAxis(Controller.RightStickX), 1, HID_USAGES.HID_USAGE_Z);
            VJoystick.SetAxis(ConvertToVJOYAxis(Controller.RightStickY), 1, HID_USAGES.HID_USAGE_RX);
            VJoystick.SetAxis(ConvertToVJOYAxis(Controller.MotionX), 1, HID_USAGES.HID_USAGE_SL0);
            VJoystick.SetAxis(ConvertToVJOYAxis(Controller.MotionY), 1, HID_USAGES.HID_USAGE_SL1);
            VJoystick.SetAxis(ConvertToVJOYAxis(Controller.MotionZ), 1, HID_USAGES.HID_USAGE_WHL);
        }

        private void InitXbox()
        {
            if (VJoystick != null)
            {
                VJoystick.RelinquishVJD(1);
            }
            /*if (PSController != null)
            {
                PSController.Disconnect();
                PSController = null;
            }

            XController = ViGEmClient.CreateXbox360Controller();*/
        }

        private void OutputXbox() 
        { 
            //if (XController == null) InitXbox();


        }

        private void InitPlayStation()
        {
            if (VJoystick != null)
            {
                VJoystick.RelinquishVJD(1);
            }
            /*if (XController != null)
            {
                XController.Disconnect();
                XController = null;
            }

            PSController = ViGEmClient.CreateDualShock4Controller();*/
        }

        private void OutputPlayStation()
        {
            //if (PSController == null) InitPlayStation();


        }

        private int ConvertToVJOYAxis(double value)
        {
            return (int)(Math.Min(1, Math.Max(-1, value)) * 16384 + 16382);
        }
    }
}
