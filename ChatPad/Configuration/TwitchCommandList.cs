using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;
using ChatPad.Configuration.JSONObjects;

namespace ChatPad.Configuration
{
    internal class TwitchCommandList
    {
        [JsonIgnore] public const int BUTTON_LENGTH = 16;
        [JsonIgnore] public const int AXIS_LENGTH = 4;
        [JsonIgnore] public static int MACRO_LENGTH { get; private set; } = 0;

        [JsonProperty("left_stick_x_axis")] public CommandAxis LeftStickXAxis { get { return AxisMap[0]; } set { AxisMap[0] = value; } }
        [JsonProperty("left_stick_y_axis")] public CommandAxis LeftStickYAxis { get { return AxisMap[1]; } set { AxisMap[1] = value; } }
        [JsonProperty("right_stick_x_axis")] public CommandAxis RightStickXAxis { get { return AxisMap[2]; } set { AxisMap[2] = value; } }
        [JsonProperty("right_stick_y_axis")] public CommandAxis RightStickYAxis { get { return AxisMap[3]; } set { AxisMap[3] = value; } }

        [JsonProperty("a_button")] public CommandButton AButton { get { return ButtonMap[0]; } set { ButtonMap[0] = value; } }
        [JsonProperty("b_button")] public CommandButton BButton { get { return ButtonMap[1]; } set { ButtonMap[1] = value; } }
        [JsonProperty("x_button")] public CommandButton XButton { get { return ButtonMap[2]; } set { ButtonMap[2] = value; } }
        [JsonProperty("y_button")] public CommandButton YButton { get { return ButtonMap[3]; } set { ButtonMap[3] = value; } }
        [JsonProperty("up_button")] public CommandButton UpButton { get { return ButtonMap[4]; } set { ButtonMap[4] = value; } }
        [JsonProperty("down_button")] public CommandButton DownButton { get { return ButtonMap[5]; } set { ButtonMap[5] = value; } }
        [JsonProperty("left_button")] public CommandButton LeftButton { get { return ButtonMap[6]; } set { ButtonMap[6] = value; } }
        [JsonProperty("right_button")] public CommandButton RightButton { get { return ButtonMap[7]; } set { ButtonMap[7] = value; } }
        [JsonProperty("left_bumper_button")] public CommandButton LeftBumperButton { get { return ButtonMap[8]; } set { ButtonMap[8] = value; } }
        [JsonProperty("right_bumper_button")] public CommandButton RightBumperButton { get { return ButtonMap[9]; } set { ButtonMap[9] = value; } }
        [JsonProperty("left_trigger_button")] public CommandButton LeftTriggerButton { get { return ButtonMap[10]; } set { ButtonMap[10] = value; } }
        [JsonProperty("right_trigger_button")] public CommandButton RightTriggerButton { get { return ButtonMap[11]; } set { ButtonMap[11] = value; } }
        [JsonProperty("left_stick_button")] public CommandButton LeftStickButton { get { return ButtonMap[12]; } set { ButtonMap[12] = value; } }
        [JsonProperty("right_stick_button")] public CommandButton RightStickButton { get { return ButtonMap[13]; } set { ButtonMap[13] = value; } }
        [JsonProperty("plus_button")] public CommandButton PlusButton { get { return ButtonMap[14]; } set { ButtonMap[14] = value; } }
        [JsonProperty("minus_button")] public CommandButton MinusButton { get { return ButtonMap[15]; } set { ButtonMap[15] = value; } }

        [JsonIgnore] public CommandButton[] ButtonMap = new CommandButton[BUTTON_LENGTH];
        [JsonIgnore] public CommandAxis[] AxisMap = new CommandAxis[AXIS_LENGTH];

        public TwitchCommandList()
        {
            ButtonMap = new CommandButton[BUTTON_LENGTH];
            AxisMap = new CommandAxis[AXIS_LENGTH];

            for (int i = 0; i < BUTTON_LENGTH; i++)
            {
                ButtonMap[i] = new CommandButton();
            }
            for (int i = 0; i < AXIS_LENGTH; i++)
            {
                AxisMap[i] = new CommandAxis();
            }
        }

        public void ButtonOutput(string[] cmds, ref bool[] buttonPress, ref bool[] buttonHold, ref bool[] buttonRelease)
        {
            buttonPress = new bool[BUTTON_LENGTH];
            buttonHold = new bool[BUTTON_LENGTH];
            buttonRelease = new bool[BUTTON_LENGTH];
            for (int i = 0; i < BUTTON_LENGTH; i++)
            {
                for (int j = 0; j < cmds.Length; j++)
                {
                    buttonPress[i] = ButtonMap[i].Press.Contains(cmds[j]);
                    buttonHold[i] = ButtonMap[i].Hold.Contains(cmds[j]);
                    buttonRelease[i] = ButtonMap[i].Release.Contains(cmds[j]);
                    if (buttonPress[i] || buttonHold[i] || buttonRelease[i]) break;
                }
            }
        }

        public void AxisOutput(string[] cmds, ref float[] axisMap, ref bool[] axisVote)
        {
            axisMap = new float[AXIS_LENGTH];
            axisVote = new bool[AXIS_LENGTH];
            for (int i = 0; i < AXIS_LENGTH; i++)
            {
                bool max = false;
                bool min = false;
                bool zero = false;
                for (int j = 0; j < cmds.Length; j++)
                {
                    max = max || AxisMap[i].Max.Contains(cmds[j]);
                    min = min || AxisMap[i].Min.Contains(cmds[j]);
                    zero = zero || AxisMap[i].Zero.Contains(cmds[j]);
                }
                axisMap[i] = (max ? 1 : 0) + (min ? -1 : 0);
                axisVote[i] = max || min || zero;
            }
        }

        //[JsonIgnore] private static string[] helpButtons = { "A", "B", "X", "Y", "D-Pad Up", "D-Pad Down", "D-Pad Left", "D-Pad Right", "Left Bumper", "Right Bumper", "Left Trigger", "Right Trigger", "Left Stick Button", "Right Stick Button", "Plus", "Minus" };

        public string GenerateHelpList()
        {
            StringBuilder str = new StringBuilder();

            for (int i = 0; i < AxisMap.Length; i++)
            {
                if (AxisMap[i].Enabled && !AxisMap[i].Passthrough)
                {
                    if (AxisMap[i].Min.Length > 0)
                    {
                        str.Append("[" + string.Join(", ", AxisMap[i].Min) + "] ");
                    }
                    if (AxisMap[i].Zero.Length > 0)
                    {
                        str.Append("[" + string.Join(", ", AxisMap[i].Zero) + "] ");
                    }
                    if (AxisMap[i].Max.Length > 0)
                    {
                        str.Append("[" + string.Join(", ", AxisMap[i].Max) + "] ");
                    }
                }
            }

            for (int i = 0; i < ButtonMap.Length; i++)
            {
                if (ButtonMap[i].Enabled && !ButtonMap[i].Passthrough)
                {
                    if (ButtonMap[i].Press.Length > 0)
                    {
                        str.Append("[" + string.Join(", ", ButtonMap[i].Press) + "] ");
                    }
                    if (ButtonMap[i].Hold.Length > 0)
                    {
                        str.Append("[" + string.Join(", ", ButtonMap[i].Hold) + "] ");
                    }
                    if (ButtonMap[i].Release.Length > 0)
                    {
                        str.Append("[" + string.Join(", ", ButtonMap[i].Release) + "] ");
                    }
                }
            }

            return str.ToString();
        }
    }
}
