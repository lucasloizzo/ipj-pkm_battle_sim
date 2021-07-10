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

    public Movement MovementChoice(Pokemon pokemon)
    {
        ShowMovements(pokemon);
        int input = 0;
        Console.WriteLine("Choose # of move: ");
        input = Convert.ToInt32(Console.ReadLine());
        try
        {
            return pokemon.GetMovement(input);
        }
        catch (System.ArgumentOutOfRangeException)
        {
            throw;
        }
    }

    public void ShowMovements(Pokemon pokemon)
    {
        List<Movement> moveList = pokemon.GetMovements();
        for (int i = 0; i < moveList.Count; i++)
        {
            Console.WriteLine("#" + i + " " + moveList[i].name);
        }
    }

    public int PokemonChoice(List<Pokemon> pokemons)
    {
        ShowTeam(pokemons);
        int input = 0;

        Console.WriteLine("Choose # of Pkm to switch: ");
        input = Convert.ToInt32(Console.ReadLine());

        return input;
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
