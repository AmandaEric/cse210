using System;
using System.Runtime.CompilerServices;

class Program
{
    static void Main(string[] args)
    {
        Journal journal = new Journal();
        PromptGenerator generator = new PromptGenerator();
        bool running = true;

        while (running)
        {
            Console.WriteLine("Journal Menu:");
            Console.WriteLine("1. Write a new entry");
            Console.WriteLine("2. Display journal");
            Console.WriteLine("3. Save journal to file");
            Console.WriteLine("4. Load journal from file");
            Console.WriteLine("5. Exit");
            Console.Write("Choose a menu number: ");
            string input = Console.ReadLine();
            Console.WriteLine();


            switch (input)
            {
                case "1":
                    string prompt = generator.GetRandomPrompt();
                    Console.WriteLine("Prompt: " + prompt);
                    Console.Write("Your response: ");
                    string response = Console.ReadLine();
                    string date = DateTime.Now.ToString("yyyy-MM-dd");

                    Entry entry = new Entry(date, prompt, response);
                    journal.AddEntry(entry);
                    Console.WriteLine("Entry added.\n");
                    break;

                case "2":
                    journal.DisplayEntries();
                    break;

                case "3":
                    Console.Write("Enter filename to save: ");
                    string saveFile = Console.ReadLine();
                    journal.SaveToFile(saveFile);
                    break;

                case "4":
                    Console.Write("Enter filename to load: ");
                    string loadFile = Console.ReadLine();
                    journal.LoadFromFile(loadFile);
                    break;

                case "5":
                    running = false;
                    Console.WriteLine("Goodbye!");
                    break;

                default:
                    Console.WriteLine("Nope.\n");
                    break;
            }
        }
        Entry e = new Entry("", "", "");
        Console.WriteLine(e.ToString());
        public override string ToString()
    {
        return "";

    }
}

public class Entry
{
    string _date;
    string _prompt;
    string _response;

    public Entry(string date, string prompt, string response)
    {
        _date = date;
        _prompt = prompt;
        _response = response;
    }

     public string GetDisplay()
    {
        return $"Date: {_date}\nPrompt: {_prompt}\nResponse: {_response}\n";
    }

     public string GetSaveFormat()
    {
        return $"{_date}~|~{_prompt}~|~{_response}";
    }

     public static Entry FromSavedString(string line)
    {
        string[] parts = line.Split("~|~");
        return new Entry(parts[0], parts[1], parts[2]);
    }
}

public class PromptGenerator
{
    List<string> prompts = new List<string>
    {
        "Who was the most interesting person I interacted with today?",
        "What was the best part of my day?",
        "How did I see the hand of the Lord in my life today?",
        "What was the strongest emotion I felt today?",
        "If I had one thing I could do over today, what would it be?"
    };

    Random random = new Random();

     public string GetRandomPrompt()
    {
        int index = random.Next(prompts.Count);
        return prompts[index];
    }
}

public class Journal
{
    List<Entry> entries = new List<Entry>();

     public void AddEntry(Entry entry)
    {
        entries.Add(entry);
    }

    public void DisplayEntries()
{
    if (entries.Count == 0)
    {
        Console.WriteLine("No entries!\n");
        return;
    }

    foreach (Entry entry in entries)
    {
        Console.WriteLine(entry.GetDisplay());
    }
}

     public void SaveToFile(string filename)
    {
        using (StreamWriter writer = new StreamWriter(filename))
        {
            foreach (Entry entry in entries)
            {
                writer.WriteLine(entry.GetSaveFormat());
            }
        }
        Console.WriteLine("Saved.\n");
    }

     public void LoadFromFile(string filename)
    {
        if (!File.Exists(filename))
        {
            Console.WriteLine("Does not Exist.\n");
            return;
        }

        entries.Clear();
        string[] lines = File.ReadAllLines(filename);

        foreach (string line in lines)
        {
            entries.Add(Entry.FromSavedString(line));
        }

        Console.WriteLine("Journal loaded.\n");
    }
}

// class Main
// {
//     static void main()
//     {
    //     Journal journal = new Journal();
    //     PromptGenerator generator = new PromptGenerator();
    //     bool running = true;

    //     while (running)
    //     {
    //         Console.WriteLine("Journal Menu:");
    //         Console.WriteLine("1. Write a new entry");
    //         Console.WriteLine("2. Display journal");
    //         Console.WriteLine("3. Save journal to file");
    //         Console.WriteLine("4. Load journal from file");
    //         Console.WriteLine("5. Exit");
    //         Console.Write("Choose a menu number: ");
    //         string input = Console.ReadLine();
    //         Console.WriteLine();

    //         switch (input)
    //         {
    //             case "1":
    //                 string prompt = generator.GetRandomPrompt();
    //                 Console.WriteLine("Prompt: " + prompt);
    //                 Console.Write("Your response: ");
    //                 string response = Console.ReadLine();
    //                 string date = DateTime.Now.ToString("yyyy-MM-dd");

    //                 Entry entry = new Entry(date, prompt, response);
    //                 journal.AddEntry(entry);
    //                 Console.WriteLine("Entry added.\n");
    //                 break;

    //             case "2":
    //                 journal.DisplayEntries();
    //                 break;

    //             case "3":
    //                 Console.Write("Enter filename to save: ");
    //                 string saveFile = Console.ReadLine();
    //                 journal.SaveToFile(saveFile);
    //                 break;

    //             case "4":
    //                 Console.Write("Enter filename to load: ");
    //                 string loadFile = Console.ReadLine();
    //                 journal.LoadFromFile(loadFile);
    //                 break;

    //             case "5":
    //                 running = false;
    //                 Console.WriteLine("Goodbye!");
    //                 break;

    //             default:
    //                 Console.WriteLine("Nope.\n");
    //                 break;
    //         }
    //     }
    // }
// }