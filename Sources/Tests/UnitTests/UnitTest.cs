using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;

namespace UnitTests
{
    [TestClass]
    public class UnitTest
    {
        // import winmm.dll
        [DllImport("winmm.dll")]
        private static extern bool PlaySound(string szSound, System.IntPtr hMod, PlaySoundFlags flags);

        [DllImport("kernel32.dll")]
        public static extern IntPtr LoadLibrary(string dllToLoad);
                
        [Flags]
        public enum PlaySoundFlags : int
        {
            SND_SYNC = 0x0000,  // play synchronously (default)
            SND_ASYNC = 0x0001,  // play asynchronously
            SND_FILENAME = 0x00020000, // name is file name
            SND_PURGE = 0x0040,  // purge non-static events
        }

        [TestMethod]
        public void TestPlaySound()
        {
            string binPath = Path.GetDirectoryName(GetType().Assembly.Location);
            string winmmPath = "";

            if(IntPtr.Size == 8)
            {
#if DEBUG
                winmmPath = Path.Combine(binPath, "../../../x64/Debug/winmm.dll");
#else
                winmmPath = Path.Combine(binPath, "../../../x64/Release/winmm.dll");
#endif
            }
            else
            {
#if DEBUG
                winmmPath = Path.Combine(binPath, "../../../Debug/winmm.dll");
#else
                winmmPath = Path.Combine(binPath, "../../../Release/winmm.dll");
#endif
            }

            if(!File.Exists(winmmPath))
            {
                throw new Exception($"{winmmPath} does not exists, please compile winmm project first!");
            }

            IntPtr module = LoadLibrary(winmmPath);

            if(module == IntPtr.Zero)
            {
                throw new Exception($"Failed load {winmmPath}!");
            }

            for(int i=1; i<=10; ++i)
            {
                string path = $"C:\\Windows\\Media\\Alarm{i:00}.wav";

                if (File.Exists(path))
                {
                    PlaySound(path, IntPtr.Zero, PlaySoundFlags.SND_FILENAME | PlaySoundFlags.SND_SYNC);

                    Console.WriteLine($"play:{path}");
                }
            }
        }
    }
}
