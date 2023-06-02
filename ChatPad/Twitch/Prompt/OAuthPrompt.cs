using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using ChatPad.Configuration;

namespace ChatPad.Twitch.Prompt
{
    public partial class OAuthPrompt : Form
    {
        public OAuthPrompt()
        {
            InitializeComponent();
        }

        private void OAuthPrompt_Load(object sender, EventArgs e)
        {
            userbox.Text = Config.username;
            oauthBox.Text = Config.oauth;
        }

        private void save_Click(object sender, EventArgs e)
        {
            Config.username = userbox.Text;
            Config.oauth = oauthBox.Text;
            Config.Save();
            
            Close();
        }
    }
}
