using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

public static class PokemonReader
{
    public static Pokemon ReadPokemonFromFile(Pokemon P, string path)
    {
        string[] lines;

        try
        {
            lines = File.ReadAllLines(path);
        }
        catch (Exception)
        {

            throw;
        }

        P.LoadPokemon(P, lines);
        return P;
    }
}

