namespace ST_ASCII;

/// <summary>
/// For use with <see cref="AsciiEngine"/>.
/// Defines behavior for each frame update.
/// Example code shows how you can instantiate initial GameState,
/// <example>
///     AsciiEngine engine = new AsciiEngine(80, 25);
///     engine.Start(new [GameStateChild](null, engine.Ascii));
/// </example>
/// where [GameStateChild] implements GameState.
/// </summary>
public abstract class GameState
{
    public GameState NextState { get; private set; }
    
    protected Ascii Ascii { get; }
    protected object Metadata { get; }

    protected GameState(object metadata, Ascii ascii)
    {
        Ascii = ascii;
        Metadata = metadata;
        NextState = null;
    }
    public abstract void Update(float deltatime);
    public abstract void Display();

    protected void SwitchState(GameState newState)
    {
        NextState = newState;
    }
}