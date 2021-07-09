using System;
using System.Collections.Generic;
using System.Text;

public sealed class Player : Trainer
{
    public Player(string name, string path) : base(name, path)
    {

    }

    public void Choice()
    {
        Console.WriteLine("1- Fight     2- Switch");

    }
}
