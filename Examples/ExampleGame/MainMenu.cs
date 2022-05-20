using System;
using ST_ASCII;
using ST_ASCII.Geometry;
using ST_ASCII.Gui;
using ST_ASCII.Utility;

namespace Example2DField;

public class MainMenu : GameState
{

    private Circle circle;
    private float fRadius;
    private bool isLowering;
    private Vector2Int maxMinRadius;
    private float pulseSpeed;

    public MainMenu(object metadata, Ascii ascii) : base(metadata, ascii)
    {
        pulseSpeed = 0.4f;
        maxMinRadius = new Vector2Int(20, 16);
        fRadius = 18;
        isLowering = true;
        circle = new Circle((int)fRadius, '#');
    }

    public override void Update(float deltatime)
    {
        if (Ascii.IsJustPressed(ConsoleKey.Spacebar))
        {
            SwitchState(new GameField(null, Ascii));
        }

        float addNum = deltatime * pulseSpeed;
        if (isLowering)
        {
            fRadius -= addNum;
            if ((int)fRadius < maxMinRadius.y) isLowering = false;
        }
        else
        {
            fRadius += addNum;
            if ((int)fRadius > maxMinRadius.x) isLowering = true;
        }

        circle.Radius = (int)fRadius;

    }

    public override void Display()
    {
        Ascii.Draw(Ascii.Width / 2, Ascii.Height / 2, circle);
        int initX = Ascii.Width / 2 - 12;
        Ascii.Draw(initX, Ascii.Height / 2, "Press ");
        initX += 6;
        Ascii.Draw(initX, Ascii.Height / 2, "spacebar", ConsoleColor.Red);
        initX += 8;
        Ascii.Draw(initX, Ascii.Height / 2, " to begin!");
    }
}