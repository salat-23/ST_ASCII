using System;

namespace ST_ASCII;

/// <summary>
/// Abstraction above <see cref="Ascii"/> class.
/// This is an engine with its own gameloop.
/// Screen is cleared each frame.
/// Fps is capped at 60FPS.
/// Use <see cref="GameState"/> class to implement in-game logic.
/// </summary>
public class AsciiEngine
{
    const float LowLimit = 0.0167f;          // Keep below 60fps
    const float HighLimit = 0.1f;            // Keep above 10fps

    private GameState state;
    public Ascii Ascii { get; }

    public AsciiEngine(int width, int height)
    {
        Ascii = new Ascii(width, height);
    }
    public void Start(GameState initialState)
    {
        state = initialState;
        long lastTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
        while (true)
        {
            long currentTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            float deltaTime = (currentTime - lastTime) / 1000f;
            if ( deltaTime < LowLimit )
                deltaTime = LowLimit;
            else if ( deltaTime > HighLimit )
                deltaTime = HighLimit;
            Ascii.Clear();
            state.Update(deltaTime);
            state.Display();
            Ascii.Refresh();
            if (state.NextState != null)
            {
                state = state.NextState;
            }
            lastTime = currentTime;
        }
    }
}