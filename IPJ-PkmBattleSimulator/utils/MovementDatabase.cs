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
                case "Thunderbolt":
                    return new Thunderbolt();
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