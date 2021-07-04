using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static void Main()
    {
        bool gameOpen = true;
        try
        {
            Game game = Game.GetInstance();
            do
            {
                gameOpen = game.Start();
            } while (gameOpen);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error: " + e.Message);
            throw;
        }
    }
}

