# How to process user input with ST_ASCII

Any good game should have at least 1 way to detect user input. ST_ASCII has **3** methods for this purpose!

 - IsPressed(ConsoleKey key)
 - IsJustPressed(ConsoleKey key)
 - IsConsolePressed(ConsoleKey key)

Let's see what each of these 3 methods does:

IsPressed(ConsoleKey key) returns true if the key is pressed at the moment, and false if not. (Hold)

IsJustPressed(ConsoleKey key) returns true once the key is pressed, afterwards it returns false until the key isn't pressed again (Tap)

IsConsolePressed(ConsoleKey key) is a tricky one so I suggest you to experiment with it for yourself!)

    bool isSpacebarPressed = ascii.IsPressed(ConsoleKey.Spacebar);
    bool isAPressed = ascii.IsJustPressed(ConsoleKey.A);
    bool isEnterPressed = ascii.IsConsolePressed(ConsoleKey.Enter);

If you are trying to implement some type of text input system in your game, I highly recommend using IsConsolePressed(ConsoleKey key) method, because it basically emulates console key behavior.