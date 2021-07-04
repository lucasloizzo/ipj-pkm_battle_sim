using System;
using System.Collections.Generic;
using System.Text;


class Game
{
    private static Game instance;
    private Gameplay gameplay;

    private Game()
    {
        gameplay = new Gameplay();
    }

    public static Game GetInstance()
    {
        if (instance == null)
        {
            instance = new Game();
        }
        return instance;
    }

    public bool Start()
    {
        //load trainers
        gameplay.Play();
        Console.WriteLine("Play again? (y/n)");
        string answer = Console.ReadLine();

        switch (answer)
        {
            case "y":
                return true;
            case "n":
                return false;
            default:
                break;
        }
        return false;
    }
}
