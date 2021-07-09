using System;
using System.Collections.Generic;
using System.Text;

public sealed class Player : Trainer
{
    public Player(string name, string path) : base(name, path)
    {

    }

    public BattleChoice Choice()
    {
        //hacer enum global para opcion de switch
        Console.WriteLine("1- Fight     2- Switch");
        int input = 0;
        input = Convert.ToInt32(Console.ReadLine());
        BattleChoice battleChoice;
        if (input >= 1 && input < (int)BattleChoice.Error)
        {
            battleChoice = (BattleChoice)input;
        }
        else
        {
            battleChoice = BattleChoice.Error;
        }

        return battleChoice;
    }

    public void MovementChoice()
    {
        //show movements
    }

    public void PokemonChoice(List<Pokemon> pokemons)
    {
        ShowTeam(pokemons);
        int input = 0;
        Pokemon aux = new Pokemon();

        Console.WriteLine("Choose # of Pkm to switch: ");
        input = Convert.ToInt32(Console.ReadLine());
        
        aux = pokemons[0];
        pokemons[0] = pokemons[input];
        pokemons[input] = aux;
    }

    public void ShowTeam(List<Pokemon> pokemons)
    {
        for (int i = 1; i < pokemons.Count; i++)
        {
            Console.WriteLine("# - " + i);
            Console.WriteLine(pokemons[i].GetPkmData());
        }
    }
}
