using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using vJoyInterfaceWrap;
using ChatPad.Graphics;
using System.Drawing.Text;
using ChatPad.Configuration;
using ChatPad.Input;
using static SDL2.SDL;
using ChatPad.Twitch;
using ChatPad.Configuration.OptionsForms;
using ChatPad.Configuration.JSONObjects;
using System.Drawing.Drawing2D;
using ChatPad.Properties;
using System.Runtime.InteropServices;
using ChatPad.Twitch.Prompt;

namespace ChatPad
{
    public partial class Form1 : Form
    {
        public static Form1 Instance;

        public Form1()
        {
            Instance = this;

            quicksandFont = new PrivateFontCollection();
            int fontLength = Resources.Quicksand_VariableFont_wght.Length;
            byte[] fontData = Resources.Quicksand_VariableFont_wght;
            IntPtr data = Marshal.AllocCoTaskMem(fontLength);
            Marshal.Copy(fontData, 0, data, fontLength);
            quicksandFont.AddMemoryFont(data, fontLength);

            InitializeComponent();
        }

        private void InitEvents()
        {
            buttonSelectMenu.Closing += (s, evt) => {
                if (evt.CloseReason == ToolStripDropDownCloseReason.ItemClicked) evt.Cancel = true;
            };
            motionSelectMenu.Closing += (s, evt) => {
                if (evt.CloseReason == ToolStripDropDownCloseReason.ItemClicked) evt.Cancel = true;
            };
            compoundSelectMenu.Closing += (s, evt) => {
                if (evt.CloseReason == ToolStripDropDownCloseReason.ItemClicked) evt.Cancel = true;
            };

            Resize += (s, evt) => controllerPanel.Invalidate();

            compoundSelectXAxisConfig.Click += (s, evt) => { axisSelectConfig_Click(0); };
            compoundSelectYAxisConfig.Click += (s, evt) => { axisSelectConfig_Click(1); };
            motionSelectXAxisConfig.Click += (s, evt) => { axisSelectConfig_Click(0); };
            motionSelectYAxisConfig.Click += (s, evt) => { axisSelectConfig_Click(1); };
            motionSelectZAxisConfig.Click += (s, evt) => { axisSelectConfig_Click(2); };

            buttonSelectEnabled.CheckedChanged += (s, evt) => { if (selectedButton > -1) controllerButtons[selectedButton].Button.Enabled = buttonSelectEnabled.Checked; };
            buttonSelectPassthrough.CheckedChanged += (s, evt) => { if (selectedButton > -1) controllerButtons[selectedButton].Button.Passthrough = buttonSelectPassthrough.Checked; };

            compoundSelectButtonEnabled.CheckedChanged += (s, evt) => { if (selectedButton > -1) controllerButtons[selectedButton].Button.Enabled = compoundSelectButtonEnabled.Checked; };
            compoundSelectButtonPassthrough.CheckedChanged += (s, evt) => { if (selectedButton > -1) controllerButtons[selectedButton].Button.Passthrough = compoundSelectButtonPassthrough.Checked; };
            compoundSelectXAxisEnabled.CheckedChanged += (s, evt) => { if (selectedButton > -1) controllerButtons[selectedButton].AxisX.Enabled = compoundSelectXAxisEnabled.Checked; };
            compoundSelectXAxisPassthrough.CheckedChanged += (s, evt) => { if (selectedButton > -1) controllerButtons[selectedButton].AxisX.Passthrough = compoundSelectXAxisPassthrough.Checked; };
            compoundSelectYAxisEnabled.CheckedChanged += (s, evt) => { if (selectedButton > -1) controllerButtons[selectedButton].AxisY.Enabled = compoundSelectYAxisEnabled.Checked; };
            compoundSelectYAxisPassthrough.CheckedChanged += (s, evt) => { if (selectedButton > -1) controllerButtons[selectedButton].AxisY.Passthrough = compoundSelectYAxisPassthrough.Checked; };

            loadConfigToolStripMenuItem.Click += (s, evt) =>
            {
                OpenFileDialog fileDialog = new OpenFileDialog() { Multiselect = false, Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*" };
                DialogResult result = fileDialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    Config.CONFIG_PATH = fileDialog.FileName;
                    Config.Load();
                    controllerPanel.Invalidate();
                }
            };
            saveConfigToolStripMenuItem.Click += (s, evt) =>
            {
                Config.Save();
            };
            resetConfigToolStripMenuItem.Click += (s, evt) =>
            {
                Config.Reset();
                controllerPanel.Invalidate();
            };
        }

