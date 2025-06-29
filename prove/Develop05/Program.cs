using System;
using System.Collections.Generic;
using System.IO; // Added for File operations

class Program
{
    static List<Goal> goals = new List<Goal>();
    static int totalScore = 0; // Added total score

    static void Main(string[] args)
    {
        bool running = true;
        while (running)
        {
            Console.WriteLine($"\nYour Current Score: {totalScore} points"); // Display total score
            Console.WriteLine("\nMenu Options:");
            Console.WriteLine("1. Create New Goal");
            Console.WriteLine("2. List Goals");
            Console.WriteLine("3. Save Goals");
            Console.WriteLine("4. Load Goals");
            Console.WriteLine("5. Record Event");
            Console.WriteLine("6. Quit");
            Console.Write("Choose a menu number: ");
            string input = Console.ReadLine();

            switch (input)
            {
                case "1": CreateGoal(); break;
                case "2": ListGoals(); break;
                case "3": SaveGoals(); break;
                case "4": LoadGoals(); break;
                case "5": RecordGoalEvent(); break;
                case "6": running = false; Console.WriteLine("Goodbye!"); break;
                default: Console.WriteLine("Invalid option. Try again."); break;
            }
        }
    }

    static void CreateGoal()
    {
        Console.WriteLine("Select goal type:");
        Console.WriteLine("1. Simple Goal");
        Console.WriteLine("2. Eternal Goal");
        Console.WriteLine("3. Checklist Goal");
        string choice = Console.ReadLine();

        Console.Write("Enter goal name: ");
        string name = Console.ReadLine();
        Console.Write("Enter description: ");
        string desc = Console.ReadLine();

        int points;
        Console.Write("Enter point value: ");
        while (!int.TryParse(Console.ReadLine(), out points) || points < 0)
        {
            Console.Write("Invalid input. Please enter a non-negative number for points: ");
        }

        switch (choice)
        {
            case "1":
                goals.Add(new SimpleGoal(name, desc, points));
                break;
            case "2":
                goals.Add(new EternalGoal(name, desc, points));
                break;
            case "3":
                int targetCount;
                Console.Write("Enter number of times to complete: ");
                while (!int.TryParse(Console.ReadLine(), out targetCount) || targetCount <= 0)
                {
                    Console.Write("Invalid input. Please enter a positive number for times to complete: ");
                }

                int bonus;
                Console.Write("Enter bonus points: ");
                while (!int.TryParse(Console.ReadLine(), out bonus) || bonus < 0)
                {
                    Console.Write("Invalid input. Please enter a non-negative number for bonus points: ");
                }
                goals.Add(new ChecklistGoal(name, desc, points, targetCount, bonus));
                break;
            default:
                Console.WriteLine("Invalid goal type. Goal not created.");
                return;
        }

        Console.WriteLine("Goal created!");
    }

    static void ListGoals()
    {
        if (goals.Count == 0)
        {
            Console.WriteLine("No goals to display.");
            return;
        }

        Console.WriteLine("\nYour Goals:");
        int i = 1;
        foreach (Goal goal in goals)
        {
            Console.WriteLine($"{i++}. {goal.GetDetails()}");
        }
    }

    static void SaveGoals()
    {
        using (StreamWriter writer = new StreamWriter("goals.txt"))
        {
            foreach (Goal goal in goals)
            {
                writer.WriteLine(goal.GetStringRepresentation());
            }
            writer.WriteLine($"TotalScore:{totalScore}"); // Save total score
        }
        Console.WriteLine("Goals saved.");
    }

    static void LoadGoals()
    {
        goals.Clear();
        totalScore = 0; // Reset total score before loading
        if (File.Exists("goals.txt"))
        {
            string[] lines = File.ReadAllLines("goals.txt");
            foreach (string line in lines)
            {
                if (line.StartsWith("TotalScore:"))
                {
                    totalScore = int.Parse(line.Split(':')[1]);
                    continue;
                }

                string[] parts = line.Split('|');
                switch (parts[0])
                {
                    case "SimpleGoal":
                        var simple = new SimpleGoal(parts[1], parts[2], int.Parse(parts[3]));
                        
                        if (bool.Parse(parts[4]))
                        {
                            simple.MarkComplete(); 
                        }
                        goals.Add(simple);
                        break;
                    case "EternalGoal":
                        goals.Add(new EternalGoal(parts[1], parts[2], int.Parse(parts[3])));
                        break;
                    case "ChecklistGoal":

                        var checklist = new ChecklistGoal(parts[1], parts[2], int.Parse(parts[3]), int.Parse(parts[5]), int.Parse(parts[4]));
                        checklist.SetCompletedCount(int.Parse(parts[6])); 
                        goals.Add(checklist);
                        break;
                }
            }
            Console.WriteLine("Goals loaded.");
        }
        else
        {
            Console.WriteLine("No saved goals found.");
        }
    }

