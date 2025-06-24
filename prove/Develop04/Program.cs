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
        Console.WriteLine($"--{_name}---\n");
        Console.WriteLine(_description);
        Console.Write("\nEnter duration in seconds: ");
        _duration = int.Parse(Console.ReadLine());

            }
}