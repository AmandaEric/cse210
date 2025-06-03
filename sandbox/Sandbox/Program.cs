// using System;

// class Program
// {
//     static void Main(string[] args)
//     {
//         Console.WriteLine("Hello Sandbox World!");
//     }
// }

List<string> animals = new List<string>();
// List<int> debits = new List<int>();
// need to defind what type of variable the inputs are string etc.
animals.Add("Cow");
animals.Add("Wallaby");
animals.Add("Pig");
animals.Add("Donkey");

foreach (string animal in animals)
{
    Console.WriteLine(animal);
}
Console.WriteLine(animals.Count);

// Python Function Definition
// def add_numbers(a,b);
//  sum = a + B
//  return sum

int AddNumbers(int a, int b)
{
    int sum = a + b;
    return sum;
}
Console.WriteLine(AddNumbers(1,2));