using System;
using System.Collections.Generic;
using System.Text;


public abstract class Trainer
{
    protected string name;
    protected List<Pokemon> pokemons;

    public Trainer(string name, string path) //TODO read folder name as trainer name
    {
        this.name = name;
        pokemons = new List<Pokemon>();
        for (int i = 0; i < 3; i++) //TODO  change condition for quantity of files in folder
        {
            Pokemon P = new Pokemon();
            P = PokemonReader.ReadPokemonFromFile(P, path + "/pokemon" + (i + 1) + ".pkm");
            pokemons.Add(P);
        }
    }

    public bool CheckTeamAlive(Trainer trainer)
    {
        bool teamAlive = true;
        List<bool> teamCheck = new List<bool>();
        int countFaintedPkm = 0;

        for (int i = 0; i < trainer.pokemons.Count; i++)
        {
            teamCheck.Add(pokemons[i].CheckPokemonAlive(pokemons[i].GetCurrentLife()));
        }

        for (int i = 0; i < teamCheck.Count; i++)
        {
            if (teamCheck[i] == false)
            {
                countFaintedPkm++;
            }
        }

        if (countFaintedPkm == teamCheck.Count)
        {
            teamAlive = false;
        }
        return teamAlive;
    }

    public void ChangeActivePokemon(int input)
    {
        Pokemon aux = new Pokemon();

        aux = pokemons[0];
        pokemons[0] = pokemons[input];
        pokemons[input] = aux;
    }

    public Player PlayerPkmFainted(Player player) //ver si lo paso a player
    {
        bool teamAlive = player.CheckTeamAlive(player);
        int pokemonChoice = 0;
        if (teamAlive == true)
        {

            do
            {
                Console.WriteLine("Active Pokemon fainted.");
                pokemonChoice = player.PokemonChoice(player.GetPokemonTeam());
                try
                {
                    player.ChangeActivePokemon(pokemonChoice);
                }
                catch (Exception)
                {
                    Console.WriteLine("That # of pokemons does not exist. Try again.");
                }
            } while (player.GetActivePokemon().CheckPokemonAlive(player.GetActivePokemon().GetCurrentLife()) == false);
        }
        return player;
    }

    public Pokemon GetActivePokemon()
    {
        return pokemons[0];
    }

    public string GetName()
    {
        return name;
    }

    public List<Pokemon> GetPokemonTeam()
    {
        return pokemons;
    }
}
