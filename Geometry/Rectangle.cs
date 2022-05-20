using System;

namespace ST_ASCII.Geometry;

/// <summary>
/// May be used to draw rectangular shapes in console.
/// </summary>
public class Rectangle : GeometricShape, IDrawable
{
    public int Width
    {
        get => width;
        set
        {
            if (value < 0) width = 0;
            width = value;
        }
    }
    public int Height
    {
        get => height;
        set
        {
            if (value < 0) height = 0;
            height = value;
        }
    }
    public string Charset { get; set; }
    
    private int width;
    private int height;

    public Rectangle(int width, int height, string charset = "#")
    {
        Width = width;
        Height = height;
        Charset = charset;
    }

    public void DrawSelf(Ascii ascii, int x, int y, ConsoleColor foreground, ConsoleColor background)
    {
        x = 0 + x;
        y = 0 + y;

        int charsetIndex = 0;
        
        //Top left to top right
        for (int i = x; i < x + width; i++)
        {
            ascii.Draw(i, y, Charset[charsetIndex], foreground, background);
            charsetIndex = GetNextCharsetIndex(Charset, charsetIndex);
        }
        
        //Top right to bottom right
        //charsetIndex = GetNextCharsetIndex(Charset, charsetIndex);
        for (int i = y + 1; i < y + height; i++)
        {
            ascii.Draw(x + width - 1, i, Charset[charsetIndex], foreground, background);
            charsetIndex = GetNextCharsetIndex(Charset, charsetIndex);
        }
        
        //Bottom right to bottom left
        //charsetIndex = GetNextCharsetIndex(Charset, charsetIndex);
        for (int i = x + width - 2; i >= x; i--)
        {
            ascii.Draw(i, y + height - 1, Charset[charsetIndex], foreground, background);
            charsetIndex = GetNextCharsetIndex(Charset, charsetIndex);
        }
        
        //Bottom left to top left 
        //charsetIndex = GetNextCharsetIndex(Charset, charsetIndex);
        for (int i = y + height - 2; i >= y + 1; i--)
        {
            ascii.Draw(x, i, Charset[charsetIndex], foreground, background);
            charsetIndex = GetNextCharsetIndex(Charset, charsetIndex);
        }
    }

    private int GetNextCharsetIndex(string charset, int currentIndex)
    {
        if (currentIndex < 0) return charset.Length - 1;
        if (currentIndex + 1 > charset.Length - 1) return 0;
        return currentIndex + 1;
    }
}