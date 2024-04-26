using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.CaptainHook
{
    public class KeyboardHookMessageEventArgs : EventArgs
    {
        public int VirtKeyCode { get; private set; }
        public KeyboardMessage MessageType { get; private set; }

        public KeyboardHookMessageEventArgs(int vkCode, KeyboardMessage msg)
        {
            VirtKeyCode = vkCode;
            MessageType = msg;
        }
    }
}
