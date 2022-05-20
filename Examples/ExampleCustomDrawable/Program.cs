using ExampleCustomDrawable;
using ST_ASCII;

static class Program
{
    public static void Main(string[] args)
    {
        Ascii ascii = new Ascii(80, 25);
        Cross cross = new Cross();

        ascii.Draw(1, 1, cross);

        ascii.Refresh();
    }
}