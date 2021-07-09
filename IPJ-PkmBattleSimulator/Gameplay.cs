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
        BattleChoice battleChoice;
        SpeedCheck speedCheck;

        do
        {
            Console.Clear();
            //show state
            ShowGameState(player);
            ShowGameState(enemy);
            //player choice
            do
            {
                battleChoice = player.Choice();

                if (battleChoice == BattleChoice.Fight)
                {
                    player.MovementChoice();
                }
                else if (battleChoice == BattleChoice.Switch)
                {
                    try
                    {
                        player.PokemonChoice(player.GetPokemonTeam());
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("That # of pokemons does not exist. Try again.");
                        battleChoice = BattleChoice.Error;
                    }
                }
                else
                {
                    Console.WriteLine("This is not the time nor the place. Choose again.");
                }
            } while (battleChoice == BattleChoice.Error);
            //enemy decision

            //check turn order
            //if (battleChoice == BattleChoice.Switch)
            //{
            //    //player move first
            //    //enemy moves second
            //    //check if pkm of player faints
            //}
            //else if (enemyDecision == BattleChoice.Switch)
            //{
            //    //enemy move first
            //    //player moves second
            //    //check if pkm of enemy faints
            //}
            //else
            //{
            //    //check speed
            //    switch (speedCheck)
            //    {
            //        case SpeedCheck.PlayerFirst:
            //            //player pokemon use move
            //            //check if enemy pkm faints (if it does, select one to replace)
            //            //enemy pkm use move
            //            //check if player pkm faints (if it does, select one to replace)
            //            break;
            //        case SpeedCheck.EnemyFirst:
            //            //enemy pkm use move
            //            //check if player pkm faints (if it does, select one to replace)
            //            //player pokemon use move
            //            //check if enemy pkm faints (if it does, select one to replace)
            //            break;
            //        case SpeedCheck.Equal:
            //            //remove this option and add random on method SpeedCheck
            //            break;
            //        default:
            //            break;
            //    }
            //}

            //check battle end condition (both teams fainted)
            //battling = CheckTrainersTeams(player, enemy);
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

