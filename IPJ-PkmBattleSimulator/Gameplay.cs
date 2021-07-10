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
        string path = "../../../resources/Red";
        DirectoryInfo directoryPlayer = new DirectoryInfo(path);
        string namePlayer = directoryPlayer.Name;
        player = new Player(namePlayer, path);
        path = "../../../resources/Blue";
        DirectoryInfo directoryEnemy = new DirectoryInfo(path);
        string nameEnemy = directoryEnemy.Name;
        enemy = new Enemy(nameEnemy, path);
    }

    public void Play()
    {
        Trainer winner; 
        winner = Battle(player, enemy);
        Console.WriteLine("The winner is " + winner.GetName() + ", congratulations!");
    }

    public Trainer Battle(Player player, Enemy enemy)
    {
        bool battling;
        BattleChoice playerBattleChoice;
        BattleChoice enemyDecision;
        SpeedCheck speedCheck;
        Movement playerMovementChoice = new DefaultError();
        Movement enemyMovement;
        int pokemonChoice = 0;
        bool pkmAlive;

        do
        {
            Console.Clear();
            //show state
            ShowGameState(player);
            ShowGameState(enemy);
            //player choice
            do
            {
                playerBattleChoice = player.Choice();

                if (playerBattleChoice == BattleChoice.Fight)
                {
                    try
                    {
                        playerMovementChoice = player.MovementChoice(player.GetActivePokemon());
                    }
                    catch (Exception )
                    {
                        Console.WriteLine("That # of move does not exist. Try again.");
                        playerBattleChoice = BattleChoice.Error;
                    }
                }
                else if (playerBattleChoice == BattleChoice.Switch)
                {
                        pokemonChoice = player.PokemonChoice(player.GetPokemonTeam());
                }
                else
                {
                    Console.WriteLine("This is not the time nor the place. Choose again.");
                }
            } while (playerBattleChoice == BattleChoice.Error);

            //TODO enemy decision. change when implemented in AI (only return movement choice now) it needs to return <BattleChoice, Movement>
            enemyDecision = BattleChoice.Fight;
            enemyMovement = enemy.UpdateState(enemy, player.GetActivePokemon());

            //check turn order
            switch (playerBattleChoice)
            {
                case BattleChoice.Switch:
                    try
                    {
                        player.ChangeActivePokemon(pokemonChoice);
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("That # of pokemons does not exist. Try again.");
                        playerBattleChoice = BattleChoice.Error;
                    }
                    switch (enemyDecision)
                    {
                        case BattleChoice.Switch:

                            //TODO implement enemy switch

                            break;
                        case BattleChoice.Fight:
                            //enemy pkm use move
                            player = (Player)DamageCalc(enemy, player, enemyMovement);
                            //check if player pkm faints (if it does, select one to replace)
                            pkmAlive = player.GetActivePokemon().CheckPokemonAlive(player.GetActivePokemon().GetCurrentLife());
                            if (pkmAlive == false)
                            {
                                Console.WriteLine(player.FaintMessage(player));
                                player.PlayerPkmFainted(player);
                            }
                            break;
                        default:
                            break;
                    }
                    break;
                case BattleChoice.Fight:
                    switch (enemyDecision)
                    {
                        case BattleChoice.Switch:

                            //TODO implement enemy switch

                            //player pokemon use move
                            enemy = (Enemy)DamageCalc(player, enemy, playerMovementChoice);
                            //check if enemy pkm faints (if it does, check if team is alive and if it is, select one to replace)
                            pkmAlive = enemy.GetActivePokemon().CheckPokemonAlive(enemy.GetActivePokemon().GetCurrentLife());
                            if (pkmAlive == false)
                            {
                                Console.WriteLine(enemy.FaintMessage(enemy));
                                enemy.EnemyPkmFainted(enemy);
                            }
                            break;
                        case BattleChoice.Fight:
                            speedCheck = CheckTurnOrder(player.GetActivePokemon().GetSpeed(), enemy.GetActivePokemon().GetSpeed());
                            switch (speedCheck)
                            {
                                case SpeedCheck.PlayerFirst:
                                    //player pokemon use move
                                    enemy = (Enemy)DamageCalc(player, enemy, playerMovementChoice);
                                    //check if enemy pkm faints (if it does, check if team is alive and if it is, select one to replace)
                                    pkmAlive = enemy.GetActivePokemon().CheckPokemonAlive(enemy.GetActivePokemon().GetCurrentLife());
                                    if (pkmAlive == false)
                                    {
                                        Console.WriteLine(enemy.FaintMessage(enemy));
                                        enemyMovement = new DefaultError();
                                        enemy.EnemyPkmFainted(enemy);
                                    }
                                    else
                                    {
                                        //enemy pkm use move
                                        if (enemyMovement.name != "DefaultError")
                                        {
                                            player = (Player)DamageCalc(enemy, player, enemyMovement);
                                        }
                                    }
                                    //check if player pkm faints (if it does, select one to replace)
                                    pkmAlive = player.GetActivePokemon().CheckPokemonAlive(player.GetActivePokemon().GetCurrentLife());
                                    if (pkmAlive == false)
                                    {
                                        Console.WriteLine(player.FaintMessage(player));
                                        player.PlayerPkmFainted(player);
                                    }
                                    break;
                                case SpeedCheck.EnemyFirst:
                                    //enemy pkm use move
                                    player = (Player)DamageCalc(enemy, player, enemyMovement);
                                    //check if player pkm faints (if it does, select one to replace)
                                    pkmAlive = player.GetActivePokemon().CheckPokemonAlive(player.GetActivePokemon().GetCurrentLife());
                                    if (pkmAlive == false)
                                    {
                                        Console.WriteLine(player.FaintMessage(player));
                                        player.PlayerPkmFainted(player);
                                    }
                                    else
                                    {
                                        //player pokemon use move
                                        enemy = (Enemy)DamageCalc(player, enemy, playerMovementChoice);
                                    }
                                    //check if enemy pkm faints (if it does, select one to replace)
                                    pkmAlive = enemy.GetActivePokemon().CheckPokemonAlive(enemy.GetActivePokemon().GetCurrentLife());
                                    if (pkmAlive == false)
                                    {
                                        Console.WriteLine(enemy.FaintMessage(enemy));
                                        enemy.EnemyPkmFainted(enemy);
                                    }
                                    break;
                                default:
                                    break;
                            }
                            break;
                        default:
                            break;
                    }
                    break;

                default:
                    break;
            }

            //check battle end condition (any team fainted)
            battling = CheckTrainersTeams(player, enemy);
            Console.WriteLine("Turn ended. Press any key to continue...");
            Console.ReadKey();
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

    public Trainer DamageCalc(Trainer caster, Trainer objective, Movement movementChoice)
    {
        float damage, movementDamage;
        float effectiveness;
        float bonification = 1;
        float variation = new Random().Next(85, 100);
        TypeEffectiveness typeEffectiveness;
        Pokemon casterPkm = caster.GetActivePokemon();
        Pokemon objectivePkm = objective.GetActivePokemon();
        typeEffectiveness = TypeEffectivenessCheck.CheckTypeEffectiveness(Types.GetInstance().GetType(movementChoice.type), Types.GetInstance().GetType(objectivePkm.GetTypes().Trim()));

        //Check movement effectiveness againts target pkm
        effectiveness = TypeEffectivenessCheck.GetEffectivenessValue(typeEffectiveness);

        //If movement is the same type of the pokemon, it gets a 1.5 bonus
        if (Types.GetInstance().GetType(movementChoice.type) == Types.GetInstance().GetType(objectivePkm.GetTypes().Trim()))
        {
            bonification = 1.5f;
        }

        movementDamage = movementChoice.Use(casterPkm, objectivePkm);
        damage = 0.01f * bonification * effectiveness * variation * movementDamage;

        objectivePkm.TakeDamage((int)damage);
        Console.WriteLine(caster.GetName() + "'s " + casterPkm.GetName() + " uses " + movementChoice + ". It's " + TypeEffectivenessCheck.GetEffectiveness(typeEffectiveness)  + ". It dealt " + (int)damage + " damage.\n");

        return objective;
    }
}