using System;
using System.IO;
using System.Media;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Win32.SafeHandles;
using ST_ASCII.Utility;

#pragma warning disable CA1416
namespace ST_ASCII
{
    /// <summary>
    /// Barebones static drawing class.
    /// Use <see cref="Ascii"/> instead of this class, to setup new console screen.
    /// </summary>
    static class FastConsole
    {
        
        private const int MF_BYCOMMAND = 0x00000000;
        private const int SC_CLOSE = 0xF060;
        private const int SC_MINIMIZE = 0xF020;
        private const int SC_MAXIMIZE = 0xF030;
        private const int SC_SIZE = 0xF000;
        
        
        
        
        private static SafeFileHandle handle;
        private static CharInfo[] buf;
        private static SmallRect rect;
        private static Coord coordRB;
        private static Coord coordLT;

        [DllImport("winmm.dll")]
        static extern Int32 mciSendString(string command, StringBuilder buffer, int bufferSize, IntPtr hwndCallback);
        
        [DllImport("Kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern SafeFileHandle CreateFile(
            string fileName,
            [MarshalAs(UnmanagedType.U4)] uint fileAccess,
            [MarshalAs(UnmanagedType.U4)] uint fileShare,
            IntPtr securityAttributes,
            [MarshalAs(UnmanagedType.U4)] FileMode creationDisposition,
            [MarshalAs(UnmanagedType.U4)] int flags,
            IntPtr template);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool WriteConsoleOutputW(
          SafeFileHandle hConsoleOutput,
          CharInfo[] lpBuffer,
          Coord dwBufferSize,
          Coord dwBufferCoord,
          ref SmallRect lpWriteRegion);
        
        [DllImport("user32.dll")]
        static extern int GetAsyncKeyState(int VKey);
        
        [DllImport("user32.dll")]
        public static extern int DeleteMenu(IntPtr hMenu, int nPosition, int wFlags);

        [DllImport("user32.dll")]
        private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr GetConsoleWindow();

        [StructLayout(LayoutKind.Sequential)]
        public struct Coord
        {
            public short X;
            public short Y;
            public Coord(short X, short Y)
            {
                this.X = X;
                this.Y = Y;
            }
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct CharUnion
        {
            [FieldOffset(0)] public ushort UnicodeChar;
            [FieldOffset(0)] public byte AsciiChar;
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct CharInfo
        {
            [FieldOffset(0)] public CharUnion Char;
            [FieldOffset(2)] public short Attributes;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SmallRect
        {
            public short Left;
            public short Top;
            public short Right;
            public short Bottom;
        }

        public static void Init(Vector2Int size)
        {
            Init(size.x, size.y);
        }

        public static void Init(int x, int y)
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.OutputEncoding = Encoding.Unicode;
            Console.SetWindowSize(x, y);
            Console.BufferHeight = y;
            Console.BufferWidth = x;
            Console.CursorVisible = false;
            IntPtr windowHandle = GetConsoleWindow();
            IntPtr sysMenu = GetSystemMenu(windowHandle, false);
            if (windowHandle != IntPtr.Zero)
            {
                //DeleteMenu(sysMenu, SC_CLOSE, MF_BYCOMMAND);
                //DeleteMenu(sysMenu, SC_MINIMIZE, MF_BYCOMMAND);
                DeleteMenu(sysMenu, SC_MAXIMIZE, MF_BYCOMMAND);
                DeleteMenu(sysMenu, SC_SIZE, MF_BYCOMMAND);
            }
            handle = CreateFile("CONOUT$", 0x40000000, 2, IntPtr.Zero, FileMode.Open, 0, IntPtr.Zero);
            if (handle.IsInvalid) Environment.Exit(-1);
            buf = new CharInfo[x * y];
            rect = new SmallRect() { Left = 0, Top = 0, Right = (short)x, Bottom = (short)y };
            coordRB = new Coord((short)x, (short)y);
            coordLT = new Coord(0, 0);
            
            for (int i = 0; i < buf.Length; ++i)
            {
                buf[i].Attributes = 0;
                buf[i].Char.UnicodeChar = ' ';
            }
        }
        
        
        public static void Play(string sound, int channel = 1)
        {
            Task.Run(() =>
            {
                string alias = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
                StringBuilder builder = new StringBuilder();
                mciSendString($"close {alias}", builder, 0, IntPtr.Zero);
                mciSendString($"open {sound} type waveaudio alias {alias}", builder, 0, IntPtr.Zero);
                mciSendString($"play {alias}", builder, 0, IntPtr.Zero);
            });
        }

        public static void Draw(Vector2Int position, char character, ConsoleColor foreground = ConsoleColor.White,
            ConsoleColor background = ConsoleColor.Black)
        {
            Draw(position.x, position.y, character, foreground, background);
        }
        
        public static void Draw(Vector2Int position, string text, ConsoleColor foreground = ConsoleColor.White,
            ConsoleColor background = ConsoleColor.Black)
        {
            Draw(position.x, position.y, text, foreground, background);
        }

        public static void Draw(int x, int y, char character, ConsoleColor foreground = ConsoleColor.White,
            ConsoleColor background = ConsoleColor.Black)
        {
            if (x < 0 || x > coordRB.X - 1 || y < 0 || y > coordRB.Y - 1) return;
            buf[x + y * coordRB.X].Attributes = (short)((short)foreground | (short)background << 4);
            buf[x + y * coordRB.X].Char.UnicodeChar = character;
        }

        public static void Draw(int x, int y, string text, ConsoleColor foreground = ConsoleColor.White,
            ConsoleColor background = ConsoleColor.Black)
        {
            int stringI = -1;
            for (int i = x; i < x + text.Length; i++)
            {
                stringI++;
                Draw(i, y, text[stringI], foreground, background);
            }
        }
        
        public static bool IsPressed(ConsoleKey key)
        {
            int keyStatus = GetAsyncKeyState((int)key);
            if (keyStatus > 0 && keyStatus != 32769)
            {
                int test = keyStatus;
            }
            return 0 != (keyStatus & 0x8000);
        }
        
        public static bool IsConsolePressed(ConsoleKey key)
        {
            int keyStatus = GetAsyncKeyState((int)key);
            return 0 != (keyStatus & 0x0001);
        }

        public static void Clear()
        {
            for (int i = 0; i < buf.Length; ++i)
            {
                buf[i].Attributes = 0;
                buf[i].Char.UnicodeChar = ' ';
            }
        }

        public static void Render()
        {
            WriteConsoleOutputW(handle, 
                buf,
                coordRB,
                coordLT,
                ref rect);
        }
    }
}