        ControllerDriverManager controllerDriverManager;
        VirtualControllerManager virtualControllerManager;
        ControllerMapper controllerMapper;
        TwitchMapper twitchMapper;
        private void Form1_Load(object sender, EventArgs e)
        {
            InitEvents();

            controllerSelectList.SelectedIndex = 0;

            Config.Load("./config.json");

            channelTextbox.Text = Config.Settings.Channel;

            controllerMapper = new ControllerMapper();
            twitchMapper = new TwitchMapper();
            virtualControllerManager = new VirtualControllerManager(controllerMapper, twitchMapper);
            controllerDriverManager = new ControllerDriverManager(virtualControllerManager);

            controllerDriverManager.Init();

            SDL_SetHint(SDL_HINT_WINDOWS_DISABLE_THREAD_NAMING, "1");
            SDL_Init(SDL_INIT_GAMECONTROLLER + SDL_INIT_JOYSTICK + SDL_INIT_SENSOR + SDL_INIT_HAPTIC);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (controllerDriverManager != null) controllerDriverManager.Stop();
            TwitchManager.Stop();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.youtube.com/@astro_dev");
        }

        private void connectBtn_Click(object sender, EventArgs e)
        {
            connectBtn.Enabled = false;
            connectBtn.Text = "Connecting...";
            connectBtn.ForeColor = Color.FromArgb(195, 195, 0);
            connectBtn.Update();
            Config.Save();
            //Check if connects
            if ((TwitchManager.Manager == null || !TwitchManager.Manager.IsConnected)) {
                if (TwitchManager.TryConnect(channelTextbox.Text.Trim().ToLower()))
                {
                    connectBtn.Text = "Connected!";
                    connectBtn.ForeColor = Color.Green;
                }
                else
                {
                    connectBtn.Text = "Connection Error";
                    connectBtn.ForeColor = Color.Red;
                }
            }
            connectBtn.Enabled = true;
        }

