using System;
using System.Collections.Generic;
using System.Text;

public abstract class Movement
{
    public string name;
    public int potency;
    //public int maxUses;
    public string type;
    public abstract int Use(Pokemon caster, Pokemon objective);

}

public sealed class DefaultError : Movement
{
    public DefaultError()
    {
        name = "DefaultError";
        potency = 0;
        type = "Normal";
    }
    public override int Use(Pokemon caster, Pokemon objective)
    {
        return 0;
    }
    public string GetDefaultErrror()
    {
        return name;
    }
}

public sealed class Tackle : Movement
{
    public Tackle()
    {
        name = "Tackle";
        potency = 40;
        type = "Normal";
    }

    public override int Use(Pokemon caster, Pokemon objective)
    {
        int damage = 0;
        damage = (caster.GetAttack() + potency - objective.GetDefense());
        return damage;
    }
}

public sealed class Thunderbolt : Movement
{
    public Thunderbolt()
    {
        name = "Thunderbolt";
        potency = 400;
        type = "Electric";
    }

    public override int Use(Pokemon caster, Pokemon objective)
    {
        int damage = 0;
        damage = (caster.GetSpecialAttack() + potency - objective.GetSpecialDefense());
        return damage;
    }
}