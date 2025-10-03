using System;

public abstract class Vehicle
{
    public Vehicle()
    {
        Console.WriteLine("Vehicle created");
    }

    public abstract void StartEngine();
}

public class Car : Vehicle
{
    public override void StartEngine()
    {
        Console.WriteLine("Car engine started");
    }
}

public class Bicycle : Vehicle
{
    public override void StartEngine()
    {
        Console.WriteLine("Bicycle has no engine to start!");
    }
}

class Program
{
    static void Main(string[] args)
    {
        Car c1 = new();
        Bicycle b1 = new();
        Vehicle v1 = c1;
        Vehicle v2 = b1;
        c1.StartEngine();
        b1.StartEngine();
        v1.StartEngine();
        v2.StartEngine();
    }
}