        private PaintButton[] controllerButtons =
        {
            new PaintButton(PaintButtonControls.A, 180, -50, 35, 35, true, PaintButtonType.Button),
            new PaintButton(PaintButtonControls.B, 150, -20, 35, 35, true, PaintButtonType.Button),
            new PaintButton(PaintButtonControls.X, 150, -80, 35, 35, true, PaintButtonType.Button),
            new PaintButton(PaintButtonControls.Y, 120, -50, 35, 35, true, PaintButtonType.Button),

            new PaintButton(PaintButtonControls.Up, -100, 20, 35, 35, true, PaintButtonType.Button),
            new PaintButton(PaintButtonControls.Down, -100, 80, 35, 35, true, PaintButtonType.Button),
            new PaintButton(PaintButtonControls.Left, -130, 50, 35, 35, true, PaintButtonType.Button),
            new PaintButton(PaintButtonControls.Right, -70, 50, 35, 35, true, PaintButtonType.Button),

            new PaintButton(PaintButtonControls.LeftBumper, -250, 12.5f, 65, 40, false, PaintButtonType.Button),
            new PaintButton(PaintButtonControls.RightBumper, 250, 12.5f, 65, 40, false, PaintButtonType.Button),
            new PaintButton(PaintButtonControls.LeftTrigger, -250, -50, 65, 65, false, PaintButtonType.Button),
            new PaintButton(PaintButtonControls.RightTrigger, 250, -50, 65, 65, false, PaintButtonType.Button),

            new PaintButton(PaintButtonControls.LeftStick, -150, -50, 50, 50, new RectangleF(-200, -100, 100, 100), true, PaintButtonType.Stick),
            new PaintButton(PaintButtonControls.RightStick, 100, 50, 50, 50, new RectangleF(50, 0, 100, 100), true, PaintButtonType.Stick),

            new PaintButton(PaintButtonControls.Plus, 50, -75, 25, 25, true, PaintButtonType.Button),
            new PaintButton(PaintButtonControls.Minus, -50, -75, 25, 25, true, PaintButtonType.Button)
        };
        private PrivateFontCollection quicksandFont;
        private readonly PaintButton pauseplayButton = new PaintButton(PaintButtonControls.None, 0, -12.5f, 100, 100, true, PaintButtonType.None);
        private readonly GraphicsPath playpath = new GraphicsPath(new PointF[] { new PointF(24f, -12.5f), new PointF(-13.5f, 12.5f), new PointF(-13.5f, -37.5f), new PointF(24f, -12.5f) }, new byte[] { 0, 1, 1, 1 });
        private readonly SizeF drawingBounds = new SizeF(600, 300);
        private void controllerPanel_Paint(object sender, PaintEventArgs e)
        {
            Brush GetBrush(RectangleF rect, bool enabled, bool passthrough, bool depressed)
            {
                Color color1 = enabled ? passthrough ? Color.FromArgb(85, 85, 85) : Color.FromArgb(145, 70, 255) : Color.FromArgb(245, 245, 245);
                Color color2 = enabled ? passthrough ? Color.FromArgb(50, 50, 50) : Color.FromArgb(116, 56, 204) : Color.FromArgb(225, 225, 225);
                if (depressed)
                {
                    color1 = Color.FromArgb(color1.R - 25, color1.G - 25, color1.B - 25);
                    color2 = Color.FromArgb(color2.R - 25, color2.G - 25, color2.B - 25);
                }
                return new LinearGradientBrush(rect, color1, color2, LinearGradientMode.Vertical);
            }
            Brush GetFontBrush(bool enabled, bool passthrough, bool depressed)
            {
                Color color = enabled ? passthrough ? Color.FromArgb(40, 40, 40) : Color.FromArgb(90, 44, 160) : Color.FromArgb(200, 200, 200);
                if (depressed) color = Color.FromArgb(color.R - 25, color.G - 25, color.B - 25);
                return new SolidBrush(color);
            }
            Pen GetPen(bool enabled, bool passthrough, bool depressed)
            {
                Color color = enabled ? passthrough ? Color.FromArgb(45, 45, 45) : Color.FromArgb(101, 49, 178) : Color.FromArgb(215, 215, 215);
                if (depressed) color = Color.FromArgb(color.R - 25, color.G - 25, color.B - 25);
                return new Pen(color, 5) { LineJoin = LineJoin.Round, StartCap = LineCap.Round, EndCap = LineCap.Round };
            }
            GraphicsPath GenerateTrigger(RectangleF rect)
            {
                return new GraphicsPath(new PointF[] {
                rect.Location + new SizeF(0, rect.Height),
                rect.Location + rect.Size,

                rect.Location + new SizeF(rect.Width, rect.Height * 0.5f),
                rect.Location + new SizeF(rect.Width, 0),
                rect.Location + new SizeF(rect.Width * 0.5f, 0),

                rect.Location,
                rect.Location + new SizeF(0, rect.Height * 0.5f),
                rect.Location + new SizeF(0, rect.Height),
                }, new byte[] { 0, 1, 3, 3, 3, 3, 3, 3 });
            }

            //Transform for centering and aspect ration
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            e.Graphics.TranslateTransform(e.Graphics.VisibleClipBounds.Width / 2, e.Graphics.VisibleClipBounds.Height / 2);
            float scaleX = e.Graphics.VisibleClipBounds.Width / drawingBounds.Width;
            float scaleY = e.Graphics.VisibleClipBounds.Height / drawingBounds.Height;
            e.Graphics.ScaleTransform(Math.Min(scaleX, scaleY), Math.Min(scaleX, scaleY));

            //Pause / Play button
            e.Graphics.DrawEllipse(new Pen(RuntimeOptions.Paused ? Color.Green : Color.Red, 5), pauseplayButton.DrawRect);
            if (RuntimeOptions.Paused)
            {
                e.Graphics.FillPath(Brushes.Green, playpath);
                e.Graphics.DrawPath(new Pen(Color.Green, 5) { LineJoin = LineJoin.Round, EndCap = LineCap.Round, StartCap = LineCap.Round }, playpath);
            }
            else
            {
                e.Graphics.FillRectangle(Brushes.Red, -20, -32.5f, 40, 40);
                e.Graphics.DrawRectangle(new Pen(Color.Red, 5) { LineJoin = LineJoin.Round }, -20, -32.5f, 40, 40);
            }

            Font font = new Font(quicksandFont.Families[0], 21.5f, FontStyle.Bold);
            StringFormat format = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };

