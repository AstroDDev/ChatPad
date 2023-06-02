using System.Windows.Forms;
using ChatPad.Graphics;

namespace ChatPad
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadConfigToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveConfigToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resetConfigToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.channelTextbox = new System.Windows.Forms.TextBox();
            this.connectBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.controllerSelectList = new System.Windows.Forms.ComboBox();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.buttonSelectMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.buttonSelectName = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.buttonSelectEnabled = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonSelectPassthrough = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonSelectConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.motionSelectMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.axisSelectName = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.xAxisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.motionSelectXAxisEnabled = new System.Windows.Forms.ToolStripMenuItem();
            this.motionSelectXAxisPassthrough = new System.Windows.Forms.ToolStripMenuItem();
            this.motionSelectXAxisConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.yAxisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.motionSelectYAxisEnabled = new System.Windows.Forms.ToolStripMenuItem();
            this.motionSelectYAxisPassthrough = new System.Windows.Forms.ToolStripMenuItem();
            this.motionSelectYAxisConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.zAxisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.motionSelectZAxisEnabled = new System.Windows.Forms.ToolStripMenuItem();
            this.motionSelectZAxisPassthrough = new System.Windows.Forms.ToolStripMenuItem();
            this.motionSelectZAxisConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.compoundSelectMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.compoundSelectName = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
            this.compoundSelectButtonEnabled = new System.Windows.Forms.ToolStripMenuItem();
            this.compoundSelectButtonPassthrough = new System.Windows.Forms.ToolStripMenuItem();
            this.compoundSelectButtonConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripMenuItem();
            this.compoundSelectXAxisEnabled = new System.Windows.Forms.ToolStripMenuItem();
            this.compoundSelectXAxisPassthrough = new System.Windows.Forms.ToolStripMenuItem();
            this.compoundSelectXAxisConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem8 = new System.Windows.Forms.ToolStripMenuItem();
            this.compoundSelectYAxisEnabled = new System.Windows.Forms.ToolStripMenuItem();
            this.compoundSelectYAxisPassthrough = new System.Windows.Forms.ToolStripMenuItem();
            this.compoundSelectYAxisConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.controllerPanel = new ChatPad.Graphics.BufferedPanel();
            this.controllerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.twitchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.buttonSelectMenu.SuspendLayout();
            this.motionSelectMenu.SuspendLayout();
            this.compoundSelectMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.optionsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(933, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadConfigToolStripMenuItem,
            this.saveConfigToolStripMenuItem,
            this.resetConfigToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // loadConfigToolStripMenuItem
            // 
            this.loadConfigToolStripMenuItem.Name = "loadConfigToolStripMenuItem";
            this.loadConfigToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.loadConfigToolStripMenuItem.Text = "Load Config";
            // 
            // saveConfigToolStripMenuItem
            // 
            this.saveConfigToolStripMenuItem.Name = "saveConfigToolStripMenuItem";
            this.saveConfigToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.saveConfigToolStripMenuItem.Text = "Save Config";
            // 
            // resetConfigToolStripMenuItem
            // 
            this.resetConfigToolStripMenuItem.Name = "resetConfigToolStripMenuItem";
            this.resetConfigToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.resetConfigToolStripMenuItem.Text = "Reset Config";
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.controllerToolStripMenuItem,
            this.twitchToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.optionsToolStripMenuItem.Text = "Options";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Controls.Add(this.channelTextbox, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.connectBtn, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.controllerSelectList, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.linkLabel1, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 24);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(933, 100);
            this.tableLayoutPanel1.TabIndex = 19;
            // 
            // channelTextbox
            // 
            this.channelTextbox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.channelTextbox.Location = new System.Drawing.Point(366, 38);
            this.channelTextbox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.channelTextbox.MaxLength = 25;
            this.channelTextbox.Name = "channelTextbox";
            this.channelTextbox.Size = new System.Drawing.Size(200, 22);
            this.channelTextbox.TabIndex = 10;
            this.channelTextbox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // connectBtn
            // 
            this.connectBtn.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.connectBtn.Location = new System.Drawing.Point(392, 70);
            this.connectBtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.connectBtn.Name = "connectBtn";
            this.connectBtn.Size = new System.Drawing.Size(149, 26);
            this.connectBtn.TabIndex = 11;
            this.connectBtn.Text = "Connect";
            this.connectBtn.UseVisualStyleBackColor = true;
            this.connectBtn.Click += new System.EventHandler(this.connectBtn_Click);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(411, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 16);
            this.label1.TabIndex = 9;
            this.label1.Text = "Twitch Channel";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // controllerSelectList
            // 
            this.controllerSelectList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.controllerSelectList.FormattingEnabled = true;
            this.controllerSelectList.Items.AddRange(new object[] {
            "Select A Controller"});
            this.controllerSelectList.Location = new System.Drawing.Point(3, 2);
            this.controllerSelectList.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.controllerSelectList.MaxDropDownItems = 16;
            this.controllerSelectList.Name = "controllerSelectList";
            this.controllerSelectList.Size = new System.Drawing.Size(200, 24);
            this.controllerSelectList.TabIndex = 0;
            this.controllerSelectList.DropDown += new System.EventHandler(this.controllerSelectList_DropDown);
            this.controllerSelectList.SelectedIndexChanged += new System.EventHandler(this.controllerSelectList_SelectedIndexChanged);
            // 
            // linkLabel1
            // 
            this.linkLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(768, 0);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(162, 16);
            this.linkLabel1.TabIndex = 12;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "youtube.com/@astro_dev";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // buttonSelectMenu
            // 
            this.buttonSelectMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.buttonSelectName,
            this.toolStripSeparator2,
            this.buttonSelectEnabled,
            this.buttonSelectPassthrough,
            this.buttonSelectConfig});
            this.buttonSelectMenu.Name = "contextMenuStrip1";
            this.buttonSelectMenu.Size = new System.Drawing.Size(146, 98);
            // 
            // buttonSelectName
            // 
            this.buttonSelectName.Enabled = false;
            this.buttonSelectName.Name = "buttonSelectName";
            this.buttonSelectName.Size = new System.Drawing.Size(145, 22);
            this.buttonSelectName.Text = "Button Name";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(142, 6);
            // 
            // buttonSelectEnabled
            // 
            this.buttonSelectEnabled.CheckOnClick = true;
            this.buttonSelectEnabled.Name = "buttonSelectEnabled";
            this.buttonSelectEnabled.Size = new System.Drawing.Size(145, 22);
            this.buttonSelectEnabled.Text = "Enabled";
            // 
            // buttonSelectPassthrough
            // 
            this.buttonSelectPassthrough.CheckOnClick = true;
            this.buttonSelectPassthrough.Name = "buttonSelectPassthrough";
            this.buttonSelectPassthrough.Size = new System.Drawing.Size(145, 22);
            this.buttonSelectPassthrough.Text = "Passthrough";
            // 
            // buttonSelectConfig
            // 
            this.buttonSelectConfig.Name = "buttonSelectConfig";
            this.buttonSelectConfig.Size = new System.Drawing.Size(145, 22);
            this.buttonSelectConfig.Text = "Configure";
            this.buttonSelectConfig.Click += new System.EventHandler(this.buttonSelectConfig_Click);
            // 
            // motionSelectMenu
            // 
            this.motionSelectMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.axisSelectName,
            this.toolStripSeparator1,
            this.xAxisToolStripMenuItem,
            this.yAxisToolStripMenuItem,
            this.zAxisToolStripMenuItem});
            this.motionSelectMenu.Name = "contextMenuStrip1";
            this.motionSelectMenu.Size = new System.Drawing.Size(162, 98);
            // 
            // axisSelectName
            // 
            this.axisSelectName.Enabled = false;
            this.axisSelectName.Name = "axisSelectName";
            this.axisSelectName.Size = new System.Drawing.Size(161, 22);
            this.axisSelectName.Text = "Motion Controls";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(158, 6);
            // 
            // xAxisToolStripMenuItem
            // 
            this.xAxisToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.motionSelectXAxisEnabled,
            this.motionSelectXAxisPassthrough,
            this.motionSelectXAxisConfig});
            this.xAxisToolStripMenuItem.Name = "xAxisToolStripMenuItem";
            this.xAxisToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.xAxisToolStripMenuItem.Text = "X Axis";
            // 
            // motionSelectXAxisEnabled
            // 
            this.motionSelectXAxisEnabled.Name = "motionSelectXAxisEnabled";
            this.motionSelectXAxisEnabled.Size = new System.Drawing.Size(140, 22);
            this.motionSelectXAxisEnabled.Text = "Enabled";
            // 
            // motionSelectXAxisPassthrough
            // 
            this.motionSelectXAxisPassthrough.Name = "motionSelectXAxisPassthrough";
            this.motionSelectXAxisPassthrough.Size = new System.Drawing.Size(140, 22);
            this.motionSelectXAxisPassthrough.Text = "Passthrough";
            // 
            // motionSelectXAxisConfig
            // 
            this.motionSelectXAxisConfig.Name = "motionSelectXAxisConfig";
            this.motionSelectXAxisConfig.Size = new System.Drawing.Size(140, 22);
            this.motionSelectXAxisConfig.Text = "Configure";
            // 
            // yAxisToolStripMenuItem
            // 
            this.yAxisToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.motionSelectYAxisEnabled,
            this.motionSelectYAxisPassthrough,
            this.motionSelectYAxisConfig});
            this.yAxisToolStripMenuItem.Name = "yAxisToolStripMenuItem";
            this.yAxisToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.yAxisToolStripMenuItem.Text = "Y Axis";
            // 
            // motionSelectYAxisEnabled
            // 
            this.motionSelectYAxisEnabled.Name = "motionSelectYAxisEnabled";
            this.motionSelectYAxisEnabled.Size = new System.Drawing.Size(140, 22);
            this.motionSelectYAxisEnabled.Text = "Enabled";
            // 
            // motionSelectYAxisPassthrough
            // 
            this.motionSelectYAxisPassthrough.Name = "motionSelectYAxisPassthrough";
            this.motionSelectYAxisPassthrough.Size = new System.Drawing.Size(140, 22);
            this.motionSelectYAxisPassthrough.Text = "Passthrough";
            // 
            // motionSelectYAxisConfig
            // 
            this.motionSelectYAxisConfig.Name = "motionSelectYAxisConfig";
            this.motionSelectYAxisConfig.Size = new System.Drawing.Size(140, 22);
            this.motionSelectYAxisConfig.Text = "Configure";
            // 
            // zAxisToolStripMenuItem
            // 
            this.zAxisToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.motionSelectZAxisEnabled,
            this.motionSelectZAxisPassthrough,
            this.motionSelectZAxisConfig});
            this.zAxisToolStripMenuItem.Name = "zAxisToolStripMenuItem";
            this.zAxisToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.zAxisToolStripMenuItem.Text = "Z Axis";
            // 
            // motionSelectZAxisEnabled
            // 
            this.motionSelectZAxisEnabled.Name = "motionSelectZAxisEnabled";
            this.motionSelectZAxisEnabled.Size = new System.Drawing.Size(140, 22);
            this.motionSelectZAxisEnabled.Text = "Enabled";
            // 
            // motionSelectZAxisPassthrough
            // 
            this.motionSelectZAxisPassthrough.Name = "motionSelectZAxisPassthrough";
            this.motionSelectZAxisPassthrough.Size = new System.Drawing.Size(140, 22);
            this.motionSelectZAxisPassthrough.Text = "Passthrough";
            // 
            // motionSelectZAxisConfig
            // 
            this.motionSelectZAxisConfig.Name = "motionSelectZAxisConfig";
            this.motionSelectZAxisConfig.Size = new System.Drawing.Size(140, 22);
            this.motionSelectZAxisConfig.Text = "Configure";
            // 
            // compoundSelectMenu
            // 
            this.compoundSelectMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.compoundSelectName,
            this.toolStripSeparator3,
            this.toolStripMenuItem6,
            this.toolStripMenuItem7,
            this.toolStripMenuItem8});
            this.compoundSelectMenu.Name = "contextMenuStrip1";
            this.compoundSelectMenu.Size = new System.Drawing.Size(135, 98);
            // 
            // compoundSelectName
            // 
            this.compoundSelectName.Enabled = false;
            this.compoundSelectName.Name = "compoundSelectName";
            this.compoundSelectName.Size = new System.Drawing.Size(134, 22);
            this.compoundSelectName.Text = "Stick Name";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(131, 6);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.compoundSelectButtonEnabled,
            this.compoundSelectButtonPassthrough,
            this.compoundSelectButtonConfig});
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(134, 22);
            this.toolStripMenuItem6.Text = "Button";
            // 
            // compoundSelectButtonEnabled
            // 
            this.compoundSelectButtonEnabled.CheckOnClick = true;
            this.compoundSelectButtonEnabled.Name = "compoundSelectButtonEnabled";
            this.compoundSelectButtonEnabled.Size = new System.Drawing.Size(140, 22);
            this.compoundSelectButtonEnabled.Text = "Enabled";
            // 
            // compoundSelectButtonPassthrough
            // 
            this.compoundSelectButtonPassthrough.CheckOnClick = true;
            this.compoundSelectButtonPassthrough.Name = "compoundSelectButtonPassthrough";
            this.compoundSelectButtonPassthrough.Size = new System.Drawing.Size(140, 22);
            this.compoundSelectButtonPassthrough.Text = "Passthrough";
            // 
            // compoundSelectButtonConfig
            // 
            this.compoundSelectButtonConfig.Name = "compoundSelectButtonConfig";
            this.compoundSelectButtonConfig.Size = new System.Drawing.Size(140, 22);
            this.compoundSelectButtonConfig.Text = "Configure";
            this.compoundSelectButtonConfig.Click += new System.EventHandler(this.buttonSelectConfig_Click);
            // 
            // toolStripMenuItem7
            // 
            this.toolStripMenuItem7.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.compoundSelectXAxisEnabled,
            this.compoundSelectXAxisPassthrough,
            this.compoundSelectXAxisConfig});
            this.toolStripMenuItem7.Name = "toolStripMenuItem7";
            this.toolStripMenuItem7.Size = new System.Drawing.Size(134, 22);
            this.toolStripMenuItem7.Text = "X Axis";
            // 
            // compoundSelectXAxisEnabled
            // 
            this.compoundSelectXAxisEnabled.CheckOnClick = true;
            this.compoundSelectXAxisEnabled.Name = "compoundSelectXAxisEnabled";
            this.compoundSelectXAxisEnabled.Size = new System.Drawing.Size(140, 22);
            this.compoundSelectXAxisEnabled.Text = "Enabled";
            // 
            // compoundSelectXAxisPassthrough
            // 
            this.compoundSelectXAxisPassthrough.CheckOnClick = true;
            this.compoundSelectXAxisPassthrough.Name = "compoundSelectXAxisPassthrough";
            this.compoundSelectXAxisPassthrough.Size = new System.Drawing.Size(140, 22);
            this.compoundSelectXAxisPassthrough.Text = "Passthrough";
            // 
            // compoundSelectXAxisConfig
            // 
            this.compoundSelectXAxisConfig.Name = "compoundSelectXAxisConfig";
            this.compoundSelectXAxisConfig.Size = new System.Drawing.Size(140, 22);
            this.compoundSelectXAxisConfig.Text = "Configure";
            // 
            // toolStripMenuItem8
            // 
            this.toolStripMenuItem8.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.compoundSelectYAxisEnabled,
            this.compoundSelectYAxisPassthrough,
            this.compoundSelectYAxisConfig});
            this.toolStripMenuItem8.Name = "toolStripMenuItem8";
            this.toolStripMenuItem8.Size = new System.Drawing.Size(134, 22);
            this.toolStripMenuItem8.Text = "Y Axis";
            // 
            // compoundSelectYAxisEnabled
            // 
            this.compoundSelectYAxisEnabled.CheckOnClick = true;
            this.compoundSelectYAxisEnabled.Name = "compoundSelectYAxisEnabled";
            this.compoundSelectYAxisEnabled.Size = new System.Drawing.Size(140, 22);
            this.compoundSelectYAxisEnabled.Text = "Enabled";
            // 
            // compoundSelectYAxisPassthrough
            // 
            this.compoundSelectYAxisPassthrough.CheckOnClick = true;
            this.compoundSelectYAxisPassthrough.Name = "compoundSelectYAxisPassthrough";
            this.compoundSelectYAxisPassthrough.Size = new System.Drawing.Size(140, 22);
            this.compoundSelectYAxisPassthrough.Text = "Passthrough";
            // 
            // compoundSelectYAxisConfig
            // 
            this.compoundSelectYAxisConfig.Name = "compoundSelectYAxisConfig";
            this.compoundSelectYAxisConfig.Size = new System.Drawing.Size(140, 22);
            this.compoundSelectYAxisConfig.Text = "Configure";
            // 
            // controllerPanel
            // 
            this.controllerPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.controllerPanel.Location = new System.Drawing.Point(0, 124);
            this.controllerPanel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.controllerPanel.Name = "controllerPanel";
            this.controllerPanel.Size = new System.Drawing.Size(933, 430);
            this.controllerPanel.TabIndex = 18;
            this.controllerPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.controllerPanel_Paint);
            this.controllerPanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.controllerPanel_Click);
            // 
            // controllerToolStripMenuItem
            // 
            this.controllerToolStripMenuItem.Name = "controllerToolStripMenuItem";
            this.controllerToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.controllerToolStripMenuItem.Text = "Controller";
            this.controllerToolStripMenuItem.Click += new System.EventHandler(this.controllerOptionsToolStripMenuItem_Click);
            // 
            // twitchToolStripMenuItem
            // 
            this.twitchToolStripMenuItem.Name = "twitchToolStripMenuItem";
            this.twitchToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.twitchToolStripMenuItem.Text = "Twitch";
            this.twitchToolStripMenuItem.Click += new System.EventHandler(this.twitchOptionsToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(933, 554);
            this.Controls.Add(this.controllerPanel);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Form1";
            this.Text = "Twitch Plays";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.buttonSelectMenu.ResumeLayout(false);
            this.motionSelectMenu.ResumeLayout(false);
            this.compoundSelectMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadConfigToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resetConfigToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveConfigToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox channelTextbox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox controllerSelectList;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.ContextMenuStrip buttonSelectMenu;
        private System.Windows.Forms.ToolStripMenuItem buttonSelectEnabled;
        private System.Windows.Forms.ToolStripMenuItem buttonSelectPassthrough;
        private System.Windows.Forms.ToolStripMenuItem buttonSelectConfig;
        private ToolStripMenuItem buttonSelectName;
        private ContextMenuStrip motionSelectMenu;
        private ToolStripMenuItem axisSelectName;
        private ToolStripMenuItem xAxisToolStripMenuItem;
        private ContextMenuStrip compoundSelectMenu;
        private ToolStripMenuItem compoundSelectName;
        private ToolStripMenuItem toolStripMenuItem6;
        private ToolStripMenuItem compoundSelectButtonEnabled;
        private ToolStripMenuItem compoundSelectButtonPassthrough;
        private ToolStripMenuItem toolStripMenuItem7;
        private ToolStripMenuItem compoundSelectXAxisEnabled;
        private ToolStripMenuItem compoundSelectXAxisPassthrough;
        private ToolStripMenuItem toolStripMenuItem8;
        private ToolStripMenuItem compoundSelectYAxisEnabled;
        private ToolStripMenuItem compoundSelectYAxisPassthrough;
        private ToolStripMenuItem compoundSelectButtonConfig;
        private ToolStripMenuItem compoundSelectXAxisConfig;
        private ToolStripMenuItem compoundSelectYAxisConfig;
        private ToolStripMenuItem yAxisToolStripMenuItem;
        private ToolStripMenuItem zAxisToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripMenuItem motionSelectXAxisEnabled;
        private ToolStripMenuItem motionSelectXAxisPassthrough;
        private ToolStripMenuItem motionSelectXAxisConfig;
        private ToolStripMenuItem motionSelectYAxisEnabled;
        private ToolStripMenuItem motionSelectYAxisPassthrough;
        private ToolStripMenuItem motionSelectYAxisConfig;
        private ToolStripMenuItem motionSelectZAxisEnabled;
        private ToolStripMenuItem motionSelectZAxisPassthrough;
        private ToolStripMenuItem motionSelectZAxisConfig;
        internal BufferedPanel controllerPanel;
        public Button connectBtn;
        private ToolStripMenuItem controllerToolStripMenuItem;
        private ToolStripMenuItem twitchToolStripMenuItem;
    }
}

