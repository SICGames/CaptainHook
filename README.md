## CaptainHook
CaptainHook is a Mouse & Keyboard Hooking .NET Framework library. 

# Requirements 
.NET Framework 4.8+

# Usage
``` C#
//-- global declaration
private CaptainHook CaptainHook;

//--
private void MainWindow_Loaded()
{
  CaptainHook = new CaptainHook();
  CaptainHook.onKeyboardMessage += CaptainHook_onKeyboardMessage;
  CaptainHook.onInstalled += CaptainHook_onInstalled;
  CaptainHook.Install();
}

private void CaptainHook_onKeyboardMessage(object sender, KeyboardHookMessageEventArgs e)
{
  int vkey = e.VirtKeyCode;
  var key = KeyInterop.KeyFromVirtualKey(vkey); //-- if using inside WPF application
  bool keyDown = e.MessageType == KeyboardMessage.KeyDown ? true : false;
  if(keyDown)
  {
    //--- do whatever you want
  }
}

private void MainWindow_Closing()
{
   if(CaptainHook != null)
   {
      CaptainHook.Uninstall();
   }
}
```
