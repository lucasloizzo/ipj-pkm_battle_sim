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
        winner = Battle(player, enemy);
        Console.WriteLine("The winner is " + winner.GetName() + ", congratulations!");
    }

    public Trainer Battle(Player player, Enemy enemy)
    {
        bool battling = true;
        BattleChoice battleChoice;
        SpeedCheck speedCheck;
        Movement movementChoice = new DefaultError();
        int pokemonChoice = 0;
        int damage = 0;

        do
        {
            //show state
            ShowGameState(player);
            ShowGameState(enemy);
            //player choice
            do
            {
                battleChoice = player.Choice();

                if (battleChoice == BattleChoice.Fight)
                {
                    try
                    {
                        movementChoice = player.MovementChoice(player.GetActivePokemon());
                    }
                    catch (Exception )
                    {
                        Console.WriteLine("That # of move does not exist. Try again.");
                        battleChoice = BattleChoice.Error;
                    }
                }
                else if (battleChoice == BattleChoice.Switch)
                {
                        pokemonChoice = player.PokemonChoice(player.GetPokemonTeam());
                }
                else
                {
                    Console.WriteLine("This is not the time nor the place. Choose again.");
                }
            } while (battleChoice == BattleChoice.Error);
            Console.Clear();
            //TODO enemy decision

            //check turn order
            if (battleChoice == BattleChoice.Switch)
            {
                try
                {
                    player.ChangeActivePokemon(pokemonChoice);
                }
                catch (Exception)
                {
                    Console.WriteLine("That # of pokemons does not exist. Try again.");
                    battleChoice = BattleChoice.Error;
                }
                //enemy moves second
                //check if pkm of player faints
            }
            //else if (enemyDecision == BattleChoice.Switch)
            //{
            //    //enemy move first
            //    //player moves second
            //    //check if pkm of enemy faints
            //}
            else
            {
                //check speed
                speedCheck = CheckTurnOrder(player.GetActivePokemon().GetSpeed(), enemy.GetActivePokemon().GetSpeed());
                switch (speedCheck)
                {
                    case SpeedCheck.PlayerFirst: //TODO add this code to a function for the enemy and another for the player
                        //player pokemon use move
                        damage = movementChoice.Use(player.GetActivePokemon(), enemy.GetActivePokemon());
                        enemy.GetActivePokemon().TakeDamage(damage);
                        Console.WriteLine(player.GetActivePokemon().GetName() + " uses " + movementChoice + ". It dealt " + damage + " damage.\n");
                        //check if enemy pkm faints (if it does, check if team is alive and if it is, select one to replace)
                        bool pkmAlive = enemy.GetActivePokemon().CheckPokemonAlive(enemy.GetActivePokemon().GetCurrentLife());
                        if (pkmAlive == false)
                        {
                            EnemyPkmFainted();
                        }
                        else
                        {
                            //TODO enemy pkm use move
                            Console.WriteLine("Enemy forgot how to act");
                        }
                        //check if player pkm faints (if it does, select one to replace)
                        pkmAlive = player.GetActivePokemon().CheckPokemonAlive(player.GetActivePokemon().GetCurrentLife());
                        if (pkmAlive == false)
                        {
                            PlayerPkmFainted();
                        }
                        break;
                    case SpeedCheck.EnemyFirst:
                        //enemy pkm use move
                        Console.WriteLine("Enemy forgot how to act");
                        //check if player pkm faints (if it does, select one to replace)
                        pkmAlive = player.GetActivePokemon().CheckPokemonAlive(player.GetActivePokemon().GetCurrentLife());
                        if (pkmAlive == false)
                        {
                            PlayerPkmFainted();
                        }
                        else
                        {
                            //player pokemon use move
                            damage = movementChoice.Use(player.GetActivePokemon(), enemy.GetActivePokemon());
                            enemy.GetActivePokemon().TakeDamage(damage);
                            Console.WriteLine(player.GetActivePokemon().GetName() + " uses " + movementChoice + ". It dealt " + damage + " damage.\n");
                        }
                        //check if enemy pkm faints (if it does, select one to replace)
                        pkmAlive = enemy.GetActivePokemon().CheckPokemonAlive(enemy.GetActivePokemon().GetCurrentLife());
                        if (pkmAlive == false)
                        {
                            EnemyPkmFainted();
                        }
                        break;
                    default:
                        break;
                }
            }

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
        if (checkPlayerTeam == false || checkEnemyTeam == false)
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

    public SpeedCheck CheckTurnOrder(int playerPokemonSpeed, int enemyPokemonSpeed)
    {
        SpeedCheck turnOrder = SpeedCheck.Equal;

        if (playerPokemonSpeed > enemyPokemonSpeed)
        {
            turnOrder = SpeedCheck.PlayerFirst;
        }
        else if (enemyPokemonSpeed > playerPokemonSpeed)
        {
            turnOrder = SpeedCheck.EnemyFirst;
        }
        else
        {
            int roll = new Random().Next(0, 1);
            switch (roll)
            {
                case 1:
                    turnOrder = SpeedCheck.PlayerFirst;
                    break;
                case 2:
                    turnOrder = SpeedCheck.EnemyFirst;
                    break;
                default:
                    break;
            }
        }
        return turnOrder;
    }

    public void EnemyPkmFainted()
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
    }

    public void PlayerPkmFainted()
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
    }
}