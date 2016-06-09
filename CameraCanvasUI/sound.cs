using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;    // for PInvoke
using Microsoft.Win32;    // RegistryKey

namespace CameraCanvas
{
    public class sound
    //class sound
    {
        [DllImport("WinMM.dll")]
        public static extern bool PlaySound(string fname, int Mod, int flag);

        public void Play(string fname)
        {
            PlaySound(fname, 0, 0x00020000 | 0x0001);
        }

    }
}
