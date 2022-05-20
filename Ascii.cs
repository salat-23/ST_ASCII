
using ST_ASCII.Utility;

namespace ST_ASCII
{
    
    /// <summary>
    /// Used to setup and use console screen.
    /// Has many drawing methods and built-in audio player.
    /// Use this class instead of <see cref="FastConsole"/>.
    /// </summary>
    public class Ascii
    {
        public int Width { get; }
        public int Height { get; }

        private HashSet<ConsoleKey> holdedKeys;

        public Ascii(int width, int height)
        {
            Height = height;
            Width = width;
            holdedKeys = new HashSet<ConsoleKey>();
            FastConsole.Init(width, height);
            Clear();
        }

        public bool IsPressed(ConsoleKey key)
        {
            return FastConsole.IsPressed(key);
        }
        
        public bool IsJustPressed(ConsoleKey key)
        {
            bool isPressed = FastConsole.IsPressed(key);
            
            if (!isPressed)
            {
                holdedKeys.Remove(key);
            }
            
            if (holdedKeys.Contains(key)) return false;
            
            if (isPressed)
            {
                holdedKeys.Add(key);
            }

            return isPressed;
        }

        public bool IsConsolePresser(ConsoleKey key)
        {
            return FastConsole.IsConsolePressed(key);
        }

        public void Clear()
        {
            FastConsole.Clear();
        }
        
        public void Draw(Vector2Int position, char character, ConsoleColor foreground = ConsoleColor.White,
            ConsoleColor background = ConsoleColor.Black)
        {
            Draw(position.x, position.y, character, foreground, background);
        }
        
        public void Draw(Vector2Int position, string text, ConsoleColor foreground = ConsoleColor.White,
            ConsoleColor background = ConsoleColor.Black)
        {
            Draw(position.x, position.y, text, foreground, background);
        }
        
        public void Draw(Vector2Int position, Drawable drawable, ConsoleColor foreground = ConsoleColor.White,
            ConsoleColor background = ConsoleColor.Black)
        {
            Draw(position.x, position.y, drawable, foreground, background);
        }

        public void Draw(int x, int y, string text, ConsoleColor foreground = ConsoleColor.White,
            ConsoleColor background = ConsoleColor.Black)
        {
            if (y > Height - 1 || y < 0) return;
            foreach (var character in text)
            {
                if (!(x > Width - 1 || x < 0))
                    FastConsole.Draw(x, y, character, foreground, background);
                x++;
            }
        }

        public void Draw(int x, int y, char text, ConsoleColor foreground = ConsoleColor.White,
            ConsoleColor background = ConsoleColor.Black)
        {
            if (y > Height - 1 || y < 0) return;
            if (!(x > Width - 1 || x < 0))
                FastConsole.Draw(x, y, text, foreground, background);
        }

        public void Draw(int x, int y, Drawable drawable, ConsoleColor foreground = ConsoleColor.White,
            ConsoleColor background = ConsoleColor.Black)
        {
            drawable.DrawSelf(this, x, y, foreground, background);
        }

        public void Play(string sound)
        {
            FastConsole.Play(sound);
        }

        public void Refresh()
        {
            FastConsole.Render();
        }
    } 
}