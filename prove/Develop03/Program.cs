// using System;

// class Program
// {
//     static void Main(string[] args)
//     {
//         Console.WriteLine("Hello Develop03 World!");
//     }
// }
using System;

abstract class Activity
{
    private string _name;
    private string _description;
    private int _duration;

    public Activity(string name, string description)
    {
        _name = name;
        _description = description;
    }

    public void Start()
    {
        Console.Clear();
        Console.WriteLine($"--- {_name} ---\n");
        Console.WriteLine(_description);
        Console.Write("\nEnter duration in seconds: ");
        _duration = int.Parse(Console.ReadLine());

        Console.WriteLine("\nGet ready...");
        ShowSpinner(3);
    }

    public void End()
    {
        Console.WriteLine("\nGood job!");
        Console.WriteLine($"You have completed the {_name} activity for {_duration} seconds.");
        ShowSpinner(3);
    }

    public int GetDuration()
    {
        return _duration;
    }

    private void ShowSpinner(int seconds)
    {
        for (int i = 0; i < seconds; i++)
        {
            Console.Write(".");
            Thread.Sleep(1000);
        }
        Console.WriteLine();
    }

    protected void ShowCountdown(int seconds)
    {
        for (int i = seconds; i > 0; i--)
        {
            Console.Write(i + " ");
            Thread.Sleep(1000);
        }
        Console.WriteLine();
    }

    public abstract void Run();
}

// ---------------------------

class BreathingActivity : Activity
{
    public BreathingActivity() : base("Breathing Activity",
        "This activity will help you relax by walking you through breathing in and out slowly.\nClear your mind and focus on your breathing.") {}

    public override void Run()
    {
        Start();
        int time = 0;
        while (time < GetDuration())
        {
            Console.Write("\nBreathe in... ");
            ShowCountdown(3);
            time += 3;
            if (time >= GetDuration()) break;

            Console.Write("Breathe out... ");
            ShowCountdown(3);
            time += 3;
        }
        End();
    }
}

// ---------------------------

class ReflectionActivity : Activity
{
    private List<string> _prompts = new List<string>
    {
        "Think of a time when you stood up for someone else.",
        "Think of a time when you did something really difficult.",
        "Think of a time when you helped someone in need.",
        "Think of a time when you did something truly selfless."
    };

    private List<string> _questions = new List<string>
    {
        "Why was this experience meaningful to you?",
        "Have you ever done anything like this before?",
        "How did you get started?",
        "How did you feel when it was complete?",
        "What made this time different than other times?",
        "What could you learn from this experience?",
        "How can you apply this to your life?",
        "What did you learn about yourself?"
    };

    private Random _rand = new Random();

    public ReflectionActivity() : base("Reflection Activity",
        "This activity will help you reflect on times in your life when you have shown strength and resilience.") {}

    public override void Run()
    {
        Start();

        Console.WriteLine("\nPrompt:");
        Console.WriteLine($"--> {_prompts[_rand.Next(_prompts.Count)]}");
        ShowSpinner(3);

        int elapsed = 0;
        while (elapsed < GetDuration())
        {
            string question = _questions[_rand.Next(_questions.Count)];
            Console.WriteLine($"\n{question}");
            ShowSpinner(4);
            elapsed += 4;
        }

        End();
    }
}

// ---------------------------

class ListingActivity : Activity
{
    private List<string> _prompts = new List<string>
    {
        "Who are people that you appreciate?",
        "What are personal strengths of yours?",
        "Who are people that you have helped this week?",
        "When have you felt the Holy Ghost this month?",
        "Who are some of your personal heroes?"
    };

    private Random _rand = new Random();

    public ListingActivity() : base("Listing Activity",
        "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.") {}

    public override void Run()
    {
        Start();
        Console.WriteLine("\nPrompt:");
        Console.WriteLine($"--> {_prompts[_rand.Next(_prompts.Count)]}");
        Console.WriteLine("Start listing items (press Enter after each one):");
        ShowCountdown(5);

        int count = 0;
        DateTime endTime = DateTime.Now.AddSeconds(GetDuration());

        while (DateTime.Now < endTime)
        {
            if (Console.KeyAvailable)
            {
                Console.ReadLine();
                count++;
            }
        }

        Console.WriteLine($"\nYou listed {count} items.");
        End();
    }
}

// ---------------------------

class Program
{
    static void Main(string[] args)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Mindfulness App");
            Console.WriteLine("1. Breathing Activity");
            Console.WriteLine("2. Reflection Activity");
            Console.WriteLine("3. Listing Activity");
            Console.WriteLine("4. Quit");
            Console.Write("Choose an option: ");
            string choice = Console.ReadLine();

            Activity activity = null;

            switch (choice)
            {
                case "1":
                    activity = new BreathingActivity();
                    break;
                case "2":
                    activity = new ReflectionActivity();
                    break;
                case "3":
                    activity = new ListingActivity();
                    break;
                case "4":
                    return;
                default:
                    Console.WriteLine("Invalid choice.");
                    Thread.Sleep(1000);
                    continue;
            }

            activity.Run();
        }
    }
}