            //Draw all input buttons
            e.Graphics.FillEllipse(GetBrush(controllerButtons[0].DrawRect, Config.Commands.AButton.Enabled, Config.Commands.AButton.Passthrough, virtualControllerManager.AButton), controllerButtons[0].DrawRect);
            e.Graphics.DrawEllipse(GetPen(Config.Commands.AButton.Enabled, Config.Commands.AButton.Passthrough, virtualControllerManager.AButton), controllerButtons[0].DrawRect);
            e.Graphics.DrawString("A", font, GetFontBrush(Config.Commands.AButton.Enabled, Config.Commands.AButton.Passthrough, virtualControllerManager.AButton), controllerButtons[0].DrawCenter, format);
            e.Graphics.FillEllipse(GetBrush(controllerButtons[1].DrawRect, Config.Commands.BButton.Enabled, Config.Commands.BButton.Passthrough, virtualControllerManager.BButton), controllerButtons[1].DrawRect);
            e.Graphics.DrawEllipse(GetPen(Config.Commands.BButton.Enabled, Config.Commands.BButton.Passthrough, virtualControllerManager.BButton), controllerButtons[1].DrawRect);
            e.Graphics.DrawString("B", font, GetFontBrush(Config.Commands.BButton.Enabled, Config.Commands.BButton.Passthrough, virtualControllerManager.BButton), controllerButtons[1].DrawCenter + new SizeF(0, 1f), format);
            e.Graphics.FillEllipse(GetBrush(controllerButtons[2].DrawRect, Config.Commands.XButton.Enabled, Config.Commands.XButton.Passthrough, virtualControllerManager.XButton), controllerButtons[2].DrawRect);
            e.Graphics.DrawEllipse(GetPen(Config.Commands.XButton.Enabled, Config.Commands.XButton.Passthrough, virtualControllerManager.XButton), controllerButtons[2].DrawRect);
            e.Graphics.DrawString("X", font, GetFontBrush(Config.Commands.XButton.Enabled, Config.Commands.XButton.Passthrough, virtualControllerManager.XButton), controllerButtons[2].DrawCenter + new SizeF(0.5f, 1f), format);
            e.Graphics.FillEllipse(GetBrush(controllerButtons[3].DrawRect, Config.Commands.YButton.Enabled, Config.Commands.YButton.Passthrough, virtualControllerManager.YButton), controllerButtons[3].DrawRect);
            e.Graphics.DrawEllipse(GetPen(Config.Commands.YButton.Enabled, Config.Commands.YButton.Passthrough, virtualControllerManager.YButton), controllerButtons[3].DrawRect);
            e.Graphics.DrawString("Y", font, GetFontBrush(Config.Commands.YButton.Enabled, Config.Commands.YButton.Passthrough, virtualControllerManager.YButton), controllerButtons[3].DrawCenter + new SizeF(0, 2f), format);

            e.Graphics.FillEllipse(GetBrush(controllerButtons[4].DrawRect, Config.Commands.UpButton.Enabled, Config.Commands.UpButton.Passthrough, virtualControllerManager.UpButton), controllerButtons[4].DrawRect);
            e.Graphics.DrawEllipse(GetPen(Config.Commands.UpButton.Enabled, Config.Commands.UpButton.Passthrough, virtualControllerManager.UpButton), controllerButtons[4].DrawRect);
            e.Graphics.DrawString("▲", new Font(quicksandFont.Families[0], 14.5f, FontStyle.Bold), GetFontBrush(Config.Commands.UpButton.Enabled, Config.Commands.UpButton.Passthrough, virtualControllerManager.UpButton), controllerButtons[4].DrawCenter, format);
            e.Graphics.FillEllipse(GetBrush(controllerButtons[5].DrawRect, Config.Commands.DownButton.Enabled, Config.Commands.DownButton.Passthrough, virtualControllerManager.DownButton), controllerButtons[5].DrawRect);
            e.Graphics.DrawEllipse(GetPen(Config.Commands.DownButton.Enabled, Config.Commands.DownButton.Passthrough, virtualControllerManager.DownButton), controllerButtons[5].DrawRect);
            e.Graphics.DrawString("▼", new Font(quicksandFont.Families[0], 14.5f, FontStyle.Bold), GetFontBrush(Config.Commands.DownButton.Enabled, Config.Commands.DownButton.Passthrough, virtualControllerManager.DownButton), controllerButtons[5].DrawCenter + new SizeF(0f, 3.75f), format);
            e.Graphics.FillEllipse(GetBrush(controllerButtons[6].DrawRect, Config.Commands.LeftButton.Enabled, Config.Commands.LeftButton.Passthrough, virtualControllerManager.LeftButton), controllerButtons[6].DrawRect);
            e.Graphics.DrawEllipse(GetPen(Config.Commands.LeftButton.Enabled, Config.Commands.LeftButton.Passthrough, virtualControllerManager.LeftButton), controllerButtons[6].DrawRect);
            e.Graphics.DrawString("⯇", font, GetFontBrush(Config.Commands.LeftButton.Enabled, Config.Commands.LeftButton.Passthrough, virtualControllerManager.LeftButton), controllerButtons[6].DrawCenter + new SizeF(-0.5f, 2f), format);
            e.Graphics.FillEllipse(GetBrush(controllerButtons[7].DrawRect, Config.Commands.RightButton.Enabled, Config.Commands.RightButton.Passthrough, virtualControllerManager.RightButton), controllerButtons[7].DrawRect);
            e.Graphics.DrawEllipse(GetPen(Config.Commands.RightButton.Enabled, Config.Commands.RightButton.Passthrough, virtualControllerManager.RightButton), controllerButtons[7].DrawRect);
            e.Graphics.DrawString("⯈", font, GetFontBrush(Config.Commands.RightButton.Enabled, Config.Commands.RightButton.Passthrough, virtualControllerManager.RightButton), controllerButtons[7].DrawCenter + new SizeF(0.5f, 2f), format);

