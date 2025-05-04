using System;

class Program
{
    static void Main(string[] args)
    {
        
        List<int> numbers = new List<int>();

        int userInput;
        do
        {
            Console.Write("Enter a number (0 to stop): ");
            userInput = int.Parse(Console.ReadLine());

            if (userInput != 0)
            {
                numbers.Add(userInput);
            }

        } while (userInput != 0);

        int sum = 0;
        foreach (int number in numbers)
        {
            sum += number;
        }
        Console.WriteLine("Sum: " + sum);

        double average = 0;
        if (numbers.Count > 0)
        {
            average = (double)sum / numbers.Count;
        }
        Console.WriteLine("Average: " + average);

        int max = int.MinValue;
        foreach (int number in numbers)
        {
            if (number > max)
            {
                max = number;
            }
        }
        Console.WriteLine("Maximum: " + max);
    }
}