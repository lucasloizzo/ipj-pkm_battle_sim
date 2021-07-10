using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

public static class TypeEffectivenessCheck
{
    //TODO Adapt the checks and the result for multi type pkm
    public static TypeEffectiveness CheckTypeEffectiveness(Type caster, Type objective)
    {
        TypeEffectiveness typeEffectiveness = TypeEffectiveness.Neutral;

        switch (caster)
        {
            case Type.Normal:
                if (objective == Type.Rock)
                {
                    typeEffectiveness = TypeEffectiveness.NotVeryEfffective;
                }
                else if (objective == Type.Ghost)
                {
                    typeEffectiveness = TypeEffectiveness.Immune;
                }
                else
                {
                    typeEffectiveness = TypeEffectiveness.Neutral;
                }

                break;
            case Type.Fighting:
                break;
            case Type.Flying:
                if (objective == Type.Grass || objective == Type.Bug || objective == Type.Fighting)
                {
                    typeEffectiveness = TypeEffectiveness.SuperEffective;
                }
                else if (objective == Type.Ghost || objective == Type.Flying || objective == Type.Poison || objective == Type.Fighting || objective == Type.Fire)
                {
                    typeEffectiveness = TypeEffectiveness.NotVeryEfffective;
                }
                else
                {
                    typeEffectiveness = TypeEffectiveness.Neutral;
                }
                break;
            case Type.Poison:
                break;
            case Type.Ground:
                break;
            case Type.Rock:
                break;
            case Type.Bug:
                if (objective == Type.Grass || objective == Type.Psychic)
                {
                    typeEffectiveness = TypeEffectiveness.SuperEffective;
                }
                else if (objective == Type.Electric || objective == Type.Grass)
                {
                    typeEffectiveness = TypeEffectiveness.NotVeryEfffective;
                }
                else if (objective == Type.Ground)
                {
                    typeEffectiveness = TypeEffectiveness.Immune;
                }
                else
                {
                    typeEffectiveness = TypeEffectiveness.Neutral;
                }
                break;
            case Type.Ghost:
                break;
            case Type.Fire:
                if (objective == Type.Grass || objective == Type.Bug)
                {
                    typeEffectiveness = TypeEffectiveness.SuperEffective;
                }
                else if (objective == Type.Rock || objective == Type.Water || objective == Type.Fire || objective == Type.Dragon)
                {
                    typeEffectiveness = TypeEffectiveness.NotVeryEfffective;
                }
                else
                {
                    typeEffectiveness = TypeEffectiveness.Neutral;
                }
                break;
            case Type.Water:
                break;
            case Type.Grass:
                break;
            case Type.Electric:
                if (objective == Type.Water || objective == Type.Flying)
                {
                    typeEffectiveness = TypeEffectiveness.SuperEffective;
                }
                else if (objective == Type.Electric || objective == Type.Grass)
                {
                    typeEffectiveness = TypeEffectiveness.NotVeryEfffective;
                }
                else if (objective == Type.Ground)
                {
                    typeEffectiveness = TypeEffectiveness.Immune;
                }
                else
                {
                    typeEffectiveness = TypeEffectiveness.Neutral;
                }
                break;
            case Type.Psychic:
                break;
            case Type.Ice:
                break;
            case Type.Dragon:
                break;
            case Type.Error:
                break;
            default:
                break;
        }

        return typeEffectiveness;
    }

    public static string GetEffectiveness(TypeEffectiveness typeEffectiveness)
    {
        string effect = "";

        switch (typeEffectiveness)
        {
            case TypeEffectiveness.SuperEffective:
                effect = "Super effective!";
                break;
            case TypeEffectiveness.NotVeryEfffective:
                effect = "not very effective..";
                break;
            case TypeEffectiveness.Immune:
                effect = "immune to that type of damage";
                break;
            case TypeEffectiveness.Neutral:
                effect = "neutral";
                break;
            default:
                break;
        }
        return effect;
    }

    public static float GetEffectivenessValue(TypeEffectiveness typeEffectiveness)
    {
        float effectiveness;
        //TODO add this check to TypeEffectivnessCheck and make it return a pair of string,float
        switch (typeEffectiveness)
        {
            case TypeEffectiveness.SuperEffective:
                effectiveness = 2;
                break;
            case TypeEffectiveness.NotVeryEfffective:
                effectiveness = 0.5f;
                break;
            case TypeEffectiveness.Immune:
                effectiveness = 0;
                break;
            case TypeEffectiveness.Neutral:
                effectiveness = 1;
                break;
            default:
                effectiveness = 1;
                break;
        }
        return effectiveness;
    }

}