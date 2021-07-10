using System;
using System.Collections.Generic;
using System.Text;

public sealed class Enemy : Trainer
{
    private STATES currentState;

    public Enemy(string name, string path) : base(name, path)
    {
        currentState = STATES.NORMAL;
    }

    public Enemy EnemyPkmFainted(Enemy enemy)
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

    public enum STATES
    {
        NORMAL,
        AGGRESSIVE,
        DEFENSIVE,
        ERROR
    };


    public Movement UpdateState(Enemy trainer, Pokemon playerPkm)
    {
        Movement enemyMovement = new DefaultError();
        currentState = CheckAdvantage(trainer, playerPkm);
        switch (currentState)
        {
            case STATES.NORMAL:
                enemyMovement = Normal(trainer.GetActivePokemon().GetMovements(), Types.GetInstance().GetType(playerPkm.GetTypes().Trim()));
                break;
            case STATES.AGGRESSIVE:
                enemyMovement = Aggressive(trainer.GetActivePokemon().GetMovements(), Types.GetInstance().GetType(playerPkm.GetTypes().Trim()));
                break;
            case STATES.DEFENSIVE:
                //swap when defensive state implemented
                //use normal when pkm left <= 1 or all pkm have disadvantage
                //Defensive();
                enemyMovement = Normal(trainer.GetActivePokemon().GetMovements(), Types.GetInstance().GetType(playerPkm.GetTypes().Trim()));
                break;
            default:
                enemyMovement = Normal(trainer.GetActivePokemon().GetMovements(), Types.GetInstance().GetType(playerPkm.GetTypes().Trim()));
                break;
        }
        return enemyMovement;
    }

    private STATES CheckAdvantage(Enemy enemy, Pokemon playerPkm)
    {
        Pokemon enemyPkm = enemy.GetActivePokemon();
        TypeEffectiveness checkAdvantage = TypeEffectivenessCheck.CheckTypeEffectiveness(Types.GetInstance().GetType(enemyPkm.GetTypes().Trim()), Types.GetInstance().GetType(playerPkm.GetTypes().Trim()));
        switch (checkAdvantage)
        {
            case TypeEffectiveness.SuperEffective:
                currentState = STATES.AGGRESSIVE;
                break;
            case TypeEffectiveness.NotVeryEfffective:
                //TODO swap to defensive when implemented
                //currentState = STATES.DEFENSIVE; 
                currentState = STATES.NORMAL;
                break;
            case TypeEffectiveness.Immune:
                //TODO swap to defensive when implemented
                //currentState = STATES.DEFENSIVE;
                currentState = STATES.NORMAL;
                break;
            case TypeEffectiveness.Neutral:
                currentState = STATES.NORMAL;
                break;
            default:
                currentState = STATES.NORMAL;
                break;
        }
        return currentState;
    }

    private Movement Normal(List<Movement> movements, Type type)
    {
        Movement enemyMovement = movements[0];
        foreach (Movement movement in movements)
        {
            TypeEffectiveness checkAdvantage = TypeEffectivenessCheck.CheckTypeEffectiveness(Types.GetInstance().GetType(movement.type), type);
            //check if not immune or not effective
            //use higest potency
            if (checkAdvantage != TypeEffectiveness.Immune && checkAdvantage != TypeEffectiveness.NotVeryEfffective)
            {
                if (movement.potency > enemyMovement.potency)
                {
                    enemyMovement = movement;
                }
            }
        }
        return enemyMovement;
    }

    private Movement Aggressive(List<Movement> movements, Type type)
    {
        Movement enemyMovement = movements[0];
        foreach (Movement movement in movements)
        {
            //check advantage move
            //if true use aggresive, else use normal and swap to normal
            TypeEffectiveness checkAdvantage = TypeEffectivenessCheck.CheckTypeEffectiveness(Types.GetInstance().GetType(movement.type), type);
            if (checkAdvantage == TypeEffectiveness.SuperEffective)
            {
                if (movement.potency * 2 >= enemyMovement.potency)
                {
                    enemyMovement = movement;
                }
            }
        }
        return enemyMovement;
    }

    private void Defensive()
    {

    }

}