    static void RecordGoalEvent()
    {
        if (goals.Count == 0)
        {
            Console.WriteLine("No goals to record events for. Create some goals first.");
            return;
        }

        ListGoals();
        Console.Write("Select goal number to record event: ");
        int index;
        while (!int.TryParse(Console.ReadLine(), out index) || index <= 0 || index > goals.Count)
        {
            Console.Write("Invalid input. Please enter a valid goal number: ");
        }

        Goal selectedGoal = goals[index - 1];
        int pointsEarned = selectedGoal.RecordEventAndGetPoints(); 
        totalScore += pointsEarned;
        Console.WriteLine($"You have accumulated {totalScore} points.");
    }

    public abstract class Goal
    {
        public string GoalName { get; protected set; }
        public string Description { get; protected set; }
        public int Points { get; protected set; }

        public Goal(string name, string description, int points)
        {
            GoalName = name;
            Description = description;
            Points = points;
        }

        public abstract int RecordEventAndGetPoints(); 
        public abstract bool IsComplete();
        public abstract string GetDetails();
        public abstract string GetStringRepresentation();
    }



    class SimpleGoal : Goal
    {
        private bool _completed;

        public SimpleGoal(string name, string description, int points) : base(name, description, points)
        {
            _completed = false;
        }

        // Added for loading to set completion without awarding points again
        public void MarkComplete()
        {
            _completed = true;
        }

        public override int RecordEventAndGetPoints()
        {
            if (!_completed)
            {
                _completed = true;
                Console.WriteLine($"Congratulations! You completed '{GoalName}' and earned {Points} points!");
                return Points;
            }
            else
            {
                Console.WriteLine($"The goal '{GoalName}' is already completed.");
                return 0;
            }
        }

        public override bool IsComplete() => _completed;

        public override string GetDetails() => $"[{(_completed ? "X" : " ")}] {GoalName} ({Description})";

        public override string GetStringRepresentation() => $"SimpleGoal|{GoalName}|{Description}|{Points}|{_completed}";
    }

    class EternalGoal : Goal
    {
        public EternalGoal(string name, string description, int points) : base(name, description, points) { }

        public override int RecordEventAndGetPoints()
        {
            Console.WriteLine($"You recorded an event for '{GoalName}' and earned {Points} points!");
            return Points;
        }

        public override bool IsComplete() => false;

        public override string GetDetails() => $"[∞] {GoalName} ({Description})";

        public override string GetStringRepresentation() => $"EternalGoal|{GoalName}|{Description}|{Points}";
    }

    class ChecklistGoal : Goal
    {
        private int _targetCount;
        private int _completedCount;
        private int _bonus;

        public ChecklistGoal(string name, string description, int points, int targetCount, int bonus) : base(name, description, points)
        {
            _targetCount = targetCount;
            _completedCount = 0;
            _bonus = bonus;
        }

        // Added for loading to set completed count directly
        public void SetCompletedCount(int count)
        {
            _completedCount = count;
        }

        public override int RecordEventAndGetPoints()
        {
            if (_completedCount < _targetCount)
            {
                _completedCount++;
                if (_completedCount == _targetCount)
                {
                    Console.WriteLine($"Congratulations! You completed '{GoalName}' and earned {Points + _bonus} points!");
                    return Points + _bonus;
                }
                else
                {
                    Console.WriteLine($"You recorded an event for '{GoalName}' and earned {Points} points. You are {_completedCount}/{_targetCount} times complete.");
                    return Points;
                }
            }
            else
            {
                Console.WriteLine($"The checklist goal '{GoalName}' is already completed.");
                return 0;
            }
        }

        public override bool IsComplete() => _completedCount >= _targetCount;

        public override string GetDetails() => $"[{_completedCount}/{_targetCount}] {GoalName} ({Description}) {(IsComplete() ? "(Completed)" : "")}";

        public override string GetStringRepresentation() =>
            $"ChecklistGoal|{GoalName}|{Description}|{Points}|{_bonus}|{_targetCount}|{_completedCount}";
    }
}

// eternal goal is never completed but will still give points
// abstract Goal will
// name:
// Description:
// Points
// simple goal
// menu shows: 1.create new goal 2. list goals 3. save goals 4. load goal 5. record event 6. quit
// abstract class Goal