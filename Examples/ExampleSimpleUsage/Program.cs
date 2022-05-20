using ST_ASCII;
using ST_ASCII.Geometry;

namespace ExampleSimpleUsage;

public static class Program
{
    public static void Main(string[] args)
    {
        //Create new instance with defined width and height
        Ascii ascii = new Ascii(80, 25);

        //Rectangle class demonstration
        //To understand how charset argument works here just play with it a bit yourself. By default its value is - "#"
        Rectangle rectangle = new Rectangle(10, 5, "friend");
        //Infinite loop
        for (;;)
        {
            ascii.Clear(); // Clear screen
            
            //Draw "Hello world!" string at x=5 y=5 with green color and dark gray background
            ascii.Draw(5, 5, "Hello world!", ConsoleColor.Green, ConsoleColor.DarkGray);
            
            //Draw our rectangle at x=0 y=0 red color and dark red background
            ascii.Draw(0, 0, rectangle, ConsoleColor.Red, ConsoleColor.DarkRed);
            
            //!!!!!!!!!!!!!!!!!
            //This is important function. It is used to transfer your virtual console screen to your real one.
            //I recommend calling this only 1 time at the end of your loop
            //Without calling it, you wont see anything on your screen 
            ascii.Refresh();
            
        }
    }
}