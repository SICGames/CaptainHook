using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.CaptainHook
{
    public class MouseHookMessageEventArgs : EventArgs
    {
        public Point Position { get; private set; }
        public MouseMessage MessageType { get; private set; }

        public MouseHookMessageEventArgs(Point position, MouseMessage msg)
        {
            Position = position;
            MessageType = msg;
        }
    }
}