            e.Graphics.FillRectangle(GetBrush(controllerButtons[8].DrawRect, Config.Commands.LeftBumperButton.Enabled, Config.Commands.LeftBumperButton.Passthrough, virtualControllerManager.LeftBumperButton), controllerButtons[8].DrawRect);
            e.Graphics.DrawRectangle(GetPen(Config.Commands.LeftBumperButton.Enabled, Config.Commands.LeftBumperButton.Passthrough, virtualControllerManager.LeftBumperButton), controllerButtons[8].DrawRect.X, controllerButtons[8].DrawRect.Y, controllerButtons[8].DrawRect.Width, controllerButtons[8].DrawRect.Height);
            e.Graphics.DrawString("LB", font, GetFontBrush(Config.Commands.LeftBumperButton.Enabled, Config.Commands.LeftBumperButton.Passthrough, virtualControllerManager.LeftBumperButton), controllerButtons[8].DrawCenter, format);
            e.Graphics.FillRectangle(GetBrush(controllerButtons[9].DrawRect, Config.Commands.RightBumperButton.Enabled, Config.Commands.RightBumperButton.Passthrough, virtualControllerManager.RightBumperButton), controllerButtons[9].DrawRect);
            e.Graphics.DrawRectangle(GetPen(Config.Commands.RightBumperButton.Enabled, Config.Commands.RightBumperButton.Passthrough, virtualControllerManager.RightBumperButton), controllerButtons[9].DrawRect.X, controllerButtons[9].DrawRect.Y, controllerButtons[9].DrawRect.Width, controllerButtons[9].DrawRect.Height);
            e.Graphics.DrawString("RB", font, GetFontBrush(Config.Commands.RightBumperButton.Enabled, Config.Commands.RightBumperButton.Passthrough, virtualControllerManager.RightBumperButton), controllerButtons[9].DrawCenter, format);

            GraphicsPath leftTrigger = GenerateTrigger(controllerButtons[10].DrawRect);
            GraphicsPath rightTrigger = GenerateTrigger(controllerButtons[11].DrawRect);
            e.Graphics.FillPath(GetBrush(controllerButtons[10].DrawRect, Config.Commands.LeftTriggerButton.Enabled, Config.Commands.LeftTriggerButton.Passthrough, virtualControllerManager.LeftTriggerButton), leftTrigger);
            e.Graphics.DrawPath(GetPen(Config.Commands.LeftTriggerButton.Enabled, Config.Commands.LeftTriggerButton.Passthrough, virtualControllerManager.LeftTriggerButton), leftTrigger);
            e.Graphics.DrawString("LT", font, GetFontBrush(Config.Commands.LeftTriggerButton.Enabled, Config.Commands.LeftTriggerButton.Passthrough, virtualControllerManager.LeftTriggerButton), controllerButtons[10].DrawCenter + new SizeF(0, 5f), format);
            e.Graphics.FillPath(GetBrush(controllerButtons[11].DrawRect, Config.Commands.RightTriggerButton.Enabled, Config.Commands.RightTriggerButton.Passthrough, virtualControllerManager.RightTriggerButton), rightTrigger);
            e.Graphics.DrawPath(GetPen(Config.Commands.RightTriggerButton.Enabled, Config.Commands.RightTriggerButton.Passthrough, virtualControllerManager.RightTriggerButton), rightTrigger);
            e.Graphics.DrawString("RT", font, GetFontBrush(Config.Commands.RightTriggerButton.Enabled, Config.Commands.RightTriggerButton.Passthrough, virtualControllerManager.RightTriggerButton), controllerButtons[11].DrawCenter + new SizeF(0, 5f), format);

