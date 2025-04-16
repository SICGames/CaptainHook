using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace com.CaptainHook
{
    public class CaptainHook : IDisposable
    {
        private bool disposedValue;

        private bool isHooksUninstalledAlready = false;

        [DllImport("user32.dll")]
        internal static extern IntPtr SetWindowsHookEx(HookType idHook, HookProc lpfn, IntPtr hMod, int dwThreadId);

        [DllImport("user32.dll")]
        internal static extern int UnhookWindowsHookEx(IntPtr hHook);

        [DllImport("user32.dll")]
        internal static extern IntPtr CallNextHookEx(IntPtr _, int nCode, UIntPtr wParam, IntPtr lParam);

        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        internal delegate IntPtr HookProc(int nCode, UIntPtr wParam, IntPtr lParam);

        private IntPtr MouseHook, KeyboardHook;
        private HookProc mouseHookProc, keyboardHookProc;

        public EventHandler<KeyboardHookMessageEventArgs> onKeyboardMessage;
        public EventHandler<MouseHookMessageEventArgs> onMouseHookMessage;
        public EventHandler<EventArgs> onInstalled;
        public EventHandler<EventArgs> onUninstalled;
        public EventHandler<EventArgs> onError;

        public CaptainHook() 
        {
            mouseHookProc = LowLevelMouseProc;
            keyboardHookProc = LowLevelKeyboardProc;
        }

        public void Install()
        {
            if (MouseHook == IntPtr.Zero)
                MouseHook = SetWindowsHookEx(HookType.LowLevelMouse, mouseHookProc, IntPtr.Zero, 0);
            if (KeyboardHook == IntPtr.Zero)
                KeyboardHook = SetWindowsHookEx(HookType.LowLevelKeyboard, keyboardHookProc, IntPtr.Zero, 0);

            if (MouseHook != IntPtr.Zero && KeyboardHook != IntPtr.Zero)
            {
                onInstalled?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                onError?.Invoke(this, EventArgs.Empty);
            }
        }
        public void Uninstall()
        {
            if (!isHooksUninstalledAlready)
            {
                UnhookWindowsHookEx(MouseHook);
                UnhookWindowsHookEx(KeyboardHook);
                MouseHook = KeyboardHook = IntPtr.Zero;
                onUninstalled?.Invoke(this, EventArgs.Empty);
                isHooksUninstalledAlready = true;
            }
        }

        private IntPtr LowLevelMouseProc(int nCode, UIntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                var st = Marshal.PtrToStructure<MouseLowLevelHookStruct>(lParam);

                onMouseHookMessage?.Invoke(this, new MouseHookMessageEventArgs(st.pt, (MouseMessage)wParam));
            }
            return CallNextHookEx(IntPtr.Zero, nCode, wParam, lParam);
        }

        private IntPtr LowLevelKeyboardProc(int nCode, UIntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                var st = Marshal.PtrToStructure<KeyboardLowLevelHookStruct>(lParam);
                onKeyboardMessage?.Invoke(this, new KeyboardHookMessageEventArgs(st.vkCode, (KeyboardMessage)wParam));
            }
            return CallNextHookEx(IntPtr.Zero, nCode, wParam, lParam);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                Uninstall();
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~CaptainHook()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

         ~CaptainHook() => Dispose(false);

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
