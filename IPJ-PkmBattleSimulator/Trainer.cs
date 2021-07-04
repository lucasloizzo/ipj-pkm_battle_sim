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

    public void LoadTeamFromFolder(string name, string path)
    {

    }
}