            e.Graphics.FillEllipse(GetBrush(controllerButtons[14].DrawRect, Config.Commands.PlusButton.Enabled, Config.Commands.PlusButton.Passthrough, virtualControllerManager.PlusButton), controllerButtons[14].DrawRect);
            e.Graphics.DrawEllipse(GetPen(Config.Commands.PlusButton.Enabled, Config.Commands.PlusButton.Passthrough, virtualControllerManager.PlusButton), controllerButtons[14].DrawRect);
            e.Graphics.DrawString("+", new Font(quicksandFont.Families[0], 17.5f, FontStyle.Bold), GetFontBrush(Config.Commands.PlusButton.Enabled, Config.Commands.PlusButton.Passthrough, virtualControllerManager.PlusButton), controllerButtons[14].DrawCenter + new SizeF(0, 0.6f), format);
            e.Graphics.FillEllipse(GetBrush(controllerButtons[15].DrawRect, Config.Commands.MinusButton.Enabled, Config.Commands.MinusButton.Passthrough, virtualControllerManager.MinusButton), controllerButtons[15].DrawRect);
            e.Graphics.DrawEllipse(GetPen(Config.Commands.MinusButton.Enabled, Config.Commands.MinusButton.Passthrough, virtualControllerManager.MinusButton), controllerButtons[15].DrawRect);
            e.Graphics.DrawString("-", font, GetFontBrush(Config.Commands.MinusButton.Enabled, Config.Commands.MinusButton.Passthrough, virtualControllerManager.MinusButton), controllerButtons[15].DrawCenter, format);

            //Sticks
            e.Graphics.DrawEllipse(new Pen(Color.FromArgb(215, 215, 215), 5), 50, 0, 100, 100);
            e.Graphics.DrawEllipse(new Pen(Color.FromArgb(215, 215, 215), 5), -200, -100, 100, 100);

            double mag = Math.Sqrt(Math.Pow(virtualControllerManager.LeftStickX, 2) + Math.Pow(virtualControllerManager.LeftStickY, 2));
            controllerButtons[12].SetDrawCenter(-150 + (float)(virtualControllerManager.LeftStickX / (mag > 1 ? mag : 1) * 40), -50 + (float)(virtualControllerManager.LeftStickY / (mag > 1 ? mag : 1) * 40));
            e.Graphics.FillEllipse(GetBrush(controllerButtons[12].DrawRect, Config.Commands.LeftStickXAxis.Enabled || Config.Commands.LeftStickYAxis.Enabled, Config.Commands.LeftStickXAxis.Passthrough && Config.Commands.LeftStickYAxis.Passthrough, virtualControllerManager.LeftStickButton), controllerButtons[12].DrawRect);
            e.Graphics.DrawEllipse(GetPen(Config.Commands.LeftStickXAxis.Enabled || Config.Commands.LeftStickYAxis.Enabled, Config.Commands.LeftStickXAxis.Passthrough && Config.Commands.LeftStickYAxis.Passthrough, virtualControllerManager.LeftStickButton), controllerButtons[12].DrawRect);
            e.Graphics.DrawString("LS", font, GetFontBrush(Config.Commands.LeftStickXAxis.Enabled || Config.Commands.LeftStickYAxis.Enabled, Config.Commands.LeftStickXAxis.Passthrough && Config.Commands.LeftStickYAxis.Passthrough, virtualControllerManager.LeftStickButton), controllerButtons[12].DrawCenter + new SizeF(0, 1f), format);

