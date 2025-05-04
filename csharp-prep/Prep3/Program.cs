using System;

class Program
{
    static void Main(string[] args)
    {
        Random randomGenerator = new Random();

        int magicNumber = randomGenerator.Next(1, 101);

        // Console.WriteLine("Enter the mgic number:");
        // int magicNumber = int.Parse(Console.ReadLine());
        
        int guess = -1;

        while (guess != magicNumber)
        {
            Console.WriteLine("Guess the magic number:");
            int guess = int.Parse(Console.ReadLine());

            if(guess < magicNumber)
            {
                Console.WriteLine("Higher");
            }
            else if (guess > magicNumber)
            {
            Console.WriteLine("Lower");
            }
            else
            {
            Consle.WriteLine("You guessed it!")
            }
        }
    }
}