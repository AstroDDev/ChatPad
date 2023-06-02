using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ChatPad.Configuration.JSONObjects;

namespace ChatPad.Configuration.OptionsForms
{
    public partial class OptionsForm : Form
    {
        public OptionsForm()
        {
            InitializeComponent();

            prefixTextBox.Text = Config.Settings.Prefix;
            voteLifeNumBox.Value = (decimal)Config.Settings.VoteLifespan;
            upsNumBox.Value = (decimal)Config.Settings.UpdatesPerSecond;
            buttonPressNumBox.Value = (decimal)Config.Settings.ButtonPressLength;
        }

        private void OptionsForm_Load(object sender, EventArgs e)
        {
            button1.Click += (s, evt) => OpenConfig(0, true, "A Button Commands");
            button2.Click += (s, evt) => OpenConfig(1, true, "B Button Commands");
            button3.Click += (s, evt) => OpenConfig(2, true, "X Button Commands");
            button4.Click += (s, evt) => OpenConfig(3, true, "Y Button Commands");

            button5.Click += (s, evt) => OpenConfig(4, true, "D-Pad Up Button Commands");
            button6.Click += (s, evt) => OpenConfig(5, true, "D-Pad Down Button Commands");
            button7.Click += (s, evt) => OpenConfig(6, true, "D-Pad Left Button Commands");
            button8.Click += (s, evt) => OpenConfig(7, true, "D-Pad Right Button Commands");

            button9.Click += (s, evt) => OpenConfig(8, true, "Left Bumper Button Commands");
            button10.Click += (s, evt) => OpenConfig(9, true, "Right Bumper Button Commands");
            button11.Click += (s, evt) => OpenConfig(10, true, "Left Trigger Button Commands");
            button12.Click += (s, evt) => OpenConfig(11, true, "Right Trigger Button Commands");

            button13.Click += (s, evt) => OpenConfig(12, true, "Left Stick Button Commands");
            button14.Click += (s, evt) => OpenConfig(13, true, "Right Stick Button Commands");
            button15.Click += (s, evt) => OpenConfig(14, true, "Plus Button Commands");
            button16.Click += (s, evt) => OpenConfig(15, true, "Minus Button Commands");

            button17.Click += (s, evt) => OpenConfig(0, false, "Left Stick X-Axis Commands");
            button18.Click += (s, evt) => OpenConfig(1, false, "Left Stick Y-Axis Commands");
            button19.Click += (s, evt) => OpenConfig(2, false, "Right Stick X-Axis Commands");
            button20.Click += (s, evt) => OpenConfig(3, false, "Right Stick Y-Axis Commands");

            button21.Click += (s, evt) => OpenConfig(4, false, "Motion X-Axis Commands");
            button22.Click += (s, evt) => OpenConfig(5, false, "Motion Y-Axis Commands");
            button23.Click += (s, evt) => OpenConfig(6, false, "Motion Z-Axis Commands");
        }

        private void OpenConfig(int index, bool button, string configname)
        {
            if (button)
            {
                ConfigForm form = new ConfigForm();

                form.title.Text = configname;
                form.label1.Text = "Press";
                form.label2.Text = "Hold";
                form.label3.Text = "Release";

                form.list1.Items.Clear();
                form.list1.Items.AddRange(Config.Commands.ButtonMap[index].Press);
                form.list2.Items.Clear();
                form.list2.Items.AddRange(Config.Commands.ButtonMap[index].Hold);
                form.list3.Items.Clear();
                form.list3.Items.AddRange(Config.Commands.ButtonMap[index].Release);

                form.enabledCheckBox.Checked = Config.Commands.ButtonMap[index].Enabled;
                form.passthroughCheckBox.Checked = Config.Commands.ButtonMap[index].Passthrough;

                form.thresholdBox.Value = (decimal)Config.Commands.ButtonMap[index].Threshold;

                form.Show();
                Enabled = false;

                form.save += () => {
                    Enabled = true;

                    Config.Commands.ButtonMap[index].Press = new string[form.list1.Items.Count];
                    form.list1.Items.CopyTo(Config.Commands.ButtonMap[index].Press, 0);
                    Config.Commands.ButtonMap[index].Hold = new string[form.list2.Items.Count];
                    form.list2.Items.CopyTo(Config.Commands.ButtonMap[index].Hold, 0);
                    Config.Commands.ButtonMap[index].Release = new string[form.list3.Items.Count];
                    form.list3.Items.CopyTo(Config.Commands.ButtonMap[index].Release, 0);

                    Config.Commands.ButtonMap[index].Enabled = form.enabledCheckBox.Checked;
                    Config.Commands.ButtonMap[index].Passthrough = form.passthroughCheckBox.Checked;
                    Config.Commands.ButtonMap[index].Threshold = (double)form.thresholdBox.Value;

                    form.Dispose();

                    Config.Save(Config.CONFIG_PATH);
                };
            }
            else
            {
                ConfigForm form = new ConfigForm();

                form.title.Text = configname;
                form.label1.Text = "Min (-1)";
                form.label2.Text = "Zero (0)";
                form.label3.Text = "Max (1)";

                form.list1.Items.Clear();
                form.list1.Items.AddRange(Config.Commands.AxisMap[index].Min);
                form.list2.Items.Clear();
                form.list2.Items.AddRange(Config.Commands.AxisMap[index].Zero);
                form.list3.Items.Clear();
                form.list3.Items.AddRange(Config.Commands.AxisMap[index].Max);

                form.enabledCheckBox.Checked = Config.Commands.AxisMap[index].Enabled;
                form.passthroughCheckBox.Checked = Config.Commands.AxisMap[index].Passthrough;

                form.thresholdBox.Value = (decimal)Config.Commands.AxisMap[index].Threshold;

                form.Show();
                Enabled = false;

                form.save += () => {
                    Enabled = true;

                    Config.Commands.AxisMap[index].Min = new string[form.list1.Items.Count];
                    form.list1.Items.CopyTo(Config.Commands.AxisMap[index].Min, 0);
                    Config.Commands.AxisMap[index].Zero = new string[form.list2.Items.Count];
                    form.list2.Items.CopyTo(Config.Commands.AxisMap[index].Zero, 0);
                    Config.Commands.AxisMap[index].Max = new string[form.list3.Items.Count];
                    form.list3.Items.CopyTo(Config.Commands.AxisMap[index].Max, 0);

                    Config.Commands.AxisMap[index].Enabled = form.enabledCheckBox.Checked;
                    Config.Commands.AxisMap[index].Passthrough = form.passthroughCheckBox.Checked;
                    Config.Commands.AxisMap[index].Threshold = (double)form.thresholdBox.Value;

                    form.Dispose();

                    Config.Save(Config.CONFIG_PATH);
                };

                form.FormClosing += (s, evt) =>
                {
                    form.Dispose();
                };
            }
        }

        private void OptionsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Config.Settings.Prefix = prefixTextBox.Text.Replace(" ", "").Replace("\t", "").Replace("\n", "");
            Config.Settings.VoteLifespan = (float)voteLifeNumBox.Value;
            Config.Settings.ButtonPressLength = (int)buttonPressNumBox.Value;
            Config.Settings.UpdatesPerSecond = (float)upsNumBox.Value;

            Config.Save();
        }
    }
}
