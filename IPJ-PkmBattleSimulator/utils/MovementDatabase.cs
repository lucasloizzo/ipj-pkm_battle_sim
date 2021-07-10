using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

public static class MovementDatabase
{
    public static Movement GetMovement(string movementName)
    {
        try
        {
            switch (movementName)
            {
                case "Tackle":
                    return new Tackle();
                case "Slash":
                    return new Slash();
                case "Peck":
                    return new Peck();
                case "Thunderbolt":
                    return new Thunderbolt();
                case "Ember":
                    return new Ember();
                case "Splash":
                    return new Splash();
                default:
                    throw new MissingMovementException("The movement " + movementName + " does not exist");
            }

        }
        catch (MissingMovementException e)
        {
            Console.WriteLine(e.Message);
            return e.GetDefaultMovement();
        }
    }
}