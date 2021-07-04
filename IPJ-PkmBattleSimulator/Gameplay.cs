using System;
using System.Collections.Generic;
using System.Text;
using System.IO;


public class Gameplay
{
    private Player player;
    private Enemy enemy;

    public Gameplay()
    {
        player = new Player("Red", "../../../resources/Red");
        enemy = new Enemy("Blue", "../../../resources/Blue");
    }

    public void Play()
    {
        Console.WriteLine("Playing");
    }
}

