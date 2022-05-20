namespace ST_ASCII;

/// <summary>
/// Used to implement custom drawing behaviour for other classes.
/// </summary>
public interface Drawable
{
    public void DrawSelf(Ascii ascii, int x, int y, ConsoleColor foreground, ConsoleColor background);
}