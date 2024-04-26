using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace com.CaptainHook
{
    public enum MouseMessage
    {
        MouseMove = 0x200,
        LButtonDown = 0x201,
        LButtonUp = 0x202,
        RButtonDown = 0x204,
        RButtonUp = 0x205,
        MouseWheel = 0x20a,
        MouseHWheel = 0x20e
    }
    [StructLayout(LayoutKind.Sequential)]
    internal struct MouseLowLevelHookStruct
    {
        public Point pt;
        public int mouseData;
        public int flags;
        public int time;
        public IntPtr dwExtraInfo;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct Point
    {
        public int x;
        public int y;
    }
}
