using ST_ASCII;

namespace ExampleCustomDrawable;

public class Cross : IDrawable
{
    public void DrawSelf(Ascii ascii, int x, int y, ConsoleColor foreground, ConsoleColor background) {
        //We use ascii instance to draw

        ascii.Draw(x, y, '+', foreground, background);
        ascii.Draw(x, y + 1, '+', foreground, background);
        ascii.Draw(x, y - 1, '+', foreground, background);
        ascii.Draw(x + 1, y, '+', foreground, background);
        ascii.Draw(x - 1, y, '+', foreground, background);
    }
}