            mag = Math.Sqrt(Math.Pow(virtualControllerManager.RightStickX, 2) + Math.Pow(virtualControllerManager.RightStickY, 2));
            controllerButtons[13].SetDrawCenter(100 + (float)(virtualControllerManager.RightStickX / (mag > 1 ? mag : 1) * 40), 50 + (float)(virtualControllerManager.RightStickY / (mag > 1 ? mag : 1) * 40));
            e.Graphics.FillEllipse(GetBrush(controllerButtons[13].DrawRect, Config.Commands.RightStickXAxis.Enabled || Config.Commands.RightStickYAxis.Enabled, Config.Commands.RightStickXAxis.Passthrough && Config.Commands.RightStickYAxis.Passthrough, virtualControllerManager.RightStickButton), controllerButtons[13].DrawRect);
            e.Graphics.DrawEllipse(GetPen(Config.Commands.RightStickXAxis.Enabled || Config.Commands.RightStickYAxis.Enabled, Config.Commands.RightStickXAxis.Passthrough && Config.Commands.RightStickYAxis.Passthrough, virtualControllerManager.RightStickButton), controllerButtons[13].DrawRect);
            e.Graphics.DrawString("RS", font, GetFontBrush(Config.Commands.RightStickXAxis.Enabled || Config.Commands.RightStickYAxis.Enabled, Config.Commands.RightStickXAxis.Passthrough && Config.Commands.RightStickYAxis.Passthrough, virtualControllerManager.RightStickButton), controllerButtons[13].DrawCenter + new SizeF(0, 1f), format);
        }

        private int selectedButton = -1;
        private void controllerPanel_Click(object sender, MouseEventArgs e)
        {
            PointF click = PaintButton.TransformPoint(e.Location, controllerPanel.Size, drawingBounds);

            for (int i = 0; i < controllerButtons.Length; i++)
            {
                if (controllerButtons[i].InHovered(click))
                {
                    selectedButton = i;
                    //Show correct config menu
                    switch (controllerButtons[i].Type)
                    {
                        case PaintButtonType.Button:
                            buttonSelectName.Text = controllerButtons[i].Name;

                            buttonSelectEnabled.Checked = Config.Commands.ButtonMap[i].Enabled;
                            buttonSelectPassthrough.Checked = Config.Commands.ButtonMap[i].Passthrough;

                            buttonSelectMenu.Show(MousePosition);
                            return;
                        case PaintButtonType.Motion:
                            motionSelectXAxisEnabled.Checked = Config.Commands.MotionXAxis.Enabled;
                            motionSelectYAxisEnabled.Checked = Config.Commands.MotionYAxis.Enabled;
                            motionSelectZAxisEnabled.Checked = Config.Commands.MotionZAxis.Enabled;

                            motionSelectXAxisPassthrough.Checked = Config.Commands.MotionXAxis.Passthrough;
                            motionSelectYAxisPassthrough.Checked = Config.Commands.MotionYAxis.Passthrough;
                            motionSelectZAxisPassthrough.Checked = Config.Commands.MotionZAxis.Passthrough;

                            motionSelectMenu.Show(MousePosition);
                            return;
                        case PaintButtonType.Stick:
                            compoundSelectName.Text = controllerButtons[i].Name;

                            compoundSelectButtonEnabled.Checked = controllerButtons[i].Button.Enabled;
                            compoundSelectButtonPassthrough.Checked = controllerButtons[i].Button.Passthrough;

                            compoundSelectXAxisEnabled.Checked = controllerButtons[i].AxisX.Enabled;
                            compoundSelectXAxisPassthrough.Checked = controllerButtons[i].AxisX.Passthrough;

                            compoundSelectYAxisEnabled.Checked = controllerButtons[i].AxisY.Enabled;
                            compoundSelectYAxisPassthrough.Checked = controllerButtons[i].AxisY.Passthrough;

                            compoundSelectMenu.Show(MousePosition);
                            return;
                    }
                }
            }

            if (pauseplayButton.InHovered(click))
            {
                RuntimeOptions.Paused = !RuntimeOptions.Paused;
                controllerPanel.Invalidate();
            }
        }

