

using ST_ASCII;
using ST_ASCII.Geometry;

namespace Example2DField
{
    static class Program
    {
        public static void Main(string[] args)
        {
            AsciiEngine engine = new AsciiEngine(80, 25);
            engine.Start(new MainMenu(null, engine.Ascii));
        }
    }
}
