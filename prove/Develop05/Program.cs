using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static List<Goal> goals = new List<Goal>();
    static int totalScore = 0;

    static void Main()
    {
        string choice = "";
        while (choice != "6")
        {
            Console.WriteLine($"\nScore: {totalScore}");
            Console.WriteLine("1. Create New Goal");
            Console.WriteLine("2. List Goals");
            Console.WriteLine("3. Save Goals");
            Console.WriteLine("4. Load Goals");
            Console.WriteLine("5. Record Event");
            Console.WriteLine("6. Quit");
            Console.Write("Choose an option: ");
            choice = Console.ReadLine();

            switch (choice)
            {
                case "1": CreateGoal(); break;
                case "2": ListGoals(); break;
                case "3": SaveGoals(); break;
                case "4": LoadGoals(); break;
                case "5": RecordEvent(); break;
                case "6": Console.WriteLine("Goodbye!"); break;
                default: Console.WriteLine("Invalid option."); break;
            }
        }
    }

    static void CreateGoal()
    {
        Console.WriteLine("Choose goal type:");
        Console.WriteLine("1. Simple");
        Console.WriteLine("2. Eternal");
        Console.WriteLine("3. Checklist");
        string type = Console.ReadLine();

        Console.Write("Goal name: ");
        string name = Console.ReadLine();
        Console.Write("Description: ");
        string desc = Console.ReadLine();
        Console.Write("Point value: ");
        int points = int.Parse(Console.ReadLine());

        if (type == "1")
        {
            goals.Add(new SimpleGoal(name, desc, points));
        }
        else if (type == "2")
        {
            goals.Add(new EternalGoal(name, desc, points));
        }
        else if (type == "3")
        {
            Console.Write("How many times to complete: ");
            int target = int.Parse(Console.ReadLine());
            Console.Write("Bonus points: ");
            int bonus = int.Parse(Console.ReadLine());
            goals.Add(new ChecklistGoal(name, desc, points, target, bonus));
        }
        else
        {
            Console.WriteLine("Invalid goal type.");
        }
    }

    static void ListGoals()
    {
        for (int i = 0; i < goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {goals[i].GetDetails()}");
        }
    }

    static void SaveGoals()
    {
        using (StreamWriter writer = new StreamWriter("goals.txt"))
        {
            foreach (Goal g in goals)
            {
                writer.WriteLine(g.SaveString());
            }
            writer.WriteLine($"SCORE:{totalScore}");
        }
        Console.WriteLine("Goals saved.");
    }

    static void LoadGoals()
    {
        if (File.Exists("goals.txt"))
        {
            goals.Clear();
            string[] lines = File.ReadAllLines("goals.txt");
            foreach (string line in lines)
            {
                if (line.StartsWith("SCORE:"))
                {
                    totalScore = int.Parse(line.Split(':')[1]);
                }
                else
                {
                    string[] parts = line.Split('|');
                    string type = parts[0];
                    if (type == "Simple")
                    {
                        bool done = bool.Parse(parts[4]);
                        var g = new SimpleGoal(parts[1], parts[2], int.Parse(parts[3]));
                        if (done) g.MarkDone();
                        goals.Add(g);
                    }
                    else if (type == "Eternal")
                    {
                        goals.Add(new EternalGoal(parts[1], parts[2], int.Parse(parts[3])));
                    }
                    else if (type == "Checklist")
                    {
                        var g = new ChecklistGoal(parts[1], parts[2], int.Parse(parts[3]),
                                                  int.Parse(parts[4]), int.Parse(parts[5]));
                        g.SetCount(int.Parse(parts[6]));
                        goals.Add(g);
                    }
                }
            }
            Console.WriteLine("Goals loaded.");
        }
        else
        {
            Console.WriteLine("No saved goals found.");
        }
    }

    static void RecordEvent()
    {
        ListGoals();
        Console.Write("Enter goal number to record: ");
        int index = int.Parse(Console.ReadLine()) - 1;
        if (index >= 0 && index < goals.Count)
        {
            int earned = goals[index].RecordEvent();
            totalScore += earned;
            Console.WriteLine($"Points earned: {earned}");
        }
        else
        {
            Console.WriteLine("Invalid goal number.");
        }
    }
}

// ==========================
//        Goal Classes
// ==========================

abstract class Goal
{
    public string Name;
    public string Description;
    public int Points;

    public Goal(string name, string description, int points)
    {
        Name = name;
        Description = description;
        Points = points;
    }

    public abstract int RecordEvent();
    public abstract string GetDetails();
    public abstract string SaveString();
}

class SimpleGoal : Goal
{
    private bool done = false;

    public SimpleGoal(string name, string description, int points) : base(name, description, points) { }

    public void MarkDone() => done = true;

    public override int RecordEvent()
    {
        if (!done)
        {
            done = true;
            return Points;
        }
        return 0;
    }

    public override string GetDetails()
    {
        return $"[{(done ? "X" : " ")}] {Name} - {Description}";
    }

    public override string SaveString()
    {
        return $"Simple|{Name}|{Description}|{Points}|{done}";
    }
}

class EternalGoal : Goal
{
    public EternalGoal(string name, string description, int points) : base(name, description, points) { }

    public override int RecordEvent() => Points;

    public override string GetDetails() => $"[∞] {Name} - {Description}";

    public override string SaveString()
    {
        return $"Eternal|{Name}|{Description}|{Points}";
    }
}

class ChecklistGoal : Goal
{
    private int count = 0;
    private int target;
    private int bonus;

    public ChecklistGoal(string name, string description, int points, int target, int bonus)
        : base(name, description, points)
    {
        this.target = target;
        this.bonus = bonus;
    }

    public void SetCount(int c) => count = c;

    public override int RecordEvent()
    {
        if (count < target)
        {
            count++;
            if (count == target)
                return Points + bonus;
            return Points;
        }
        return 0;
    }

    public override string GetDetails()
    {
        return $"[{count}/{target}] {Name} - {Description}";
    }

    public override string SaveString()
    {
        return $"Checklist|{Name}|{Description}|{Points}|{target}|{bonus}|{count}";
    }
}
