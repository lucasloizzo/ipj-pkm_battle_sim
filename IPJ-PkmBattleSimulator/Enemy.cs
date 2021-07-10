using System;
using System.Collections.Generic;
using System.Text;

public sealed class Enemy : Trainer
{
    public Enemy(string name, string path) : base(name, path)
    {

    }

    public Enemy EnemyPkmFainted(Enemy enemy) //ver si lo paso a enemy
    {
        bool teamAlive = enemy.CheckTeamAlive(enemy);
        bool pkmAlive;
        if (teamAlive == true)
        {
            int num = 1;
            do
            {
                enemy.ChangeActivePokemon(num);
                pkmAlive = enemy.GetActivePokemon().CheckPokemonAlive(enemy.GetActivePokemon().GetCurrentLife());
                num++;
            } while (num <= enemy.GetPokemonTeam().Count && pkmAlive == false);
        }

        return enemy;
    }
}
