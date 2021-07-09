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
        Trainer winner; 
        Console.WriteLine("Playing");
        winner = Battle(player, enemy);
    }

    public Trainer Battle(Player player, Enemy enemy)
    {
        bool battling = true;
        do
        {
            //show state
            ShowGameState(player);
            ShowGameState(enemy);
            //player choice
            //player.Choice();
            //enemy decision
            //check turn order
            //do actions chosen (depends on order)
            //check battle end condition (both teams fainted)
            battling = CheckTrainersTeams(player, enemy);
        } while (battling);

        Trainer winner = CheckWinner(player, enemy);

        return winner;
    }

    public Trainer CheckWinner(Player player, Enemy enemy)
    {
        bool checkWin;
        checkWin = enemy.CheckTeamAlive(enemy);
        if (checkWin == false)
        {
            return player;
        }

        checkWin = player.CheckTeamAlive(player);
        if (checkWin == false)
        {
            return enemy;
        }

        return null;
    }

    public bool CheckTrainersTeams(Player player, Enemy enemy)
    {
        bool checkPlayerTeam, checkEnemyTeam;
        checkEnemyTeam = enemy.CheckTeamAlive(enemy);
        checkPlayerTeam = player.CheckTeamAlive(player);
        if (checkPlayerTeam == false && checkEnemyTeam == false)
        {
            return false;
        }
        return true;    
    }

    public void ShowGameState(Trainer trainer)
    {
        Console.WriteLine(trainer.GetName());
        Console.WriteLine(trainer.GetActivePokemon().GetStatus());
    }
}

