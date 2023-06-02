using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatPad.Configuration;

namespace ChatPad.Input
{
    internal abstract class VirtualController
    {
        public bool AButton { get { return ButtonMap[0]; } set { ButtonMap[0] = value; } }
        public bool BButton { get { return ButtonMap[1]; } set { ButtonMap[1] = value; } }
        public bool XButton { get { return ButtonMap[2]; } set { ButtonMap[2] = value; } }
        public bool YButton { get { return ButtonMap[3]; } set { ButtonMap[3] = value; } }
        public bool UpButton { get { return ButtonMap[4]; } set { ButtonMap[4] = value; } }
        public bool DownButton { get { return ButtonMap[5]; } set { ButtonMap[5] = value; } }
        public bool LeftButton { get { return ButtonMap[6]; } set { ButtonMap[6] = value; } }
        public bool RightButton { get { return ButtonMap[7]; } set { ButtonMap[7] = value; } }
        public bool LeftBumperButton { get { return ButtonMap[8]; } set { ButtonMap[8] = value; } }
        public bool RightBumperButton { get { return ButtonMap[9]; } set { ButtonMap[9] = value; } }
        public bool LeftTriggerButton { get { return ButtonMap[10]; } set { ButtonMap[10] = value; } }
        public bool RightTriggerButton { get { return ButtonMap[11]; } set { ButtonMap[11] = value; } }
        public bool LeftStickButton { get { return ButtonMap[12]; } set { ButtonMap[12] = value; } }
        public bool RightStickButton { get { return ButtonMap[13]; } set { ButtonMap[13] = value; } }
        public bool PlusButton { get { return ButtonMap[14]; } set { ButtonMap[14] = value; } }
        public bool MinusButton { get { return ButtonMap[15]; } set { ButtonMap[15] = value; } }
        public double LeftStickX { get { return AxisMap[0]; } set { AxisMap[0] = value; } }
        public double LeftStickY { get { return AxisMap[1]; } set { AxisMap[1] = value; } }
        public double RightStickX { get { return AxisMap[2]; } set { AxisMap[2] = value; } }
        public double RightStickY { get { return AxisMap[3]; } set { AxisMap[3] = value; } }
        public double MotionX { get { return AxisMap[4]; } set { AxisMap[4] = value; } }
        public double MotionY { get { return AxisMap[5]; } set { AxisMap[5] = value; } }
        public double MotionZ { get { return AxisMap[6]; } set { AxisMap[6] = value; } }

        public bool[] ButtonMap = new bool[TwitchCommandList.BUTTON_LENGTH];
        public double[] AxisMap = new double[TwitchCommandList.AXIS_LENGTH];

        public virtual void UpdateControllerState() { }
    }
}
