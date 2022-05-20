using System.Numerics;
using ST_ASCII;
using ST_ASCII.Utility;

namespace Example2DField;

public class GameField : GameState
{

    private List<Vector2> obstacles;
    private List<int> entitiesToDelete;
    private Random random;
    private Vector2 playerPos;
    private float speed = 0.6f;
    private float verticalSpeed = 0f;
    
    public GameField(object metadata, Ascii ascii) : base(metadata, ascii)
    {
        playerPos = new Vector2(10, 0);
        random = new Random();
        entitiesToDelete = new List<int>();
        obstacles = new List<Vector2>();
    }

    public override void Update(float deltatime)
    {
        if (Ascii.IsPressed(ConsoleKey.Spacebar) && playerPos.Y == 0)
            verticalSpeed = 1f;
        verticalSpeed -= 0.07f * deltatime;
        playerPos.Y += verticalSpeed * deltatime;
        if (playerPos.Y < 0)
        {
            playerPos.Y = 0;
            verticalSpeed = 0;
        }

        for (int i = 0; i < obstacles.Count; i++)
        {
            if (obstacles[i].X < 0) entitiesToDelete.Add(i);
        }
        for (int i = entitiesToDelete.Count - 1; i >= 0; i--)
        {
            obstacles.RemoveAt(entitiesToDelete[i]);
        }
        entitiesToDelete.Clear();
        for (int i = 0; i < obstacles.Count; i++)
        {
            var vector = obstacles[i];
            vector.X -= speed * deltatime;
            obstacles[i] = vector;
            if ((int)obstacles[i].X == (int)playerPos.X && playerPos.Y <= 1) SwitchState(new MainMenu(null, Ascii));
        }
        while (obstacles.Count < 4)
        {
            obstacles.Add(new Vector2Int(random.Next(120, 160), 0));
        }

    }

    public override void Display()
    {
        Ascii.Draw(5, 5, "You are playing...");
        for (int i = 0; i < 80; i++)
            Ascii.Draw(i, 25 - 1 - 2, '#');
        foreach (var obstacle in obstacles)
        {
            Ascii.Draw((int)obstacle.X, 25 - 1 - 3, '|', ConsoleColor.Red);
        }
        Ascii.Draw((int)playerPos.X, 25 - 1 - 3 - (int)playerPos.Y, '4', ConsoleColor.Blue);
    }
}