namespace ChatPad.Twitch.Prompt
{
    partial class OAuthPrompt
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
            this.userbox = new System.Windows.Forms.TextBox();
            this.oauthBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.save = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // userbox
            // 
            this.userbox.Location = new System.Drawing.Point(88, 6);
            this.userbox.MaxLength = 32;
            this.userbox.Name = "userbox";
            this.userbox.Size = new System.Drawing.Size(200, 22);
            this.userbox.TabIndex = 0;
            // 
            // oauthBox
            // 
            this.oauthBox.Location = new System.Drawing.Point(88, 33);
            this.oauthBox.MaxLength = 32;
            this.oauthBox.Name = "oauthBox";
            this.oauthBox.Size = new System.Drawing.Size(200, 22);
            this.oauthBox.TabIndex = 1;
            this.oauthBox.UseSystemPasswordChar = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Username";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "OAuth";
            // 
            // save
            // 
            this.save.Location = new System.Drawing.Point(213, 61);
            this.save.Name = "save";
            this.save.Size = new System.Drawing.Size(75, 23);
            this.save.TabIndex = 4;
            this.save.Text = "Save";
            this.save.UseVisualStyleBackColor = true;
            this.save.Click += new System.EventHandler(this.save_Click);
            // 
            // OAuthPrompt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(300, 92);
            this.Controls.Add(this.save);
            this.Controls.Add(this.oauthBox);
            this.Controls.Add(this.userbox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "OAuthPrompt";
            this.Text = "Twitch Options";
            this.Load += new System.EventHandler(this.OAuthPrompt_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox userbox;
        private System.Windows.Forms.TextBox oauthBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button save;
    }
}