using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatPad.Graphics
{
    internal class BufferedPanel : Panel
    {
        public BufferedPanel() 
        {
            DoubleBuffered = true;
        }
    }
}
