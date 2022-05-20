namespace ST_ASCII.Geometry;

/// <summary>
/// May be used to draw circular shapes in console.
/// </summary>
public class Circle : GeometricShape, Drawable
{
    
    public int Radius { get; set; }
    public char Character {get; set; }

    public Circle(int radius, char character = '#')
    {
        Radius = radius;
        Character = character;
    }
    
    public void DrawSelf(Ascii ascii, int x, int y, ConsoleColor foreground, ConsoleColor background)
    {
        //https://www.geeksforgeeks.org/mid-point-circle-drawing-algorithm/?ref=lbp
        int x_sec = Radius, y_sec = 0;

        // When radius is zero only a single
        // point will be printed

        if (Radius >= 0)
        {
            ascii.Draw(x_sec + x, -y_sec + y, Character, foreground, background);
            ascii.Draw(y_sec + x, x_sec + y, Character, foreground, background);
            ascii.Draw(-y_sec + x, x_sec + y, Character, foreground, background);
            
            ascii.Draw(-x_sec + x, y_sec + y, Character, foreground, background);
            ascii.Draw(-y_sec + x, -x_sec + y, Character, foreground, background);
            ascii.Draw(y_sec + x, -x_sec + y, Character, foreground, background);
        }
     
        // Initialising the value of P
        int P = 1 - Radius;
        while (x_sec > y_sec)
        {
            y_sec++;
         
            // Mid-point is inside or on the perimeter
            if (P <= 0)
                P = P + 2*y_sec + 1;
            // Mid-point is outside the perimeter
            else
            {
                x_sec--;
                P = P + 2*y_sec - 2*x_sec + 1;
            }
         
            // All the perimeter points have already been printed
            if (x_sec < y_sec)
                break;
         
            // Printing the generated point and its reflection
            // in the other octants after translation
            ascii.Draw(x_sec + x, y_sec + y, Character, foreground, background);
            ascii.Draw(-x_sec + x, y_sec + y, Character, foreground, background);
            ascii.Draw(x_sec + x, -y_sec + y, Character, foreground, background);
            ascii.Draw(-x_sec + x, -y_sec + y, Character, foreground, background);

            // If the generated point is on the line x = y then
            // the perimeter points have already been printed
            if (x_sec != y_sec)
            {
                ascii.Draw(y_sec + x, x_sec + y, Character, foreground, background);
                ascii.Draw(-y_sec + x, x_sec + y, Character, foreground, background);
                ascii.Draw(y_sec + x, -x_sec + y, Character, foreground, background);
                ascii.Draw(-y_sec + x, -x_sec + y, Character, foreground, background);
            }
        }    
    }
    
}