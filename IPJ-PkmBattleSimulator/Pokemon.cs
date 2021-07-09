using System;
using System.Collections.Generic;
using System.Text;
using System.IO;


public class Pokemon
{
    private string name;
    private int level;
    private string type;
    //private List<Type> types;
    private int maxLife;
    private int currentLife;
    private int attack;
    private int specialAttack;
    private int defense;
    private int specialDefense;
    private int speed;
    //private List<Movement> movements;
    private int precision;
    private int evasion;

    public Pokemon()
    {

    }

    public Pokemon LoadPokemon(Pokemon P, string[] lines)
    {
        this.name = lines[0];
        this.level = Convert.ToInt32(lines[1]);
        this.type = lines[2];
        this.maxLife = Convert.ToInt32(lines[3]);
        this.currentLife = maxLife;
        this.attack = Convert.ToInt32(lines[4]);
        this.specialAttack = Convert.ToInt32(lines[5]);
        this.defense = Convert.ToInt32(lines[6]);
        this.specialDefense = Convert.ToInt32(lines[7]);
        this.speed = Convert.ToInt32(lines[8]);
        this.precision = 100;
        this.evasion = 1;

        return this;
    }

    public bool CheckPokemonAlive(int currentLife)
    {
        bool isPkmAlive = true;

        if (currentLife <= 0)
        {
            isPkmAlive = false;
        }

        return isPkmAlive;
    }

    public int GetCurrentLife()
    {
        return currentLife;
    }

    public string GetStatus()
    {
        string status = "";

        status += name + "\n";
        status += "HP: " + currentLife + "/" + maxLife + "\n";

        return status;
    }

    public string GetPkmData()
    {
        string status = "";

        status += name + "\n";
        status += "HP: " + currentLife + "/" + maxLife + "\n";
        status += "Types: " + type + "\n";
        //status += GetMovementName();
        return status;
    }

    //public List<Movement> GetMovements()
    //{
    //    return movements;
    //}
}

