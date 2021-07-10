using System;
using System.Collections.Generic;
using System.Text;
using System.IO;


public class Pokemon
{
    private string name;
    private int level;
    private List<Type> types;
    private int maxLife;
    private int currentLife;
    private int attack;
    private int specialAttack;
    private int defense;
    private int specialDefense;
    private int speed;
    private List<Movement> movements;
    private int precision;
    private int evasion;

    public Pokemon()
    {
        types = new List<Type>();
        movements = new List<Movement>();
    }

    public Pokemon LoadPokemon(Pokemon P, string[] lines)
    {
        this.name = lines[0];
        this.level = Convert.ToInt32(lines[1]);
        string[] types = lines[2].Split(" ");
        for (int i = 0; i < types.Length; i++)
        {
            this.types.Add(Types.GetInstance().GetType(types[i]));
        }
        this.maxLife = Convert.ToInt32(lines[3]);
        this.currentLife = maxLife;
        this.attack = Convert.ToInt32(lines[4]);
        this.specialAttack = Convert.ToInt32(lines[5]);
        this.defense = Convert.ToInt32(lines[6]);
        this.specialDefense = Convert.ToInt32(lines[7]);
        this.speed = Convert.ToInt32(lines[8]);
        if (lines.Length >= 10)
        {
            this.movements.Add(MovementDatabase.GetMovement(lines[9]));
        }
        if (lines.Length >= 11)
        {
            this.movements.Add(MovementDatabase.GetMovement(lines[10]));
        }
        if (lines.Length >= 12)
        {
            this.movements.Add(MovementDatabase.GetMovement(lines[11]));
        }
        if (lines.Length >= 13)
        {
            this.movements.Add(MovementDatabase.GetMovement(lines[12]));
        }
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

    public string GetName()
    {
        return name;
    }

    public int GetLevel()
    {
        return level;
    }

    public int GetCurrentLife()
    {
        return currentLife;
    }

    public int GetAttack()
    {
        return attack;
    }
    
    public int GetSpecialAttack()
    {
        return specialAttack;
    }

    public int GetDefense()
    {
        return defense;
    }

    public int GetSpecialDefense()
    {
        return specialDefense;
    } 

    public int GetSpeed()
    {
        return speed;
    }

    public string GetTypes()
    {
        string types = "";
        for (int i = 0; i < this.types.Count; i++)
        {
            types += this.types[i] + " ";
        }
        return types;
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
        status += "Types: " + GetTypes() + "\n";
        //status += GetMovementName();
        return status;
    }

    public List<Movement> GetMovements()
    {
        return movements;
    }

    public Movement GetMovement(int choice)
    {
        return movements[choice];
    }

    public int TakeDamage(int damage)
    {
        currentLife -= damage;
        return currentLife;
    }
}