        private void buttonSelectConfig_Click(object sender, EventArgs e)
        {
            //Open Button Config Form
            ConfigForm form = new ConfigForm();

            form.title.Text = controllerButtons[selectedButton].Name + " Button Commands";
            form.label1.Text = "Press";
            form.label2.Text = "Hold";
            form.label3.Text = "Release";

            form.list1.Items.Clear();
            form.list1.Items.AddRange(controllerButtons[selectedButton].Button.Press);
            form.list2.Items.Clear();
            form.list2.Items.AddRange(controllerButtons[selectedButton].Button.Hold);
            form.list3.Items.Clear();
            form.list3.Items.AddRange(controllerButtons[selectedButton].Button.Release);

            form.enabledCheckBox.Checked = controllerButtons[selectedButton].Button.Enabled;
            form.passthroughCheckBox.Checked = controllerButtons[selectedButton].Button.Passthrough;

            form.thresholdBox.Value = (decimal)controllerButtons[selectedButton].Button.Threshold;

            form.Show();
            Enabled = false;

            form.save += () => {
                Enabled = true;

                controllerButtons[selectedButton].Button.Press = new string[form.list1.Items.Count];
                form.list1.Items.CopyTo(controllerButtons[selectedButton].Button.Press, 0);
                controllerButtons[selectedButton].Button.Hold = new string[form.list2.Items.Count];
                form.list2.Items.CopyTo(controllerButtons[selectedButton].Button.Hold, 0);
                controllerButtons[selectedButton].Button.Release = new string[form.list3.Items.Count];
                form.list3.Items.CopyTo(controllerButtons[selectedButton].Button.Release, 0);

                controllerButtons[selectedButton].Button.Enabled = form.enabledCheckBox.Checked;
                controllerButtons[selectedButton].Button.Passthrough = form.passthroughCheckBox.Checked;
                controllerButtons[selectedButton].Button.Threshold = (double)form.thresholdBox.Value;

                Config.Save(Config.CONFIG_PATH);

                form.Close();
            };

            form.FormClosing += (s, evt) =>
            {
                Enabled = true;
                form.Dispose();
            };
        }

        string[] axisName = { "X", "Y", "Z" };
        private void axisSelectConfig_Click(int axis)
        {
            ConfigForm form = new ConfigForm();

            form.title.Text = controllerButtons[selectedButton].Name + " " + axisName[axis] + "-Axis Commands";
            form.label1.Text = "Min (-1)";
            form.label2.Text = "Zero (0)";
            form.label3.Text = "Max (1)";

            CommandAxis directAxis;
            switch (axis)
            {
                case 0:
                    directAxis = controllerButtons[selectedButton].AxisX;
                    break;
                case 1:
                    directAxis = controllerButtons[selectedButton].AxisY;
                    break;
                default:
                    directAxis = controllerButtons[selectedButton].AxisZ;
                    break;
            }

            form.list1.Items.Clear();
            form.list1.Items.AddRange(directAxis.Min);
            form.list2.Items.Clear();
            form.list2.Items.AddRange(directAxis.Zero);
            form.list3.Items.Clear();
            form.list3.Items.AddRange(directAxis.Max);

            form.enabledCheckBox.Checked = directAxis.Enabled;
            form.passthroughCheckBox.Checked = directAxis.Passthrough;

            form.thresholdBox.Value = (decimal)directAxis.Threshold;

            form.Show();
            Enabled = false;

            form.save += () => { 
                Enabled = true;

                directAxis.Min = new string[form.list1.Items.Count];
                form.list1.Items.CopyTo(directAxis.Min, 0);
                directAxis.Zero = new string[form.list2.Items.Count];
                form.list2.Items.CopyTo(directAxis.Zero, 0);
                directAxis.Max = new string[form.list3.Items.Count];
                form.list3.Items.CopyTo(directAxis.Max, 0);

                directAxis.Enabled = form.enabledCheckBox.Checked;
                directAxis.Passthrough = form.passthroughCheckBox.Checked;
                directAxis.Threshold = (double)form.thresholdBox.Value;

                Config.Save(Config.CONFIG_PATH);

                form.Close();
            };

            form.FormClosing += (s, evt) =>
            {
                Enabled = true;
                form.Dispose();
            };
        }

        private void controllerSelectList_DropDown(object sender, EventArgs e)
        {
            controllerMapper.UpdateList();
            controllerSelectList.Items.Clear();
            controllerSelectList.Items.Add("Select A Controller");
            foreach (string device in controllerMapper.ControllerList)
            {
                controllerSelectList.Items.Add(device);
            }
            controllerSelectList.SelectedIndex = controllerMapper.SelectedIndex + 1;
        }

        private void controllerSelectList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (controllerSelectList.SelectedIndex > 0) controllerMapper.SelectDevice(controllerSelectList.SelectedIndex - 1);
            if (controllerMapper != null && controllerMapper.SelectedIndex > -1) controllerMapper.UpdateControllerState();
        }

        private void controllerOptionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OptionsForm form = new OptionsForm();
            Enabled = false;
            form.Show();
            form.FormClosing += (s, evt) =>
            {
                Enabled = true;
                form.Dispose();
            };
        }

        private void twitchOptionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OAuthPrompt form = new OAuthPrompt();
            Enabled = false;
            form.Show();
            form.FormClosing += (s, evt) =>
            {
                Enabled = true;
                form.Dispose();
            };
        }
    }
}
