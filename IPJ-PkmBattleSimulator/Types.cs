using System;
using System.Collections.Generic;
using System.Text;

public class Types
{
    Dictionary<string, Type> typesDatabase = new Dictionary<string, Type>();

    private static Types instance;

    private Types()
    {
        typesDatabase.Add("Normal", Type.Normal);
        typesDatabase.Add("Fighting", Type.Fighting);
        typesDatabase.Add("Flying", Type.Flying);
        typesDatabase.Add("Poison", Type.Poison);
        typesDatabase.Add("Ground", Type.Ground);
        typesDatabase.Add("Rock", Type.Rock);
        typesDatabase.Add("Bug", Type.Bug);
        typesDatabase.Add("Ghost", Type.Ghost);
        typesDatabase.Add("Fire", Type.Fire);
        typesDatabase.Add("Water", Type.Water);
        typesDatabase.Add("Grass", Type.Grass);
        typesDatabase.Add("Electric", Type.Electric);
        typesDatabase.Add("Psychic", Type.Psychic);
        typesDatabase.Add("Ice", Type.Ice);
        typesDatabase.Add("Dragon", Type.Dragon);
    }

    public Type GetType(string typeName)
    {
        if (typesDatabase.ContainsKey(typeName))
        {
            return typesDatabase[typeName];
        }
        else
        {
            Console.WriteLine("Error");
            return Type.Error;
        }
    }

    public static Types GetInstance()
    {
        if (instance == null)
        {
            instance = new Types();
        }
        return instance;
    }
}