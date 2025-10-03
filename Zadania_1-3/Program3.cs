using System;
using System.Collections.Generic;

public abstract class Vehicle
{
    protected string _brand;
    protected string _model;
    protected int _year;

    public Vehicle(string brand, string model, int year)
    {
        _brand = brand;
        _model = model;
        _year = year;
    }

    public void DisplayInfo()
    {
        Console.WriteLine($"\nBrand: {_brand}\n" +
                            $"Model: {_model}\n" +
                            $"Year: {_year}\n");
    }

    public abstract void Start();
}

public class Car : Vehicle
{
    public Car(string brand, string model, int year) : base(brand, model, year)
    {
    }

    public override void Start()
    {
        Console.WriteLine("Starting the engine...\n");
    }
}

public class Bicycle : Vehicle
{
    public Bicycle(string brand, string model, int year) : base(brand, model, year)
    {
    }

    public override void Start()
    {
        Console.WriteLine("Starting to pedal...\n");
    }
}

public enum Vehicles
{
    Car,
    Bicycle
}

public static class Menu
{
    public static List<Vehicle> VehicleList = new();
    public static void DisplayMenu()
    {
        Console.Write("1. Add a Car\n" +
                            "2. Add a Bicycle\n" +
                            "3. Display all Vehicles\n" +
                            "4. Exit\n" +
                            "Enter your choice:");
    }

    public static void CreateVehicle(Vehicles type)
    {
        if (type != Vehicles.Car && type != Vehicles.Bicycle)
            throw new Exception("Invalid vehicle type!");

        string typeStr = type.ToString();

        Console.WriteLine($"{typeStr} creator\n");

        Console.Write($"Enter {typeStr} name: ");
        string name = Console.ReadLine();
        if (name == String.Empty)
            throw new Exception("Name cannot be empty!");

        Console.Write($"Enter {typeStr} model: ");
        string model = Console.ReadLine();
        if (model == String.Empty)
            throw new Exception("Model cannot be empty!");

        Console.Write($"Enter {typeStr} year: ");
        int year = Convert.ToInt32(Console.ReadLine());

        if (year > DateTime.Today.Year)
            throw new Exception("Invalid car year!");

        if(type == Vehicles.Car)
        {
            Car c1 = new(name, model, year);
            VehicleList.Add(c1);
        }
        else
        {
            Bicycle b1 = new(name, model, year);
            VehicleList.Add(b1);
        }

        Console.WriteLine($"\nSuccesfully added new {typeStr}\n");
    }

    public static void DisplayVehicles()
    {
        foreach(Vehicle v in VehicleList)
        {
            v.DisplayInfo();
            v.Start();
        }
    }
}

class Program
{
    static void Main()
    {
        bool endProgram = false;
        while (!endProgram)
        {
            Menu.DisplayMenu();
            try
            {
                int choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        Menu.CreateVehicle(Vehicles.Car);
                        break;
                    case 2:
                        Menu.CreateVehicle(Vehicles.Bicycle);
                        break;
                    case 3:
                        Menu.DisplayVehicles();
                        break;
                    case 4:
                        Console.WriteLine("Exiting program...");
                        endProgram = true;
                        break;
                    default:
                        throw new Exception("Your choice can be between 1 and 4!");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.WriteLine("\nTry again!\n");
            }
        }
        
    }
}