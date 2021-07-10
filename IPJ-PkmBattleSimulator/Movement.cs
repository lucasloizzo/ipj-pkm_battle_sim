using System;
using System.Collections.Generic;
using System.Text;

public abstract class Movement
{
    public string name;
    public int potency;
    //public int maxUses;
    public string type;
    public abstract float Use(Pokemon caster, Pokemon objective);

}

public sealed class DefaultError : Movement
{
    public DefaultError()
    {
        name = "DefaultError";
        potency = 0;
        type = "Normal";
    }
    public override float Use(Pokemon caster, Pokemon objective)
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
        potency = 35;
        type = "Normal";
    }

    public override float Use(Pokemon caster, Pokemon objective)
    {
        float damage;
        damage = (float)(((0.2 * caster.GetLevel() + 1) * caster.GetAttack() * potency) / (25 * objective.GetDefense())) + 2;
        return damage;
    }
}

public sealed class Slash : Movement
{
    public Slash()
    {
        name = "Slash";
        potency = 50;
        type = "Normal";
    }

    public override float Use(Pokemon caster, Pokemon objective)
    {
        float damage;
        damage = (float)(((0.2 * caster.GetLevel() + 1) * caster.GetAttack() * potency) / (25 * objective.GetDefense())) + 2;
        return damage;
    }
}

public sealed class Peck : Movement
{
    public Peck()
    {
        name = "Peck";
        potency = 40;
        type = "Flying";
    }

    public override float Use(Pokemon caster, Pokemon objective)
    {
        float damage;
        damage = (float)(((0.2 * caster.GetLevel() + 1) * caster.GetAttack() * potency) / (25 * objective.GetDefense())) + 2;
        return damage;
    }
}

public sealed class Thunderbolt : Movement
{
    public Thunderbolt()
    {
        name = "Thunderbolt";
        potency = 95;
        type = "Electric";
    }

    public override float Use(Pokemon caster, Pokemon objective)
    {
        float damage;
        damage = (float)(((0.2 * caster.GetLevel() + 1) * caster.GetSpecialAttack() * potency) / (25 * objective.GetSpecialDefense())) + 2;
        return damage;
    }
}

public sealed class Ember : Movement
{
    public Ember()
    {
        name = "Ember";
        potency = 40;
        type = "Fire";
    }

    public override float Use(Pokemon caster, Pokemon objective)
    {
        float damage;
        damage = (float)(((0.2 * caster.GetLevel() + 1) * caster.GetSpecialAttack() * potency) / (25 * objective.GetSpecialDefense())) + 2;
        return damage;
    }
}

public sealed class Splash : Movement
{
    public Splash()
    {
        name = "Splash";
        potency = 0;
        type = "Normal";
    }

    public override float Use(Pokemon caster, Pokemon objective)
    {
        int damage = 0;
        return damage;
    }
}