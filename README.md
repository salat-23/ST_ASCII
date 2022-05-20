# ST_ASCII - Multi-purpose console ascii engine

Have you ever tried to create a game using only ascii or 
unicode characters but never found a good way to do this?

This engine is created for this concrete reason. Though it is **Windows** exclusive, 
it supports many features like:
- Drawing by coordinates
- Basic geometry shapes
- Ability to implement custom rendering method for your custom drawables
- Playing audio
- Color support
- Game engine with its gameloop
- Gui elements **(Coming soon!)**
- Simple and very beginner friendly structure

Below is the example how you can start using this engine **(Look in folder Examples)**:

    public static void Main(string[] args)
    {
    
        Ascii ascii = new Ascii(80, 25);
        Rectangle rectangle = new Rectangle(10, 5, "friend"); 

        for (;;) {
            ascii.Clear(); // Clear screen
            ascii.Draw(5, 5, "Hello world!", ConsoleColor.Green, ConsoleColor.DarkGray);
            ascii.Draw(0, 0, rectangle, ConsoleColor.Red, ConsoleColor.DarkRed);
            ascii.Refresh();
        }   
    }

This engine is still work in progress, so I will constantly add more documentation and features. I will accept pull requests as well as new ideas. Hope you enjoy my little work here)

# Warning! Console window is not resizable, because console buffer starts behaving very strange when you resize it.
# Although you can use windows aero function (drag window on top of the screen and hold) to break this rule and see what happens =) 
# If you want to make window bigger, just hold Ctrl and use your mouse wheel to scale console window.