using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using vJoyInterfaceWrap;

namespace ChatPad
{
    internal class VProController
    {
        private vJoy joystick;

        public VProController() 
        {
            uint index = 1;
            joystick = new vJoy();
            
            if (!joystick.vJoyEnabled())
                throw new ArgumentException("Illegal joystick device id!");

            uint apiVersion = 0;
            uint driverVersion = 0;
            bool match = joystick.DriverMatch(ref apiVersion, ref driverVersion);
            if (!match)
                Console.WriteLine("vJoy version of Driver ({0:X}) does NOT match DLL Version ({1:X})", driverVersion, apiVersion);

            var status = joystick.GetVJDStatus(index);

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

            if (error == null && !joystick.AcquireVJD(index))
                error = "Failed to acquire vJoy device number {0}";
            if (error != null)
                throw new Exception(string.Format(error, index));
        }
    }
}